using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmPayrollPeriodParameter : Form
{
    public static int _RequestID;
    public frmPayrollPeriodParameter()
    {
        InitializeComponent();
    }

    private void frmPayrollPeriodParameter_Load(object sender, EventArgs e)
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

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Payroll Period Not Define!");
            return;
        }

        switch (_RequestID)
        {
            case 5:
                clsDeclaration.sReportTag = 5;
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Loan Summary Per Resigned Employee By Area.rpt";

                clsDeclaration.sDateFrom = DateTime.Parse(txtDateFrom.Text);
                clsDeclaration.sDateTo = DateTime.Parse(txtDateTo.Text);
                frmReport frmReport = new frmReport();
                frmReport.MdiParent = ((frmMainWindow)(this.MdiParent));
                frmReport.Show();

                break;
            default:

                clsDeclaration.sPayrollPeriod = cboPayrolPeriod.Text;
                clsDeclaration.bView = true;
                Close();

                break;
        }



    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }

    private void cboPayrolPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A WHERE A.[PayrollPeriod] = '" + cboPayrolPeriod.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        txtDateFrom.Text =DateTime.Parse(clsSQLClientFunctions.GetData(_DataTable, "DateOne", "1")).ToString("MM/dd/yyyy");
        txtDateTo.Text = DateTime.Parse(clsSQLClientFunctions.GetData(_DataTable, "DateTwo", "1")).ToString("MM/dd/yyyy");
    }
}
