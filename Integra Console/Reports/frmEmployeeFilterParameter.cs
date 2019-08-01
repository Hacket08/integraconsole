using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmEmployeeFilterParameter : Form
{
    public static int _RequestID;
    public frmEmployeeFilterParameter()
    {
        InitializeComponent();
    }

    private void frmEmployeeFilterParameter_Load(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        pnlEmployee.Enabled = false;
        chkFilterPerEmployee.Checked = false;
        clsDeclaration.sFilterByEmployee = false;

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());
        }

        _SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }

        chkRankAndFile.Checked = true;
        chkSupervisory.Checked = true;
        chkManagerial.Checked = true;



        rbStandard.Checked = true;
        rbAll.Checked = true;
        rbcAll.Checked = true;
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

    private void chkFilterPerEmployee_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFilterPerEmployee.Checked == true)
        {
            pnlEmployee.Enabled = true;
            clsDeclaration.sFilterByEmployee = true;
        }
        else
        {
            pnlEmployee.Enabled = false;
            clsDeclaration.sFilterByEmployee = false;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Payroll Period Not Define!");
            return;
        }

        string _Branch = "";
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }

        clsDeclaration.sConfiLevelSelection = "0,1,2";
        if (chkRankAndFile.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0";
        }
        if (chkSupervisory.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "1";
        }
        if (chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "2";
        }
        if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0,1";
        }
        if (chkRankAndFile.Checked == true && chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0,2";
        }
        if (chkSupervisory.Checked == true && chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "1,2";
        }
        if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true && chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0,1,2";
        }

   
        clsDeclaration.sPayrollPeriod = cboPayrolPeriod.Text;
        clsDeclaration.sEmployeeFrom = txtEmployeeFrom.Text;
        clsDeclaration.sEmployeeTo = txtEmployeeTo.Text;
        clsDeclaration.sPayrollPeriod = cboPayrolPeriod.Text;


        int _Category = -1;
        if(rbcAll.Checked == true)
        {
            _Category = -1;
        }
        else
        {
            if(rbDaily.Checked == true)
            {
                _Category = 1;
            }
            else
            {
                _Category = 0;
            }
        }


        int _PayrollMode = -1;
        if (rbAll.Checked == true)
        {
            _PayrollMode = -1;
        }
        else
        {
            if (rbBank.Checked == true)
            {
                _PayrollMode = 0;
            }
            else
            {
                _PayrollMode = 1;
            }
        }

        switch (_RequestID)
        {
            case 7:
                clsDeclaration.sReportTag = 7;
                if (chkWithoutBankAccountNumber.Checked == true)
                {
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Cash Report.rpt";
                }
                else
                {
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Bank Report.rpt";
                }



                clsDeclaration.sQueryString = sqlQueryOutput(_Branch,
                                        cboPayrolPeriod.Text,
                                        clsDeclaration.sConfiLevel,
                                        clsDeclaration.sConfiLevelSelection,
                                        clsDeclaration.sFilterByEmployee,
                                        txtEmployeeFrom.Text,
                                        txtEmployeeTo.Text, _Category, _PayrollMode, chkWithoutBankAccountNumber.Checked);

                frmReport frmReport = new frmReport();
                frmReport._sqlCrystal = clsDeclaration.sQueryString;
                frmReport.MdiParent = ((frmMainWindow)(this.MdiParent));
                frmReport.Show();

                break;
        }
    }



    private string sqlQueryOutput(string _refBranch = "",
    string _refPayrollPeriod = "", string _refConfiLevel = "",
    string _refConfiSelection = "", bool _refFilterByEmployee = false,
    string _refEmployeeFrom = "", string _refEmployeeTo = "", int refCategory = -1
        , int refPayrollMode = -1, bool refBankAccountNumber = false)
    {


        string _sqlSyntax = "";

        _sqlSyntax = @"


                DECLARE @Branch NVARCHAR(25)
                DECLARE @PayrollPeriod NVARCHAR(25)
                DECLARE @ByEmployee INT
                DECLARE @EmployeeNoFrom NVARCHAR(25)
                DECLARE @EmployeeNoTo NVARCHAR(25)


                SET @Branch  = '" + _refBranch + @"'
                SET @PayrollPeriod = '" + _refPayrollPeriod + @"'
                SET @EmployeeNoFrom = '" + _refEmployeeFrom + @"'
                SET @EmployeeNoTo = '" + _refEmployeeTo + @"'


                        SELECT * FROM (
                              SELECT B.LastName, B.FirstName, B.BankAccountNo, A.NetPay ,
CASE WHEN B.ConfiLevel IN ('2') THEN 'Managers' ELSE 'Rank And File' END AS [GroupConfi],
CASE WHEN B.Category = 1 THEN 'Daily' ELSE 'Monthly' END AS Category,
C.DepName,
B.Company, B.AsBranchCode, B.EmpPosition,
B.ConfiLevel,
B.EmployeeName, A.PayrollPeriod

FROM vwsPayrollHeader A 
INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
LEFT JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode

                                        WHERE 
                                            B.ConfiLevel IN (" + _refConfiLevel + @") AND
	                                        A.PayrollType = 'PAYROLL' AND
	                                        A.PayrollPeriod = @PayrollPeriod AND
                                            C.BCode LIKE '%' + @Branch + '%'      ";


        if (refCategory  != -1)
        {
            _sqlSyntax = _sqlSyntax + @"AND B.Category = '" + refCategory + @"'";
        }

        if (refPayrollMode != -1)
        {
            _sqlSyntax = _sqlSyntax + @"AND B.PayrollMode = '" + refPayrollMode + @"'";
        }


        if (refPayrollMode != -1)
        {
            _sqlSyntax = _sqlSyntax + @"AND B.PayrollMode = '" + refPayrollMode + @"'";
        }

        if (refBankAccountNumber == true)
        {
            _sqlSyntax = _sqlSyntax + @"AND ISNULL(A.BankAccountNo,'') = ''";
        }
        else
        {
            _sqlSyntax = _sqlSyntax + @"AND ISNULL(A.BankAccountNo,'') <> ''";
        }


        _sqlSyntax = _sqlSyntax + @")   ZZ
                        WHERE ZZ.ConfiLevel IN (" + _refConfiSelection + @") ";

        if (_refFilterByEmployee == true)
        {
            _sqlSyntax = _sqlSyntax + @"
                        AND ZZ.EmployeeNo Between @EmployeeNoFrom AND @EmployeeNoTo 
                                                                ";
        }


        _sqlSyntax = _sqlSyntax + @"
                        ORDER BY ZZ.EmployeeName
                                ";

        return _sqlSyntax;
    }
}