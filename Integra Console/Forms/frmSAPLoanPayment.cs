using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
public partial class frmSAPLoanPayment : Form
{
    private static DataTable _DataList = new DataTable();

    public static string _gCompany;
    public static string _gEmpCode;
    public static string _gEmpName;
    public static string _gPayrollPeriod;


    public static DateTime _PayrollDate;
    public static DateTime _FirstDay;
    public frmSAPLoanPayment()
    {
        InitializeComponent();
    }

    private void frmSAPLoanPayment_Load(object sender, EventArgs e)
    {
        btnUpload.Enabled = false;

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = @"
                             SELECT * FROM OUSR A WHERE A.[USERID] = '" + clsDeclaration.sLoginUserID + @"'
                      ";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        string _RankAndFile = clsSQLClientFunctions.GetData(_DataTable, "RankAndFile", "0");
        string _Supervisor = clsSQLClientFunctions.GetData(_DataTable, "Supervisor", "0");
        string _Manager = clsSQLClientFunctions.GetData(_DataTable, "Manager", "0");



        //_SQLSyntax = "SELECT  [PayrollPeriod]  FROM dbo.[vwsPayrollPeriod] A";
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

        
        MessageBox.Show("Data Ready To Upload");
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {

        //string _sqlList = "";

        //if (cboPayrolPeriod.Text == "")
        //{
        //    MessageBox.Show("Please select payroll period");
        //    return;
        //}

        //DataTable _DataTable;
        //string _SQLSyntax;

        //string _BCode = "";
        //if (cboBranch.Text == "")
        //{
        //    _BCode = "";
        //}
        //else
        //{
        //    _BCode = cboBranch.Text.Substring(0, 8);
        //}


        btnUpload.Enabled = false;
        btnOVPay.Enabled = false;
        double _RowCount;
        int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        string sysDftDBCompany = ConfigurationManager.AppSettings["sysDftDBCompany"];
        string sysDBUsername = ConfigurationManager.AppSettings["sysSAPUsername"];
        string sysDBPassword = ConfigurationManager.AppSettings["sysSAPPassword"];

        bool isConnected = false;
        string _Msg = "";
        clsSAPFunctions.oCompany = clsSAPFunctions.SAPConnection(sysDftDBCompany, sysDBUsername, sysDBPassword, out isConnected, out _Msg);

        if (isConnected == false)
        {
            MessageBox.Show(_Msg);
            return;
        }


        foreach (DataRow row in _DataList.Rows)
        {
            Application.DoEvents();


            string _BCode = row["BCode"].ToString();
            string _CardCode = row["CardCode"].ToString();
            string _DocEntry = row["DocEntry"].ToString();
            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _EmployeeName = row["EmployeeName"].ToString();
            //string _Amount = row["Amount"].ToString();
            string _CashAccount = row["CashAccount"].ToString();

            string _PendingAmt = row["Pending Payroll Amount"].ToString();
            //string _DateTwo = row["DateTwo"].ToString();

            string _AccountCode = row["AccountCode"].ToString();
            string _LoanReferenceNo = row["LoanRefNo"].ToString();
            string _Company = row["Company"].ToString();

            //string _InstlmntID = row["SAPInsID"].ToString();
            //string _PayrollPeriod = row["PayrollPeriod"].ToString();

            if (_DocEntry != "")
            {
                if (double.Parse(_PendingAmt) != 0)
                {
                    clsSAPFunctions.CreateIncomingPayment(_BCode, int.Parse(_DocEntry), _CardCode, _EmployeeName, _CashAccount
                                                        , _PayrollDate, _EmployeeNo, _AccountCode, _LoanReferenceNo, _Company, isConnected);
                }

            }

            //else
            //{
            //    string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
            //    string _SQLSyntaxUpdate = @"UPDATE A SET A.[SAPError] = 'Missing AR Document'  FROM PayrollDetails A  
            //                                WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.PayrollPeriod = '" + _PayrollPeriod + @"' 
            //                                AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanReferenceNo + @"'
            //                                                ";

            //    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntaxUpdate);
            //}

            Application.DoEvents();
            _Count++;

            tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }

        clsSAPFunctions.oCompany.Disconnect();
        //button1_Click_2(sender, e);


        DataDisplay();
        MessageBox.Show("Data Uploaded.");

        panel1.Enabled = true;
        btnUpload.Enabled = true;

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        if(panel1.Enabled == false)
        {
            btnUpload.Enabled = false;
            panel1.Enabled = true;
        }
        else
        {
            Close();
        }

    }

    private void btnReProcess_Click(object sender, EventArgs e)
    {
        
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
        //frmManualPayroll._gPayrollPeriod = cboPayrolPeriod.Text;
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
        //frmPayrollTransaction._gPayrollPeriod = cboPayrolPeriod.Text;
        frmPayrollTransaction.Show();
    }

    private void cboPayrolPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    string _Year = Microsoft.VisualBasic.Strings.Left(cboPayrolPeriod.Text, 4);
        //    string _Month = Microsoft.VisualBasic.Strings.Mid(cboPayrolPeriod.Text, 6, 2);
        //    string _DayTag = Microsoft.VisualBasic.Strings.Right(cboPayrolPeriod.Text, 1);

        //    _FirstDay = DateTime.Parse(_Month + "/1/" + _Year);
        //    if(_DayTag == "A")
        //    {
        //        _PayrollDate = DateTime.Parse(_FirstDay.AddDays(14).ToString("MM/dd/yyyy"));
        //    }
        //    else
        //    {
        //        if(_Month == "02")
        //        {
        //            _PayrollDate = DateTime.Parse(_FirstDay.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy"));
        //        }
        //        else
        //        {
        //            _PayrollDate = DateTime.Parse(_FirstDay.AddDays(29).ToString("MM/dd/yyyy"));
        //        }

        //    }

        //    label4.Text = _PayrollDate.ToShortDateString();
        //}
        //catch
        //{
        //    label4.Text = "MM/DD/YYYY";
        //}
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {

  

        panel1.Enabled = false;
        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }

        //                                                --SELECT * FROM fnSAPIncomingPaymentPerMonth('" + _BCode + @"','" + cboPayrolPeriod.Text + @"') A
        DialogResult result;
        result = MessageBox.Show("Data Generation for Loan Application Branch : " + _BCode , "Payroll Loan Generation", MessageBoxButtons.OKCancel);
        if (result == System.Windows.Forms.DialogResult.Cancel)
        {
            return;
        }



        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"

                                SELECT C.EmployeeNo, C.EmployeeName, B.AccountDesc, F.AccountCode, F.LoanRefNo, '131152' AS CashAccount
                                ,F.SAPBPCode AS CardCode 
                                ,F.SAPDocEntry AS DocEntry 
                                ,C.Company
                                ,D.BCode
                                ,F.RelativeName 

                                ,(SELECT A.DocDate FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[OINV] A WHERE A.DocEntry = F.SAPDocEntry )  AS [SAP Date]
                                ,(SELECT A.U_DP FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[OINV] A WHERE A.DocEntry = F.SAPDocEntry )  AS [AR DownPayment]
                                ,(SELECT A.DocTotal FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[OINV] A WHERE A.DocEntry = F.SAPDocEntry )  AS [AR Amount]
                                ,(SELECT SUM(A.SumApplied) FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[RCT2] A WHERE A.DocEntry = F.SAPDocEntry ) AS [Applied Amount]
                                ,(SELECT A.DocTotal - A.PaidToDate FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[OINV] A 
                                WHERE A.DocEntry = F.SAPDocEntry )  AS [AR Balance]
                                ,(SELECT ISNULL(A.Debit,0) FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.JDT1 A 
                                INNER JOIN [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.OJDT B ON A.TransId = B.TransId 
                                WHERE A.ContraAct COLLATE SQL_Latin1_General_CP1_CI_AS = F.SAPBPCode 
                                AND B.U_JETYPE = 'JE Adjustment' AND B.U_oID_OINV = F.SAPDocEntry)  AS [JE Amount]


                                ,F.LoanDate
                                ,F.LoanAmount
                                ,F.DownPayment
                                ,(SELECT SUM(A.Amount) FROM [vwsPayrollDetails] A WHERE A.EmployeeNo = F.EmployeeNo AND A.AccountCode = F.AccountCode AND A.LoanRefenceNo = F.LoanRefNo) AS [Payroll Payment]
                                ,(SELECT Z.Balance FROM [dbo].[fnGetBalancePerEmployeeLoan] ( F.EmployeeNo,F.AccountCode, F.LoanRefNo) Z)  AS [Loan Balance]
                                ,(SELECT SUM(A.Amount) FROM [vwsPayrollDetails] A WHERE A.EmployeeNo = F.EmployeeNo AND A.AccountCode = F.AccountCode AND A.LoanRefenceNo = F.LoanRefNo AND (A.Uploaded = 'Y' AND A.Uploaded IS NOT NULL)) AS [Uploaded Payroll Amount]
                                ,(SELECT SUM(A.Amount) FROM [vwsPayrollDetails] A WHERE A.EmployeeNo = F.EmployeeNo AND A.AccountCode = F.AccountCode AND A.LoanRefenceNo = F.LoanRefNo AND (A.Uploaded = 'N' OR A.Uploaded IS NULL)) AS [Pending Payroll Amount]


                                FROM [vwsLoanFile] F 
                                INNER JOIN [vwsEmployees] C ON F.EmployeeNo = C.EmployeeNo
                                INNER JOIN [vwsAccountCode] B ON F.AccountCode = B.AccountCode
                                INNER JOIN [vwsDepartmentList] D ON C.Department = D.DepartmentCode  


                                WHERE 
                                B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')
                                AND D.BCode LIKE '%" + _BCode + @"%'
                                AND D.Area LIKE '%" + cboArea.Text + @"%' 

                                ";

        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);

        btnUpload.Enabled = true;
        MessageBox.Show("Payroll Loan Processing Complete");

    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void button1_Click_1(object sender, EventArgs e)
    {

    }

    private void btnCheckingDisplay_Click(object sender, EventArgs e)
    {
        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }

        string _sqlPayrollDisplay = "";

        _sqlPayrollDisplay = @"

SELECT C.EmployeeNo, C.EmployeeName, B.AccountDesc, F.AccountCode, F.LoanRefNo, '131152' AS CashAccount
,F.SAPBPCode AS CardCode 
,F.SAPDocEntry AS DocEntry 
,C.Company
,D.BCode
,F.RelativeName 

,(SELECT A.DocDate FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[OINV] A WHERE A.DocEntry = F.SAPDocEntry )  AS [SAP Date]
,(SELECT A.U_DP FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[OINV] A WHERE A.DocEntry = F.SAPDocEntry )  AS [AR DownPayment]
,(SELECT A.DocTotal FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[OINV] A WHERE A.DocEntry = F.SAPDocEntry )  AS [AR Amount]
,(SELECT SUM(A.SumApplied) FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[RCT2] A WHERE A.DocEntry = F.SAPDocEntry ) AS [Applied Amount]
,(SELECT A.DocTotal - A.PaidToDate FROM [" + ConfigurationManager.AppSettings["sysDBServer"] + @"].[" + ConfigurationManager.AppSettings["sysDftDBCompany"] + @"].dbo.[OINV] A WHERE A.DocEntry = F.SAPDocEntry )  AS [AR Balance]

,F.LoanDate
,F.LoanAmount
,F.DownPayment
,(SELECT SUM(A.Amount) FROM [vwsPayrollDetails] A WHERE A.EmployeeNo = F.EmployeeNo AND A.AccountCode = F.AccountCode AND A.LoanRefenceNo = F.LoanRefNo) AS [Payroll Payment]
,(SELECT Z.Balance FROM [dbo].[fnGetBalancePerEmployeeLoan] ( F.EmployeeNo,F.AccountCode, F.LoanRefNo) Z)  AS [Loan Balance]
,(SELECT SUM(A.Amount) FROM [vwsPayrollDetails] A WHERE A.EmployeeNo = F.EmployeeNo AND A.AccountCode = F.AccountCode AND A.LoanRefenceNo = F.LoanRefNo AND (A.Uploaded = 'Y' AND A.Uploaded IS NOT NULL)) AS [Uploaded Payroll Amount]
,(SELECT SUM(A.Amount) FROM [vwsPayrollDetails] A WHERE A.EmployeeNo = F.EmployeeNo AND A.AccountCode = F.AccountCode AND A.LoanRefenceNo = F.LoanRefNo AND (A.Uploaded = 'N' OR A.Uploaded IS NULL)) AS [Pending Payroll Amount]


FROM [vwsLoanFile] F 
INNER JOIN [vwsEmployees] C ON F.EmployeeNo = C.EmployeeNo
INNER JOIN [vwsAccountCode] B ON F.AccountCode = B.AccountCode
INNER JOIN [vwsDepartmentList] D ON C.Department = D.DepartmentCode  


  WHERE 
  B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')
  AND D.BCode LIKE '%" + _BCode + @"%'
  AND D.Area LIKE '%" + cboArea.Text + @"%' 
                                                    ";
       

        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);


