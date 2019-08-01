using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmEmpDateAccountParameter : Form
{

    public static string _RequestType;
    public static string _QueryGenerated;
    public frmEmpDateAccountParameter()
    {
        InitializeComponent();
    }

    private void frmEmpDateAccountParameter_Load(object sender, EventArgs e)
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
            case 7:
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Loan Payment Remittance.rpt";
                clsDeclaration.sQueryString = sqlQueryOutputEmpLoanCashPayment(_Branch, cboArea.Text, _Deparment, _selActive, dtpDateHiredFrom.Value.ToShortDateString(), dtpDateHiredTo.Value.ToShortDateString(), _Company,txtEmpCode.Text,
                                    clsDeclaration.sConfiLevel,
                                    clsDeclaration.sConfiLevelSelection);
                break;
        }





        frmReportList frmReportList = new frmReportList();
        //frmReportList._RequestType = _RequestType;
        frmReportList.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmReportList.Show();
    }


    private string sqlQueryOutputEmpLoanCashPayment(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refDateHiredFrom = "",
                                string _refDateHiredTo = "",
                                string _refComp = "",
                                string _refAccount = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "")
    {

        string _sqlSyntax = "";

        _sqlSyntax = @"



DECLARE @DateFrom DATE
DECLARE @DateTo DATE

SET @DateFrom = '" + _refDateHiredFrom + @"'
SET @DateTo = '" + _refDateHiredTo + @"'


SELECT * FROM (



SELECT A.EmployeeNo
	, B.EmployeeName
	, A.AccountCode
	, C.AccountDesc
	, A.LoanRefNo
	, A.LoanAmount
	, A.LoanDate
	  , (
	  
	  SELECT ISNULL(SUM(Z.Amount),0) FROM vwsLoanPayment Z 
	  WHERE  Z.Type <> 'PAYROLL' AND Z.Date BETWEEN @DateFrom AND @DateTo
AND Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode = A.AccountCode AND Z.LoanRefNo = A.LoanRefNo
AND Z.Type NOT IN ('OTHERS','PAYMENT BY EMPLOYEE')
	  ) AS [Cash Payment]
	  , (
	  
	  SELECT ISNULL(SUM(Z.Amount),0) FROM vwsLoanPayment Z 
	  WHERE  Z.Type = 'PAYROLL' AND RIGHT(LEFT(Z.PaymentDate, 9),1) = 'A' AND Z.Date BETWEEN @DateFrom AND @DateTo
AND Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode = A.AccountCode AND Z.LoanRefNo = A.LoanRefNo

	  ) AS [15th Payment] 
	  , (
	  
	  SELECT ISNULL(SUM(Z.Amount),0) FROM vwsLoanPayment Z 
	  WHERE  Z.Type = 'PAYROLL' AND RIGHT(LEFT(Z.PaymentDate, 9),1) = 'B' AND Z.Date BETWEEN @DateFrom AND @DateTo
AND Z.EmployeeNo = A.EmployeeNo AND Z.AccountCode = A.AccountCode AND Z.LoanRefNo = A.LoanRefNo

	  ) AS [30th Payment]
	, E.BName
	, E.BCode
	, E.AREA
	, CASE WHEN  E.BCode IN ('DESIHOFC','DESMHOFC') THEN 1 ELSE 2 END  AS [BGROUP]
	, A.Brand


	, B.Company
, B.TerminationStatus
, B.EmpStatus
, B.ConfiLevel
, B.DateHired
, B.DateFinish
, E.DepCode AS [DepartmentCode]
, E.DepName AS [Position]
, E.BranchCode
, E.BranchName
, 'DATE COVERED FROM " + _refDateHiredFrom + @" TO " + _refDateHiredTo + @"' AS [SubTitle]




FROM vwsLoanFile A 
	INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
	INNER JOIN vwsAccountCode C ON A.AccountCode = C.AccountCode
	LEFT JOIN vwsDepartmentList E ON B.Department = E.DepartmentCode
WHERE A.LoanDate <= @DateTo
AND B.ConfiLevel IN (" + _refConfiLevel + @")
AND ((A.RelativeName = '') OR (A.RelativeName IS NULL))
AND A.AccountCode = '" + _refAccount + @"'

) XX ";


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

        _sqlSyntax = _sqlSyntax + @" AND (XX.[Cash Payment] + XX.[15th Payment] + XX.[30th Payment]) <> 0";

        return _sqlSyntax;
    }

    private void frmEmpDateAccountParameter_FormClosing(object sender, FormClosingEventArgs e)
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

    private void lblBrowseEmployeeNo_Click(object sender, EventArgs e)
    {

        switch (clsDeclaration.sReportID)
        {
            case 7:
                frmDataList frmDataList = new frmDataList();
                frmDataList._gListGroup = "AccountList";
                frmDataList.ShowDialog();

                txtEmpCode.Text = frmDataList._gAccountCode;
                break;

        }


        //switch (_RequestID)
        //{
        //    case 6:
        //        frmDataList._gListGroup = "AccountList";
        //        frmDataList.ShowDialog();

        //        txtEmpCode.Text = frmDataList._gAccountCode;

        //        break;
        //    case 4:
        //    default:
        //        frmDataList._gListGroup = "EmployeeLoanList";
        //        frmDataList.ShowDialog();

        //        txtEmpCode.Text = frmDataList._gEmployeeNo;

        //        break;
        //}
    }
}
