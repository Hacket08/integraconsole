using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmLoanFileData : Form
{

    public static string _LoanPayrollType;
    public static string _LoanPaymentDate;
    public static string _LoanORNumber;
    public static string _LoanPaymentAmount;
    public static string _LoanRemarks;
    public static string _AddType;
    public static string _Status;




    private DataTable _dtInstDisplay = new DataTable();



    private void CreateTable()
    {

        _dtInstDisplay.Columns.Add("Amort No", typeof(int));
        _dtInstDisplay.Columns.Add("Date Due", typeof(DateTime));
        _dtInstDisplay.Columns.Add("Payroll Period", typeof(string));
        _dtInstDisplay.Columns.Add("Date Paid", typeof(DateTime));
        _dtInstDisplay.Columns.Add("Prin Amount", typeof(double));
        _dtInstDisplay.Columns.Add("Payment Amt", typeof(double));
        _dtInstDisplay.Columns.Add("Rebate", typeof(double));
        _dtInstDisplay.Columns.Add("Penalty", typeof(double));
        _dtInstDisplay.Columns.Add("Balance", typeof(double));
        _dtInstDisplay.Columns.Add("Remarks", typeof(string));
        _dtInstDisplay.Columns.Add("Type", typeof(string));



    }

    public frmLoanFileData()
    {
        InitializeComponent();
    }

    #region Function Group
    private void ReadOnlyText( bool _value)
    {

        txtABranchName.ReadOnly = _value;
        txtABranchCode.ReadOnly = _value;
        txtBranchName.ReadOnly = _value;
        txtBranchCode.ReadOnly = _value;
        txtLoanInterest.ReadOnly = _value;
        txtRebateAmount.ReadOnly = _value;
        txtParticular.ReadOnly = _value;
        txtDownPayment.ReadOnly = _value;
        txtSpotCashAmount.ReadOnly = _value;
        txtLCPPrice.ReadOnly = _value;
        txtTerms.ReadOnly = _value;
        txtBrand.ReadOnly = _value;
        txtBalance.ReadOnly = _value;
        txtTotalCredit.ReadOnly = _value;
        txtPayroll.ReadOnly = _value;
        txtCash.ReadOnly = _value;
        txtAmortization.ReadOnly = _value;
        txtPayableLoanAmount.ReadOnly = _value;
        txtAmountGranted.ReadOnly = _value;
        txtLoanReferenceNo.ReadOnly = _value;
        txtLoanDate.ReadOnly = _value;
        txtDescription.ReadOnly = _value;
        txtLoanAccountCode.ReadOnly = _value;
        txtCompany.ReadOnly = _value;
        txtEmpCode.ReadOnly = _value;
        txtEmpName.ReadOnly = _value;
    }

    private void ClearData()
    {
        chkRelativeAccount.Checked = false;
        txtRelativeName.Text = "";
        txtEmpCode.Text = "";
        txtEmpName.Text = "";
        txtCompany.Text = "";
        txtLoanAccountCode.Text = "";
        txtABranchCode.Text = "";
        txtABranchName.Text = "";
        txtBranchCode.Text = "";
        txtBranchName.Text = "";

        txtDescription.Text = "";
        txtLoanReferenceNo.Text = "";
        txtLoanDate.Text = "";
        txtCardCode.Text = "";
        txtDocEntry.Text = "";
        //txtLoanDate.Text = DateTime.Today.ToString();
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


        txtTransferredDate.Text = "";
        dtFirstDueDate.Text = "";
        dtDueDate.Text = "";
        dtMonthlyDueDate.Text = "";


        chkIsBranchAccount.Checked = false;
        chkIsTransferred.Checked = false;
        chkPostToSAP.Checked = false;
        ViewAllP.Checked = false;
    }
    private void SearchGroup()
    {
        //ClearData();
        PerFormControl(false);

        lblBrowseEmployeeNo.Visible = false;
        lblBrowseReleasingBranch.Visible = false;
        lblBrowseLoanAccountCode.Visible = false;

        lblSearchEmployeeNo.Visible = true;
        lblSearchLoanAccountCode.Visible = true;
        lblSearchLoanReferenceNo.Visible = true;
        lblSearchEmployeeName.Visible = true;

        //ReadOnlyText(true);
        //PerFormControl(true);

        //Start Textbox Group
        txtEmpCode.ReadOnly = false;
        txtLoanAccountCode.ReadOnly = false;
        txtLoanReferenceNo.ReadOnly = false;
        txtLoanDate.ReadOnly = false;
        txtEmpName.ReadOnly = false;

        txtLoanDate.Enabled = false;
        txtCardCode.ReadOnly = true;
        txtDocEntry.ReadOnly = true;

        txtEmpCode.BackColor = Color.LightGoldenrodYellow;
        txtLoanAccountCode.BackColor = Color.LightGoldenrodYellow;
        txtLoanReferenceNo.BackColor = Color.LightGoldenrodYellow;
        txtEmpName.BackColor = Color.LightGoldenrodYellow;
        //End


        txtTermsOfPayment.Enabled = false;
        txtStartofDeduction.Enabled = false;
        txtRebateApplication.Enabled = false;

        btnSearch.Enabled = true;
        btnNew.Enabled = true;
        btnSave.Enabled = false;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;

        btnRowAdd.Enabled = true;
        btnRowEdit.Enabled = true;
        btnRowDelete.Enabled = true;

        chkDisplayZeroAccount.Enabled = true;
        chkRelativeAccount.Enabled = false;
        chkActive.Enabled = true;
        chkActive.Checked = true;


        chkIsBranchAccount.Checked = false;
        chkIsTransferred.Checked = false;
        chkPostToSAP.Checked = false;
        ViewAllP.Checked = false;



        int _IsViewAll = 0;
        _IsViewAll = 1;
        txtStartofDeduction.Items.Clear();
        txtStartofDeduction.Items.Add("");
        foreach (DataRow row in clsFunctions.getListofPayrollPeriod(txtABranchCode.Text, _IsViewAll).Rows)
        {
            txtStartofDeduction.Items.Add(row[0].ToString());
        }


        _IsViewAll = 1;
        cboPostToSAP.Items.Clear();
        cboPostToSAP.Items.Add("");
        foreach (DataRow row in clsFunctions.getListofPayrollPeriod(txtABranchCode.Text, _IsViewAll).Rows)
        {
            cboPostToSAP.Items.Add(row[0].ToString());
        }
    }
    private void NewGroup()
    {
        //ClearData();
        PerFormControl(true);

        txtDate.Text = DateTime.Today.ToString("MM/dd/yyyy");

        lblBrowseEmployeeNo.Visible = true;
        lblBrowseReleasingBranch.Visible = true;
        lblBrowseLoanAccountCode.Visible = true;

        lblSearchEmployeeNo.Visible = false;
        lblSearchLoanAccountCode.Visible = false;
        lblSearchLoanReferenceNo.Visible = false;
        lblSearchEmployeeName.Visible = false;

        //ReadOnlyText(false);
        //PerFormControl(false);

        #region Textbox Group
        txtEmpCode.ReadOnly = true;
        txtEmpName.ReadOnly = true;
        txtCompany.ReadOnly = true;
        txtDate.ReadOnly = true;
        dtMonthlyDueDate.Enabled = false;
        dtDueDate.Enabled = false;


        txtLoanAccountCode.ReadOnly = true;
        txtDescription.ReadOnly = true;
        txtABranchCode.ReadOnly = true;
        txtABranchName.ReadOnly = true;
        txtBranchCode.ReadOnly = true;
        txtBranchName.ReadOnly = true;
        txtCash.ReadOnly = true;
        txtPayroll.ReadOnly = true;
        txtTotalCredit.ReadOnly = true;
        txtBalance.ReadOnly = true;

        txtEmpCode.BackColor = SystemColors.Control;
        txtLoanAccountCode.BackColor = SystemColors.Control;
        txtLoanReferenceNo.BackColor = SystemColors.Window;
        txtEmpName.BackColor = SystemColors.Control;


        txtAmountGranted.Text = "0.00";
        txtPayableLoanAmount.Text = "0.00";
        txtAmortization.Text = "0.00";
        txtLoanInterest.Text = "0.00";
        txtRebateAmount.Text = "0.00";
        txtLCPPrice.Text = "0.00";
        txtSpotCashAmount.Text = "0.00";
        txtDownPayment.Text = "0.00";

        txtLoanDate.Text = "";
        txtCardCode.Text = "";
        txtDocEntry.Text = "";

        dtFirstDueDate.Text = "";
        dtDueDate.Text = "";
        dtMonthlyDueDate.Text = "";
        #endregion


        txtLoanDate.Enabled = true;
        txtCardCode.ReadOnly = true;
        txtDocEntry.ReadOnly = true;
        txtTermsOfPayment.Enabled = true;
        txtStartofDeduction.Enabled = true;
        txtRebateApplication.Enabled = true;

        btnSearch.Enabled = true;
        btnNew.Enabled = true;
        btnSave.Enabled = false;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;

        btnRowAdd.Enabled = true;
        btnRowEdit.Enabled = true;
        btnRowDelete.Enabled = true;

        chkDisplayZeroAccount.Enabled = false;
        chkRelativeAccount.Enabled = true;
        chkActive.Enabled = true;
        chkActive.Checked = true;


        chkIsBranchAccount.Checked = false;
        chkIsTransferred.Checked = false;
        chkPostToSAP.Checked = false;
        ViewAllP.Checked = false;


        txtAdviceNo.ReadOnly = true;
        txtTransferredDate.ReadOnly = true;
        cboPostToSAP.Enabled = false;
    }

    private void SavedGroup()
    {      
        btnNew.Enabled = false;
        btnSearch.Enabled = false;
        btnSave.Enabled = true;
        btnDelete.Enabled = true;
        btnCancel.Enabled = true;
    }

    #endregion

    private void PerFormControl(bool _value)
    {
        #region Form Control
        int i = 0;
        foreach (Control cLayer1 in this.Controls)
        {
            if (cLayer1 is MaskedTextBox)
            {
                cLayer1.Text = "";
                if (_value == true)
                {
                    ((MaskedTextBox)cLayer1).ReadOnly = false;
                }
                else
                {
                    ((MaskedTextBox)cLayer1).ReadOnly = true;
                }

            }
            if (cLayer1 is TextBox)
            {
                cLayer1.Text = "";
                if (_value == true)
                {
                    ((TextBox)cLayer1).ReadOnly = false;
                }
                else
                {
                    ((TextBox)cLayer1).ReadOnly = true;
                }

            }
            if (cLayer1 is ComboBox)
            {
                ((ComboBox)cLayer1).SelectedIndex = 0;
                cLayer1.Enabled = _value;
            }
            if (cLayer1 is Button)
            {
                cLayer1.Enabled = _value;
            }
            if (cLayer1 is DateTimePicker)
            {
                ((DateTimePicker)cLayer1).Value = DateTime.Today;
                cLayer1.Enabled = _value;
            }
            if (cLayer1 is Label)
            {
                if (cLayer1.Name.StartsWith("lbl"))
                {
                    cLayer1.Visible = _value;
                }
            }

            // tab control
            foreach (Control cLayer2 in cLayer1.Controls)
            {
                foreach (Control cLayer3 in cLayer2.Controls)
                {
                    if (cLayer3 is MaskedTextBox)
                    {
                        cLayer3.Text = "";
                        if (_value == true)
                        {
                            ((MaskedTextBox)cLayer3).ReadOnly = false;
                        }
                        else
                        {
                            ((MaskedTextBox)cLayer3).ReadOnly = true;
                        }
                    }
                    if (cLayer3 is TextBox)
                    {
                        cLayer3.Text = "";
                        if (_value == true)
                        {
                            ((TextBox)cLayer3).ReadOnly = false;
                        }
                        else
                        {
                            ((TextBox)cLayer3).ReadOnly = true;
                        }
                    }

                    if (cLayer3 is ComboBox)
                    {
                        //((ComboBox)cLayer3).SelectedIndex = 0;
                        cLayer3.Enabled = _value;
                    }

                    if (cLayer3 is Button)
                    {
                        cLayer3.Enabled = _value;
                    }
                    if (cLayer3 is DateTimePicker)
                    {
                        ((DateTimePicker)cLayer3).Value = DateTime.Today;
                        cLayer3.Enabled = _value;
                    }
                    if (cLayer3 is CheckBox)
                    {
                        cLayer3.Enabled = _value;
                    }
                    if (cLayer3 is Label)
                    {
                        if (cLayer3.Name.StartsWith("lbl"))
                        {
                            cLayer3.Visible = _value;
                        }
                    }
                }
            }
        }

        i++;
        #endregion
    }

    private void frmLoanFileData_Load(object sender, EventArgs e)
    {

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

        //DataTable _DataTable;
        //string _SQLSyntax;
        //_SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A ORDER BY A.[PayrollPeriod] DESC";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);


        //txtStartofDeduction.Items.Clear();
        //txtStartofDeduction.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    txtStartofDeduction.Items.Add(row[0].ToString());
        //}

        btnSearch_Click(sender, e);

        txtDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
        CreateTable();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        if (btnSave.Enabled == true)
        {
            NewGroup();


            string _sqlList = @"SELECT NULL AS Type, NULL AS PaymentDate,  NULL AS ORNo,  NULL AS [Principal Amount],  NULL AS [Interest Amount],  NULL AS Amount,  NULL AS Remarks";
            DataTable _tblList;
            _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
            clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);



            _sqlList = @"SELECT A.[Amort No]
      ,A.[Date Due]
      ,A.[Payroll Period]
      ,A.[Date Paid]
      ,A.[Prin Amount]
      ,A.[Payment Amt]
      ,A.[Rebate]
      ,A.[Penalty]
      ,A.[Balance]
      ,A.[Remarks]
      ,A.[Type]
  FROM [Installment] A
  WHERE A.[Emp No] = ''
      AND A.[Loan Ref No] = ''
      AND A.[Account Code] = ''";
            _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
            clsFunctions.DataGridViewSetup(dgvInstDisplay, _tblList, "Installment");
        }
        else
        {
            Close();
        }
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        _AddType = "EDIT";
        SearchGroup();
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        _AddType = "ADD";
        NewGroup();

    }

    private void lblBrowseEmployeeNo_Click(object sender, EventArgs e)
    {
        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "EmployeeList";
        frmDataList.ShowDialog();

        txtCompany.Text = frmDataList._gCompany;
        txtEmpCode.Text = frmDataList._gEmployeeNo;

        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

        string _sqlList = "";

        _sqlList = @"SELECT A.[EmployeeName], B.BCode, B.BName  FROM vwsEmployees A INNER JOIN  vwsDepartmentList B ON A.Department = B.DepartmentCode
        WHERE A.EmployeeNo COLLATE Latin1_General_CI_AS = '" + txtEmpCode.Text + @"'";

        txtEmpName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "EmployeeName");
        txtABranchCode.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "BCode");
        txtABranchName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "BName");


        int _IsViewAll = 0;
        if (chkAll.Checked == true)
        {
            _IsViewAll = 1;
        }
        else
        {
            _IsViewAll = 0;
        }

        txtStartofDeduction.Items.Clear();
        txtStartofDeduction.Items.Add("");
        foreach (DataRow row in clsFunctions.getListofPayrollPeriod(txtABranchCode.Text, _IsViewAll).Rows)
        {
            txtStartofDeduction.Items.Add(row[0].ToString());
        }


        _IsViewAll = 0;
        if (ViewAllP.Checked == true)
        {
            _IsViewAll = 1;
        }
        else
        {
            _IsViewAll = 0;
        }

        cboPostToSAP.Items.Clear();
        cboPostToSAP.Items.Add("");
        foreach (DataRow row in clsFunctions.getListofPayrollPeriod(txtABranchCode.Text, _IsViewAll).Rows)
        {
            cboPostToSAP.Items.Add(row[0].ToString());
        }


        //        DataTable _DataTable;
        //        string _SQLSyntax;
        //        if (txtABranchCode.Text == "")
        //        {
        //            return;
        //        }

        //        if (chkAll.CheckState == CheckState.Checked)
        //        {

        //            _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        //        }
        //        else
        //        {
        //            _SQLSyntax = @"SELECT DISTINCT A.[PayrollPeriod],A.[DateOne],A.[DateTwo],A.[IsLocked] 
        //FROM vwsPayrollPeriod A LEFT JOIN PayrollLocker B ON A.PayrollPeriod = B.PayrollPeriod 
        //WHERE (B.Branch = '" + txtABranchCode.Text + @"' OR B.Branch IS NULL) AND (B.IsLocked IS NULL OR B.IsLocked = 0)
        //						ORDER BY A.[PayrollPeriod] DESC";
        //        }

        //        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        //        txtStartofDeduction.Items.Clear();
        //        txtStartofDeduction.Items.Add("");
        //        foreach (DataRow row in _DataTable.Rows)
        //        {
        //            txtStartofDeduction.Items.Add(row[0].ToString());
        //        }
    }

    private void lblBrowseReleasingBranch_Click(object sender, EventArgs e)
    {
        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "BranchList";
        frmDataList.ShowDialog();

        txtBranchCode.Text = frmDataList._gBranchCode;
        txtBranchName.Text = frmDataList._gBranchName;
    }

    private void lblBrowseLoanAccountCode_Click(object sender, EventArgs e)
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
        if (txtTerms.Text == "")
        {
            if (clsFunctions.getDateValue(dtMonthlyDueDate.Text) != "")
            {
                dtDueDate.Text = DateTime.Parse(dtMonthlyDueDate.Text).ToString("MM/dd/yyyy");
            }
        }
        else
        {
            if (clsFunctions.getDateValue(dtMonthlyDueDate.Text) != "")
            {
            dtDueDate.Text = DateTime.Parse(dtMonthlyDueDate.Text).AddMonths(int.Parse(txtTerms.Text) - 1).ToString("MM/dd/yyyy");
            }

        }

        if (txtEmpCode.Text != "" && txtLoanAccountCode.Text != "" && txtLoanReferenceNo.Text != "")
        {
            SavedGroup();
        }
        else
        {
            btnNew.Enabled = true;
            btnSearch.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = true;
        }
    }

    private void btn_SearchClick(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        txtStartofDeduction.Items.Clear();
        txtStartofDeduction.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            txtStartofDeduction.Items.Add(row[0].ToString());
        }


        string lblTag = ((Label)sender).Tag.ToString();
        frmDataList frmDataList = new frmDataList();

        frmDataList._gStatus = _Status;
        switch (lblTag)
        {
            case "EmployeeNo":
                frmDataList._gEmployeeNo = txtEmpCode.Text;
                break;
            case "EmployeeName":
                frmDataList._gEmployeeName = txtEmpName.Text;
                break;
            case "LoanAccountCode":
                frmDataList._gAccountCode = txtLoanAccountCode.Text;
                break;
            case "LoanReferenceNumber":
                frmDataList._gLoanRefNo = txtLoanReferenceNo.Text;
                break;
        }

        frmDataList._gListGroup = lblTag;
        frmDataList.ShowDialog();

        if (frmDataList._gCompany != "")
        {
            NewGroup();
            txtLoanReferenceNo.ReadOnly = true;
            txtLoanDate.ReadOnly = true;

            lblBrowseEmployeeNo.Visible = false;
            lblBrowseReleasingBranch.Visible = true;
            lblBrowseLoanAccountCode.Visible = false;

            lblSearchEmployeeNo.Visible = false;
            lblSearchLoanAccountCode.Visible = false;
            lblSearchLoanReferenceNo.Visible = false;
            lblSearchEmployeeName.Visible = false;

            btnDelete.Enabled = true;
            txtLoanDate.Enabled = true;
            txtCardCode.ReadOnly = true;
            txtDocEntry.ReadOnly = true;
        }

        txtCompany.Text = frmDataList._gCompany;
        txtEmpCode.Text = frmDataList._gEmployeeNo;
        txtLoanAccountCode.Text = frmDataList._gAccountCode;
        txtLoanReferenceNo.Text = frmDataList._gLoanRefNo;

        pvDisplayData();

        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);
        string _sqlList = "";

        _sqlList = @"SELECT CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName]
                            FROM vwsEmployees A WHERE A.EmployeeNo COLLATE Latin1_General_CI_AS = '" + txtEmpCode.Text + @"'";
        txtEmpName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "EmployeeName");


        _sqlList = @"SELECT A.AccountDesc
                            FROM vwsAccountCode A WHERE A.AccountCode  = '" + txtLoanAccountCode.Text + @"'";
        txtDescription.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "AccountDesc");


        if (chkRelativeAccount.Checked == true)
        {
            txtRelativeName.ReadOnly = false;
        }
        else
        {
            txtRelativeName.ReadOnly = true;
        }

        if (chkIsTransferred.Checked == true)
        {
            txtTransferredDate.ReadOnly = false;
        }
        else
        {
            txtTransferredDate.ReadOnly = true;
        }
    }

    private void pvDisplayData()
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
                                            ,CASE WHEN ISNULL(A.RebateApplication,'') = '' THEN '3' ELSE  ISNULL(A.RebateApplication,'3') END AS RebateApplication
                                            ,ISNULL(A.LoanInterest,'0') AS LoanInterest
                                            ,CASE WHEN ISNULL(A.[OrigBCode],'') = '' THEN C.BCode ELSE ISNULL(A.[OrigBCode],'') END [OrigBCode]
                                            ,CASE WHEN ISNULL(A.[OrigBName],'') = '' THEN C.BName ELSE ISNULL(A.[OrigBName],'') END [OrigBName]
                                            ,A.[CreateDate]
                                            ,A.[UpdateDate]
                                            ,A.[Company] , C.BCode, C.BName , A.RelativeName
                                            ,A.[FirstDueDate]
                                            ,A.[MonthlyDueDate]
                                            ,A.[DueDate]

                                            ,A.[AdviceNo]
                                            ,ISNULL(A.[IsTransfered],0) AS [IsTransfered]
                                            ,ISNULL(A.[PostToSAP], 0) AS [PostToSAP]
                                            ,ISNULL(A.[BranchAccount], 0) AS [BranchAccount]
                                            ,A.[TranferredDate], A.[StartOfPosting], A.[SAPDocEntry], A.[SAPBPCode]
                            FROM [vwsLoanFile] A 
                                        INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
                                        INNER JOIN vwsDepartmentList C ON B.Department = C.DepartmentCode                     
                                        WHERE A.[EmployeeNo]  = '" + txtEmpCode.Text + @"' AND A.[AccountCode]  = '" + txtLoanAccountCode.Text + @"'  
                            AND B.[Company]  = '" + txtCompany.Text + @"'  AND A.[LoanRefNo]  = '" + txtLoanReferenceNo.Text + @"'";


            txtABranchCode.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "BCode");
            txtABranchName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "BName");

            txtBranchCode.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "OrigBCode");
            txtBranchName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "OrigBName");

            txtLoanDate.Text = DateTime.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "LoanDate")).ToString("MM/dd/yyyy");
            txtAmountGranted.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "AmountGranted").ToString("N2");
            txtPayableLoanAmount.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "LoanAmount").ToString("N2");
            txtAmortization.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Amortization").ToString("N2");
            txtLoanInterest.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "LoanInterest").ToString("N2");


            txtStartofDeduction.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "StartOfDeduction");
            txtTermsOfPayment.SelectedIndex = int.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "TermsOfPayment"));

            txtTerms.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Terms");

            txtRebateAmount.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Rebate").ToString("N2");
            txtParticular.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Particular");
            txtBrand.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Brand");
            txtLCPPrice.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "LCPPrice").ToString("N2");
            txtSpotCashAmount.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "SpotCashAmount").ToString("N2");
            txtDownPayment.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "DownPayment").ToString("N2");

            txtRebateApplication.SelectedIndex = int.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "RebateApplication"));

            txtRelativeName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "RelativeName");
            cboPostToSAP.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "StartOfPosting");
            if (txtRelativeName.Text == "")
            {
                chkRelativeAccount.Checked = false;
            }
            else
            {
                chkRelativeAccount.Checked = true;
            }

            txtAdviceNo.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "AdviceNo");
            if (clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "IsTransfered") == 0)
            {
                chkIsTransferred.Checked = false;
            }
            else
            {
                chkIsTransferred.Checked = true;
            }


            txtDocEntry.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "SAPDocEntry");
            txtCardCode.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "SAPBPCode");

            if (clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "BranchAccount") == 0)
            {
                chkIsBranchAccount.Checked = false;
            }
            else
            {
                chkIsBranchAccount.Checked = true;
            }

            if (clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "PostToSAP") == 0)
            {
                chkPostToSAP.Checked = false;
            }
            else
            {
                chkPostToSAP.Checked = true;
            }


            if (clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "TranferredDate") == "")
            {
                txtTransferredDate.Text = "";
            }
            else
            {
                txtTransferredDate.Text = DateTime.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "TranferredDate")).ToString("MM/dd/yyyy");
            }


            if (clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "FirstDueDate") == "")
            {
                dtFirstDueDate.Text = "";
            }
            else
            {
                dtFirstDueDate.Text = DateTime.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "FirstDueDate")).ToString("MM/dd/yyyy");
            }


            if (clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "MonthlyDueDate") == "")
            {
                dtMonthlyDueDate.Text = "";
            }
            else
            {
                dtMonthlyDueDate.Text = DateTime.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "MonthlyDueDate")).ToString("MM/dd/yyyy");
            }


            if (clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "DueDate") == "")
            {
                dtDueDate.Text = "";
            }
            else
            {
                dtDueDate.Text = DateTime.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "DueDate")).ToString("MM/dd/yyyy");
            }




            txtDate.Text = DateTime.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "CreateDate")).ToString("MM/dd/yyyy");
            if (clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Status") == "0")
            {
                lblStatus.Text = "Active";
                chkActive.Checked = true;
            }
            else
            {
                lblStatus.Text = "Inactive";
                chkActive.Checked = false;
            }

            string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

            _sqlList = @"SELECT * FROM [dbo].[fnGetBalancePerEmployeeLoan] (  '" + txtEmpCode.Text + @"' ,'" + txtLoanAccountCode.Text + @"', '" + txtLoanReferenceNo.Text + @"')
                ";

            txtCash.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Cash Amount").ToString("N2");
            txtPayroll.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Payroll Amount").ToString("N2");
            txtTotalCredit.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Total Payment").ToString("N2");
            txtBalance.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlList, "Balance").ToString("N2");

            _sqlList = @"SELECT Z.Type, Z.PaymentDate, Z.ORNo, Z.[Principal Amount], Z.[Interest Amount], Z.Amount, Z.Remarks, Z.Uploaded FROM (
                            SELECT ISNULL([Type],'CASH PAYMENT') AS [Type],CONVERT(nvarchar(30), [PaymentDate], 101) AS [PaymentDate],[ORNo],0 AS [Principal Amount] ,0 AS [Interest Amount],[Amount],[Remarks],[EmployeeNo],[AccountCode],[LoanRefNo], NULL AS [Uploaded] 
