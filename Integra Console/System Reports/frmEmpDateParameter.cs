using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmEmpDateParameter : Form
{

    public static string _RequestType;
    public static string _QueryGenerated;
    public frmEmpDateParameter()
    {
        InitializeComponent();
    }

    private void frmEmpDateParameter_Load(object sender, EventArgs e)
    {
        //rbAll.Checked = true;
        rbActive.Checked = true;


        clsDeclaration.bView = false;

        //pnlEmployee.Enabled = false;
        //chkFilterPerEmployee.Checked = false;
        clsDeclaration.sFilterByEmployee = false;

        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT CONCAT(A.CompCode,' - ',A.CompanyName) AS Company FROM OCMP A WHERE A.Active = '1'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboCompany.Items.Clear();
        cboCompany.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }

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

        switch (clsDeclaration.sReportID)
        {
            case 3:
                lblLabel.Text = "Date Hired";
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                lblLabel.Text = "Date";
                break;

        }
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

        string _Company = "";
        if (cboCompany.Text == "")
        {
            _Company = "";
        }
        else
        {
            _Company = cboCompany.Text.Substring(0, 4);
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

        string _selActive = "0";
        if(rbActive.Checked == true)
        {
            _selActive = "0";
        }
        else
        {
            _selActive = "1";
        }


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

        switch(clsDeclaration.sReportID)
        {
            case 3:
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee List And Loan Summary.rpt";
                clsDeclaration.sQueryString = sqlQueryOutputLoanList(_Branch, cboArea.Text, _Deparment, _selActive, dtpDateHiredFrom.Value.ToShortDateString(), dtpDateHiredTo.Value.ToShortDateString(), _Company,
                                    clsDeclaration.sConfiLevel,
                                    clsDeclaration.sConfiLevelSelection);
                break;
            case 4:
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Loan Report - Goverment.rpt";
                clsDeclaration.sQueryString = sqlQueryOutputEmpLoanGov(_Branch, cboArea.Text, _Deparment, _selActive, dtpDateHiredFrom.Value.ToShortDateString(), dtpDateHiredTo.Value.ToShortDateString(), _Company,
                                    clsDeclaration.sConfiLevel,
                                    clsDeclaration.sConfiLevelSelection);
                break;
            case 5:
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Loan Report - Installment.rpt";
                clsDeclaration.sQueryString = sqlQueryOutputEmpLoanGov(_Branch, cboArea.Text, _Deparment, _selActive, dtpDateHiredFrom.Value.ToShortDateString(), dtpDateHiredTo.Value.ToShortDateString(), _Company,
                                    clsDeclaration.sConfiLevel,
                                    clsDeclaration.sConfiLevelSelection);
                break;
            case 6:
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Loan List Report.rpt";
                clsDeclaration.sQueryString = sqlQueryOutputEmpLoan(_Branch, cboArea.Text, _Deparment, _selActive, dtpDateHiredFrom.Value.ToShortDateString(), dtpDateHiredTo.Value.ToShortDateString(), _Company,
                                    clsDeclaration.sConfiLevel,
                                    clsDeclaration.sConfiLevelSelection);
                break;

        }





        frmReportList frmReportList = new frmReportList();
        //frmReportList._RequestType = _RequestType;
        frmReportList.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmReportList.Show();
    }



    private string sqlQueryOutputLoanList(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refDateHiredFrom = "",
                                string _refDateHiredTo = "",
                                string _refComp = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "")
    {


        string _strActive = "";
        switch (_refActive)
        {
            case "0":
                _strActive = "Active Employee List And Loan Summary";
                break;
            case "1":
                _strActive = "Resigned Employee List And Loan Summary";
                break;
            default:
                _strActive = "Employee List And Loan Summary";
                break;
        }

        string _sqlSyntax = "";

        _sqlSyntax = @"

SELECT * FROM (



SELECT A.LoanRefNo,A.LoanDate,ISNULL(A.AmountGranted,0) AS AmountGranted
                                            ,ISNULL(A.LoanAmount,0) AS LoanAmount
                                            ,ISNULL(A.Amortization,0) AS Amortization,A.StartOfDeduction,A.TermsOfPayment
                                            ,A.Terms,A.TotalCash,A.TotalPayroll,A.TotalCredit
                                            ,ISNULL(A.Rebate,0) AS Rebate
                                            ,A.Particular
                                            ,A.Brand
                                            ,ISNULL(A.LCPPrice,0) AS LCPPrice
                                            ,ISNULL(A.SpotCashAmount,0) AS SpotCashAmount
                                            ,ISNULL(A.DownPayment,0) AS DownPayment,A.Status
                                            ,CASE WHEN ISNULL(A.RebateApplication,'') = '' THEN '3' ELSE  ISNULL(A.RebateApplication,'3') END AS RebateApplication
                                            ,ISNULL(A.LoanInterest,'0') AS LoanInterest
                                            ,CASE WHEN ISNULL(A.[OrigBCode],'') = '' THEN C.BCode ELSE ISNULL(A.[OrigBCode],'') END [OrigBCode]
                                            ,CASE WHEN ISNULL(A.[OrigBName],'') = '' THEN C.BName ELSE ISNULL(A.[OrigBName],'') END [OrigBName]
                                            ,A.[CreateDate]
                                            ,A.[UpdateDate]
                                            ,A.[Company] , C.BCode, C.BName , A.RelativeName
                                            ,A.[FirstDueDate]
                                            ,A.[MonthlyDueDate]
                                            ,A.[DueDate], D.AccountCode, D.AccountDesc, B.EmployeeNo, B.EmployeeName, C.Area
											,(SELECT Z.[Balance] FROM dbo.[fnGetBalancePerEmployeeLoan](A.EmployeeNo, A.AccountCode, A.LoanRefNo) Z) AS [Balance]
											,(SELECT Z.[Total Payment] FROM dbo.[fnGetBalancePerEmployeeLoan](A.EmployeeNo, A.AccountCode, A.LoanRefNo) Z) AS [Total Payment]
, B.TerminationStatus
, B.EmpStatus
, B.ConfiLevel
, B.DateHired
, B.DateFinish
, C.DepCode AS [DepartmentCode]
, C.DepName AS [Position]
, C.BranchCode
, C.BranchName
, '" + _strActive + @"' AS [Active]
, 'DATE COVERED FROM " + _refDateHiredFrom + @" TO " + _refDateHiredTo + @"' AS [SubTitle]
, CASE WHEN " + _refActive + @" = 0 THEN 'Date Hired : ' + CONVERT(nvarchar(30), B.DateHired, 101) ELSE 'Resigned Date : ' + CONVERT(nvarchar(30), B.DateFinish, 101) END AS [Date]
							FROM [vwsLoanFile] A 
                                        INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
                                        INNER JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode     
										INNER JOIN vwsAccountCode D ON A.AccountCode = D.AccountCode  
WHERE B.ConfiLevel IN (" + _refConfiLevel + @")

) XX";


        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";

        if (_refComp != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[Company] LIKE '%" + _refComp + @"%'";
        }


        if (_refDepartment != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[DepartmentCode] LIKE '%" + _refDepartment + @"%'";
        }

        if (_refBranch != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[BCode] LIKE '%" + _refBranch + @"%'";
        }

        if (_refArea != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[AREA] LIKE '%" + _refArea + @"%'";
        }



        if (_refActive == "0")
        {
            _sqlSyntax = _sqlSyntax + @" 
                                        AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) IN (0,1,2)";



            _sqlSyntax = _sqlSyntax + @" 
                                        AND XX.DateHired BETWEEN '" + _refDateHiredFrom + @"' AND '" + _refDateHiredTo + @"' ";
        }

        if (_refActive == "1")
        {
            _sqlSyntax = _sqlSyntax + @" 
                                        AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) NOT IN (0,1,2)";



            _sqlSyntax = _sqlSyntax + @" 
                                        AND XX.DateFinish BETWEEN '" + _refDateHiredFrom + @"' AND '" + _refDateHiredTo + @"' ";
        }



        return _sqlSyntax;
    }





    private string sqlQueryOutputEmpLoanGov(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refDateHiredFrom = "",
                                string _refDateHiredTo = "",
                                string _refComp = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "")
    {


        //string _strActive = "";
        //switch (_refActive)
        //{
        //    case "0":
        //        _strActive = "Active Employee List And Loan Summary";
        //        break;
        //    case "1":
        //        _strActive = "Resigned Employee List And Loan Summary";
        //        break;
        //    default:
        //        _strActive = "Employee List And Loan Summary";
        //        break;
        //}

        string _sqlSyntax = "";

        _sqlSyntax = @"

SELECT * FROM (



SELECT A.EmployeeNo, B.EmployeeName,A.CreateDate, A.LoanRefNo, A.AccountCode, D.AccountDesc, A.LoanDate, A.AmountGranted
, A.Terms, (A.LoanAmount + ISNULL(A.LoanInterest,0)) AS LoanAmount, A.Amortization, A.StartOfDeduction
, C.AREA, C.BCode, C.BName, A.Status
, DATEADD(mm,  cast(a.Terms as int), LoanDate) Maturity
, A.DownPayment

, B.Company
, B.TerminationStatus
, B.EmpStatus
, B.ConfiLevel
, B.DateHired
, B.DateFinish
, C.DepCode AS [DepartmentCode]
, C.DepName AS [Position]
, C.BranchCode
, C.BranchName
, 'DATE COVERED FROM " + _refDateHiredFrom + @" TO " + _refDateHiredTo + @"' AS [SubTitle]
							FROM [vwsLoanFile] A 
                                        INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
                                        INNER JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode     
										INNER JOIN vwsAccountCode D ON A.AccountCode = D.AccountCode  
WHERE B.ConfiLevel IN (" + _refConfiLevel + @")
AND A.AccountCode NOT IN ('8-502','8-503','8-504','8-516','8-522')

) XX";


        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";

        if (_refComp != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[Company] LIKE '%" + _refComp + @"%'";
        }


        if (_refDepartment != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[DepartmentCode] LIKE '%" + _refDepartment + @"%'";
        }

        if (_refBranch != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[BCode] LIKE '%" + _refBranch + @"%'";
        }

        if (_refArea != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[AREA] LIKE '%" + _refArea + @"%'";
        }


        _sqlSyntax = _sqlSyntax + @" 
                                        AND XX.Status = '" + _refActive + @"'";

        _sqlSyntax = _sqlSyntax + @" 
                                        AND XX.CreateDate BETWEEN '" + _refDateHiredFrom + @"' AND '" + _refDateHiredTo + @"' ";

        return _sqlSyntax;
    }




    private string sqlQueryOutputEmpLoanIns(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refDateHiredFrom = "",
                                string _refDateHiredTo = "",
                                string _refComp = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "")
    {


        //string _strActive = "";
        //switch (_refActive)
        //{
        //    case "0":
        //        _strActive = "Active Employee List And Loan Summary";
        //        break;
        //    case "1":
        //        _strActive = "Resigned Employee List And Loan Summary";
        //        break;
        //    default:
        //        _strActive = "Employee List And Loan Summary";
        //        break;
        //}

        string _sqlSyntax = "";

        _sqlSyntax = @"

SELECT * FROM (



SELECT A.EmployeeNo, B.EmployeeName,A.CreateDate, A.LoanRefNo, A.AccountCode, D.AccountDesc, A.LoanDate, A.AmountGranted
, A.Terms, (A.LoanAmount + ISNULL(A.LoanInterest,0)) AS LoanAmount, A.Amortization, A.StartOfDeduction
, C.AREA, C.BCode, C.BName, A.Status
, DATEADD(mm,  cast(a.Terms as int), LoanDate) Maturity
, A.DownPayment

, B.Company
, B.TerminationStatus
, B.EmpStatus
, B.ConfiLevel
, B.DateHired
, B.DateFinish
, C.DepCode AS [DepartmentCode]
, C.DepName AS [Position]
, C.BranchCode
, C.BranchName
, 'DATE COVERED FROM " + _refDateHiredFrom + @" TO " + _refDateHiredTo + @"' AS [SubTitle]
							FROM [vwsLoanFile] A 
                                        INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
                                        INNER JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode     
										INNER JOIN vwsAccountCode D ON A.AccountCode = D.AccountCode  
WHERE B.ConfiLevel IN (" + _refConfiLevel + @")
AND A.AccountCode IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')

) XX";


        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";

        if (_refComp != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[Company] LIKE '%" + _refComp + @"%'";
        }


        if (_refDepartment != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[DepartmentCode] LIKE '%" + _refDepartment + @"%'";
        }

        if (_refBranch != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[BCode] LIKE '%" + _refBranch + @"%'";
        }

        if (_refArea != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[AREA] LIKE '%" + _refArea + @"%'";
        }


        _sqlSyntax = _sqlSyntax + @" 
                                        AND XX.Status = '" + _refActive + @"'";

        _sqlSyntax = _sqlSyntax + @" 
                                        AND XX.CreateDate BETWEEN '" + _refDateHiredFrom + @"' AND '" + _refDateHiredTo + @"' ";

        return _sqlSyntax;
    }



    private string sqlQueryOutputEmpLoan(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refDateHiredFrom = "",
                                string _refDateHiredTo = "",
                                string _refComp = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "")
    {


        //string _strActive = "";
        //switch (_refActive)
        //{
        //    case "0":
        //        _strActive = "Active Employee List And Loan Summary";
        //        break;
        //    case "1":
        //        _strActive = "Resigned Employee List And Loan Summary";
        //        break;
        //    default:
        //        _strActive = "Employee List And Loan Summary";
        //        break;
        //}

        string _sqlSyntax = "";

        _sqlSyntax = @"

SELECT * FROM (



SELECT A.EmployeeNo, B.EmployeeName,A.CreateDate, A.LoanRefNo, A.AccountCode, D.AccountDesc, A.LoanDate, A.AmountGranted
, A.Terms, (A.LoanAmount + ISNULL(A.LoanInterest,0)) AS LoanAmount, A.Amortization, A.StartOfDeduction
, C.AREA, C.BCode, C.BName, A.Status
, DATEADD(mm,  cast(a.Terms as int), LoanDate) Maturity
, A.DownPayment

, B.Company
, B.TerminationStatus
, B.EmpStatus
, B.ConfiLevel
, B.DateHired
, B.DateFinish
, C.DepCode AS [DepartmentCode]
, C.DepName AS [Position]
, C.BranchCode
, C.BranchName
, 'DATE COVERED FROM " + _refDateHiredFrom + @" TO " + _refDateHiredTo + @"' AS [SubTitle]
							FROM [vwsLoanFile] A 
                                        INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
                                        INNER JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode     
										INNER JOIN vwsAccountCode D ON A.AccountCode = D.AccountCode  
WHERE B.ConfiLevel IN (" + _refConfiLevel + @")

) XX";


        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";

        if (_refComp != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[Company] LIKE '%" + _refComp + @"%'";
        }


        if (_refDepartment != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[DepartmentCode] LIKE '%" + _refDepartment + @"%'";
        }

        if (_refBranch != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[BCode] LIKE '%" + _refBranch + @"%'";
        }

        if (_refArea != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[AREA] LIKE '%" + _refArea + @"%'";
        }


        _sqlSyntax = _sqlSyntax + @" 
                                        AND XX.Status = '" + _refActive + @"'";

        _sqlSyntax = _sqlSyntax + @" 
                                        AND XX.CreateDate BETWEEN '" + _refDateHiredFrom + @"' AND '" + _refDateHiredTo + @"' ";

        return _sqlSyntax;
    }




    private void frmEmpDateParameter_FormClosing(object sender, FormClosingEventArgs e)
    {
        clsDeclaration.sReportID = 0;
        clsDeclaration.sReportPath = "";
        clsDeclaration.sQueryString = "";
    }

    private void rbActive_CheckedChanged(object sender, EventArgs e)
    {
        switch (clsDeclaration.sReportID)
        {
            case 3:
                lblLabel.Text = "Date Hired";
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                lblLabel.Text = "Date";
                break;

        }
    }

    private void rbInActive_CheckedChanged(object sender, EventArgs e)
    {


        switch (clsDeclaration.sReportID)
        {
            case 3:
                lblLabel.Text = "Date Finished";
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                lblLabel.Text = "Date";
                break;

        }
    }
}
