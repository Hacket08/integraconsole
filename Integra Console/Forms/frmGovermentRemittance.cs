using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;

public partial class frmGovermentRemittance : Form
{
    private static DataTable _DataListRemittance = new DataTable();
    private static DataTable _DataListRemittanceBranch = new DataTable();

    private static DataTable _dlDESISSS = new DataTable();
    private static DataTable _dlDESISSSBranch = new DataTable();

    public frmGovermentRemittance()
    {
        InitializeComponent();
    }

    private void frmGovermentRemittance_Load(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT DISTINCT LEFT([PayrollPeriod],7) AS [MonthyPayroll] FROM dbo.[vwsPayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        cboPayrolPeriod.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());
        }

        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = 1";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboCompany.Items.Clear();
        cboCompany.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }
        cboCompany.SelectedIndex = 0;

        cboGoverment.Items.Clear();
        cboGoverment.Items.Add("ALL");
        cboGoverment.Items.Add("SSS");
        cboGoverment.Items.Add("PAGIBIG");
        cboGoverment.Items.Add("WTAX");
        cboGoverment.SelectedIndex = 0;
    }


    private static string GovRemittanceDetails(string _PayrollPeriod ,string _Company = "", string _ReportType = "")
    {


        string _sqlReportType = "";

        switch (_ReportType)
        {
            case "WTAX":
                _sqlReportType = @"
                                                , SUM(AA.[WTax Month Cont]) AS [WTax Month Cont]
                                                , SUM(AA.[Total Tax]) AS [Total Tax]
                                                ";
                break;
            case "SSS":
                _sqlReportType = @"
                                                , SUM(AA.[SSS Month Cont]) AS [SSS Month Cont]
                                                , SUM(AA.[SSS Employee]) AS [SSS Employee]
                                                , SUM(AA.[SSS Employer]) AS [SSS Employer]
                                                , SUM(AA.[SSS Total Cont]) AS [SSS Total Cont]

                                                , ISNULL(SUM(AA.[15TH SSS Loan]),0) AS [15TH SSS Loan]
                                                , ISNULL(SUM(AA.[30TH SSS Loan]),0) AS [30TH SSS Loan]
                                                , ISNULL(SUM(AA.[15TH SSS Loan]),0) + ISNULL(SUM(AA.[30TH SSS Loan]),0) AS [Total SSS Loan]

                                                , SUM(AA.[PhilHealth Month Cont]) AS [PhilHealth Month Cont]
                                                , SUM(AA.[PhilHealth Employee]) AS [PhilHealth Employee]
                                                , SUM(AA.[PhilHealth Employer]) AS [PhilHealth Employer]
                                                , SUM(AA.[PhilHealth Total Cont]) AS [PhilHealth Total Cont]
                                                ";

                break;
            case "PAGIBIG":
                _sqlReportType = @"
                                                , SUM(AA.[PagIbig Month Cont]) AS [PagIbig Month Cont]
                                                , SUM(AA.[PagIbig Employee]) AS [PagIbig Employee]
                                                , SUM(AA.[PagIbig Employer]) AS [PagIbig Employer]
                                                , SUM(AA.[PagIbig Total Cont]) AS [PagIbig Total Cont]

                                                , ISNULL(SUM(AA.[15TH PagIbig Loan]),0) AS [15TH PagIbig Loan]
                                                , ISNULL(SUM(AA.[30TH PagIbig Loan]),0) AS [30TH PagIbig Loan]
                                                , ISNULL(SUM(AA.[15TH PagIbig Loan]),0) + ISNULL(SUM(AA.[30TH PagIbig Loan]),0) AS [Total PagIbig Loan]

                                                , ISNULL(SUM(AA.[15TH Calamity Loan]),0) AS [15TH Calamity Loan]
                                                , ISNULL(SUM(AA.[30TH Calamity Loan]),0) AS [30TH Calamity Loan]
                                                , ISNULL(SUM(AA.[15TH Calamity Loan]),0) + ISNULL(SUM(AA.[30TH Calamity Loan]),0) AS [Total Calamity Loan]

                                                , ISNULL(SUM(AA.[15TH Pagibig Mod II Loan]),0) AS [15TH Pagibig Mod II Loan]
                                                , ISNULL(SUM(AA.[30TH Pagibig Mod II Loan]),0) AS [30TH Pagibig Mod II Loan]
                                                , ISNULL(SUM(AA.[15TH Pagibig Mod II Loan]),0) + ISNULL(SUM(AA.[30TH Pagibig Mod II Loan]),0) AS [Total Pagibig Mod II Loan]
                                                ";

                break;
            default:
                _sqlReportType = @"
                                                , SUM(AA.[SSS Month Cont]) AS [SSS Month Cont]
                                                , SUM(AA.[SSS Employee]) AS [SSS Employee]
                                                , SUM(AA.[SSS Employer]) AS [SSS Employer]
                                                , SUM(AA.[SSS Total Cont]) AS [SSS Total Cont]

                                                , ISNULL(SUM(AA.[15TH SSS Loan]),0) AS [15TH SSS Loan]
                                                , ISNULL(SUM(AA.[30TH SSS Loan]),0) AS [30TH SSS Loan]
                                                , ISNULL(SUM(AA.[15TH SSS Loan]),0) + ISNULL(SUM(AA.[30TH SSS Loan]),0) AS [Total SSS Loan]


                                                , SUM(AA.[PhilHealth Month Cont]) AS [PhilHealth Month Cont]
                                                , SUM(AA.[PhilHealth Employee]) AS [PhilHealth Employee]
                                                , SUM(AA.[PhilHealth Employer]) AS [PhilHealth Employer]
                                                , SUM(AA.[PhilHealth Total Cont]) AS [PhilHealth Total Cont]



                                                , SUM(AA.[PagIbig Month Cont]) AS [PagIbig Month Cont]
                                                , SUM(AA.[PagIbig Employee]) AS [PagIbig Employee]
                                                , SUM(AA.[PagIbig Employer]) AS [PagIbig Employer]
                                                , SUM(AA.[PagIbig Total Cont]) AS [PagIbig Total Cont]

                                                , ISNULL(SUM(AA.[15TH PagIbig Loan]),0) AS [15TH PagIbig Loan]
                                                , ISNULL(SUM(AA.[30TH PagIbig Loan]),0) AS [30TH PagIbig Loan]
                                                , ISNULL(SUM(AA.[15TH PagIbig Loan]),0) + ISNULL(SUM(AA.[30TH PagIbig Loan]),0) AS [Total PagIbig Loan]

                                                , ISNULL(SUM(AA.[15TH Calamity Loan]),0) AS [15TH Calamity Loan]
                                                , ISNULL(SUM(AA.[30TH Calamity Loan]),0) AS [30TH Calamity Loan]
                                                , ISNULL(SUM(AA.[15TH Calamity Loan]),0) + ISNULL(SUM(AA.[30TH Calamity Loan]),0) AS [Total Calamity Loan]

                                                , ISNULL(SUM(AA.[15TH Pagibig Mod II Loan]),0) AS [15TH Pagibig Mod II Loan]
                                                , ISNULL(SUM(AA.[30TH Pagibig Mod II Loan]),0) AS [30TH Pagibig Mod II Loan]
                                                , ISNULL(SUM(AA.[15TH Pagibig Mod II Loan]),0) + ISNULL(SUM(AA.[30TH Pagibig Mod II Loan]),0) AS [Total Pagibig Mod II Loan]

                                                , SUM(AA.[WTax Month Cont]) AS [WTax Month Cont]
                                                , SUM(AA.[Total Tax]) AS [Total Tax]";

                break;
        }

        string _sqlRemittance = @"
                                                SELECT
                                                AA.EmployeeNo
                                                , AA.EmployeeName
                                                , AA.BName
                                                , LEFT(AA.SSSNo,2) + '-'  + SUBSTRING(AA.SSSNo,3,7) + '-' + RIGHT(AA.SSSNo,1) AS [SSS No] " 
                                                
                                                +
                                                _sqlReportType
                                                + @"




                                                FROM (


			                                                SELECT 
			                                                  B.EmployeeNo
			                                                , B.EmployeeName
                                                            , B.AsBranchCode
                                                            , C.BCode
			                                                , (SELECT DISTINCT Z.BName FROM vwsDepartmentList Z WHERE Z.BCode = (CASE WHEN ISNULL(B.AsBranchCode,'') = '' THEN ISNULL(C.BCode,'') ELSE ISNULL(B.AsBranchCode,'') END)) AS BName 
			                                                , B.SSSNo
			                                                , A.SSSEmployee AS [SSS Month Cont]
			                                                , A.SSSEmployee AS [SSS Employee]
			                                                , A.SSSEmployer + A.SSSEC AS [SSS Employer]
			                                                , A.SSSEmployee + A.SSSEmployer + A.SSSEC AS [SSS Total Cont]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'A' THEN ISNULL(A.SSSLoan,0) ELSE 0 END AS [15TH SSS Loan]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'B' THEN ISNULL(A.SSSLoan,0) ELSE 0 END AS [30TH SSS Loan]


			                                                , A.PhilHealthEmployee AS [PhilHealth Month Cont]
			                                                , A.PhilHealthEmployee AS [PhilHealth Employee]
			                                                , A.PhilHealthEmployer AS [PhilHealth Employer]
			                                                , A.PhilHealthEmployee + A.PhilHealthEmployer AS [PhilHealth Total Cont]


			                                                , A.PagIbigEmployee AS [PagIbig Month Cont]
			                                                , A.PagIbigEmployee AS [PagIbig Employee]
			                                                , A.PagIbigEmployer AS [PagIbig Employer]
			                                                , A.PagIbigEmployee + A.PagIbigEmployer AS [PagIbig Total Cont]

			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'A' THEN ISNULL(A.PagibigLoan,0) - ISNULL((SELECT ISNULL(SUM(Z.Amount),0) AS Amount FROM vwsPayrollDetails Z WHERE Z.AccountCode IN ('8-522') AND Z.EmployeeNo = A.EmployeeNo AND Z.PayrollPeriod = A.PayrollPeriod), 0) ELSE 0 END AS [15TH Pagibig Loan]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'B' THEN ISNULL(A.PagibigLoan,0) - ISNULL((SELECT ISNULL(SUM(Z.Amount),0) AS Amount FROM vwsPayrollDetails Z WHERE Z.AccountCode IN ('8-522') AND Z.EmployeeNo = A.EmployeeNo AND Z.PayrollPeriod = A.PayrollPeriod), 0) ELSE 0 END AS [30TH Pagibig Loan]

			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'A' THEN ISNULL(A.CalamityLoan,0) ELSE 0 END AS [15TH Calamity Loan]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'B' THEN ISNULL(A.CalamityLoan,0) ELSE 0 END AS [30TH Calamity Loan]

			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'A' THEN (SELECT ISNULL(SUM(Z.Amount),0) AS Amount FROM vwsPayrollDetails Z WHERE Z.AccountCode IN ('8-521') AND Z.EmployeeNo = A.EmployeeNo AND Z.PayrollPeriod = A.PayrollPeriod) ELSE 0 END AS [15TH Pagibig Mod II Loan]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'B' THEN (SELECT ISNULL(SUM(Z.Amount),0) AS Amount FROM vwsPayrollDetails Z WHERE Z.AccountCode IN ('8-521') AND Z.EmployeeNo = A.EmployeeNo AND Z.PayrollPeriod = A.PayrollPeriod) ELSE 0 END AS [30TH Pagibig Mod II Loan]


			                                                , A.WitholdingTax AS [WTax Month Cont]
			                                                , A.WitholdingTax AS [Total Tax]

			                                                FROM vwsPayrollHeader A 
				                                                INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
				                                                INNER JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode
                                                            WHERE B.Company Like '%" + _Company + @"%' AND LEFT(A.[PayrollPeriod],7) = '" + _PayrollPeriod+ @"'
                                                                        AND A.PayrollType = 'PAYROLL'
	                                                ) AA

                                                GROUP BY  AA.BName, AA.EmployeeName, AA.EmployeeNo
                                                , AA.SSSNo
                                        ";

        return _sqlRemittance;
    }
    
    private static string GovRemittanceBranch(string _PayrollPeriod, string _Company = "", string _ReportType = "")
    {



        string _sqlReportType = "";

        switch (_ReportType)
        {
            case "WTAX":
                _sqlReportType = @"
                                                , SUM(AA.[WTax Month Cont]) AS [WTax Month Cont]
                                                , SUM(AA.[Total Tax]) AS [Total Tax]
                                                ";
                break;
            case "SSS":
                _sqlReportType = @"
                                                , SUM(AA.[SSS Month Cont]) AS [SSS Month Cont]
                                                , SUM(AA.[SSS Employee]) AS [SSS Employee]
                                                , SUM(AA.[SSS Employer]) AS [SSS Employer]
                                                , SUM(AA.[SSS Total Cont]) AS [SSS Total Cont]

                                                , ISNULL(SUM(AA.[15TH SSS Loan]),0) AS [15TH SSS Loan]
                                                , ISNULL(SUM(AA.[30TH SSS Loan]),0) AS [30TH SSS Loan]
                                                , ISNULL(SUM(AA.[15TH SSS Loan]),0) + ISNULL(SUM(AA.[30TH SSS Loan]),0) AS [Total SSS Loan]

                                                , SUM(AA.[PhilHealth Month Cont]) AS [PhilHealth Month Cont]
                                                , SUM(AA.[PhilHealth Employee]) AS [PhilHealth Employee]
                                                , SUM(AA.[PhilHealth Employer]) AS [PhilHealth Employer]
                                                , SUM(AA.[PhilHealth Total Cont]) AS [PhilHealth Total Cont]
                                                ";

                break;
            case "PAGIBIG":
                _sqlReportType = @"
                                                , SUM(AA.[PagIbig Month Cont]) AS [PagIbig Month Cont]
                                                , SUM(AA.[PagIbig Employee]) AS [PagIbig Employee]
                                                , SUM(AA.[PagIbig Employer]) AS [PagIbig Employer]
                                                , SUM(AA.[PagIbig Total Cont]) AS [PagIbig Total Cont]

                                                , ISNULL(SUM(AA.[15TH PagIbig Loan]),0) AS [15TH PagIbig Loan]
                                                , ISNULL(SUM(AA.[30TH PagIbig Loan]),0) AS [30TH PagIbig Loan]
                                                , ISNULL(SUM(AA.[15TH PagIbig Loan]),0) + ISNULL(SUM(AA.[30TH PagIbig Loan]),0) AS [Total PagIbig Loan]

                                                , ISNULL(SUM(AA.[15TH Calamity Loan]),0) AS [15TH Calamity Loan]
                                                , ISNULL(SUM(AA.[30TH Calamity Loan]),0) AS [30TH Calamity Loan]
                                                , ISNULL(SUM(AA.[15TH Calamity Loan]),0) + ISNULL(SUM(AA.[30TH Calamity Loan]),0) AS [Total Calamity Loan]

                                                , ISNULL(SUM(AA.[15TH Pagibig Mod II Loan]),0) AS [15TH Pagibig Mod II Loan]
                                                , ISNULL(SUM(AA.[30TH Pagibig Mod II Loan]),0) AS [30TH Pagibig Mod II Loan]
                                                , ISNULL(SUM(AA.[15TH Pagibig Mod II Loan]),0) + ISNULL(SUM(AA.[30TH Pagibig Mod II Loan]),0) AS [Total Pagibig Mod II Loan]
                                                ";

                break;
            default:
                _sqlReportType = @"
                                                , SUM(AA.[SSS Month Cont]) AS [SSS Month Cont]
                                                , SUM(AA.[SSS Employee]) AS [SSS Employee]
                                                , SUM(AA.[SSS Employer]) AS [SSS Employer]
                                                , SUM(AA.[SSS Total Cont]) AS [SSS Total Cont]

                                                , ISNULL(SUM(AA.[15TH SSS Loan]),0) AS [15TH SSS Loan]
                                                , ISNULL(SUM(AA.[30TH SSS Loan]),0) AS [30TH SSS Loan]
                                                , ISNULL(SUM(AA.[15TH SSS Loan]),0) + ISNULL(SUM(AA.[30TH SSS Loan]),0) AS [Total SSS Loan]


                                                , SUM(AA.[PhilHealth Month Cont]) AS [PhilHealth Month Cont]
                                                , SUM(AA.[PhilHealth Employee]) AS [PhilHealth Employee]
                                                , SUM(AA.[PhilHealth Employer]) AS [PhilHealth Employer]
                                                , SUM(AA.[PhilHealth Total Cont]) AS [PhilHealth Total Cont]



                                                , SUM(AA.[PagIbig Month Cont]) AS [PagIbig Month Cont]
                                                , SUM(AA.[PagIbig Employee]) AS [PagIbig Employee]
                                                , SUM(AA.[PagIbig Employer]) AS [PagIbig Employer]
                                                , SUM(AA.[PagIbig Total Cont]) AS [PagIbig Total Cont]

                                                , ISNULL(SUM(AA.[15TH PagIbig Loan]),0) AS [15TH PagIbig Loan]
                                                , ISNULL(SUM(AA.[30TH PagIbig Loan]),0) AS [30TH PagIbig Loan]
                                                , ISNULL(SUM(AA.[15TH PagIbig Loan]),0) + ISNULL(SUM(AA.[30TH PagIbig Loan]),0) AS [Total PagIbig Loan]

                                                , ISNULL(SUM(AA.[15TH Calamity Loan]),0) AS [15TH Calamity Loan]
                                                , ISNULL(SUM(AA.[30TH Calamity Loan]),0) AS [30TH Calamity Loan]
                                                , ISNULL(SUM(AA.[15TH Calamity Loan]),0) + ISNULL(SUM(AA.[30TH Calamity Loan]),0) AS [Total Calamity Loan]

                                                , ISNULL(SUM(AA.[15TH Pagibig Mod II Loan]),0) AS [15TH Pagibig Mod II Loan]
                                                , ISNULL(SUM(AA.[30TH Pagibig Mod II Loan]),0) AS [30TH Pagibig Mod II Loan]
                                                , ISNULL(SUM(AA.[15TH Pagibig Mod II Loan]),0) + ISNULL(SUM(AA.[30TH Pagibig Mod II Loan]),0) AS [Total Pagibig Mod II Loan]

                                                , SUM(AA.[WTax Month Cont]) AS [WTax Month Cont]
                                                , SUM(AA.[Total Tax]) AS [Total Tax]";

                break;
        }


        string _sqlRemittance = @"
                                                SELECT
                                                 ''
                                                , ''
                                                , AA.BName
                                                , ''"

                                                +
                                                _sqlReportType
                                                + @"




                                                FROM (


			                                                SELECT 
			                                                    B.EmployeeNo
			                                                , B.EmployeeName
                                                            , B.AsBranchCode
                                                            , C.BCode
			                                                , (SELECT DISTINCT Z.BName FROM vwsDepartmentList Z WHERE Z.BCode = (CASE WHEN ISNULL(B.AsBranchCode,'') = '' THEN ISNULL(C.BCode,'') ELSE ISNULL(B.AsBranchCode,'') END)) AS BName 
			                                                , B.SSSNo
			                                                , A.SSSEmployee AS [SSS Month Cont]
			                                                , A.SSSEmployee AS [SSS Employee]
			                                                , A.SSSEmployer + A.SSSEC AS [SSS Employer]
			                                                , A.SSSEmployee + A.SSSEmployer + A.SSSEC AS [SSS Total Cont]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'A' THEN ISNULL(A.SSSLoan,0) ELSE 0 END AS [15TH SSS Loan]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'B' THEN ISNULL(A.SSSLoan,0) ELSE 0 END AS [30TH SSS Loan]


			                                                , A.PhilHealthEmployee AS [PhilHealth Month Cont]
			                                                , A.PhilHealthEmployee AS [PhilHealth Employee]
			                                                , A.PhilHealthEmployer AS [PhilHealth Employer]
			                                                , A.PhilHealthEmployee + A.PhilHealthEmployer AS [PhilHealth Total Cont]


			                                                , A.PagIbigEmployee AS [PagIbig Month Cont]
			                                                , A.PagIbigEmployee AS [PagIbig Employee]
			                                                , A.PagIbigEmployer AS [PagIbig Employer]
			                                                , A.PagIbigEmployee + A.PagIbigEmployer AS [PagIbig Total Cont]

			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'A' THEN ISNULL(A.PagibigLoan,0) ELSE 0 END AS [15TH Pagibig Loan]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'B' THEN ISNULL(A.PagibigLoan,0) ELSE 0 END AS [30TH Pagibig Loan]

			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'A' THEN ISNULL(A.CalamityLoan,0) ELSE 0 END AS [15TH Calamity Loan]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'B' THEN ISNULL(A.CalamityLoan,0) ELSE 0 END AS [30TH Calamity Loan]


			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'A' THEN (SELECT ISNULL(SUM(Z.Amount),0) AS Amount FROM vwsPayrollDetails Z WHERE Z.AccountCode IN ('8-521') AND Z.EmployeeNo = A.EmployeeNo AND Z.PayrollPeriod = A.PayrollPeriod) ELSE 0 END AS [15TH Pagibig Mod II Loan]
			                                                , CASE WHEN RIGHT(A.PayrollPeriod, 1) = 'B' THEN (SELECT ISNULL(SUM(Z.Amount),0) AS Amount FROM vwsPayrollDetails Z WHERE Z.AccountCode IN ('8-521') AND Z.EmployeeNo = A.EmployeeNo AND Z.PayrollPeriod = A.PayrollPeriod) ELSE 0 END AS [30TH Pagibig Mod II Loan]


			                                                , A.WitholdingTax AS [WTax Month Cont]
			                                                , A.WitholdingTax AS [Total Tax]

			                                                FROM vwsPayrollHeader A 
				                                                INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
				                                                INNER JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode
                                                            WHERE B.Company Like '%" + _Company + @"%' AND LEFT(A.[PayrollPeriod],7) = '" + _PayrollPeriod + @"'
                                                                        AND A.PayrollType = 'PAYROLL'
	                                                ) AA

                                                GROUP BY  AA.BName
                                        ";

        return _sqlRemittance;
    }


    private void btnGenerate_Click(object sender, EventArgs e)
    {

        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please select payroll period");
            return;
        }

        string _BCompany = "";
        if (cboCompany.Text == "")
        {
            _BCompany = "";
        }
        else
        {
            _BCompany = cboCompany.Text.Substring(0, 4);
        }



        string _sqlRemittance;
        _sqlRemittance = GovRemittanceDetails(cboPayrolPeriod.Text,_BCompany, cboGoverment.Text);
        _DataListRemittance = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlRemittance);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataListRemittance);


        string _sqlRemittanceBranch;
        _sqlRemittanceBranch = GovRemittanceBranch(cboPayrolPeriod.Text, _BCompany, cboGoverment.Text);
        _DataListRemittanceBranch = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlRemittanceBranch);
        

        MessageBox.Show("Data Generated Successfully");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }


    private void ExcelSummary()
    {

    }



    private void btnUpload_Click(object sender, EventArgs e)
    {
        string _MonthName = "";
        switch (cboPayrolPeriod.Text.Substring(5, 2))
        {
            case "01":
                _MonthName = "January";
                break;
            case "02":
                _MonthName = "February";
                break;
            case "03":
                _MonthName = "March";
                break;
            case "04":
                _MonthName = "April";
                break;
            case "05":
                _MonthName = "May";
                break;
            case "06":
                _MonthName = "June";
                break;
            case "07":
                _MonthName = "July";
                break;
            case "018":
                _MonthName = "August";
                break;
            case "09":
                _MonthName = "September";
                break;
            case "10":
                _MonthName = "October";
                break;
            case "11":
                _MonthName = "November";
                break;
            case "12":
                _MonthName = "December";
                break;
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
        xlWorkSheet.Name = "Summary";


        int i = 0;
        int j = 0;

        i = 5;
        for (j = 1; j <= _DataListRemittance.Columns.Count; j++)
        {
            xlWorkSheet.Cells[i, j] = _DataListRemittance.Columns[j - 1].ColumnName;
        }

        double _RowCount;
        int _Count = 0;
        _RowCount = _DataListRemittance.Rows.Count;
        
        foreach (DataRow row in _DataListRemittance.Rows)
        {

            string _EmployeeName = row[1].ToString();

            for (j = 0; j <= _DataListRemittance.Columns.Count - 1; j++)
            {
                xlWorkSheet.Cells[i + 1, j + 1] = row[j].ToString();
            }

            i++;
            Application.DoEvents();
            _Count++;
  
            //frmMainWindow frmMainWindow = new frmMainWindow();
            tssDataStatus.Text = "Payroll Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }

        int[] arr;

        switch (cboGoverment.Text)
        {
            case "WTAX":
                arr = new int[2] { 5, 6 };
                break;
            case "SSS":
                arr = new int[11] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                break;
            case "PAGIBIG":
                arr = new int[13] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                break;
            default:
                arr = new int[26] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
                break;
        }

        //int[] arr = new int[26] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        Excel.Range range;
        range = xlWorkSheet.UsedRange;

        xlApp.Range["E6:AD" + i].Select();
        range.Subtotal(3, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum, arr, true, false, Microsoft.Office.Interop.Excel.XlSummaryRow.xlSummaryBelow);

        xlApp.Columns["E:AD"].Select();
        xlApp.Selection.Style = "Comma";

        range.Activate();
        //   xlApp.Selection.Subtotal GroupBy:= 3, Function:= xlSum, TotalList:= Array(5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30), Replace:=True, PageBreaks:= True, SummaryBelowData:= True
        //ActiveWindow.SmallScroll Down:= 15


        int rCount = xlWorkSheet.UsedRange.Rows.Count + 5;

        i =  rCount;
        _Count = 0;
        _RowCount = _DataListRemittanceBranch.Rows.Count;

        foreach (DataRow row in _DataListRemittanceBranch.Rows)
        {

            string _EmployeeName = row[1].ToString();

            for (j = 0; j <= _DataListRemittanceBranch.Columns.Count - 1; j++)
            {
                xlWorkSheet.Cells[i + 1, j + 1] = row[j].ToString();
            }

            i++;
            Application.DoEvents();
            _Count++;

            //frmMainWindow frmMainWindow = new frmMainWindow();
            tssDataStatus.Text = "Payroll Data Processing: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }

        xlApp.Range["E" + (i + 1)].Select();
        xlApp.ActiveCell.Formula = "=SUM(E" + (rCount + 1) + ":E" + (i) + ")";
        xlApp.Selection.Font.Bold = true;

        xlApp.Selection.Copy();
        xlApp.Range["F" + (i + 1) + ":AD" + (i + 1) + ""].Select();
        xlApp.ActiveSheet.Paste();


        string _BCompany = "";
        if (cboCompany.Text == "")
        {
            _BCompany = "";
        }
        else
        {
            _BCompany = cboCompany.Text.Substring(0, 4);
        }

        switch (_BCompany)
        {
            case "DESI":
                xlWorkSheet.Cells[1, 1] = "Du Ek Sam Inc.,";
                break;
            case "DESM":
                xlWorkSheet.Cells[1, 1] = "DES Marketing";
                break;
            default:
                xlWorkSheet.Cells[1, 1] = "Du Ek Sam Inc., / DES Marketing";
                break;
        }
        
        switch (cboGoverment.Text)
        {
            case "WTAX":
                xlWorkSheet.Cells[2, 1] = "Summary of Withholding Tax";
                break;
            case "SSS":
                xlWorkSheet.Cells[2, 1] = "SSS/Philhealth Remittance";
                break;
            case "PAGIBIG":
                xlWorkSheet.Cells[2, 1] = "Pagibig Remittance";
                break;
            default:
                xlWorkSheet.Cells[2, 1] = "Summary of Remittance";
                break;
        }

       
        xlWorkSheet.Cells[3, 1] = "For The Month of " + _MonthName + ", " + cboPayrolPeriod.Text.Substring(0,4) ;






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