FROM LoanCashPayment
                            UNION ALL
                            SELECT 'PAYROLL' AS [Type],CONCAT(A.[PayrollPeriod], ' (', CONVERT(nvarchar(30), B.DateOne, 101),' - ', CONVERT(nvarchar(30), B.DateTwo, 101), ') '),NULL as [ORNo],ISNULL(A.PrincipalAmt,0) AS [Principal Amount] ,ISNULL(A.InterestAmt,0) AS [Interest Amount] 
,A.[Amount], NULL as [Remarks],[EmployeeNo],[AccountCode],[LoanRefenceNo],[Uploaded] 
FROM PayrollDetails A INNER JOIN PayrollPeriod B
                            ON A.PayrollPeriod = B.PayrollPeriod) Z 
                            WHERE Z.EmployeeNo = '" + txtEmpCode.Text + @"' AND Z.AccountCode = '" + txtLoanAccountCode.Text + @"' AND Z.LoanRefNo = '" + txtLoanReferenceNo.Text + @"'";
            DataTable _tblList;
            _tblList = clsSQLClientFunctions.DataList(_ConCompany, _sqlList);
            clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
        }
        catch
        {

        }

    }
    private bool CheckTextField()
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

        if (txtStartofDeduction.Text == "" || txtTermsOfPayment.Text == "")
        {
            MessageBox.Show("Start of Deduction, Terms of Payment Field Must Not Blank!");
            return false;
        }
        return true;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        DateTime _ActiveDate = DateTime.Now.AddDays((DateTime.Today.Day * -1) + 1);
        DateTime _BlockDate = _ActiveDate.AddDays(-1).AddDays(10);


        string _sqlCheck = "";
        DataTable _tblSave;



        if (clsFunctions.getDateValue(dtFirstDueDate.Text) == "")
        {
            MessageBox.Show("Please Dont leave First Due Date Blank");
            return;
        }
        if (clsFunctions.getDateValue(txtLoanDate.Text) == "")
        {
            MessageBox.Show("Please Dont leave Loan Date Blank");
            return;
        }


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




        int _IsTransferred = 0;
        if (chkIsTransferred.Checked == true)
        {
            _IsTransferred = 1;
        }
        else
        {
            _IsTransferred = 0;
        }


        int _PostToSAP = 0;
        if (chkPostToSAP.Checked == true)
        {
            _PostToSAP = 1;
        }
        else
        {
            _PostToSAP = 0;
        }



        int _IsBranchAccount = 0;
        if (chkIsBranchAccount.Checked == true)
        {
            _IsBranchAccount = 1;
        }
        else
        {
            _IsBranchAccount = 0;
        }


        if (_IsTransferred == 1 && clsFunctions.getDateValue(txtTransferredDate.Text) == "")
        {
            MessageBox.Show("Please define Transfer Date");
            return;
        }

        string _sqlSave = "";
        string _msg = "";



        if (_AddType == "EDIT")
        {
            _sqlSave = @"
                                    UPDATE A
                                       SET 
	                                       A.[StartOfDeduction] = '" + txtStartofDeduction.Text + @"'
                                          ,A.[LoanAmount] = CASE WHEN '" + double.Parse(txtPayableLoanAmount.Text) + @"' = '' THEN 0 ELSE " + double.Parse(txtPayableLoanAmount.Text) + @" END
                                          ,A.[AmountGranted] = CASE WHEN '" + double.Parse(txtAmountGranted.Text) + @"' = '' THEN 0 ELSE " + double.Parse(txtAmountGranted.Text) + @" END ";

            if (clsFunctions.getDateValue(txtLoanDate.Text) != "")
            {
                _sqlSave = _sqlSave + @" 
                                                 ,A.[LoanDate] = '" + txtLoanDate.Text + @"' ";
            }
            else
            {
                _sqlSave = _sqlSave + @" 
                                                 ,A.[LoanDate] = NULL ";
            }

            _sqlSave = _sqlSave + @"
                                          
                                          ,A.[TermsOfPayment] = '" + txtTermsOfPayment.SelectedIndex + @"'
                                          ,A.[Amortization] = CASE WHEN '" + double.Parse(txtAmortization.Text) + @"' = '' THEN 0 ELSE " + double.Parse(txtAmortization.Text) + @" END
                                          ,A.[LoanInterest] = CASE WHEN '" + double.Parse(txtLoanInterest.Text) + @"' = '' THEN 0 ELSE " + double.Parse(txtLoanInterest.Text) + @" END
                                          ,A.[Status] = '" + _Status + @"'
                                          ,A.[Brand] = '" + txtBrand.Text + @"'
                                          ,A.[Terms] = '" + txtTerms.Text + @"'
                                          ,A.[Particular] = '" + txtParticular.Text.Replace("'", "''") + @"'
                                          ,A.[LCPPrice] = CASE WHEN '" + txtLCPPrice.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtLCPPrice.Text) + @" END
                                          ,A.[SpotCashAmount] = CASE WHEN '" + txtSpotCashAmount.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtSpotCashAmount.Text) + @" END
                                          ,A.[DownPayment] = CASE WHEN '" + txtDownPayment.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtDownPayment.Text) + @" END
                                          ,A.[Rebate] = CASE WHEN '" + txtRebateAmount.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtRebateAmount.Text) + @" END
                                          ,A.[RebateApplication] = '" + txtRebateApplication.SelectedIndex + @"'
                                          ,A.[UpdateDate] = '" + DateTime.Now.ToString() + @"'
                                          ,A.[OrigBCode] = '" + txtBranchCode.Text + @"'
                                          ,A.[OrigBName] = '" + txtBranchName.Text + @"'
                                          ,A.[RelativeName] = '" + txtRelativeName.Text + @"'
                                          ,A.[IsTransfered] = '" + _IsTransferred + @"'
                                          ,A.[PostToSAP] = '" + _PostToSAP + @"'
                                          ,A.[BranchAccount] = '" + _IsBranchAccount + @"'
                                          ,A.[AdviceNo] = '" + txtAdviceNo.Text + @"'
                                          ,A.[StartOfPosting] = '" + cboPostToSAP.Text + @"'
                                          ";

            if (clsFunctions.getDateValue(txtTransferredDate.Text) != "")
            {
                _sqlSave = _sqlSave + @" 
                                                 ,A.[TranferredDate] = '" + txtTransferredDate.Text + @"' ";
            }
            else
            {
                _sqlSave = _sqlSave + @" 
                                                 ,A.[TranferredDate] = NULL ";
            }

            if (clsFunctions.getDateValue(dtFirstDueDate.Text) != "")
            {
                _sqlSave = _sqlSave + @" 
                                                 ,A.[FirstDueDate] = '" + dtFirstDueDate.Text + @"' ";
            }
            else
            {
                _sqlSave = _sqlSave + @" 
                                                 ,A.[FirstDueDate] = NULL ";
            }


            if (clsFunctions.getDateValue(dtMonthlyDueDate.Text) != "")
            {
                _sqlSave = _sqlSave + @" 
                                                 ,A.[MonthlyDueDate] = '" + dtMonthlyDueDate.Text + @"' ";
            }
            else
            {
                _sqlSave = _sqlSave + @" 
                                                 ,A.[MonthlyDueDate] = NULL ";

            }


            if (clsFunctions.getDateValue(dtDueDate.Text) != "")
            {
                _sqlSave = _sqlSave + @" 
                                                    ,A.[DueDate] = '" + dtDueDate.Text + @"' ";
            }
            else
            {
                _sqlSave = _sqlSave + @" 
                                                    ,A.[DueDate] = NULL ";
            }


            _sqlSave = _sqlSave + @" 
            FROM LoanFile A WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"' AND A.AccountCode = '" + txtLoanAccountCode.Text + @"'
                                  ";




            _sqlCheck = @"SELECT 'TRUE' FROM LoanFile A WHERE A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"' 
                                        AND A.EmployeeNo = '" + txtEmpCode.Text + @"' 
                                        AND A.AccountCode = '" + txtLoanAccountCode.Text + @"' AND ISNULL(A.SAPDocEntry,'') <> ''";
            _tblSave = clsSQLClientFunctions.DataList(_ConCompany, _sqlCheck);

            if (_tblSave.Rows.Count > 0)
            {
                //MessageBox.Show("Transaction Cannot Update Already Uploaded To SAP");

                _sqlSave = @"
                                    UPDATE A
                                       SET 
	                                       A.[Particular] = '" + txtParticular.Text.Replace("'", "''") + @"'";
                   
                _sqlSave = _sqlSave + @" 
            FROM LoanFile A WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"' AND A.AccountCode = '" + txtLoanAccountCode.Text + @"'
                                  ";



                DialogResult res = MessageBox.Show("This Transaction is ALready Uploaded To SAP. Only The Remark Fields Will Be Updated. Do You Want To Continue?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    return;
                }


            }

            _msg = "Data Successfully Updated";
        }
        else
        {
            _sqlCheck = @"SELECT 'TRUE' FROM LoanFile A WHERE A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"'";
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
                                    ,[RebateApplication]
                                    ,[CreateDate]
                                    ,[UpdateDate]
                                    ,OrigBCode, OrigBName,[RelativeName],[AdviceNo],[IsTransfered],[PostToSAP],[BranchAccount],[StartOfPosting]
,[TranferredDate]
,[FirstDueDate]
,[MonthlyDueDate]
,[DueDate]
                                    )
                                            VALUES
                                                ( '" + txtEmpCode.Text + @"' 
                                                , '" + txtLoanAccountCode.Text + @"' 
                                                , '" + txtLoanReferenceNo.Text + @"' 
                                                , '" + txtStartofDeduction.Text + @"' 
                                                , CASE WHEN '" + txtPayableLoanAmount.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtPayableLoanAmount.Text) + @" END
                                                , CASE WHEN '" + txtAmountGranted.Text + @"' = '' THEN 0 ELSE " + double.Parse(txtAmountGranted.Text) + @" END ";


            if (clsFunctions.getDateValue(txtLoanDate.Text) != "")
            {
                _sqlSave = _sqlSave + @" 
                                                , '" + txtLoanDate.Text + @"' ";
            }


            else
            {
                _sqlSave = _sqlSave + @" 
                                            ,  NULL  ";

        }


            _sqlSave = _sqlSave + @" 
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
                                                ,  '" + txtRelativeName.Text + @"' 
                                                ,  '" + txtAdviceNo.Text + @"'  
                                                ,  '" + _IsTransferred + @"' 
                                                ,  '" + _PostToSAP + @"' 
                                                ,  '" + _IsBranchAccount + @"' 
                                                ,  '" + cboPostToSAP.Text + @"'";



            if (clsFunctions.getDateValue(txtTransferredDate.Text) != "")
            {
                _sqlSave = _sqlSave + @" 
                                            ,  '" + txtTransferredDate.Text + @"'  ";
            }
            else
            {
                _sqlSave = _sqlSave + @" 
                                            ,  NULL  ";

            }


            if (clsFunctions.getDateValue(dtFirstDueDate.Text) != "")
            {
                    _sqlSave = _sqlSave + @" 
                                                ,  '" + dtFirstDueDate.Text + @"'  ";
            }
            else
            {
                    _sqlSave = _sqlSave + @" 
                                                ,  NULL  ";

            }

            if (clsFunctions.getDateValue(dtMonthlyDueDate.Text) != "")
            {
                _sqlSave = _sqlSave + @" 
                                            ,  '" + dtMonthlyDueDate.Text + @"' ";
            }
            else
            {
                _sqlSave = _sqlSave + @" 
                                            ,  NULL  ";

            }


            if (clsFunctions.getDateValue(dtDueDate.Text) != "")
            {
            _sqlSave = _sqlSave + @" 
                                        ,  '" + dtDueDate.Text + @"'  ";
        }
        else
            {
                _sqlSave = _sqlSave + @" 
                                            ,  NULL  ";

            }


            _sqlSave = _sqlSave + @" 
                                                )
                                  ";





            if (DateTime.Today > _BlockDate)
            {
                if (_IsTransferred == 0)
                {
                    if (_BlockDate.Month != DateTime.Parse(txtLoanDate.Text).Month)
                    {
                        MessageBox.Show("Transaction Not Add. Cannot Add Loan From Previews Month");
                        return;
                    }


                    if (_BlockDate.Month != DateTime.Parse(dtFirstDueDate.Text).Month)
                    {
                        MessageBox.Show("Transaction Not Add. Cannot Add Loan From Previews Month. Please Check Your First Due Date");
                        return;
                    }

                    if (_BlockDate.Month != int.Parse(Microsoft.VisualBasic.Strings.Mid(txtStartofDeduction.Text, 6, 2)))
                    {
                        MessageBox.Show("Transaction Not Add. Cannot Add Loan From Previews Month. Please Check Your Start of Deduction");
                        return;
                    }
                }
                else
                {
                    if (_BlockDate.Month != DateTime.Parse(txtTransferredDate.Text).Month)
                    {
                        MessageBox.Show("Transaction Not Add. Cannot Add Loan From Previews Month");
                        return;
                    }
                }
            }


            _msg = "Data Successfully Added";
        }

        
        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlSave);
        MessageBox.Show(_msg);

        pvDisplayData();

        _AddType = "EDIT";
        btnNew.Enabled = true;
        btnSearch.Enabled = true;
        btnSave.Enabled = false;
        btnDelete.Enabled = true;
        btnCancel.Enabled = true;
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


        pvDisplayData();
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


        pvDisplayData();
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

    private void btnRowDelete_Click(object sender, EventArgs e)
    {
        if (_LoanPayrollType == "PAYROLL")
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

        pvDisplayData();
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

        NewGroup();
    }

    private void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        SavedGroup();
        _Status = chkActive.Checked.ToString();
    }

    private void chkRelativeAccount_CheckedChanged(object sender, EventArgs e)
    {
        SavedGroup();
        if (chkRelativeAccount.Checked == true)
        {
            txtRelativeName.ReadOnly = false;
        }
        else
        {
            txtRelativeName.ReadOnly = true;
        }
    }

    private void CalculateLoanAmount_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    private void CalculateLoanAmount(object sender, EventArgs e)
    {
        //try
        //{
        //    txtPayableLoanAmount.Text = (double.Parse(txtAmountGranted.Text) - double.Parse(txtDownPayment.Text)).ToString("N2");
        //    txtAmortization.Text = (double.Parse(txtPayableLoanAmount.Text) / (double.Parse(txtTerms.Text) / 2)).ToString("N2");
        //}
        //catch
        //{ }
    }

    private void txtStartofDeduction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(txtStartofDeduction.Text == "")
        {
            return;
        }

        DateTime _StartOfTheMonth = DateTime.Parse(txtStartofDeduction.Text.Substring(5, 2) + "/1/" + txtStartofDeduction.Text.Substring(0, 4));

        if (txtStartofDeduction.Text.Substring(8,1) == "A")
        {
            dtFirstDueDate.Text = _StartOfTheMonth.AddDays(14).ToString("MM/dd/yyyy");

            //if (_StartOfTheMonth.Month == 2)
            //{
            //    dtMonthlyDueDate.Value = _StartOfTheMonth.AddMonths(1).AddDays(-1);
            //}
            //else
            //{
            //    dtMonthlyDueDate.Value = dtFirstDueDate.Value.AddDays(15);
            //}
        }
        else
        {
            if(_StartOfTheMonth.Month == 2)
            {
                dtFirstDueDate.Text = _StartOfTheMonth.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy");
            }
            else
            {
                dtFirstDueDate.Text = _StartOfTheMonth.AddDays(29).ToString("MM/dd/yyyy");
            }

            //dtMonthlyDueDate.Value = _StartOfTheMonth.AddMonths(1).AddDays(14);
        }



        if (clsFunctions.getDateValue(dtFirstDueDate.Text) == "")
        {
            dtFirstDueDate.Text = "";
            dtMonthlyDueDate.Text = "";
            dtDueDate.Text = "";

            return;
        }



        _StartOfTheMonth = DateTime.Parse(DateTime.Parse(dtFirstDueDate.Text).Month + "/1/" + DateTime.Parse(dtFirstDueDate.Text).Year);
        DateTime _15thOfTheMonth = DateTime.Parse(DateTime.Parse(dtFirstDueDate.Text).Month + "/15/" + DateTime.Parse(dtFirstDueDate.Text).Year);
        DateTime _EndOfTheMonth = _StartOfTheMonth.AddMonths(1).AddDays(-1);

        if (DateTime.Parse(dtFirstDueDate.Text).Day >= _StartOfTheMonth.Day && DateTime.Parse(dtFirstDueDate.Text).Day <= _15thOfTheMonth.Day)
        {
            if (_StartOfTheMonth.Month == 2)
            {
                dtMonthlyDueDate.Text = _StartOfTheMonth.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy");
            }
            else
            {
                dtMonthlyDueDate.Text = _15thOfTheMonth.AddDays(15).ToString("MM/dd/yyyy");
            }
        }
        else
        {
            dtMonthlyDueDate.Text = _EndOfTheMonth.AddDays(15).ToString("MM/dd/yyyy");
        }

        if (txtTerms.Text == "")
        {
            dtDueDate.Text = DateTime.Parse(dtMonthlyDueDate.Text).ToString("MM/dd/yyyy");
        }
        else
        {
            dtDueDate.Text = DateTime.Parse(dtMonthlyDueDate.Text).AddMonths(int.Parse(txtTerms.Text) - 1).ToString("MM/dd/yyyy");
        }
    }

    private void dtFirstDueDate_ValueChanged(object sender, EventArgs e)
    {

        if (clsFunctions.getDateValue(dtFirstDueDate.Text) == "")
        {
            dtFirstDueDate.Text = "";
            dtMonthlyDueDate.Text = "";
            dtDueDate.Text = "";

            return;
        }



        DateTime _StartOfTheMonth = DateTime.Parse(DateTime.Parse(dtFirstDueDate.Text).Month + "/1/" + DateTime.Parse(dtFirstDueDate.Text).Year);
        DateTime _15thOfTheMonth = DateTime.Parse(DateTime.Parse(dtFirstDueDate.Text).Month + "/15/" + DateTime.Parse(dtFirstDueDate.Text).Year);
        DateTime _EndOfTheMonth = _StartOfTheMonth.AddMonths(1).AddDays(-1);

        if (DateTime.Parse(dtFirstDueDate.Text).Day >= _StartOfTheMonth.Day && DateTime.Parse(dtFirstDueDate.Text).Day <= _15thOfTheMonth.Day)
        {
            if (_StartOfTheMonth.Month == 2)
            {
                dtMonthlyDueDate.Text = _StartOfTheMonth.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy");
            }
            else
            {
                dtMonthlyDueDate.Text = _15thOfTheMonth.AddDays(15).ToString("MM/dd/yyyy");
            }
        }
        else
        {
            dtMonthlyDueDate.Text = _EndOfTheMonth.AddDays(15).ToString("MM/dd/yyyy");
        }

        if(txtTerms.Text == "")
        {
            dtDueDate.Text = DateTime.Parse(dtMonthlyDueDate.Text).ToString("MM/dd/yyyy");
        }
        else
        {
            dtDueDate.Text = DateTime.Parse(dtMonthlyDueDate.Text).AddMonths(int.Parse(txtTerms.Text) - 1).ToString("MM/dd/yyyy");
        }
    }

    private void lblCalculate_Click(object sender, EventArgs e)
    {
        if (clsFunctions.getDateValue(dtFirstDueDate.Text) == "")
        {
            MessageBox.Show("Please Dont leave First Due Date Blank");
            return;
        }
        if (clsFunctions.getDateValue(txtLoanDate.Text) == "")
        {
            MessageBox.Show("Please Dont leave Loan Date Blank");
            return;
        }


        DateTime _StartOfTheMonth = DateTime.Parse(DateTime.Parse(dtFirstDueDate.Text).Month + "/1/" + DateTime.Parse(dtFirstDueDate.Text).Year);
        DateTime _15thOfTheMonth = DateTime.Parse(DateTime.Parse(dtFirstDueDate.Text).Month + "/15/" + DateTime.Parse(dtFirstDueDate.Text).Year);
        DateTime _EndOfTheMonth = _StartOfTheMonth.AddMonths(1).AddDays(-1);

        if (DateTime.Parse(dtFirstDueDate.Text).Day >= _StartOfTheMonth.Day && DateTime.Parse(dtFirstDueDate.Text).Day <= _15thOfTheMonth.Day)
        {
            if (_StartOfTheMonth.Month == 2)
            {
                dtMonthlyDueDate.Text = _StartOfTheMonth.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy");
            }
            else
            {
                dtMonthlyDueDate.Text = _15thOfTheMonth.AddDays(15).ToString("MM/dd/yyyy");
            }
        }
        else
        {
            dtMonthlyDueDate.Text = _EndOfTheMonth.AddDays(15).ToString("MM/dd/yyyy");
        }

        if (txtTerms.Text == "")
        {
            dtDueDate.Text = DateTime.Parse(dtMonthlyDueDate.Text).ToString("MM/dd/yyyy");
        }
        else
        {
            dtDueDate.Text = DateTime.Parse(dtMonthlyDueDate.Text).AddMonths(int.Parse(txtTerms.Text) - 1).ToString("MM/dd/yyyy");
        }

        if(txtAmountGranted.Text == "") { txtAmountGranted.Text = "0"; }
        if (txtLoanInterest.Text == "") { txtLoanInterest.Text = "0"; }
        if (txtDownPayment.Text == "") { txtDownPayment.Text = "0"; }

        txtPayableLoanAmount.Text = ((double.Parse(txtAmountGranted.Text) + double.Parse(txtLoanInterest.Text)) - double.Parse(txtDownPayment.Text)).ToString("N2");
        txtAmortization.Text = ((double.Parse(txtPayableLoanAmount.Text) / int.Parse(txtTerms.Text)) / 2).ToString("N2");
    }

    private void btnInstSearch_Click(object sender, EventArgs e)
    {
        string _strPayrollPeriod;

        string _sqlSelect = @"SELECT A.[FirstDueDate], A.[StartOfDeduction]
                            FROM [vwsLoanFile] A                  
                                        WHERE A.[EmployeeNo]  = '" + txtEmpCode.Text + @"' AND A.[AccountCode]  = '" + txtLoanAccountCode.Text + @"'  
                            AND A.[Company]  = '" + txtCompany.Text + @"'  AND A.[LoanRefNo]  = '" + txtLoanReferenceNo.Text + @"'";
        DataTable _tblSelect = new DataTable();
        _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);


        string _FirstDueDate = clsSQLClientFunctions.GetData(_tblSelect, "FirstDueDate", "0");
        string _StartOfDeduction = clsSQLClientFunctions.GetData(_tblSelect, "StartOfDeduction", "0");

        DateTime _RunningDate;

        if(_FirstDueDate == "")
        {
            if(Microsoft.VisualBasic.Strings.Right(_StartOfDeduction, 1) == "A")
            {
                _RunningDate = DateTime.Parse(Microsoft.VisualBasic.Strings.Mid(_StartOfDeduction, 6, 2) + "/15/" + Microsoft.VisualBasic.Strings.Left(_StartOfDeduction, 4));
            }
            else
            {
                _RunningDate = DateTime.Parse(Microsoft.VisualBasic.Strings.Mid(_StartOfDeduction, 6, 2) + "/1/" + Microsoft.VisualBasic.Strings.Left(_StartOfDeduction, 4)).AddMonths(1).AddDays(-1);
            }
  
        }
        else
        {
            _RunningDate = DateTime.Parse(dtFirstDueDate.Text);
        }

        DateTime _FirstDay;
        DateTime _LastDay;
        int _Count = 0;
        _dtInstDisplay.Rows.Clear();

        _dtInstDisplay.Rows.Add();
        _dtInstDisplay.Rows[_Count][1] = DateTime.Parse(txtLoanDate.Text).ToString("MM/dd/yyyy");
        _dtInstDisplay.Rows[_Count][0] = _Count.ToString("00");
        _dtInstDisplay.Rows[_Count][8] = txtPayableLoanAmount.Text;

        //_Count++;
        //_dtInstDisplay.Rows.Add();
        //_dtInstDisplay.Rows[_Count][1] = _RunningDate.ToShortDateString();





        do
        {




            _FirstDay = DateTime.Parse(_RunningDate.Month.ToString() + "/01/" + _RunningDate.Year.ToString());
            _LastDay = _FirstDay.AddMonths(1).AddDays(-1);


            if(_RunningDate == _FirstDay.AddDays(14))
            {
                _dtInstDisplay.Rows.Add();
                _Count++;

                _strPayrollPeriod = _FirstDay.Year.ToString() + "-" + _FirstDay.Month.ToString("00") + "-A";
                _dtInstDisplay.Rows[_Count][0] = _Count.ToString("00");
                _dtInstDisplay.Rows[_Count][1] = _RunningDate.ToShortDateString();
                _dtInstDisplay.Rows[_Count][4] = double.Parse(txtAmortization.Text).ToString("N2");

                _sqlSelect = @"SELECT A.Amount FROM vwsLoanPayment A WHERE A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"' AND A.Type = 'PAYROLL' 
                    AND A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.AccountCode = '" + txtLoanAccountCode.Text + @"' AND A.PaymentDate LIKE '%" + _strPayrollPeriod + @"%'";
                _tblSelect = new DataTable();
                _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
                double _Amount = double.Parse(clsSQLClientFunctions.GetData(_tblSelect, "Amount", "1"));
                _dtInstDisplay.Rows[_Count][5] = _Amount.ToString("N2");
                if (_Amount != 0)
                {
                    _dtInstDisplay.Rows[_Count][2] = _strPayrollPeriod;
                    _dtInstDisplay.Rows[_Count][3] = _RunningDate.ToShortDateString();
                    _dtInstDisplay.Rows[_Count][10] = "PAYROLL";
                }


                _sqlSelect = @"SELECT A.Amount FROM vwsLoanPayment A WHERE A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"' AND A.Type = 'REBATE' 
                    AND A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.AccountCode = '" + txtLoanAccountCode.Text + @"' AND A.ORNo LIKE '%" + _strPayrollPeriod + @"%'";
                _tblSelect = new DataTable();
                _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
                _Amount = double.Parse(clsSQLClientFunctions.GetData(_tblSelect, "Amount", "1"));
                _dtInstDisplay.Rows[_Count][6] = _Amount.ToString("N2");

                _RunningDate = _LastDay;
            }



            if (_RunningDate == _LastDay)
            {
                _dtInstDisplay.Rows.Add();
                _Count++;

                _strPayrollPeriod = _FirstDay.Year.ToString() + "-" + _FirstDay.Month.ToString("00") + "-B";
                _dtInstDisplay.Rows[_Count][0] = _Count.ToString("00");
                _dtInstDisplay.Rows[_Count][1] = _RunningDate.ToShortDateString();
                _dtInstDisplay.Rows[_Count][4] = double.Parse(txtAmortization.Text).ToString("N2");

                _sqlSelect = @"SELECT A.Amount FROM vwsLoanPayment A WHERE A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"' AND A.Type = 'PAYROLL' 
                    AND A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.AccountCode = '" + txtLoanAccountCode.Text + @"' AND A.PaymentDate LIKE '%" + _strPayrollPeriod + @"%'";
                _tblSelect = new DataTable();
                _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
                double _Amount = double.Parse(clsSQLClientFunctions.GetData(_tblSelect, "Amount", "1"));

                _dtInstDisplay.Rows[_Count][5] = _Amount.ToString("N2");
                if (_Amount != 0)
                {
                    _dtInstDisplay.Rows[_Count][2] = _strPayrollPeriod;
                    _dtInstDisplay.Rows[_Count][3] = _RunningDate.ToShortDateString();
                    _dtInstDisplay.Rows[_Count][10] = "PAYROLL";
                }



                _sqlSelect = @"SELECT A.Amount FROM vwsLoanPayment A WHERE A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"' AND A.Type = 'REBATE' 
                    AND A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.AccountCode = '" + txtLoanAccountCode.Text + @"' AND A.ORNo LIKE '%" + _strPayrollPeriod + @"%'";
                _tblSelect = new DataTable();
                _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
                _Amount = double.Parse(clsSQLClientFunctions.GetData(_tblSelect, "Amount", "1"));
                _dtInstDisplay.Rows[_Count][6] = _Amount.ToString("N2");

                _RunningDate = _LastDay.AddDays(15);
            }


      

        } while (DateTime.Parse(dtDueDate.Text) >= _LastDay);
        //_dtInstDisplay.Rows[_Count][4] = double.Parse(txtAmortization.Text).ToString("N2");

        DataTable _tblAllList = new DataTable();
        string _sqlGetList = @"
