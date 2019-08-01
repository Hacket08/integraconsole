using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmEmployeeNoParameter : Form
{
    public static int _RequestID;

    public frmEmployeeNoParameter()
    {
        InitializeComponent();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        frmReport frmReport = new frmReport();
        if (txtEmpCode.Text == "")
        {
            MessageBox.Show("Data Not Define!");
            return;
        }

        switch (_RequestID)
        {
            case 6:
                clsDeclaration.sReportTag = 6;
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Loan Summary Report Per Employee.rpt";

                clsDeclaration.sAccountCode = txtEmpCode.Text;
                frmReport.MdiParent = ((frmMainWindow)(this.MdiParent));
                frmReport.Show();
                break;
            case 4:
                clsDeclaration.sReportTag = 4;
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Loan Summary Report Per Employee.rpt";

                clsDeclaration.sEmployeeNo = txtEmpCode.Text;
                frmReport.MdiParent = ((frmMainWindow)(this.MdiParent));
                frmReport.Show();

                break;
            default:

                clsDeclaration.sEmployeeNo = txtEmpCode.Text;
                clsDeclaration.bView = true;
                Close();

                break;
        }





    }

    private void frmEmployeeNoParameter_Load(object sender, EventArgs e)
    {

        switch (_RequestID)
        {
            case 6:
                lblLabel.Text = "Account Code";
                break;
            case 4:
            default:
                lblLabel.Text = "Employee No.";
                break;
        }

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lblBrowseEmployeeNo_Click(object sender, EventArgs e)
    {

        frmDataList frmDataList = new frmDataList();
        switch (_RequestID)
        {
            case 6:
                frmDataList._gListGroup = "AccountList";
                frmDataList.ShowDialog();

                txtEmpCode.Text = frmDataList._gAccountCode;

                break;
            case 4:
            default:
                frmDataList._gListGroup = "EmployeeLoanList";
                frmDataList.ShowDialog();

                txtEmpCode.Text = frmDataList._gEmployeeNo;

                break;
        }





    }

    private void lblLabel_Click(object sender, EventArgs e)
    {

    }
}
