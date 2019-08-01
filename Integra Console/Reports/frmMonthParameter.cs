using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

public partial class frmMonthParameter : Form
{
    public frmMonthParameter()
    {
        InitializeComponent();
    }

    private void frmMonthParameter_Load(object sender, EventArgs e)
    {
        for (int i = ((DateTime.Now).Year - 10) ; i < ((DateTime.Now).Year + 20); i++)
        {
            cboYear.Items.Add(i);
        }
        cboYear.Text = (DateTime.Now).Year.ToString();


        for (int i = 1; i <= 12; i++)
        {
            cboMonth.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
        }
        cboMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Now).Month);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        clsDeclaration.sPayrollPeriod = cboYear.Text + "-" + (cboMonth.SelectedIndex + 1).ToString("00");

        clsDeclaration.bView = true;
        Close();
    }
}