using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmSILPrParameter : Form
{

    public static string _RequestType;
    public static string _QueryGenerated;
    public frmSILPrParameter()
    {
        InitializeComponent();
    }

    private void frmSILPrParameter_Load(object sender, EventArgs e)
    {
        //rbAll.Checked = true;
        //rbActive.Checked = true;


        clsDeclaration.bView = false;

        //pnlEmployee.Enabled = false;
        //chkFilterPerEmployee.Checked = false;
        clsDeclaration.sFilterByEmployee = false;

        DataTable _DataTable;
        string _SQLSyntax;

        //_SQLSyntax = "SELECT CONCAT(A.CompCode,' - ',A.CompanyName) AS Company FROM OCMP A WHERE A.Active = '1'";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        //cboCompany.Items.Clear();
        //cboCompany.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboCompany.Items.Add(row[0].ToString());
        //}

        _SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }


        _SQLSyntax = "SELECT DISTINCT A.DepCode, A.DepName FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboDepartment.Items.Clear();
        cboDepartment.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboDepartment.Items.Add(row[0].ToString() + " - " + row[1].ToString());
        }

        chkRankAndFile.Checked = true;
        chkSupervisory.Checked = true;
        chkManagerial.Checked = true;
        //chkFiltered.Checked = true;

        //switch (clsDeclaration.sReportID)
        //{
        //    case 3:
        //        lblLabel.Text = "Date Hired";
        //        break;
        //    case 4:
        //    case 5:
        //        lblLabel.Text = "Date";
        //        break;

        //}
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
        //if (chkFilterPerEmployee.Checked == true)
        //{
        //    pnlEmployee.Enabled = true;
        //    clsDeclaration.sFilterByEmployee = true;
        //}
        //else
        //{
        //    pnlEmployee.Enabled = false;
        //    clsDeclaration.sFilterByEmployee = false;
        //}
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }

    //private void btnUpload_Click(object sender, EventArgs e)
    //{

    //    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Loan Summary Per Employee Detailed.rpt";

    //    string _Branch = "";
    //    if (cboBranch.Text == "")
    //    {
    //        _Branch = "";
    //    }
    //    else
    //    {
    //        _Branch = cboBranch.Text.Substring(0, 8);
    //    }

    //    clsDeclaration.sConfiLevelSelection = "0,1,2";
    //    if (chkRankAndFile.Checked == true)
    //    {
    //        clsDeclaration.sConfiLevelSelection = "0";
    //    }
    //    if (chkSupervisory.Checked == true)
    //    {
    //        clsDeclaration.sConfiLevelSelection = "1";
    //    }
    //    if (chkManagerial.Checked == true)
    //    {
    //        clsDeclaration.sConfiLevelSelection = "2";
    //    }
    //    if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true)
    //    {
    //        clsDeclaration.sConfiLevelSelection = "0,1";
    //    }
    //    if (chkRankAndFile.Checked == true && chkManagerial.Checked == true)
    //    {
    //        clsDeclaration.sConfiLevelSelection = "0,2";
    //    }
    //    if (chkSupervisory.Checked == true && chkManagerial.Checked == true)
    //    {
    //        clsDeclaration.sConfiLevelSelection = "1,2";
    //    }
    //    if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true && chkManagerial.Checked == true)
    //    {
    //        clsDeclaration.sConfiLevelSelection = "0,1,2";
    //    }

    //    _QueryGenerated = sqlLoanLedger(_Branch,
    //                                            clsDeclaration.sConfiLevel,
    //                                            clsDeclaration.sConfiLevelSelection,
    //                                            clsDeclaration.sFilterByEmployee,
    //                                            txtEmployeeFrom.Text,
    //                                            txtEmployeeTo.Text);

    //    clsDeclaration.bView = true;
    //    Close();
    //}



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

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
        //if(radioButton2.Checked == true)
        //{
        //    panel1.Enabled = true;
        //}
        //else
        //{
        //    panel1.Enabled = false;
        //}
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
        //if (radioButton2.Checked == true)
        //{
        //    panel1.Enabled = true;
        //}
        //else
        //{
        //    panel1.Enabled = false;
        //}
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        #region Parameter Selection

        //string _Company = "";
        //if (cboCompany.Text == "")
        //{
        //    _Company = "";
        //}
        //else
        //{
        //    _Company = cboCompany.Text.Substring(0, 4);
        //}


        string _Branch = "";
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }

        string _Deparment = "";
        if (cboDepartment.Text == "")
        {
            _Deparment = "";
        }
        else
        {
            _Deparment = cboDepartment.Text.Substring(0, 4);
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

        //string _selActive = "0";
        //if(rbActive.Checked == true)
        //{
        //    _selActive = "0";
        //}
        //else
        //{
        //    _selActive = "1";
        //}


        //string _selAsOf = "0";
        //if (chkFiltered.Checked == true)
        //{
        //    _selAsOf = "0";
        //}
        //else
        //{
        //    _selAsOf = "1";
        //}

        #endregion

        clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payslip Global - SILP.rpt";
        clsDeclaration.sQueryString = sqlQueryOutput(_Branch, txtYear.Text, _Deparment,
                            clsDeclaration.sConfiLevel,
                            clsDeclaration.sConfiLevelSelection);




        frmReportList frmReportList = new frmReportList();
        //frmReportList._RequestType = _RequestType;
        frmReportList.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmReportList.Show();
    }



    private string sqlQueryOutput(string _refBranch = "",
                                string _refYear = "",
                                string _refDepartment = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "")
    {



        string _sqlSyntax = "";

        _sqlSyntax = @"



                DECLARE @Branch NVARCHAR(25)
                DECLARE @Year NVARCHAR(25)
                DECLARE @ByEmployee INT
                DECLARE @EmployeeNoFrom NVARCHAR(25)
                DECLARE @EmployeeNoTo NVARCHAR(25)


                SET @Branch  = '" + _refBranch + @"'
                SET @Year = '" + _refYear + @"'

                        SELECT * FROM (
                                        SELECT
		                                (SELECT Z.CompanyName  FROM OCMP Z WHERE Z.Active = 1 AND Z.CompanyCode = LEFT(@Branch,4)) AS CompanyName,
                                        A.Year AS PayrollPeriod ,
                                        A.EmployeeNo ,
                                        B.EmployeeName, 
                                        C.BCode,
                                        C.BName, 
                                        NULL AS DateOne,
                                        NULL AS DateTwo,
                                        B.DailyRate,
                                        0.00 AS BasicPay,
                                        0.00 AS OTPay,
                                        0.00 AS OtherIncome,
                                        0.00 AS SSSEmployee,
                                        0.00 AS PhilHealthEmployee,
                                        0.00 AS PagIbigEmployee,
                                        0.00 AS WitholdingTax,
                                        0.00 AS Gross,
                                        0.00 AS TotalDeductions,
                                        A.NetAmount AS NetPay,
                                        0.00 AS TotalDays,
                                        0.00 AS SPLPay,
                                        0.00 AS LEGPay,
                                        0.00 AS SUNPay,
										B.ConfiLevel,
                                        0.00 AS  [Allowance],
                                        0.00 AS  [Load Allowance],
                                        0.00 AS  [MP2],
                                        0.00 AS  [SSSLoan],
                                        0.00 AS  [SSSLoanBalance],
0.00 AS  [PagibigLoan],
0.00 AS  [PagibigLoanBalance],
0.00 AS  [CalamityLoan],
        0.00 AS [CalamityLoanBalance],
               0.00 AS [Advances],
               0.00 AS [AdvancesBalance],
               0.00 AS [Lending],
                                        0.00 AS [LendingBalance],
                                        0.00 AS [Applicance],
                                        0.00 AS [ApplicanceBalance],
        0.00 AS [Motorcycle],
0.00 AS [MotorcycleBalance],


                                        0.00 AS [Furniture],
                                        0.00 AS [FurnitureBalance],
                                        0.00 AS [Cellphone],
                                        0.00 AS [CellphoneBalance],
                                        0.00 AS [Computer],
                                        0.00 AS [ComputerBalance],
                                        0.00 AS [SpareParts],
                                        0.00 AS [SparePartsBalance],


                                        0.00 AS [Others],

		                C.BranchCode,
		                C.BranchName,
		                CASE WHEN B.ConfiLevel IN ('2') THEN 'Managers' ELSE 'Rank And File' END AS [GroupConfi],
		                CASE WHEN B.Category = 1 THEN 'Daily' ELSE 'Monthly' END AS Category,
                        C.DepCode AS [DepartmentCode],
		                C.DepName,
		                B.Company

                                        FROM SILPDetails A INNER JOIN 
	                                            vwsEmployees B ON A.EmployeeNo = B.EmployeeNo LEFT JOIN 
	                                            vwsDepartmentList C ON B.Department = C.DepartmentCode
                                        WHERE 
                                        B.ConfiLevel IN (" + _refConfiLevel + @") AND
	                                    A.Year = @Year AND
                                        C.BCode LIKE '%' + @Branch + '%'           
                        )   ZZ
                        WHERE ZZ.ConfiLevel IN  (" + _refConfiSelection + @") 
                        
                               ";


        if (_refDepartment != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND ZZ.[DepartmentCode] LIKE '%" + _refDepartment + @"%'";
        }



        _sqlSyntax = _sqlSyntax + @" ORDER BY ZZ.EmployeeName";
        return _sqlSyntax;
    }


    
    private void frmSILPrParameter_FormClosing(object sender, FormClosingEventArgs e)
    {
        clsDeclaration.sReportID = 0;
        clsDeclaration.sReportPath = "";
        clsDeclaration.sQueryString = "";
    }

    private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void label5_Click(object sender, EventArgs e)
    {

    }
}
