using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

public partial class frm13thMonth : Form
{
    public frm13thMonth()
    {
        InitializeComponent();
    }

    private void frm13thMonth_Load(object sender, EventArgs e)
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

        string _PayType = "";
        if(chk13Month.CheckState == CheckState.Checked)
        {
            _PayType = "LASTPAY";
        }
        else
        {
            _PayType = "PAYROLL";
        }

        string _sqlPayrollDisplay = "";

        _sqlPayrollDisplay = @"
        DECLARE @DFROM AS DATE
        DECLARE @DTO AS DATE

        SET @DFROM  = '" + txtDateFrom.Text + @"'
        SET @DTO  =  '" + txtDateTo.Text + @"'

        SELECT * FROM (
        SELECT A.EmployeeNo, A.EmployeeName, A.Company, A.DailyRate
        , CASE WHEN A.DateHired < @DFROM THEN @DFROM ELSE A.DateHired END AS [Service Start Date]
        , CASE WHEN A.DateFinish BETWEEN @DFROM AND @DTO THEN A.DateFinish ELSE @DTO END AS [Service End Date]
        , (SELECT MAX(X.[PayrollPeriod]) FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  A.EmployeeNo) AS [Last Payroll Period]
        , 0.00 AS [Service]
        , 0.00 [January]
        , 0.00 [February]
        , 0.00 [March]
        , 0.00 [April]
        , 0.00 [May]
        , 0.00 [June]
        , 0.00 [July]
        , 0.00 [August]
        , 0.00 [September]
        , 0.00 [October]
        , 0.00 [November]
        , 0.00 [December]
        , 0.00 [13 Month Calc]
        , A.ConfiLevel
        , CASE WHEN A.DateFinish BETWEEN @DFROM AND @DTO THEN 'LASTPAY' ELSE 'PAYROLL' END AS [Type]
        , A.Department
        , B.BCode
        , A.Category
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
                                                     AND XX.Type = '" + _PayType + @"'
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
                string getPayrollPeriod = "";
                string getSalaryWage_A = "";
                string getSalaryWage_B = "";


                int _month = 0;
                while (_month <= 11)
                {
                    _month++;

                    getPayrollPeriod = cboYear.Text + "-" + _month.ToString("00")  + "-A";


                    string _sqlSelect = "SELECT SUM(X.[BasicPay]) AS [Wage] FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND X.PayrollPeriod = '" + getPayrollPeriod + @"'";
                    DataTable _tblSelect = new DataTable();
                    _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
                    getSalaryWage_A = clsSQLClientFunctions.GetData(_tblSelect, "Wage", "1");
                    if (getSalaryWage_A == "") { getSalaryWage_A = "0.00"; }

                    if (string.Compare(getPayrollPeriod, sLastPayrollPeriod) < 0)
                    {
                    }
                    else if (string.Compare(getPayrollPeriod, sLastPayrollPeriod) == 0)
                    {
                    }
                    else
                    {
                        getSalaryWage_A = ((double.Parse(sRate) * 26) / 2).ToString("N2");
                    }




                    getPayrollPeriod = cboYear.Text + "-" + _month.ToString("00") + "-B";
                    _sqlSelect = "SELECT SUM(X.[BasicPay]) AS [Wage] FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND X.PayrollPeriod = '" + getPayrollPeriod + @"'";
                    _tblSelect = new DataTable();
                    _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
                    getSalaryWage_B = clsSQLClientFunctions.GetData(_tblSelect, "Wage", "1");
                    if (getSalaryWage_B == "") { getSalaryWage_B = "0.00"; }


                    if (string.Compare(getPayrollPeriod, sLastPayrollPeriod) < 0)
                    {
                    }
                    else if (string.Compare(getPayrollPeriod, sLastPayrollPeriod) == 0)
                    {
                    }
                    else
                    {
                        getSalaryWage_B = ((double.Parse(sRate) * 26) / 2).ToString("N2");
                    }


                    string _MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(_month);



                    if(sLastPayrollPeriod == "")
                    {
                        row[_MonthName] = 0.00;
                    }
                    else
                    {
                        row[_MonthName] = (double.Parse(getSalaryWage_A) + double.Parse(getSalaryWage_B)).ToString("N2");
                    }
                }


                // January Payroll
                //getPayrollPeriod = cboYear.Text + "-01-A";

                //string _sqlSelect = "SELECT SUM(X.[BasicPay]) AS [Wage] FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND X.PayrollPeriod = '" + getPayrollPeriod + @"'";
                //DataTable _tblSelect = new DataTable();
                //_tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
                //getSalaryWage_A = clsSQLClientFunctions.GetData(_tblSelect, "Wage", "1");
                //if (getSalaryWage_A == "") { getSalaryWage_A = "0.00"; }

                //if (string.Compare(getPayrollPeriod, sLastPayrollPeriod) < 0)
                //{
                //}
                //else if (string.Compare(getPayrollPeriod, sLastPayrollPeriod) == 0)
                //{
                //}
                //else
                //{
                //    getSalaryWage_A = (double.Parse(sRate) * 26).ToString("N2");
                //}



                //getPayrollPeriod = cboYear.Text + "-01-B";
                //_sqlSelect = "SELECT SUM(X.[BasicPay]) AS [Wage] FROM [vwsPayrollHeader] X WHERE X.EmployeeNo =  '" + _EmployeeNo + @"' AND X.PayrollPeriod = '" + getPayrollPeriod + @"'";
                //_tblSelect = new DataTable();
                //_tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
                //getSalaryWage_B = clsSQLClientFunctions.GetData(_tblSelect, "Wage", "1");
                //if (getSalaryWage_B == "") { getSalaryWage_B = "0.00"; }


                //if (string.Compare(getPayrollPeriod, sLastPayrollPeriod) < 0)
                //{
                //}
                //else if (string.Compare(getPayrollPeriod, sLastPayrollPeriod) == 0)
                //{
                //}
                //else
                //{
                //    getSalaryWage_B = (double.Parse(sRate) * 26).ToString("N2");
                //}

                //row["January"] = (double.Parse(getSalaryWage_A) + double.Parse(getSalaryWage_B)).ToString("N2");




                row["13 Month Calc"] = ((double.Parse(row["January"].ToString()) +
                    double.Parse(row["February"].ToString()) +
                    double.Parse(row["March"].ToString()) +
                    double.Parse(row["April"].ToString()) +
                    double.Parse(row["May"].ToString()) +
                    double.Parse(row["June"].ToString()) +
                    double.Parse(row["July"].ToString()) +
                    double.Parse(row["August"].ToString()) +
                    double.Parse(row["September"].ToString()) +
                    double.Parse(row["October"].ToString()) +
                    double.Parse(row["November"].ToString()) +
                    double.Parse(row["December"].ToString())) / 12).ToString("N2");






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

        _Count = 0;
        _RowCount = _DataTable.Rows.Count;
        foreach (DataRow row in _DataTable.Rows)
        {
            #region Varialble Declaration For the Employee Data

            string _Year = cboYear.Text;
            string _Company = row["Company"].ToString();
            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _EmployeeName = row["EmployeeName"].ToString();
            string _Branch = row["BCode"].ToString();
            string _Department = row["Department"].ToString();


            string _ServiceStartDate = row["Service Start Date"].ToString();
            string _ServiceEndDate = row["Service End Date"].ToString();



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
                                                   ,'" + (_DailyRate) + @"'
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
            string _13MonthCalc = row["13 Month Calc"].ToString();

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


            #endregion


            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Leave Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
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

                                SELECT *, ISNULL(LEAD(XX.[StartDate]) OVER(ORDER BY XX.[StartDate]), @DateTo) AS [EndDate]
                                        , '' AS [DFrom]
                                        , '' AS [DTo] 
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

    private void button2_Click(object sender, EventArgs e)
    {

    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void cboPosition_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}