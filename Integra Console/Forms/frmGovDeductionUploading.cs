using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class frmGovDeductionUploading : Form
{
    private static DataTable _DataList = new DataTable();
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DeductionList = new DataTable();
    public frmGovDeductionUploading()
    {
        InitializeComponent();
    }

    private void frmGovDeductionUploading_Load(object sender, EventArgs e)
    {
        //string _SQLSyntax;
        //_SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
        //_CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        //foreach (DataRow row in _CompanyList.Rows)
        //{
        //    cboCompany.Items.Add(row[0].ToString());

        //}

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
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




    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {

        string _Branch;
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 4);
        }


        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = @"
DECLARE @PayPeriod AS NVARCHAR(20)
SET @PayPeriod = '" + cboPayrolPeriod.Text + @"'

DECLARE @Deduction TABLE
(
EmployeeNo NVARCHAR(30),
FullName NVARCHAR(250),
DateHired DATETIME,
DailyRate NUMERIC(19,6),
MonthlyRate NUMERIC(19,6),
RateDivisor NUMERIC(19,6),

SSSSchedule01 INT,
SSSSchedule02 INT,
SSSSchedule06 INT,
PagIbigSchedule01 INT,
PagIbigSchedule02 INT,
PagIbigSchedule06 INT,
PHealthSchedule01 INT,
PHealthSchedule02 INT,
PHealthSchedule06 INT,

TaxSchedule01 INT,
TaxSchedule06 INT,

[Payroll Period Cutoff] NVARCHAR(2),

StatusCode NVARCHAR(10),
[PersonalExempt] NUMERIC(19,6),
[Dependents] NUMERIC(19,6),

[SSSEmployee] NUMERIC(19,6),
[SSSEmployer] NUMERIC(19,6),
[SSSEC] NUMERIC(19,6),
[PhilHealthEmployee] NUMERIC(19,6),
[PhilHealthEmployer] NUMERIC(19,6),
[PAGIBIGEmployee] NUMERIC(19,6),
[PAGIBIGEmployer] NUMERIC(19,6)
)


INSERT INTO @Deduction

SELECT 

 A.EmployeeNo
,CONCAT(A.LastName ,', ' ,A.FirstName,' ',A.SuffixName,' ',A.MiddleInitial) AS FullName
,A.DateHired
,A.DailyRate
,A.MonthlyRate
,A.RateDivisor 

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

,RIGHT(@PayPeriod,1) AS [Payroll Period Cutoff]


,F.StatusCode
,F.PersonalExempt
,F.Dependents

, CASE WHEN A.DailyRate = 0 THEN 0 ELSE (SELECT Z.Employee / 2 FROM SSSTable Z WHERE (A.DailyRate * 26) BETWEEN Z.BracketFrom AND Z.BracketTo) END AS [SSSEmployee]
, CASE WHEN A.DailyRate = 0 THEN 0 ELSE (SELECT Z.Employer / 2 FROM SSSTable Z WHERE (A.DailyRate * 26) BETWEEN Z.BracketFrom AND Z.BracketTo) END AS [SSSEmployer]
, CASE WHEN A.DailyRate = 0 THEN 0 ELSE (SELECT Z.ECC / 2 FROM SSSTable Z WHERE (A.DailyRate * 26) BETWEEN Z.BracketFrom AND Z.BracketTo) END AS [SSSEC]


, CASE WHEN A.DailyRate = 0 THEN 0 ELSE (SELECT Z.Employee / 2 FROM PhilHealthTable Z WHERE (A.DailyRate * 26) BETWEEN Z.BracketFrom AND Z.BracketTo) END AS [PhilHealthEmployee]
, CASE WHEN A.DailyRate = 0 THEN 0 ELSE (SELECT Z.Employer / 2 FROM PhilHealthTable Z WHERE (A.DailyRate * 26) BETWEEN Z.BracketFrom AND Z.BracketTo) END AS [PhilHealthEmployer]

, CASE WHEN A.DailyRate = 0 THEN 0 ELSE (SELECT  ROUND(ROUND(((A.DailyRate * 26.16665) * (Z.Employee / 100)),0) / 2,0) FROM 
		(SELECT *,
		ISNULL(LEAD(p.IncomeBracket) OVER (ORDER BY p.IncomeBracket),99999) NextValue
		FROM PAGIBIGTable p) 
		Z WHERE (A.DailyRate * 26.16665) BETWEEN Z.IncomeBracket AND Z.NextValue) END AS [PAGIBIGEmployee]

, CASE WHEN A.DailyRate = 0 THEN 0 ELSE (SELECT ROUND(ROUND(((A.DailyRate * 26.16665) * (Z.Employer / 100)),0) / 2,0) FROM 
		(SELECT *,
		ISNULL(LEAD(p.IncomeBracket) OVER (ORDER BY p.IncomeBracket),99999) NextValue
		FROM PAGIBIGTable p) 
		Z WHERE (A.DailyRate * 26.16665) BETWEEN Z.IncomeBracket AND Z.NextValue) END AS [PAGIBIGEmployer]

FROM Employees A 
INNER JOIN CustomPYSetup D ON A.CustomPYCode = D.CustomPYCode
INNER JOIN TaxStatus F ON A.TaxStatus = F.StatusCode
LEFT JOIN vwsDepartmentList G ON A.Department = G.DepartmentCode
WHERE G.Area LIKE '%" + cboArea.Text + @"%' AND G.BranchCode LIKE '%" + _Branch + @"%'




SELECT 
*
,
(
SELECT Z.Excess FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
) AS [Based Computation]

,(Y1.[Taxable Income] - 
(
SELECT Z.Excess FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
)) AS [SEC Based Computation]


,
(
SELECT Z.Rate FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
) AS WTRate

,(
SELECT Z.Base FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
) AS WTBase



,ROUND((Y1.[Taxable Income] - 
(
SELECT Z.Excess FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
)) * 
((
SELECT Z.Rate FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
) / 100),2)
AS [WTax Additional]


,
CASE WHEN Y1.StatusCode = 'Z' THEN 0 ELSE

CASE 
	WHEN Y1.TaxSchedule01 = 1 THEN (
ROUND((((
SELECT Z.Base FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
) + 
((Y1.[Taxable Income] - 
(
SELECT Z.Excess FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
)) * 
((
SELECT Z.Rate FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
) / 100))) / 24),2))
	WHEN Y1.TaxSchedule06 = 1 AND Y1.[Payroll Period Cutoff] = 'B' THEN (
ROUND((((
SELECT Z.Base FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
) + 
((Y1.[Taxable Income] - 
(
SELECT Z.Excess FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
)) * 
((
SELECT Z.Rate FROM 
(SELECT *,
		(ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
		FROM AnnualTaxTable P) Z
WHERE Y1.[Taxable Income] BETWEEN Z.Excess AND Z.NextValue
) / 100))) / 24),2)) * 2
	ELSE 0
END

END AS [WTax Amount]


FROM (


SELECT 

Q.EmployeeNo,Q.FullName,Q.DailyRate,Q.MonthlyRate,Q.RateDivisor

,CASE 
	WHEN Q.SSSSchedule01 = 1 THEN Q.[SSSEmployee] 
	WHEN Q.SSSSchedule02 = 1 AND Q.[Payroll Period Cutoff] = 'A' THEN Q.[SSSEmployee] * 2
	WHEN Q.SSSSchedule06 = 1 AND Q.[Payroll Period Cutoff] = 'B' THEN Q.[SSSEmployee] * 2
	ELSE 0
END AS [SSSEmployee]
,CASE 
	WHEN Q.SSSSchedule01 = 1 THEN Q.[SSSEmployer] 
	WHEN Q.SSSSchedule02 = 1 AND Q.[Payroll Period Cutoff] = 'A' THEN Q.[SSSEmployer] * 2 
	WHEN Q.SSSSchedule06 = 1 AND Q.[Payroll Period Cutoff] = 'B' THEN Q.[SSSEmployer] * 2 
	ELSE 0
END AS [SSSEmployer]
,CASE 
	WHEN Q.SSSSchedule01 = 1 THEN Q.[SSSEC] 
	WHEN Q.SSSSchedule02 = 1 AND Q.[Payroll Period Cutoff] = 'A' THEN Q.[SSSEC] * 2 
	WHEN Q.SSSSchedule06 = 1 AND Q.[Payroll Period Cutoff] = 'B' THEN Q.[SSSEC] * 2 
	ELSE 0
END AS [SSSEC]

,CASE 
	WHEN Q.PHealthSchedule01 = 1 THEN Q.[PhilHealthEmployee] 
	WHEN Q.PHealthSchedule02 = 1 AND Q.[Payroll Period Cutoff] = 'A' THEN Q.[PhilHealthEmployee] * 2
	WHEN Q.PHealthSchedule06 = 1 AND Q.[Payroll Period Cutoff] = 'B' THEN Q.[PhilHealthEmployee] * 2
	ELSE 0
END AS [PhilHealthEmployee]
,CASE 
	WHEN Q.PHealthSchedule01 = 1 THEN Q.[PhilHealthEmployer] 
	WHEN Q.PHealthSchedule02 = 1 AND Q.[Payroll Period Cutoff] = 'A' THEN Q.[PhilHealthEmployer] * 2 
	WHEN Q.PHealthSchedule06 = 1 AND Q.[Payroll Period Cutoff] = 'B' THEN Q.[PhilHealthEmployer] * 2 
	ELSE 0
END AS [PhilHealthEmployer]

,CASE 
	WHEN Q.PagIbigSchedule01 = 1 THEN Q.[PAGIBIGEmployee] 
	WHEN Q.PagIbigSchedule02 = 1 AND Q.[Payroll Period Cutoff] = 'A' THEN Q.[PAGIBIGEmployee] * 2
	WHEN Q.PagIbigSchedule06 = 1 AND Q.[Payroll Period Cutoff] = 'B' THEN Q.[PAGIBIGEmployee] * 2
	ELSE 0
END AS [PAGIBIGEmployee]
,CASE 
	WHEN Q.PagIbigSchedule01 = 1 THEN Q.[PAGIBIGEmployer] 
	WHEN Q.PagIbigSchedule02 = 1 AND Q.[Payroll Period Cutoff] = 'A' THEN Q.[PAGIBIGEmployer] * 2 
	WHEN Q.PagIbigSchedule06 = 1 AND Q.[Payroll Period Cutoff] = 'B' THEN Q.[PAGIBIGEmployer] * 2 
	ELSE 0
END AS [PAGIBIGEmployer]


,Q.SSSSchedule01
,Q.SSSSchedule02
,Q.SSSSchedule06

,Q.PagIbigSchedule01
,Q.PagIbigSchedule02
,Q.PagIbigSchedule06

,Q.PHealthSchedule01
,Q.PHealthSchedule02
,Q.PHealthSchedule06

,Q.TaxSchedule01
,Q.TaxSchedule06


,Q.[Payroll Period Cutoff]
,CAST((Q.DailyRate * Q.RateDivisor * 12) AS NUMERIC(19,2)) AS [Gross Compensation]
,CAST((Q.PersonalExempt + Q.Dependents) AS NUMERIC(19,2)) AS [Total Exemption]
,((Q.SSSEmployee * 24) + (Q.PhilHealthEmployee * 24) + (Q.PAGIBIGEmployee * 24)) AS [Total Contribution]
,(CAST((Q.PersonalExempt + Q.Dependents) AS NUMERIC(19,2))) + ((Q.SSSEmployee * 24) + (Q.PhilHealthEmployee * 24) + (Q.PAGIBIGEmployee * 24)) AS [Deduction Amount]
, CASE WHEN Q.DailyRate = 0 THEN 0 ELSE ((CAST((Q.DailyRate * Q.RateDivisor * 12) AS NUMERIC(19,2))) -
((CAST((Q.PersonalExempt + Q.Dependents) AS NUMERIC(19,2))) + ((Q.SSSEmployee * 24) + (Q.PhilHealthEmployee * 24) + (Q.PAGIBIGEmployee * 24)))
) END AS [Taxable Income]
, Q.StatusCode

FROM @Deduction Q
) Y1
                      ";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        _DeductionList = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable, "PayrollUpdate");

    }



    private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsDeclaration.sServer = _CompanyList.Rows[cboCompany.SelectedIndex][3].ToString();
        clsDeclaration.sCompany = _CompanyList.Rows[cboCompany.SelectedIndex][4].ToString();
        clsDeclaration.sUsername = _CompanyList.Rows[cboCompany.SelectedIndex][5].ToString();
        clsDeclaration.sPassword = _CompanyList.Rows[cboCompany.SelectedIndex][6].ToString();

        clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                       clsDeclaration.sServer, clsDeclaration.sCompany,
                                       clsDeclaration.sUsername, clsDeclaration.sPassword
                                    );

        if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == false)
        {
            return;
        }



        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM [PayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);

        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());

        }



        _SQLSyntax = "SELECT DISTINCT A.Area FROM usr_Branches A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }

    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        //clsDeclaration.sServer = _CompanyList.Rows[cboCompany.SelectedIndex][3].ToString();
        //clsDeclaration.sCompany = _CompanyList.Rows[cboCompany.SelectedIndex][4].ToString();
        //clsDeclaration.sUsername = _CompanyList.Rows[cboCompany.SelectedIndex][5].ToString();
        //clsDeclaration.sPassword = _CompanyList.Rows[cboCompany.SelectedIndex][6].ToString();

        //clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
        //                               clsDeclaration.sServer, clsDeclaration.sCompany,
        //                               clsDeclaration.sUsername, clsDeclaration.sPassword
        //                            );
        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please select payroll period");
            return;
        }

        foreach (DataRow row in _DataList.Rows)
        {
            string _Company = row[0].ToString();
            clsDeclaration.sCompanyConnection = clsFunctions.GetCompanyConnectionString(_Company);

            string _Employee = row[1].ToString();
            double _BasicRate = double.Parse(row[3].ToString());
            double _MonthlyRate = double.Parse(row[4].ToString());





            DataTable _SettingDataTable;
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
            _SettingDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SettingSyntax);



            string _SSSSchedule01;
            string _SSSSchedule02;
            string _SSSSchedule06;

            _SSSSchedule01 = clsSQLClientFunctions.GetData(_SettingDataTable, "SSSSchedule01", "1");
            _SSSSchedule02 = clsSQLClientFunctions.GetData(_SettingDataTable, "SSSSchedule02", "1");
            _SSSSchedule06 = clsSQLClientFunctions.GetData(_SettingDataTable, "SSSSchedule06", "1");


            double _SSSEmployee = 0;
            double _SSSEmployeer = 0;
            double _SSSECC = 0;

            if (_SSSSchedule01 == "1")
            {
                _SSSEmployee = double.Parse(row[7].ToString()) / 2;
                _SSSEmployeer = double.Parse(row[8].ToString()) / 2;
                _SSSECC = double.Parse(row[9].ToString()) / 2;
            }
            if (_SSSSchedule02 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "A")
            {
                _SSSEmployee = double.Parse(row[7].ToString());
                _SSSEmployeer = double.Parse(row[8].ToString());
                _SSSECC = double.Parse(row[9].ToString());
            }
            if (_SSSSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            {
                _SSSEmployee = double.Parse(row[7].ToString());
                _SSSEmployeer = double.Parse(row[8].ToString());
                _SSSECC = double.Parse(row[9].ToString());
            }


            double _SSSBase;
            if (_SSSEmployee == 0)
            {
                _SSSBase = 0;
            }
            else
            {
                _SSSBase = (_MonthlyRate);
            }



            string _PagIbigSchedule01;
            string _PagIbigSchedule02;
            string _PagIbigSchedule06;

            _PagIbigSchedule01 = clsSQLClientFunctions.GetData(_SettingDataTable, "PagIbigSchedule01", "1");
            _PagIbigSchedule02 = clsSQLClientFunctions.GetData(_SettingDataTable, "PagIbigSchedule02", "1");
            _PagIbigSchedule06 = clsSQLClientFunctions.GetData(_SettingDataTable, "PagIbigSchedule06", "1");


            double _PagibigEmployee = 0;
            double _PagibigEmployer = 0;

            if (_PagIbigSchedule01 == "1")
            {
                _PagibigEmployee = double.Parse(row[10].ToString()) / 2;
                _PagibigEmployer = double.Parse(row[11].ToString()) / 2;
            }
            if (_PagIbigSchedule02 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "A")
            {
                _PagibigEmployee = double.Parse(row[10].ToString());
                _PagibigEmployer = double.Parse(row[11].ToString());
            }
            if (_PagIbigSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            {
                _PagibigEmployee = double.Parse(row[10].ToString());
                _PagibigEmployer = double.Parse(row[11].ToString());
            }


            double _PagIbigBase;
            if (_PagibigEmployee == 0)
            {
                _PagIbigBase = 0;
            }
            else
            {
                _PagIbigBase = (_MonthlyRate);
            }



            string _PHealthSchedule01;
            string _PHealthSchedule02;
            string _PHealthSchedule06;

            _PHealthSchedule01 = clsSQLClientFunctions.GetData(_SettingDataTable, "PHealthSchedule01", "1");
            _PHealthSchedule02 = clsSQLClientFunctions.GetData(_SettingDataTable, "PHealthSchedule02", "1");
            _PHealthSchedule06 = clsSQLClientFunctions.GetData(_SettingDataTable, "PHealthSchedule06", "1");



            double _PhilHEmployee = 0;
            double _PhilHEmployeer = 0;

            if (_PHealthSchedule01 == "1")
            {
                _PhilHEmployee = double.Parse(row[12].ToString()) / 2;
                _PhilHEmployeer = double.Parse(row[13].ToString()) / 2;
            }
            if (_PHealthSchedule02 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "A")
            {
                _PhilHEmployee = double.Parse(row[12].ToString());
                _PhilHEmployeer = double.Parse(row[13].ToString());
            }
            if (_PHealthSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            {
                _PhilHEmployee = double.Parse(row[12].ToString());
                _PhilHEmployeer = double.Parse(row[13].ToString());
            }


            double _PhilHBase;
            if (_PhilHEmployee == 0)
            {
                _PhilHBase = 0;
            }
            else
            {
                _PhilHBase = (_MonthlyRate);
            }





            string _TaxSchedule01;
            string _TaxSchedule06;

            _TaxSchedule01 = clsSQLClientFunctions.GetData(_SettingDataTable, "TaxSchedule01", "1");
            _TaxSchedule06 = clsSQLClientFunctions.GetData(_SettingDataTable, "TaxSchedule06", "1");


            double _WTax = 0;
            //if (_TaxSchedule01 == "1")
            //{
            //    _WTax = double.Parse(row[11].ToString()) / 2;
            //}

            //if (_TaxSchedule06 == "1" && cboPayrolPeriod.Text.Substring(8, 1) == "B")
            //{
            //    _WTax = double.Parse(row[11].ToString());
            //}





            double _WTaxBase;
            if (_WTax == 0)
            {
                _WTaxBase = 0;
            }
            else
            {
                _WTaxBase = (_BasicRate);
            }





            string _SQLExecute;
            _SQLExecute = @"

                    UPDATE A SET 
                    A.SSSBaseAmount = '" + _SSSBase + @"' 
                    , A.SSSEmployee = '" + _SSSEmployee + @"' 
                    , A.SSSEmployer = '" + _SSSEmployeer + @"' 
                    , A.SSSEC = '" + _SSSECC + @"' 
                    , A.PhilHealthBaseAmount = '" + _PhilHBase + @"' 
                    , A.PhilHealthEmployee = '" + _PhilHEmployee + @"' 
                    , A.PhilHealthEmployer = '" + _PhilHEmployeer + @"' 
                    , A.PagIbigBaseAmount = '" + _PagIbigBase + @"' 
                    , A.PagIbigEmployee = '" + _PagibigEmployee + @"' 
                    , A.PagIbigEmployer = '" + _PagibigEmployer + @"' 
                    , A.NonTaxPagIbig = '" + _PagibigEmployer + @"' 
                    , A.TaxBaseAmount = '" + _WTaxBase + @"'
                    , A.WitholdingTax = '" + _WTax + @"'
                    , A.TotalDeductions = (A.OtherDeduction + " + (_SSSEmployee + _PhilHEmployee + _PagibigEmployee + _WTax) + @")
                    , A.NetPay = (A.Gross - (A.OtherDeduction + " + (_SSSEmployee + _PhilHEmployee + _PagibigEmployee + _WTax) + @"))
                    FROM PayrollHeader A
                    WHERE A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"' AND A.EmployeeNo = '" + _Employee + @"'

                              ";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLExecute);
        
        
    }


        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }


        string _SQLSyntax;
        _SQLSyntax = @"
                    SELECT A.EmployeeNo
                    , A.SSSEmployee, A.SSSEmployer, A.SSSEC
                    , A.PhilHealthEmployee, A.PhilHealthEmployer
                    , A.PagIbigEmployee, A.PagIbigEmployer, A.NonTaxPagIbig
                    , A.WitholdingTax
                    FROM vwsPayrollHeader A INNER JOIN vwsEmployees B ON A.EmployeeNo =  B.EmployeeNo
					LEFT JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode
                    WHERE A.PayrollPeriod = '" + cboPayrolPeriod .Text+ @"'
                    AND C.BCode LIKE '%" + _BCode + @"%' AND C.AREA LIKE '%" + cboArea.Text + @"%'
                 ";
        DataTable _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);




        //if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == false)
        //{
        //    return;
        //}
        //string _PayrollPeriod = cboPayrolPeriod.Text;


        //string _Branch;
        //if (cboBranch.Text == "")
        //{
        //    _Branch = "";
        //}
        //else
        //{
        //    _Branch = cboBranch.Text.Substring(0, 4);
        //}




        //foreach (DataRow row in _DeductionList.Rows)
        //{

        //    string _Employee = row[0].ToString();
        //    double _BasicRate = double.Parse(row[2].ToString());
        //    double _MonthlyRate = double.Parse(row[3].ToString());







        MessageBox.Show("Data Uploaded.");

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
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
            _BCode = cboBranch.Text.Substring(0,8);
        }


            _SQLSyntax = @"SELECT A.Company, A.EmployeeNo, CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS[EmployeeName],
        A.DailyRate, A.RateDivisor, A.MonthlyRate, A.TaxStatus 
        ,'' AS [SSS Employee] 
        ,'' AS [SSS Employer] 
        ,'' AS [SSS ECC] 
        ,'' AS [PAGIBIG Employee] 
        ,'' AS [PAGIBIG Employer] 
        ,'' AS [PHILHEALTH Employee] 
        ,'' AS [PHILHEALTH Employee] 
        ,'' AS [WTax Amount]

        FROM vwsEmployees A INNER JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
        WHERE B.BCode LIKE '%" + _BCode + @"%' AND B.Area LIKE '%" + cboArea.Text + @"%' AND A.EmpStatus NOT IN (3,4,6)
        AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);


        double _PayrollMonth;
        string _PayrollYear;
        _PayrollMonth = 12 - double.Parse(cboPayrolPeriod.Text.Substring(5, 2));
        _PayrollYear = cboPayrolPeriod.Text.Substring(0, 4);



        foreach (DataRow row in _DataTable.Rows)
        {
            string _Company = row[0].ToString();
            clsDeclaration.sCompanyConnection = clsFunctions.GetCompanyConnectionString(_Company);


            string _GOVSyntax;
            DataTable _GOVDataTable;

            string _Employee = row[1].ToString();
            string _EmployeeName = row[2].ToString();


            string _Rate = row[3].ToString();
            string _Monthly = row[5].ToString();
            string _TaxStatus = row[6].ToString();




            _GOVSyntax = "SELECT Z.Employer, Z.Employee, Z.ECC FROM SSSTable Z WHERE '" + _Monthly + "' BETWEEN Z.BracketFrom AND Z.BracketTo";
            _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);


            double _SSSEmployee;
            double _SSSEmployer;
            double _SSSECC;

            _SSSEmployee = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employee", "1"));
            _SSSEmployer = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employer", "1"));
            _SSSECC = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "ECC", "1"));



            _GOVSyntax = @"SELECT * FROM 
                            (SELECT *,
                            ISNULL(LEAD(p.IncomeBracket) OVER(ORDER BY p.IncomeBracket), 99999) NextValue
                            FROM PAGIBIGTable p)
                            Z WHERE '" + _Monthly + "' BETWEEN Z.IncomeBracket AND Z.NextValue";
            _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

            double _PAGIBIGEmployee;
            double _PAGIBIGEmployer;
            double _MaxCont;

            _MaxCont = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "MaxContribution", "1"));

            _PAGIBIGEmployee = Math.Round(double.Parse(_Monthly) * (double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employee", "1")) / 100),0,MidpointRounding.AwayFromZero);
            _PAGIBIGEmployer = Math.Round(double.Parse(_Monthly) * (double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employer", "1")) / 100), 0, MidpointRounding.AwayFromZero);

            if (_PAGIBIGEmployee > _MaxCont)
            {
                _PAGIBIGEmployee = _MaxCont;
                _PAGIBIGEmployer = _MaxCont;
            }



            _GOVSyntax = "SELECT Z.Employer, Z.Employee FROM PhilHealthTable Z WHERE '" + _Monthly + "' BETWEEN Z.BracketFrom AND Z.BracketTo";
            _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);


            double _PhilHealthEmployee;
            double _PhilHealthEmployer;

            _PhilHealthEmployee = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employee", "1"));
            _PhilHealthEmployer = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employer", "1"));







            _GOVSyntax = "SELECT * FROM TaxStatus Z WHERE Z.StatusCode = '" + _TaxStatus + "'";
            _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

            double _PersonalExempt;
            double _Dependents;

            _PersonalExempt = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "PersonalExempt", "1"));
            _Dependents = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Dependents", "1"));




            double _TotalConribution;
            double _TotalExemption;



            _TotalConribution = (_SSSEmployee * 12) + (_PAGIBIGEmployee * 12) + (_PhilHealthEmployee * 12);
            _TotalExemption = _PersonalExempt + _Dependents;



            _GOVSyntax = @"SELECT ISNULL(ROUND(SUM(Z.Amount),2),0) AS RegularPay
                                FROM PayrollDetails Z WHERE Z.AccountCode in 
                                (select AccountCode from AccountCode X WHERE X.AccountType IN (0)) 
                                AND Z.EmployeeNo = '" + _Employee + "' AND SUBSTRING(Z.PayrollPeriod,1,7) = '" + cboPayrolPeriod.Text.Substring(0, 7) + "'";
            _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

            double _BasicPay;
            _BasicPay = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "RegularPay", "1"));



            _GOVSyntax = @"SELECT ISNULL(SUM(Z.WitholdingTax),0) AS WitholdingTax FROM PayrollHeader Z WHERE CAST(SUBSTRING(Z.PayrollPeriod,6,2) AS INT) < '" + double.Parse(cboPayrolPeriod.Text.Substring(5, 2)) + @"' AND EmployeeNo = '" + _Employee + "' AND SUBSTRING(Z.PayrollPeriod,1,4) = '" + _PayrollYear + "'";
            _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

            double _GrossCompensation;
            _GrossCompensation = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "WitholdingTax", "1")) + _BasicPay + (double.Parse(_Monthly) * _PayrollMonth);






            double _DeductionAmount;
            double _TaxableIncome;

            _DeductionAmount = _TotalExemption + _TotalConribution;
            _TaxableIncome = _GrossCompensation - _DeductionAmount;




            _GOVSyntax = @"SELECT * FROM 
                            (SELECT *,
                              (ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
                              FROM AnnualTaxTable P) Z
                            WHERE '" + _TaxableIncome + "' BETWEEN Z.Excess AND Z.NextValue";
            _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);


            double _WtaxExcess;
            double _WtaxBase;
            double _WtaxRate;
            _WtaxExcess = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Excess", "1"));
            _WtaxBase = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Base", "1"));
            _WtaxRate = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Rate", "1"));


            double _BasedComputation;
            double _WTaxAdditional;
            double _WtaxAmount;

            _BasedComputation = _TaxableIncome - _WtaxExcess;
            _WTaxAdditional = (_BasedComputation * (_WtaxRate / 100));
            _WtaxAmount = ((_WtaxBase + _WTaxAdditional) / 12);


            row[7] = Math.Round(_SSSEmployee, 2);
            row[8] = Math.Round(_SSSEmployer, 2);
            row[9] = Math.Round(_SSSECC, 2);
            row[10] = Math.Round(_PAGIBIGEmployee, 2, MidpointRounding.AwayFromZero);
            row[11] = Math.Round(_PAGIBIGEmployer, 2, MidpointRounding.AwayFromZero);
            row[12] = Math.Round(_PhilHealthEmployee, 2, MidpointRounding.AwayFromZero);
            row[13] = Math.Round(_PhilHealthEmployer, 2, MidpointRounding.AwayFromZero);
            row[14] = Math.Round(0.00, 2);





        }

        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);
        _DataList = _DataTable;
        MessageBox.Show("Data Ready To Upload");

        //string _Branch;
        //if (cboBranch.Text == "")
        //{
        //    _Branch = "";
        //}
        //else
        //{
        //    _Branch = cboBranch.Text.Substring(0, 4);
        //}



        //double _PayrollMonth;
        //string _PayrollYear;
        //_PayrollMonth = 12 - double.Parse(cboPayrolPeriod.Text.Substring(5, 2));
        //_PayrollYear = cboPayrolPeriod.Text.Substring(0, 4);

        //string _GOVSyntax;
        //DataTable _GOVDataTable;

        //string _SQLSyntax;
        //_SQLSyntax = @"
        //            SELECT A.EmployeeNo, CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName],
        //            A.DailyRate,D.RateDivisor, A.MonthlyRate, A.TaxStatus
        //            FROM Employees A 
        //            INNER JOIN CustomPYSetup D ON A.CustomPYCode = D.CustomPYCode
        //            LEFT JOIN vwsDepartmentList G ON A.Department = G.DepartmentCode
        //            WHERE  G.BranchCode LIKE '%" + _Branch + @"%' AND G.AREA LIKE '%" + cboArea.Text + @"%'
        //            AND A.Category IN (" +clsDeclaration.sConfiLevel + @")
        //            ORDER BY A.LastName,A.FirstName
        //             ";
        //DataTable _DataTable;
        //_DataTable  = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);


        //string _Display;
        //_Display = "SELECT * FROM (";

        //int _Count = _DataTable.Rows.Count;
        //int i = 0;
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    string _Employee = row[0].ToString();
        //    string _EmployeeName = row[1].ToString();


        //    string _Rate = row[2].ToString();
        //    string _Monthly = row[4].ToString();
        //    string _TaxStatus = row[5].ToString();



        //    _GOVSyntax = "SELECT Z.Employer, Z.Employee, Z.ECC FROM SSSTable Z WHERE '" + _Monthly  + "' BETWEEN Z.BracketFrom AND Z.BracketTo";
        //    _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);


        //    double _SSSEmployee;
        //    double _SSSEmployer;
        //    double _SSSECC;

        //    _SSSEmployee = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employee","1"));
        //    _SSSEmployer = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employer", "1"));
        //    _SSSECC = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "ECC", "1"));



        //    _GOVSyntax = @"SELECT * FROM 
        //                (SELECT *,
        //                ISNULL(LEAD(p.IncomeBracket) OVER(ORDER BY p.IncomeBracket), 99999) NextValue
        //                FROM PAGIBIGTable p)
        //                Z WHERE '" + _Monthly + "' BETWEEN Z.IncomeBracket AND Z.NextValue";
        //    _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

        //    double _PAGIBIGEmployee;
        //    double _PAGIBIGEmployer;

        //    _PAGIBIGEmployee = double.Parse(_Monthly) * (double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employee", "1")) / 100);
        //    _PAGIBIGEmployer = double.Parse(_Monthly) * (double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employer", "1")) / 100);



        //    _GOVSyntax = "SELECT Z.Employer, Z.Employee FROM PhilHealthTable Z WHERE '" + _Monthly + "' BETWEEN Z.BracketFrom AND Z.BracketTo";
        //    _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);


        //    double _PhilHealthEmployee;
        //    double _PhilHealthEmployer;

        //    _PhilHealthEmployee = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employee", "1"));
        //    _PhilHealthEmployer = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Employer", "1"));







        //    _GOVSyntax = "SELECT * FROM TaxStatus Z WHERE Z.StatusCode = '" + _TaxStatus  + "'";
        //    _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

        //    double _PersonalExempt;
        //    double _Dependents;

        //    _PersonalExempt = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "PersonalExempt", "1"));
        //    _Dependents = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Dependents", "1"));




        //    double _TotalConribution;
        //    double _TotalExemption;



        //    _TotalConribution = (_SSSEmployee * 12) + (_PAGIBIGEmployee * 12) + (_PhilHealthEmployee * 12);
        //    _TotalExemption = _PersonalExempt + _Dependents;



        //    _GOVSyntax = @"SELECT ISNULL(ROUND(SUM(Z.Amount),2),0) AS RegularPay
        //                    FROM PayrollDetails Z WHERE Z.AccountCode in 
        //                    (select AccountCode from AccountCode X WHERE X.AccountType IN (0)) 
        //                    AND Z.EmployeeNo = '" + _Employee  + "' AND SUBSTRING(Z.PayrollPeriod,1,7) = '" + cboPayrolPeriod.Text.Substring(0,7) + "'";
        //    _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

        //    double _BasicPay;
        //    _BasicPay = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "RegularPay", "1"));



        //    _GOVSyntax = @"SELECT ISNULL(SUM(Z.WitholdingTax),0) AS WitholdingTax FROM PayrollHeader Z WHERE CAST(SUBSTRING(Z.PayrollPeriod,6,2) AS INT) < '" + double.Parse(cboPayrolPeriod.Text.Substring(5, 2))  + @"' AND EmployeeNo = '" + _Employee + "' AND SUBSTRING(Z.PayrollPeriod,1,4) = '" + _PayrollYear  + "'";
        //    _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);

        //    double _GrossCompensation;
        //    _GrossCompensation = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "WitholdingTax", "1")) + _BasicPay + (double.Parse(_Monthly) * _PayrollMonth);






        //    double _DeductionAmount;
        //    double _TaxableIncome;

        //    _DeductionAmount = _TotalExemption + _TotalConribution;
        //    _TaxableIncome = _GrossCompensation - _DeductionAmount;




        //    _GOVSyntax = @"SELECT * FROM 
        //                (SELECT *,
        //                  (ISNULL(LEAD(P.Excess) OVER (ORDER BY P.Excess),99999)) - 1 NextValue
        //                  FROM AnnualTaxTable P) Z
        //                WHERE '" + _TaxableIncome  + "' BETWEEN Z.Excess AND Z.NextValue";
        //    _GOVDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _GOVSyntax);


        //    double _WtaxExcess;
        //    double _WtaxBase;
        //    double _WtaxRate;
        //    _WtaxExcess = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Excess", "1"));
        //    _WtaxBase = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Base", "1"));
        //    _WtaxRate = double.Parse(clsSQLClientFunctions.GetData(_GOVDataTable, "Rate", "1"));


        //    double _BasedComputation;
        //    double _WTaxAdditional;
        //    double _WtaxAmount;

        //    _BasedComputation = _TaxableIncome - _WtaxExcess;
        //    _WTaxAdditional = (_BasedComputation * (_WtaxRate / 100));
        //    _WtaxAmount = ((_WtaxBase + _WTaxAdditional) / 12);

        //    _Display = _Display + @"
        //                                SELECT '" + _Employee + @"' AS [EMPLOYEE CODE]
        //                                      ,'" + _EmployeeName + @"' AS [EMPLOYEE NAME]
        //                                      ,'" + _BasicPay + @"' AS [Basic Pay]
        //                                      ,'" + _Monthly + @"' AS [Monthly Pay]
        //                                      ,'" + Math.Round(_SSSEmployee, 2) + @"' AS [SSS Employee]
        //                                      ,'" + Math.Round(_SSSEmployer, 2) + @"' AS [SSS Employer]
        //                                      ,'" + Math.Round(_SSSECC, 2) + @"' AS [SSS ECC]
        //                                      ,'" + Math.Round(_PAGIBIGEmployee) + @"' AS [PAGIBIG Employee]
        //                                      ,'" + Math.Round(_PAGIBIGEmployer) + @"' AS [PAGIBIG Employer]
        //                                      ,'" + Math.Round(_PhilHealthEmployee, 2) + @"' AS [PHILHEALTH Employee]
        //                                      ,'" + Math.Round(_PhilHealthEmployer, 2) + @"' AS [PHILHEALTH Employer]


        //                                      --,'" + _GrossCompensation + @"' AS [Gross Compensation]
        //                                      --,'" + _TaxableIncome + @"' AS [Taxable Income]
        //                                      --,'" + _WtaxExcess + @"' AS [Excess]
        //                                      --,'" + _BasedComputation + @"' AS [Based Computation]

        //                                      --,'" + _WtaxBase + @"' AS [Base]
        //                                      --,'" + _WTaxAdditional + @"' AS [WTax Additional]
        //                                      ,'" + Math.Round(0.00, 2) + @"' AS [WTax Amount]
        //                                ";

        //    i++;
        //    if (i != _Count)
        //    {
        //        _Display = _Display + @"UNION ALL
        //                                ";
        //    }
        //}



        //_Display = _Display + ") Z ORDER BY Z.[EMPLOYEE NAME]";
        //DataTable _Table;
        //_Table = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _Display);
        //_DeductionList = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _Display);
        //clsFunctions.DataGridViewSetup(dgvDisplay, _Table);

        //MessageBox.Show("Data ready to upload.");
    }
}