DELETE FROM Installment WHERE  [Emp No] = '" + txtEmpCode.Text + @"' AND [Loan Ref No] = '" + txtLoanReferenceNo.Text + @"'  AND [Account Code] = '" + txtLoanAccountCode.Text + @"'

INSERT INTO [Installment]
           ([Emp No]
           ,[Loan Ref No]
           ,[Account Code]
           ,[Amort No]
           ,[Date Due]
           ,[Payroll Period]
           ,[Date Paid]
           ,[Prin Amount]
           ,[Payment Amt]
           ,[Rebate]
           ,[Penalty]
           ,[Balance]
           ,[Remarks]
           ,[Type])


SELECT '" + txtEmpCode.Text + @"','" + txtLoanReferenceNo.Text + @"', '" + txtLoanAccountCode.Text + @"', * FROM (
                                 ";


        int i = 0;
        foreach (DataRow row in _dtInstDisplay.Rows)
        {
            string _PayDate = row[3].ToString();

            double _PrinAmt = 0;
            double.TryParse(row[4].ToString(), out _PrinAmt);

            double _PayAmt = 0;
            double.TryParse(row[5].ToString(), out _PayAmt);

            double _Rebate = 0;
            double.TryParse(row[6].ToString(), out _Rebate);

            double _Penalty = 0;
            double.TryParse(row[7].ToString(), out _Penalty);
            double _Balance = 0;
            double.TryParse(row[8].ToString(), out _Balance);



            if (i != 0)
                _sqlGetList = _sqlGetList + @" UNION ALL
                                           ";

            _sqlGetList = _sqlGetList + @" SELECT 
                                '" + row[0].ToString() + @"' AS [Amort No],
                                CAST('" + row[1].ToString() + @"' AS DATE) AS [Date Due],
                                '" + row[2].ToString() + @"' AS [Payroll Period],
                                CASE WHEN '" + _PayDate  + @"' = '' THEN  NULL ELSE CAST('" + _PayDate + @"' AS DATE) END AS [Date Paid],
                                '" + _PrinAmt + @"' AS [Prin Amount],
                                '" + _PayAmt + @"' AS [Payment Amt],
                                '" + _Rebate + @"' AS [Rebate],
                                '" + _Penalty + @"' AS [Penalty],
                                '" + _Balance + @"' AS [Balance],
                                '" + row[9].ToString() + @"' AS [Remarks],
                                '" + row[10].ToString() + @"' AS [Type]
                                      ";


            i++;
        }

        _sqlGetList = _sqlGetList + @"

									  UNION ALL

									  SELECT                '' AS [Amort No],
                                A.PaymentDate AS [Date Due],
                                A.ORNo AS [Payroll Period],
                                A.PaymentDate AS [Date Paid],
                                '0' AS [Prin Amount],
                                A.Amount AS [Payment Amt],
                                '0' AS [Rebate],
                                '0' AS [Penalty],
                                '0' AS [Balance],
                                A.Remarks AS [Remarks],
                                A.Type AS [Type]  FROM vwsLoanPayment A WHERE A.Type NOT IN ('REBATE', 'PAYROLL')
								AND A.LoanRefNo = '" + txtLoanReferenceNo.Text + @"' AND A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.AccountCode = '" + txtLoanAccountCode.Text + @"'

                                      ";

        _sqlGetList = _sqlGetList + @") ZZ ORDER BY CAST(ZZ.[Date Due] AS DATE)";
        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlGetList);




        _sqlGetList = @"SELECT A.[Amort No]
      ,A.[Date Due]
      ,A.[Payroll Period]
      ,A.[Date Paid]
      ,A.[Prin Amount]
      ,A.[Payment Amt]
      ,A.[Rebate]
      ,A.[Penalty]
      ,A.[Balance]
      ,A.[Remarks]
      ,A.[Type]
  FROM [Installment] A
  WHERE A.[Emp No] = '" + txtEmpCode.Text + @"'
      AND A.[Loan Ref No] = '" + txtLoanReferenceNo.Text + @"'
      AND A.[Account Code] = '" + txtLoanAccountCode.Text + @"'";
        _tblAllList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlGetList);

        double _RunningBalance = double.Parse(_tblAllList.Rows[0][8].ToString());
        i = 0;
        foreach (DataRow row in _tblAllList.Rows)
        {
            if (i != 0)
            {
                double _Payment = double.Parse(row[5].ToString());
                double _Rebate = double.Parse(row[6].ToString());
                _RunningBalance = _RunningBalance - (_Payment + _Rebate);

                _tblAllList.Rows[i][8] = _RunningBalance;
            }


            i++;
        }
        clsFunctions.DataGridViewSetup(dgvInstDisplay, _tblAllList, "Installment");


        MessageBox.Show("Installment Generated");
        //_tblAllList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlGetList);



    }

    private void chkAll_CheckedChanged(object sender, EventArgs e)
    {
//        DataTable _DataTable;
//        string _SQLSyntax;
//        if (txtABranchCode.Text == "")
//        {
//            return;
//        }

//        if (chkAll.CheckState == CheckState.Checked)
//        {

//            _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
//        }
//        else
//        {
//            _SQLSyntax = @"SELECT DISTINCT A.[PayrollPeriod],A.[DateOne],A.[DateTwo],A.[IsLocked] 
//FROM vwsPayrollPeriod A LEFT JOIN PayrollLocker B ON A.PayrollPeriod = B.PayrollPeriod 
//WHERE (B.Branch = '" + txtABranchCode.Text + @"' OR B.Branch IS NULL) AND (B.IsLocked IS NULL OR B.IsLocked = 0)
//						ORDER BY A.[PayrollPeriod] DESC";
//        }

//        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

//        txtStartofDeduction.Items.Clear();
//        txtStartofDeduction.Items.Add("");
//        foreach (DataRow row in _DataTable.Rows)
//        {
//            txtStartofDeduction.Items.Add(row[0].ToString());
//        }


        int _IsViewAll = 0;
        if (chkAll.Checked == true)
        {
            _IsViewAll = 1;
        }
        else
        {
            _IsViewAll = 0;
        }

        txtStartofDeduction.Items.Clear();
        txtStartofDeduction.Items.Add("");
        foreach (DataRow row in clsFunctions.getListofPayrollPeriod(txtABranchCode.Text, _IsViewAll).Rows)
        {
            txtStartofDeduction.Items.Add(row[0].ToString());
        }
    }

    private void dtFirstDueDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
    {

    }

    private void chk_CheckedChanged(object sender, EventArgs e)
    {
        SavedGroup();
        if (chkIsTransferred.Checked == true)
        {
            txtTransferredDate.ReadOnly = false;
            txtTransferredDate.Text = "";
        }
        else
        {
            txtTransferredDate.ReadOnly = true;
            txtTransferredDate.Text = "";
        }
    }

    private void lblPreviewReport_Click(object sender, EventArgs e)
    {

        clsDeclaration.sReportID = 9;

        clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Loan Ledger Report.rpt";
        clsDeclaration.sQueryString = sqlQueryOutput(txtEmpCode.Text, txtLoanAccountCode.Text, txtLoanReferenceNo.Text);

        frmReportList frmReportList = new frmReportList();
        //frmReportList._RequestType = _RequestType;
        frmReportList.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmReportList.Show();
    }



    private string sqlQueryOutput(string _refEmployeeNo = "",
                                string _refAccountCode = "",
                                string _refLoanReferenceNo = "")
    {


        string _sqlSyntax = "";
        _sqlSyntax = @"
                        SELECT A.*, B.EmployeeName, A.AccountCode, C.AccountDesc
                        , D.Type, D.PaymentDate, D.ORNo, D.Amount, D.Remarks, E.BCode, E.BName, B.ConfiLevel
                        FROM vwsLoanFile A 
                        INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo
                        INNER JOIN vwsAccountCode C ON A.AccountCode = C.AccountCode
                        INNER JOIN vwsLoanPayment D ON A.EmployeeNo = D.EmployeeNo AND A.AccountCode = D.AccountCode AND A.LoanRefNo = D.LoanRefNo
                        LEFT JOIN vwsDepartmentList E ON B.Department = E.DepartmentCode
                        WHERE A.EmployeeNo = '" + _refEmployeeNo  + @"' AND A.AccountCode = '" + _refAccountCode + @"' AND A.LoanRefNo = '" + _refLoanReferenceNo + @"'";


        return _sqlSyntax;
    }

    private void txtABranchCode_TextChanged(object sender, EventArgs e)
    {

    }

    private void chk_PostToSAP(object sender, EventArgs e)
    {
        SavedGroup();
        if (chkPostToSAP.Checked == true)
        {
            cboPostToSAP.Enabled = true;
        }
        else
        {
            cboPostToSAP.Enabled = false;
        }

        //int _IsViewAll = 0;
        //if (ViewAllP.Checked == true)
        //{
        //    _IsViewAll = 1;
        //}
        //else
        //{
        //    _IsViewAll = 0;
        //}

        //if (chkPostToSAP.Checked == true)
        //{
        //    cboPostToSAP.Enabled = true;

        //    cboPostToSAP.Items.Clear();
        //    cboPostToSAP.Items.Add("");
        //    foreach (DataRow row in clsFunctions.getListofPayrollPeriod(txtABranchCode.Text, _IsViewAll).Rows)
        //    {
        //        cboPostToSAP.Items.Add(row[0].ToString());
        //    }
        //}
        //else
        //{
        //    cboPostToSAP.Enabled = false;
        //    cboPostToSAP.Items.Clear();
        //}
    }

    private void ViewAllP_CheckedChanged(object sender, EventArgs e)
    {
        SavedGroup();

        int _IsViewAll = 0;
        if (ViewAllP.Checked == true)
        {
            _IsViewAll = 1;
        }
        else
        {
            _IsViewAll = 0;
        }

        cboPostToSAP.Items.Clear();
        cboPostToSAP.Items.Add("");
        foreach (DataRow row in clsFunctions.getListofPayrollPeriod(txtABranchCode.Text, _IsViewAll).Rows)
        {
            cboPostToSAP.Items.Add(row[0].ToString());
        }
    }

    private void chkIsBranchAccount_CheckedChanged(object sender, EventArgs e)
    {
        SavedGroup();
        if (chkIsBranchAccount.Checked == true)
        {
            txtAdviceNo.ReadOnly = false;
        }
        else
        {
            txtAdviceNo.ReadOnly = true;
        }

    }
}