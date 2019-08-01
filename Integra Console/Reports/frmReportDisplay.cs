using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Windows.Forms;

using CrystalDecisions.Shared;


using System.Configuration;
using System.Data.OleDb;

public partial class frmReportDisplay : Form
{
    public static string _RequestType;
    public static string _QueryGenerated;
    public static string _AdjustmentType;
    public frmReportDisplay()
    {
        InitializeComponent();
    }

    private void frmReportDisplay_Load(object sender, EventArgs e)
    {
        //ReportDocument cryRpt = new ReportDocument();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;


        //crConnectionInfo.ServerName = clsDeclaration.sServer;
        //crConnectionInfo.DatabaseName = clsDeclaration.sCompany;
        //crConnectionInfo.UserID = clsDeclaration.sUsername;
        //crConnectionInfo.Password = clsDeclaration.sPassword;

        crConnectionInfo.ServerName = ConfigurationManager.AppSettings["DBServer"];
        crConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["DBName"];
        crConnectionInfo.UserID = ConfigurationManager.AppSettings["DBUsername"];
        crConnectionInfo.Password = ConfigurationManager.AppSettings["DBPassword"];


        switch (clsDeclaration.sReportTag)
        {
            case 20:
                //frmPayrollRegParameter frmPayrollRegParameter = new frmPayrollRegParameter();
                //frmPayrollRegParameter._RequestType = _RequestType;
                //frmPayrollRegParameter.ShowDialog();
                //_AdjustmentType = frmPayrollRegParameter._AdjustmentType;

                //if (clsDeclaration.bView == false)
                //{
                //    return;
                //}

                break;
            case 21:
                //frmPaySlipParameter frmPaySlipParameter = new frmPaySlipParameter();
                ////frmPaySlipParameter._RequestType = _RequestType;
                //frmPaySlipParameter.ShowDialog();
                //_QueryGenerated = frmPaySlipParameter._QueryGenerated;


                //if (clsDeclaration.bView == false)
                //{
                //    return;
                //}

                break;
            case 22:
                frmPerBranchParameter frmPerBranchParameter = new frmPerBranchParameter();
                //frmPaySlipParameter._RequestType = _RequestType;
                frmPerBranchParameter.ShowDialog();
                _QueryGenerated = frmPerBranchParameter._QueryGenerated;


                if (clsDeclaration.bView == false)
                {
                    return;
                }

                break;
            case 23:
                frmEmployeeNoParameter frmEmployeeNoParameter = new frmEmployeeNoParameter();
                //frmPaySlipParameter._RequestType = _RequestType;
                frmEmployeeNoParameter.ShowDialog();

                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\QuitClaims.rpt";

                if (clsDeclaration.bView == false)
                {
                    return;
                }

                break;
            case 24:
                frmAccountByRangeParameter frmAccountByRangeParameter = new frmAccountByRangeParameter();
                //frmPaySlipParameter._RequestType = _RequestType;
                frmAccountByRangeParameter.ShowDialog();

                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Loan Summary.rpt";

                if (clsDeclaration.bView == false)
                {
                    return;
                }

                break;

            case 25:
                frmEmployeeListParameter frmEmployeeListParameter = new frmEmployeeListParameter();
                frmEmployeeListParameter.ShowDialog();

                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\List of Employees.rpt";

                if (clsDeclaration.bView == false)
                {
                    return;
                }

                break;
        }



        string _Path = clsDeclaration.sReportPath;
        cryRpt.Load(_Path, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault);

        // Passing SQL Data Via C# Datatable
        string _ConString = clsDeclaration.sSystemConnection;
        // Passing tru Data Table
        DataTable _tblCrystal = new DataTable();


        // Defining the sql syntax for the Crystal Report
        string _sqlCrystal = "";
        switch (_RequestType)
        {
            case "GrossPay":
                _sqlCrystal = sqlGrossPayComputation(clsDeclaration.sBranch,
                                                clsDeclaration.sPayrollPeriod,
                                                clsDeclaration.sConfiLevel,
                                                clsDeclaration.sConfiLevelSelection,
                                                clsDeclaration.sFilterByEmployee,
                                                clsDeclaration.sEmployeeFrom,
                                                clsDeclaration.sEmployeeTo);

                _tblCrystal = clsSQLClientFunctions.DataList(_ConString, _sqlCrystal);
                break;

            case "Register":
                _sqlCrystal = sqlPayrollRegisterOutput(clsDeclaration.sReportDispaly,
                                                clsDeclaration.sBranch,
                                                clsDeclaration.sPayrollPeriod,
                                                clsDeclaration.sConfiLevel,
                                                clsDeclaration.sConfiLevelSelection,
                                                clsDeclaration.sFilterByEmployee,
                                                clsDeclaration.sEmployeeFrom,
                                                clsDeclaration.sEmployeeTo,
                                                clsDeclaration.sReportTitle);

                _tblCrystal = clsSQLClientFunctions.DataList(_ConString, _sqlCrystal);
                break;
            case "Adjustment":
                switch (_AdjustmentType)
                {
                    case "Deduction":
                        _sqlCrystal = sqlPayrollAdjustmentOutput(clsDeclaration.sReportDispaly,
                                clsDeclaration.sBranch,
                                clsDeclaration.sPayrollPeriod,
                                clsDeclaration.sConfiLevel,
                                clsDeclaration.sConfiLevelSelection,
                                clsDeclaration.sFilterByEmployee,
                                clsDeclaration.sEmployeeFrom,
                                clsDeclaration.sEmployeeTo,
                                clsDeclaration.sReportTitle);

                        _tblCrystal = clsSQLClientFunctions.DataList(_ConString, _sqlCrystal);
                        break;
                    case "Income":

                        _sqlCrystal = sqlPayrollAdjustmentIncomeOutput(clsDeclaration.sReportDispaly,
                                clsDeclaration.sBranch,
                                clsDeclaration.sPayrollPeriod,
                                clsDeclaration.sConfiLevel,
                                clsDeclaration.sConfiLevelSelection,
                                clsDeclaration.sFilterByEmployee,
                                clsDeclaration.sEmployeeFrom,
                                clsDeclaration.sEmployeeTo,
                                clsDeclaration.sReportTitle);

                        _tblCrystal = clsSQLClientFunctions.DataList(_ConString, _sqlCrystal);
                        break;
                }


                break;
            case "Payslip":
                _sqlCrystal = _QueryGenerated;

                _tblCrystal = clsSQLClientFunctions.DataList(_ConString, _sqlCrystal);
                break;
            case "LoanLedger":
                _sqlCrystal = _QueryGenerated;

                _tblCrystal = clsSQLClientFunctions.DataList(_ConString, _sqlCrystal);
                break;
        }
        

        CrTables = cryRpt.Database.Tables;
        foreach (Table CrTable in CrTables)
        {
            crtableLogoninfo = CrTable.LogOnInfo;
            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
            CrTable.ApplyLogOnInfo(crtableLogoninfo);

            //CrTable.Location.Substring(CrTable.Location.LastIndexOf(".") + 1);

            //CrTable.SetDataSource(_tblCrystal);
        }


        if (_RequestType != "OverDeduct" && _RequestType != "DetiledPayslip")
        {
            cryRpt.SetDataSource(_tblCrystal);
        }
        cryRpt.Refresh();

        ParameterFieldDefinitions crParameterFieldDefinitions;
        ParameterFieldDefinition crParameterFieldDefinition;

        ParameterValues crParameterValues = new ParameterValues();
        ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

        switch (clsDeclaration.sReportTag)
        {
            case 25:
                crParameterDiscreteValue.Value = clsDeclaration.sBranch;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["Branch"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                //crParameterDiscreteValue.Value = clsDeclaration.sEmpStatus;
                //crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                //crParameterFieldDefinition = crParameterFieldDefinitions["EmpStatus"];
                //crParameterValues = crParameterFieldDefinition.CurrentValues;

                //crParameterValues.Clear();
                //crParameterValues.Add(crParameterDiscreteValue);
                //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                break;
            case 26:

                //cryRpt.SetParameterValue("EmployeeNo", clsDeclaration.sEmployeeNo, "Other Income");
                //cryRpt.SetParameterValue("EmployeeNo", clsDeclaration.sEmployeeNo, "Deduction Details");
                //cryRpt.SetParameterValue("PayrollPeriod", clsDeclaration.sPayrollPeriod, "Other Income");
                //cryRpt.SetParameterValue("PayrollPeriod", clsDeclaration.sPayrollPeriod, "Deduction Details");


                crParameterDiscreteValue.Value = clsDeclaration.sEmployeeNo;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["EmployeeNo"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                crParameterDiscreteValue.Value = clsDeclaration.sPayrollPeriod;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["PayrollPeriod"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);


                break;
        }



        //if (clsDeclaration.sReportTag == 20)
        //{


        //    crParameterDiscreteValue.Value = clsDeclaration.sBranch;
        //    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //    crParameterFieldDefinition = crParameterFieldDefinitions["Branch"];
        //    crParameterValues = crParameterFieldDefinition.CurrentValues;

        //    crParameterValues.Clear();
        //    crParameterValues.Add(crParameterDiscreteValue);
        //    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);



        //    crParameterDiscreteValue.Value = clsDeclaration.sPayrollPeriod;
        //    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //    crParameterFieldDefinition = crParameterFieldDefinitions["PayrollPeriod"];
        //    crParameterValues = crParameterFieldDefinition.CurrentValues;

        //    crParameterValues.Clear();
        //    crParameterValues.Add(crParameterDiscreteValue);
        //    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);



        //    crParameterDiscreteValue.Value = clsDeclaration.sConfiLevel;
        //    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //    crParameterFieldDefinition = crParameterFieldDefinitions["ConfiLevel"];
        //    crParameterValues = crParameterFieldDefinition.CurrentValues;

        //    crParameterValues.Clear();
        //    crParameterValues.Add(crParameterDiscreteValue);
        //    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
        //}

        crystalReportViewer1.Refresh();
        crystalReportViewer1.ReportSource = cryRpt;
    }


    private static string sqlGrossPayComputation(string _refBranch = "",
        string _refPayrollPeriod = "", string _refConfiLevel = "", string _refConfiSelection = "", bool _refFilterByEmployee = false,
        string _refEmployeeFrom = "", string _refEmployeeTo = "")
    {




        string _sqlSyntax = "";
        _sqlSyntax = @" 

            SELECT * FROM (
            SELECT 
            YY.EmployeeNo, YY.EmployeeName, YY.TaxStatus, XX.Description, YY.BranchName , YY.TaxIDNo, YY.BCode, XX.PayrollPeriod, YY.ConfiLevel,
            MAX(LEFT(XX.PayrollPeriod,4)) AS YEAR,
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '01-A' THEN XX.Amount ELSE 0 END) AS [JAN A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '01-B' THEN XX.Amount ELSE 0 END) AS [JAN B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '02-A' THEN XX.Amount ELSE 0 END) AS [FEB A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '02-B' THEN XX.Amount ELSE 0 END) AS [FEB B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '03-A' THEN XX.Amount ELSE 0 END) AS [MAR A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '03-B' THEN XX.Amount ELSE 0 END) AS [MAR B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '04-A' THEN XX.Amount ELSE 0 END) AS [APR A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '04-B' THEN XX.Amount ELSE 0 END) AS [APR B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '05-A' THEN XX.Amount ELSE 0 END) AS [MAY A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '05-B' THEN XX.Amount ELSE 0 END) AS [MAY B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '06-A' THEN XX.Amount ELSE 0 END) AS [JUN A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '06-B' THEN XX.Amount ELSE 0 END) AS [JUN B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '07-A' THEN XX.Amount ELSE 0 END) AS [JUL A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '07-B' THEN XX.Amount ELSE 0 END) AS [JUL B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '08-A' THEN XX.Amount ELSE 0 END) AS [AUG A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '08-B' THEN XX.Amount ELSE 0 END) AS [AUG B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '09-A' THEN XX.Amount ELSE 0 END) AS [SEP A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '09-B' THEN XX.Amount ELSE 0 END) AS [SEP B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '10-A' THEN XX.Amount ELSE 0 END) AS [OCT A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '10-B' THEN XX.Amount ELSE 0 END) AS [OCT B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '11-A' THEN XX.Amount ELSE 0 END) AS [NOV A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '11-B' THEN XX.Amount ELSE 0 END) AS [NOV B],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '12-A' THEN XX.Amount ELSE 0 END) AS [DEC A],
            SUM(CASE WHEN  RIGHT(XX.PayrollPeriod,4) = '12-B' THEN XX.Amount ELSE 0 END) AS [DEC B],
            SUM(XX.Amount) AS [Amount],
            SUM(CASE WHEN  XX.Description IN ('SSS Employee','PhilHealth Employee','PagIbig Employee')  THEN XX.Amount ELSE 0 END) AS [Deduction],
            SUM(CASE WHEN  XX.Description IN ('Gross Pay')  THEN XX.Amount ELSE 0 END) AS [Gross Pay]

            FROM (

            SELECT A.PayrollPeriod, A.EmployeeNo , A.BasicPay 

            - (SELECT ISNULL(SUM(Y.Amount),0) AS [Leave No Pay] 
            FROM [vwsPayrollDetails] Y WHERE Y.EmployeeNo = A.EmployeeNo  AND Y.PayrollPeriod = A.PayrollPeriod AND Y.AccountCode IN ('9-528'))

            AS Amount , 'Gross Pay' AS Description , 1 AS [ID], A.Department FROM vwsPayrollHeader A
            UNION ALL

            SELECT A.PayrollPeriod, A.EmployeeNo , A.SSSEmployee , 'SSS Employee', 2 AS [ID], A.Department FROM vwsPayrollHeader A
            UNION ALL

            SELECT A.PayrollPeriod, A.EmployeeNo , A.PhilHealthEmployee , 'PhilHealth Employee', 3 AS [ID], A.Department FROM vwsPayrollHeader A
            UNION ALL

            SELECT A.PayrollPeriod, A.EmployeeNo , A.PagIbigEmployee , 'PagIbig Employee', 4 AS [ID], A.Department FROM vwsPayrollHeader A
            UNION ALL

            SELECT A.PayrollPeriod, A.EmployeeNo , A.WitholdingTax , 'Witholding Tax', 5 AS [ID], A.Department FROM vwsPayrollHeader A

            ) XX INNER JOIN [vwsEmployeeDetails] YY ON XX.EmployeeNo = YY.EmployeeNo 
            WHERE  YY.ConfiLevel IN (" + _refConfiLevel + @") 
        GROUP BY YY.EmployeeNo, YY.EmployeeName, YY.TaxStatus, XX.Description , YY.BranchName , YY.TaxIDNo , YY.BCode, XX.PayrollPeriod, YY.ConfiLevel

		) QQ 


        WHERE LEFT(QQ.PayrollPeriod,4) = LEFT( '" + _refPayrollPeriod + @"',4) AND QQ.BCode = '" + _refBranch + @"'
        AND QQ.ConfiLevel IN (" + _refConfiSelection + @") 
        AND QQ.PayrollPeriod <=  '" + _refPayrollPeriod + @"'";

        if (_refFilterByEmployee == true)
        {
            _sqlSyntax = _sqlSyntax + @"
                                        AND QQ.EmployeeNo Between '" + _refEmployeeFrom + @"' AND '" + _refEmployeeTo + @"'
                                                                                ";
        }

        return _sqlSyntax;
    }


