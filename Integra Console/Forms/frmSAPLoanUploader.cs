using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
public partial class frmSAPLoanUploader : Form
{
    private static DataTable _DataList = new DataTable();

    public static string _gCompany;
    public static string _gEmpCode;
    public static string _gEmpName;
    public static string _gPayrollPeriod;


    public static DateTime _PayrollDate;
    public static DateTime _FirstDay;
    public frmSAPLoanUploader()
    {
        InitializeComponent();
    }

    private void frmSAPLoanUploader_Load(object sender, EventArgs e)
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

        

        _SQLSyntax = "SELECT  [PayrollPeriod]  FROM dbo.[vwsPayrollPeriod] A";
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

        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please select payroll period");
            return;
        }

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
            string _Amount = row["Amount"].ToString();
            string _CashAccount = row["CashAccount"].ToString();

            //string _DateTwo = row["DateTwo"].ToString();

            string _AccountCode = row["AccountCode"].ToString();
            string _LoanReferenceNo = row["LoanRefenceNo"].ToString();
            string _Company = row["Company"].ToString();
            string _InstlmntID = row["SAPInsID"].ToString();

            if (_DocEntry != "")
            {

                    clsSAPFunctions.CreateIncomingPayment(_BCode, int.Parse(_DocEntry), _CardCode, _EmployeeName, _CashAccount
                                                        , _PayrollDate, _EmployeeNo, _AccountCode, _LoanReferenceNo, _Company, isConnected);

            }
            else
            {
                string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                string _SQLSyntaxUpdate = @"UPDATE A SET A.[SAPError] = 'Missing AR Document'  FROM PayrollDetails A  
                                            WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"' 
                                            AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanReferenceNo + @"'
                                                            ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntaxUpdate);
            }

            Application.DoEvents();
            _Count++;

            tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }

        clsSAPFunctions.oCompany.Disconnect();
        btnCheckingDisplay_Click(sender, e);

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
        frmManualPayroll._gPayrollPeriod = cboPayrolPeriod.Text;
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
        frmPayrollTransaction._gPayrollPeriod = cboPayrolPeriod.Text;
        frmPayrollTransaction.Show();
    }

    private void cboPayrolPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string _Year = Microsoft.VisualBasic.Strings.Left(cboPayrolPeriod.Text, 4);
            string _Month = Microsoft.VisualBasic.Strings.Mid(cboPayrolPeriod.Text, 6, 2);
            string _DayTag = Microsoft.VisualBasic.Strings.Right(cboPayrolPeriod.Text, 1);

            _FirstDay = DateTime.Parse(_Month + "/1/" + _Year);
            if(_DayTag == "A")
            {
                _PayrollDate = DateTime.Parse(_FirstDay.AddDays(14).ToString("MM/dd/yyyy"));
            }
            else
            {
                if(_Month == "02")
                {
                    _PayrollDate = DateTime.Parse(_FirstDay.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy"));
                }
                else
                {
                    _PayrollDate = DateTime.Parse(_FirstDay.AddDays(29).ToString("MM/dd/yyyy"));
                }

            }

            label4.Text = _PayrollDate.ToShortDateString();
        }
        catch
        {
            label4.Text = "MM/DD/YYYY";
        }
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {

        DialogResult result;
        result = MessageBox.Show("Data Generation for Loan Application Payroll Period : " + cboPayrolPeriod.Text, "Payroll Loan Generation", MessageBoxButtons.OKCancel);
        if (result == System.Windows.Forms.DialogResult.Cancel)
        {
            return;
        }


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



        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"

SELECT C.EmployeeNo, C.EmployeeName, B.AccountDesc, A.AccountCode, A.LoanRefenceNo, A.Amount, E.DateTwo, '131152' AS CashAccount
,F.SAPBPCode AS CardCode 
,F.SAPDocEntry AS DocEntry 
,A.SAPError 
,A.SAPInsDate
,A.SAPInsID
,C.Company
,D.BCode
  FROM [vwsPayrollDetails] A 
  INNER JOIN [vwsAccountCode] B ON A.AccountCode = B.AccountCode  
  INNER JOIN [vwsEmployees] C ON A.EmployeeNo = C.EmployeeNo  
  INNER JOIN [vwsDepartmentList] D ON C.Department = D.DepartmentCode  
  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
  INNER JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    

  WHERE 
  A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'
  AND B.AccountType IN (7,8)
  AND B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')
  AND D.BCode LIKE '%" + _BCode + @"%'
  AND D.Area LIKE '%" + cboArea.Text + @"%' 
  AND F.SAPARCount = 1
  AND (A.Uploaded IS NULL OR A.Uploaded = 'N')
  AND A.Amount <> 0

                                                    ";

        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);








        double _RowCount;
        int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            Application.DoEvents();

            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _LoanRefNo = row["LoanRefenceNo"].ToString();
            string _Company = row["Company"].ToString();
            string _AccountCode = row["AccountCode"].ToString();

            string _DocEntry = row["DocEntry"].ToString();





            string _sqlSAPData = "SELECT  MIN(Z.InstlmntID) AS InstlmntID FROM INV6 Z WHERE Z.DocEntry = '" + _DocEntry + @"' AND Z.Status = 'O'";
            string _InstlmntID = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "InstlmntID");

            _sqlSAPData = "SELECT A.DueDate FROM INV6 A WHERE A.InstlmntID = '" + _InstlmntID + @"' AND A.DocEntry = '" + _DocEntry + @"' AND A.Status = 'O'";
            string _DueDate = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "DueDate");




            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
            string _SQLSyntaxUpdate = @"UPDATE A SET A.[SAPInsID] = '" + _InstlmntID + @"' ,A.[SAPInsDate] = '" + _DueDate + @"'   FROM PayrollDetails A  
                                            WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"' 
                                            AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanRefNo + @"'
                                                            ";
            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntaxUpdate);





            // Excel Progress Monitoring
            Application.DoEvents();
            _Count++;

            //frmMainWindow frmMainWindow = new frmMainWindow();
            tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeNo + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }





        _sqlPayrollDisplay = @"

