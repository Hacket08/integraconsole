using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmDateRangeParameter : Form
{
    frmReport frmReport = new frmReport();

    public frmDateRangeParameter()
    {
        InitializeComponent();
    }

    private void frmDateRangeParameter_Load(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        clsDeclaration.sDateFrom = DateTime.Parse(dtpFrom.Text);
        clsDeclaration.sDateTo = DateTime.Parse(dtpTo.Text);

        switch (clsDeclaration.sReportTag)
        {
            case 3:
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Loan Summary Remittance Eye Glasses.rpt";
                frmReport.MdiParent = ((frmMainWindow)(this.MdiParent));
                frmReport.Show();
                break;
        }

        clsDeclaration.bView = true;
        Close();
    }
}
