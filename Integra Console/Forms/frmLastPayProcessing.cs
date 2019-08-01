using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmLastPayProcessing : Form
{
    private static DataTable _DataList = new DataTable();

    public static string _gCompany;
    public static string _gEmpCode;
    public static string _gEmpName;
    public static string _gPayrollPeriod;

    public frmLastPayProcessing()
    {
        InitializeComponent();
    }

    private void frmLastPayProcessing_Load(object sender, EventArgs e)
    {
        //rbAll.Checked = true;
        chkRankAndFile.Checked = true;
        chkSupervisory.Checked = true;
        chkManagerial.Checked = true;

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        cboPayrolPeriod.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());
        }

        _SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
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
    private void button1_Click(object sender, EventArgs e)
    {

        //string _Category = "";
        //if (rbMonthly.Checked == true)
        //{
        //    _Category = "0";
        //}
        //else if (rbDaily.Checked == true)
        //{
        //    _Category = "1";
        //}


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



        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please select payroll period");
            return;
        }

        DataTable _DataTable;
        string _SQLSyntax;

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


        _SQLSyntax = "SELECT 'TRUE' FROM [PayrollLocker] A WHERE A.[PayrollPeriod] = '" + cboPayrolPeriod.Text + "' AND A.[Branch] LIKE '%" + _BCode + "%' AND [Position] LIKE '%" + _BPosition + "%'   AND [IsLocked] = '1'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        if (_DataTable.Rows.Count != 0)
        {
            MessageBox.Show("Payroll Period Already Locked To This Branch And Department!");
            return;
        }


        _SQLSyntax = @"
SELECT * FROM (
        SELECT A.Company, A.EmployeeNo, CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName],
        A.DailyRate, A.RateDivisor, A.MonthlyRate, A.TaxStatus 
        ,0.00 AS [TotalDays]
        ,0.00 AS [BasicPay]
        ,0.00 AS [OTHrs]
        ,0.00 AS [OTPay]
        ,0.00 AS [Spl HolHrs]
        ,0.00 AS [Spl HolPay]
        ,0.00 AS [Legal HolHrs]
        ,0.00 AS [Legal HolPay]
        ,0.00 AS [Other Income]
        ,0.00 AS [Gross Pay]
        ,0.00 AS [SSS Loan]
        ,0.00 AS [PAGIBIG Loan]
        ,0.00 AS [Other Deduction]

        ,0.00 AS [SSS Employee] 
        ,0.00 AS [SSS Employer] 
        ,0.00 AS [SSS ECC] 
        ,0.00 AS [PAGIBIG Employee] 
        ,0.00 AS [PAGIBIG Employer] 
        ,0.00 AS [PHILHEALTH Employee] 
        ,0.00 AS [PHILHEALTH Employer] 
        ,0.00 AS [WTax Amount]
        ,0.00 AS [Total Deduction]
        ,0.00 AS [Net Pay]


        ,0.00 AS [COLAAmount]
        ,0.00 AS [TotalHrs]
        ,0.00 AS [OtherLoan]


        ,0.00 AS [SUNHrs]
        ,0.00 AS [SUNPay]
        ,0.00 AS [Calamity Loan]

