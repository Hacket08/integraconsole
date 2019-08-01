using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

public partial class frm13thMonthPreview : Form
{

    DataTable _tblDisplay = new DataTable();
    public frm13thMonthPreview()
    {
        InitializeComponent();
    }

    private void frm13thMonthPreview_Load(object sender, EventArgs e)
    {
        //rbAll.Checked = true;
        chkRankAndFile.Checked = true;
        chkSupervisory.Checked = true;
        chkManagerial.Checked = true;


        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = @"
                             SELECT * FROM OUSR A WHERE A.[USERID] = '" + clsDeclaration.sLoginUserID + @"'
                      ";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        string _RankAndFile = clsSQLClientFunctions.GetData(_DataTable, "RankAndFile", "0");
        string _Supervisor = clsSQLClientFunctions.GetData(_DataTable, "Supervisor", "0");
        string _Manager = clsSQLClientFunctions.GetData(_DataTable, "Manager", "0");



        _SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }

        cboYear.Items.Clear();
        int _year = DateTime.Now.Year - 5;
        while (_year <= (DateTime.Now.Year + 10))
        {
            cboYear.Items.Add(_year);
            _year++;
        }


        _SQLSyntax = "SELECT DISTINCT CONCAT(A.DepCode, ' - ', A.DepName) FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboDepartment.Items.Clear();
        cboDepartment.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboDepartment.Items.Add(row[0].ToString());
        }
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
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
        if (rbActive.Checked == true)
        {
            _selActive = "1";
        }
        else
        {
            _selActive = "0";
        }


        string _sqlSelection = "";
        //DataTable _tblDisplay = new DataTable();

        string _selAll = "0";
        if (chkAll.Checked == true)
        {
            _selAll = "1";
        }
        else
        {
            _selAll = "0";
        }

        _sqlSelection = sqlQueryOutput13thMonth(_Branch, cboArea.Text, _Deparment, _selActive, cboYear.Text, txtDateFrom.Text, txtDateTo.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text, _selAll);
        _tblDisplay = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelection);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay);



        MessageBox.Show("13th Month Successfully Generated");
    }


    private double GetTotalService(DateTime sDateFrom, DateTime sDateTo)
    {
        DateTime _RangeStart;
        DateTime _CurrentReadDate;
        //DateTime _LastDayofTheMonth;
        DateTime _StartDayofTheMonth;
        _RangeStart = sDateFrom;

        int i = 1;
        double _NoOfMonths = 0;
        double _NoOfDays = 0;

        _StartDayofTheMonth = _RangeStart;
        while (_RangeStart <= sDateTo)
        {
            _CurrentReadDate = _RangeStart.AddMonths(i);
            if (_CurrentReadDate > sDateTo)
            {
                if (_CurrentReadDate.AddDays(-1) == sDateTo)
                {
                    _NoOfMonths++;
                    break;
                }
                else
                {
                    _StartDayofTheMonth = _CurrentReadDate.AddMonths(-1);
                    _NoOfDays = NumberOfWorkDays(_StartDayofTheMonth, sDateTo);
                    break;
                }
            }

            i++;
            _NoOfMonths++;
        }

        return _NoOfMonths + (_NoOfDays / 26);
    }


    private int NumberOfWorkDays(DateTime start, DateTime end)
    {
        int workDays = 0;
        while (DateTime.Parse(start.ToShortDateString()) <= DateTime.Parse(end.ToShortDateString()))
        {
            if (start.DayOfWeek != DayOfWeek.Sunday)
            {
                workDays++;
            }
            start = start.AddDays(1);
        }
        return workDays;
    }

    private double getMonthlyWage(string _EmployeeNo, string _MonthPeriod)
    {
        string _sqlSelect = "SELECT SUM(X.[BasicPay]) AS [Wage] FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND LEFT(X.PayrollPeriod,7) = '" + _MonthPeriod + @"'";
        DataTable _tblSelect = new DataTable();
        _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);

        double _wageOutput = 0;
        double.TryParse(clsSQLClientFunctions.GetData(_tblSelect, "Wage", "1"), out _wageOutput);

        return _wageOutput;
    }

    private string sqlQueryOutput(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refDateFrom = "",
                                string _refDateTo = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "",
                                string _refEmployeeNo = "")
    {

        
        string _sqlSyntax = "";
        _sqlSyntax = @"

        DECLARE @DFROM AS DATE
        DECLARE @DTO AS DATE

        SET @DFROM  = '" + _refDateFrom + @"'
        SET @DTO  =  '" + _refDateTo + @"'

SELECT * FROM (


SELECT 
	1 AS [Count],
	A.EmployeeNo,
	A.EmployeeName,
	A.MiddleName,
	CASE A.Gender
	WHEN 0 THEN 'MALE'
	WHEN 1 THEN 'FEMALE'
	END AS Gender,
	A.EmpPosition,
	A.DateHired,
	A.DateRegular,
	A.DailyRate,
	A.Department,
	(CASE WHEN ISNULL(A.TerminationStatus,A.EmpStatus) IN (7,8,9) THEN A.TerminationStatus ELSE A.EmpStatus END) AS [EEStatus],

    CASE (CASE WHEN ISNULL(A.TerminationStatus,A.EmpStatus) IN (7,8,9) THEN A.TerminationStatus ELSE A.EmpStatus END)
	WHEN 0 THEN 'Regular'
	WHEN 1 THEN 'Probitionary'
	WHEN 2 THEN 'Contractual'
	WHEN 3 THEN 'Finished Contract'
	WHEN 4 THEN 'Resigned'
	WHEN 5 THEN 'Temporary'
	WHEN 6 THEN 'Terminated'
	WHEN 7 THEN 'AWOL'
	WHEN 8 THEN 'Retired'
	WHEN 9 THEN 'Deceased'
    WHEN 10 THEN 'Back - Out'
	END AS Status, 


	A.DateFinish,
	(CASE WHEN A.DateHired < @DFROM THEN @DFROM ELSE A.DateHired END) AS [Start Service],
	(CASE WHEN A.DateFinish IS NOT NULL THEN A.DateFinish ELSE @DTO END) AS [End Service],
	CASE A.CivilStatus 
	WHEN 0 THEN 'Single'
	WHEN 1 THEN 'Married'
	WHEN 2 THEN 'Widow/er'
	WHEN 3 THEN 'Separated'
	END AS CivilStatus ,
	A.Birthday,
	A.SSSNo,
	A.PagIbigNo,
	A.PhilHealthNo,
	A.TaxIDNo,
	A.Remarks,
	A.ConfiLevel,
	A.EmpStatus,
	A.TerminationStatus
	, CAST(DATEDIFF(MONTH, A.DateHired, GETDATE()) AS NUMERIC(19,6)) / 12 AS [Service]
	,'' AS [SubTitle]
	, A.Company
	, B.DepCode AS [DepartmentCode]
	, B.DepName
	, B.BName
	, (SELECT X.Name FROM OBLP X WHERE X.BranchCode = A.AsBranchCode) AS [AssignBranch]
	, B.BranchCode
	, B.BranchName
	, B.AREA
	, B.BCode
	, 'Employee Detailed Report - Active' AS [Active]
	FROM [vwsEmployees] A 
	INNER JOIN [vwsDepartmentList] B ON A.Department = B.DepartmentCode
WHERE A.ConfiLevel IN (" + _refConfiLevel + @")


) XX";

        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";




        if (_refEmployeeNo != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[EmployeeNo] = '" + _refEmployeeNo + @"'";
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


            _sqlSyntax = _sqlSyntax + @" AND XX.[Start Service] <= '" + _refDateTo + @"'";
        }

        if (_refActive == "1")
        {
            _sqlSyntax = _sqlSyntax + @" 
                                        AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) NOT IN (0,1,2)";

            _sqlSyntax = _sqlSyntax + @" AND XX.[End Service] BETWEEN '" + _refDateFrom + @"' AND '" + _refDateTo + @"'";
        }



        return _sqlSyntax;
    }


    #region 13th Month Details


    private string sqlQueryOutput13thMonth(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refYear= "",
                                string _refDateFrom = "",
                                string _refDateTo = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "",
                                string _refEmployeeNo = "",
                                string _refViewAll = "")
    {


        string _sqlSyntax = "";
        _sqlSyntax = @"
SELECT YY.* FROM (

SELECT 
 1 AS [Count]
, B.DepCode AS [DepartmentCode]
, B.DepName AS [Position]
, B.BName
, (SELECT X.Name FROM OBLP X WHERE X.BranchCode = A.AsBranchCode) AS [AssignBranch]
, B.BranchCode
, B.BranchName
, B.AREA
, B.BCode
, A.ConfiLevel
, A.EmployeeNo
, A.TerminationStatus
, A.EmpStatus
, A.Department
	FROM [vwsEmployees] A 
	INNER JOIN [vwsDepartmentList] B ON A.Department = B.DepartmentCode
    
WHERE A.ConfiLevel IN (" + _refConfiLevel + @")


) XX  INNER JOIN [13MonthDetails] YY ON XX.EmployeeNo = YY.EmployeeNo AND XX.Department = YY.Department";

        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";
        _sqlSyntax = _sqlSyntax + @" AND YY.[Year] = '" + _refYear + @"'";
        //
        if(_refViewAll == "0")
        {
            _sqlSyntax = _sqlSyntax + @" AND YY.[Validated] = '0'";
        }

        if (_refEmployeeNo != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[EmployeeNo] = '" + _refEmployeeNo + @"'";
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

            _sqlSyntax = _sqlSyntax + @" AND YY.[DateEnd] <= '" + _refDateTo + @"'";
        }

        if (_refActive == "1")
        {
            _sqlSyntax = _sqlSyntax + @" 
                                        AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) NOT IN (0,1,2)";
            _sqlSyntax = _sqlSyntax + @" AND YY.[DateEnd] BETWEEN '" + _refDateFrom + @"' AND '" + _refDateTo + @"'";
        }

        return _sqlSyntax;
    }

    #endregion

    #region 13thMonth Report Query


    private string sqlQueryOutput13thMonthReport(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refYear = "",
                                string _refDateFrom = "",
                                string _refDateTo = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "",
                                string _refEmployeeNo = "",
                                string _refViewAll = "")
    {

        string _SubTitile = "";
        if (_refActive == "0")
        {
            _SubTitile = _refYear + @" ACTIVE EMPLOYEES";
        }
        else
        {
            _SubTitile = _refYear + @" RESIGNED EMPLOYEES";
        }



        string _sqlSyntax = "";
        _sqlSyntax = @"
SELECT * FROM (

SELECT 
 1 AS [Count]
, B.DepCode AS [DepartmentCode]
, B.DepName AS [Position]
, B.BName
, (SELECT X.Name FROM OBLP X WHERE X.BranchCode = A.AsBranchCode) AS [AssignBranch]
, B.BranchCode
, B.BranchName
, B.AREA
, B.BCode
, A.ConfiLevel
, A.EmployeeNo
, A.TerminationStatus
, A.EmpStatus
, A.Department
, A.DateFinish

,    CASE (CASE WHEN ISNULL(A.TerminationStatus,A.EmpStatus) IN (7,8,9) THEN A.TerminationStatus ELSE A.EmpStatus END)
	WHEN 0 THEN 'Regular'
	WHEN 1 THEN 'Probitionary'
	WHEN 2 THEN 'Contractual'
	WHEN 3 THEN 'Finished Contract'
	WHEN 4 THEN 'Resigned'
	WHEN 5 THEN 'Temporary'
	WHEN 6 THEN 'Terminated'
	WHEN 7 THEN 'AWOL'
	WHEN 8 THEN 'Retired'
	WHEN 9 THEN 'Deceased'
    WHEN 10 THEN 'Back - Out'
	END AS Status
, '" + _SubTitile + @"' AS [SubTitle]
	FROM [vwsEmployees] A 
	INNER JOIN [vwsDepartmentList] B ON A.Department = B.DepartmentCode
    
WHERE A.ConfiLevel IN (" + _refConfiLevel + @")


) XX  INNER JOIN [13MonthDetails] YY ON XX.EmployeeNo = YY.EmployeeNo AND XX.Department = YY.Department";

        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";
        _sqlSyntax = _sqlSyntax + @" AND YY.[Year] = '" + _refYear + @"'";
        if (_refViewAll == "0")
        {
            _sqlSyntax = _sqlSyntax + @" AND YY.[Validated] = '0'";
        }


        if (_refEmployeeNo != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[EmployeeNo] = '" + _refEmployeeNo + @"'";
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

            _sqlSyntax = _sqlSyntax + @" AND YY.[DateEnd] <= '" + _refDateTo + @"'";
        }

        if (_refActive == "1")
        {
            _sqlSyntax = _sqlSyntax + @" 
                                        AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) NOT IN (0,1,2)";
            _sqlSyntax = _sqlSyntax + @" AND YY.[DateEnd] BETWEEN '" + _refDateFrom + @"' AND '" + _refDateTo + @"'";
        }

        return _sqlSyntax;
    }
    #endregion

    private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtFrom = DateTime.Parse("12/25/" + cboYear.Text);
        txtDateTo.Text = dtFrom.ToString("MM/dd/yyyy");
        txtDateFrom.Text = dtFrom.AddDays(1).AddYears(-1).ToString("MM/dd/yyyy");

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

    private void lblPreviewReport_Click(object sender, EventArgs e)
    {

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
        if (rbActive.Checked == true)
        {
            _selActive = "1";
        }
        else
        {
            _selActive = "0";
        }


        string _selAll = "0";
        if (chkAll.Checked == true)
        {
            _selAll = "1";
        }
        else
        {
            _selAll = "0";
        }

        clsDeclaration.sReportID = 8;

        clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\13th Month Report.rpt";
        clsDeclaration.sQueryString = sqlQueryOutput13thMonthReport(_Branch, cboArea.Text, _Deparment, _selActive, cboYear.Text, txtDateFrom.Text, txtDateTo.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text, _selAll);

        frmReportList frmReportList = new frmReportList();
        //frmReportList._RequestType = _RequestType;
        frmReportList.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmReportList.Show();
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        //_tblDisplay = new DataTable();
        double _RowCount;
        int _Count = 0;
        _RowCount = _tblDisplay.Rows.Count;

        foreach (DataRow row in _tblDisplay.Rows)
        {
            #region Varialble Declaration For the Employee Data

            string _Year = cboYear.Text;
            string _Company = row["Company"].ToString();
            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _EmployeeName = row["EmployeeName"].ToString();
            string _Branch = row["Branch"].ToString();
            string _Department = row["Department"].ToString();


            string _ServiceStartDate = row["DateStart"].ToString();
            string _ServiceEndDate = row["DateEnd"].ToString();



            double _DailyRate = double.Parse(row["DailyRate"].ToString());
            //double _ColaAmount = double.Parse(row["ColaAmount"].ToString());
            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);



            string _sqlInsertLeaveDelete = "";
            _sqlInsertLeaveDelete = @"
                                                        DELETE FROM [13MonthData] WHERE [EmployeeNo] = '" + _EmployeeNo + "' AND Year = '" + _Year + @"'
                                                        DELETE FROM [13MonthDetails] WHERE [EmployeeNo] = '" + _EmployeeNo + "' AND Year = '" + _Year + @"'
                                                        ";

            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertLeaveDelete);

            string _sqlInsertLeaveHeader = "";
            _sqlInsertLeaveHeader = @"    

                                        INSERT INTO [dbo].[13MonthData]
                                                   ([EmployeeNo]
                                                       ,[DailyRate]
                                                       ,[Year]
                                                       ,[ServiceStart]
                                                       ,[ServiceEnd]
                                                       ,[DateFrom]
                                                       ,[DateTo]
                                                       ,[Branch]
                                                       ,[Department])
                                             VALUES
                                                   ('" + _EmployeeNo + @"'
                                                   ,'" + _DailyRate + @"'
                                                   ,'" + _Year + @"'
                                                   ,'" + _ServiceStartDate + @"'
                                                   ,'" + _ServiceEndDate + @"'
                                                   ,'" + txtDateFrom.Text + @"'
                                                   ,'" + txtDateTo.Text + @"'
                                                   ,'" + _Branch + @"'
                                                   ,'" + _Department + @"')
                                                     ";

            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertLeaveHeader);



            string _LoanRefenceNo = "";
            string _AccountCode = clsDeclaration.sys13MonthAccount;

            //string _LeaveCredit = row["LeaveCredit"].ToString();
            string _Service = row["Service"].ToString();
            //string _NoofDays = row["NoofDays"].ToString();
            string _13MonthCalc = row["13thMon"].ToString();

            double _Amount = double.Parse(_13MonthCalc);

            string _sqlInsertLeaveDetails = "";
            _sqlInsertLeaveDetails = @"    

                                        INSERT INTO [dbo].[13MonthDetails]
                                                   ([Year]
                                                    ,[EmployeeNo]
                                                    ,[AccountCode]
                                                    ,[LoanRefenceNo]
                                                    ,[Amount]
                                                    ,[NoOfService]
                                                    ,[Branch]
                                                    ,[Department])
                                             VALUES
                                                   ('" + _Year + @"'
                                                   ,'" + _EmployeeNo + @"'
                                                   ,'" + _AccountCode + @"'
                                                   ,'" + _LoanRefenceNo + @"'
                                                   ,'" + _Amount + @"'
                                                   ,'" + _Service + @"'
                                                   ,'" + _Branch + @"'
                                                   ,'" + _Department + @"')
                                                     ";

            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertLeaveDetails);

           
      
            _sqlInsertLeaveDetails = @"    
                                    UPDATE A SET A.[Validated] = 1, A.[DateValidated] = GETDATE() FROM [dbo].[13MonthDetails] A WHERE A.[EmployeeNo] = '" + _EmployeeNo + @"' AND A.[Year] = '" + cboYear.Text + @"'
                                                     ";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlInsertLeaveDetails);
            #endregion


            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Leave Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }

        MessageBox.Show("Confirmed");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}