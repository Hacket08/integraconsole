using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmEmployeeParameter : Form
{
    public frmEmployeeParameter()
    {
        InitializeComponent();
    }

    private void frmEmployeeParameter_Load(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());

        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (txtEmployeeNo.Text == "")
        {
            MessageBox.Show("Employee Number Not Define!");
            return;
        }

        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Payroll Period Not Define!");
            return;
        }

        clsDeclaration.sEmployeeNo = txtEmployeeNo.Text;
        clsDeclaration.sPayrollPeriod = cboPayrolPeriod.Text;

        clsDeclaration.bView = true;
        Close();
    }
}