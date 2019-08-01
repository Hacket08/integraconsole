using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

public partial class frmPerformanceBonusPreview : Form
{

    DataTable _tblDisplay = new DataTable();
    public frmPerformanceBonusPreview()
    {
        InitializeComponent();
    }

    private void frmPerformanceBonusPreview_Load(object sender, EventArgs e)
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

        //string _selActive = "0";
        //if (rbActive.Checked == true)
        //{
        //    _selActive = "1";
        //}
        //else
        //{
        //    _selActive = "0";
        //}


        string _sqlSelection = "";
        //DataTable _tblDisplay = new DataTable();

        //string _selAll = "0";
        //if (chkAll.Checked == true)
        //{
        //    _selAll = "1";
        //}
        //else
        //{
        //    _selAll = "0";
        //}

//        _sqlSelection = sqlQueryOutputBonus(_Branch, cboArea.Text, _Deparment, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text);
//        _tblDisplay = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelection);
//        //clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay);


//        double _RowCount;
//        int _Count = 0;
//        _RowCount = _tblDisplay.Rows.Count;

//        foreach (DataRow row in _tblDisplay.Rows)
//        {
//            try
//            {
//                string _EmployeeNo = row["EmployeeNo"].ToString();
//                string _EmployeeName = row["EmployeeName"].ToString();
//                string _DailyRate = row["DailyRate"].ToString();
//                string _Company = row["Company"].ToString();
//                string _BCode = row["BCode"].ToString();
//                string _Department = row["Department"].ToString();
//                string _EEStatus = row["EEStat"].ToString();


//                string _DateHired = row["DateHired"].ToString();
//                string _DateRegular = row["DateRegular"].ToString();

//                double _Service = double.Parse(getAnnualService(_EmployeeNo, cboYear.Text).ToString("N2"));
//                double _13MonthAmount = double.Parse(get13MonthAmount(_EmployeeNo, cboYear.Text).ToString("N2"));

//                string _sqlInsertData = "";
//                _sqlInsertData = @"
//                                    DELETE FROM [dbo].[PerformanceBonusDetails] WHERE [EmployeeNo] = '" + _EmployeeNo + @"' AND [Year] = '" + cboYear.Text + @"'

//INSERT INTO [dbo].[PerformanceBonusDetails]
//           ([Year]
//           ,[EmployeeNo]
//           ,[EmployeeName]
//           ,[Company]
//           ,[Branch]
//           ,[Department]
//           ,[DateHired]
//           ,[DateRegular]
//           ,[DailyRate]
//           ,[Service]
//           ,[Appraisal]
//           ,[Br Adj]
//           ,[Idv Adj]
//           ,[Final Rating]
//           ,[13th Month]
//           ,[Bonus Amt]
//           ,[TotalAmt]
//,[Adjustment]
//,[FinalAmt])
//     VALUES
//           ('" + cboYear.Text + @"'
//           ,'" + _EmployeeNo + @"'
//           ,'" + _EmployeeName + @"'
//           ,'" + _Company + @"'
//           ,'" + _Branch + @"'
//           ,'" + _Department + @"'
//           ,'" + _DateHired + @"'
//           ,'" + _DateRegular + @"'
//           ,'" + _DailyRate + @"'
//           ,'" + _Service + @"'
//           ,0.00
//           ,0.00
//           ,0.00
//           ,0.00
//           ,'" + _13MonthAmount + @"'
//           ,0.00
//           ,'" + _13MonthAmount + @"'
//           ,0.00
//           ,0.00
//)
//                                            ";

//                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlInsertData);


//                Application.DoEvents();
//                _Count++;
//                tssDataStatus.Text = "Leave Credit Generation Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
//                pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));


//            }
//            catch
//            {

//            }
//        }