SELECT C.EmployeeNo, C.EmployeeName, B.AccountDesc, A.AccountCode, A.LoanRefenceNo, A.Amount, E.DateTwo, '131152' AS CashAccount
,F.SAPBPCode AS CardCode 
,F.SAPDocEntry AS DocEntry 
,A.SAPError 
,A.SAPInsDate
,A.SAPInsID
,C.Company
,D.BCode
  FROM [vwsPayrollDetails] A 
  INNER JOIN [vwsAccountCode] B ON A.AccountCode = B.AccountCode  
  INNER JOIN [vwsEmployees] C ON A.EmployeeNo = C.EmployeeNo  
  INNER JOIN [vwsDepartmentList] D ON C.Department = D.DepartmentCode  
  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
  INNER JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    

  WHERE 
  A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'
  AND B.AccountType IN (7,8)
  AND B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')
  AND D.BCode LIKE '%" + _BCode + @"%'
  AND D.Area LIKE '%" + cboArea.Text + @"%' 
  AND F.SAPARCount = 1
  AND (A.Uploaded IS NULL OR A.Uploaded = 'N')
AND A.Amount <> 0

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
SELECT C.EmployeeNo, C.EmployeeName, B.AccountDesc, A.AccountCode, A.LoanRefenceNo, A.Amount, E.DateTwo, '131152' AS CashAccount
,F.SAPBPCode AS CardCode 
,F.SAPDocEntry AS DocEntry 
,A.SAPError 
,A.SAPInsDate
,A.SAPInsID
,C.Company
,D.BCode
  FROM [vwsPayrollDetails] A 
  INNER JOIN [vwsAccountCode] B ON A.AccountCode = B.AccountCode  
  INNER JOIN [vwsEmployees] C ON A.EmployeeNo = C.EmployeeNo  
  INNER JOIN [vwsDepartmentList] D ON C.Department = D.DepartmentCode  
  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
  INNER JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    

  WHERE 
  A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'
  AND B.AccountType IN (7,8)
  AND B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')
  AND D.BCode LIKE '%" + _BCode + @"%'
  AND D.Area LIKE '%" + cboArea.Text + @"%' 
  AND F.SAPARCount = 1
  AND (A.Uploaded IS NULL OR A.Uploaded = 'N')
  AND A.Amount <> 0
                                                    ";

        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);


        btnUpload.Enabled = false;
    }
}
