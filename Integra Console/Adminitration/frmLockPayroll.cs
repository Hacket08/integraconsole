using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

public partial class frmLockPayroll : Form
{
    public frmLockPayroll()
    {
        InitializeComponent();
    }

    private void frmLockPayroll_Load(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        //_SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        //cboPayrolPeriod.Items.Clear();
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboPayrolPeriod.Items.Add(row[0].ToString());

        //}

        _SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }


        _SQLSyntax = "SELECT DISTINCT CONCAT(A.DepCode, ' - ', A.DepName) FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboPosition.Items.Clear();
        cboPosition.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPosition.Items.Add(row[0].ToString());
        }

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT CONCAT(A.BCode,' - ',A.BName) AS Branch FROM dbo.[vwsDepartmentList] A WHERE A.Area = '" + cboArea.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboBranch.Items.Clear();
        cboBranch.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboBranch.Items.Add(row[0].ToString());
        }
    }

    private void btnLocked_Click(object sender, EventArgs e)
    {
        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }

        string _BPosition = "";
        if (cboPosition.Text == "")
        {
            _BPosition = "";
        }
        else
        {
            _BPosition = cboPosition.Text.Substring(0, 4);
        }

        string _Status = "N";
        string _BLocked = "";
        if (btnLocked.Text == "Locked")
        {
            _Status = "Y";
            _BLocked = "1";
        }
        else
        {
            _Status = "N";
            _BLocked = "0";
        }

        string _InsertData = @"
                                        IF EXISTS(SELECT 'TRUE' FROM [PayrollLocker] A  WHERE A.[PayrollPeriod] = '" + cboPayrolPeriod.Text + @"'
                                                                                  AND A.[Branch] LIKE '%" + _BCode + @"%'
                                                                                  AND A.[Position] LIKE '%" + _BPosition + @"%')
                                        BEGIN
                                              UPDATE A
                                                SET [IsLocked] = '" + _BLocked + @"'
                                                FROM [PayrollLocker] A  WHERE A.[PayrollPeriod] = '" + cboPayrolPeriod.Text + @"'
                                                                                  AND A.[Branch] LIKE '%" + _BCode + @"%'
                                                                                  AND A.[Position] LIKE '%" + _BPosition + @"%'
                                        END
                                        ELSE
                                        BEGIN
                                                             INSERT INTO PayrollLocker ([PayrollPeriod]
        			                                        ,[Branch],[Position]
        			                                        ,[IsLocked]) VALUES ('" + cboPayrolPeriod.Text + @"','" + _BCode + @"','" + _BPosition + @"','" + _BLocked + @"')
                                        END
                                      ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _InsertData);


        string _sysDB = ConfigurationManager.AppSettings["DBName"];
        string _sysDBServer = ConfigurationManager.AppSettings["DBServer"];


        string _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
        DataTable _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        foreach (DataRow row in _DataTable.Rows)
        {
            string _strCompServer = row[3].ToString();
            string _strCompany = row[4].ToString();

            _InsertData = @"
                                     DECLARE @Payrollperiod as nvarchar(30)
                                        SET @Payrollperiod = '" + cboPayrolPeriod.Text + @"'

                                        UPDATE A SET A.Validated = '" + _Status + @"' FROM [" + _strCompServer + @"].[" + _strCompany + @"].dbo.[PayrollHeader] A WHERE A.PayrollPeriod = @Payrollperiod
                                        AND A.Branch IN (SELECT B.Branch FROM [" + _sysDBServer + @"].[" + _sysDB + @"].dbo.[PayrollLocker] B  
                                        WHERE B.PayrollPeriod = @Payrollperiod AND B.IsLocked = '" + _BLocked + @"' AND B.Branch = '" + _BCode + @"')
                                      ";




            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _InsertData);
        }

        MessageBox.Show("Branch " + _BCode + " is Lock for the Payroll Period " + cboPayrolPeriod.Text);
        Locker();
    }

    private void Locker()
    {
        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }

        string _BPosition = "";
        if (cboPosition.Text == "")
        {
            _BPosition = "";
        }
        else
        {
            _BPosition = cboPosition.Text.Substring(0, 4);
        }

        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT 'TRUE' FROM [PayrollLocker] A WHERE A.[PayrollPeriod] = '" + cboPayrolPeriod.Text + "' AND A.[Branch] LIKE '%" + _BCode + "%' AND [Position] LIKE '%" + _BPosition + "%'   AND [IsLocked] = '1'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);


        if (_DataTable.Rows.Count == 0)
        {
            btnLocked.Text = "Locked";
        }
        else
        {
            btnLocked.Text = "Unlock";
        }

    }


    private void cbo_Leave(object sender, EventArgs e)
    {
        Locker();
    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        if (cboBranch.Text == "")
        {
            return;
        }

        if (chkAll.CheckState == CheckState.Checked)
        {

            _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        }
        else
        {
            _SQLSyntax = @"SELECT DISTINCT A.[PayrollPeriod],A.[DateOne],A.[DateTwo],A.[IsLocked] 
FROM vwsPayrollPeriod A LEFT JOIN PayrollLocker B ON A.PayrollPeriod = B.PayrollPeriod 
WHERE (B.Branch = '" + cboBranch.Text.Substring(0, 8) + @"' OR B.Branch IS NULL) AND (B.IsLocked IS NULL OR B.IsLocked = 0)
						ORDER BY A.[PayrollPeriod] DESC";
        }

        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        cboPayrolPeriod.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());
        }

        Locker();
    }

    private void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        if (cboBranch.Text == "")
        {
            return;
        }

        if (chkAll.CheckState == CheckState.Checked)
        {

            _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        }
        else
        {
            _SQLSyntax = @"SELECT DISTINCT A.[PayrollPeriod],A.[DateOne],A.[DateTwo],A.[IsLocked] 
            FROM vwsPayrollPeriod A LEFT JOIN PayrollLocker B ON A.PayrollPeriod = B.PayrollPeriod 
            WHERE (B.Branch = '" + cboBranch.Text.Substring(0, 8) + @"' OR B.Branch IS NULL) AND (B.IsLocked IS NULL OR B.IsLocked = 0)
						            ORDER BY A.[PayrollPeriod] DESC";
        }

        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        cboPayrolPeriod.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());
        }
    }

    private void cboPayrolPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locker();
    }
}
