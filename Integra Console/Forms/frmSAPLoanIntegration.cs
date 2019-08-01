using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
public partial class frmSAPLoanIntegration : Form
{
    private static DataTable _DataList = new DataTable();

    public static string _gCompany;
    public static string _gEmployeeNo;
    public static string _gLoanRefNo;
    public static string _gAccountCode;
    public static string _gEmpName;
    public static string _gPayrollPeriod;

    public static DateTime _PayrollDate;
    public static DateTime _FirstDay;
    public frmSAPLoanIntegration()
    {
        InitializeComponent();
    }

    private void frmSAPLoanIntegration_Load(object sender, EventArgs e)
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

        //_SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        //cboArea.Items.Clear();
        //cboArea.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboArea.Items.Add(row[0].ToString());
        //}

    }

    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataTable _DataTable;
        //string _SQLSyntax;

        //_SQLSyntax = "SELECT DISTINCT CONCAT(A.BCode,' - ',A.BName) AS Branch FROM dbo.[vwsDepartmentList] A WHERE A.Area = '" + cboArea.Text + "'";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        //cboBranch.Items.Clear();
        //cboBranch.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboBranch.Items.Add(row[0].ToString());
        //}
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



        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"
                                                SELECT * FROM vwsLoanFile A WHERE A.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519') AND ISNULL(A.SAPDocEntry,'') = ''
                                                AND A.EmployeeNo = '" + _gEmployeeNo + @"' AND A.AccountCode = '" + _gAccountCode + @"' AND A.LoanRefNo = '" + _gLoanRefNo + @"'
                                                    ";

        DataTable _LoanTable = new DataTable();
        _LoanTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);


        foreach (DataRow row in _LoanTable.Rows)
        {
            Application.DoEvents();

            //string _CardCode = row["SAPBPCode"].ToString();
            string _LoanDate = row["LoanDate"].ToString();
            string _FirstDueDate = row["FirstDueDate"].ToString();

            string _StartOfDeduction = row["StartOfDeduction"].ToString();
            string _IsTransfered = row["IsTransfered"].ToString();
            string _TranferredDate = row["TranferredDate"].ToString();

            string _Year = Microsoft.VisualBasic.Strings.Left(_StartOfDeduction, 4);
            string _Month = Microsoft.VisualBasic.Strings.Mid(_StartOfDeduction, 6, 2);
            string _DayTag = Microsoft.VisualBasic.Strings.Right(_StartOfDeduction, 1);

            _FirstDay = DateTime.Parse(_Month + "/1/" + _Year);
            if (_DayTag == "A")
            {
                _PayrollDate = DateTime.Parse(_FirstDay.AddDays(14).ToString("MM/dd/yyyy"));
            }
            else
            {
                if (_Month == "02")
                {
                    _PayrollDate = DateTime.Parse(_FirstDay.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy"));
                }
                else
                {
                    _PayrollDate = DateTime.Parse(_FirstDay.AddDays(29).ToString("MM/dd/yyyy"));
                }

            }

            if(_FirstDueDate == "")
            {
                _FirstDueDate = _PayrollDate.ToShortDateString();
            }
            
            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _AccountCode = row["AccountCode"].ToString();
            string _LoanAmount = row["LoanAmount"].ToString();
            string _LoanRefNo = row["LoanRefNo"].ToString();

            string _Terms = row["Terms"].ToString();
            string _Company = row["Company"].ToString();
            string _OrigBCode = row["OrigBCode"].ToString();
            string _Brand = row["Brand"].ToString();

            string _sqlSAPData = "SELECT  Z.EmployeeName FROM vwsEmployees Z WHERE Z.EmployeeNo = '" + _EmployeeNo + @"'";
            string _EmployeeName = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlSAPData, "EmployeeName");


            _sqlSAPData = "SELECT  Z.AccountDesc FROM vwsAccountCode Z WHERE Z.AccountCode = '" + _AccountCode + @"'";
            string _AccountName = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlSAPData, "AccountDesc");


            _sqlSAPData = "SELECT A.GroupNum FROM OCTG A WHERE A.InstNum = '" + _Terms + @"'";
            string _GroupNum = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "GroupNum");
            if (_GroupNum == "")
            {
                _GroupNum = "-1";
            }

            _sqlSAPData = "SELECT A.AcctCode FROM OACT A WHERE A.AccntntCod = '" + _OrigBCode + @"'";
            string _SAPAcctCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "AcctCode");



            _sqlSAPData = "SELECT A.CardCode FROM OCRD A WHERE A.AddID = '" + _EmployeeNo + @"'";
            string _CardCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "CardCode");






            _sqlSAPData = @"SELECT  Z.NumAtCard,  Z.CardCode, MAX(Z.DocEntry) AS DocEntry,  COUNT(Z.DocEntry) AS [ARCount]
                            FROM 
                            (SELECT 
	                            A.DocNum
	                            , A.DocEntry
	                            , A.NumAtCard
	                            , A.CardCode
	                            , A.DocTotal
	                            , A.PaidToDate
	                            , ISNULL((	SELECT SUM(DOCTOTAL) 
		                            FROM ORIN X 
			                            INNER JOIN RIN1 XA ON X.DocEntry = XA.DocEntry 
		                            WHERE XA.BaseEntry = A.DocEntry AND XA.BaseType = A.ObjType AND X.CANCELED = 'N'),0) [CM AMT]
                            FROM OINV A
                            WHERE A.CANCELED = 'N') Z
                            WHERE (Z.DOCTOTAL - Z.[CM AMT]) <> 0
                            AND Z.NumAtCard  = '" + _LoanRefNo + @"' AND Z.CardCode = '" + _CardCode + @"' 
                            GROUP BY Z.NumAtCard,  Z.CardCode
                            ORDER BY COUNT(Z.DocEntry) DESC
                            ";


            string _DocEntry = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "DocEntry");
            int _ARCount = int.Parse(clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlSAPData, "ARCount").ToString());

            string _ConCompany = "";

            if (_DocEntry != "")
            {
                _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                string _SQLSyntaxUpdate = @"UPDATE A SET A.SAPBPCode =  '" + _CardCode + @"', A.SAPDocEntry = '" + _DocEntry + @"', SAPARCount = '" + _ARCount + @"' FROM LoanFile A  
                                            WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefNo = '" + _LoanRefNo + @"'
              ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntaxUpdate);
            }

            if (_CardCode == "")
            {

                _sqlSAPData = @"declare @bpcount int
                                create table  ##bp (bp varchar(50))
                                insert into ##bp
                                select left(CardCode,5) bp from ocrd

                                select @bpcount = (select count(bp) from ##bp where right(bp,4) = LEFT(DBO.[RemoveNonAlphaCharacters]('" + _EmployeeName + @"'),4))

                                SELECT 'C' + UPPER(LEFT(DBO.[RemoveNonAlphaCharacters]('" + _EmployeeName + @"'),4))+ RIGHT('00000'+CAST(@bpcount+1 AS varchar(5)),5) AS [CardCode]

                                drop table ##bp";

                string _CodeValue = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "CardCode");


                clsSAPFunctions.CreateBP(_CodeValue, _EmployeeNo, _EmployeeName, _AccountCode, _LoanRefNo, _Company, isConnected);
            }

            _sqlSAPData = "SELECT A.CardCode FROM OCRD A WHERE A.AddID = '" + _EmployeeNo + @"'";
            _CardCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "CardCode");



            int valTrans = 0;
            int.TryParse(_IsTransfered, out valTrans);

            int monTerms = 1;
            int.TryParse(_Terms, out monTerms);


            if (_DocEntry == "")
            {
                if (_CardCode != "")
                {
                    if (_Terms != "")
                    {
                        clsSAPFunctions.CreateARInvoice(DateTime.Parse(_LoanDate), DateTime.Parse(_FirstDueDate), _CardCode, _EmployeeNo, _EmployeeName, _SAPAcctCode, _AccountCode, _AccountName, _LoanRefNo, double.Parse(_LoanAmount), _OrigBCode, _Brand, int.Parse(_GroupNum), monTerms, valTrans, _TranferredDate, _Company, isConnected);
                    }
                    else
                    {
                        _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                        string _SQLSyntax = @"UPDATE A SET  A.[SAPError] = 'Terms Not Found'  FROM LoanFile A  WHERE A.EmployeeNo = '" + _EmployeeNo + "'  AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefNo = '" + _LoanRefNo + @"'
                                         ";

                        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);
                    }
                }
            }



            Application.DoEvents();
            _Count++;

            tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }

        clsSAPFunctions.oCompany.Disconnect();
        //btnGenerate_Click(sender, e);

        //MessageBox.Show("Data Uploaded.");
        
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnReProcess_Click(object sender, EventArgs e)
    {
        
    }

    private void openInManualPayrollToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            _gLoanRefNo = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
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

                _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                _gLoanRefNo = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();

            }
        }
        catch
        {

        }
    }

    private void updatePayrollTimeRecordToolStripMenuItem_Click(object sender, EventArgs e)
    {

        //foreach (Form form in Application.OpenForms)
        //{
        //    if (form.GetType() == typeof(frmPayrollTransaction))
        //    {
        //        form.Activate();
        //        return;
        //    }
        //}

        //frmPayrollTransaction frmPayrollTransaction = new frmPayrollTransaction();
        //frmPayrollTransaction.MdiParent = ((frmMainWindow)(this.MdiParent));

        //frmPayrollTransaction._gCompany = _gCompany;
        //frmPayrollTransaction._gEmpCode = _gEmpCode;
        //frmPayrollTransaction._gEmpName = _gEmpName;
        //frmPayrollTransaction.Show();
    }

    private void cboPayrolPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        //DateTime _ActiveDate = DateTime.Parse(DateTime.Parse(DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString("00")).ToString("MM/dd/yyyy"));
        DateTime _ActiveDate = DateTime.Now.AddDays(DateTime.Today.Day * -1);

        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"
                                SELECT * FROM vwsLoanFile A 
                                         WHERE A.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519') 
                                         AND ISNULL(A.SAPDocEntry,'') = '' 
                                         AND A.LoanAmount <> 0
                                         AND (CASE WHEN ISNULL(A.IsTransfered,0) = 1 THEN A.TranferredDate ELSE A.LoanDate END) <= '" + _ActiveDate.ToShortDateString()  + @"'
                               ";

        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);

        double _RowCount;
        int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ")";
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

    }

    private void button1_Click_2(object sender, EventArgs e)
    {

        //double parsedValue;
        double _RowCount;
        int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            Application.DoEvents();

            _gEmployeeNo = row["EmployeeNo"].ToString();
            _gLoanRefNo = row["LoanRefNo"].ToString();
            _gAccountCode = row["AccountCode"].ToString();


            btnUpload_Click(sender, e);

            Application.DoEvents();
            _Count++;

            //frmMainWindow frmMainWindow = new frmMainWindow();
            tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _gEmployeeNo + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }
    }
}
