using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

public partial class frmGrossPayPreview : Form
{

    DataTable _tblDisplay = new DataTable();
    public frmGrossPayPreview()
    {
        InitializeComponent();
    }

    private void frmGrossPayPreview_Load(object sender, EventArgs e)
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

        _sqlSelection = sqlQueryOutputGrossPay(_Branch, cboArea.Text, _Deparment, _selActive, cboYear.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text, _selAll);
        _tblDisplay = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelection);
        //clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay);


        double _RowCount;
        int _Count = 0;
        _RowCount = _tblDisplay.Rows.Count;
        int LoopCount = 0;
        NextGroup:
        _Count = 0;
        LoopCount++;
        foreach (DataRow row in _tblDisplay.Rows)
        {
            try
            {
                string _EmployeeNo = row["EmployeeNo"].ToString();
                string _EmployeeName = row["EmployeeName"].ToString();
                //string _DailyRate = row["DailyRate"].ToString();
                string _Company = row["Company"].ToString();
                string _BCode = row["BCode"].ToString();
                string _Department = row["Department"].ToString();
                string _EEStatus = row["EEStat"].ToString();

                int ID = LoopCount;

                string _Description = "";
                switch (ID)
                {

                    case 1:
                        _Description = "Gross Pay";
                        break;
                    case 2:
                        _Description = "SSS";
                        break;
                    case 3:
                        _Description = "PhilHealth";
                        break;
                    case 4:
                        _Description = "PagIbig";
                        break;
                    case 5:

                        _Description = "Witholding Tax";
                        break;
                    default:
                        break;
                }
                double _January_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-01-A", ID, cboPayrolPeriod.Text);
                double _February_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-02-A", ID, cboPayrolPeriod.Text);
                double _March_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-03-A", ID, cboPayrolPeriod.Text);
                double _April_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-04-A", ID, cboPayrolPeriod.Text);
                double _May_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-05-A", ID, cboPayrolPeriod.Text);
                double _June_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-06-A", ID, cboPayrolPeriod.Text);
                double _July_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-07-A", ID, cboPayrolPeriod.Text);
                double _August_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-08-A", ID, cboPayrolPeriod.Text);
                double _September_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-09-A", ID, cboPayrolPeriod.Text);
                double _October_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-10-A", ID, cboPayrolPeriod.Text);
                double _November_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-11-A", ID, cboPayrolPeriod.Text);
                double _December_A = getMonthlyWage(_EmployeeNo, cboYear.Text + "-12-A", ID, cboPayrolPeriod.Text);

                double _January_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-01-B", ID, cboPayrolPeriod.Text);
                double _February_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-02-B", ID, cboPayrolPeriod.Text);
                double _March_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-03-B", ID, cboPayrolPeriod.Text);
                double _April_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-04-B", ID, cboPayrolPeriod.Text);
                double _May_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-05-B", ID, cboPayrolPeriod.Text);
                double _June_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-06-B", ID, cboPayrolPeriod.Text);
                double _July_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-07-B", ID, cboPayrolPeriod.Text);
                double _August_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-08-B", ID, cboPayrolPeriod.Text);
                double _September_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-09-B", ID, cboPayrolPeriod.Text);
                double _October_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-10-B", ID, cboPayrolPeriod.Text);
                double _November_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-11-B", ID, cboPayrolPeriod.Text);
                double _December_B = getMonthlyWage(_EmployeeNo, cboYear.Text + "-12-B", ID, cboPayrolPeriod.Text);


                double _TotalAmount = (_January_A + _February_A + _March_A + _April_A + _May_A + _June_A + _July_A + _August_A + _September_A + _October_A + _November_A + _December_A)
                    + (_January_B + _February_B + _March_B + _April_B + _May_B + _June_B + _July_B + _August_B + _September_B + _October_B + _November_B + _December_B);

                double _Deduction = 0.00;
                if (ID != 1)
                {
                    _Deduction = _TotalAmount;
                    _TotalAmount = 0.00;
                }

                string _sqlInsertData = "";
                _sqlInsertData = @"
                                    DELETE FROM [dbo].[GrossPayDetails] WHERE [EmployeeNo] = '" + _EmployeeNo + @"' AND [Year] = '" + cboYear.Text + @"' AND ID  = '" + ID + @"'

INSERT INTO [dbo].[GrossPayDetails]
           ([ID]
           ,[Description]
           ,[Year]
           ,[EmployeeNo]
           ,[EmployeeName]
           ,[Company]
           ,[Branch]
           ,[Department]
           ,[Jan A]
           ,[Feb A]
           ,[Mar A]
           ,[Apr A]
           ,[May A]
           ,[Jun A]
           ,[Jul A]
           ,[Aug A]
           ,[Sep A]
           ,[Oct A]
           ,[Nov A]
           ,[Dec A]
           ,[Jan B]
           ,[Feb B]
           ,[Mar B]
           ,[Apr B]
           ,[May B]
           ,[Jun B]
           ,[Jul B]
           ,[Aug B]
           ,[Sep B]
           ,[Oct B]
           ,[Nov B]
           ,[Dec B]
           ,[TotalAmt]
           ,[Deduction]
           ,[GrossPay]
            )
     VALUES
           ('" + ID + @"'
            ,'" + _Description + @"'
            ,'" + cboYear.Text + @"'
            ,'" + _EmployeeNo + @"'
            ,'" + _EmployeeName + @"'
            ,'" + _Company + @"'
            ,'" + _BCode + @"'
            ,'" + _Department + @"'
            ,'" + _January_A + @"'
            ,'" + _February_A + @"'
            ,'" + _March_A + @"'
            ,'" + _April_A + @"'
            ,'" + _May_A + @"'
            ,'" + _June_A + @"'
            ,'" + _July_A + @"'
            ,'" + _August_A + @"'
            ,'" + _September_A + @"'
            ,'" + _October_A + @"'
            ,'" + _November_A + @"'
            ,'" + _December_A + @"'
            ,'" + _January_B + @"'
            ,'" + _February_B + @"'
            ,'" + _March_B + @"'
            ,'" + _April_B + @"'
            ,'" + _May_B + @"'
            ,'" + _June_B + @"'
            ,'" + _July_B + @"'
            ,'" + _August_B + @"'
            ,'" + _September_B + @"'
            ,'" + _October_B + @"'
            ,'" + _November_B + @"'
            ,'" + _December_B + @"'
            ,'" + _TotalAmount + @"'
            ,'" + _Deduction + @"'
            ,'" + _TotalAmount + @"'
)
                                            ";

                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlInsertData);


                Application.DoEvents();
                _Count++;
                tssDataStatus.Text = "Leave Credit Generation Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
                pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));


            }
            catch
            {

            }
        }

        if (LoopCount != 5)
        {
            goto NextGroup;
        }

        _sqlSelection = sqlQueryOutputGrossPayReprotPreview(_Branch, cboArea.Text, _Deparment, _selActive, cboYear.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text, _selAll);
        _tblDisplay = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelection);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay);

        MessageBox.Show("Gross Pay Successfully Generated");
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

    private double getMonthlyWage(string _EmployeeNo, string _PayrollPeriod, int _GroupType, string _AsOfPeriod)
    {
        string _sqlSelect = @"";
        switch (_GroupType)
        {
            case 1:
                _sqlSelect = @"
SELECT (SUM(X.[BasicPay])
-
(SELECT ISNULL(SUM(Y.Amount),0) AS [Leave No Pay] 
FROM [vwsPayrollDetails] Y WHERE Y.EmployeeNo = X.EmployeeNo  AND Y.PayrollPeriod =  X.PayrollPeriod AND Y.AccountCode IN ('9-528'))) AS [Wage]

FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND X.PayrollPeriod = '" + _PayrollPeriod + @"'
AND X.PayrollPeriod <= '" + _AsOfPeriod + @"'
GROUP BY X.EmployeeNo, X.PayrollPeriod 

";
                break;
            case 2:
                _sqlSelect = @"
SELECT SUM(X.SSSEmployee) AS [Wage]
FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND X.PayrollPeriod = '" + _PayrollPeriod + @"'
AND X.PayrollPeriod <= '" + _AsOfPeriod + @"'
GROUP BY X.EmployeeNo, X.PayrollPeriod 

";
                break;
            case 3:
                _sqlSelect = @"
SELECT SUM(X.PhilHealthEmployee) AS [Wage]
FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND X.PayrollPeriod = '" + _PayrollPeriod + @"'
AND X.PayrollPeriod <= '" + _AsOfPeriod + @"'
GROUP BY X.EmployeeNo, X.PayrollPeriod 

";
                break;
            case 4:
                _sqlSelect = @"
SELECT SUM(X.PagIbigEmployee) AS [Wage]
FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND X.PayrollPeriod = '" + _PayrollPeriod + @"'
AND X.PayrollPeriod <= '" + _AsOfPeriod + @"'
GROUP BY X.EmployeeNo, X.PayrollPeriod 

";
                break;
            case 5:
                _sqlSelect = @"
SELECT SUM(X.WitholdingTax) AS [Wage]
FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND X.PayrollPeriod = '" + _PayrollPeriod + @"'
AND X.PayrollPeriod <= '" + _AsOfPeriod + @"'
GROUP BY X.EmployeeNo, X.PayrollPeriod 

";
                break;
            default:
                break;
        }

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




    #region Gross Pay Details


    private string sqlQueryOutputGrossPay(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refYear = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "",
                                string _refEmployeeNo = "",
                                string _refViewAll = "")
    {


        string _sqlSyntax = "";
        _sqlSyntax = @"
SELECT * FROM ( SELECT * FROM [vwsEmployeeDetails] A 
				WHERE A.ConfiLevel IN (0,1,2)
			   ) XX ";

        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";
        //_sqlSyntax = _sqlSyntax + @" AND YY.[Year] = '" + _refYear + @"'";
        //
        //if (_refViewAll == "0")
        //{
        //    _sqlSyntax = _sqlSyntax + @" AND YY.[Validated] = '0'";
        //}

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

        if (_refViewAll == "0")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[IsActive] = '" + _refActive + @"'";
        }



        //if (_refActive == "0")
        //{
        //    _sqlSyntax = _sqlSyntax + @" 
        //                                AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) IN (0,1,2)";

        //    _sqlSyntax = _sqlSyntax + @" AND YY.[DateEnd] <= '" + _refDateTo + @"'";
        //}

        //if (_refActive == "1")
        //{
        //    _sqlSyntax = _sqlSyntax + @" 
        //                                AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) NOT IN (0,1,2)";
        //    _sqlSyntax = _sqlSyntax + @" AND YY.[DateEnd] BETWEEN '" + _refDateFrom + @"' AND '" + _refDateTo + @"'";
        //}

        return _sqlSyntax;
    }

    #endregion



    #region Gross Pay Reprot Preview


    private string sqlQueryOutputGrossPayReprotPreview(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refYear = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "",
                                string _refEmployeeNo = "",
                                string _refViewAll = "")
    {


        string _sqlSyntax = "";
        _sqlSyntax = @"
SELECT * FROM ( SELECT * FROM [vwsEmployeeDetails] A 
				WHERE A.ConfiLevel IN (" + _refConfiLevel + @") 
			   ) XX INNER JOIN  [GrossPayDetails] YY ON XX.EmployeeNo = YY.EmployeeNo AND XX.Department = YY.Department";

        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";
        _sqlSyntax = _sqlSyntax + @" AND YY.[Year] = '" + _refYear + @"'";
        //
        //if (_refViewAll == "0")
        //{
        //    _sqlSyntax = _sqlSyntax + @" AND YY.[Validated] = '0'";
        //}

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

        if (_refViewAll == "0")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[IsActive] = '" + _refActive + @"'";
        }



        //if (_refActive == "0")
        //{
        //    _sqlSyntax = _sqlSyntax + @" 
        //                                AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) IN (0,1,2)";

        //    _sqlSyntax = _sqlSyntax + @" AND YY.[DateEnd] <= '" + _refDateTo + @"'";
        //}

        //if (_refActive == "1")
        //{
        //    _sqlSyntax = _sqlSyntax + @" 
        //                                AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) NOT IN (0,1,2)";
        //    _sqlSyntax = _sqlSyntax + @" AND YY.[DateEnd] BETWEEN '" + _refDateFrom + @"' AND '" + _refDateTo + @"'";
        //}

        return _sqlSyntax;
    }

    #endregion

    private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtFrom = DateTime.Parse("12/25/" + cboYear.Text);
        txtDateTo.Text = dtFrom.ToString("MM/dd/yyyy");
        txtDateFrom.Text = dtFrom.AddDays(1).AddYears(-1).ToString("MM/dd/yyyy");


        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM  [PayrollPeriod] A WHERE LEFT(A.[PayrollPeriod], 4) = '" + cboYear.Text + @"' Order by [PayrollPeriod] DESC";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());

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

        clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Gross Pay Report.rpt";
        clsDeclaration.sQueryString = sqlQueryOutputGrossPayReprotPreview(_Branch, cboArea.Text, _Deparment, _selActive, cboYear.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text, _selAll);

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

    private void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAll.Checked == true)
        {
            rbActive.Enabled = false;
            rbActive.Checked = false;
        }
        else
        {
            rbActive.Enabled = true;
            rbActive.Checked = false;
        }
    }
}