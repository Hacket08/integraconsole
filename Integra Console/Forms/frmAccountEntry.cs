using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmAccountEntry : Form
{
    public frmAccountEntry()
    {
        InitializeComponent();
    }

    private void frmAccountEntry_Load(object sender, EventArgs e)
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

    private void btnEmployee_Click(object sender, EventArgs e)
    {
        if( cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please Identify Payroll Period");
            return;
        }


        frmDataList frmDataList = new frmDataList();
        frmDataList._gPayrollPeriod = cboPayrolPeriod.Text;
        frmDataList._gListGroup = "PayrollEntry";
        frmDataList.ShowDialog();

        txtEmpCode.Text = frmDataList._gEmployeeNo;

        string _sqlList = "";

        _sqlList = @"SELECT CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName]
                            FROM vwsEmployees A WHERE A.EmployeeNo COLLATE Latin1_General_CI_AS = '" + txtEmpCode.Text + @"'";
        txtEmpName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "EmployeeName");

        string strCompany = frmDataList._gCompany;
        string _ConCompany = clsFunctions.GetCompanyConnectionString(strCompany);

        DataTable _tblList;

        _sqlList = @"SELECT A.AccountCode, B.AccountDesc, A.TotalDays, A.TotalHrs, A.Remarks FROM PayrollTrans01 A INNER JOIN AccountCode B ON A.AccountCode = B.AccountCode
                            WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'";
        _tblList = clsSQLClientFunctions.DataList(_ConCompany, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplayTimeSheet, _tblList);

        _sqlList = @"SELECT A.AccountCode, B.AccountDesc, A.Amount, A.Remarks FROM PayrollTrans02 A INNER JOIN AccountCode B ON A.AccountCode = B.AccountCode
                            WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.PayrollPeriod = '" + cboPayrolPeriod.Text + @"'";
        _tblList = clsSQLClientFunctions.DataList(_ConCompany, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplayOthrs, _tblList);

    }

    private void dgvDisplayTimeSheet_CellClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dgvDisplayOthrs_CellClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}