, A.ConfiLevel
, A.AttendanceExempt
        FROM vwsEmployees A INNER JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
        WHERE B.BCode LIKE '%" + _BCode + @"%' AND B.Area LIKE '%" + cboArea.Text + @"%' AND A.EmpStatus IN (3,4,6)
        AND A.EmployeeNo  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtEmpNo.Text + @"%' 
        AND CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName)   COLLATE Latin1_General_CI_AS  LIKE  '%" + txtName.Text + @"%'
        AND B.DepCode LIKE '%" + _BPosition + @"%'
        AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
        AND A.DateHired < (SELECT Z.DateTwo FROM vwsPayrollPeriod Z WHERE Z.PayrollPeriod = '" + cboPayrolPeriod.Text + @"')


) XX
WHERE XX.ConfiLevel IN (" + _GroupConfi + @")
        ORDER BY XX.[EmployeeName]
        ";

        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        double _PayrollMonth;
        string _PayrollYear;
        _PayrollMonth = 12 - double.Parse(cboPayrolPeriod.Text.Substring(5, 2));
        _PayrollYear = cboPayrolPeriod.Text.Substring(0, 4);




        double _RowCount;
        int _Count = 0;
        _RowCount = _DataTable.Rows.Count;

        foreach (DataRow row in _DataTable.Rows)
        {
            string _Company = row[0].ToString();
            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);


            string _Employee = row[1].ToString();
            string _EmployeeName = row[2].ToString();

            double _DailyRate = double.Parse(row[3].ToString());


            string _sqlList;
            _sqlList = @"SELECT ISNULL(SUM(A.TotalDays), 0) AS TotalDays
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] = 0";
            double _TotalDays = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "TotalDays");
            row[7] = Math.Round(_TotalDays, 5, MidpointRounding.AwayFromZero).ToString("0.00000");

            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS BasicPay 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] = 0";
            double _BasicPay = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "BasicPay");

            row[8] = Math.Round(_BasicPay, 2, MidpointRounding.AwayFromZero).ToString("0.00");

            _sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS OTHrs 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] = 1";
            double _OTHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "OTHrs");

            row[9] = Math.Round(_OTHrs, 5, MidpointRounding.AwayFromZero).ToString("0.00000");

            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS OTPay 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] = 1";
            double _OTPay = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "OTPay");

            row[10] = Math.Round(_OTPay, 2, MidpointRounding.AwayFromZero).ToString("0.00");



            _sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS SunSplHolHrs 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] IN (2, 3)
                AND A.AccountCode NOT IN ('3-203','3-208','3-214','3-215','4-307','4-308','4-311','4-309','4-310','4-302')";
            double _SunSplHolHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SunSplHolHrs");

            row[11] = Math.Round(_SunSplHolHrs, 5, MidpointRounding.AwayFromZero).ToString("0.00000");


            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS SunSplHolPay 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] IN (2, 3)
                AND A.AccountCode NOT IN ('3-203','3-208','3-214','3-215','4-307','4-308','4-311','4-309','4-310','4-302')";
            double _SunSplHolPay = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SunSplHolPay");

            row[12] = Math.Round(_SunSplHolPay, 2, MidpointRounding.AwayFromZero).ToString("0.00");


            _sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS LegalHolHrs 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] IN (2, 3)
                AND A.AccountCode IN ('3-203','3-208','3-214','3-215','4-307','4-308','4-311','4-309','4-310')";
            double _LegalHolHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "LegalHolHrs");

            row[13] = Math.Round(_LegalHolHrs, 5, MidpointRounding.AwayFromZero).ToString("0.00000");


            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS LegalHolPay 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] IN (2, 3)
                AND A.AccountCode IN ('3-203','3-208','3-214','3-215','4-307','4-308','4-311','4-309','4-310')";
            double _LegalHolPay = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "LegalHolPay");

            row[14] = Math.Round(_LegalHolPay, 2, MidpointRounding.AwayFromZero).ToString("0.00");



            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS OtherIncome 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] IN (6,4)";
            double _OtherIncome = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "OtherIncome");

            row[15] = Math.Round(_OtherIncome, 2, MidpointRounding.AwayFromZero).ToString("0.00");




            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS SSSLoan 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.AccountCode IN ('8-502')";
            double _SSSLoan = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SSSLoan");

            row[17] = Math.Round(_SSSLoan, 2, MidpointRounding.AwayFromZero).ToString("0.00");


            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS PAGIBIGLoan 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.AccountCode IN ('8-504')";
            double _PAGIBIGLoan = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "PAGIBIGLoan");

            row[18] = Math.Round(_PAGIBIGLoan, 2, MidpointRounding.AwayFromZero).ToString("0.00");




            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS CalamityLoan 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.AccountCode IN ('8-503', '8-516')";
            double _CalamityLoan = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "CalamityLoan");

            row[35] = Math.Round(_CalamityLoan, 2, MidpointRounding.AwayFromZero).ToString("0.00");


            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS OtherLoan 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.AccountType IN (7) AND A.AccountCode NOT IN ('8-504','8-502','8-503', '8-516')";

            double _OtherLoan = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "OtherLoan");
            row[32] = Math.Round(_OtherLoan, 2, MidpointRounding.AwayFromZero).ToString("0.00000");



            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS OtherDeduction 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.AccountType IN (8)";
            double _OtherDeduction = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "OtherDeduction");

            row[19] = Math.Round(_OtherDeduction, 2, MidpointRounding.AwayFromZero).ToString("0.00");



            //Goverment Deductions

            string _SettingSyntax;
            _SettingSyntax = @"
                                SELECT 

                                A.EmployeeNo
                                ,D.CustomPYCode
                                ,D.SSSSchedule01
                                ,D.SSSSchedule02
                                ,D.SSSSchedule06

                                ,D.PagIbigSchedule01
                                ,D.PagIbigSchedule02
                                ,D.PagIbigSchedule06

                                ,D.PHealthSchedule01
                                ,D.PHealthSchedule02
                                ,D.PHealthSchedule06

                                ,D.TaxSchedule01
                                ,D.TaxSchedule06


                                FROM CustomPYSetup D INNER JOIN Employees A ON A.CustomPYCode = D.CustomPYCode
                                WHERE A.EmployeeNo = '" + _Employee + @"'
                              ";



            string _SSSSchedule01 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "SSSSchedule01");
            string _SSSSchedule02 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "SSSSchedule02");
            string _SSSSchedule06 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "SSSSchedule06");

            string _PagIbigSchedule01 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "PagIbigSchedule01");
            string _PagIbigSchedule02 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "PagIbigSchedule02");
            string _PagIbigSchedule06 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "PagIbigSchedule06");

            string _PHealthSchedule01 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "PHealthSchedule01");
            string _PHealthSchedule02 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "PHealthSchedule02");
            string _PHealthSchedule06 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "PHealthSchedule06");

            string _TaxSchedule01 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "TaxSchedule01");
            string _TaxSchedule06 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "TaxSchedule01");




            string _Rate = row[3].ToString();
            string _Monthly = row[5].ToString();
            string _TaxStatus = row[6].ToString();




            _sqlList = "SELECT Z.Employer, Z.Employee, Z.ECC FROM SSSTable Z WHERE '" + _Monthly + "' BETWEEN Z.BracketFrom AND Z.BracketTo";
            double _SSSEmployee = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employee");
            double _SSSEmployer = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employer");
            double _SSSECC = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "ECC");

            row[20] = 0.00;
            row[21] = 0.00;
            row[22] = 0.00;

            //if (_SSSSchedule01 == "1")
            //{
            //    row[20] = Math.Round(_SSSEmployee / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[21] = Math.Round(_SSSEmployer / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[22] = Math.Round(_SSSECC / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}
            //if (_SSSSchedule02 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "A")
            //{
            //    row[20] = Math.Round(_SSSEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[21] = Math.Round(_SSSEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[22] = Math.Round(_SSSECC, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}
            //if (_SSSSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            //{
            //    row[20] = Math.Round(_SSSEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[21] = Math.Round(_SSSEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[22] = Math.Round(_SSSECC, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}






            _sqlList = @"SELECT * FROM 
                            (SELECT *,
                            ISNULL(LEAD(p.IncomeBracket) OVER(ORDER BY p.IncomeBracket), 99999) NextValue
                            FROM PAGIBIGTable p)
                            Z WHERE '" + _Monthly + "' BETWEEN Z.IncomeBracket AND Z.NextValue";


            double _MaxCont = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "MaxContribution");
            double _PAGIBIGEmployee = Math.Round(double.Parse(_Monthly) * (clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employee") / 100), 0, MidpointRounding.AwayFromZero);
            double _PAGIBIGEmployer = Math.Round(double.Parse(_Monthly) * (clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employer") / 100), 0, MidpointRounding.AwayFromZero);

            if (_PAGIBIGEmployee > _MaxCont)
            {
                _PAGIBIGEmployee = _MaxCont;
                _PAGIBIGEmployer = _MaxCont;
            }

            row[23] = 0.00;
            row[24] = 0.00;


            //if (_PagIbigSchedule01 == "1")
            //{
            //    row[23] = Math.Round(_PAGIBIGEmployee / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[24] = Math.Round(_PAGIBIGEmployer / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}
            //if (_PagIbigSchedule02 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "A")
            //{
            //    row[23] = Math.Round(_PAGIBIGEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[24] = Math.Round(_PAGIBIGEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}
            //if (_PagIbigSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            //{
            //    row[23] = Math.Round(_PAGIBIGEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[24] = Math.Round(_PAGIBIGEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}


            //_sqlList = @"SELECT Z.Employer, Z.Employee FROM PhilHealthTable Z 
            //            WHERE '" + _Monthly + @"' BETWEEN Z.BracketFrom AND Z.BracketTo";
            _sqlList = @"SELECT Z.Employee, Z.Employer, Z.Rate FROM PhilHealthTable Z WHERE '" + _DailyRate + @"' BETWEEN Z.BracketFrom AND Z.BracketTo";
            double _PhilHealthEmployee = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employee");
            double _PhilHealthEmployer = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employer");
            double _PhilHealthRate = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Rate");

            row[25] = 0.00;
            row[26] = 0.00;


            //if (_PhilHealthRate != 1)
            //{
            //    double _PhilHealthBase = (_DailyRate * 26 * (_PhilHealthRate / 100));

            //    _PhilHealthEmployer = (Math.Round(_PhilHealthBase, 2, MidpointRounding.AwayFromZero) / 2);
            //    _PhilHealthEmployee = (Math.Round(_PhilHealthBase, 2, MidpointRounding.AwayFromZero) - Math.Round(_PhilHealthEmployer, 2, MidpointRounding.AwayFromZero));

            //}

            //if (_PHealthSchedule01 == "1")
            //{
            //    row[25] = Math.Round(_PhilHealthEmployee / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[26] = Math.Round(_PhilHealthEmployer / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}
            //if (_PHealthSchedule02 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "A")
            //{
            //    row[25] = Math.Round(_PhilHealthEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[26] = Math.Round(_PhilHealthEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}
            //if (_PHealthSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            //{
            //    row[25] = Math.Round(_PhilHealthEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //    row[26] = Math.Round(_PhilHealthEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}




            _sqlList = "SELECT * FROM TaxStatus Z WHERE Z.StatusCode = '" + _TaxStatus + "'";

            double _PersonalExempt = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "PersonalExempt");
            double _Dependents = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Dependents");



            double _TotalConribution;
            double _TotalExemption;

            _TotalConribution = (_SSSEmployee * 12) + (_PAGIBIGEmployee * 12) + (_PhilHealthEmployee * 12);
            _TotalExemption = _PersonalExempt + _Dependents;



            //_GOVSyntax = @"SELECT ISNULL(ROUND(SUM(Z.Amount),2),0) AS RegularPay
            //                    FROM PayrollDetails Z WHERE Z.AccountCode in 
            //                    (select AccountCode from AccountCode X WHERE X.AccountType IN (0)) 
            //                    AND Z.EmployeeNo = '" + _Employee + "' AND SUBSTRING(Z.PayrollPeriod,1,7) = '" + cboPayrolPeriod.Text.Substring(0, 7) + "'";
            //_GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

            //double _BasicPay;
            //_BasicPay = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "RegularPay", "1"));



            _sqlList = @"SELECT ISNULL(SUM(Z.WitholdingTax),0) AS WitholdingTax FROM PayrollHeader Z 
                                WHERE CAST(SUBSTRING(Z.PayrollPeriod,6,2) AS INT) < '" + double.Parse(cboPayrolPeriod.Text.Substring(5, 2)) + @"' 
                                AND EmployeeNo = '" + _Employee + "' AND SUBSTRING(Z.PayrollPeriod,1,4) = '" + _PayrollYear + "'";

            double _WitholdingTax = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "WitholdingTax");
            double _GrossCompensation = _WitholdingTax + _BasicPay + (double.Parse(_Monthly) * _PayrollMonth);



            double _DeductionAmount;
            double _TaxableIncome;

            _DeductionAmount = _TotalExemption + _TotalConribution;
            _TaxableIncome = _GrossCompensation - _DeductionAmount;




            _sqlList = @"SELECT * FROM 
                            (SELECT *,
                              (ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
                              FROM AnnualTaxTable P) Z
                            WHERE '" + _TaxableIncome + "' BETWEEN Z.Excess AND Z.NextValue";

            double _WtaxExcess = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Excess");
            double _WtaxBase = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Base");
            double _WtaxRate = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Rate");


            double _BasedComputation;
            double _WTaxAdditional;
            double _WtaxAmount;

            _BasedComputation = _TaxableIncome - _WtaxExcess;
            _WTaxAdditional = (_BasedComputation * (_WtaxRate / 100));
            _WtaxAmount = ((_WtaxBase + _WTaxAdditional) / 12);


            row[27] = 0.00;

            //if (_TaxSchedule01 == "1")
            //{
            //    row[27] = Math.Round(_WtaxAmount / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}

            //if (_TaxSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            //{
            //    row[27] = Math.Round(_WtaxAmount, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            //}



            double _TotalDeduction = 0;
            double _NetPay = 0;

            _SSSEmployee = double.Parse(row[20].ToString());
            _PAGIBIGEmployee = double.Parse(row[23].ToString());
            _PhilHealthEmployee = double.Parse(row[25].ToString());
            _WtaxAmount = double.Parse(row[27].ToString());



            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS COLAAmount 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.AccountCode IN ('7-405')";
            double _COLAAmount = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "COLAAmount");

            row[30] = Math.Round(_COLAAmount, 2, MidpointRounding.AwayFromZero).ToString("0.00");



            _sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS TotalHrs 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.[AccountType] = 0";
            double _TotalHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "TotalHrs");
            row[31] = Math.Round(_TotalHrs, 5, MidpointRounding.AwayFromZero).ToString("0.00000");



            _sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS SUNHrs 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.AccountCode IN ('4-302')";
            double _SUNHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SUNHrs");

            row[33] = Math.Round(_SUNHrs, 5, MidpointRounding.AwayFromZero).ToString("0.00000");


            _sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS SUNPay 
                FROM dbo.[fnTabPayrollTransaction]('" + _Employee + @"','" + cboPayrolPeriod.Text + @"') A 
                WHERE A.Amount <> 0 AND A.AccountCode IN ('4-302')";
            double _SUNPay = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SUNPay");

            row[34] = Math.Round(_SUNPay, 2, MidpointRounding.AwayFromZero).ToString("0.00");




            double _GrossPay = (_BasicPay + _OTPay + _SunSplHolPay + _LegalHolPay + _OtherIncome + _SUNPay);
            row[16] = Math.Round(_GrossPay, 2, MidpointRounding.AwayFromZero);


            _TotalDeduction = _SSSLoan + _PAGIBIGLoan + _CalamityLoan + _OtherLoan + _OtherDeduction + _SSSEmployee + _PAGIBIGEmployee + _PhilHealthEmployee + _WtaxAmount;
            _NetPay = _GrossPay - _TotalDeduction;

            row[28] = Math.Round(_TotalDeduction, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            row[29] = Math.Round(_NetPay, 2, MidpointRounding.AwayFromZero).ToString("0.00");




            // Excel Progress Monitoring
            Application.DoEvents();
            _Count++;

            //frmMainWindow frmMainWindow = new frmMainWindow();
            tssDataStatus.Text = "Payroll Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }


        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable, "PayrollProcess");
        _DataList = _DataTable;
        MessageBox.Show("Data Ready To Upload");
    }
    private void btnUpload_Click(object sender, EventArgs e)
    {

        string _sqlList = "";

        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please select payroll period");
            return;
        }

        DataTable _DataTable;
        string _SQLSyntax;

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


        _SQLSyntax = "SELECT 'TRUE' FROM [PayrollLocker] A WHERE A.[PayrollPeriod] = '" + cboPayrolPeriod.Text + "' AND A.[Branch] LIKE '%" + _BCode + "%' AND [Position] LIKE '%" + _BPosition + "%'   AND [IsLocked] = '1'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        if (_DataTable.Rows.Count != 0)
        {
            MessageBox.Show("Payroll Period Already Locked To This Branch And Department!");
            return;
        }


        _SQLSyntax = "SELECT  A.Company, A.EmployeeNo FROM vwsEmployees A WHERE A.EmpStatus IN (3,4,6)";
        DataTable _tblData = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        foreach (DataRow row in _DataList.Rows)
        {
            string _Company = row[0].ToString();
            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

            string _Employee = row[1].ToString();
            string _PayrollPeriod = cboPayrolPeriod.Text;

            string _SQLExecute = @"
                                        DECLARE @Employee NVARCHAR(30)
                                        DECLARE @PayPeriod NVARCHAR(30)

                                        SET @Employee = '" + _Employee + @"'
                                        SET @PayPeriod = '" + _PayrollPeriod + @"'

	                                    DELETE FROM [PayrollDetails] WHERE [EmployeeNo] = @Employee AND [PayrollPeriod] = @PayPeriod
                                        DELETE FROM [PayrollHeader] WHERE [EmployeeNo] = @Employee AND [PayrollPeriod] = @PayPeriod
                                                ";
            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLExecute);
        }



        double _RowCount;
        int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            Application.DoEvents();

            string _Company = row[0].ToString();
            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

            string _Employee = row[1].ToString();
            string _EmployeeName = row[2].ToString();

            double _DailyRate = double.Parse(row[3].ToString());
            double _MonthlyRate = double.Parse(row[4].ToString());
            string _PayrollPeriod = cboPayrolPeriod.Text;


            double _BasicPay = double.Parse(row[8].ToString());
            double _OTPay = double.Parse(row[10].ToString());
            double _OtherIncome = double.Parse(row[15].ToString());
            double _Month13th = 0.00;

            double _SSSEmployee = double.Parse(row[20].ToString());
            double _SSSEmployer = double.Parse(row[21].ToString());
            double _SSSEC = double.Parse(row[22].ToString());
            double _PagIbigEmployee = double.Parse(row[23].ToString());
            double _PagIbigEmployer = double.Parse(row[24].ToString());
            double _PhilHealthEmployee = double.Parse(row[25].ToString());
            double _PhilHealthEmployer = double.Parse(row[26].ToString());
            double _WitholdingTax = double.Parse(row[27].ToString());

            double _GrossPay = double.Parse(row[16].ToString());
            double _TotalDeductions = double.Parse(row[28].ToString());
            double _NetPay = double.Parse(row[29].ToString());
            double _COLAAmount = double.Parse(row[30].ToString());
            double _OtherDeduction = double.Parse(row[19].ToString());

            double _TotalDays = double.Parse(row[7].ToString());
            double _TotalHrs = double.Parse(row[31].ToString());


            double _SSSLoan = double.Parse(row[17].ToString());
            double _PagibigLoan = double.Parse(row[18].ToString());
            double _CalamityLoan = double.Parse(row[35].ToString());
            double _OtherLoan = double.Parse(row[32].ToString());


            double _OTHrs = double.Parse(row[9].ToString());

            double _SPLHrs = double.Parse(row[11].ToString());
            double _SPLPay = double.Parse(row[12].ToString());


            double _LEGHrs = double.Parse(row[13].ToString());
            double _LEGPay = double.Parse(row[14].ToString());

            double _SUNHrs = double.Parse(row[33].ToString());
            double _SUNPay = double.Parse(row[34].ToString());



            double _SSSBaseAmount = _MonthlyRate;
            double _PhilHealthBaseAmount = _MonthlyRate;
            double _PagIbigBaseAmount = _MonthlyRate;
            double _TaxBaseAmount = _BasicPay;

            _sqlList = "SELECT A.Department,A.BankAccountNo, A.PayrollMode  FROM Employees A WHERE A.EmployeeNo = '" + _Employee + "'";
            string _Department = clsSQLClientFunctions.GetStringValue(_ConCompany, _sqlList, "Department");
            string _BankAccountNo = clsSQLClientFunctions.GetStringValue(_ConCompany, _sqlList, "BankAccountNo");
            string _PayrollMode = clsSQLClientFunctions.GetStringValue(_ConCompany, _sqlList, "PayrollMode");


            string _SQLExecute = @"
                                        DECLARE @Employee NVARCHAR(30)
                                        DECLARE @PayPeriod NVARCHAR(30)

                                        SET @Employee = '" + _Employee + @"'
                                        SET @PayPeriod = '" + _PayrollPeriod + @"'


                                        DELETE FROM [PayrollDetails] WHERE [EmployeeNo] = @Employee AND [PayrollPeriod] = @PayPeriod
                                        DELETE FROM [PayrollHeader] WHERE [EmployeeNo] = @Employee AND [PayrollPeriod] = @PayPeriod


                                        INSERT INTO [PayrollDetails] 
                                        SELECT A.[PayrollPeriod]
                                                ,A.[EmployeeNo]
                                                ,A.[AccountCode]
                                                ,A.[LoanRefenceNo]
                                                ,A.[NoOfHrs]
                                                ,ROUND(A.[Amount],2)
                                                ,A.[NoOfMins]
                                                ,A.[BillingAmount]
                                                ,A.[TotalHrs]
                                                ,A.[TotalDays]
                                                ,'" + _BCode + @"'
                                                ,'" + _Department + @"'
                                            FROM dbo.[fnTabPayrollTransaction](@Employee,@PayPeriod) A 
                                            WHERE A.Amount <> 0 


                                        INSERT INTO [dbo].[PayrollHeader]
                                                   ([PayrollPeriod]
                                                   ,[EmployeeNo]
                                                   ,[MonthlyRate]
                                                   ,[DailyRate]
                                                   ,[DepartmentCode]
                                                   ,[Department]
                                                   ,[BasicPay]
                                                   ,[OTPay]
                                                   ,[OtherIncome]
                                                   ,[Month13th]
                                                   ,[SSSEmployee]
                                                   ,[SSSEmployer]
                                                   ,[SSSEC]
                                                   ,[PhilHealthEmployee]
                                                   ,[PhilHealthEmployer]
                                                   ,[PagIbigEmployee]
                                                   ,[PagIbigEmployer]
                                                   ,[WitholdingTax]
                                                   ,[OtherDeduction]
                                                   ,[Gross]
                                                   ,[TotalDeductions]
                                                   ,[NetPay]
                                                   ,[SSSBaseAmount]
                                                   ,[PhilHealthBaseAmount]
                                                   ,[PagIbigBaseAmount]
                                                   ,[TaxBaseAmount]
                                                   ,[COLAAmount]
                                                   ,[NonTaxPagIbig]
                                                   ,[TotalDays]
                                                   ,[TotalHrs]
                                                   ,[SSSLoan]
                                                   ,[PagibigLoan]
                                                   ,[OtherLoan]
                                                   ,[OTHrs]
                                                   ,[SPLHrs]
                                                   ,[LEGHrs]
                                                   ,[SUNHrs]
                                                   ,[SPLPay]
                                                   ,[LEGPay]
                                                   ,[SUNPay]
                                                   ,[CalamityLoan]
                                                   ,[Branch]
                                                   ,[PayrollType]
                                                   ,[BankAccountNo]
                                                   ,[PayrollMode])
                                             VALUES
                                                   ('" + _PayrollPeriod + @"'
                                                   ,'" + _Employee + @"'
                                                   ,'" + _MonthlyRate + @"'
                                                   ,'" + _DailyRate + @"'
                                                   ,'" + _Department + @"'
                                                   ,'" + _Department + @"'
                                                   ,'" + _BasicPay + @"'
                                                   ,'" + _OTPay + @"'
                                                   ,'" + _OtherIncome + @"'
                                                   ,'" + _Month13th + @"'
                                                   ,'" + _SSSEmployee + @"'
                                                   ,'" + _SSSEmployer + @"'
                                                   ,'" + _SSSEC + @"'
                                                   ,'" + _PhilHealthEmployee + @"'
                                                   ,'" + _PhilHealthEmployer + @"'
                                                   ,'" + _PagIbigEmployee + @"'
                                                   ,'" + _PagIbigEmployer + @"'
                                                   ,'" + _WitholdingTax + @"'
                                                   ,'" + _OtherDeduction + @"'
                                                   ,'" + _GrossPay + @"'
                                                   ,'" + _TotalDeductions + @"'
                                                   ,'" + _NetPay + @"'
                                                   ,'" + _SSSBaseAmount + @"'
                                                   ,'" + _PhilHealthBaseAmount + @"'
                                                   ,'" + _PagIbigBaseAmount + @"'
                                                   ,'" + _TaxBaseAmount + @"'
                                                   ,'" + _COLAAmount + @"'
                                                   ,'" + _PagIbigEmployer + @"'
                                                   ,'" + _TotalDays + @"'
                                                   ,'" + _TotalHrs + @"'
                                                   ,'" + _SSSLoan + @"'
                                                   ,'" + _PagibigLoan + @"'
                                                   ,'" + _OtherLoan + @"'
                                                   ,'" + _OTHrs + @"'
                                                   ,'" + _SPLHrs + @"'
                                                   ,'" + _LEGHrs + @"'
                                                   ,'" + _SUNHrs + @"'
                                                   ,'" + _SPLPay + @"'
                                                   ,'" + _LEGPay + @"'
                                                   ,'" + _SUNPay + @"'
                                                   ,'" + _CalamityLoan + @"'
                                                   ,'" + _BCode + @"'
                                                   ,'LASTPAY'
                                                   ,'" + _BankAccountNo + @"'
                                                   ,'" + _PayrollMode + @"'
                                                    )


                                                  ";

            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLExecute);





            _SQLExecute = @"
                 DELETE FROM [LoanCashPayment] WHERE [EmployeeNo] = '" + _Employee + @"' AND [PayrollPeriod] = '" + _PayrollPeriod + @"'
                                        ";

            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLExecute);




            string _sqlRebate = @"

                                        DECLARE @Employee NVARCHAR(30)
                                        DECLARE @PayrollPeriod NVARCHAR(30)

                                        SET @Employee = '" + _Employee + @"'
                                        SET @PayrollPeriod = '" + _PayrollPeriod + @"'

                                SELECT Z.Company,Z.EmployeeNo, Z.AccountCode, Z.LoanRefNo, CONCAT(@PayrollPeriod,' | ',Z.EmployeeNo)  AS ORNo
                                , (SELECT X.DateTwo FROM vwsPayrollPeriod X WHERE X.PayrollPeriod = @PayrollPeriod) AS PaymentDate
                                , Z.Rebate AS Amount
                                , (SELECT CONCAT(X.[PayrollPeriod], ' (', CONVERT(nvarchar(30), X.DateOne, 101),' - '
                                , CONVERT(nvarchar(30), X.DateTwo, 101), ') ') FROM vwsPayrollPeriod X WHERE X.PayrollPeriod = @PayrollPeriod) AS Remarks
                                ,'REBATE' AS TYPE

                                FROM (
                                SELECT * FROM vwsLoanFile A WHERE ISNULL(A.Rebate,0) <> 0 AND A.RebateApplication = 0
                                UNION ALL
                                SELECT * FROM vwsLoanFile A WHERE ISNULL(A.Rebate,0) <> 0 
                                AND A.RebateApplication = (CASE WHEN RIGHT(@PayrollPeriod,1) = 'A' THEN '1'
								                                WHEN RIGHT(@PayrollPeriod,1) = 'B' THEN '2' END)) Z


                                WHERE  Z.StartOfDeduction <= @PayrollPeriod AND Z.EmployeeNo = @Employee;


                                            ";
            DataTable _tblRebate = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlRebate);



            foreach (DataRow rowCashRebate in _tblRebate.Rows)
            {

                string _AccountCode = rowCashRebate[2].ToString();
                string _LoanRefNo = rowCashRebate[3].ToString();
                string _ORNo = rowCashRebate[4].ToString();
                string _PaymentDate = rowCashRebate[5].ToString();
                string _Amount = rowCashRebate[6].ToString();
                string _Remarks = rowCashRebate[7].ToString();
                string _Type = rowCashRebate[8].ToString();


                string _SQLRebate = @"
                 DELETE FROM [LoanCashPayment] WHERE [EmployeeNo] = '" + _Employee + @"' AND [PayrollPeriod] = '" + _PayrollPeriod + @"'
                AND [AccountCode] = '" + _AccountCode + @"' AND [LoanRefNo] = '" + _LoanRefNo + @"'

                INSERT INTO [dbo].[LoanCashPayment]
                           ([EmployeeNo]
                           ,[AccountCode]
                           ,[LoanRefNo]
                           ,[ORNo]
                           ,[PaymentDate]
                           ,[Amount]
                           ,[Remarks]
                           ,[Type]
                           ,[PayrollPeriod])
                     VALUES
                           ('" + _Employee + @"'
                           ,'" + _AccountCode + @"'
                           ,'" + _LoanRefNo + @"'
                           ,'" + _ORNo + @"'
                           ,'" + _PaymentDate + @"'
                           ,'" + _Amount + @"'
                           ,'" + _Remarks + @"'
                           ,'" + _Type + @"'
                           ,'" + _PayrollPeriod + @"')

                                                ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLRebate);

            }









            // Excel Progress Monitoring
            Application.DoEvents();
            _Count++;

            //frmMainWindow frmMainWindow = new frmMainWindow();
            tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }







        MessageBox.Show("Data Uploaded.");
    }
    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
    private void btnReProcess_Click(object sender, EventArgs e)
    {

        double _RowCount;
        int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            string _Company = row[0].ToString();
            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

            string _Employee = row[1].ToString();
            string _EmployeeName = row[2].ToString();

            string _sqlList;
            _sqlList = @"SELECT A.NoOfHrs, A.NoOfMins, A.AccountCode  FROM PayrollTrans01 A WHERE A.EmployeeNo = '" + _Employee + @"' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'";
            DataTable _tblData = clsSQLClientFunctions.DataList(_ConCompany, _sqlList);
            foreach (DataRow rowPT in _tblData.Rows)
            {
                double _NoOfHrs = double.Parse(rowPT[0].ToString());
                double _NoOfMins = double.Parse(rowPT[1].ToString());
                string _AccountCode = rowPT[2].ToString();


                double _TotalHours = Math.Round((Math.Round(_NoOfHrs, 0, MidpointRounding.AwayFromZero) + (Math.Round(_NoOfMins, 0, MidpointRounding.AwayFromZero) / 60)), 5, MidpointRounding.AwayFromZero);
                double _TotalDays = Math.Round((_TotalHours / 8), 5, MidpointRounding.AwayFromZero);

                string _SQLExecute = "";
                _SQLExecute = @"UPDATE A SET A.TotalDays = '" + _TotalDays + @"', A.TotalHrs = '" + _TotalHours + @"' FROM PayrollTrans01 A WHERE A.AccountCode = '" + _AccountCode + "' AND A.EmployeeNo = '" + _Employee + @"' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'";
                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLExecute);
            }



            // Excel Progress Monitoring
            Application.DoEvents();
            _Count++;

            tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }

        button1_Click(sender, e);
    }

    private void openInManualPayrollToolStripMenuItem_Click(object sender, EventArgs e)
    {

        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(frmManualPayroll))
            {
                form.Activate();
                return;
            }
        }

        frmManualPayroll frmManualPayroll = new frmManualPayroll();
        frmManualPayroll.MdiParent = ((frmMainWindow)(this.MdiParent));

        frmManualPayroll._gCompany = _gCompany;
        frmManualPayroll._gEmpCode = _gEmpCode;
        frmManualPayroll._gEmpName = _gEmpName;
        frmManualPayroll._gPayrollPeriod = cboPayrolPeriod.Text;
        frmManualPayroll.Show();

    }

    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            _gEmpCode = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            _gEmpName = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
        }
        catch
        {

        }
    }

    private void dgvDisplay_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvDisplay.ClearSelection();
                dgvDisplay.Rows[e.RowIndex].Selected = true;

                _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                _gEmpCode = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                _gEmpName = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            }
        }
        catch
        {

        }
    }

    private void updatePayrollTimeRecordToolStripMenuItem_Click(object sender, EventArgs e)
    {

        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(frmPayrollTransaction))
            {
                form.Activate();
                return;
            }
        }

        frmPayrollTransaction frmPayrollTransaction = new frmPayrollTransaction();
        frmPayrollTransaction.MdiParent = ((frmMainWindow)(this.MdiParent));

        frmPayrollTransaction._gCompany = _gCompany;
        frmPayrollTransaction._gEmpCode = _gEmpCode;
        frmPayrollTransaction._gEmpName = _gEmpName;
        frmPayrollTransaction._gPayrollPeriod = cboPayrolPeriod.Text;
        frmPayrollTransaction.Show();
    }




}