        _sqlSelection = sqlQueryOutputPerformanceBonusPreview(_Branch, cboArea.Text, _Deparment, cboYear.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text);
        _tblDisplay = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelection);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay, "Bonus");

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



    private double getAnnualService(string _EmployeeNo, string _Year)
    {
        string _sqlSelect = @"";
        _sqlSelect = @"
SELECT SUM(XX.Days) AS [Days] FROM (
SELECT LEFT(PayrollPeriod, 7) AS [Month], (SUM(TotalDays) / 26) AS [Days] FROM vwsPayrollDetails WHERE EmployeeNo = '" + _EmployeeNo  + @"' AND AccountCode = '1-100' AND LEFT(PayrollPeriod, 4) = '" + _Year + @"'
GROUP BY  LEFT(PayrollPeriod, 7)) XX

";

        DataTable _tblSelect = new DataTable();
        _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);

        double _ServiceOutput = 0;
        double.TryParse(clsSQLClientFunctions.GetData(_tblSelect, "Days", "1"), out _ServiceOutput);

        return _ServiceOutput;
    }



    private double get13MonthAmount(string _EmployeeNo, string _Year)
    {
        string _sqlSelect = @"";
        _sqlSelect = @"
SELECT A.[13thMon] AS [Amt] FROM [13MonthDetails] A WHERE A.Validated = 1  AND A.EmployeeNo = '" + _EmployeeNo + @"' AND A.Year = '" + _Year + @"'
";

        DataTable _tblSelect = new DataTable();
        _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);

        double _AmountOutput = 0;
        double.TryParse(clsSQLClientFunctions.GetData(_tblSelect, "Amt", "1"), out _AmountOutput);

        return _AmountOutput;
    }


    #region Performance Bonus Details
    private string sqlQueryOutputBonus(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "",
                                string _refEmployeeNo = "")
    {


        string _sqlSyntax = "";
        _sqlSyntax = @"
SELECT * FROM ( SELECT * FROM [vwsEmployeeDetails] A 
				WHERE A.ConfiLevel IN (0,1,2)
			   ) XX ";

        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";
        //_sqlSyntax = _sqlSyntax + @" AND YY.[Year] = '" + _refYear + @"'";

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



    #region Performance Bonus Preview


    private string sqlQueryOutputPerformanceBonusPreview(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refYear = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "",
                                string _refEmployeeNo = "")
    {


        string _sqlSyntax = "";
        _sqlSyntax = @"
SELECT YY.*, XX.BranchName, XX.EmpPosition  FROM ( SELECT * FROM [vwsEmployeeDetails] A 
				WHERE A.ConfiLevel IN (" + _refConfiLevel + @") 
			   ) XX INNER JOIN  [PerformanceBonusDetails] YY ON XX.EmployeeNo = YY.EmployeeNo AND XX.Department = YY.Department";

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


        clsDeclaration.sReportID = 8;

        clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Performace Report.rpt";
        clsDeclaration.sQueryString = sqlQueryOutputPerformanceBonusPreview(_Branch, cboArea.Text, _Deparment, cboYear.Text, clsDeclaration.sConfiLevel, clsDeclaration.sConfiLevelSelection, txtEmpNo.Text);

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
            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _EmployeeName = row["EmployeeName"].ToString();


            string _oID = row["oID"].ToString();

            string _Appraisal = row[11].ToString();
            string _BrAdj = row[12].ToString();
            string _IdvAdj = row[13].ToString();
            string _FinalRating = row[14].ToString();


            string _BonusAmt = row[16].ToString();



            string _Adjustment = row[17].ToString();

            string _TotalAmt = row[18].ToString();
            string _FinalAmt = row[19].ToString();
            string _Remarks = row[20].ToString();


            string _sqlInsertLeaveDetails = "";

            _sqlInsertLeaveDetails = @"    
                                   UPDATE A
                                               SET [Appraisal] = '" + _Appraisal + @"'
                                                  ,[Br Adj] = '" + _BrAdj + @"'
                                                  ,[Idv Adj] = '" + _IdvAdj + @"'
                                                  ,[Final Rating] = '" + _FinalRating + @"'
                                                  ,[Bonus Amt] = '" + _BonusAmt + @"'
                                                  ,[TotalAmt] = '" + _TotalAmt + @"'
                                                  ,[Adjustment] = '" + _Adjustment + @"'
                                                  ,[FinalAmt] = '" + _FinalAmt + @"'
                                                  ,[Remarks] = '" + _Remarks + @"'
	                                              FROM [dbo].[PerformanceBonusDetails] A
                                             WHERE A.oID = '" + _oID + @"'
                                                     ";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlInsertLeaveDetails);
            #endregion


            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Leave Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }

        MessageBox.Show("Performance Bonus Successfully Saved");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void dgvDisplay_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            string _Rate = dgvDisplay.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
            string _Service = dgvDisplay.Rows[e.RowIndex].Cells[10].Value.ToString().Trim();

            string _Appraisal = dgvDisplay.Rows[e.RowIndex].Cells[11].Value.ToString().Trim();
            string _BRAdj = dgvDisplay.Rows[e.RowIndex].Cells[12].Value.ToString().Trim();
            string _IdvAdj = dgvDisplay.Rows[e.RowIndex].Cells[13].Value.ToString().Trim();

            dgvDisplay.Rows[e.RowIndex].Cells[14].Value = double.Parse(_Appraisal) - double.Parse(_BRAdj) - double.Parse(_IdvAdj);

            string _FinalRate = dgvDisplay.Rows[e.RowIndex].Cells[14].Value.ToString().Trim();
            dgvDisplay.Rows[e.RowIndex].Cells[16].Value = (double.Parse(_Rate) * double.Parse(_Service)) * 21 / 12 * (double.Parse(_FinalRate) / 100);


            string _13ThMonth = dgvDisplay.Rows[e.RowIndex].Cells[15].Value.ToString().Trim();
            string _Bonus = dgvDisplay.Rows[e.RowIndex].Cells[16].Value.ToString().Trim();

            string _ComputedAmt = dgvDisplay.Rows[e.RowIndex].Cells[18].Value.ToString().Trim();
            string _FinalAmt = dgvDisplay.Rows[e.RowIndex].Cells[19].Value.ToString().Trim();

            dgvDisplay.Rows[e.RowIndex].Cells[17].Value = double.Parse(_FinalAmt) - double.Parse(_ComputedAmt);
            string _Adjustment = dgvDisplay.Rows[e.RowIndex].Cells[17].Value.ToString().Trim();
            dgvDisplay.Rows[e.RowIndex].Cells[18].Value = double.Parse(_13ThMonth) + double.Parse(_Bonus);
            dgvDisplay.Rows[e.RowIndex].Cells[19].Value = double.Parse(_13ThMonth) + double.Parse(_Bonus) + double.Parse(_Adjustment);



            dgvDisplay.UpdateCellValue(14, e.RowIndex);
            dgvDisplay.UpdateCellValue(16, e.RowIndex);
            dgvDisplay.UpdateCellValue(18, e.RowIndex);
            dgvDisplay.UpdateCellValue(19, e.RowIndex);

        }
        catch
        {
            string _Rate = dgvDisplay.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
            string _Service = dgvDisplay.Rows[e.RowIndex].Cells[10].Value.ToString().Trim();

            string _Appraisal = "0.00";
            string _BRAdj = "0.00";
            string _IdvAdj = "0.00";

            dgvDisplay.Rows[e.RowIndex].Cells[14].Value = double.Parse(_Appraisal) - double.Parse(_BRAdj) - double.Parse(_IdvAdj);

            string _FinalRate = dgvDisplay.Rows[e.RowIndex].Cells[14].Value.ToString().Trim();
            dgvDisplay.Rows[e.RowIndex].Cells[16].Value = (double.Parse(_Rate) * double.Parse(_Service)) * 21 / 12 * (double.Parse(_FinalRate) / 100);


            string _13ThMonth = dgvDisplay.Rows[e.RowIndex].Cells[15].Value.ToString().Trim();
            string _Bonus = dgvDisplay.Rows[e.RowIndex].Cells[16].Value.ToString().Trim();

            string _ComputedAmt = dgvDisplay.Rows[e.RowIndex].Cells[18].Value.ToString().Trim();
            string _FinalAmt = dgvDisplay.Rows[e.RowIndex].Cells[19].Value.ToString().Trim();

            dgvDisplay.Rows[e.RowIndex].Cells[17].Value = double.Parse(_FinalAmt) - double.Parse(_ComputedAmt);
            string _Adjustment = dgvDisplay.Rows[e.RowIndex].Cells[17].Value.ToString().Trim();
            dgvDisplay.Rows[e.RowIndex].Cells[18].Value = double.Parse(_13ThMonth) + double.Parse(_Bonus);
            dgvDisplay.Rows[e.RowIndex].Cells[19].Value = double.Parse(_13ThMonth) + double.Parse(_Bonus) + double.Parse(_Adjustment);


            dgvDisplay.UpdateCellValue(11, e.RowIndex);
            dgvDisplay.UpdateCellValue(12, e.RowIndex);
            dgvDisplay.UpdateCellValue(13, e.RowIndex);
            dgvDisplay.UpdateCellValue(14, e.RowIndex);
            dgvDisplay.UpdateCellValue(16, e.RowIndex);
            dgvDisplay.UpdateCellValue(18, e.RowIndex);
            dgvDisplay.UpdateCellValue(19, e.RowIndex);
        }
    }
}