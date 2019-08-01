using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
public partial class frmSAPBranchPayrollUploader : Form
{
    private static DataTable _DataList = new DataTable();

    public static string _gCompany;
    public static string _gEmpCode;
    public static string _gEmpName;
    public static string _gPayrollPeriod;


    public static DateTime _PayrollDate;
    public static DateTime _FirstDay;
    public frmSAPBranchPayrollUploader()
    {
        InitializeComponent();
    }

    private void frmSAPBranchPayrollUploader_Load(object sender, EventArgs e)
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

        btnUpload.Enabled = false;
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

        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }


        btnUpload.Enabled = false;
        double _RowCount;
        //int _Count = 0;
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

        clsSAPFunctions.CreateBranchJournalEntry(_BCode, cboPayrolPeriod.Text, DateTime.Parse(label4.Text), _DataList, isConnected);


        clsSAPFunctions.oCompany.Disconnect();
        //btnCheckingDisplay_Click(sender, e);

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

        btnUpload.Enabled = false;
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

        string _sqlSAPData = "SELECT A.AcctCode FROM OACT A WHERE A.AccntntCod = '" + _BCode + @"'";
        //string _sqlSAPData = "SELECT CASE WHEN '" + _BCode + @"' = 'DESIHOFC' THEN '201013' ELSE A.AcctCode END AS AcctCode FROM OACT A WHERE A.AccntntCod = '" + _BCode + @"'";
        string _SAPAcctCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "AcctCode");

        if (_BCode == "DESIHOFC")
        {
            _SAPAcctCode = "VPAYR00001";
        }

        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"
DECLARE @PAYROLLPERIOD NVARCHAR(20)
DECLARE @BRANCH NVARCHAR(20)
DECLARE @AREA NVARCHAR(20)

SET @PAYROLLPERIOD =  '" + cboPayrolPeriod.Text + @"'
SET @BRANCH = '" + _BCode + @"'


