using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmPerBranchParameter : Form
{

    public static string _RequestType;
    public static string _QueryGenerated;
    public frmPerBranchParameter()
    {
        InitializeComponent();
    }

    private void frmPerBranchParameter_Load(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        pnlEmployee.Enabled = false;
        chkFilterPerEmployee.Checked = false;
        clsDeclaration.sFilterByEmployee = false;

        DataTable _DataTable;
        string _SQLSyntax;

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

        clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Loan Summary Per Employee Detailed.rpt";

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

        _QueryGenerated = sqlLoanLedger(_Branch,
                                                clsDeclaration.sConfiLevel,
                                                clsDeclaration.sConfiLevelSelection,
                                                clsDeclaration.sFilterByEmployee,
                                                txtEmployeeFrom.Text,
                                                txtEmployeeTo.Text);

        clsDeclaration.bView = true;
        Close();
    }



    private string sqlLoanLedger(string _refBranch = "", string _refConfiLevel = "",
        string _refConfiSelection = "", bool _refFilterByEmployee = false,
        string _refEmployeeFrom = "", string _refEmployeeTo = "")
    {


        string _sqlSyntax = "";

        _sqlSyntax = @"
                DECLARE @Branch NVARCHAR(25)
                DECLARE @ByEmployee INT
                DECLARE @EmployeeNoFrom NVARCHAR(25)
                DECLARE @EmployeeNoTo NVARCHAR(25)

                SET @Branch  = '" + _refBranch + @"'
                SET @EmployeeNoFrom = '" + _refEmployeeFrom + @"'
                SET @EmployeeNoTo = '" + _refEmployeeTo + @"'

                SELECT * FROM (
                                        SELECT A.EmployeeNo, B.EmployeeName, A.AccountCode, C.AccountDesc, A.LoanRefNo, A.LoanAmount, A.LoanDate
                                        , D.Type, D.PaymentDate, D.ORNo, D.Amount, D.Remarks, E.BCode, E.BName, B.ConfiLevel
                                        FROM vwsLoanFile A 
                                        INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
                                        INNER JOIN vwsAccountCode C ON A.AccountCode = C.AccountCode
                                        INNER JOIN vwsLoanPayment D ON A.EmployeeNo = D.EmployeeNo AND A.AccountCode = D.AccountCode AND A.LoanRefNo = D.LoanRefNo
                                        LEFT JOIN vwsDepartmentList E ON B.Department = E.DepartmentCode
                                        WHERE B.ConfiLevel IN (" + _refConfiLevel + @") AND
                                        E.BCode LIKE '%' + @Branch + '%'   
                              ";
        
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
