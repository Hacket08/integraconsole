using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;

public partial class frmSAPUploader : Form
{
    public frmSAPUploader()
    {
        InitializeComponent();
    }

    private void frmSAPUploader_Load(object sender, EventArgs e)
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

        _SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
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

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnGenerate_Click(object sender, EventArgs e)
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
            _BCode = cboBranch.Text.Substring(0, 8);
        }
        
        _SQLSyntax = @"
                SELECT A.[Loan Account], A.[DocDate] ,A.Account, A.Debit, A.Credit, A.AccountName, A.EmployeeName FROM [dbo].[fnSAPTransaction]('" + _BCode  + @"','" + cboPayrolPeriod.Text + @"') A
";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);

        _SQLSyntax = @"
                SELECT A.* FROM [dbo].[fnSAPIncomingPayment]('" + _BCode + @"','" + cboPayrolPeriod.Text + @"') A
WHERE ISNULL(A.DocEntry,'') <> ''
  AND A.AccountCode = ''
";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dgvValid, _DataTable);

        _SQLSyntax = @"
                SELECT A.* FROM [dbo].[fnSAPIncomingPayment]('" + _BCode + @"','" + cboPayrolPeriod.Text + @"') A
WHERE ISNULL(A.DocEntry,'') = ''
  AND A.AccountCode = ''
";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dgvInvalid, _DataTable);


        _SQLSyntax = @"
                SELECT SUM(A.Credit) AS Credit, SUM(A.Debit) AS Debit FROM [dbo].[fnSAPTransaction]('" + _BCode + @"','" + cboPayrolPeriod.Text + @"') A
        ";

        txtCredit.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _SQLSyntax, "Credit").ToString("N2");
        txtDebit.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _SQLSyntax, "Debit").ToString("N2");
        txtDiff.Text = (double.Parse(txtCredit.Text) - double.Parse(txtDebit.Text)).ToString("N2");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        string sysDBServer = ConfigurationManager.AppSettings["sysDBServer"];
        string sysDftDBCompany = ConfigurationManager.AppSettings["sysDftDBCompany"];
        string sysDBUsername = ConfigurationManager.AppSettings["sysDBUsername"];
        string sysDBPassword = ConfigurationManager.AppSettings["sysDBPassword"];

        clsDeclaration.sSAPConnection = clsSQLClientFunctions.GlobalConnectionString(sysDBServer, sysDftDBCompany, sysDBUsername, sysDBPassword);



        sysDftDBCompany = ConfigurationManager.AppSettings["sysDftDBCompany"];
        sysDBUsername = ConfigurationManager.AppSettings["sysSAPUsername"];
        sysDBPassword = ConfigurationManager.AppSettings["sysSAPPassword"];

        bool isConnected = false;
        string _Msg = "";
        clsSAPFunctions.oCompany = clsSAPFunctions.SAPConnection(sysDftDBCompany, sysDBUsername, sysDBPassword, out isConnected, out _Msg);

        if (isConnected == false)
        {
            MessageBox.Show(_Msg);
            return;
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


        if(clsSAPFunctions.JEAllocation(_BCode, cboPayrolPeriod.Text) == true)
        {

            DataTable _DataTable;
            string _SQLSyntax;

            _SQLSyntax = @"
                                        SELECT 
				                          A.EmployeeNo
				                        , A.EmployeeName
				                        , A.AccountDesc
				                        , A.AccountCode
				                        , A.LoanRefenceNo
				                        , A.Amount
				                        , A.DateTwo
				                        , A.CashAccount 
                                        , A.CardCode
                                        , A.DocEntry
                                        , A.Company
                                        FROM [dbo].[fnSAPIncomingPayment]('" + _BCode + @"','" + cboPayrolPeriod.Text + @"') A
                                        WHERE ISNULL(A.DocEntry,'') <> ''
                                      ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dgvValid, _DataTable);

            double _RowCount;
            int _Count = 0;
            _RowCount = _DataTable.Rows.Count;

            foreach (DataRow row in _DataTable.Rows)
            {
                string _DocEntry = row["DocEntry"].ToString();
                string _CardCode = row["CardCode"].ToString();
                string _EmployeeName = row["EmployeeName"].ToString();
                string _CashAccount = row["CashAccount"].ToString();
                string _Amount = row["Amount"].ToString();
                string _DocDate = row["DateTwo"].ToString();

                string _EmployeeNo = row["EmployeeNo"].ToString();
                string _AccountCode = row["AccountCode"].ToString();
                string _LoanReferenceNo = row["LoanRefenceNo"].ToString();
                string _Company = row["Company"].ToString();

                string _InstlmntID = row["InstlmntID"].ToString();


                clsSAPFunctions.CreateIncomingPayment(_BCode,int.Parse(_DocEntry), _CardCode, _EmployeeName, _CashAccount, DateTime.Parse(_DocDate)
                    , _EmployeeNo, _AccountCode, _LoanReferenceNo, _Company, isConnected);

                // Excel Progress Monitoring
                Application.DoEvents();
                _Count++;
                tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
                pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
            }



        }

        clsSAPFunctions.oCompany.Disconnect();
        MessageBox.Show("Payment Successfully Posted");
    }

    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {

        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataTable _DataTable;
            string _SQLSyntax;


            string _Account = dgvDisplay.Rows[e.RowIndex].Cells["Loan Account"].Value.ToString().Trim();
            string _AccountName = dgvDisplay.Rows[e.RowIndex].Cells["AccountName"].Value.ToString().Trim();

            string _BCode = "";
            if (cboBranch.Text == "")
            {
                _BCode = "";
            }
            else
            {
                _BCode = cboBranch.Text.Substring(0, 8);
            }

            _SQLSyntax = @"
                SELECT A.* FROM [dbo].[fnSAPIncomingPayment]('" + _BCode + @"','" + cboPayrolPeriod.Text + @"') A
WHERE ISNULL(A.DocEntry,'') <> ''
  AND A.AccountCode = '" + _Account + @"'
                                        ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dgvValid, _DataTable);


            _SQLSyntax = @"
                SELECT A.* FROM [dbo].[fnSAPIncomingPayment]('" + _BCode + @"','" + cboPayrolPeriod.Text + @"') A
WHERE ISNULL(A.DocEntry,'') = ''
  AND A.AccountCode = '" + _Account + @"'
                                        ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dgvInvalid, _DataTable);

            _SQLSyntax = @"
                SELECT SUM(A.Amount) AS Amount  FROM [dbo].[fnSAPIncomingPayment]('" + _BCode + @"','" + cboPayrolPeriod.Text + @"') A
WHERE ISNULL(A.DocEntry,'') <> ''
  AND A.AccountCode = '" + _Account + @"'
        ";

            txtAmount.Text = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _SQLSyntax, "Amount").ToString("N2");

            lblGross.Text = "Total " + _AccountName;

        }
        catch
        {

        }



    }
}