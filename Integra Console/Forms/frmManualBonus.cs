using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmManualBonus : Form
{

    public static string _gCompany = "";
    public static string _gEmpCode = "";
    public static string _gEmpName = "";
    public static string _gPayrollPeriod = "";
    public static string _gAmount = "";


    public static string _gAccountCode = "";
    public static string _gLoanReferenceNo = "";
    public frmManualBonus()
    {
        InitializeComponent();
        _gCompany = "";
        _gEmpCode = "";
        _gEmpName = "";
        _gPayrollPeriod = "";


        _gAccountCode = "";
        _gLoanReferenceNo = "";
    }

    private void frmManualBonus_Load(object sender, EventArgs e)
    {
        cboYear.Items.Clear();
        int _year = DateTime.Now.Year - 5;
        while (_year <= (DateTime.Now.Year + 10))
        {
            cboYear.Items.Add(_year);
            _year++;
        }

        //DataTable _DataTable;
        //string _SQLSyntax;
        //_SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);



        //if (_gCompany != "")
        //{
        //    txtCompany.Text = _gCompany;
        //    txtEmpCode.Text = _gEmpCode;
        //    txtEmpName.Text = _gEmpName;
        //    txtPayrollPeriod.Text = _gPayrollPeriod;
        //}
        //button1_Click(sender, e);
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
            _gAmount = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();



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


            //clsFunctions.UpdatePayrollHeader(_Company,PA, _EmployeeNo, _MonthlyRate);
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
            txtEmpName.Text = _EmployeeName;
            //txtRate.Text = _DailyRate.ToString("N2");


            generateManualPayroll(_Company, _EmployeeNo);


            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
            string _sqlSyntax = @"SELECT ZZ.[AccountCode]
	  
                                          ,XX.AccountDesc
                                          ,[Amount]
                                          ,[NoOfService]
	                                      FROM (
                                    SELECT * FROM [PerformanceDetails]
                                      UNION ALL
                                    SELECT * FROM [13MonthDetails] ) ZZ
                                    INNER JOIN AccountCode XX ON ZZ.AccountCode = XX.AccountCode
                                    WHERE ZZ.Year = '" + cboYear.Text + @"' AND ZZ.EmployeeNo = '" + _EmployeeNo + @"'";


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
        //frmPayrollDetails._gPayrollPeriod = txtPayrollPeriod.Text;
        frmPayrollDetails.ShowDialog();


        button1_Click(sender, e);
        //Display();
    }

    private void btnRowEdit_Click(object sender, EventArgs e)
    {
        frmPayrollDetails frmPayrollDetails = new frmPayrollDetails();
        frmPayrollDetails._gCompany = txtCompany.Text;
        frmPayrollDetails._gAddState = "b_Edit";

        frmPayrollDetails._gAccountCode = _gAccountCode;
        frmPayrollDetails._gAmount = _gAmount;
        frmPayrollDetails._gEmployeeNo = txtEmpCode.Text;
        frmPayrollDetails._gPayrollPeriod = cboYear.Text;

        frmPayrollDetails.ShowDialog();
        button1_Click(sender, e);
        //Display();
    }

    private void btnRowDelete_Click(object sender, EventArgs e)
    {
        //string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

        //string _sqlDelete;
        //_sqlDelete = @"DELETE FROM [PayrollDetails] WHERE [PayrollPeriod] = '" + cboPayrolPeriod.Text + @"'
        //        AND [EmployeeNo] = '" + txtEmpCode.Text + @"' 
        //        AND [AccountCode] = '" + _gAccountCode + @"' 
        //        AND [LoanRefenceNo] = '" + _gLoanReferenceNo + @"'";

        //clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlDelete);
        //button1_Click(sender, e);
        //Display();
    }

    private void txtEmpCode_TextChanged(object sender, EventArgs e)
    {

    }




    private void btnDelete_Click(object sender, EventArgs e)
    {
        //DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        //if (res == DialogResult.Cancel)
        //{
        //    return;
        //}

        //string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);
        
        //string _sqlSyntax = @"
        //                        DELETE
        //                                FROM [PayrollHeader]
        //                                WHERE EmployeeNo = '" + txtEmpCode.Text + @"' AND PayrollPeriod = '" + txtPayrollPeriod.Text + @"'
        //                        DELETE
        //                                FROM [PayrollDetails]
        //                                WHERE EmployeeNo = '" + txtEmpCode.Text + @"' AND PayrollPeriod = '" + txtPayrollPeriod.Text + @"'";

        //clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlSyntax);

        //MessageBox.Show(" Payment Successfully Deleted!");
        
    }




    private void generateManualPayroll(string _Company, string _EmployeeNo)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

        string _sqlPayrollDetails = "";
        DataTable _tblPayrollDetails = new DataTable();

        // Get Regular Hours / Days
        _sqlPayrollDetails = @" SELECT * FROM [PerformanceDetails] ZZ WHERE ZZ.Year = '" + cboYear.Text + @"' AND ZZ.EmployeeNo = '" + _EmployeeNo + @"'";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        //double _BasicPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        //double _RegularHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        //double _RegularDays = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalDays", "1")), 5, MidpointRounding.AwayFromZero);

        txtPerformancePay.Text = double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")).ToString("N2");
        txtNoOfService.Text = double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "NoOfService", "1")).ToString("N2");

        _sqlPayrollDetails = @" SELECT * FROM [13MonthDetails] ZZ WHERE ZZ.Year = '" + cboYear.Text + @"' AND ZZ.EmployeeNo = '" + _EmployeeNo + @"'";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        txt13MonthPay.Text = double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")).ToString("N2");


        txtTotalBonus.Text = (double.Parse(txtPerformancePay.Text) + double.Parse(txt13MonthPay.Text)).ToString("N2");

        _sqlPayrollDetails = @" SELECT * FROM [PerformanceData] ZZ WHERE ZZ.Year = '" + cboYear.Text + @"' AND ZZ.EmployeeNo = '" + _EmployeeNo + @"'";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        txtRate.Text = double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "DailyRate", "1")).ToString("N2");
    }

    private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtFrom = DateTime.Parse("12/25/" + cboYear.Text);
        txtDateTo.Text = dtFrom.ToString("MM/dd/yyyy");
        txtDateFrom.Text = dtFrom.AddDays(1).AddYears(-1).ToString("MM/dd/yyyy");
    }
}