SELECT XXX.EMPLOYEE, XXX.EMPLOYEENAME, XXX.[JEACCT], XXX.ACCOUNT, XXX.ACCOUNTNAME, XXX.SUDENTRY, XXX.DEBIT, XXX.CREDIT FROM (


SELECT 1 ID ,ZZ.EMPLOYEE, ZZ.EMPLOYEENAME, ZZ.ACCOUNT AS [JEACCT], ZZ.ACCOUNT, ZZ.ACCOUNTNAME, ZZ.SUDENTRY, 0.00 AS DEBIT, SUM(ZZ.Amount) AS CREDIT FROM (


  
SELECT 

''  AS EMPLOYEE,'' AS EMPLOYEENAME
,CASE 
WHEN B.AccountCode  IN ('004')  THEN 'SSS EE' -- SSS Employee
WHEN B.AccountCode  IN ('005')  THEN 'SSS ER' -- SSS Employer
WHEN B.AccountCode  IN ('006')  THEN 'SSS ER' -- SSSEC Employer
WHEN B.AccountCode  IN ('007')  THEN 'PHIL EE' -- PhilHealth Employee
WHEN B.AccountCode  IN ('008')  THEN 'PHIL ER' -- PhilHealth Employer
WHEN B.AccountCode  IN ('009')  THEN 'PAGIBIG EE' -- PagIbig Employee
WHEN B.AccountCode  IN ('010')  THEN 'PAGIBIG ER' -- PagIbig Employer
WHEN B.AccountCode  IN ('011')  THEN 'BIR EE' -- Withholding Tax
ELSE
'000'
END
AS ACCOUNTNAME

,CASE 
WHEN B.AccountCode  IN ('004')  THEN 'VSOCI00001' -- SSS Employee
WHEN B.AccountCode  IN ('005')  THEN 'VSOCI00001' -- SSS Employer
WHEN B.AccountCode  IN ('006')  THEN 'VSOCI00001' -- SSSEC Employer
WHEN B.AccountCode  IN ('007')  THEN 'VPHIL00002' -- PhilHealth Employee
WHEN B.AccountCode  IN ('008')  THEN 'VPHIL00002' -- PhilHealth Employer
WHEN B.AccountCode  IN ('009')  THEN 'VPAGI00001' -- PagIbig Employee
WHEN B.AccountCode  IN ('010')  THEN 'VPAGI00001' -- PagIbig Employer
WHEN B.AccountCode  IN ('011')  THEN 'VBURE00001' -- Withholding Tax
ELSE
'000'
END
AS ACCOUNT


,CASE 
WHEN B.AccountCode  IN ('004')  THEN '' -- SSS Employee
WHEN B.AccountCode  IN ('005')  THEN '' -- SSS Employer
WHEN B.AccountCode  IN ('006')  THEN '' -- SSSEC Employer
WHEN B.AccountCode  IN ('007')  THEN '' -- PhilHealth Employee
WHEN B.AccountCode  IN ('008')  THEN '' -- PhilHealth Employer
WHEN B.AccountCode  IN ('009')  THEN '' -- PagIbig Employee
WHEN B.AccountCode  IN ('010')  THEN '' -- PagIbig Employer
WHEN B.AccountCode  IN ('011')  THEN '' -- Withholding Tax
ELSE
''
END
AS SUDENTRY

, 0.00 AS DEBIT, 0.00 AS CREDIT, A.Amount
, A.AccountCode AS [LOAN ACCOUNT], '' AS DOCDATE
, D.BCode
  FROM [vwsPayrollDetails] A 
  INNER JOIN [vwsEmployees] C ON A.EmployeeNo = C.EmployeeNo  
  INNER JOIN [vwsDepartmentList] D ON A.Department = D.DepartmentCode  
  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
  INNER JOIN [vwsPayrollRegsCode] B ON A.AccountCode = B.AccountCode  
  INNER JOIN [vwsPayrollHeader] G ON A.EmployeeNo = G.EmployeeNo AND A.PayrollPeriod = G.PayrollPeriod AND G.PayrollType = 'PAYROLL'
  LEFT JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    

  WHERE 
  A.PayrollPeriod = @PAYROLLPERIOD
  AND CAST(LEFT(A.PayrollPeriod, 4) AS INT) < '2019'
  --AND B.AccountType IN (7,8)
  AND B.AccountCode  NOT IN ('005','006','008','010')
AND A.Branch = @BRANCH
  AND A.Amount <> 0		

UNION ALL

SELECT 

CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519','8-502','8-516','8-504','8-503','8-521','8-522','8-523','8-512') THEN ''
ELSE C.EmployeeNo END
AS EMPLOYEE,CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519','8-502','8-516','8-504','8-503','8-521','8-522','8-523','8-512') THEN ''
ELSE C.EmployeeName END AS EMPLOYEENAME
,CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519') THEN 

(CASE WHEN F.[BranchAccount] = 1 THEN 
        (CASE WHEN F.[PostToSAP] = 1 THEN 
            (CASE WHEN F.StartOfPosting > @PAYROLLPERIOD THEN  'LS BC / DUE TO/FROM' ELSE 'LOAN SUSPENSE ACCOUNT' END) 
        ELSE 'LS BC / DUE TO/FROM' 
        END)
ELSE 'LOAN SUSPENSE ACCOUNT' END) 


WHEN B.AccountCode  IN ('8-502')  THEN 'SSS LOAN'
WHEN B.AccountCode  IN ('8-516')  THEN 'SSS CALAMITY LOAN'
WHEN B.AccountCode  IN ('8-504')  THEN 'PAGIBIG LOAN'
WHEN B.AccountCode  IN ('8-503')  THEN 'PAGIBIG CALAMITY LOAN'
WHEN B.AccountCode  IN ('8-521')  THEN 'PAGIBIG MP2'
WHEN B.AccountCode  IN ('8-522')  THEN 'PAGIBIG HOUSING'
WHEN B.AccountCode  IN ('8-523')  THEN 'IDEAL VISION'
WHEN B.AccountCode  IN ('8-512')  THEN 'LENDING CAPITAL'
ELSE
UPPER(B.AccountDesc)
END
AS ACCOUNTNAME

,CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')  THEN 

(CASE WHEN F.[BranchAccount] = 1 THEN 

        (CASE WHEN F.[PostToSAP] = 1 THEN 
            (CASE WHEN F.StartOfPosting > @PAYROLLPERIOD THEN  '" + _SAPAcctCode + @"' ELSE '131152' END) 
        ELSE '" + _SAPAcctCode + @"'
        END)

ELSE '131152' END)  


WHEN B.AccountCode  IN ('8-502')  THEN 'VSOCI00001L' -- SSS LOAN
WHEN B.AccountCode  IN ('8-516')  THEN 'VSOCI00001L' -- SSS CALAMITY
WHEN B.AccountCode  IN ('8-504')  THEN 'VPAGI0000L' -- PAGIBIG LOAN
WHEN B.AccountCode  IN ('8-503')  THEN 'VPAGI0000L' -- PAGIBIG CALAMITY LOAN
WHEN B.AccountCode  IN ('8-521')  THEN 'VPAGI0000L' -- PAGIBIG MP2
WHEN B.AccountCode  IN ('8-522')  THEN 'VPAGI0000L' -- PAGIBIG HOUSING
WHEN B.AccountCode  IN ('8-523')  THEN 'VIDEA00002' -- IDEAL VISION
WHEN B.AccountCode  IN ('8-512')  THEN 'VDUEK00001' -- LENDING CAPITAL
ELSE
ISNULL(B.SAPAccount,'103006')
END
AS ACCOUNT
,'' AS SUDENTRY

, 0.00 AS DEBIT, 0.00 AS CREDIT
, CASE WHEN ISNULL(A.WithInterest,0) = 0 THEN ROUND(A.Amount,2) ELSE A.PrincipalAmt END AS Amount
, A.AccountCode AS [LOAN ACCOUNT], '' AS DOCDATE
, D.BCode
  FROM [vwsPayrollDetails] A 
  INNER JOIN [vwsEmployees] C ON A.EmployeeNo = C.EmployeeNo  
  INNER JOIN [vwsDepartmentList] D ON A.Department = D.DepartmentCode  
  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
  INNER JOIN [vwsPayrollHeader] G ON A.EmployeeNo = G.EmployeeNo AND A.PayrollPeriod = G.PayrollPeriod AND G.PayrollType = 'PAYROLL'
  LEFT JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    
  LEFT JOIN [vwsAccountCode] B ON A.AccountCode = B.AccountCode  
  WHERE 
  A.PayrollPeriod = @PAYROLLPERIOD
  AND CAST(LEFT(A.PayrollPeriod, 4) AS INT) < '2019'
  AND B.AccountType IN (7,8)
  --AND B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518')
AND A.Branch = @BRANCH
  AND A.Amount <> 0		





  -- fOR lOAN sUSPENCE aCCOUNT oNLY

  
UNION ALL

SELECT 

CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519','8-502','8-516','8-504','8-503','8-521','8-522','8-523','8-512') THEN ''
ELSE C.EmployeeNo END
AS EMPLOYEE,CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519','8-502','8-516','8-504','8-503','8-521','8-522','8-523','8-512') THEN ''
ELSE C.EmployeeName END AS EMPLOYEENAME
,CASE  
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')  THEN 'LS INTEREST'
WHEN B.AccountCode  IN ('8-502')  THEN 'SSS LOAN'
WHEN B.AccountCode  IN ('8-516')  THEN 'SSS CALAMITY LOAN'
WHEN B.AccountCode  IN ('8-504')  THEN 'PAGIBIG LOAN'
WHEN B.AccountCode  IN ('8-503')  THEN 'PAGIBIG CALAMITY LOAN'
WHEN B.AccountCode  IN ('8-521')  THEN 'PAGIBIG MP2'
WHEN B.AccountCode  IN ('8-522')  THEN 'PAGIBIG HOUSING'
WHEN B.AccountCode  IN ('8-523')  THEN 'IDEAL VISION'
WHEN B.AccountCode  IN ('8-512')  THEN 'LENDING CAPITAL'
ELSE
UPPER(B.AccountDesc)
END
AS ACCOUNTNAME

,CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')  THEN '401012'
WHEN B.AccountCode  IN ('8-502')  THEN 'VSOCI00001L' -- SSS LOAN
WHEN B.AccountCode  IN ('8-516')  THEN 'VSOCI00001L' -- SSS CALAMITY
WHEN B.AccountCode  IN ('8-504')  THEN 'VPAGI0000L' -- PAGIBIG LOAN
WHEN B.AccountCode  IN ('8-503')  THEN 'VPAGI0000L' -- PAGIBIG CALAMITY LOAN
WHEN B.AccountCode  IN ('8-521')  THEN 'VPAGI0000L' -- PAGIBIG MP2
WHEN B.AccountCode  IN ('8-522')  THEN 'VPAGI0000L' -- PAGIBIG HOUSING
WHEN B.AccountCode  IN ('8-523')  THEN 'VIDEA00002' -- IDEAL VISION
WHEN B.AccountCode  IN ('8-512')  THEN 'VDUEK00001' -- LENDING CAPITAL
ELSE
ISNULL(B.SAPAccount,'103006')
END
AS ACCOUNT
,'' AS SUDENTRY

, 0.00 AS DEBIT, 0.00 AS CREDIT

, CASE WHEN ISNULL(A.WithInterest,0) = 1 THEN A.InterestAmt ELSE 0 END AS Amount

, A.AccountCode AS [LOAN ACCOUNT], '' AS DOCDATE
, D.BCode
  FROM [vwsPayrollDetails] A 
  INNER JOIN [vwsEmployees] C ON A.EmployeeNo = C.EmployeeNo  
  INNER JOIN [vwsDepartmentList] D ON A.Department = D.DepartmentCode  
  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
  INNER JOIN [vwsPayrollHeader] G ON A.EmployeeNo = G.EmployeeNo AND A.PayrollPeriod = G.PayrollPeriod AND G.PayrollType = 'PAYROLL'
  LEFT JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    
  LEFT JOIN [vwsAccountCode] B ON A.AccountCode = B.AccountCode  
  WHERE 
  A.PayrollPeriod = @PAYROLLPERIOD
  AND CAST(LEFT(A.PayrollPeriod, 4) AS INT) < '2019'
  AND B.AccountType IN (7)
  AND B.AccountCode IN ('8-510','8-511','8-514','8-515','8-517','8-518')
AND A.Branch = @BRANCH
  AND (CASE WHEN ISNULL(A.WithInterest,0) = 1 THEN A.InterestAmt ELSE 0 END) <> 0		







  
  ) ZZ
  GROUP BY ZZ.ACCOUNT, ZZ.ACCOUNTNAME,ZZ.EMPLOYEE, ZZ.EMPLOYEENAME, ZZ.SUDENTRY


  UNION ALL

  
SELECT 0 ID , '' AS EMPLOYEE, '' AS EMPLOYEENAME, '" + _SAPAcctCode + @"' AS [JEACCT], ZZ.ACCOUNT, ZZ.ACCOUNTNAME, ZZ.SUDENTRY, SUM(ZZ.Amount) AS DEBIT, 0.00 AS CREDIT FROM (


  
SELECT 

''  AS EMPLOYEE,'' AS EMPLOYEENAME
,CASE 
WHEN B.AccountCode  IN ('004')  THEN 'SSS EE' -- SSS Employee
WHEN B.AccountCode  IN ('005')  THEN 'SSS ER' -- SSS Employer
WHEN B.AccountCode  IN ('006')  THEN 'SSS ER' -- SSSEC Employer
WHEN B.AccountCode  IN ('007')  THEN 'PHIL EE' -- PhilHealth Employee
WHEN B.AccountCode  IN ('008')  THEN 'PHIL ER' -- PhilHealth Employer
WHEN B.AccountCode  IN ('009')  THEN 'PAGIBIG EE' -- PagIbig Employee
WHEN B.AccountCode  IN ('010')  THEN 'PAGIBIG ER' -- PagIbig Employer
WHEN B.AccountCode  IN ('011')  THEN 'BIR EE' -- Withholding Tax
ELSE
'000'
END
AS ACCOUNTNAME

,CASE 
WHEN B.AccountCode  IN ('004')  THEN 'VSOCI00001' -- SSS Employee
WHEN B.AccountCode  IN ('005')  THEN 'VSOCI00001' -- SSS Employer
WHEN B.AccountCode  IN ('006')  THEN 'VSOCI00001' -- SSSEC Employer
WHEN B.AccountCode  IN ('007')  THEN 'VPHIL00002' -- PhilHealth Employee
WHEN B.AccountCode  IN ('008')  THEN 'VPHIL00002' -- PhilHealth Employer
WHEN B.AccountCode  IN ('009')  THEN 'VPAGI00001' -- PagIbig Employee
WHEN B.AccountCode  IN ('010')  THEN 'VPAGI00001' -- PagIbig Employer
WHEN B.AccountCode  IN ('011')  THEN 'VBURE00001' -- Withholding Tax
ELSE
'000'
END
AS ACCOUNT


,CASE 
WHEN B.AccountCode  IN ('004')  THEN '' -- SSS Employee
WHEN B.AccountCode  IN ('005')  THEN '601010' -- SSS Employer
WHEN B.AccountCode  IN ('006')  THEN '601010' -- SSSEC Employer
WHEN B.AccountCode  IN ('007')  THEN '' -- PhilHealth Employee
WHEN B.AccountCode  IN ('008')  THEN '601011' -- PhilHealth Employer
WHEN B.AccountCode  IN ('009')  THEN '' -- PagIbig Employee
WHEN B.AccountCode  IN ('010')  THEN '601012' -- PagIbig Employer
WHEN B.AccountCode  IN ('011')  THEN '' -- Withholding Tax
ELSE
'000'
END
AS SUDENTRY

, 0.00 AS DEBIT, 0.00 AS CREDIT, A.Amount
, A.AccountCode AS [LOAN ACCOUNT], '' AS DOCDATE
, D.BCode
  FROM [vwsPayrollDetails] A 
  INNER JOIN [vwsEmployees] C ON A.EmployeeNo = C.EmployeeNo  
  INNER JOIN [vwsDepartmentList] D ON A.Department = D.DepartmentCode  
  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
  INNER JOIN [vwsPayrollRegsCode] B ON A.AccountCode = B.AccountCode  
  INNER JOIN [vwsPayrollHeader] G ON A.EmployeeNo = G.EmployeeNo AND A.PayrollPeriod = G.PayrollPeriod AND G.PayrollType = 'PAYROLL'
  LEFT JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    

  WHERE 
  A.PayrollPeriod = @PAYROLLPERIOD
  AND CAST(LEFT(A.PayrollPeriod, 4) AS INT) < '2019'
  --AND B.AccountType IN (7,8)
  AND B.AccountCode  NOT IN ('005','006','008','010')
  AND A.Branch = @BRANCH
  AND A.Amount <> 0		

UNION ALL

SELECT 

CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519','8-502','8-516','8-504','8-503','8-521','8-522','8-523','8-512') THEN ''
ELSE C.EmployeeNo END
AS EMPLOYEE,CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519','8-502','8-516','8-504','8-503','8-521','8-522','8-523','8-512') THEN ''
ELSE C.EmployeeName END AS EMPLOYEENAME
,CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')  THEN 'LOAN SUSPENSE ACCOUNT'
WHEN B.AccountCode  IN ('8-502')  THEN 'SSS LOAN'
WHEN B.AccountCode  IN ('8-516')  THEN 'SSS CALAMITY LOAN'
WHEN B.AccountCode  IN ('8-504')  THEN 'PAGIBIG LOAN'
WHEN B.AccountCode  IN ('8-503')  THEN 'PAGIBIG CALAMITY LOAN'
WHEN B.AccountCode  IN ('8-521')  THEN 'PAGIBIG MP2'
WHEN B.AccountCode  IN ('8-522')  THEN 'PAGIBIG HOUSING'
WHEN B.AccountCode  IN ('8-523')  THEN 'IDEAL VISION'
WHEN B.AccountCode  IN ('8-512')  THEN 'LENDING CAPITAL'
ELSE
UPPER(B.AccountDesc)
END
AS ACCOUNTNAME

,CASE 
WHEN B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')  THEN '131152'
WHEN B.AccountCode  IN ('8-502')  THEN 'VSOCI00001L' -- SSS LOAN
WHEN B.AccountCode  IN ('8-516')  THEN 'VSOCI00001L' -- SSS CALAMITY
WHEN B.AccountCode  IN ('8-504')  THEN 'VPAGI0000L' -- PAGIBIG LOAN
WHEN B.AccountCode  IN ('8-503')  THEN 'VPAGI0000L' -- PAGIBIG CALAMITY LOAN
WHEN B.AccountCode  IN ('8-521')  THEN 'VPAGI0000L' -- PAGIBIG MP2
WHEN B.AccountCode  IN ('8-522')  THEN 'VPAGI0000L' -- PAGIBIG HOUSING
WHEN B.AccountCode  IN ('8-523')  THEN 'VIDEA00002' -- IDEAL VISION
WHEN B.AccountCode  IN ('8-512')  THEN 'VDUEK00001' -- LENDING CAPITAL
ELSE
ISNULL(B.SAPAccount,'103006')
END
AS ACCOUNT
,'' AS SUDENTRY

, 0.00 AS DEBIT, 0.00 AS CREDIT, ROUND(A.Amount,2) AS Amount
, A.AccountCode AS [LOAN ACCOUNT], '' AS DOCDATE
, D.BCode
  FROM [vwsPayrollDetails] A 
  INNER JOIN [vwsEmployees] C ON A.EmployeeNo = C.EmployeeNo  
  INNER JOIN [vwsDepartmentList] D ON A.Department = D.DepartmentCode  
  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
  INNER JOIN [vwsPayrollHeader] G ON A.EmployeeNo = G.EmployeeNo AND A.PayrollPeriod = G.PayrollPeriod AND G.PayrollType = 'PAYROLL'
  LEFT JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    
  LEFT JOIN [vwsAccountCode] B ON A.AccountCode = B.AccountCode  
  WHERE 
  A.PayrollPeriod = @PAYROLLPERIOD
  AND CAST(LEFT(A.PayrollPeriod, 4) AS INT) < '2019'
  AND B.AccountType IN (7,8)
  --AND B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518')
  AND A.Branch = @BRANCH
  AND A.Amount <> 0		
  
  ) ZZ
  GROUP BY ZZ.ACCOUNT, ZZ.ACCOUNTNAME, ZZ.SUDENTRY

  ) XXX
  ORDER BY XXX.ACCOUNTNAME, XXX.ID
  
                                                    ";

        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);

        double _TotalAmount = 0;
        foreach (DataRow row in _DataList.Rows)
        {
            double _Amount = double.Parse(row["CREDIT"].ToString());
            _TotalAmount = _TotalAmount + _Amount;  
        }

        lblTotalAmount.Text = "Total Amount : " + _TotalAmount.ToString("N2");



        //double _RowCount;
        //int _Count = 0;
        //_RowCount = _DataList.Rows.Count;

        //foreach (DataRow row in _DataList.Rows)
        //{
        //    Application.DoEvents();

        //    string _EmployeeNo = row["EmployeeNo"].ToString();
        //    string _LoanRefNo = row["LoanRefenceNo"].ToString();
        //    string _Company = row["Company"].ToString();
        //    string _AccountCode = row["AccountCode"].ToString();

        //    string _DocEntry = row["DocEntry"].ToString();





        //    string _sqlSAPData = "SELECT  MIN(Z.InstlmntID) AS InstlmntID FROM INV6 Z WHERE Z.DocEntry = '" + _DocEntry + @"' AND Z.Status = 'O'";
        //    string _InstlmntID = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "InstlmntID");

        //    _sqlSAPData = "SELECT A.DueDate FROM INV6 A WHERE A.InstlmntID = '" + _InstlmntID + @"' AND A.DocEntry = '" + _DocEntry + @"' AND A.Status = 'O'";
        //    string _DueDate = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "DueDate");




        //    string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
        //    string _SQLSyntaxUpdate = @"UPDATE A SET A.[SAPInsID] = '" + _InstlmntID + @"' ,A.[SAPInsDate] = '" + _DueDate + @"'   FROM PayrollDetails A  
        //                                    WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"' 
        //                                    AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _LoanRefNo + @"'
        //                                                    ";
        //    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntaxUpdate);





        //    // Excel Progress Monitoring
        //    Application.DoEvents();
        //    _Count++;

        //    //frmMainWindow frmMainWindow = new frmMainWindow();
        //    tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeNo + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
        //    pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        //}





        //        _sqlPayrollDisplay = @"

        //SELECT C.EmployeeNo, C.EmployeeName, B.AccountDesc, A.AccountCode, A.LoanRefenceNo, A.Amount, E.DateTwo, '131152' AS CashAccount
        //,F.SAPBPCode AS CardCode 
        //,F.SAPDocEntry AS DocEntry 
        //,A.SAPError 
        //,A.SAPInsDate
        //,A.SAPInsID
        //,C.Company
        //,D.BCode
        //  FROM [vwsPayrollDetails] A 
        //  INNER JOIN [vwsAccountCode] B ON A.AccountCode = B.AccountCode  
        //  INNER JOIN [vwsEmployees] C ON A.EmployeeNo = C.EmployeeNo  
        //  INNER JOIN [vwsDepartmentList] D ON C.Department = D.DepartmentCode  
        //  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
        //  INNER JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    

        //  WHERE 
        //  A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'
        //  AND B.AccountType IN (7,8)
        //  AND B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')
        //  AND D.BCode LIKE '%" + _BCode + @"%'
        //  AND D.Area LIKE '%" + cboArea.Text + @"%' 
        //  AND F.SAPARCount = 1
        //  AND (A.Uploaded IS NULL OR A.Uploaded = 'N')
        //AND A.Amount <> 0

        //                                                    ";

        //        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        //        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);


        btnUpload.Enabled = true;
        MessageBox.Show("Payroll Loan Processing Complete");

    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnUpload.Enabled = false;
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
  INNER JOIN [vwsDepartmentList] D ON A.Department = D.DepartmentCode  
  INNER JOIN [vwsPayrollPeriod] E ON A.PayrollPeriod = E.PayrollPeriod  
  INNER JOIN [vwsLoanFile] F ON F.EmployeeNo = A.EmployeeNo  AND F.AccountCode = A.AccountCode AND F.LoanRefNo = A.LoanRefenceNo    
  INNER JOIN [vwsPayrollHeader] G ON A.EmployeeNo = G.EmployeeNo AND A.PayrollPeriod = G.PayrollPeriod AND G.PayrollType = 'PAYROLL'

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
