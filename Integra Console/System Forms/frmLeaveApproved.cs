using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmLeaveApproved : Form
{
    DataTable _tblDisplay = new DataTable();
    public frmLeaveApproved()
    {
        InitializeComponent();
    }

    private void frmLeaveApproved_Load(object sender, EventArgs e)
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
        DateTime dtFrom = DateTime.Parse(clsDeclaration.sysLeaveCutoffMonth + "/" + clsDeclaration.sysLeaveCutoffDay + "/" + cboYear.Text);
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




    private string sqlQueryOutput(string _refBranch = "",
                            string _refArea = "",
                            string _refDepartment = "",
                            string _refYear = "",
                            string _refConfiLevel = "",
                            string _refConfiSelection = "",
                            string _refEmployeeNo = "")
    {


        string _sqlSyntax = "";
        _sqlSyntax = @"
SELECT YY.* FROM ( SELECT * FROM [vwsEmployeeDetails] A 
				WHERE A.ConfiLevel IN (" + _refConfiLevel + @") 
			   ) XX INNER JOIN  [LeavesApproved] YY ON XX.EmployeeNo = YY.EmployeeNo AND XX.Department = YY.Department";

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

        _sqlSyntax = _sqlSyntax + @" AND XX.[IsActive] = '0'";

        _sqlSyntax = _sqlSyntax + @" ORDER BY XX.[EmployeeName]";

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

    private void btnRowAdd_Click(object sender, EventArgs e)
    {
        frmLeaveDetails frmLeaveDetails = new frmLeaveDetails();
        frmLeaveDetails.ShowDialog();
    }

    private void lblSearchEmployeeName_Click(object sender, EventArgs e)
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

        string _sqlSelection = "";
        _sqlSelection = sqlQueryOutput(_Branch, cboArea.Text, _Deparment, cboYear.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text);
        _tblDisplay = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelection);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay);
    }
}