        btnUpload.Enabled = false;
    }

    private void button1_Click_2(object sender, EventArgs e)
    {
        panel1.Enabled = false;
        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }

        DialogResult result;
        result = MessageBox.Show("Data Generation for Loan Application Branch : " + _BCode, "Payroll Loan Generation", MessageBoxButtons.OKCancel);
        if (result == System.Windows.Forms.DialogResult.Cancel)
        {
            return;
        }

        DataDisplay();

        btnUpload.Enabled = true;
        MessageBox.Show("Payroll Loan Processing Complete");

    }

    private void button2_Click(object sender, EventArgs e)
    {

        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }


        foreach (DataGridViewRow row in dgvDisplay.Rows)
        {
            Application.DoEvents();


            string _CardCode = row.Cells["CardCode"].Value.ToString();
            string _EmployeeNo = row.Cells["EmployeeNo"].Value.ToString();
            string _AccountCode = row.Cells["AccountCode"].Value.ToString();
            string _LoanRefNo = row.Cells["LoanRefNo"].Value.ToString();

            string _Company = row.Cells["Company"].Value.ToString();


            string _synPayroll = @"SELECT A.PayrollPeriod FROM vwsPayrollDetails A WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanRefNo + @"'";
            DataTable _dtPayroll = new DataTable();
            _dtPayroll = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _synPayroll);


            foreach (DataRow row1 in _dtPayroll.Rows)
            {
                string _Payroll = row1[0].ToString();

                string _SQLSyntax = @"SELECT * FROM ORCT A WHERE A.Canceled = 'N' AND A.U_DocNum = '" + _Payroll + @"' AND A.U_Branch = '" + _BCode + @"' AND A.CardCode = '" + _CardCode + @"' AND A.U_AR = '" + _LoanRefNo + @"'";
                DataTable _dt = new DataTable();
                _dt = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _SQLSyntax);

                if (_dt.Rows.Count == 0)
                {

                    string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                    _SQLSyntax = @"UPDATE A SET A.[Uploaded] = 'N', A.[SAPError] = '' , A.DateUploaded = GETDATE() 
                                FROM PayrollDetails A  WHERE A.EmployeeNo = '" + _EmployeeNo + @"' 
                                AND A.AccountCode = '" + _AccountCode + @"' 
                                AND A.LoanRefenceNo = '" + _LoanRefNo + @"'
                                AND A.PayrollPeriod = '" + _Payroll + @"'

                    ";

                    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);
                }
            }


            //Application.DoEvents();
            //_Count++;

            //tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            //pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }



        DataDisplay();
        //button1_Click_2(sender, e);

        MessageBox.Show("Data Reset.");

        panel1.Enabled = true;
        btnUpload.Enabled = true;
    }

    private void button3_Click(object sender, EventArgs e)
    {

        btnUpload.Enabled = false;
        double _RowCount;
        int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        string sysDftDBCompany = ConfigurationManager.AppSettings["sysDftDBCompany"];
        string sysDBUsername = ConfigurationManager.AppSettings["sysSAPUsername"];
        string sysDBPassword = ConfigurationManager.AppSettings["sysSAPPassword"];

        bool isConnected = false;
        string _Msg = "";
        clsSAPFunctions.oCompany = clsSAPFunctions.SAPConnection(sysDftDBCompany, sysDBUsername, sysDBPassword, out isConnected, out _Msg);

        if (isConnected == false)
        {
            MessageBox.Show(_Msg);
            return;
        }


        foreach (DataRow row in _DataList.Rows)
        {
            Application.DoEvents();


            string _BCode = row["BCode"].ToString();
            string _CardCode = row["CardCode"].ToString();
            string _DocEntry = row["DocEntry"].ToString();
            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _EmployeeName = row["EmployeeName"].ToString();
            //string _Amount = row["Amount"].ToString();
            string _CashAccount = row["CashAccount"].ToString();

            string _PendingAmt = row["Pending Payroll Amount"].ToString();
            //string _DateTwo = row["DateTwo"].ToString();

            string _AccountCode = row["AccountCode"].ToString();
            string _LoanReferenceNo = row["LoanRefNo"].ToString();
            string _Company = row["Company"].ToString();


            string _ARBal = row["AR Balance"].ToString();
            string _Overpay = row["Pay Bal Amount"].ToString();

            //string _InstlmntID = row["SAPInsID"].ToString();
            //string _PayrollPeriod = row["PayrollPeriod"].ToString();

            if (_DocEntry != "")
            {
                if (double.Parse(_ARBal) == 0)
                {
                    if (double.Parse(_PendingAmt) != 0 || double.Parse(_Overpay) != 0)
                    {
                        clsSAPFunctions.OverPaymentJE( _BCode, _EmployeeNo, _EmployeeName, _AccountCode, _LoanReferenceNo, _CardCode, _DocEntry, _Company, isConnected);
                    }
                }
            }

            Application.DoEvents();
            _Count++;

            tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }

        clsSAPFunctions.oCompany.Disconnect();
        //button1_Click_2(sender, e);

        DataDisplay();
        MessageBox.Show("Over Payment Data Uploaded.");

        panel1.Enabled = true;
        btnUpload.Enabled = true;
    }


    public void DataDisplay()
    {
        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }

        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"

                                SELECT C.EmployeeNo, C.EmployeeName, B.AccountDesc, F.AccountCode, F.LoanRefNo, '131152' AS CashAccount
                                , F.SAPBPCode AS CardCode 
                                , F.SAPDocEntry AS DocEntry 
                                , C.Company
                                , D.BCode
                                , F.RelativeName 

                                , '' AS [SAP Date]
                                , 0.00 AS [AR DownPayment]
                                , 0.00 AS [AR Amount]
                                , 0.00 AS [Applied Amount]
                                , 0.00 AS [AR Balance]
                                , 0.00 AS [JE Amount]

                                , F.LoanDate
                                , F.LoanAmount
                                , F.DownPayment
                                , 0.00 AS [Payroll Payment]
                                , 0.00 AS [Loan Balance]
                                , 0.00 AS [Uploaded Payroll Amount]
                                , 0.00 AS [Pending Payroll Amount]
                                , 0.00 AS [Branch Amount]
                                , 0.00 AS [Pay Bal Amount]

                                FROM [vwsLoanFile] F 
                                INNER JOIN [vwsEmployees] C ON F.EmployeeNo = C.EmployeeNo
                                INNER JOIN [vwsAccountCode] B ON F.AccountCode = B.AccountCode
                                INNER JOIN [vwsDepartmentList] D ON C.Department = D.DepartmentCode  


                                  WHERE 
                                  B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')
                                  AND D.BCode LIKE '%" + _BCode + @"%'
                                  AND D.Area LIKE '%" + cboArea.Text + @"%' 
                                                    ";

        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);



        foreach (DataRow row in _DataList.Rows)
        {
            Application.DoEvents();


            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _AccountCode = row["AccountCode"].ToString();
            string _LoanRefNo = row["LoanRefNo"].ToString();

            string _CardCode = row["CardCode"].ToString();
            string _DocEntry = row["DocEntry"].ToString();

            string _Company = row["Company"].ToString();

            string _sqlData = "";
            if (_CardCode != "" && _DocEntry != "")
            {
                if (_LoanRefNo == "04819")
                {
                    MessageBox.Show(_EmployeeNo);
                }

                _sqlData = @"SELECT A.DocEntry,A.DocDate,A.U_DP,A.DocTotal,(A.DocTotal - A.PaidToDate) AS Balance   FROM [OINV] A WHERE A.DocEntry = '" + _DocEntry + "' AND A.CardCode = '" + _CardCode + "' ";


                string _dateValue = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlData, "DocDate");
                if (_dateValue != "")
                {
                    row["SAP Date"] = DateTime.Parse(_dateValue).ToShortDateString();
                }


                //row["SAP Date"] = DateTime.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlData, "DocDate")).ToShortDateString();
                row["AR DownPayment"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "U_DP").ToString("N2");
                row["AR Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "DocTotal").ToString("N2");
                row["AR Balance"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "Balance").ToString("N2");
                row["DocEntry"] = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlData, "DocEntry");

                _sqlData = @"SELECT SUM(A.SumApplied) AS SumApplied FROM [RCT2] A WHERE A.DocEntry = '" + _DocEntry + "'  AND InvType = '13'";
                row["Applied Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "SumApplied").ToString("N2");

                _sqlData = @"SELECT ISNULL(A.Debit,0) AS [Amount] FROM JDT1 A 
                                        INNER JOIN OJDT B ON A.TransId = B.TransId 
                                        WHERE A.ShortName = '" + _CardCode + @"'
                                        AND B.U_JETYPE = 'JE Adjustment' AND B.U_oID_OINV = '" + _DocEntry + "'";
                row["JE Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "Amount").ToString("N2");

            }


            _sqlData = @"SELECT  ISNULL(SUM(A.LoanAmount + ISNULL(A.LoanInterest, 0)), 0) AS [Amount] FROM vwsLoanFile A WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.AccountCode = '" + _AccountCode + "' AND A.LoanRefNo = '" + _LoanRefNo + "'";
            string _LoanAmount = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");



            _sqlData = @"  SELECT ISNULL(SUM(A.Amount), 0) AS Amount  FROM vwsLoanPayment A
  WHERE A.Type = 'PAYROLL'
AND A.EmployeeNo = '" + _EmployeeNo + "' AND A.AccountCode = '" + _AccountCode + "' AND A.LoanRefNo = '" + _LoanRefNo + "'";
            string _PayrollAmount = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");

            _sqlData = @"  SELECT ISNULL(SUM(A.Amount), 0) AS Amount   FROM vwsLoanPayment A
  WHERE  A.Type <> 'PAYROLL'
AND A.EmployeeNo = '" + _EmployeeNo + "' AND A.AccountCode = '" + _AccountCode + "' AND A.LoanRefNo = '" + _LoanRefNo + "'";
            string _CashAmount = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");



            row["Payroll Payment"] = double.Parse(_PayrollAmount).ToString("N2");
            row["Loan Balance"] = (double.Parse(_LoanAmount) - (double.Parse(_PayrollAmount) + double.Parse(_CashAmount))).ToString("N2");





            _sqlData = @"SELECT SUM(CASE WHEN ISNULL(A.WithInterest, 0) = 0 THEN A.Amount ELSE A.PrincipalAmt END) 
- SUM(CASE WHEN ISNULL(A.OPPosted ,'0') = '1' THEN 0 ELSE ISNULL(A.PaymentBalance,0) END)
AS Amount FROM vwsPayrollDetails A 
INNER JOIN vwsLoanFile B on  A.EmployeeNo = B.EmployeeNo AND A.AccountCode = B.AccountCode AND A.LoanRefenceNo = B.LoanRefNo
INNER JOIN vwsPayrollHeader C on A.EmployeeNo = C.EmployeeNo AND A.PayrollPeriod = C.PayrollPeriod
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanRefNo + @"' 
                                                AND (A.Uploaded = 'Y' AND A.Uploaded IS NOT NULL) 
                                                AND C.PayrollType = 'PAYROLL'";
            row["Uploaded Payroll Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");





            //_sqlData = @"SELECT SUM(CASE WHEN ISNULL(A.WithInterest, 0) = 0 THEN A.Amount ELSE A.PrincipalAmt END) AS Amount FROM vwsPayrollDetails A INNER JOIN vwsLoanFile B on  A.EmployeeNo = B.EmployeeNo AND A.AccountCode = B.AccountCode AND A.LoanRefenceNo = B.LoanRefNo
            //                                    WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanRefNo + @"' 
            //                                    AND (A.Uploaded = 'N' OR A.Uploaded IS NULL OR A.Uploaded = '' ) 
            //AND A.PayrollPeriod >= (CASE WHEN B.BranchAccount = 1  THEN (CASE WHEN B.PostToSAP = 1 THEN B.StartOfPosting ELSE A.PayrollPeriod END) ELSE A.PayrollPeriod END)";
            //row["Pending Payroll Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");



            _sqlData = @"SELECT ISNULL(SUM(XX.Amount),0) AS Amount FROM 
(
SELECT 
A.PayrollPeriod,
(CASE WHEN ISNULL(A.WithInterest, 0) = 0 THEN A.Amount ELSE A.PrincipalAmt END)

AS Amount 
,A.LoanRefenceNo, B.BranchAccount , B.StartOfPosting
,
CASE WHEN  B.BranchAccount = 1 THEN 

	CASE WHEN B.PostToSAP = 1 THEN 
			CASE WHEN 
					(A.PayrollPeriod < (CASE WHEN B.PostToSAP = 1 THEN B.StartOfPosting ELSE A.PayrollPeriod END)) 
					THEN 
					'BC'
			ELSE 
					'LS' 
			END
	ELSE 
			'BC'
	END

ELSE 'LS' END AS [Type]


FROM vwsPayrollDetails A 
INNER JOIN vwsLoanFile B on  A.EmployeeNo = B.EmployeeNo AND A.AccountCode = B.AccountCode AND A.LoanRefenceNo = B.LoanRefNo
INNER JOIN vwsPayrollHeader C on A.EmployeeNo = C.EmployeeNo AND A.PayrollPeriod = C.PayrollPeriod
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanRefNo + @"' 
                                                AND (A.Uploaded = 'N' OR A.Uploaded IS NULL) 
												--AND A.PayrollPeriod >= (CASE WHEN B.BranchAccount = 1  THEN (CASE WHEN B.PostToSAP = 1 THEN B.StartOfPosting ELSE A.PayrollPeriod END) ELSE A.PayrollPeriod END)
												--AND B.BranchAccount = 1
                                                AND C.PayrollType = 'PAYROLL'

) XX
WHERE XX.Type = 'LS'";
            row["Pending Payroll Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");






            _sqlData = @"SELECT ISNULL(SUM(XX.Amount),0) AS Amount FROM 
(
SELECT 
A.PayrollPeriod,
(CASE WHEN ISNULL(A.WithInterest, 0) = 0 THEN A.Amount ELSE A.PrincipalAmt END)

AS Amount 
,A.LoanRefenceNo, B.BranchAccount , B.StartOfPosting
,
CASE WHEN  B.BranchAccount = 1 THEN 

	CASE WHEN B.PostToSAP = 1 THEN 
			CASE WHEN 
					(A.PayrollPeriod < (CASE WHEN B.PostToSAP = 1 THEN B.StartOfPosting ELSE A.PayrollPeriod END)) 
					THEN 
					'BC'
			ELSE 
					'LS' 
			END
	ELSE 
			'BC'
	END

ELSE 'LS' END AS [Type]


FROM vwsPayrollDetails A 
INNER JOIN vwsLoanFile B on  A.EmployeeNo = B.EmployeeNo AND A.AccountCode = B.AccountCode AND A.LoanRefenceNo = B.LoanRefNo
INNER JOIN vwsPayrollHeader C on A.EmployeeNo = C.EmployeeNo AND A.PayrollPeriod = C.PayrollPeriod

                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanRefNo + @"' 
                                                AND (A.Uploaded = 'N' OR A.Uploaded IS NULL) 
												--AND A.PayrollPeriod >= (CASE WHEN B.BranchAccount = 1  THEN (CASE WHEN B.PostToSAP = 1 THEN B.StartOfPosting ELSE A.PayrollPeriod END) ELSE A.PayrollPeriod END)
												--AND B.BranchAccount = 1
                                                AND C.PayrollType = 'PAYROLL'

) XX
WHERE XX.Type = 'BC'";
            row["Branch Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");


            _sqlData = @"SELECT SUM(CASE WHEN ISNULL(A.OPPosted ,'0') = '1' THEN 0 ELSE ISNULL(A.PaymentBalance,0) END) AS Amount FROM vwsPayrollDetails A 
INNER JOIN vwsLoanFile B on  A.EmployeeNo = B.EmployeeNo AND A.AccountCode = B.AccountCode AND A.LoanRefenceNo = B.LoanRefNo
INNER JOIN vwsPayrollHeader C on A.EmployeeNo = C.EmployeeNo AND A.PayrollPeriod = C.PayrollPeriod
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanRefNo + @"'
                                                AND C.PayrollType = 'PAYROLL'";
            row["Pay Bal Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");



            //Application.DoEvents();
            //_Count++;

            //tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            //pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }




        btnOVPay.Enabled = false;
        foreach (DataRow row in _DataList.Rows)
        {
            Application.DoEvents();

            string _DocEntry = row["DocEntry"].ToString();
            string _PendingAmt = row["Pending Payroll Amount"].ToString();
            string _ARBal = row["AR Balance"].ToString();
            string _Overpay = row["Pay Bal Amount"].ToString();

            if (_DocEntry != "")
            {
                if (double.Parse(_ARBal) == 0)
                {
                    if (double.Parse(_PendingAmt) != 0 || double.Parse(_Overpay) != 0)
                    {
                        btnOVPay.Enabled = true;
                    }
                }
            }


        }
    }
}
