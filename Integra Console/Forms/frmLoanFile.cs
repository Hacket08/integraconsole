using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmLoanFile : Form
{

    public static string _LoanPayrollType;
    public static string _LoanPaymentDate;
    public static string _LoanORNumber;
    public static string _LoanPaymentAmount;
    public static string _LoanRemarks;

    public frmLoanFile()
    {
        InitializeComponent();


        txtEmpCode.Text = "";
        txtEmpName.Text = "";
        txtCompany.Text = "";
        txtLoanAccountCode.Text = "";
        txtBranchCode.Text = "";
        txtBranchName.Text = "";
        txtDescription.Text = "";
        txtLoanReferenceNo.Text = "";
        txtLoanDate.Text = DateTime.Today.ToString();
        txtAmountGranted.Text = "0.00";
        txtPayableLoanAmount.Text = "0.00";
        txtAmortization.Text = "0.00";
        txtLoanInterest.Text = "0.00";

        txtStartofDeduction.Text = "";
        txtTermsOfPayment.Text = "";
        txtRebateApplication.Text = "";

        txtTerms.Text = "";

        txtCash.Text = "0.00";
        txtPayroll.Text = "0.00";
        txtTotalCredit.Text = "0.00";
        txtBalance.Text = "0.00";

        txtRebateAmount.Text = "0.00";
        txtParticular.Text = "";
        txtBrand.Text = "";
        txtLCPPrice.Text = "0.00";
        txtSpotCashAmount.Text = "0.00";
        txtDownPayment.Text = "0.00";


        txtTermsOfPayment.Items.Clear();
        txtTermsOfPayment.Items.Add("Always");
        txtTermsOfPayment.Items.Add("First Payroll");
        txtTermsOfPayment.Items.Add("Second Payroll");
        txtTermsOfPayment.Items.Add("");

        txtRebateApplication.Items.Clear();
        txtRebateApplication.Items.Add("Always");
        txtRebateApplication.Items.Add("First Payroll");
        txtRebateApplication.Items.Add("Second Payroll");
        txtRebateApplication.Items.Add("");



        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A ORDER BY A.[PayrollPeriod] DESC";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);


        txtStartofDeduction.Items.Clear();
        txtStartofDeduction.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            txtStartofDeduction.Items.Add(row[0].ToString());
        }

    }

    private void frmLoanFile_Load(object sender, EventArgs e)
    {
        btnEmployee.Enabled = false;
        btnAccount.Enabled = false;
        btnBranch.Enabled = false;

        btnSearch.Enabled = true;
        btnNew.Enabled = true;
        btnSave.Enabled = false;
        btnDelete.Enabled = true;
        btnCancel.Enabled = true;

        btnRowAdd.Enabled = false;
        btnRowEdit.Enabled = false;
        btnRowDelete.Enabled = false;

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }


    private  bool CheckTextField()
    {
        //if (txtBranchCode.Text == "" || txtBranchName.Text == "" )
        //{
        //    MessageBox.Show("Branch Field Must Not Blank!");
        //    return false;
        //}

        if (txtEmpCode.Text == "" || txtLoanAccountCode.Text == "" || txtLoanReferenceNo.Text == "")
        {
            MessageBox.Show("Employee, Reference Number, Account Code Field Must Not Blank!");
            return false;
        }

        if (txtStartofDeduction.Text == "" || txtTermsOfPayment.Text == "" )
        {
            MessageBox.Show("Start of Deduction, Terms of Payment Field Must Not Blank!");
            return false;
        }
        return true;
    }


    private void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckTextField() == false)
        {
            return;
        }

        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);
        int _Status;

        if (chkActive.Checked == true)
        {
            _Status = 0;
        }
        else
        {
            _Status = 1;
        }

        string _sqlSave = "";
        string _msg = "";

 

        if (txtEmpCode.ReadOnly == true)
        {
            _sqlSave = @"
                                    UPDATE A
                                       SET 
	                                       A.[StartOfDeduction] = '" + txtStartofDeduction.Text + @"'
                                          ,A.[LoanAmount] = CASE WHEN '" + double.Parse(txtPayableLoanAmount.Text) + @"' = '' THEN 0 ELSE " + double.Parse(txtPayableLoanAmount.Text) + @" END
                                          ,A.[AmountGranted] = CASE WHEN '" + double.Parse(txtAmountGranted.Text) + @"' = '' THEN 0 ELSE " + double.Parse(txtAmountGranted.Text) + @" END
                                          ,A.[LoanDate] = '" + txtLoanDate.Text + @"'
                                          ,A.[TermsOfPayment] = '" + txtTermsOfPayment.SelectedIndex + @"'
                                          ,A.[Amortization] = CASE WHEN '" + double.Parse(txtAmortization.Text) + @"' = '' THEN 0 ELSE " + double.Parse(txtAmortization.Text) + @" END
                                          ,A.[LoanInterest] = CASE WHEN '" + double.Parse(txtLoanInterest.Text) + @"' = '' THEN 0 ELSE " + double.Parse(txtLoanInterest.Text) + @" END
                                          ,A.[Status] = '" + _Status + @"'
                                          ,A.[Brand] = '" + txtBrand.Text + @"'
                                          ,A.[Terms] = '" + txtTerms.Text + @"'
                                          ,A.[Particular] = '" + txtParticular.Text.Replace("'","''") + @"'
                                          ,A.[LCPPrice] = CASE WHEN '" + txtLCPPrice.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtLCPPrice.Text) + @" END
                                          ,A.[SpotCashAmount] = CASE WHEN '" + txtSpotCashAmount.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtSpotCashAmount.Text) + @" END
                                          ,A.[DownPayment] = CASE WHEN '" + txtDownPayment.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtDownPayment.Text) + @" END
                                          ,A.[Rebate] = CASE WHEN '" + txtRebateAmount.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtRebateAmount.Text) + @" END
                                          ,A.[RebateApplication] = '" + txtRebateApplication.SelectedIndex + @"'
                                          ,A.[UpdateDate] = '" + DateTime.Now.ToString() + @"'
                                          ,A.[OrigBCode] = '" + txtBranchCode.Text + @"'
                                          ,A.[OrigBName] = '" + txtBranchName.Text + @"'
                                    FROM LoanFile A WHERE A.EmployeeNo = '" + txtEmpCode.Text+ @"' AND A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"' AND A.AccountCode = '" + txtLoanAccountCode.Text + @"'
                                  ";

            _msg = "Data Successfully Updated";
        }
        else
        {
            string _sqlCheck = "";
            _sqlCheck = @"SELECT 'TRUE' FROM LoanFile A WHERE A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"'";
            DataTable _tblSave;
            _tblSave = clsSQLClientFunctions.DataList(_ConCompany, _sqlCheck);

            if (_tblSave.Rows.Count > 1)
            {
                MessageBox.Show("Reference Number Already Exists");
                return;
            }



            _sqlSave = @"

                                    INSERT INTO [dbo].[LoanFile]
                                                ([EmployeeNo]
                                                ,[AccountCode]
                                                ,[LoanRefNo]
                                                ,[StartOfDeduction]
                                                ,[LoanAmount]
                                                ,[AmountGranted]
                                                ,[LoanDate]
                                                ,[TermsOfPayment]
                                                ,[Amortization]
                                                ,[LoanInterest]
                                                ,[Status]
                                                ,[Brand]
                                                ,[Terms]
                                                ,[Particular]
                                                ,[LCPPrice]
                                                ,[SpotCashAmount]
                                                ,[DownPayment]
                                                ,[Rebate]
,A.[RebateApplication]
,A.[CreateDate]
,A.[UpdateDate]
,A.OrigBCode, A.OrigBName
)
                                            VALUES
                                                ( '" + txtEmpCode.Text + @"' 
                                                , '" + txtLoanAccountCode.Text + @"' 
                                                , '" + txtLoanReferenceNo.Text + @"' 
                                                , '" + txtStartofDeduction.Text + @"' 
                                                , CASE WHEN '" + txtPayableLoanAmount.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtPayableLoanAmount.Text) + @" END
                                                , CASE WHEN '" + txtAmountGranted.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtAmountGranted.Text) + @" END
                                                , '" + txtLoanDate.Text + @"' 
                                                , '" + txtTermsOfPayment.SelectedIndex + @"' 
                                                , CASE WHEN '" + txtAmortization.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtAmortization.Text) + @" END
                                                , CASE WHEN '" + txtLoanInterest.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtLoanInterest.Text) + @" END
                                                , '" + _Status + @"' 
                                                , '" + txtBrand.Text + @"' 
                                                , '" + txtTerms.Text + @"' 
                                                , '" + txtParticular.Text.Replace("'", "''") + @"' 
                                                , CASE WHEN '" + txtLCPPrice.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtLCPPrice.Text) + @" END
                                                , CASE WHEN '" + txtSpotCashAmount.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtSpotCashAmount.Text) + @" END
                                                , CASE WHEN '" + txtDownPayment.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtDownPayment.Text) + @" END
                                                , CASE WHEN '" + txtRebateAmount.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtRebateAmount.Text) + @" END
,  '" + txtRebateApplication.SelectedIndex + @"' 
,  '" + DateTime.Now.ToString() + @"' 
,  '" + DateTime.Now.ToString() + @"' 
,  '" + txtBranchCode.Text + @"' 
,  '" + txtBranchName.Text + @"' 
)

                                  ";

            _msg = "Data Successfully Added";
        }


        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlSave);
        MessageBox.Show(_msg);

        _DisplayData();


        btnEmployee.Enabled = false;
        btnAccount.Enabled = false;


        btnSearch.Enabled = true;
        btnNew.Enabled = true;
        btnSave.Enabled = false;
        btnDelete.Enabled = true;
        btnCancel.Enabled = true;


        btnRowAdd.Enabled = true;
        btnRowEdit.Enabled = true;
        btnRowDelete.Enabled = true;

        txtEmpCode.ReadOnly = true;
        txtLoanAccountCode.ReadOnly = true;
        txtLoanReferenceNo.ReadOnly = true;


        txtBranchCode.ReadOnly = false;
        txtBranchName.ReadOnly = false;
        btnBranch.Enabled = true;


        txtAmountGranted.ReadOnly = false;
        txtPayableLoanAmount.ReadOnly = false;
        txtAmortization.ReadOnly = false;
        txtLoanInterest.ReadOnly = false;

        txtStartofDeduction.Enabled = true;
        txtTermsOfPayment.Enabled = true;
        txtRebateApplication.Enabled = true;


        chkActive.Enabled = true;
        txtLoanDate.Enabled = true;



        txtRebateAmount.ReadOnly = false;
        txtParticular.ReadOnly = false;
        txtBrand.ReadOnly = false;
        txtLCPPrice.ReadOnly = false;
        txtSpotCashAmount.ReadOnly = false;
        txtDownPayment.ReadOnly = false;
        txtTerms.ReadOnly = false;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {

        DialogResult res = MessageBox.Show("Are you sure you want to Delete the Selected Loan Account. This process is irreversible", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        if (res == DialogResult.Cancel)
        {
            return;
        }



        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

        string _sqlDelete = "";
        _sqlDelete = "DELETE FROM [LoanFile] WHERE EmployeeNo = '" + txtEmpCode.Text + @"' 
AND LoanRefNo = '" + txtLoanReferenceNo.Text + @"' 
AND AccountCode = '" + txtLoanAccountCode.Text + @"'";

        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlDelete);
        MessageBox.Show("Data Successfully Deleted!");



        btnEmployee.Enabled = false;
        btnAccount.Enabled = false;

        btnSearch.Enabled = true;
        btnNew.Enabled = true;
        btnSave.Enabled = false;
        btnDelete.Enabled = true;
        btnCancel.Enabled = true;


        txtEmpCode.ReadOnly = true;
        txtLoanAccountCode.ReadOnly = true;

        txtBranchCode.ReadOnly = true;
        txtBranchName.ReadOnly = true;
        btnBranch.Enabled = false;


        txtLoanReferenceNo.ReadOnly = true;
        txtAmountGranted.ReadOnly = true;
        txtPayableLoanAmount.ReadOnly = true;
        txtAmortization.ReadOnly = true;
        txtLoanInterest.ReadOnly = true;

        txtStartofDeduction.Enabled = false;
        txtTermsOfPayment.Enabled = false;
        txtRebateApplication.Enabled = false;


        chkActive.Enabled = false;
        txtLoanDate.Enabled = false;
        chkActive.Checked = false;


        txtRebateAmount.ReadOnly = true;
        txtParticular.ReadOnly = true;
        txtBrand.ReadOnly = true;
        txtLCPPrice.ReadOnly = true;
        txtSpotCashAmount.ReadOnly = true;
        txtDownPayment.ReadOnly = true;
        txtTerms.ReadOnly = true;


        txtEmpCode.Text = "";
        txtEmpName.Text = "";
        txtCompany.Text = "";
        txtLoanAccountCode.Text = "";

        txtBranchCode.Text = "";
        txtBranchName.Text = "";

        txtDescription.Text = "";
        txtLoanReferenceNo.Text = "";
        txtLoanDate.Text = DateTime.Today.ToString();
        txtAmountGranted.Text = "0.00";
        txtPayableLoanAmount.Text = "0.00";
        txtAmortization.Text = "0.00";
        txtLoanInterest.Text = "0.00";

        txtStartofDeduction.Text = "";
        txtTermsOfPayment.Text = "";
        txtRebateApplication.Text = "";

        txtTerms.Text = "";

        txtCash.Text = "0.00";
        txtPayroll.Text = "0.00";
        txtTotalCredit.Text = "0.00";
        txtBalance.Text = "0.00";

        txtRebateAmount.Text = "0.00";
        txtParticular.Text = "";
        txtBrand.Text = "";
        txtLCPPrice.Text = "0.00";
        txtSpotCashAmount.Text = "0.00";
        txtDownPayment.Text = "0.00";
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        btnEmployee.Enabled = true;
        btnAccount.Enabled = true;

        btnSearch.Enabled = false;
        btnNew.Enabled = false;
        btnSave.Enabled = true;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;


        txtEmpCode.ReadOnly = false;
        txtLoanAccountCode.ReadOnly = false;

        txtBranchCode.ReadOnly = false;
        txtBranchName.ReadOnly = false;
        btnBranch.Enabled = true;

        txtLoanReferenceNo.ReadOnly = false;
        txtAmountGranted.ReadOnly = false;
        txtPayableLoanAmount.ReadOnly = false;
        txtAmortization.ReadOnly = false;
        txtLoanInterest.ReadOnly = false;


        txtStartofDeduction.Enabled = true;
        txtTermsOfPayment.Enabled = true;
        txtRebateApplication.Enabled = true;


        chkActive.Enabled = true;
        txtLoanDate.Enabled = true;
        chkActive.Checked = true;


        txtRebateAmount.ReadOnly = false;
        txtParticular.ReadOnly = false;
        txtBrand.ReadOnly = false;
        txtLCPPrice.ReadOnly = false;
        txtSpotCashAmount.ReadOnly = false;
        txtDownPayment.ReadOnly = false;
        txtTerms.ReadOnly = false;




        txtEmpCode.Text = "";
        txtEmpName.Text = "";
        txtCompany.Text = "";
        txtLoanAccountCode.Text = "";

        txtBranchCode.Text = "";
        txtBranchName.Text = "";

        txtDescription.Text = "";
        txtLoanReferenceNo.Text = "";
        txtLoanDate.Text = DateTime.Today.ToString();
        txtAmountGranted.Text = "0.00";
        txtPayableLoanAmount.Text = "0.00";
        txtAmortization.Text = "0.00";
        txtLoanInterest.Text = "0.00";

        txtStartofDeduction.Text = "";
        txtTermsOfPayment.Text = "";
        txtRebateApplication.Text = "";

        txtTerms.Text = "";

        txtCash.Text = "0.00";
        txtPayroll.Text = "0.00";
        txtTotalCredit.Text = "0.00";
        txtBalance.Text = "0.00";

        txtRebateAmount.Text = "0.00";
        txtParticular.Text = "";
        txtBrand.Text = "";
        txtLCPPrice.Text = "0.00";
        txtSpotCashAmount.Text = "0.00";
        txtDownPayment.Text = "0.00";
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        btnEmployee.Enabled = false;
        btnAccount.Enabled = false;

        btnSearch.Enabled = true;
        btnNew.Enabled = true;
        btnSave.Enabled = false;
        btnDelete.Enabled = true;
        btnCancel.Enabled = true;

        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "LoanList";
        frmDataList.ShowDialog();

        txtCompany.Text = frmDataList._gCompany;
        txtEmpCode.Text = frmDataList._gEmployeeNo;
        txtLoanAccountCode.Text = frmDataList._gAccountCode;
        txtLoanReferenceNo.Text = frmDataList._gLoanRefNo;

        if (txtEmpCode.Text == "")
        {
            return;
        }



        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

        string _sqlList = "";

        _sqlList = @"SELECT CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName]
                            FROM vwsEmployees A WHERE A.EmployeeNo COLLATE Latin1_General_CI_AS = '" + txtEmpCode.Text + @"'";
        txtEmpName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "EmployeeName");


        _sqlList = @"SELECT A.AccountDesc
                            FROM vwsAccountCode A WHERE A.AccountCode  = '" + txtLoanAccountCode.Text + @"'";
        txtDescription.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "AccountDesc");


        _DisplayData();


        _sqlList = @"SELECT Z.Type, Z.PaymentDate, Z.ORNo, Z.[Principal Amount], Z.[Interest Amount], Z.Amount, Z.Remarks FROM (
                            SELECT ISNULL([Type],'CASH PAYMENT') AS [Type],CONVERT(nvarchar(30), [PaymentDate], 101) AS [PaymentDate],[ORNo],0 AS [Principal Amount] ,0 AS [Interest Amount],[Amount],[Remarks],[EmployeeNo],[AccountCode],[LoanRefNo] FROM LoanCashPayment
                            UNION ALL
                            SELECT 'PAYROLL' AS [Type],CONCAT(A.[PayrollPeriod], ' (', CONVERT(nvarchar(30), B.DateOne, 101),' - ', CONVERT(nvarchar(30), B.DateTwo, 101), ') '),NULL as [ORNo],ISNULL(A.PrincipalAmt,0) AS [Principal Amount] ,ISNULL(A.InterestAmt,0) AS [Interest Amount] ,A.[Amount], NULL as [Remarks],[EmployeeNo],[AccountCode],[LoanRefenceNo] FROM PayrollDetails A INNER JOIN PayrollPeriod B
                            ON A.PayrollPeriod = B.PayrollPeriod) Z 
                            WHERE Z.EmployeeNo = '" + txtEmpCode.Text  + @"' AND Z.AccountCode = '" + txtLoanAccountCode.Text + @"' AND Z.LoanRefNo = '" + txtLoanReferenceNo.Text + @"'";
        DataTable _tblList;
        _tblList = clsSQLClientFunctions.DataList(_ConCompany, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);




        txtEmpCode.ReadOnly = true;
        txtLoanAccountCode.ReadOnly = true;
        txtLoanReferenceNo.ReadOnly = true;

        txtBranchCode.ReadOnly = false;
        txtBranchName.ReadOnly = false;
        btnBranch.Enabled = true;

        txtAmountGranted.ReadOnly = false;
        txtPayableLoanAmount.ReadOnly = false;
        txtAmortization.ReadOnly = false;
        txtLoanInterest.ReadOnly = false;

        txtStartofDeduction.Enabled = true;
        txtTermsOfPayment.Enabled = true;
        txtRebateApplication.Enabled = true;

        chkActive.Enabled = true;
        txtLoanDate.Enabled = true;


        txtRebateAmount.ReadOnly = false;
        txtParticular.ReadOnly = false;
        txtBrand.ReadOnly = false;
        txtLCPPrice.ReadOnly = false;
        txtSpotCashAmount.ReadOnly = false;
        txtDownPayment.ReadOnly = false;
        txtTerms.ReadOnly = false;

        btnRowAdd.Enabled = true;
        btnRowEdit.Enabled = true;
        btnRowDelete.Enabled = true;
    }


    private void _DisplayData()
    {
        try
        {
            string _sqlList;
            _sqlList = @"SELECT A.LoanDate,ISNULL(A.AmountGranted,0) AS AmountGranted
,ISNULL(A.LoanAmount,0) AS LoanAmount
,ISNULL(A.Amortization,0) AS Amortization,A.StartOfDeduction,A.TermsOfPayment
,A.Terms,A.TotalCash,A.TotalPayroll,A.TotalCredit
,ISNULL(A.Rebate,0) AS Rebate
,A.Particular
,A.Brand
,ISNULL(A.LCPPrice,0) AS LCPPrice
,ISNULL(A.SpotCashAmount,0) AS SpotCashAmount
,ISNULL(A.DownPayment,0) AS DownPayment,A.Status
,ISNULL(A.RebateApplication,'3') AS RebateApplication
,ISNULL(A.LoanInterest,'0') AS LoanInterest
,A.OrigBCode, A.OrigBName
                            FROM [vwsLoanFile] A WHERE A.[EmployeeNo]  = '" + txtEmpCode.Text + @"' AND A.[AccountCode]  = '" + txtLoanAccountCode.Text + @"'  
                            AND A.[Company]  = '" + txtCompany.Text + @"'  AND A.[LoanRefNo]  = '" + txtLoanReferenceNo.Text + @"'";


            txtBranchCode.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "OrigBCode");
            txtBranchName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "OrigBName");

            txtLoanDate.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "LoanDate");
            txtAmountGranted.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "AmountGranted").ToString("N2");
            txtPayableLoanAmount.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "LoanAmount").ToString("N2");
            txtAmortization.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Amortization").ToString("N2");
            txtLoanInterest.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "LoanInterest").ToString("N2");



            txtStartofDeduction.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "StartOfDeduction");
            txtTermsOfPayment.SelectedIndex = int.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "TermsOfPayment"));

            txtTerms.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Terms");

            //txtCash.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "TotalCash").ToString("N2");
            //txtPayroll.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "TotalPayroll").ToString("N2");
            //txtTotalCredit.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "TotalCredit").ToString("N2");
            //txtBalance.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Terms");

            txtRebateAmount.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Rebate").ToString("N2");
            txtParticular.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Particular");
            txtBrand.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Brand");
            txtLCPPrice.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "LCPPrice").ToString("N2");
            txtSpotCashAmount.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "SpotCashAmount").ToString("N2");
            txtDownPayment.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "DownPayment").ToString("N2");

            txtRebateApplication.SelectedIndex = int.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "RebateApplication"));


            if (clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Status") == "0")
            {
                chkActive.Checked = true;
            }
            else
            {
                chkActive.Checked = false;
            }

            string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

           //_sqlList = @"SELECT Q.LoanAmount, Q.[Cash Payment], Q.[Payroll Payment], (Q.[Cash Payment] + Q.[Payroll Payment]) AS [Total Payment]  
           //     , (Q.LoanAmount - (Q.[Cash Payment] + Q.[Payroll Payment])) AS [Balance]
           //     FROM (

           //     SELECT A.LoanAmount,

           //     ISNULL((SELECT SUM(Z.Amount) FROM LoanCashPayment Z 
           //     WHERE Z.LoanRefNo = A.LoanRefNo AND Z.EmployeeNo = A.EmployeeNo 
           //     AND Z.AccountCode = A.AccountCode),0) AS 'Cash Payment',


           //     ISNULL((SELECT SUM(Z.Amount) FROM PayrollDetails Z 
           //     WHERE Z.LoanRefenceNo = A.LoanRefNo AND Z.EmployeeNo = A.EmployeeNo
           //     AND Z.AccountCode = A.AccountCode),0) AS 'Payroll Payment'

           //     FROM LoanFile A 
           //     WHERE A.[EmployeeNo]  = '" + txtEmpCode.Text + @"' AND A.[AccountCode]  = '" + txtLoanAccountCode.Text + @"' AND A.[LoanRefNo]  =  '" + txtLoanReferenceNo.Text + @"'
           //     ) Q
           //     ";

            _sqlList = @"SELECT * FROM [dbo].[fnGetBalancePerEmployeeLoan] (  '" + txtEmpCode.Text + @"' ,'" + txtLoanAccountCode.Text + @"', '" + txtLoanReferenceNo.Text + @"')
                ";


            txtCash.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Cash Amount").ToString("N2");
            txtPayroll.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Payroll Amount").ToString("N2");
            txtTotalCredit.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Total Payment").ToString("N2");
            txtBalance.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Balance").ToString("N2");




        }
        catch
        {

        }
       
    }

    private void btnRowAdd_Click(object sender, EventArgs e)
    {
        frmLoanPayment frmLoanPayment = new frmLoanPayment();

        frmLoanPayment._Company = txtCompany.Text;
        frmLoanPayment._Employee = txtEmpCode.Text;
        frmLoanPayment._LoanRefNo = txtLoanReferenceNo.Text;
        frmLoanPayment._AccountCode = txtLoanAccountCode.Text;

        frmLoanPayment._LoanPayrollType = "";
        frmLoanPayment._LoanPaymentDate = DateTime.Today.ToShortDateString();
        frmLoanPayment._LoanORNumber = "";
        frmLoanPayment._LoanPaymentAmount = "";
        frmLoanPayment._LoanRemarks = "";
        frmLoanPayment._AddType = "ADD";
        frmLoanPayment.ShowDialog();


        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

        string _sqlList = @"SELECT Z.Type, Z.PaymentDate, Z.ORNo, Z.[Principal Amount], Z.[Interest Amount], Z.Amount, Z.Remarks FROM (
                            SELECT ISNULL([Type],'CASH PAYMENT') AS [Type],CONVERT(nvarchar(30), [PaymentDate], 101) AS [PaymentDate],[ORNo],0 AS [Principal Amount] ,0 AS [Interest Amount],[Amount],[Remarks],[EmployeeNo],[AccountCode],[LoanRefNo] FROM LoanCashPayment
                            UNION ALL
                            SELECT 'PAYROLL' AS [Type],CONCAT(A.[PayrollPeriod], ' (', CONVERT(nvarchar(30), B.DateOne, 101),' - ', CONVERT(nvarchar(30), B.DateTwo, 101), ') '),NULL as [ORNo],ISNULL(A.PrincipalAmt,0) AS [Principal Amount] ,ISNULL(A.InterestAmt,0) AS [Interest Amount] ,A.[Amount], NULL as [Remarks],[EmployeeNo],[AccountCode],[LoanRefenceNo] FROM PayrollDetails A INNER JOIN PayrollPeriod B
                            ON A.PayrollPeriod = B.PayrollPeriod) Z 
                            WHERE Z.EmployeeNo = '" + txtEmpCode.Text + @"' AND Z.AccountCode = '" + txtLoanAccountCode.Text + @"' AND Z.LoanRefNo = '" + txtLoanReferenceNo.Text + @"'";
        DataTable _tblList;
        _tblList = clsSQLClientFunctions.DataList(_ConCompany, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
    }

    private void btnRowEdit_Click(object sender, EventArgs e)
    {
        if (_LoanPayrollType == "PAYROLL")
        {
            MessageBox.Show("Editing This Payment is not Allowed!");
            return;
        }


        frmLoanPayment frmLoanPayment = new frmLoanPayment();

        frmLoanPayment._Company = txtCompany.Text;
        frmLoanPayment._Employee = txtEmpCode.Text;
        frmLoanPayment._LoanRefNo = txtLoanReferenceNo.Text;
        frmLoanPayment._AccountCode = txtLoanAccountCode.Text;


        frmLoanPayment._LoanPayrollType = _LoanPayrollType;
        frmLoanPayment._LoanPaymentDate = _LoanPaymentDate;
        frmLoanPayment._LoanORNumber = _LoanORNumber;
        frmLoanPayment._LoanPaymentAmount = _LoanPaymentAmount;
        frmLoanPayment._LoanRemarks = _LoanRemarks;
        frmLoanPayment._AddType = "EDIT";


        frmLoanPayment.ShowDialog();
    }

    private void btnRowDelete_Click(object sender, EventArgs e)
    {
        if(_LoanPayrollType == "PAYROLL")
        {
            MessageBox.Show("Deleting This Payment is not Allowed!");
            return;
        }

        DialogResult res = MessageBox.Show("Are you sure you want to Delete the Selected Payment", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        if (res == DialogResult.Cancel)
        {
            return;
        }

        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

        string _SQLRebate = @"
                 DELETE FROM [LoanCashPayment] WHERE [EmployeeNo] = '" + txtEmpCode.Text + @"' AND [AccountCode] = '" + txtLoanAccountCode.Text + @"' 
AND [LoanRefNo] = '" + txtLoanReferenceNo.Text + @"' AND [ORNo] = '" + _LoanORNumber + @"' AND [PaymentDate] = '" + _LoanPaymentDate + @"' 
                                                ";

        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLRebate);

        MessageBox.Show("Loan Payment Successfully Deleted!");

        string _sqlList = @"SELECT Z.Type, Z.PaymentDate, Z.ORNo, Z.[Principal Amount], Z.[Interest Amount], Z.Amount, Z.Remarks FROM (
                            SELECT ISNULL([Type],'CASH PAYMENT') AS [Type],CONVERT(nvarchar(30), [PaymentDate], 101) AS [PaymentDate],[ORNo],0 AS [Principal Amount] ,0 AS [Interest Amount],[Amount],[Remarks],[EmployeeNo],[AccountCode],[LoanRefNo] FROM LoanCashPayment
                            UNION ALL
                            SELECT 'PAYROLL' AS [Type],CONCAT(A.[PayrollPeriod], ' (', CONVERT(nvarchar(30), B.DateOne, 101),' - ', CONVERT(nvarchar(30), B.DateTwo, 101), ') '),NULL as [ORNo],ISNULL(A.PrincipalAmt,0) AS [Principal Amount] ,ISNULL(A.InterestAmt,0) AS [Interest Amount] ,A.[Amount], NULL as [Remarks],[EmployeeNo],[AccountCode],[LoanRefenceNo] FROM PayrollDetails A INNER JOIN PayrollPeriod B
                            ON A.PayrollPeriod = B.PayrollPeriod) Z 
                            WHERE Z.EmployeeNo = '" + txtEmpCode.Text + @"' AND Z.AccountCode = '" + txtLoanAccountCode.Text + @"' AND Z.LoanRefNo = '" + txtLoanReferenceNo.Text + @"'";
        DataTable _tblList;
        _tblList = clsSQLClientFunctions.DataList(_ConCompany, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
    }

    private void btnEmployee_Click(object sender, EventArgs e)
    {
        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "EmployeeList";
        frmDataList.ShowDialog();

        txtCompany.Text = frmDataList._gCompany;
        txtEmpCode.Text = frmDataList._gEmployeeNo;

        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

        string _sqlList = "";

        _sqlList = @"SELECT CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName]
                            FROM vwsEmployees A WHERE A.EmployeeNo COLLATE Latin1_General_CI_AS = '" + txtEmpCode.Text + @"'";
        txtEmpName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "EmployeeName");

    }

    private void btnAccount_Click(object sender, EventArgs e)
    {
        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "AccountList";
        frmDataList.ShowDialog();

        txtLoanAccountCode.Text = frmDataList._gAccountCode;
        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

        string _sqlList = "";
        _sqlList = @"SELECT A.AccountDesc
                            FROM vwsAccountCode A WHERE A.AccountCode  = '" + txtLoanAccountCode.Text + @"'";
        txtDescription.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "AccountDesc");
    }

    private void txt_TextChanged(object sender, EventArgs e)
    {

    }

    private void txt_KeyPress(object sender, KeyPressEventArgs e)
    {
        btnEmployee.Enabled = true;
        btnAccount.Enabled = true;
        btnBranch.Enabled = true;

        btnSearch.Enabled = false;
        btnNew.Enabled = false;
        btnSave.Enabled = true;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;
    }

    private void txt_MouseDown(object sender, MouseEventArgs e)
    {
        btnEmployee.Enabled = true;
        btnAccount.Enabled = true;
        btnBranch.Enabled = true;

        btnSearch.Enabled = false;
        btnNew.Enabled = false;
        btnSave.Enabled = true;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;
    }

    private void txt_Leave(object sender, EventArgs e)
    {
        if (txtAmountGranted.Text == "") { txtAmountGranted.Text = "0.00"; }
        if (txtPayableLoanAmount.Text == "") { txtPayableLoanAmount.Text = "0.00"; }
        if (txtAmortization.Text == "") { txtAmortization.Text = "0.00"; }
        if (txtLoanInterest.Text == "") { txtLoanInterest.Text = "0.00"; }

        if (txtRebateAmount.Text == "") { txtRebateAmount.Text = "0.00"; }
        if (txtLCPPrice.Text == "") { txtLCPPrice.Text = "0.00"; }
        if (txtSpotCashAmount.Text == "") { txtSpotCashAmount.Text = "0.00"; }
        if (txtDownPayment.Text == "") { txtDownPayment.Text = "0.00"; }
    }

    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            _LoanPayrollType = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            _LoanPaymentDate = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            _LoanORNumber = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            _LoanPaymentAmount = dgvDisplay.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();
            _LoanRemarks = dgvDisplay.Rows[e.RowIndex].Cells[6].Value.ToString().Trim();

        }
        catch
        {

        }
    }

    private void btnBranch_Click(object sender, EventArgs e)
    {
        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "BranchList";
        frmDataList.ShowDialog();
        
        txtBranchCode.Text = frmDataList._gBranchCode;
        txtBranchName.Text = frmDataList._gBranchName;
    }

    private void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        btnEmployee.Enabled = true;
        btnAccount.Enabled = true;
        btnBranch.Enabled = true;

        btnSearch.Enabled = false;
        btnNew.Enabled = false;
        btnSave.Enabled = true;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;
    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click_1(object sender, EventArgs e)
    {

    }

    private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void chkRelativeAccount_CheckedChanged(object sender, EventArgs e)
    {
        if(chkRelativeAccount.Checked == true)
        {
            txtRelativeName.ReadOnly = false;
        }
        else
        {
            txtRelativeName.ReadOnly = true;
        }
    }
}