using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmManualPayroll : Form
{

    public static string _gCompany = "";
    public static string _gEmpCode = "";
    public static string _gEmpName = "";
    public static string _gPayrollPeriod = "";


    public static string _gAccountCode = "";
    public static string _gLoanReferenceNo = "";
    public frmManualPayroll()
    {
        InitializeComponent();
        _gCompany = "";
        _gEmpCode = "";
        _gEmpName = "";
        _gPayrollPeriod = "";


        _gAccountCode = "";
        _gLoanReferenceNo = "";
    }

    private void frmManualPayroll_Load(object sender, EventArgs e)
    {
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

        if (_gCompany != "")
        {
            txtCompany.Text = _gCompany;
            txtEmpCode.Text = _gEmpCode;
            txtEmpName.Text = _gEmpName;
            txtPayrollPeriod.Text = _gPayrollPeriod;
            cboPayrolPeriod.Text = _gPayrollPeriod;
        }
        button1_Click(sender, e);
        //Display();
    }



    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }


            _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            _gLoanReferenceNo = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();



        }
        catch
        {

        }
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {

        string _sqlEmployeeList = "";
        _sqlEmployeeList = @"SELECT  A.Company, A.EmployeeNo, A.EmployeeName, A.DailyRate, A.MonthlyRate
                            FROM vwsEmployees A WHERE A.EmployeeNo  = '" + txtEmpCode.Text + @"'
                            AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
                            ";

        DataTable _tblEmployeeList = new DataTable();
        _tblEmployeeList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlEmployeeList);

        foreach (DataRow row in _tblEmployeeList.Rows)
        {
            string _Company = row["Company"].ToString();
            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _EmployeeName = row["EmployeeName"].ToString();
            double _DailyRate = double.Parse(row["DailyRate"].ToString());
            double _MonthlyRate = double.Parse(row["MonthlyRate"].ToString());

            int _Ishold = 0;
            if (chkHold.CheckState == CheckState.Checked)
            {
                _Ishold = 1;
            }

            clsFunctions.UpdatePayrollHeader(_Company, cboPayrolPeriod.Text, _EmployeeNo, _MonthlyRate, _Ishold);
        }

        MessageBox.Show("Payroll Data Successfully Updated");



        //string _sqlSelect;
        //DataTable _tblSelect;

        //string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);
        //_sqlSelect = "SELECT A.[Department],[MonthlyRate],[DailyRate],A.BankAccountNo, A.PayrollMode  FROM Employees A WHERE A.EmployeeNo = '" + txtEmpCode.Text + "'";
        //_tblSelect = clsSQLClientFunctions.DataList(_ConCompany, _sqlSelect);


        //string _BankAccountNo = clsSQLClientFunctions.GetData(_tblSelect, "BankAccountNo", "0");
        //string _PayrollMode = clsSQLClientFunctions.GetData(_tblSelect, "PayrollMode", "0");
        //string _Department = clsSQLClientFunctions.GetData(_tblSelect, "Department", "0");
        //double _DailyRate = double.Parse(clsSQLClientFunctions.GetData(_tblSelect, "DailyRate", "1"));
        //double _MonthlyRate = double.Parse(clsSQLClientFunctions.GetData(_tblSelect, "MonthlyRate", "1"));
        //double _Month13th = 0.00;





        //string _sqlList = "";
        //_sqlList = @"SELECT ISNULL(SUM(A.TotalDays), 0) AS TotalDays
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.[AccountType] = 0";
        //string _TotalDays = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "TotalDays").ToString("0.00000");


        //_sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS OTHrs 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.[AccountType] = 1";
        //string _OTHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "OTHrs").ToString("0.00000");

        //_sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS SunSplHolHrs 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.[AccountType] IN (2, 3)
        //        AND A.AccountCode NOT IN ('3-203','3-208','3-214','3-215','4-307','4-308','4-311','4-309','4-310','4-302')";
        //string _SPLHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SunSplHolHrs").ToString("0.00000");

        //_sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS LegalHolHrs 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.[AccountType] IN (2, 3)
        //        AND A.AccountCode IN ('3-203','3-208','3-214','3-215','4-307','4-308','4-311','4-309','4-310')";
        //string _LEGHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "LegalHolHrs").ToString("0.00000");


        //_sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS TotalHrs 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.[AccountType] = 0";
        //string _TotalHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "TotalHrs").ToString("0.00000");


        //_sqlList = @"SELECT ISNULL(SUM(A.TotalHrs),0) AS SUNHrs 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.AccountCode IN ('4-302')";
        //string _SUNHrs = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SUNHrs").ToString("0.00000");


        //_sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS COLAAmount 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.AccountCode IN ('7-405')";
        //string _COLAAmount = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "COLAAmount").ToString("0.00");


        //_sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS SunSplHolPay 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.[AccountType] IN (2, 3)
        //        AND A.AccountCode NOT IN ('3-203','3-208','3-214','3-215','4-307','4-308','4-311','4-309','4-310','4-302')";
        //string _SPLPay = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SunSplHolPay").ToString("0.00");


        //_sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS LegalHolPay 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.[AccountType] IN (2, 3)
        //        AND A.AccountCode IN ('3-203','3-208','3-214','3-215','4-307','4-308','4-311','4-309','4-310')";
        //string _LEGPay = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "LegalHolPay").ToString("0.00");


        //_sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS SUNPay 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.AccountCode IN ('4-302')";
        //string _SUNPay = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SUNPay").ToString("0.00");

        //_sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS SSSLoan 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.AccountCode IN ('8-502')";
        //string _SSSLoan = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "SSSLoan").ToString("0.00");

        //_sqlList = @"SELECT ISNULL(SUM(A.Amount),0) AS PAGIBIGLoan 
        //        FROM dbo.[fnTabPayrollDetails]('" + txtEmpCode.Text + @"','" + cboPayrolPeriod.Text + @"') A 
        //        WHERE A.Amount <> 0 AND A.AccountCode IN ('8-504')";
        //string _PagibigLoan = clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlList, "PAGIBIGLoan").ToString("0.00");


        //string _PayType = "";
        //if (chkLastPay.Checked == true)
        //{
        //    _PayType = "LASTPAY";
        //}
        //else
        //{
        //    _PayType = "PAYROLL";
        //}



        //string _SQLExecute = @"
        //                                DECLARE @Employee NVARCHAR(30)
        //                                DECLARE @PayPeriod NVARCHAR(30)

        //                                SET @Employee = '" + txtEmpCode.Text + @"'
        //                                SET @PayPeriod = '" + txtPayrollPeriod.Text + @"'

        //                                DELETE FROM [PayrollHeader] WHERE [EmployeeNo] = @Employee AND [PayrollPeriod] = @PayPeriod

        //                                INSERT INTO [dbo].[PayrollHeader]
        //                                           ([PayrollPeriod]
        //                                           ,[EmployeeNo]
        //                                           ,[MonthlyRate]
        //                                           ,[DailyRate]
        //                                           ,[DepartmentCode]
        //                                           ,[Department]
        //                                           ,[BasicPay]
        //                                           ,[OTPay]
        //                                           ,[OtherIncome]
        //                                           ,[Month13th]
        //                                           ,[SSSEmployee]
        //                                           ,[SSSEmployer]
        //                                           ,[SSSEC]
        //                                           ,[PhilHealthEmployee]
        //                                           ,[PhilHealthEmployer]
        //                                           ,[PagIbigEmployee]
        //                                           ,[PagIbigEmployer]
        //                                           ,[WitholdingTax]
        //                                           ,[OtherDeduction]
        //                                           ,[Gross]
        //                                           ,[TotalDeductions]
        //                                           ,[NetPay]
        //                                           ,[SSSBaseAmount]
        //                                           ,[PhilHealthBaseAmount]
        //                                           ,[PagIbigBaseAmount]
        //                                           ,[TaxBaseAmount]
        //                                           ,[COLAAmount]
        //                                           ,[NonTaxPagIbig]
        //                                           ,[TotalDays]
        //                                           ,[TotalHrs]
        //                                           ,[SSSLoan]
        //                                           ,[PagibigLoan]
        //                                           ,[OtherLoan]
        //                                           ,[OTHrs]
        //                                           ,[SPLHrs]
        //                                           ,[LEGHrs]
        //                                           ,[SUNHrs]
        //                                           ,[SPLPay]
        //                                           ,[LEGPay]
        //                                           ,[SUNPay]
        //                                           ,[PayrollType]
        //                                           ,[BankAccountNo]
        //                                           ,[PayrollMode])
        //                                     VALUES
        //                                           ('" + txtPayrollPeriod.Text + @"'
        //                                           ,'" + txtEmpCode.Text + @"'
        //                                           ,'" + (double.Parse(txtRate.Text) * 26)+ @"'
        //                                           ,'" + txtRate.Text + @"'
        //                                           ,'" + _Department + @"'
        //                                           ,'" + _Department + @"'
        //                                           ,'" + txtBasicPay.Text + @"'
        //                                           ,'" + txtOTPay.Text + @"'
        //                                           ,'" + txtOtherIncome.Text + @"'
        //                                           ,'" + _Month13th + @"'
        //                                           ,'" + txtSSSEmployee.Text + @"'
        //                                           ,'" + txtSSSEmployer.Text + @"'
        //                                           ,'" + txtSSSECC.Text + @"'
        //                                           ,'" + txtPhilHealthEmployee.Text + @"'
        //                                           ,'" + txtPhilHealthEmployer.Text + @"'
        //                                           ,'" + txtPAGIBIGEmployee.Text + @"'
        //                                           ,'" + txtPAGIBIGEmployer.Text + @"'
        //                                           ,'" + txtWtaxAmount.Text + @"'
        //                                           ,'" + txtOtherDeduction.Text + @"'
        //                                           ,'" + txtGrossPay.Text + @"'
        //                                           ,'" + txtTotalDeduction.Text + @"'
        //                                           ,'" + txtNetPay.Text + @"'
        //                                           ,'" + txtSSSBaseAmount.Text + @"'
        //                                           ,'" + txtPhiheahltBaseAmount.Text + @"'
        //                                           ,'" + txtPagibigBaseAmount.Text + @"'
        //                                           ,'" + txtWtaxBaseAmount.Text + @"'
        //                                           ,'" + _COLAAmount + @"'
        //                                           ,'" + txtPAGIBIGEmployer.Text + @"'
        //                                           ,'" + _TotalDays + @"'
        //                                           ,'" + _TotalHrs + @"'
        //                                           ,'" + _SSSLoan + @"'
        //                                           ,'" + _PagibigLoan + @"'
        //                                           ,'" + txtOtherLoan.Text + @"'
        //                                           ,'" + _OTHrs + @"'
        //                                           ,'" + _SPLHrs + @"'
        //                                           ,'" + _LEGHrs + @"'
        //                                           ,'" + _SUNHrs + @"'
        //                                           ,'" + _SPLPay + @"'
        //                                           ,'" + _LEGPay + @"'
        //                                           ,'" + _SUNPay + @"'
        //                                            ,'" + _PayType + @"'
        //                                           ,'" + _BankAccountNo + @"'
        //                                           ,'" + _PayrollMode + @"')


        //                                          ";

        //clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLExecute);





        //_SQLExecute = @"
        //         DELETE FROM [LoanCashPayment] WHERE [EmployeeNo] = '" + txtEmpCode.Text + @"' AND [PayrollPeriod] = '" + txtPayrollPeriod.Text + @"'
        //                                ";

        //clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLExecute);




        //string _sqlRebate = @"

        //                                DECLARE @Employee NVARCHAR(30)
        //                                DECLARE @PayrollPeriod NVARCHAR(30)

        //                                SET @Employee = '" + txtEmpCode.Text + @"'
        //                                SET @PayrollPeriod = '" + txtPayrollPeriod.Text + @"'

        //                        SELECT Z.Company,Z.EmployeeNo, Z.AccountCode, Z.LoanRefNo, CONCAT(@PayrollPeriod,' | ',Z.EmployeeNo)  AS ORNo
        //                        , (SELECT X.DateTwo FROM vwsPayrollPeriod X WHERE X.PayrollPeriod = @PayrollPeriod) AS PaymentDate
        //                        , Z.Rebate AS Amount
        //                        , (SELECT CONCAT(X.[PayrollPeriod], ' (', CONVERT(nvarchar(30), X.DateOne, 101),' - '
        //                        , CONVERT(nvarchar(30), X.DateTwo, 101), ') ') FROM vwsPayrollPeriod X WHERE X.PayrollPeriod = @PayrollPeriod) AS Remarks
        //                        ,'REBATE' AS TYPE

        //                        FROM (
        //                        SELECT * FROM vwsLoanFile A WHERE ISNULL(A.Rebate,0) <> 0 AND A.RebateApplication = 0
        //                        UNION ALL
        //                        SELECT * FROM vwsLoanFile A WHERE ISNULL(A.Rebate,0) <> 0 
        //                        AND A.RebateApplication = (CASE WHEN RIGHT(@PayrollPeriod,1) = 'A' THEN '1'
        //                                WHEN RIGHT(@PayrollPeriod,1) = 'B' THEN '2' END)) Z


        //                        WHERE  Z.StartOfDeduction <= @PayrollPeriod AND Z.EmployeeNo = @Employee;


        //                                    ";
        //DataTable _tblRebate = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlRebate);



        //foreach (DataRow rowCashRebate in _tblRebate.Rows)
        //{

        //    string _AccountCode = rowCashRebate[2].ToString();
        //    string _LoanRefNo = rowCashRebate[3].ToString();
        //    string _ORNo = rowCashRebate[4].ToString();
        //    string _PaymentDate = rowCashRebate[5].ToString();
        //    string _Amount = rowCashRebate[6].ToString();
        //    string _Remarks = rowCashRebate[7].ToString();
        //    string _Type = rowCashRebate[8].ToString();


        //    string _SQLRebate = @"
        //         DELETE FROM [LoanCashPayment] WHERE [EmployeeNo] = '" + txtEmpCode.Text + @"' AND [PayrollPeriod] = '" + txtPayrollPeriod.Text + @"'
        //        AND [AccountCode] = '" + _AccountCode + @"' AND [LoanRefNo] = '" + _LoanRefNo + @"'

        //        INSERT INTO [dbo].[LoanCashPayment]
        //                   ([EmployeeNo]
        //                   ,[AccountCode]
        //                   ,[LoanRefNo]
        //                   ,[ORNo]
        //                   ,[PaymentDate]
        //                   ,[Amount]
        //                   ,[Remarks]
        //                   ,[Type]
        //                   ,[PayrollPeriod])
        //             VALUES
        //                   ('" + txtEmpCode.Text + @"'
        //                   ,'" + _AccountCode + @"'
        //                   ,'" + _LoanRefNo + @"'
        //                   ,'" + _ORNo + @"'
        //                   ,'" + _PaymentDate + @"'
        //                   ,'" + _Amount + @"'
        //                   ,'" + _Remarks + @"'
        //                   ,'" + _Type + @"'
        //                   ,'" + txtPayrollPeriod.Text + @"')

        //                                        ";

        //    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLRebate);

        //}



    }

    private void button1_Click(object sender, EventArgs e)
    {

        string _sqlEmployeeList = "";
        _sqlEmployeeList = @"SELECT  A.Company, A.EmployeeNo, A.EmployeeName, A.DailyRate, A.MonthlyRate
                            FROM vwsEmployees A WHERE A.EmployeeNo  = '" + txtEmpCode.Text + @"'
                            AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
                            ";

        DataTable _tblEmployeeList = new DataTable();
        _tblEmployeeList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlEmployeeList);

        foreach (DataRow row in _tblEmployeeList.Rows)
        {
            string _Company = row["Company"].ToString();
            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _EmployeeName = row["EmployeeName"].ToString();
            double _DailyRate = double.Parse(row["DailyRate"].ToString());
            double _MonthlyRate = double.Parse(row["MonthlyRate"].ToString());

            txtCompany.Text = _Company;
            txtPayrollPeriod.Text = cboPayrolPeriod.Text;
            txtEmpName.Text = _EmployeeName;
            txtRate.Text = _DailyRate.ToString("N2");

            txtSSSBaseAmount.Text = _MonthlyRate.ToString("N2");
            txtPagibigBaseAmount.Text = _MonthlyRate.ToString("N2");
            txtPhiheahltBaseAmount.Text = _MonthlyRate.ToString("N2");
            txtWtaxBaseAmount.Text = _MonthlyRate.ToString("N2");

            generateManualPayroll(_Company, _EmployeeNo, cboPayrolPeriod.Text);


            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
            string _sqlSyntax = @"SELECT  A.[AccountCode] AS [Account Code]
                                                ,ISNULL(B.[AccountDesc],(SELECT Z.Description FROM PayrollRegsCode Z WHERE Z.AccountCode = A.[AccountCode])) AS [Account Name]
                                                ,A.[LoanRefenceNo] AS [Lon Ref No]
                                                ,A.[NoOfHours] AS [No Of Hours]
                                                ,A.[NoOfMins] AS [No Of Mins]
                                                ,ISNULL(A.[PrincipalAmt], 0.00) AS [Principal Amount]
                                                ,ISNULL(A.[InterestAmt], 0.00) AS [Interest Amount]
                                                ,A.[Amount]
                                           
                                        FROM [PayrollDetails] A LEFT JOIN [AccountCode] B ON A.AccountCode = B.AccountCode
                                        WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'";


            DataTable _tblDisplay;
            _tblDisplay = clsSQLClientFunctions.DataList(_ConCompany, _sqlSyntax);
            clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay);

        }





       // RetriveData();

        //_gCompany = "";
        //_gEmpCode = "";
        //_gEmpName = "";
        //_gPayrollPeriod = "";
        
        //_gAccountCode = "";
        //_gLoanReferenceNo = "";
    
        //if (cboPayrolPeriod.Text == "")
        //{
        //    MessageBox.Show("Please select payroll period");
        //    return;
        //}


        //frmDataList frmDataList = new frmDataList();
        //frmDataList._gListGroup = "PayrollList";
        //frmDataList._gPayrollPeriod = cboPayrolPeriod.Text;
        //frmDataList.ShowDialog();

        //txtPayrollPeriod.Text = frmDataList._gPayrollPeriod;
        //txtEmpCode.Text = frmDataList._gEmployeeNo;



        //string _sqlList = "";

        //_sqlList = @"SELECT  A.Company, CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName]
        //                    FROM vwsEmployees A WHERE A.EmployeeNo COLLATE Latin1_General_CI_AS = '" + txtEmpCode.Text + @"'";
        //txtEmpName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "EmployeeName");
        //txtCompany.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Company");


        //string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);
        //Display();
        ////GenerateData();
    }








    private void btnRowAdd_Click(object sender, EventArgs e)
    {
        frmPayrollDetails frmPayrollDetails = new frmPayrollDetails();
        frmPayrollDetails._gCompany = txtCompany.Text;
        frmPayrollDetails._gAddState = "Add";

        frmPayrollDetails._gEmployeeNo = txtEmpCode.Text;
        frmPayrollDetails._gPayrollPeriod = txtPayrollPeriod.Text;
        frmPayrollDetails.ShowDialog();


        button1_Click(sender, e);
        //Display();
    }

    private void btnRowEdit_Click(object sender, EventArgs e)
    {
        frmPayrollDetails frmPayrollDetails = new frmPayrollDetails();
        frmPayrollDetails._gCompany = txtCompany.Text;
        frmPayrollDetails._gAddState = "Edit";

        frmPayrollDetails._gAccountCode = _gAccountCode;
        frmPayrollDetails._gLoanRefNo = _gLoanReferenceNo;
        frmPayrollDetails._gEmployeeNo = txtEmpCode.Text;
        frmPayrollDetails._gPayrollPeriod = txtPayrollPeriod.Text;

        frmPayrollDetails.ShowDialog();
        button1_Click(sender, e);
        //Display();
    }

    private void btnRowDelete_Click(object sender, EventArgs e)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

        string _sqlDelete;
        _sqlDelete = @"DELETE FROM [PayrollDetails] WHERE [PayrollPeriod] = '" + cboPayrolPeriod.Text + @"'
                AND [EmployeeNo] = '" + txtEmpCode.Text + @"' 
                AND [AccountCode] = '" + _gAccountCode + @"' 
                AND [LoanRefenceNo] = '" + _gLoanReferenceNo + @"'";

        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlDelete);
        button1_Click(sender, e);
        //Display();
    }

    private void txtEmpCode_TextChanged(object sender, EventArgs e)
    {

    }




    private void btnDelete_Click(object sender, EventArgs e)
    {
        DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        if (res == DialogResult.Cancel)
        {
            return;
        }

        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);
        
        string _sqlSyntax = @"
                                DELETE
                                        FROM [PayrollHeader]
                                        WHERE EmployeeNo = '" + txtEmpCode.Text + @"' AND PayrollPeriod = '" + txtPayrollPeriod.Text + @"'
                                DELETE
                                        FROM [PayrollDetails]
                                        WHERE EmployeeNo = '" + txtEmpCode.Text + @"' AND PayrollPeriod = '" + txtPayrollPeriod.Text + @"'";

        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlSyntax);

        MessageBox.Show(" Payment Successfully Deleted!");
        
    }




    private void generateManualPayroll(string _Company, string _EmployeeNo, string _PayrollPeriod)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

        string _sqlPayrollDetails = "";
        DataTable _tblPayrollDetails = new DataTable();

        #region Regular Days Calculation
        // Get Regular Hours / Days
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalDays),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'Basic Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _BasicPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _RegularHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        double _RegularDays = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalDays", "1")), 5, MidpointRounding.AwayFromZero);

        txtBasicPay.Text = _BasicPay.ToString("N2");

        #endregion
        #region Overtime Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalDays),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'OT Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _OTPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _OTHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);

        txtOTPay.Text = _OTPay.ToString("N2");
        #endregion
        #region Sunday/Restday Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalDays),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'SUN Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _SUNPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _SUNHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Special Holiday Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalDays),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'Special Holiday Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _SPLPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _SPLHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Legal Holiday Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalDays),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'Legal Holiday Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _LEGPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _LEGHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion

        txtHolidayPay.Text = (_SUNPay + _SPLPay + _LEGPay).ToString("N2");

        #region Goverment Calculation
        #region SSS Goverment Calculation
        double _SSSEmployee = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSS Employee"), 2, MidpointRounding.AwayFromZero);
        double _SSSEmployer = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSS Employer"), 2, MidpointRounding.AwayFromZero);
        double _SSSEC = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSSEC Employee"), 2, MidpointRounding.AwayFromZero);

        txtSSSEmployee.Text = _SSSEmployee.ToString("N2");
        txtSSSEmployer.Text = _SSSEmployer.ToString("N2");
        txtSSSECC.Text = _SSSEC.ToString("N2");

        #endregion
        #region PHILHEALTH Goverment Calculation
        double _PhilHealthEmployee = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PhilHealth Employee"), 2, MidpointRounding.AwayFromZero);
        double _PhilHealthEmployer = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PhilHealth Employer"), 2, MidpointRounding.AwayFromZero);

        txtPhilHealthEmployee.Text = _PhilHealthEmployee.ToString("N2");
        txtPhilHealthEmployer.Text = _PhilHealthEmployer.ToString("N2");


        #endregion
        #region PAGIBIG Goverment Calculation
        double _PagIbigEmployee = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PagIbig Employee"), 2, MidpointRounding.AwayFromZero);
        double _PagIbigEmployer = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PagIbig Employer"), 2, MidpointRounding.AwayFromZero);

        txtPAGIBIGEmployee.Text = _PagIbigEmployee.ToString("N2");
        txtPAGIBIGEmployer.Text = _PagIbigEmployer.ToString("N2");

        #endregion
        #region WTAX Goverment Calculation
        double _WitholdingTax = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Withholding Tax"), 2, MidpointRounding.AwayFromZero);

        txtWtaxAmount.Text = _WitholdingTax.ToString("N2");
        double _GovDeduction = _SSSEmployee + _PagIbigEmployee + _PhilHealthEmployee + _WitholdingTax;

        #endregion

        #endregion
        #region Gross Pay Calculation
        double _COLAAmount = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "COLA Amount"), 2, MidpointRounding.AwayFromZero);
        double _AllowanceAmount = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Allowance Amount"), 2, MidpointRounding.AwayFromZero);
        double _PERAAmount = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PERRA Amount"), 2, MidpointRounding.AwayFromZero);
        double _OtherIncome = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Other Income"), 2, MidpointRounding.AwayFromZero);
        double _PaidLeaves = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Paid Leaves"), 2, MidpointRounding.AwayFromZero);
        //double _Absences = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Absences"), 2, MidpointRounding.AwayFromZero);
        txtPaidLeaves.Text = _PaidLeaves.ToString("N2");



        double _TotalOtherIncome = _COLAAmount + _AllowanceAmount + _PERAAmount + _OtherIncome + _PaidLeaves;
        txtOtherIncome.Text = _TotalOtherIncome.ToString("N2");
        double _GrossPay = (_BasicPay + _OTPay + _SUNPay + _SPLPay + _LEGPay + _TotalOtherIncome);
        txtGrossPay.Text = _GrossPay.ToString("N2");
        #endregion
        #region Loan Pay Calculation
        double _SSSLoan = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSS Loan"), 2, MidpointRounding.AwayFromZero);
        double _PagibigLoan = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Pagibig Loan"), 2, MidpointRounding.AwayFromZero);
        double _CalamityLoan = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Calamity Loan"), 2, MidpointRounding.AwayFromZero);
        txtGovLoans.Text = (_SSSLoan + _PagibigLoan + _CalamityLoan).ToString("N2");

        double _OtherLoan = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Other Loan"), 2, MidpointRounding.AwayFromZero);
        txtOtherLoan.Text = _OtherLoan.ToString("N2");

        #endregion
        #region Net Pay Calculation
        double _OtherDeduction = Math.Round(clsFunctions.GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Other Deduction"), 2, MidpointRounding.AwayFromZero);
        txtOtherDeduction.Text = _OtherDeduction.ToString("N2");

        double _TotalDeduction = _SSSLoan + _PagibigLoan + _CalamityLoan + _OtherLoan + _OtherDeduction + _GovDeduction;
        txtTotalDeduction.Text = _TotalDeduction.ToString("N2");

        double _NetPay = _GrossPay - _TotalDeduction;
        txtNetPay.Text = _NetPay.ToString("N2");
        #endregion





        #region Payroll Header Details
        string _sqlPayrollHeader = "";
        DataTable _tblPayrollHeader = new DataTable();

        _sqlPayrollHeader = @"SELECT ISNULL([IsHold], 0) AS [IsHold] FROM [vwsPayrollHeader] A WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"' ";
        _tblPayrollHeader = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollHeader);

        chkHold.Checked = false;
        if (clsSQLClientFunctions.GetData(_tblPayrollHeader, "IsHold", "1") == "1" )
        {
            chkHold.Checked = true;
        }


        #endregion




    }

}