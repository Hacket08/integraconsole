using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
public partial class frmProcessPayroll : Form
{
    private static DataTable _DataList = new DataTable();

    public static string _gCompany;
    public static string _gEmpCode;
    public static string _gEmpName;
    public static string _gPayrollPeriod;

    public frmProcessPayroll()
    {
        InitializeComponent();
    }

    private void frmProcessPayroll_Load(object sender, EventArgs e)
    {

        btnReProcess.Visible = false;
        if (clsDeclaration.sSuperUser == "1")
        {
            btnReProcess.Visible = true;
        }
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

        

        //_SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        //cboPayrolPeriod.Items.Clear();
        //cboPayrolPeriod.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboPayrolPeriod.Items.Add(row[0].ToString());
        //}

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
        WHERE B.BCode LIKE '%" + _BCode + @"%' AND B.Area LIKE '%" + cboArea.Text + @"%' AND A.EmpStatus NOT IN (3,4,6)
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
            string _AttendanceExempt = row["AttendanceExempt"].ToString().Trim();


            double _DailyRate = double.Parse(row[3].ToString());



            if (_AttendanceExempt == "1")
            {

                string _SQLInsert;
                _SQLInsert = @"
                                    DELETE FROM [PayrollEntry] WHERE [EmployeeNo] = '" + _Employee + @"' AND [PayrollPeriod] = '" + cboPayrolPeriod.Text + @"'
                                    DELETE FROM [PayrollTrans01] WHERE [EmployeeNo] = '" + _Employee + @"' AND [PayrollPeriod] = '" + cboPayrolPeriod.Text + @"'


									INSERT INTO [PayrollEntry]
												([PayrollPeriod]
                                                  ,[EmployeeNo])
												VALUES
												(
												'" + cboPayrolPeriod.Text + @"',
												'" + _Employee + @"'
												)

                                     ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLInsert);

                string _valueRead = "";
                string _Account = "";
                string cellIndex;
                string timeFormat;

                timeFormat = ConfigurationManager.AppSettings["timeRegularDay"];
                cellIndex = ConfigurationManager.AppSettings["excelRegularDay"];
                _Account = ConfigurationManager.AppSettings["RegularDay"];

                if (cellIndex != "")
                {
                    string _sqlPayrollPeriod = "SELECT [WorkDays] FROM dbo.[vwsPayrollPeriod] A WHERE A.[PayrollPeriod] = '" + cboPayrolPeriod.Text+ "'";
                    _valueRead = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlPayrollPeriod, "WorkDays");


                    if (_valueRead == "") { _valueRead = "0"; }

                    if (double.Parse(_valueRead) != 0)
                    {
                        clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, _ConCompany);
                    }
                }




            }













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
            string _TaxSchedule06 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "TaxSchedule06");




            string _Rate = row[3].ToString();
            string _Monthly = row[5].ToString();
            string _TaxStatus = row[6].ToString();



            //_sqlList = "SELECT Z.Employer, Z.Employee, Z.ECC FROM SSSTable Z WHERE '" + _Monthly + "' BETWEEN Z.BracketFrom AND Z.BracketTo";
            //double _SSSEmployee = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employee");
            //double _SSSEmployer = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employer");
            //double _SSSECC = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "ECC");

            double _SSSEmployee = 0;
            double _SSSEmployer = 0;
            double _SSSECC = 0;

            clsGovermentComputation.SSSComputation(clsDeclaration.sSystemConnection,double.Parse(_Monthly),out _SSSEmployee,out _SSSEmployer,out _SSSECC);
            row[20] = 0.00;
            row[21] = 0.00;
            row[22] = 0.00;

            if (_SSSSchedule01 == "1")
            {
                row[20] = Math.Round(_SSSEmployee / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[21] = Math.Round(_SSSEmployer / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[22] = Math.Round(_SSSECC / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }
            if (_SSSSchedule02 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "A")
            {
                row[20] = Math.Round(_SSSEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[21] = Math.Round(_SSSEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[22] = Math.Round(_SSSECC, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }
            if (_SSSSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            {
                row[20] = Math.Round(_SSSEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[21] = Math.Round(_SSSEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[22] = Math.Round(_SSSECC, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }






            //_sqlList = @"SELECT * FROM 
            //                (SELECT *,
            //                ISNULL(LEAD(p.IncomeBracket) OVER(ORDER BY p.IncomeBracket), 99999) NextValue
            //                FROM PAGIBIGTable p)
            //                Z WHERE '" + _Monthly + "' BETWEEN Z.IncomeBracket AND Z.NextValue";


            //double _MaxCont = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "MaxContribution");
            //double _PAGIBIGEmployee = Math.Round(double.Parse(_Monthly) * (clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employee") / 100), 0, MidpointRounding.AwayFromZero);
            //double _PAGIBIGEmployer = Math.Round(double.Parse(_Monthly) * (clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employer") / 100), 0, MidpointRounding.AwayFromZero);

            //if (_PAGIBIGEmployee > _MaxCont)
            //{
            //    _PAGIBIGEmployee = _MaxCont;
            //    _PAGIBIGEmployer = _MaxCont;
            //}

            double _PAGIBIGEmployee = 0;
            double _PAGIBIGEmployer = 0;
            clsGovermentComputation.PAGIBIGComputation(clsDeclaration.sSystemConnection, double.Parse(_Monthly), out _PAGIBIGEmployee, out _PAGIBIGEmployer);


            row[23] = 0.00;
            row[24] = 0.00;


            if (_PagIbigSchedule01 == "1")
            {
                row[23] = Math.Round(_PAGIBIGEmployee / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[24] = Math.Round(_PAGIBIGEmployer / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }
            if (_PagIbigSchedule02 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "A")
            {
                row[23] = Math.Round(_PAGIBIGEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[24] = Math.Round(_PAGIBIGEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }
            if (_PagIbigSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            {
                row[23] = Math.Round(_PAGIBIGEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[24] = Math.Round(_PAGIBIGEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }


            ////_sqlList = @"SELECT Z.Employer, Z.Employee FROM PhilHealthTable Z 
            ////            WHERE '" + _Monthly + @"' BETWEEN Z.BracketFrom AND Z.BracketTo";
            //_sqlList = @"SELECT Z.Employee, Z.Employer, Z.Rate FROM PhilHealthTable Z WHERE '" + _DailyRate + @"' BETWEEN Z.BracketFrom AND Z.BracketTo";
            //double _PhilHealthEmployee = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employee");
            //double _PhilHealthEmployer = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Employer");
            //double _PhilHealthRate = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Rate");

            //if (_PhilHealthRate != 1)
            //{
            //    double _PhilHealthBase = (_DailyRate * 26 * (_PhilHealthRate / 100));

            //    _PhilHealthEmployer = (Math.Round(_PhilHealthBase, 2, MidpointRounding.AwayFromZero) / 2);
            //    _PhilHealthEmployee = (Math.Round(_PhilHealthBase, 2, MidpointRounding.AwayFromZero) - Math.Round(_PhilHealthEmployer, 2, MidpointRounding.AwayFromZero));
            //}

            double _PhilHealthEmployee = 0;
            double _PhilHealthEmployer = 0;
            clsGovermentComputation.PHILHEALTHComputation(clsDeclaration.sSystemConnection, 26, _DailyRate, out _PhilHealthEmployee, out _PhilHealthEmployer);


            row[25] = 0.00;
            row[26] = 0.00;

            if (_PHealthSchedule01 == "1")
            {
                row[25] = Math.Round(_PhilHealthEmployee / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[26] = Math.Round(_PhilHealthEmployer / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }
            if (_PHealthSchedule02 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "A")
            {
                row[25] = Math.Round(_PhilHealthEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[26] = Math.Round(_PhilHealthEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }
            if (_PHealthSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            {
                row[25] = Math.Round(_PhilHealthEmployee, 2, MidpointRounding.AwayFromZero).ToString("0.00");
                row[26] = Math.Round(_PhilHealthEmployer, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }




            //_sqlList = "SELECT * FROM TaxStatus Z WHERE Z.StatusCode = '" + _TaxStatus + "'";

            //double _PersonalExempt = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "PersonalExempt");
            //double _Dependents = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Dependents");



            //double _TotalConribution;
            //double _TotalExemption;

            //_TotalConribution = (_SSSEmployee * 12) + (_PAGIBIGEmployee * 12) + (_PhilHealthEmployee * 12);
            //_TotalExemption = _PersonalExempt + _Dependents;



            ////_GOVSyntax = @"SELECT ISNULL(ROUND(SUM(Z.Amount),2),0) AS RegularPay
            ////                    FROM PayrollDetails Z WHERE Z.AccountCode in 
            ////                    (select AccountCode from AccountCode X WHERE X.AccountType IN (0)) 
            ////                    AND Z.EmployeeNo = '" + _Employee + "' AND SUBSTRING(Z.PayrollPeriod,1,7) = '" + cboPayrolPeriod.Text.Substring(0, 7) + "'";
            ////_GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

            ////double _BasicPay;
            ////_BasicPay = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "RegularPay", "1"));



            //_sqlList = @"SELECT ISNULL(SUM(Z.WitholdingTax),0) AS WitholdingTax FROM PayrollHeader Z 
            //                    WHERE CAST(SUBSTRING(Z.PayrollPeriod,6,2) AS INT) < '" + double.Parse(cboPayrolPeriod.Text.Substring(5, 2)) + @"' 
            //                    AND EmployeeNo = '" + _Employee + "' AND SUBSTRING(Z.PayrollPeriod,1,4) = '" + _PayrollYear + "'";

            //double _WitholdingTax = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "WitholdingTax");
            //double _GrossCompensation = _WitholdingTax + _BasicPay + (double.Parse(_Monthly) * _PayrollMonth);



            //double _DeductionAmount;
            //double _TaxableIncome;

            //_DeductionAmount = _TotalExemption + _TotalConribution;
            //_TaxableIncome = _GrossCompensation - _DeductionAmount;




            //_sqlList = @"SELECT * FROM 
            //                (SELECT *,
            //                  (ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
            //                  FROM AnnualTaxTable P) Z
            //                WHERE '" + _TaxableIncome + "' BETWEEN Z.Excess AND Z.NextValue";

            //double _WtaxExcess = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Excess");
            //double _WtaxBase = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Base");
            //double _WtaxRate = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "Rate");


            //double _BasedComputation;
            //double _WTaxAdditional;
            //double _WtaxAmount;

            //_BasedComputation = _TaxableIncome - _WtaxExcess;
            //_WTaxAdditional = (_BasedComputation * (_WtaxRate / 100));
            //_WtaxAmount = ((_WtaxBase + _WTaxAdditional) / 12);

            double _WtaxAmount = 0;
            clsGovermentComputation.WTAXComputationAnnual(clsDeclaration.sSystemConnection, _Employee, out _WtaxAmount);


            row[27] = 0.00;
            if (_TaxSchedule01 == "1")
            {
                row[27] = Math.Round(_WtaxAmount / 2, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }

            if (_TaxSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            {
                row[27] = Math.Round(_WtaxAmount, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            }



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



            double _sumSUNPay = Math.Round(_SUNPay, 2, MidpointRounding.AwayFromZero);
            double _sumOtherIncome = Math.Round(_OtherIncome, 2, MidpointRounding.AwayFromZero);
            double _sumLegalHolPay = Math.Round(_LegalHolPay, 2, MidpointRounding.AwayFromZero);
            double _sumSunSplHolPay = Math.Round(_SunSplHolPay, 2, MidpointRounding.AwayFromZero);
            double _sumOTPay = Math.Round(_OTPay, 2, MidpointRounding.AwayFromZero);
            double _sumBasicPay = Math.Round(_BasicPay, 2, MidpointRounding.AwayFromZero);


            double _GrossPay = (_sumBasicPay + _sumOTPay + _sumSunSplHolPay + _sumLegalHolPay + _sumOtherIncome + _sumSUNPay);
            row[16] = Math.Round(_GrossPay, 2, MidpointRounding.AwayFromZero);




            double _sumSSSLoan = Math.Round(_SSSLoan, 2, MidpointRounding.AwayFromZero);
            double _sumPAGIBIGLoan = Math.Round(_PAGIBIGLoan, 2, MidpointRounding.AwayFromZero);
            double _sumCalamityLoan = Math.Round(_CalamityLoan, 2, MidpointRounding.AwayFromZero);
            double _sumOtherLoan = Math.Round(_OtherLoan, 2, MidpointRounding.AwayFromZero);
            double _sumOtherDeduction = Math.Round(_OtherDeduction, 2, MidpointRounding.AwayFromZero);

            double _sumSSSEmployee = Math.Round(_SSSEmployee, 2, MidpointRounding.AwayFromZero);
            double _sumPAGIBIGEmployee = Math.Round(_PAGIBIGEmployee, 2, MidpointRounding.AwayFromZero);
            double _sumPhilHealthEmployee = Math.Round(_PhilHealthEmployee, 2, MidpointRounding.AwayFromZero);
            double _sumWtaxAmount = Math.Round(_WtaxAmount, 2, MidpointRounding.AwayFromZero);


            _TotalDeduction = _sumSSSLoan + _sumPAGIBIGLoan + _sumCalamityLoan + _sumOtherLoan + _sumOtherDeduction + _sumSSSEmployee + _sumPAGIBIGEmployee + _sumPhilHealthEmployee + _sumWtaxAmount;


            _NetPay = Math.Round(_GrossPay, 2, MidpointRounding.AwayFromZero) - Math.Round(_TotalDeduction, 2, MidpointRounding.AwayFromZero);

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
                                                    ,'PAYROLL'
                                                   ,'" + _BankAccountNo + @"'
                                                   ,'" + _PayrollMode + @"')


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

        DialogResult res = MessageBox.Show("Payroll Processing will overwrite all saved data on this payroll period. Do you want ot continue?", "Payroll Processing", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        if (res == DialogResult.Cancel)
        {
            return;
        }


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
        #endregion

        #region Employee Data List Query Setup
        string _sqlGenerate = "";
        _sqlGenerate = @"SELECT * FROM (
                                            SELECT A.Company
		                                                , A.EmployeeNo
		                                                , A.EmployeeName
		                                                , ISNULL(A.DailyRate,0) AS DailyRate
		                                                , A.RateDivisor
		                                                , ISNULL(A.MonthlyRate,0) AS MonthlyRate
		                                                , A.TaxStatus 
		                                                , A.ConfiLevel
		                                                , A.AttendanceExempt
		                                                , B.Area
		                                                , B.BCode
		                                                , B.DepCode
		                                                , A.DateHired
		                                                , A.DateFinish
		                                                , A.Department
		                                                , A.BankAccountNo
		                                                , A.PayrollMode
		                                                , C.PayrollPeriod
		                                                , C.DateOne
		                                                , C.DateTwo
                                                        , ISNULL(A.COLAAmount,0) AS COLAAmount
                                                        , ISNULL(A.DailyAllowance,0) AS DailyAllowance
                                                        , CASE WHEN 
	                                                        (CASE WHEN ISNULL(A.DateFinish,'') = '' THEN C.DateOne ELSE ISNULL(A.DateFinish,'') END) BETWEEN C.DateOne AND C.DateTwo
	                                                        AND ISNULL(A.DateFinish,'') <> ''
	                                                        THEN 'LASTPAY' ELSE 'PAYROLL' END AS [PayrollType]
                                            FROM vwsEmployees A 
                                                        INNER JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
                                                        , (SELECT Z.* FROM vwsPayrollPeriod Z WHERE Z.PayrollPeriod = '" + cboPayrolPeriod.Text + @"') C
                                            WHERE B.Area LIKE '%" + cboArea.Text + @"%' 
										                AND B.BCode LIKE '%" + _BCode + @"%'
										                AND A.EmployeeNo  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtEmpNo.Text + @"%' 
										                AND A.EmployeeName  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtName.Text + @"%' 
                                                        AND B.DepCode LIKE '%" + _BPosition + @"%'
										                AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
                                                        AND CASE WHEN ISNULL(A.DateFinish,'') = '' THEN C.DateOne ELSE ISNULL(A.DateFinish,'') END >= C.DateOne
AND A.EmployeeNo NOT IN (SELECT Z.EmployeeNo FROM dbo.[vwsPayrollHeader] Z WHERE  ISNULL(Z.Validated,'N')  = 'Y' AND Z.payrollperiod = '" + cboPayrolPeriod.Text + @"')
                                    ) XX 
                                        WHERE XX.ConfiLevel IN (" + _GroupConfi + @")
                                        ORDER BY XX.[EmployeeName]
                                    ";

        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlGenerate);
        #endregion

        double _RowCount;
        int _Count = 0;
        _RowCount = _DataTable.Rows.Count;
        foreach (DataRow row in _DataTable.Rows)
        {
            #region Varialble Declaration For the Employee Data

            string _Company = row["Company"].ToString();
            string _PayrollPeriod = row["PayrollPeriod"].ToString();
            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _EmployeeName = row["EmployeeName"].ToString();

            string _PayrollPeriodDateTwo = row["DateTwo"].ToString();
            string _PayrollMode = row["PayrollMode"].ToString();
            string _BankAccountNo = row["BankAccountNo"].ToString();
            string _PayrollType = row["PayrollType"].ToString();
            string _AttendanceExempt = row["AttendanceExempt"].ToString();
            double _RateDivisor = double.Parse(row["RateDivisor"].ToString());

            // Employee Salary Rates


            double _DailyAllowance = double.Parse(row["DailyAllowance"].ToString());

            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
            string _sqlDept = @"SELECT A.DailyRate, A.MonthlyRate, A.COLAAmount,A.DepartmentCode, B.BCode FROM vwsPayrollHeader A  INNER JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
                                          WHERE A.PayrollPeriod = '" + _PayrollPeriod  + @"' AND A.EmployeeNo = '" + _EmployeeNo + @"'";
            DataTable _tblDept = new DataTable();
            _tblDept = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlDept);

            double _MonthlyRate = double.Parse(clsSQLClientFunctions.GetData(_tblDept, "MonthlyRate", "1"));
            double _COLAAmount = double.Parse(clsSQLClientFunctions.GetData(_tblDept, "COLAAmount", "1"));
            double _DailyRate = double.Parse(clsSQLClientFunctions.GetData(_tblDept, "DailyRate", "1"));

            string _Branch = clsSQLClientFunctions.GetData(_tblDept, "BCode", "0");
            string _Department = clsSQLClientFunctions.GetData(_tblDept, "DepartmentCode", "0");

            //double _MonthlyRate = double.Parse(row["MonthlyRate"].ToString());
            //double _COLAAmount = double.Parse(row["COLAAmount"].ToString());
            //double _DailyRate = double.Parse(row["DailyRate"].ToString());

            //string _Branch = row["BCode"].ToString();
            //string _Department = row["DepartmentCode"].ToString();

            #endregion

            //Start of Uploading PayrollDetails
            #region Deleting of  PayrollHeader Table
            string _sqlDeletePayrollTrans = "";
            _sqlDeletePayrollTrans = @"
                                                            DECLARE @Employee NVARCHAR(30)
                                                            DECLARE @PayPeriod NVARCHAR(30)

                                                            SET @Employee = '" + _EmployeeNo + @"'
                                                            SET @PayPeriod = '" + _PayrollPeriod + @"'

                                                            DELETE FROM [PayrollHeader] WHERE [EmployeeNo] = @Employee AND [PayrollPeriod] = @PayPeriod
                                                            ";

            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlDeletePayrollTrans);
            #endregion

            #region Uploading PayrollHeader
            //Start of Uploading PayrollHeader
            clsFunctions.InsertPayrollHeader(_Company
                                        , _PayrollPeriod
                                        , _EmployeeNo
                                        , _MonthlyRate
                                        , _DailyRate
                                        , _Department
                                        , _Branch
                                        , _PayrollType
                                        , _BankAccountNo
                                        , _PayrollMode);
            #endregion


            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Payroll Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }

        #region Display Data
        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"
                                                SELECT 
                                                   B.[Company]
                                                   ,A.[EmployeeNo]
                                                   ,B.[EmployeeName]
                                                   ,A.[MonthlyRate]
                                                   ,A.[DailyRate]
                                                   ,B.[TaxStatus]
                                                   ,A.[TotalDays]
                                                   ,A.[BasicPay]
                                                   ,A.[OTPay]
                                                   ,A.[OTHrs]
                                                   ,A.[SUNPay]
                                                   ,A.[SUNHrs]
                                                   ,A.[SPLPay]
                                                   ,A.[SPLHrs]
                                                   ,A.[LEGPay]
                                                   ,A.[LEGHrs]

                                                   ,A.[COLAAmount]
                                                   ,A.[OtherIncome]
                                                   ,A.[Gross]

                                                   ,A.[SSSEmployee]
                                                   ,A.[SSSEmployer]
                                                   ,A.[SSSEC]
                                                   ,A.[PhilHealthEmployee]
                                                   ,A.[PhilHealthEmployer]
                                                   ,A.[PagIbigEmployee]
                                                   ,A.[PagIbigEmployer]
                                                   ,A.[NonTaxPagIbig]
                                                   ,A.[WitholdingTax]


                                                   ,A.[SSSLoan]
                                                   ,A.[PagibigLoan]
                                                   ,A.[CalamityLoan]
                                                   ,A.[OtherLoan]

                                                   ,A.[OtherDeduction]
                                                   ,A.[TotalDeductions]

                                                   ,A.[NetPay]

                                                   ,A.[Branch]
                                                   ,A.[PayrollType]
                                                   ,A.[BankAccountNo]
                                                   ,A.[PayrollMode]
                                                   ,A.[PayrollPeriod]
                                                FROM vwsPayrollHeader A 
	                                                INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
	                                                INNER JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode
                                                WHERE C.Area LIKE '%" + cboArea.Text + @"%' 
	                                                AND C.BCode LIKE '%" + _BCode + @"%'
	                                                AND B.EmployeeNo  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtEmpNo.Text + @"%' 
	                                                AND B.EmployeeName  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtName.Text + @"%' 
                                                    AND C.DepCode LIKE '%" + _BPosition + @"%'
	                                                AND B.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
                                                    AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'
AND ISNULL(A.Validated,'N') = 'N'
	                                                --AND A.PayrollType = 'PAYROLL'
                                            ";

        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);
        #endregion

        MessageBox.Show("Payroll Processing Complete");
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

    private void cboPayrolPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
    
        DialogResult res = MessageBox.Show("Payroll Processing will overwrite all saved data on this payroll period. Do you want ot continue?", "Payroll Processing", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        if (res == DialogResult.Cancel)
        {
            return;
        }


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
        #endregion

        #region Employee Data List Query Setup
        string _sqlGenerate = "";
        _sqlGenerate = @"SELECT * FROM (
                                            SELECT A.Company
		                                                , A.EmployeeNo
		                                                ,REPLACE(REPLACE(A.EmployeeName, CHAR(13), ' '), CHAR(10), ' ') AS EmployeeName
		                                                , ISNULL(A.DailyRate,0) AS DailyRate
		                                                , A.RateDivisor
		                                                , ISNULL(A.MonthlyRate,0) AS MonthlyRate
		                                                , A.TaxStatus 
		                                                , A.ConfiLevel
		                                                , A.AttendanceExempt
		                                                , B.Area
		                                                , B.BCode
		                                                , B.DepCode
		                                                , A.DateHired
		                                                , A.DateFinish
		                                                , A.Department
		                                                , A.BankAccountNo
		                                                , A.PayrollMode
		                                                , C.PayrollPeriod
		                                                , C.DateOne
		                                                , C.DateTwo
                                                        , ISNULL(A.COLAAmount,0) AS COLAAmount
                                                        , ISNULL(A.DailyAllowance,0) AS DailyAllowance
                                                        , CASE WHEN 
	                                                        (CASE WHEN ISNULL(A.DateFinish,'') = '' THEN C.DateOne ELSE ISNULL(A.DateFinish,'') END) BETWEEN C.DateOne AND C.DateTwo
	                                                        AND ISNULL(A.DateFinish,'') <> ''
	                                                        THEN 'LASTPAY' ELSE 'PAYROLL' END AS [PayrollType]
                                            FROM vwsEmployees A 
                                                        INNER JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
                                                        , (SELECT Z.* FROM vwsPayrollPeriod Z WHERE Z.PayrollPeriod = '" + cboPayrolPeriod.Text + @"') C
                                            WHERE B.Area LIKE '%" + cboArea.Text + @"%' 
										                AND B.BCode LIKE '%" + _BCode + @"%'
										                AND A.EmployeeNo  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtEmpNo.Text + @"%' 
										                AND A.EmployeeName  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtName.Text + @"%' 
                                                        AND B.DepCode LIKE '%" + _BPosition + @"%'
										                AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
                                                        AND CASE WHEN ISNULL(A.DateFinish,'') = '' THEN C.DateOne ELSE ISNULL(A.DateFinish,'') END >= C.DateOne
AND A.EmployeeNo NOT IN (SELECT Z.EmployeeNo FROM dbo.[vwsPayrollHeader] Z WHERE  ISNULL(Z.Validated,'N')  = 'Y' AND Z.payrollperiod = '" + cboPayrolPeriod.Text + @"')
                                    ) XX 
                                        WHERE XX.ConfiLevel IN (" + _GroupConfi + @")
                                        ORDER BY XX.[EmployeeName]
                                    ";

        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlGenerate);
        #endregion

        double _RowCount;
        int _Count = 0;
        _RowCount = _DataTable.Rows.Count;
        foreach (DataRow row in _DataTable.Rows)
        {
            #region Varialble Declaration For the Employee Data
                string _sqlAccountCode = "";

                string _Company = row["Company"].ToString();
                string _PayrollPeriod = row["PayrollPeriod"].ToString();
                string _EmployeeNo = row["EmployeeNo"].ToString();
                string _EmployeeName = row["EmployeeName"].ToString();
                string _Branch = row["BCode"].ToString();
                string _Department = row["Department"].ToString();
                string _PayrollPeriodDateTwo = row["DateTwo"].ToString();
                string _PayrollMode = row["PayrollMode"].ToString();
                string _BankAccountNo = row["BankAccountNo"].ToString();
                string _PayrollType = row["PayrollType"].ToString();
                string _AttendanceExempt = row["AttendanceExempt"].ToString();
                double _RateDivisor = double.Parse(row["RateDivisor"].ToString());

            // Employee Salary Rates
                double _DailyRate = double.Parse(row["DailyRate"].ToString());
                double _MonthlyRate = double.Parse(row["MonthlyRate"].ToString());
                double _COLAAmount = double.Parse(row["COLAAmount"].ToString());
                double _DailyAllowance = double.Parse(row["DailyAllowance"].ToString());

                string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

                string _LoanRefenceNo = "";
                string _AccountCode = "";
                double _NoOfHours = 0.00;
                double _NoOfMins = 0.00;
                double _TotalHrs = 0.00;
                double _TotalDays = 0.00;

                double _PercentDaily = 0.00;
                double _Amount = 0.00;
            #endregion
            #region Attendance Exempt Process
            if (_AttendanceExempt.Trim().ToString() == "1")
            {

                string _SQLInsert;
                _SQLInsert = @"
                                    DELETE FROM [PayrollEntry] WHERE [EmployeeNo] = '" + _EmployeeNo + @"' AND [PayrollPeriod] = '" + _PayrollPeriod + @"'
                                    DELETE FROM [PayrollTrans01] WHERE [EmployeeNo] = '" + _EmployeeNo + @"' AND [PayrollPeriod] = '" + _PayrollPeriod + @"'


									INSERT INTO [PayrollEntry]
												([PayrollPeriod]
                                                  ,[EmployeeNo])
												VALUES
												(
												'" + _PayrollPeriod + @"',
												'" + _EmployeeNo + @"'
												)

                                     ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLInsert);

                string _valueRead = "";
                string _Account = "";
                string cellIndex;
                string timeFormat;

                timeFormat = clsFunctions.SystemSettingValue("13");
                cellIndex = clsFunctions.SystemSettingValue("12");
                _Account = clsFunctions.SystemSettingValue("11");
                //timeFormat = ConfigurationManager.AppSettings["timeRegularDay"];
                //cellIndex = ConfigurationManager.AppSettings["excelRegularDay"];
                //_Account = ConfigurationManager.AppSettings["RegularDay"];

                if (cellIndex != "")
                {
                    _valueRead = (_RateDivisor / 2).ToString("N2");


                    if (_valueRead == "") { _valueRead = "0"; }

                    if (double.Parse(_valueRead) != 0)
                    {
                        clsFunctions.InsertTimeRecord(_valueRead, _EmployeeNo, _Account, timeFormat, _PayrollPeriod, _ConCompany);
                    }
                }
            }
            #endregion
            #region Regular Hours / Days Calculation
            // Get Regular Hours / Days
            string _sqlRegularHours = "";
                _sqlRegularHours = @"
                                                        SELECT 
                                                          A.PayrollPeriod
                                                        , A.EmployeeNo
                                                        , ISNULL(A.TotalHrs,0) AS TotalHrs
                                                        , ISNULL(A.TotalDays,0) AS TotalDays
                                                        FROM PayrollTrans01 A 
                                                        WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                        AND A.AccountCode = (SELECT A.VariableValue FROM SysVariables A WHERE A.VariableName = 'RegularHrs')
                                                    ";

                double _RegularHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlRegularHours, "TotalHrs");
                double _RegularDays = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlRegularHours, "TotalDays");
            #endregion

            //Start of Uploading PayrollDetails
            #region Deleting of PayrollDetails And PayrollHeader Table
                string _sqlDeletePayrollTrans = "";
                _sqlDeletePayrollTrans = @"
                                                            DECLARE @Employee NVARCHAR(30)
                                                            DECLARE @PayPeriod NVARCHAR(30)

                                                            SET @Employee = '" + _EmployeeNo + @"'
                                                            SET @PayPeriod = '" + _PayrollPeriod + @"'

                                                            DELETE FROM [PayrollDetails] WHERE [EmployeeNo] = @Employee AND [PayrollPeriod] = @PayPeriod
                                                            DELETE FROM [PayrollHeader] WHERE [EmployeeNo] = @Employee AND [PayrollPeriod] = @PayPeriod
                                                            DELETE FROM [LoanCashPayment] WHERE [EmployeeNo] = @Employee AND [ORNo] = @PayPeriod AND  [Type] = 'REBATE'
                                                            ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlDeletePayrollTrans);
            #endregion
            #region Uploading PayrollDetails
                #region PayrollTrans01 Table Calculation
                // Get Data From PayrollTrans01 TABLE
                string _sqlPayrollTrans01 = "";
                _sqlPayrollTrans01 = @"SELECT 
                                                          A.PayrollPeriod
                                                        , A.EmployeeNo
                                                        , B.AccountCode
                                                        , B.AccountDesc
                                                        , ISNULL(A.NoOfHrs,0) AS NoOfHrs
                                                        , ISNULL(A.NoOfMins,0) AS NoOfMins
                                                        , ISNULL(A.TotalHrs,0) AS TotalHrs
                                                        , ISNULL(A.TotalDays,0) AS TotalDays
                                                        , B.PercentDaily
                                                        FROM PayrollTrans01 A 
                                                        INNER JOIN AccountCode B ON A.AccountCode = B.AccountCode
                                                        WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'";

                DataTable _tblPayrollTrans01 = new DataTable();
                _tblPayrollTrans01 = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollTrans01);


                foreach (DataRow rowPT01 in _tblPayrollTrans01.Rows)
                {
                    _LoanRefenceNo = "";
                    _AccountCode = rowPT01["AccountCode"].ToString();
                    _NoOfHours = double.Parse(rowPT01["NoOfHrs"].ToString());
                    _NoOfMins = double.Parse(rowPT01["NoOfMins"].ToString());
                    _TotalHrs = double.Parse(rowPT01["TotalHrs"].ToString());
                    _TotalDays = double.Parse(rowPT01["TotalDays"].ToString());
                    _PercentDaily = double.Parse(rowPT01["PercentDaily"].ToString());
                    //_Amount = Math.Round(((_DailyRate * _PercentDaily) * _TotalDays), 2, MidpointRounding.AwayFromZero);
                    _Amount =double.Parse(((_DailyRate * _PercentDaily) * _TotalDays).ToString("N2"));

                    // Insert Data for PayrollTrans01
                    if (_Amount != 0)
                    {

                    if (_AccountCode == "6-301" || _AccountCode == "6-302" || _AccountCode == "6-304")
                    {
                        _Amount = double.Parse("0.00");
                    }

                    clsFunctions.InsertDetails(_Company
                                       , _PayrollPeriod
                                       , _EmployeeNo
                                       , _AccountCode
                                       , _LoanRefenceNo
                                       , _NoOfHours
                                       , _Amount
                                       , _NoOfMins
                                       , _TotalHrs
                                       , _TotalDays
                                       , _Branch
                                       , _Department);
                    }

                }
            #endregion

                if (_PayrollType == "PAYROLL")
                {
                    #region PayrollTrans02 Table Calculation
                    // Get Data From PayrollTrans02 TABLE
                    string _sqlPayrollTrans02 = "";
                    _sqlPayrollTrans02 = @"SELECT 
                                                              A.PayrollPeriod
                                                            , A.EmployeeNo
                                                            , A.Amount
                                                            , B.AccountCode
                                                            , B.AccountDesc
                                                            , 0.00 AS NoOfHrs
                                                            , 0.00 AS NoOfMins
                                                            , 0.00 AS TotalHrs
                                                            , 0.00 AS TotalDays
                                                            , B.PercentDaily
                                                            FROM PayrollTrans02 A 
                                                            INNER JOIN AccountCode B ON A.AccountCode = B.AccountCode
                                                            WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'";

                    DataTable _tblPayrollTrans02 = new DataTable();
                    _tblPayrollTrans02 = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollTrans02);


                    foreach (DataRow rowPT02 in _tblPayrollTrans02.Rows)
                    {
                        _LoanRefenceNo = "";
                        _AccountCode = rowPT02["AccountCode"].ToString();
                        _NoOfHours = double.Parse(rowPT02["NoOfHrs"].ToString());
                        _NoOfMins = double.Parse(rowPT02["NoOfMins"].ToString());
                        _TotalHrs = double.Parse(rowPT02["TotalHrs"].ToString());
                        _TotalDays = double.Parse(rowPT02["TotalDays"].ToString());
                        _PercentDaily = double.Parse(rowPT02["PercentDaily"].ToString());

                        _Amount = double.Parse(rowPT02["Amount"].ToString());


                        // Insert Data for PayrollTrans02
                        if (_Amount != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , _PayrollPeriod
                                           , _EmployeeNo
                                           , _AccountCode
                                           , _LoanRefenceNo
                                           , _NoOfHours
                                           , _Amount
                                           , _NoOfMins
                                           , _TotalHrs
                                           , _TotalDays
                                           , _Branch
                                           , _Department);
                        }

                    }
                    #endregion
                    #region EmployeesRecurring Table Calculation
                    // Get Data From EmployeesRecurring TABLE
                    string _sqlEmployeesRecurring = "";
                    _sqlEmployeesRecurring = @"SELECT 
                                                                     '" + _PayrollPeriod + @"' AS PayrollPeriod
                                                                    , A.EmployeeNo
                                                                    , A.Amount
                                                                    , B.AccountCode
                                                                    , B.AccountDesc
                                                                    , 0.00 AS NoOfHrs
                                                                    , 0.00 AS NoOfMins
                                                                    , 0.00 AS TotalHrs
                                                                    , 0.00 AS TotalDays
                                                                    , B.PercentDaily
                                                                    , A.Freq
                                                                    FROM [EmployeesRecurring] A 
                                                                    INNER JOIN AccountCode B ON A.AccountCode = B.AccountCode
                                                                    WHERE A.Status = 0
                                                                    AND A.EmployeeNo = '" + _EmployeeNo + @"'";

                    DataTable _tblEmployeesRecurring = new DataTable();
                    _tblEmployeesRecurring = clsSQLClientFunctions.DataList(_ConCompany, _sqlEmployeesRecurring);


                    foreach (DataRow rowER in _tblEmployeesRecurring.Rows)
                    {
                        _LoanRefenceNo = "";
                        _AccountCode = rowER["AccountCode"].ToString();
                        _NoOfHours = double.Parse(rowER["NoOfHrs"].ToString());
                        _NoOfMins = double.Parse(rowER["NoOfMins"].ToString());
                        _TotalHrs = double.Parse(rowER["TotalHrs"].ToString());
                        _TotalDays = double.Parse(rowER["TotalDays"].ToString());
                        _PercentDaily = double.Parse(rowER["PercentDaily"].ToString());
                        int _Freq = int.Parse(rowER["Freq"].ToString());

                        _Amount = 0;
                        switch (_Freq)
                        {
                            case 0:
                                _Amount = double.Parse(rowER["Amount"].ToString());
                                break;
                            case 1:
                                if (_PayrollPeriod.Substring(8, 1) == "A")
                                {
                                    _Amount = double.Parse(rowER["Amount"].ToString());
                                }
                                break;
                            case 2:
                                if (_PayrollPeriod.Substring(8, 1) == "B")
                                {
                                    _Amount = double.Parse(rowER["Amount"].ToString());
                                }
                                break;
                        }

                        // Insert Data for EmployeesRecurring
                        if (_Amount != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , _PayrollPeriod
                                           , _EmployeeNo
                                           , _AccountCode
                                           , _LoanRefenceNo
                                           , _NoOfHours
                                           , _Amount
                                           , _NoOfMins
                                           , _TotalHrs
                                           , _TotalDays
                                           , _Branch
                                           , _Department);
                        }

                    }
                #endregion
                    #region LoanFile Table Calculation
                // Get Data From LoanFile TABLE

                string _sqlCompanyInfo;
                _sqlCompanyInfo = "SELECT A.* FROM OCMP A WHERE A.CompanyCode = '" + _Company + "'";
                string _DBServer = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlCompanyInfo, "DBServer");
                string _DBName = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlCompanyInfo, "DBName");


                string _sqlLoanFile = "";
                    _sqlLoanFile = @"SELECT 
                                                         '" + _PayrollPeriod + @"' AS PayrollPeriod
                                                        , A.EmployeeNo
                                                        , A.Amortization
                                                        , B.AccountCode
                                                        , B.AccountDesc
                                                        , 0.00 AS NoOfHrs
                                                        , 0.00 AS NoOfMins
                                                        , 0.00 AS TotalHrs
                                                        , 0.00 AS TotalDays
                                                        , B.PercentDaily
                                                        , A.StartOfDeduction
                                                        , A.TermsOfPayment
                                                        , A.LoanRefNo
                                                        , A.LoanAmount
                                                        , (SELECT Z.Balance FROM [" + ConfigurationManager.AppSettings["DBName"] + "].dbo.fnGetLoanBalance(A.EmployeeNo, B.AccountCode, '" + _PayrollPeriod + @"', A.LoanRefNo ) Z) AS Balance
                                                        , A.StartOfDeduction AS RebateStartOfDeduction
                                                        , ISNULL(A.Rebate,0.00) AS Rebate
                                                        , A.RebateApplication                                                
                                                        FROM [" + _DBServer + @"].[" + _DBName + @"].dbo.LoanFile A 
                                                        INNER JOIN [" + _DBServer + @"].[" + _DBName + @"].dbo.AccountCode B ON A.AccountCode = B.AccountCode
                                                        WHERE A.Status = 0
                                                        AND A.StartOfDeduction <= '" + _PayrollPeriod + @"'
                                                        AND A.EmployeeNo = '" + _EmployeeNo + @"'
                                                        AND (SELECT Z.Balance FROM [" + ConfigurationManager.AppSettings["DBName"] + "].dbo.fnGetLoanBalance(A.EmployeeNo, B.AccountCode, '" + _PayrollPeriod + @"', A.LoanRefNo ) Z) > 0";

                    DataTable _tblLoanFile = new DataTable();
                    _tblLoanFile = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlLoanFile);


                    foreach (DataRow rowLF in _tblLoanFile.Rows)
                    {
                        _LoanRefenceNo = rowLF["LoanRefNo"].ToString();
                        _AccountCode = rowLF["AccountCode"].ToString();
                        _NoOfHours = double.Parse(rowLF["NoOfHrs"].ToString());
                        _NoOfMins = double.Parse(rowLF["NoOfMins"].ToString());
                        _TotalHrs = double.Parse(rowLF["TotalHrs"].ToString());
                        _TotalDays = double.Parse(rowLF["TotalDays"].ToString());
                        _PercentDaily = double.Parse(rowLF["PercentDaily"].ToString());

                        double _Amortization = double.Parse(rowLF["Amortization"].ToString());
                        double _Balance = double.Parse(rowLF["Balance"].ToString());

                        int _TermsOfPayment = int.Parse(rowLF["TermsOfPayment"].ToString());
                        string _RebateApplication = rowLF["RebateApplication"].ToString();
                
                        double _Rebate = double.Parse(rowLF["Rebate"].ToString());
                        string _RebateStartOfDeduction = rowLF["RebateStartOfDeduction"].ToString();


                        double _RebateAmt = 0;
                        if (_Rebate != 0)
                        {
                            int result = _RebateStartOfDeduction.CompareTo(_PayrollPeriod);
                            if (result <= 0)
                            {
                                switch (_RebateApplication)
                                {
                                    case "0":
                                        _RebateAmt = _Rebate;
                                        break;
                                    case "1":
                                        if (_PayrollPeriod.Substring(8, 1) == "A")
                                        {
                                            _RebateAmt = _Rebate;
                                        }
                                        break;
                                    case "2":
                                        if (_PayrollPeriod.Substring(8, 1) == "B")
                                        {
                                            _RebateAmt = _Rebate;
                                        }
                                        break;
                                }
                            }
                        }

                

                        if (_Balance < _Amortization)
                        {
                            _Amount = 0;
                            switch (_TermsOfPayment)
                            {
                                case 0:
                                    _Amount = _Balance;
                                    break;
                                case 1:
                                    if (_PayrollPeriod.Substring(8, 1) == "A")
                                    {
                                        _Amount = _Balance;
                                    }
                                    break;
                                case 2:
                                    if (_PayrollPeriod.Substring(8, 1) == "B")
                                    {
                                        _Amount = _Balance;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            _Amount = 0;
                            switch (_TermsOfPayment)
                            {
                                case 0:
                                    _Amount = _Amortization;
                                    break;
                                case 1:
                                    if (_PayrollPeriod.Substring(8, 1) == "A")
                                    {
                                        _Amount = _Amortization;
                                    }
                                    break;
                                case 2:
                                    if (_PayrollPeriod.Substring(8, 1) == "B")
                                    {
                                        _Amount = _Amortization;
                                    }
                                    break;
                            }
                        }


                    // Insert Data for LoanFile
                    if ((_Amount - _RebateAmt) != 0)
                    {
                        clsFunctions.InsertDetails(_Company
                                       , _PayrollPeriod
                                       , _EmployeeNo
                                       , _AccountCode
                                       , _LoanRefenceNo
                                       , _NoOfHours
                                       , (_Amount - _RebateAmt)
                                       , _NoOfMins
                                       , _TotalHrs
                                       , _TotalDays
                                       , _Branch
                                       , _Department);
                    }


                        #region Rebate Payment Application
                        if (_RebateAmt != 0)
                        {
                            clsFunctions.InsertLoanPayment(_Company
                                           , _PayrollPeriod
                                           , _EmployeeNo
                                           , _AccountCode
                                           , _LoanRefenceNo
                                           , _RebateAmt
                                           , _PayrollPeriod
                                           , "REBATE PAYMENT FOR PAYROLL PERIOD " + _PayrollPeriod
                                           , "REBATE"
                                           , _PayrollPeriodDateTwo);
                        }
                        #endregion

                    }
                    #endregion
                    #region Goverment Deduction Computation
                    // Goverment Computations
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
                                    WHERE A.EmployeeNo = '" + _EmployeeNo + @"'
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
                    string _TaxSchedule06 = clsSQLClientFunctions.GetStringValue(_ConCompany, _SettingSyntax, "TaxSchedule06");


                    _Amount = 0;
                    double _SSSEmployee = 0;
                    double _SSSEmployer = 0;
                    double _SSSECC = 0;

                    double _PAGIBIGEmployee = 0;
                    double _PAGIBIGEmployer = 0;

                    double _PhilHealthEmployee = 0;
                    double _PhilHealthEmployer = 0;

                    double _WtaxAmount = 0;

                    switch (_PayrollPeriod.Substring(8, 1))
                    {
                        case "A":
                            #region SSS Computation
                            // SSS Computation
                            clsGovermentComputation.SSSComputation(clsDeclaration.sSystemConnection, _MonthlyRate, out _SSSEmployee, out _SSSEmployer, out _SSSECC);

                            if (_SSSSchedule01 == "1")
                            {
                                _AccountCode = "004";
                                _Amount = Math.Round(_SSSEmployee / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "005";
                                _Amount = Math.Round(_SSSEmployer / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "006";
                                _Amount = Math.Round(_SSSECC / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            if (_SSSSchedule02 == "1")
                            {
                                _AccountCode = "004";
                                _Amount = Math.Round(_SSSEmployee, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "005";
                                _Amount = Math.Round(_SSSEmployer, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "006";
                                _Amount = Math.Round(_SSSECC, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            #endregion
                            #region PHILHEALTH Computation
                            // PHILHEALTH Computation
                            clsGovermentComputation.PHILHEALTHComputation(clsDeclaration.sSystemConnection, 26, _DailyRate, out _PhilHealthEmployee, out _PhilHealthEmployer);
                            if (_PHealthSchedule01 == "1")
                            {
                                _AccountCode = "007";
                                _Amount = Math.Round(_PhilHealthEmployee / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "008";
                                _Amount = Math.Round(_PhilHealthEmployer / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            if (_PHealthSchedule02 == "1")
                            {
                                _AccountCode = "007";
                                _Amount = Math.Round(_PhilHealthEmployee, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "008";
                                _Amount = Math.Round(_PhilHealthEmployer, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            #endregion
                            #region PAGIBIG Computation
                            // PAGIBIG Computation
                            clsGovermentComputation.PAGIBIGComputation(clsDeclaration.sSystemConnection, _MonthlyRate, out _PAGIBIGEmployee, out _PAGIBIGEmployer);
                            if (_PagIbigSchedule01 == "1")
                            {
                                _AccountCode = "009";
                                _Amount = Math.Round(_PAGIBIGEmployee / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "010";
                                _Amount = Math.Round(_PAGIBIGEmployer / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            if (_PagIbigSchedule02 == "1")
                            {
                                _AccountCode = "009";
                                _Amount = Math.Round(_PAGIBIGEmployee, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "010";
                                _Amount = Math.Round(_PAGIBIGEmployer, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            #endregion
                            #region WTAX Computation
                            // WTAX Computation
                            clsGovermentComputation.WTAXComputationAnnual(clsDeclaration.sSystemConnection, _EmployeeNo, out _WtaxAmount);
                            if (_TaxSchedule01 == "1")
                            {
                                _AccountCode = "011";
                                _Amount = Math.Round(_WtaxAmount / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            #endregion
                            break;
                        case "B":
                            #region SSS Computation
                            // SSS Computation
                            clsGovermentComputation.SSSComputation(clsDeclaration.sSystemConnection, _MonthlyRate, out _SSSEmployee, out _SSSEmployer, out _SSSECC);

                            if (_SSSSchedule01 == "1")
                            {
                                _AccountCode = "004";
                                _Amount = Math.Round(_SSSEmployee / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "005";
                                _Amount = Math.Round(_SSSEmployer / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "006";
                                _Amount = Math.Round(_SSSECC / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            if (_SSSSchedule06 == "1")
                            {
                                _AccountCode = "004";
                                _Amount = Math.Round(_SSSEmployee, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "005";
                                _Amount = Math.Round(_SSSEmployer, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "006";
                                _Amount = Math.Round(_SSSECC, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            #endregion
                            #region PHILHEALTH Computation
                            // PHILHEALTH Computation
                            clsGovermentComputation.PHILHEALTHComputation(clsDeclaration.sSystemConnection, 26, _DailyRate, out _PhilHealthEmployee, out _PhilHealthEmployer);
                            if (_PHealthSchedule01 == "1")
                            {
                                _AccountCode = "007";
                                _Amount = Math.Round(_PhilHealthEmployee / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "008";
                                _Amount = Math.Round(_PhilHealthEmployer / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            if (_PHealthSchedule06 == "1")
                            {
                                _AccountCode = "007";
                                _Amount = Math.Round(_PhilHealthEmployee, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "008";
                                _Amount = Math.Round(_PhilHealthEmployer, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            #endregion
                            #region PAGIBIG Computation
                            // PAGIBIG Computation
                            clsGovermentComputation.PAGIBIGComputation(clsDeclaration.sSystemConnection, _MonthlyRate, out _PAGIBIGEmployee, out _PAGIBIGEmployer);
                            if (_PagIbigSchedule01 == "1")
                            {
                                _AccountCode = "009";
                                _Amount = Math.Round(_PAGIBIGEmployee / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "010";
                                _Amount = Math.Round(_PAGIBIGEmployer / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            if (_PagIbigSchedule06 == "1")
                            {
                                _AccountCode = "009";
                                _Amount = Math.Round(_PAGIBIGEmployee, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);

                                _AccountCode = "010";
                                _Amount = Math.Round(_PAGIBIGEmployer, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            #endregion
                            #region WTAX Computation
                            // WTAX Computation
                            clsGovermentComputation.WTAXComputationAnnual(clsDeclaration.sSystemConnection, _EmployeeNo, out _WtaxAmount);
                            if (_TaxSchedule01 == "1")
                            {
                                _AccountCode = "011";
                                _Amount = Math.Round(_WtaxAmount / 2, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            if (_TaxSchedule06 == "1")
                            {
                                _AccountCode = "011";
                                _Amount = Math.Round(_WtaxAmount, 2, MidpointRounding.AwayFromZero);
                                clsFunctions.InsertDetails(_Company, _PayrollPeriod, _EmployeeNo, _AccountCode, "", 0, _Amount, 0, 0, 0, _Branch, _Department);
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }

                    #endregion
                }

                #region Cola and Allowance Calculation
                // Insert Data for Cola
                _LoanRefenceNo = "";
                _NoOfHours = 0;
                _NoOfMins = 0;
                _Amount = Math.Round((_RegularDays * _COLAAmount), 2, MidpointRounding.AwayFromZero);

                _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VariableName = 'COLA'";
                _AccountCode = clsSQLClientFunctions.GetStringValue(_ConCompany, _sqlAccountCode, "VariableValue");


                if (_Amount != 0)
                {
                    clsFunctions.InsertDetails(_Company
                                   , _PayrollPeriod
                                   , _EmployeeNo
                                   , _AccountCode
                                   , _LoanRefenceNo
                                   , _NoOfHours
                                   , _Amount
                                   , _NoOfMins
                                   , _TotalHrs
                                   , _TotalDays
                                   , _Branch
                                   , _Department);
                }



                // Insert Data for Allowance
                _LoanRefenceNo = "";
                _NoOfHours = 0;
                _NoOfMins = 0;
                _Amount = Math.Round((_RegularDays * _DailyAllowance), 2, MidpointRounding.AwayFromZero);

                _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VariableName = 'Allowance'";
                _AccountCode = clsSQLClientFunctions.GetStringValue(_ConCompany, _sqlAccountCode, "VariableValue");


                if (_Amount != 0)
                {
                    clsFunctions.InsertDetails(_Company
                                   , _PayrollPeriod
                                   , _EmployeeNo
                                   , _AccountCode
                                   , _LoanRefenceNo
                                   , _NoOfHours
                                   , _Amount
                                   , _NoOfMins
                                   , _TotalHrs
                                   , _TotalDays
                                   , _Branch
                                   , _Department);
                }
                #endregion
            #endregion
            #region Uploading PayrollHeader
            //Start of Uploading PayrollHeader
            clsFunctions.InsertPayrollHeader(_Company
                                            , _PayrollPeriod
                                            , _EmployeeNo
                                            , _MonthlyRate
                                            , _DailyRate
                                            , _Department
                                            , _Branch
                                            , _PayrollType
                                            , _BankAccountNo
                                            , _PayrollMode);
            #endregion

          
            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Payroll Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }

        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"SELECT * FROM (
                                                SELECT 
                                                   B.[Company]
                                                   ,A.[EmployeeNo]
		                                                ,REPLACE(REPLACE(B.EmployeeName, CHAR(13), ' '), CHAR(10), ' ') AS EmployeeName
                                                   ,A.[MonthlyRate]
                                                   ,A.[DailyRate]
                                                   ,B.[TaxStatus]
                                                   ,A.[TotalDays]
                                                   ,A.[BasicPay]
                                                   ,A.[OTPay]
                                                   ,A.[OTHrs]
                                                   ,A.[SUNPay]
                                                   ,A.[SUNHrs]
                                                   ,A.[SPLPay]
                                                   ,A.[SPLHrs]
                                                   ,A.[LEGPay]
                                                   ,A.[LEGHrs]

                                                   ,A.[COLAAmount]
                                                   ,A.[OtherIncome]
                                                   ,A.[Gross]

                                                   ,A.[SSSEmployee]
                                                   ,A.[SSSEmployer]
                                                   ,A.[SSSEC]
                                                   ,A.[PhilHealthEmployee]
                                                   ,A.[PhilHealthEmployer]
                                                   ,A.[PagIbigEmployee]
                                                   ,A.[PagIbigEmployer]
                                                   ,A.[NonTaxPagIbig]
                                                   ,A.[WitholdingTax]


                                                   ,A.[SSSLoan]
                                                   ,A.[PagibigLoan]
                                                   ,A.[CalamityLoan]
                                                   ,A.[OtherLoan]

                                                   ,A.[OtherDeduction]
                                                   ,A.[TotalDeductions]

                                                   ,A.[NetPay]

                                                   ,A.[Branch]
                                                   ,A.[PayrollType]
                                                   ,A.[BankAccountNo]
                                                   ,A.[PayrollMode]
                                                   ,A.[PayrollPeriod]
,B.ConfiLevel
                                                FROM vwsPayrollHeader A 
	                                                INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
	                                                INNER JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode
                                                WHERE C.Area LIKE '%" + cboArea.Text + @"%' 
	                                                AND C.BCode LIKE '%" + _BCode + @"%'
	                                                AND B.EmployeeNo  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtEmpNo.Text + @"%' 
	                                                AND B.EmployeeName  COLLATE Latin1_General_CI_AS  LIKE  '%" + txtName.Text + @"%' 
                                                    AND C.DepCode LIKE '%" + _BPosition + @"%'
	                                                AND B.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
                                                    AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'
AND ISNULL(A.Validated,'N') = 'N'
	                                                --AND A.PayrollType = 'PAYROLL'
) XX                                         
WHERE XX.ConfiLevel IN (" + _GroupConfi + @")
ORDER BY XX.[EmployeeName]
                                            ";

        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);

        MessageBox.Show("Payroll Processing Complete");
    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        if(cboBranch.Text == "")
        {
            return;
        }

        if(chkAll.CheckState == CheckState.Checked)
        {

            _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        }
        else
        {
            _SQLSyntax = @"SELECT DISTINCT A.[PayrollPeriod],A.[DateOne],A.[DateTwo],A.[IsLocked] 
FROM vwsPayrollPeriod A LEFT JOIN PayrollLocker B ON A.PayrollPeriod = B.PayrollPeriod 
WHERE (B.Branch = '" + cboBranch.Text.Substring(0, 8) + @"' OR B.Branch IS NULL) AND (B.IsLocked IS NULL OR B.IsLocked = 0)
						ORDER BY A.[PayrollPeriod] DESC";
        }

        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        cboPayrolPeriod.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());
        }
    }

    private void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        if (cboBranch.Text == "")
        {
            return;
        }

        if (chkAll.CheckState == CheckState.Checked)
        {

            _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        }
        else
        {
                        _SQLSyntax = @"SELECT DISTINCT A.[PayrollPeriod],A.[DateOne],A.[DateTwo],A.[IsLocked] 
            FROM vwsPayrollPeriod A LEFT JOIN PayrollLocker B ON A.PayrollPeriod = B.PayrollPeriod 
            WHERE (B.Branch = '" + cboBranch.Text.Substring(0, 8) + @"' OR B.Branch IS NULL) AND (B.IsLocked IS NULL OR B.IsLocked = 0)
						            ORDER BY A.[PayrollPeriod] DESC";
        }

        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        cboPayrolPeriod.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());
        }
    }
}
