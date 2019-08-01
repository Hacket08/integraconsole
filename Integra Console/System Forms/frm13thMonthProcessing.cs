using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

public partial class frm13thMonthProcessing : Form
{
    public frm13thMonthProcessing()
    {
        InitializeComponent();
    }

    private void frm13thMonthProcessing_Load(object sender, EventArgs e)
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
        DialogResult result;
        result = MessageBox.Show("You are generating data for Employee 13th Month, Some Data will be deleted! Are you sure you want to continue?", "13 Month Data Generation", MessageBoxButtons.OKCancel);

        if (result == System.Windows.Forms.DialogResult.OK)
        {
            Application.DoEvents();
            tssDataStatus.Text = @"Please wait... Data on processing..";

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
            DataTable _tblDisplay = new DataTable();

            _sqlSelection = sqlQueryOutput(_Branch, cboArea.Text, _Deparment, _selActive, txtDateFrom.Text, txtDateTo.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text);
            _tblDisplay = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelection);
            //clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay);

            double _RowCount;
            int _Count = 0;
            _RowCount = _tblDisplay.Rows.Count;

            foreach (DataRow row in _tblDisplay.Rows)
            {
                try
                {
                    string _EmployeeNo = row["EmployeeNo"].ToString();
                    string _EmployeeName = row["EmployeeName"].ToString();
                    string _DailyRate = row["DailyRate"].ToString();
                    string _StartService = row["Start Service"].ToString();
                    string _EndService = row["End Service"].ToString();
                    string _Company = row["Company"].ToString();
                    string _BCode = row["BCode"].ToString();
                    string _DepartmentCode = row["Department"].ToString();
                    string _EEStatus = row["EEStatus"].ToString();

                    double _NoofService = double.Parse(GetTotalService(DateTime.Parse(_StartService), DateTime.Parse(_EndService)).ToString("N2"));


                    double _JanuaryAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-01");
                    double _FebruaryAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-02");
                    double _MarchAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-03");
                    double _AprilAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-04");
                    double _MayAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-05");
                    double _JuneAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-06");
                    double _JulyAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-07");
                    double _AugustAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-08");
                    double _SeptemberAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-09");
                    double _OctoberAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-10");
                    double _NovemberAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-11");
                    double _DecemberAmt = getMonthlyWage(_EmployeeNo, cboYear.Text + "-12");

                    double _13Month = (_JanuaryAmt + _FebruaryAmt + _MarchAmt + _AprilAmt + _MayAmt + _JuneAmt + _JulyAmt + _AugustAmt + _SeptemberAmt + _OctoberAmt + _NovemberAmt + _DecemberAmt);

                    string _sqlDataValidation = @"SELECT 'TRUE' FROM [dbo].[13MonthDetails] A WHERE A.[EmployeeNo] = '" + _EmployeeNo + @"' AND A.[Year] = '" + cboYear.Text + @"' AND ISNULL(A.[Validated], 0) = 1";
                    DataTable _tblDataValidation = new DataTable();
                    _tblDataValidation = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlDataValidation);

                    if(_tblDataValidation.Rows.Count == 0)
                    {

                        string _sqlInsertData = "";
                        _sqlInsertData = @"
                                    DELETE FROM [dbo].[13MonthDetails] WHERE [EmployeeNo] = '" + _EmployeeNo + @"' AND [Year] = '" + cboYear.Text + @"' AND ISNULL([Validated], 0) = 0
                                    INSERT INTO [dbo].[13MonthDetails]
                                               ([Year]
                                               ,[EmployeeNo]
                                               ,[EmployeeName]
                                               ,[Company]
                                               ,[Branch]
                                               ,[Department]
                                               ,[EmpStatus]
                                               ,[DailyRate]
                                               ,[DateStart]
                                               ,[DateEnd]
                                               ,[Service]
                                               ,[January]
                                               ,[February]
                                               ,[March]
                                               ,[April]
                                               ,[May]
                                               ,[June]
                                               ,[July]
                                               ,[August]
                                               ,[September]
                                               ,[October]
                                               ,[November]
                                               ,[December]
                                               ,[TotalAmt]
                                               ,[13thMon]
                                               ,[Validated])
                                         VALUES
                                               ('" + cboYear.Text + @"'
                                               ,'" + _EmployeeNo + @"'
                                               ,'" + _EmployeeName + @"'
                                               ,'" + _Company + @"'
                                               ,'" + _BCode + @"'
                                               ,'" + _DepartmentCode + @"'
                                               ,'" + _EEStatus + @"'
                                               ,'" + _DailyRate + @"'
                                               ,'" + _StartService + @"'
                                               ,'" + _EndService + @"'
                                               ,'" + _NoofService + @"'
                                               ,'" + _JanuaryAmt + @"'
                                               ,'" + _FebruaryAmt + @"'
                                               ,'" + _MarchAmt + @"'
                                               ,'" + _AprilAmt + @"'
                                               ,'" + _MayAmt + @"'
                                               ,'" + _JuneAmt + @"'
                                               ,'" + _JulyAmt + @"'
                                               ,'" + _AugustAmt + @"'
                                               ,'" + _SeptemberAmt + @"'
                                               ,'" + _OctoberAmt + @"'
                                               ,'" + _NovemberAmt + @"'
                                               ,'" + _DecemberAmt + @"'
                                               ,'" + _13Month + @"'
                                               ,'" + (_13Month / 12) + @"'
                                               ,'0')
                                            ";

                        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlInsertData);
                    }

                    Application.DoEvents();
                    _Count++;
                    tssDataStatus.Text = "Leave Credit Generation Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
                    pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
                }
                catch (Exception)
                {
                    throw;
                }
            }





            _sqlSelection = sqlQueryOutput13thMonth(_Branch, cboArea.Text, _Deparment, _selActive, cboYear.Text, txtDateFrom.Text, txtDateTo.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text);
            _tblDisplay = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelection);
            clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay);



            MessageBox.Show("13th Month Successfully Generated");
        }
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
        //string _sqlSelect = "SELECT SUM(X.[BasicPay]) AS [Wage] FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND LEFT(X.PayrollPeriod,7) = '" + _MonthPeriod + @"'";


        string _sqlSelect = @"
SELECT (SUM(X.[BasicPay])
-
(SELECT ISNULL(SUM(Y.Amount),0) AS [Leave No Pay] 
FROM [vwsPayrollDetails] Y WHERE Y.EmployeeNo = X.EmployeeNo  AND LEFT(Y.PayrollPeriod,7) = LEFT(X.PayrollPeriod,7) AND Y.AccountCode IN ('9-528'))) AS [Wage]

FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND LEFT(X.PayrollPeriod,7) = '" + _MonthPeriod + @"'
GROUP BY X.EmployeeNo, LEFT(X.PayrollPeriod,7)

";
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
                                string _refEmployeeNo = "")
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
        _sqlSyntax = _sqlSyntax + @" AND YY.[Validated] = '0'";


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
                                string _refEmployeeNo = "")
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

        clsDeclaration.sReportID = 8;

        clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\13th Month Report.rpt";
        clsDeclaration.sQueryString = sqlQueryOutput13thMonthReport(_Branch, cboArea.Text, _Deparment, _selActive, cboYear.Text, txtDateFrom.Text, txtDateTo.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text);

        frmReportList frmReportList = new frmReportList();
        //frmReportList._RequestType = _RequestType;
        frmReportList.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmReportList.Show();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}