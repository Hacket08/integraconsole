using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;


using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

public partial class frmBonusCalculation : Form
{
    public frmBonusCalculation()
    {
        InitializeComponent();
    }

    private void frmBonusCalculation_Load(object sender, EventArgs e)
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
        cboPosition.Items.Clear();
        cboPosition.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPosition.Items.Add(row[0].ToString());
        }
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        #region Confidentiality Group Filter
        string _GroupConfi = "0,1,2";
        if (chkRankAndFile.Checked == true)
        {
            _GroupConfi = "0";
        }
        if (chkSupervisory.Checked == true)
        {
            _GroupConfi = "1";
        }
        if (chkManagerial.Checked == true)
        {
            _GroupConfi = "2";
        }
        if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true)
        {
            _GroupConfi = "0,1";
        }
        if (chkRankAndFile.Checked == true && chkManagerial.Checked == true)
        {
            _GroupConfi = "0,2";
        }
        if (chkSupervisory.Checked == true && chkManagerial.Checked == true)
        {
            _GroupConfi = "1,2";
        }
        if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true && chkManagerial.Checked == true)
        {
            _GroupConfi = "0,1,2";
        }

        #endregion
        #region Blocking of Generation of Data
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
        #endregion
        

        //        , CASE WHEN A.ConfiLevel = 2 THEN 20
        //ELSE
        //CASE WHEN A.Category = 0 THEN 15
        //ELSE 10 END
        //END AS[LeaveCredit]

        string _sqlPayrollDisplay = "";

        _sqlPayrollDisplay = @"
        DECLARE @DFROM AS DATE
        DECLARE @DTO AS DATE

        SET @DFROM  = '" + txtDateFrom.Text + @"'
        SET @DTO  =  '" + txtDateTo.Text + @"'

        SELECT * FROM (
        SELECT A.EmployeeNo, A.EmployeeName, A.Company, A.DailyRate, A.DateHired
        , CASE WHEN A.DateHired < @DFROM THEN @DFROM ELSE A.DateHired END AS [Service Start Date]
        , CASE WHEN A.DateFinish BETWEEN @DFROM AND @DTO THEN A.DateFinish ELSE @DTO END AS [Service End Date]
        , (SELECT MAX(X.[PayrollPeriod]) FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  A.EmployeeNo) AS [Last Payroll Period]
        , 0.00 AS [Service]
        , 0.00 AS [Bonus]
        , A.ConfiLevel
        , CASE WHEN A.DateFinish BETWEEN @DFROM AND @DTO THEN 'LASTPAY' ELSE 'PAYROLL' END AS [Type]
        , A.Department
        , B.BCode
        , A.Category
, A.EmpPosition
        FROM vwsEmployees A
        INNER JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
                                            WHERE B.Area LIKE '%" + cboArea.Text + @"%' 
										                AND B.BCode LIKE '%" + _BCode + @"%'
										                AND A.EmployeeNo  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtEmpNo.Text + @"%' 
										                AND A.EmployeeName  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtName.Text + @"%' 
                                                        AND B.DepCode LIKE '%" + _BPosition + @"%'
										                AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
														AND (CASE WHEN (A.DateFinish = '' OR A.DateFinish IS NULL) THEN @DTO ELSE A.DateFinish END) BETWEEN @DFROM AND @DTO
                                                        AND A.DateHired <= @DTO
                                    ) XX 
                                        WHERE XX.ConfiLevel IN (" + _GroupConfi + @")
                                                     AND XX.Type = 'PAYROLL'
                                        ORDER BY XX.[EmployeeName]
                                                    ";

        #region Data Dispaly
        //      _sqlPayrollDisplay = @"
        //      DECLARE @DFROM AS DATE
        //      DECLARE @DTO AS DATE

        //      SET @DFROM  = '" + txtDateFrom.Text + @"'
        //      SET @DTO  =  '" + txtDateTo.Text + @"'

        //      SELECT * FROM (
        //      SELECT A.EmployeeNo, A.EmployeeName, A.DailyRate, A.Company, A.EmpPosition, A.DateHired
        //      , CASE WHEN A.DateHired < @DFROM THEN @DFROM ELSE A.DateHired END AS [Service Start Date]
        //      , CASE WHEN A.DateFinish BETWEEN @DFROM AND @DTO THEN A.DateFinish ELSE @DTO END AS [Service End Date]
        //      , 0.00 AS [Service]
        //      , 0.00 AS [Days Deduction]
        //      , 0.00 AS [Total Service]
        //      , 0.00 AS [Appraisal " + cboYear.Text + @"]
        //      , 0.00 AS [Br Adj]
        //      , 0.00 AS [Inv Adj]
        //      , 0.00 AS [Final]
        //      , 0.00 AS [13th Month Pay]
        //      , 0.00 AS [Bonus]
        //      , 0.00 AS [Total]
        //      ,A.ConfiLevel
        //      , CASE WHEN A.DateFinish BETWEEN @DFROM AND @DTO THEN 'LASTPAY' ELSE 'PAYROLL' END AS [Type]
        //,A.Department
        //,B.BCode
        //      , A.Category
        //      FROM vwsEmployees A
        //      INNER JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
        //                                          WHERE B.Area LIKE '%" + cboArea.Text + @"%' 
        //								                AND B.BCode LIKE '%" + _BCode + @"%'
        //								                AND A.EmployeeNo  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtEmpNo.Text + @"%' 
        //								                AND A.EmployeeName  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtName.Text + @"%' 
        //                                                      AND B.DepCode LIKE '%" + _BPosition + @"%'
        //								                AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
        //												AND (CASE WHEN (A.DateFinish = '' OR A.DateFinish IS NULL) THEN @DTO ELSE A.DateFinish END) BETWEEN @DFROM AND @DTO
        //                                                      AND A.DateHired <= @DTO
        //                                  ) XX 
        //                                      WHERE XX.ConfiLevel IN (" + _GroupConfi + @")
        //                                                   AND XX.Type = 'PAYROLL'
        //                                      ORDER BY XX.[EmployeeName]
        //                                                  ";
        #endregion


        DataTable _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);


        double _RowCount;
        int _Count = 0;
        _RowCount = _DataTable.Rows.Count;

        foreach (DataRow row in _DataTable.Rows)
        {
            try
            {
                Application.DoEvents();
                string _ConfiLevel = row["ConfiLevel"].ToString();

                double _RateDiv = double.Parse(clsDeclaration.sysLeaveDivisor);
                string _EmployeeNo = row["EmployeeNo"].ToString();
                string _EmployeeName = row["EmployeeName"].ToString();
                string sRate = row["DailyRate"].ToString();
                string sDateFrom = row["Service Start Date"].ToString();
                string sDateTo = row["Service End Date"].ToString();
                string sLastPayrollPeriod = row["Last Payroll Period"].ToString();
 

                string sLeaveCount = row[8].ToString();
                //row[11] = BusinessDaysUntil(DateTime.Parse(sDateFrom), DateTime.Parse(sDateTo)).ToString();
                //row[11] = double.Parse((double.Parse(sTotalDays) / 26).ToString()).ToString("N2");
                if (_ConfiLevel != "0")
                {
                    //double NoofService = double.Parse("12");
                    double NoofService = double.Parse(GetTotalService(DateTime.Parse(sDateFrom), DateTime.Parse(sDateTo)).ToString("N2"));
                    row["Service"] = NoofService.ToString("N2");
                }
                else
                {
                    double NoofService = double.Parse(GetTotalService(DateTime.Parse(sDateFrom), DateTime.Parse(sDateTo)).ToString("N2"));
                    row["Service"] = NoofService.ToString("N2");
                }

                Application.DoEvents();
                _Count++;
                tssDataStatus.Text = "Leave Credit Generation Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
                pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //double _RowCount;
        //int _Count = 0;
        //_RowCount = _DataTable.Rows.Count;

        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    try
        //    {
        //        Application.DoEvents();
        //        string _ConfiLevel = row["ConfiLevel"].ToString();

        //        double _RateDiv = double.Parse(clsDeclaration.sysLeaveDivisor);
        //        string _EmployeeNo = row["EmployeeNo"].ToString();
        //        string _EmployeeName = row["EmployeeName"].ToString();
        //        string sRate = row[2].ToString();
        //        string sDateFrom = row[6].ToString();
        //        string sDateTo = row[7].ToString();
        //        string sCOLA = row["ColaAmount"].ToString();

        //        string sLeaveCount = row[8].ToString();
        //        //row[11] = BusinessDaysUntil(DateTime.Parse(sDateFrom), DateTime.Parse(sDateTo)).ToString();
        //        //row[11] = double.Parse((double.Parse(sTotalDays) / 26).ToString()).ToString("N2");
        //        if (_ConfiLevel != "0")
        //        {
        //            //double NoofService = double.Parse("12");
        //            double NoofService = double.Parse(GetTotalService(DateTime.Parse(sDateFrom), DateTime.Parse(sDateTo)).ToString("N2"));
        //            row[9] = NoofService.ToString("N2");
        //            double NoofDays = double.Parse(LeaveCalculation(txtDateFrom.Text, txtDateTo.Text, _EmployeeNo, _ConfiLevel).ToString("N5"));
        //            row[10] = NoofDays.ToString("N5");
        //            row[11] = double.Parse(((double.Parse(sRate) + double.Parse(sCOLA)) * NoofDays).ToString("N2"));
        //        }
        //        else
        //        {
        //            double NoofService = double.Parse(GetTotalService(DateTime.Parse(sDateFrom), DateTime.Parse(sDateTo)).ToString("N2"));
        //            row[9] = NoofService.ToString("N2");
        //            double NoofDays = 0;
        //            if (NoofService < 1)
        //            {
        //                NoofDays = 0;
        //            }
        //            else
        //            {
        //                //NoofDays = double.Parse(((double.Parse(sLeaveCount) / _RateDiv) * NoofService).ToString("N5"));
        //                NoofDays = double.Parse(LeaveCalculation(txtDateFrom.Text, txtDateTo.Text, _EmployeeNo, _ConfiLevel).ToString("N5"));
        //            }
        //            row[10] = NoofDays.ToString("N5");
        //            row[11] = double.Parse(((double.Parse(sRate) + double.Parse(sCOLA)) * NoofDays).ToString("N2"));
        //            //row[16] 
        //        }

        //        Application.DoEvents();
        //        _Count++;
        //        tssDataStatus.Text = "Leave Credit Generation Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
        //        pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}



        //_Count = 0;
        //_RowCount = _DataTable.Rows.Count;
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    #region Varialble Declaration For the Employee Data

        //    string _Year = cboYear.Text;
        //    string _Company = row["Company"].ToString();
        //    string _EmployeeNo = row["EmployeeNo"].ToString();
        //    string _EmployeeName = row["EmployeeName"].ToString();
        //    string _Branch = row["BCode"].ToString();
        //    string _Department = row["Department"].ToString();


        //    string _ServiceStartDate = row["Service Start Date"].ToString();
        //    string _ServiceEndDate = row["Service End Date"].ToString();



        //    double _DailyRate = double.Parse(row["DailyRate"].ToString());
        //    //double _ColaAmount = double.Parse(row["ColaAmount"].ToString());
        //    string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);



        //    string _sqlInsertLeaveDelete = "";
        //    _sqlInsertLeaveDelete = @"
        //                                                DELETE FROM [13MonthData] WHERE [EmployeeNo] = '" + _EmployeeNo + "' AND Year = '" + _Year + @"'
        //                                                DELETE FROM [13MonthDetails] WHERE [EmployeeNo] = '" + _EmployeeNo + "' AND Year = '" + _Year + @"'
        //                                                ";

        //    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertLeaveDelete);

        //    string _sqlInsertLeaveHeader = "";
        //    _sqlInsertLeaveHeader = @"    

        //                                INSERT INTO [dbo].[13MonthData]
        //                                           ([EmployeeNo]
        //                                               ,[DailyRate]
        //                                               ,[Year]
        //                                               ,[ServiceStart]
        //                                               ,[ServiceEnd]
        //                                               ,[DateFrom]
        //                                               ,[DateTo]
        //                                               ,[Branch]
        //                                               ,[Department])
        //                                     VALUES
        //                                           ('" + _EmployeeNo + @"'
        //                                           ,'" + (_DailyRate) + @"'
        //                                           ,'" + _Year + @"'
        //                                           ,'" + _ServiceStartDate + @"'
        //                                           ,'" + _ServiceEndDate + @"'
        //                                           ,'" + txtDateFrom.Text + @"'
        //                                           ,'" + txtDateTo.Text + @"'
        //                                           ,'" + _Branch + @"'
        //                                           ,'" + _Department + @"')
        //                                             ";

        //    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertLeaveHeader);



        //    string _LoanRefenceNo = "";
        //    string _AccountCode = clsDeclaration.sysVLCashConversionAccount;

        //    //string _LeaveCredit = row["LeaveCredit"].ToString();
        //    string _Service = row["Service"].ToString();
        //    //string _NoofDays = row["NoofDays"].ToString();
        //    string _13MonthCalc = row["13 Month Calc"].ToString();

        //    double _Amount = double.Parse(_13MonthCalc);

        //    string _sqlInsertLeaveDetails = "";
        //    _sqlInsertLeaveDetails = @"    

        //                                INSERT INTO [dbo].[13MonthDetails]
        //                                           ([Year]
        //                                            ,[EmployeeNo]
        //                                            ,[AccountCode]
        //                                            ,[LoanRefenceNo]
        //                                            ,[Amount]
        //                                            ,[NoOfService]
        //                                            ,[Branch]
        //                                            ,[Department])
        //                                     VALUES
        //                                           ('" + _Year + @"'
        //                                           ,'" + _EmployeeNo + @"'
        //                                           ,'" + _AccountCode + @"'
        //                                           ,'" + _LoanRefenceNo + @"'
        //                                           ,'" + _Amount + @"'
        //                                           ,'" + _Service + @"'
        //                                           ,'" + _Branch + @"'
        //                                           ,'" + _Department + @"')
        //                                             ";

        //    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertLeaveDetails);


        //    #endregion


        //    Application.DoEvents();
        //    _Count++;
        //    tssDataStatus.Text = "Leave Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
        //    pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        //}

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
            if(_CurrentReadDate > sDateTo)
            {
                if(_CurrentReadDate.AddDays(-1) == sDateTo)
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

    private DateTime GetEndOfTheMonth(DateTime _Date)
    {
        var firstDayOfMonth = new DateTime(_Date.Year, _Date.Month, 1);
        return firstDayOfMonth.AddMonths(1).AddDays(-1);
    }

private int NumberOfWorkDays(DateTime start, DateTime end)
{
    int workDays = 0;
    while(DateTime.Parse(start.ToShortDateString()) <= DateTime.Parse(end.ToShortDateString()))
    {
        if(start.DayOfWeek != DayOfWeek.Sunday)
        {
            workDays++;
        }
        start = start.AddDays(1);
    }
    return workDays;
}

    private  double MonthDifference(DateTime lValue, DateTime rValue)
    {
        return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
    }

    public static double GetBusinessDays(DateTime startD, DateTime endD)
    {
        double calcBusinessDays =
            1 + ((endD - startD).TotalDays * 7 -
            (startD.DayOfWeek - endD.DayOfWeek) * 1) / 7;

        if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
        if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

        return calcBusinessDays;
    }

    public int Weekdays(DateTime dtmStart, DateTime dtmEnd)
    {
        // This function includes the start and end date in the count if they fall on a weekday
        int dowStart = ((int)dtmStart.DayOfWeek == 0 ? 7 : (int)dtmStart.DayOfWeek);
        int dowEnd = ((int)dtmEnd.DayOfWeek == 0 ? 7 : (int)dtmEnd.DayOfWeek);
        TimeSpan tSpan = dtmEnd - dtmStart;
        if (dowStart <= dowEnd)
        {
            return (((tSpan.Days / 7) * 5) + Math.Max((Math.Min((dowEnd + 1), 6) - dowStart), 0));
        }
        return (((tSpan.Days / 7) * 5) + Math.Min((dowEnd + 6) - Math.Min(dowStart, 6), 5));
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

    private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtFrom = DateTime.Parse( "12/25/" + cboYear.Text);
        txtDateTo.Text = dtFrom.ToString("MM/dd/yyyy");
        txtDateFrom.Text = dtFrom.AddDays(1).AddYears(-1).ToString("MM/dd/yyyy");

    }

    private void btnUpload_Click(object sender, EventArgs e)
    {

    }


    private double LeaveCalculation(string dateFrom, string dateTo, string empNo, string _confiLevel)
    {

        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"

DECLARE @DateFrom AS Date
DECLARE @DateTo AS Date

        SET @DateFrom  = '" + dateFrom + @"'
        SET @DateTo  =  '" + dateTo + @"'

SELECT *,       ISNULL(LEAD(XX.[StartDate]) OVER(ORDER BY XX.[StartDate]), @DateTo) AS [EndDate], '' AS DFrom , '' AS DTo 
        , 0.00 AS [Service]
        , 0.00 AS [NoofDays]
        , 0.00 AS [LeavePay]

FROM (
SELECT A.EmployeeNo
        , A.[dftLeaveCredit] AS [Leave]
, A.DateHired AS [StartDate] FROM vwsEmployees A
UNION ALL
SELECT [EmployeeNo],[LeaveAllowed],[Date] FROM [vwsEmployeePromotion]
) XX 
WHERE XX.EmployeeNo =  '" + empNo + @"'

                                                    ";

        DataTable _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        //clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);

        foreach (DataRow row in _DataTable.Rows)
        {
            try
            {
                Application.DoEvents();
                string sDateFrom = row[2].ToString();
                string sDateTo = row[3].ToString();

                if (DateTime.Parse(sDateTo) != DateTime.Parse(dateTo))
                {
                    sDateTo = DateTime.Parse(sDateTo).AddDays(-1).ToString("MM/dd/yyyy");
                }


                if (DateTime.Parse(sDateFrom) <= DateTime.Parse(txtDateFrom.Text))
                {
                    row[4] = txtDateFrom.Text;
                }
                else
                {
                    row[4] = sDateFrom;
                }

                if (DateTime.Parse(sDateTo) < DateTime.Parse(txtDateTo.Text))
                {
                    row[5] = sDateTo;
                }
                else
                {
                    row[5] = txtDateTo.Text;
                }

                string sServDateFrom = row[4].ToString();
                string sServDateTo = row[5].ToString();

                double _RateDiv = double.Parse(clsDeclaration.sysLeaveDivisor);
                string sLeaveCount = row[1].ToString();
                double NoofService = double.Parse(GetTotalService(DateTime.Parse(sServDateFrom), DateTime.Parse(sServDateTo)).ToString("N5"));

                row[6] = NoofService.ToString("N2");
                double NoofDays = 0;

                if(_confiLevel != "0")
                {
                    NoofDays = double.Parse(((double.Parse(sLeaveCount) / _RateDiv) * double.Parse(NoofService.ToString("N2"))).ToString("N5"));
                }
                else
                {
                    if (NoofService < 1)
                    {
                        NoofDays = 0;
                    }
                    else
                    {
                        NoofDays = double.Parse(((double.Parse(sLeaveCount) / _RateDiv) * double.Parse(NoofService.ToString("N2"))).ToString("N5"));
                    }
                }
                row[7] = NoofDays.ToString("N5");
            }
            catch
            {
                return 0;
            }
        }

        double _TotLeaveCount = 0;
        //clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);
        foreach (DataRow rwTotLeave in _DataTable.Rows)
        {
            double sDaysCals = double.Parse(rwTotLeave[7].ToString());
            _TotLeaveCount = _TotLeaveCount + sDaysCals;
        }
        return double.Parse(_TotLeaveCount.ToString("N5"));
    }


    private void button1_Click(object sender, EventArgs e)
    {

        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"

DECLARE @DateFrom AS Date
DECLARE @DateTo AS Date

        SET @DateFrom  = '" + txtDateFrom.Text + @"'
        SET @DateTo  =  '" + txtDateTo.Text + @"'

SELECT *,       ISNULL(LEAD(XX.[StartDate]) OVER(ORDER BY XX.[StartDate]), @DateTo) AS [EndDate], '' AS DFrom , '' AS DTo 
        , 0.00 AS [Service]
        , 0.00 AS [NoofDays]
        , 0.00 AS [LeavePay]

FROM (
SELECT A.EmployeeNo, [dftLeaveCredit] AS [Leave], A.DateHired AS [StartDate] FROM vwsEmployees A
UNION ALL
SELECT [EmployeeNo],[LeaveAllowed],[Date] FROM [vwsEmployeePromotion]
) XX
WHERE XX.EmployeeNo = 1002

                                                    ";

        DataTable _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);

        

        double _RowCount;
        //int _Count = 0;
        _RowCount = _DataTable.Rows.Count;

        foreach (DataRow row in _DataTable.Rows)
        {
            try
            {
                Application.DoEvents();
                string sDateFrom = row[2].ToString();
                string sDateTo = row[3].ToString();

                if(DateTime.Parse(sDateFrom) <= DateTime.Parse(txtDateFrom.Text))
                {
                    row[4] = txtDateFrom.Text;
                }
                else
                {
                    row[4] = sDateFrom;
                }

                if (DateTime.Parse(sDateTo) < DateTime.Parse(txtDateTo.Text))
                {
                    row[5] = sDateTo;
                }
                else
                {
                    row[5] = txtDateTo.Text;
                }

                string sServDateFrom = row[4].ToString();
                string sServDateTo = row[5].ToString();
                
                double _RateDiv = double.Parse(clsDeclaration.sysLeaveDivisor);
                string sLeaveCount = row[1].ToString();
                ////row[11] = BusinessDaysUntil(DateTime.Parse(sDateFrom), DateTime.Parse(sDateTo)).ToString();
                //row[11] = double.Parse((double.Parse(sTotalDays) / 26).ToString()).ToString("N2");

                double NoofService = double.Parse(GetTotalService(DateTime.Parse(sServDateFrom), DateTime.Parse(sServDateTo)).ToString("N2"));

                row[6] = NoofService.ToString("N2");
                double NoofDays = 0;
                if (NoofService < 1)
                {
                    NoofDays = 0;
                }
                else
                {
                    NoofDays = double.Parse(((double.Parse(sLeaveCount) / _RateDiv) * NoofService).ToString("N5"));
                }
                row[7] = NoofDays.ToString("N5");
                
                double _TotLeaveCount = 0;
                foreach (DataRow rwTotLeave in _DataTable.Rows)
                {
                    double sDaysCals = double.Parse(rwTotLeave[7].ToString());
                    _TotLeaveCount = _TotLeaveCount + sDaysCals;
                }

                row[8] = _TotLeaveCount.ToString("N5");
                //row[8] = double.Parse((double.Parse(sRate) * NoofDays).ToString("N2"));
                
                //Application.DoEvents();
                //_Count++;
                //tssDataStatus.Text = "Leave Credit Generation Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
                //pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnExcel_Click(object sender, EventArgs e)
    {
        string _Branch;
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }


        string _MyFile = "";
        Stream myStream;
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
        saveFileDialog1.FilterIndex = 2;
        saveFileDialog1.RestoreDirectory = true;

        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            _MyFile = saveFileDialog1.FileName;
            if ((myStream = saveFileDialog1.OpenFile()) != null)
            {
                myStream.Close();
            }
        }

        if (_MyFile == "")
        {
            MessageBox.Show("File Not Found!");
            return;
        }

        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        xlApp = new Excel.Application();
        xlWorkBook = xlApp.Workbooks.Add(misValue);


        xlWorkSheet = xlWorkBook.Sheets["Sheet1"];
        xlWorkSheet.Name = _Branch;


        string _sqlCompany = "SELECT Z.CompanyName FROM OCMP Z WHERE Z.Active = 1 AND Z.CompanyCode = LEFT('" + _Branch + "',4)";
        string _CompanyName = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlCompany, "CompanyName");



        xlWorkSheet.Cells[1, 1] = _CompanyName;
        xlWorkSheet.Cells[2, 1] = cboBranch.Text;
        xlWorkSheet.Cells[3, 1] = "Performance Bonus";

        //

        int i = 5;
        //int j = 0;

        xlWorkSheet.Cells[i, 1] = "##";
        xlWorkSheet.Cells[i, 2] = "Emp No";
        xlWorkSheet.Cells[i, 3] = "Employee";
        xlWorkSheet.Cells[i, 4] = "Position";
        xlWorkSheet.Cells[i, 5] = "Date Hired";
        xlWorkSheet.Cells[i, 6] = "Daily Rate";
        xlWorkSheet.Cells[i, 7] = "Service";


        int count = 0;
        foreach (DataGridViewRow Row in dgvDisplay.Rows)
        {
            i++;
            count++;

            string _Code = Row.Cells[3].Value.ToString();
            string _EmployeeNo = Row.Cells["EmployeeNo"].Value.ToString();
            string _EmployeeName = Row.Cells["EmployeeName"].Value.ToString();
            string _EmpPosition = Row.Cells["EmpPosition"].Value.ToString();
            string _DateHired = Row.Cells["DateHired"].Value.ToString();
            string _DailyRate = Row.Cells["DailyRate"].Value.ToString();
            string _Service = Row.Cells["Service"].Value.ToString();

            xlWorkSheet.Cells[i, 1] = count;
            xlWorkSheet.Cells[i, 2] = _EmployeeNo;
            xlWorkSheet.Cells[i, 3] = _EmployeeName;
            xlWorkSheet.Cells[i, 4] = _EmpPosition;
            xlWorkSheet.Cells[i, 5] = DateTime.Parse(_DateHired).ToShortDateString();
            xlWorkSheet.Cells[i, 6] = double.Parse(_DailyRate).ToString("N2");
            xlWorkSheet.Cells[i, 7] = _Service;
        }


        //i = 5;
        //for (j = 1; j <= _DataListRemittance.Columns.Count; j++)
        //{
        //    xlWorkSheet.Cells[i, j] = _DataListRemittance.Columns[j - 1].ColumnName;
        //}



        //double _RowCount;
        //int _Count = 0;
        //_RowCount = dgvDisplay.Rows.Count;

        //foreach (DataRow row in _DataListRemittance.Rows)
        //{

        //    string _EmployeeName = row[1].ToString();

        //    for (j = 0; j <= _DataListRemittance.Columns.Count - 1; j++)
        //    {
        //        xlWorkSheet.Cells[i + 1, j + 1] = row[j].ToString();
        //    }

        //    i++;
        //    Application.DoEvents();
        //    _Count++;

        //    //frmMainWindow frmMainWindow = new frmMainWindow();
        //    tssDataStatus.Text = "Payroll Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
        //    pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        //}




        //int[] arr;

        //switch (cboGoverment.Text)
        //{
        //    case "WTAX":
        //        arr = new int[2] { 5, 6 };
        //        break;
        //    case "SSS":
        //        arr = new int[11] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        //        break;
        //    case "PAGIBIG":
        //        arr = new int[13] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        //        break;
        //    default:
        //        arr = new int[26] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        //        break;
        //}

        ////int[] arr = new int[26] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        //Excel.Range range;
        //range = xlWorkSheet.UsedRange;

        //xlApp.Range["E6:AD" + i].Select();
        //range.Subtotal(3, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum, arr, true, false, Microsoft.Office.Interop.Excel.XlSummaryRow.xlSummaryBelow);

        //xlApp.Columns["E:AD"].Select();
        //xlApp.Selection.Style = "Comma";

        //range.Activate();
        ////   xlApp.Selection.Subtotal GroupBy:= 3, Function:= xlSum, TotalList:= Array(5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30), Replace:=True, PageBreaks:= True, SummaryBelowData:= True
        ////ActiveWindow.SmallScroll Down:= 15


        //int rCount = xlWorkSheet.UsedRange.Rows.Count + 5;

        //i = rCount;
        //_Count = 0;
        //_RowCount = _DataListRemittanceBranch.Rows.Count;

        //foreach (DataRow row in _DataListRemittanceBranch.Rows)
        //{

        //    string _EmployeeName = row[1].ToString();

        //    for (j = 0; j <= _DataListRemittanceBranch.Columns.Count - 1; j++)
        //    {
        //        xlWorkSheet.Cells[i + 1, j + 1] = row[j].ToString();
        //    }

        //    i++;
        //    Application.DoEvents();
        //    _Count++;

        //    //frmMainWindow frmMainWindow = new frmMainWindow();
        //    tssDataStatus.Text = "Payroll Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
        //    pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        //}

        //xlApp.Range["E" + (i + 1)].Select();
        //xlApp.ActiveCell.Formula = "=SUM(E" + (rCount + 1) + ":E" + (i) + ")";
        //xlApp.Selection.Font.Bold = true;

        //xlApp.Selection.Copy();
        //xlApp.Range["F" + (i + 1) + ":AD" + (i + 1) + ""].Select();
        //xlApp.ActiveSheet.Paste();


        //string _BCompany = "";
        //if (cboCompany.Text == "")
        //{
        //    _BCompany = "";
        //}
        //else
        //{
        //    _BCompany = cboCompany.Text.Substring(0, 4);
        //}

        //switch (_BCompany)
        //{
        //    case "DESI":
        //        xlWorkSheet.Cells[1, 1] = "Du Ek Sam Inc.,";
        //        break;
        //    case "DESM":
        //        xlWorkSheet.Cells[1, 1] = "DES Marketing";
        //        break;
        //    default:
        //        xlWorkSheet.Cells[1, 1] = "Du Ek Sam Inc., / DES Marketing";
        //        break;
        //}

        //switch (cboGoverment.Text)
        //{
        //    case "WTAX":
        //        xlWorkSheet.Cells[2, 1] = "Summary of Withholding Tax";
        //        break;
        //    case "SSS":
        //        xlWorkSheet.Cells[2, 1] = "SSS/Philhealth Remittance";
        //        break;
        //    case "PAGIBIG":
        //        xlWorkSheet.Cells[2, 1] = "Pagibig Remittance";
        //        break;
        //    default:
        //        xlWorkSheet.Cells[2, 1] = "Summary of Remittance";
        //        break;
        //}


        //xlWorkSheet.Cells[3, 1] = "For The Month of " + _MonthName + ", " + cboPayrolPeriod.Text.Substring(0, 4);






        xlWorkBook.SaveAs(_MyFile, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        xlWorkBook.Close(true, misValue, misValue);
        xlApp.Quit();

        releaseObject(xlWorkSheet);
        releaseObject(xlWorkBook);
        releaseObject(xlApp);

        MessageBox.Show("Excel file created , you can find the file " + _MyFile);
    }



    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
        }
        finally
        {
            GC.Collect();
        }
    }
}