    private static string sqlPayrollRegisterOutput(string _refType = "", string _refBranch = "", 
        string _refPayrollPeriod = "", string _refConfiLevel = "", 
        string _refConfiSelection = "", bool _refFilterByEmployee = false,
        string _refEmployeeFrom = "", string _refEmployeeTo = "", string _ReprotTitle = "")
    {


        switch (_refType)
        {
            case "Regular":
                _ReprotTitle = _ReprotTitle.ToUpper();
                break;
            case "LastPay":
                _ReprotTitle = _ReprotTitle.ToUpper();
                break;
        }



        string _sqlSyntax = "";
        
        _sqlSyntax = @"

                DECLARE @Branch NVARCHAR(25)
                DECLARE @PayrollPeriod NVARCHAR(25)
                DECLARE @ByEmployee INT
                DECLARE @EmployeeNoFrom NVARCHAR(25)
                DECLARE @EmployeeNoTo NVARCHAR(25)

                SET @Branch  = '" + _refBranch + @"'
                SET @PayrollPeriod = '" + _refPayrollPeriod + @"'
                SET @EmployeeNoFrom = '" + _refEmployeeFrom + @"'
                SET @EmployeeNoTo = '" + _refEmployeeTo + @"'
      
	                SELECT  * FROM (
		                SELECT 
		                (SELECT Z.CompanyName  FROM OCMP Z WHERE Z.Active = 1 AND Z.CompanyCode = LEFT(@Branch,4)) AS CompanyName,A.EmployeeNo,
		                B.LastName,
		                B.FirstName,
		                A.DailyRate,
		                A.TotalDays AS [Regular Days],

		                A.BasicPay AS [Regular Days PAY],

		                A.BasicPay,
		                A.Gross,
		                A.OTHRs AS [Over Time],
		                A.OTPay AS OTPay,
		                A.SPLHrs + A.SUNHrs AS [SUN/SPL HOL],
		                A.SPLPay + A.SUNPay AS [SUN/SPL HOL PAY],
		                A.LEGHrs AS [LEGAL HOL],
		                A.LEGPay AS [LEGAL HOL PAY],
		                A.OtherIncome AS [OTHER INCOME],


		                A.SSSLoan AS [SSS LOANS],

		                ISNULL(A.PagibigLoan,0) + ISNULL(A.CalamityLoan,0) AS [PAGIBIG LOANS],

		                ISNULL(A.OtherDeduction,0) + ISNULL(A.OtherLoan,0) AS [OTHER DEDUCTION],

		                A.SSSEmployee,
		                A.PhilHealthEmployee,
		                A.PagIbigEmployee,
		                A.WitholdingTax,
		                A.NetPay,
		                C.BranchCode,
		                C.BranchName,
		                CASE WHEN B.ConfiLevel IN ('2') THEN 'Managers' ELSE 'Rank And File' END AS [GroupConfi],
		                CASE WHEN B.Category = 1 THEN 'Daily' ELSE 'Monthly' END AS Category,
		                C.DepName,
		                B.Company,
		                ISNULL(B.COLAAmount, 0) AS COLAAmount,
		                B.EmployeeName,
		                B.DateHired,
		                B.DateFinish,
		                D.DateOne,
		                D.DateTwo,
                        B.ConfiLevel,
                        '" + _ReprotTitle + @"' AS [Report Title],
                        A.PayrollPeriod,
						B.AsBranchCode,
						(SELECT Z.Name FROM OBLP Z WHERE Z.BranchCode = B.AsBranchCode) AS ASBName
                        

		                FROM vwsPayrollHeader A
		                INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
		                LEFT JOIN vwsDepartmentList C ON A.Department = C.DepartmentCode
		                LEFT JOIN vwsPayrollPeriod D ON A.PayrollPeriod = D.PayrollPeriod
		                WHERE A.Branch LIKE @Branch AND A.PayrollPeriod = @PayrollPeriod
		                AND B.ConfiLevel IN (" + _refConfiLevel + @")
                        ";
                        
                        switch (_refType)
                        {
                            case "Regular":
                                _sqlSyntax = _sqlSyntax + @"		AND A.PayrollType = 'PAYROLL'
                                                                                        ";
                                break;
                            case "LastPay":
                                _sqlSyntax = _sqlSyntax + @"		AND A.PayrollType = 'LASTPAY'
                                                                                        ";
                                break;
                        }
        
        _sqlSyntax = _sqlSyntax + @") ZZ
                        WHERE ZZ.ConfiLevel IN (" + _refConfiSelection + @") ";

        if (_refFilterByEmployee == true)
        {
                _sqlSyntax = _sqlSyntax + @"
                        AND ZZ.EmployeeNo Between @EmployeeNoFrom AND @EmployeeNoTo 
                                                                ";
         }
        

        _sqlSyntax = _sqlSyntax +@"
                        ORDER BY ZZ.EmployeeName
                                ";

        return _sqlSyntax;
    }




    private static string sqlPayrollAdjustmentOutput(string _refType = "", string _refBranch = "",
        string _refPayrollPeriod = "", string _refConfiLevel = "",
        string _refConfiSelection = "", bool _refFilterByEmployee = false,
        string _refEmployeeFrom = "", string _refEmployeeTo = "", string _ReprotTitle = "")
    {
         switch (_refType)
        {
            case "Regular":
                _ReprotTitle = _ReprotTitle.ToUpper();
                break;
            case "LastPay":
                _ReprotTitle = _ReprotTitle.ToUpper()  + " FOR LAST PAY";
                break;
        }

        string _sqlSyntax = "";
        
        _sqlSyntax = @"
      
                DECLARE @Branch NVARCHAR(25)
                DECLARE @PayrollPeriod NVARCHAR(25)
                DECLARE @ByEmployee INT
                DECLARE @EmployeeNoFrom NVARCHAR(25)
                DECLARE @EmployeeNoTo NVARCHAR(25)

                SET @Branch  = '" + _refBranch + @"'
                SET @PayrollPeriod = '" + _refPayrollPeriod + @"'
                SET @EmployeeNoFrom = '" + _refEmployeeFrom + @"'
                SET @EmployeeNoTo = '" + _refEmployeeTo + @"'

                SELECT * FROM (

                                                SELECT 
                                                (SELECT Z.CompanyName  FROM OCMP Z WHERE Z.Active = 1 AND Z.CompanyCode = LEFT(@Branch,4)) AS CompanyName
                                                ,A1.PayrollPeriod, A1.EmployeeNo, B.EmployeeName, A1.AccountCode, E.AccountDesc, A1.LoanRefenceNo, A1.Amount
                                                ,(SELECT X1.Balance FROM dbo.[fnGetBalancePerLoan](A1.EmployeeNo, A1.AccountCode, A1.PayrollPeriod, A1.LoanRefenceNo) X1) AS [Balance],
		                                                                C.BranchCode,
		                                                                C.BranchName,
		                                                                CASE WHEN B.ConfiLevel IN ('2') THEN 'Managers' ELSE 'Rank And File' END AS [GroupConfi],
		                                                                CASE WHEN B.Category = 1 THEN 'Daily' ELSE 'Monthly' END AS Category,
		                                                                C.DepName,
		                                                                B.Company,
						                                                D.DateOne,
		                                                                D.DateTwo,
                                                                        B.ConfiLevel,
                                                                        '" + _ReprotTitle + @"' AS [Report Title]

                                                FROM vwsPayrollHeader A INNER JOIN 
	                                                vwsPayrollDetails A1 ON A.EmployeeNo = A1.EmployeeNo AND A.PayrollPeriod = A1.PayrollPeriod LEFT JOIN
	                                                vwsEmployees B ON A.EmployeeNo = B.EmployeeNo LEFT JOIN 
	                                                vwsDepartmentList C ON A.Department = C.DepartmentCode LEFT JOIN 
	                                                vwsPayrollPeriod D ON A.PayrollPeriod = D.PayrollPeriod LEFT JOIN
	                                                vwsAccountCode E ON A1.AccountCode = E.AccountCode
                                                WHERE E.AccountType IN (8,7) 
                                                AND 
                                                B.ConfiLevel IN (" + _refConfiLevel + @") AND
                                                A.PayrollPeriod = @PayrollPeriod AND
                                                A.Branch LIKE '%' + @Branch + '%'      
                        ";

                switch (_refType)
                {
                    case "Regular":
                        _sqlSyntax = _sqlSyntax + @"		AND A.PayrollType = 'PAYROLL'
                                                                                                ";
                        break;
                    case "LastPay":
                        _sqlSyntax = _sqlSyntax + @"		AND A.PayrollType = 'LASTPAY'
                                                                                                ";
                        break;
                }


        _sqlSyntax = _sqlSyntax + @") ZZ
                                WHERE ZZ.ConfiLevel IN (" + _refConfiSelection + @") ";

                if (_refFilterByEmployee == true)
                {
                    _sqlSyntax = _sqlSyntax + @"
                                AND ZZ.EmployeeNo Between @EmployeeNoFrom AND @EmployeeNoTo 
                                                                        ";
                }


                _sqlSyntax = _sqlSyntax + @"
                                ORDER BY ZZ.EmployeeName
                                        ";

        return _sqlSyntax;
    }




    private static string sqlPayrollAdjustmentIncomeOutput(string _refType = "", string _refBranch = "",
        string _refPayrollPeriod = "", string _refConfiLevel = "",
        string _refConfiSelection = "", bool _refFilterByEmployee = false,
        string _refEmployeeFrom = "", string _refEmployeeTo = "", string _ReprotTitle = "")
    {
        switch (_refType)
        {
            case "Regular":
                _ReprotTitle = _ReprotTitle.ToUpper();
                break;
            case "LastPay":
                _ReprotTitle = _ReprotTitle.ToUpper() + " FOR LAST PAY";
                break;
        }

        string _sqlSyntax = "";

        _sqlSyntax = @"
      
                DECLARE @Branch NVARCHAR(25)
                DECLARE @PayrollPeriod NVARCHAR(25)
                DECLARE @ByEmployee INT
                DECLARE @EmployeeNoFrom NVARCHAR(25)
                DECLARE @EmployeeNoTo NVARCHAR(25)

                SET @Branch  = '" + _refBranch + @"'
                SET @PayrollPeriod = '" + _refPayrollPeriod + @"'
                SET @EmployeeNoFrom = '" + _refEmployeeFrom + @"'
                SET @EmployeeNoTo = '" + _refEmployeeTo + @"'

                SELECT * FROM (

                                                SELECT 
                                                (SELECT Z.CompanyName  FROM OCMP Z WHERE Z.Active = 1 AND Z.CompanyCode = LEFT(@Branch,4)) AS CompanyName
                                                ,A1.PayrollPeriod, A1.EmployeeNo, B.EmployeeName, A1.AccountCode, E.AccountDesc, A1.LoanRefenceNo, A1.Amount
                                                ,(SELECT X1.Balance FROM dbo.[fnGetBalancePerLoan](A1.EmployeeNo, A1.AccountCode, A1.PayrollPeriod, A1.LoanRefenceNo) X1) AS [Balance],
		                                                                C.BranchCode,
		                                                                C.BranchName,
		                                                                CASE WHEN B.ConfiLevel IN ('2') THEN 'Managers' ELSE 'Rank And File' END AS [GroupConfi],
		                                                                CASE WHEN B.Category = 1 THEN 'Daily' ELSE 'Monthly' END AS Category,
		                                                                C.DepName,
		                                                                B.Company,
						                                                D.DateOne,
		                                                                D.DateTwo,
                                                                        B.ConfiLevel,
                                                                        '" + _ReprotTitle + @"' AS [Report Title]

                                                FROM vwsPayrollHeader A INNER JOIN 
	                                                vwsPayrollDetails A1 ON A.EmployeeNo = A1.EmployeeNo AND A.PayrollPeriod = A1.PayrollPeriod LEFT JOIN
	                                                vwsEmployees B ON A.EmployeeNo = B.EmployeeNo LEFT JOIN 
	                                                vwsDepartmentList C ON A.Department = C.DepartmentCode LEFT JOIN 
	                                                vwsPayrollPeriod D ON A.PayrollPeriod = D.PayrollPeriod LEFT JOIN
	                                                vwsAccountCode E ON A1.AccountCode = E.AccountCode
                                                WHERE E.AccountType IN (6,4) 
                                                AND 
                                                B.ConfiLevel IN (" + _refConfiLevel + @") AND";

                                                
        switch (_refType)
        {
            case "Regular":
                _sqlSyntax = _sqlSyntax + @"		A.PayrollType = 'PAYROLL' AND
                                                                                                ";
                break;
            case "LastPay":
                _sqlSyntax = _sqlSyntax + @"		A.PayrollType = 'LASTPAY' AND
                                                                                                ";
                break;
        }


        _sqlSyntax = _sqlSyntax + @" A.PayrollPeriod = @PayrollPeriod AND
                                                A.Branch LIKE '%' + @Branch + '%'      
                        ";

        switch (_refType)
        {
            case "Regular":
                _sqlSyntax = _sqlSyntax + @"		AND A.PayrollType = 'PAYROLL'
                                                                                                ";
                break;
            case "LastPay":
                _sqlSyntax = _sqlSyntax + @"		AND A.PayrollType = 'LASTPAY'
                                                                                                ";
                break;
        }


        _sqlSyntax = _sqlSyntax + @") ZZ
                                WHERE ZZ.ConfiLevel IN (" + _refConfiSelection + @") ";

        if (_refFilterByEmployee == true)
        {
            _sqlSyntax = _sqlSyntax + @"
                                AND ZZ.EmployeeNo Between @EmployeeNoFrom AND @EmployeeNoTo 
                                                                        ";
        }


        _sqlSyntax = _sqlSyntax + @"
                                ORDER BY ZZ.EmployeeName
                                        ";

        return _sqlSyntax;
    }


    private static string sqlPaySlipOutput(string _refType = "", string _refBranch = "",
        string _refPayrollPeriod = "", string _refConfiLevel = "",
        string _refConfiSelection = "", bool _refFilterByEmployee = false,
        string _refEmployeeFrom = "", string _refEmployeeTo = "", string _ReprotTitle = "")
    {


        switch (_refType)
        {
            case "Regular":
                _ReprotTitle = _ReprotTitle.ToUpper();
                break;
            case "LastPay":
                _ReprotTitle = _ReprotTitle.ToUpper();
                break;
        }



        string _sqlSyntax = "";

        _sqlSyntax = @"

                DECLARE @Branch NVARCHAR(25)
                DECLARE @PayrollPeriod NVARCHAR(25)
                DECLARE @ByEmployee INT
                DECLARE @EmployeeNoFrom NVARCHAR(25)
                DECLARE @EmployeeNoTo NVARCHAR(25)

                SET @Branch  = '" + _refBranch + @"'
                SET @PayrollPeriod = '" + _refPayrollPeriod + @"'
                SET @EmployeeNoFrom = '" + _refEmployeeFrom + @"'
                SET @EmployeeNoTo = '" + _refEmployeeTo + @"'
      
	                SELECT  * FROM (
		                SELECT 
                                        A.PayrollPeriod,
                                        A.EmployeeNo,
                                        A.DailyRate,
                                        A.BasicPay,
                                        A.OTPay,
                                        A.OtherIncome, 
                                        A.SSSEmployee,
                                        A.PhilHealthEmployee,
                                        A.PagIbigEmployee,
                                        A.WitholdingTax,
                                        A.Gross,
                                        A.TotalDeductions,
                                        A.NetPay,
                                        A.TotalDays,
                                        A.SSSLoan,
                                        A.PagibigLoan,
                                        A.OtherLoan,
                                        A.SPLPay,
                                        A.LEGPay,
                                        A.SUNPay,
                                        B.EmployeeName,
                                        C.BCode,
                                        C.BName,
                                        D.DateOne,
                                        D.DateTwo,
                                        E.CompanyName,
                                        C.BranchCode,
                                        C.BranchName,
                                        CASE WHEN B.ConfiLevel IN ('2') THEN 'Managers' ELSE 'Rank And File' END AS [GroupConfi],
                                        CASE WHEN B.Category = 1 THEN 'Daily' ELSE 'Monthly' END AS Category,
                                        C.DepName,
                                        B.ConfiLevel,
                        '" + _ReprotTitle + @"' AS [Report Title]


                        FROM vwsPayrollHeader A
                        INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
                        LEFT JOIN vwsDepartmentList C ON A.Department = C.DepartmentCode
                        LEFT JOIN vwsPayrollPeriod D ON A.PayrollPeriod = D.PayrollPeriod
                        LEFT JOIN OCMP E ON C.Company = E.CompCode AND E.Active = 1
		                WHERE B.DateHired <= D.DateTwo 
		                AND A.Branch LIKE @Branch AND A.PayrollPeriod = @PayrollPeriod
		                AND B.ConfiLevel IN (" + _refConfiLevel + @")
                        ";

        switch (_refType)
        {
            case "Regular":
                _sqlSyntax = _sqlSyntax + @"		AND ISNULL(B.DateFinish,'') NOT BETWEEN D.DateOne AND D.DateTwo
		                                                                            AND (ISNULL(B.DateFinish,'') >= D.DateOne OR ISNULL(B.DateFinish,'') = '')
                                                                                        ";
                break;
            case "LastPay":
                _sqlSyntax = _sqlSyntax + @"		AND ISNULL(B.DateFinish,'') BETWEEN D.DateOne AND D.DateTwo
		                                                                            AND ISNULL(B.DateFinish,'') <> ''
                                                                                        ";
                break;
        }

        _sqlSyntax = _sqlSyntax + @") ZZ
                        WHERE ZZ.ConfiLevel IN (" + _refConfiSelection + @") ";

        if (_refFilterByEmployee == true)
        {
            _sqlSyntax = _sqlSyntax + @"
                        AND ZZ.EmployeeNo Between @EmployeeNoFrom AND @EmployeeNoTo 
                                                                ";
        }


        _sqlSyntax = _sqlSyntax + @"
                        ORDER BY ZZ.EmployeeName
                                ";

        return _sqlSyntax;
    }





    private void crystalReportViewer1_Load(object sender, EventArgs e)
    {

    }
}
