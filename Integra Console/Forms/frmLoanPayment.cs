using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmLoanPayment : Form
{

    public static string _Company;
    public static string _Employee;
    public static string _AccountCode;
    public static string _LoanRefNo;

    public static string _LoanPayrollType;
    public static string _LoanPaymentDate;
    public static string _LoanORNumber;
    public static string _LoanPaymentAmount;
    public static string _LoanRemarks;
    public static string _AddType;

    public frmLoanPayment()
    {
        InitializeComponent();
    }

    private void frmLoanPayment_Load(object sender, EventArgs e)
    {
        cboPaymentType.Items.Add("CASH PAYMENT");
        cboPaymentType.Items.Add("CASH REBATE");
        cboPaymentType.Items.Add("TERM MOD");
        cboPaymentType.Items.Add("INTEREST PAYMENT");
        cboPaymentType.Items.Add("PAYMENT BY EMPLOYEE");
        cboPaymentType.Items.Add("TRANFERRED TO BRANCH");
        cboPaymentType.Items.Add("REPO AMOUNT");
        cboPaymentType.Items.Add("SILP AMOUNT");
        cboPaymentType.Items.Add("13TH MONTH AMOUNT");
        cboPaymentType.Items.Add("OVER PAYMENT");
        cboPaymentType.Items.Add("REFUND");
        cboPaymentType.Items.Add("OTHERS");

        cboPaymentType.SelectedIndex = 0;
        
        cboPaymentType.Text = _LoanPayrollType;
        dtpDate.Text = _LoanPaymentDate;
        txtORNo.Text = _LoanORNumber;
        txtAmount.Text = _LoanPaymentAmount;
        txtRemarks.Text = _LoanRemarks;
}

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (char.IsDigit(e.KeyChar))
        {
            return;
        }
        if (e.KeyChar == (char)Keys.Back)
        {
            return;
        }
        if (e.KeyChar == '.' && !txtORNo.Text.Contains('.'))
        {
            return;
        }
        if (e.KeyChar == '-' && !txtORNo.Text.Contains('-'))
        {
            return;
        }

        e.Handled = true;
    }

    private void txtAmount_Leave(object sender, EventArgs e)
    {
        try
        {
            txtAmount.Text = double.Parse(txtAmount.Text).ToString("N2");
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
        string _SQLRebate = "";
        if (_AddType == "ADD")
        {
            _SQLRebate = @"
                INSERT INTO [dbo].[LoanCashPayment]
                           ([EmployeeNo]
                           ,[AccountCode]
                           ,[LoanRefNo]
                           ,[ORNo]
                           ,[PaymentDate]
                           ,[Amount]
                           ,[Remarks]
                           ,[Type]
                           ,[CreateDate]
                           ,[UpdateDate])
                     VALUES
                           ('" + _Employee + @"'
                           ,'" + _AccountCode + @"'
                           ,'" + _LoanRefNo + @"'
                           ,'" + txtORNo.Text + @"'
                           ,'" + dtpDate.Text + @"'
                           ,'" + txtAmount.Text + @"'
                           ,'" + txtRemarks.Text + @"'
                           ,'" + cboPaymentType.Text + @"'
                           ,'" + DateTime.Today + @"'
                           ,'" + DateTime.Today + @"')

                                                ";
        }

        if (_AddType == "EDIT")
        {
            _SQLRebate = @"
UPDATE [dbo].[LoanCashPayment]
   SET 
     
       [PaymentDate] = '" + dtpDate.Text + @"'
      ,[Amount] ='" + txtAmount.Text + @"'
      ,[Remarks] = '" + txtRemarks.Text + @"'
      ,[Type] = '" + cboPaymentType.Text + @"'
      ,[UpdateDate] = '" + DateTime.Today + @"'
 WHERE [EmployeeNo] = '" + _Employee + @"'
      AND [AccountCode] = '" + _AccountCode + @"'
      AND [LoanRefNo] = '" + _LoanRefNo + @"'
AND   [ORNo] = '" + txtORNo.Text + @"'

                                                ";
        }

        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLRebate);
        MessageBox.Show("Payment Successfully Added!");
        Close();

    }

    private void txtAmount_TextChanged(object sender, EventArgs e)
    {

    }
}