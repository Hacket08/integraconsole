using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
public partial class frmPaySlipParameter : Form
{
    public static int _RequestID;
    public static string _QueryGenerated;
    public frmPaySlipParameter()
    {
        InitializeComponent();
    }

    private void frmPaySlipParameter_Load(object sender, EventArgs e)
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
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        if (rbStandard.Checked == true)
        {
            clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payslip Global.rpt";
        }

        if (rbPerGroup.Checked == true)
        {
            clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payslip Global Per Group.rpt";
        }

        if (rbDetailed.Checked == true)
        {
            clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Payslip.rpt";

            if(txtEmployeeFrom.Text == "")
            {
                MessageBox.Show("You are generating Detailed Payslip Per EMployee, Please Define Employee No.");
                return;
            }
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



        _QueryGenerated = sqlPaySlipOutput(_Branch,
                                                cboPayrolPeriod.Text,
                                                clsDeclaration.sConfiLevel,
                                                clsDeclaration.sConfiLevelSelection,
                                                clsDeclaration.sFilterByEmployee,
                                                txtEmployeeFrom.Text,
                                                txtEmployeeTo.Text);

        clsDeclaration.bView = true;


        frmReportDisplay frmPaySlip = new frmReportDisplay();



        if (rbDetailed.Checked == false)
        {
            clsDeclaration.sReportTag = 21;
            frmReportDisplay._RequestType = "Payslip";
            frmReportDisplay._QueryGenerated = _QueryGenerated;
        }
        else
        {
            clsDeclaration.sReportTag = 26;
            frmReportDisplay._RequestType = "DetiledPayslip";
            clsDeclaration.sPayrollPeriod = cboPayrolPeriod.Text;
            clsDeclaration.sEmployeeNo = txtEmployeeFrom.Text;
        }




        frmPaySlip.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmPaySlip.Show();

        //Close();

    }



    private string sqlPaySlipOutput(string _refBranch = "",
        string _refPayrollPeriod = "", string _refConfiLevel = "",
        string _refConfiSelection = "", bool _refFilterByEmployee = false,
        string _refEmployeeFrom = "", string _refEmployeeTo = "")
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
                                        SELECT
		                                (SELECT Z.CompanyName  FROM OCMP Z WHERE Z.Active = 1 AND Z.CompanyCode = LEFT(@Branch,4)) AS CompanyName,
                                        A.PayrollPeriod ,
                                        A.EmployeeNo ,
                                        B.EmployeeName, 
                                        C.BCode,
                                        C.BName, 
                                        D.DateOne,
                                        D.DateTwo,
                                        A.DailyRate,
                                        A.BasicPay,
                                        A.OTPay,
                                        A.OtherIncome - ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('7-411','7-403') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS OtherIncome,
                                        A.SSSEmployee,
                                        A.PhilHealthEmployee,
                                        A.PagIbigEmployee,
                                        A.WitholdingTax,
                                        A.Gross,
                                        A.TotalDeductions,
                                        A.NetPay,
                                        A.TotalDays,
                                        A.SPLPay,
                                        A.LEGPay,
                                        A.SUNPay,
										B.ConfiLevel,
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode = '7-403' AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Allowance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode = '7-411' AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Load Allowance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode = '8-521' AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [MP2],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode = '8-502' AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [SSSLoan],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-502',A.PayrollPeriod) Z) AS [SSSLoanBalance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode = '8-504' AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [PagibigLoan],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-504',A.PayrollPeriod) Z) AS [PagibigLoanBalance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('8-503','8-516') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [CalamityLoan],
                                        ((SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-503',A.PayrollPeriod) Z) + 
										(SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-516',A.PayrollPeriod) Z)) AS [CalamityLoanBalance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('8-513') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Advances],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-513',A.PayrollPeriod) Z) AS [AdvancesBalance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('8-512') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Lending],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-512',A.PayrollPeriod) Z) AS [LendingBalance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('8-510') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Applicance],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-510',A.PayrollPeriod) Z) AS [ApplicanceBalance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('8-511') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Motorcycle],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-511',A.PayrollPeriod) Z) AS [MotorcycleBalance],


                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('8-514') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Furniture],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-514',A.PayrollPeriod) Z) AS [FurnitureBalance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('8-515') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Cellphone],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-515',A.PayrollPeriod) Z) AS [CellphoneBalance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('8-518') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Computer],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-518',A.PayrollPeriod) Z) AS [ComputerBalance],
                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode IN ('8-519') AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [SpareParts],
                                        (SELECT Z.Balance FROM dbo.[fnGetBalance](A.EmployeeNo,'8-519',A.PayrollPeriod) Z) AS [SparePartsBalance],


                                        ISNULL((SELECT SUM(Z.Amount) FROM vwsPayrollDetails Z INNER JOIN vwsAccountCode ZZ ON Z.AccountCode = ZZ.AccountCode
										WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode NOT IN ('8-511','8-510','8-512','8-513','8-503','8-516','8-504' ,'8-502','8-514','8-515','8-518','8-519') 
										AND ZZ.AccountType IN (7,8)
										AND Z.PayrollPeriod =  A.PayrollPeriod),0) AS [Others],

		                C.BranchCode,
		                C.BranchName,
		                CASE WHEN B.ConfiLevel IN ('2') THEN 'Managers' ELSE 'Rank And File' END AS [GroupConfi],
		                CASE WHEN B.Category = 1 THEN 'Daily' ELSE 'Monthly' END AS Category,
		                C.DepName,
		                B.Company

                                        FROM vwsPayrollHeader A INNER JOIN 
	                                            vwsEmployees B ON A.EmployeeNo = B.EmployeeNo LEFT JOIN 
	                                            vwsDepartmentList C ON A.Department = C.DepartmentCode LEFT JOIN 
	                                            vwsPayrollPeriod D ON A.PayrollPeriod = D.PayrollPeriod
                                        WHERE 
                                            B.ConfiLevel IN (" + _refConfiLevel  + @") AND
	                                        A.PayrollType = 'PAYROLL' AND
	                                        A.PayrollPeriod = @PayrollPeriod AND
                                            C.BCode LIKE '%' + @Branch + '%'           
                        ";


        if (rbBank.Checked == true)
        {
            _sqlSyntax = _sqlSyntax + @"AND ISNULL(A.BankCode,'') <> ''";
        }
        if (rbCash.Checked == true)
        {
            _sqlSyntax = _sqlSyntax + @"AND ISNULL(A.BankCode,'') = ''";
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
    private static double genBalance(string _Employee, string _LoanAccount, string _PayrollPeriod)
    {
        // SSS Loan
            string _ConCompany = clsDeclaration.sSystemConnection;
            string _sqlDeduction = @"SELECT * FROM dbo.[fnGetBalance]('" + _Employee + @"','" + _LoanAccount + @"','" + _PayrollPeriod + @"')
                                                  ";


        return clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlDeduction, "Balance");
    }
    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

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
}
