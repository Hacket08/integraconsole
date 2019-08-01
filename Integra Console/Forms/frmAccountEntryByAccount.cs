using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmAccountEntryByAccount : Form
{
    public static string _gAddType;
    public static string _gEmployeeNo;
    public static string _gEmployeeName;
    public static string _gAmount;
    public static string _gRemarks;

    public static string _gPayrollPeriod;
    public static string _gAccountCode;


    public frmAccountEntryByAccount()
    {
        InitializeComponent();
    }

    private void frmAccountEntryByAccount_Load(object sender, EventArgs e)
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
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please Identify Payroll Period");
            return;
        }

        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "AccountList1";
        frmDataList.ShowDialog();

        txtLoanAccountCode.Text = frmDataList._gAccountCode;

        string _sqlList = "";
        _sqlList = @"SELECT A.AccountDesc
                            FROM vwsAccountCode A WHERE A.AccountCode  = '" + txtLoanAccountCode.Text + @"'";
        txtDescription.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "AccountDesc");


        DataTable _tblList;
        _sqlList = @"
SELECT A.EmployeeNo, 
C.[EmployeeName],
 A.Amount, A.Remarks 
FROM vwsPayrollTrans02 A 
INNER JOIN vwsAccountCode B ON A.AccountCode = B.AccountCode
INNER JOIN vwsEmployees C ON A.EmployeeNo = C.EmployeeNo
                            WHERE A.AccountCode = '" + txtLoanAccountCode.Text + @"' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'";

        _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);

    }

    private void btnRowAdd_Click(object sender, EventArgs e)
    {
        frmAddAccountEntryByEmployee frmAddAccountEntryByEmployee = new frmAddAccountEntryByEmployee();

        frmAddAccountEntryByEmployee._gAddType = "ADD";

        frmAddAccountEntryByEmployee._gEmployeeNo = "";
        frmAddAccountEntryByEmployee._gEmployeeName = "";
        frmAddAccountEntryByEmployee._gAmount = "0.00";
        frmAddAccountEntryByEmployee._gRemarks = "";

        frmAddAccountEntryByEmployee._gPayrollPeriod = cboPayrolPeriod.Text;
        frmAddAccountEntryByEmployee._gAccountCode = txtLoanAccountCode.Text;

        frmAddAccountEntryByEmployee.ShowDialog();

        DataTable _tblList;
        string _sqlList = @"
SELECT A.EmployeeNo, 
C.[EmployeeName],
 A.Amount, A.Remarks 
FROM vwsPayrollTrans02 A 
INNER JOIN vwsAccountCode B ON A.AccountCode = B.AccountCode
INNER JOIN vwsEmployees C ON A.EmployeeNo = C.EmployeeNo
                            WHERE A.AccountCode = '" + txtLoanAccountCode.Text + @"' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'";

        _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
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
            _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            _gEmployeeName = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            _gAmount = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            _gRemarks = dgvDisplay.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();

            _gAccountCode = txtLoanAccountCode.Text;
            _gPayrollPeriod = cboPayrolPeriod.Text;

        }
        catch
        {

        }
    }

    private void btnRowEdit_Click(object sender, EventArgs e)
    {
        frmAddAccountEntryByEmployee frmAddAccountEntryByEmployee = new frmAddAccountEntryByEmployee();

        frmAddAccountEntryByEmployee._gAddType = "EDIT";

        frmAddAccountEntryByEmployee._gEmployeeNo = _gEmployeeNo;
        frmAddAccountEntryByEmployee._gEmployeeName = _gEmployeeName;
        frmAddAccountEntryByEmployee._gAmount = _gAmount;
        frmAddAccountEntryByEmployee._gRemarks = _gRemarks;

        frmAddAccountEntryByEmployee._gPayrollPeriod = cboPayrolPeriod.Text;
        frmAddAccountEntryByEmployee._gAccountCode = txtLoanAccountCode.Text;

        frmAddAccountEntryByEmployee.ShowDialog();


        DataTable _tblList;
        string _sqlList = @"
SELECT A.EmployeeNo, 
C.[EmployeeName],
 A.Amount, A.Remarks 
FROM vwsPayrollTrans02 A 
INNER JOIN vwsAccountCode B ON A.AccountCode = B.AccountCode
INNER JOIN vwsEmployees C ON A.EmployeeNo = C.EmployeeNo
                            WHERE A.AccountCode = '" + txtLoanAccountCode.Text + @"' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'";

        _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
    }

    private void btnRowDelete_Click(object sender, EventArgs e)
    {
        string _sqlList = "";
        _sqlList = @"SELECT A.[Company]
                            FROM vwsEmployees A WHERE A.EmployeeNo = '" + _gEmployeeNo + @"'";
        string _gCompany = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Company");
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_gCompany);

        DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        if (res == DialogResult.Cancel)
        {
            return;
        }

        string _SQLRebate = @"
                                                    DELETE FROM [dbo].[PayrollTrans02]
                                                     WHERE [PayrollPeriod] = '" + _gPayrollPeriod + @"'
                                                          AND [EmployeeNo] = '" + _gEmployeeNo + @"'
                                                          AND [AccountCode] = '" + _gAccountCode + @"'
                                                ";
        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLRebate);

        MessageBox.Show("Selected Account Successfully Deleted!");



        DataTable _tblList;
        _sqlList = @"
SELECT A.EmployeeNo, 
C.[EmployeeName],
 A.Amount, A.Remarks 
FROM vwsPayrollTrans02 A 
INNER JOIN vwsAccountCode B ON A.AccountCode = B.AccountCode
INNER JOIN vwsEmployees C ON A.EmployeeNo = C.EmployeeNo
                            WHERE A.AccountCode = '" + txtLoanAccountCode.Text + @"' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'";

        _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
    }
}