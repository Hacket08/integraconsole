using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmAccountByRangeParameter : Form
{

    public static string _RequestType;
    public frmAccountByRangeParameter()
    {
        InitializeComponent();
    }

    private void frmAccountByRangeParameter_Load(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        switch(_RequestType)
        {
            case "Account":
                lblType.Text = "Account Code";
                break;
            case "Employee":
                lblType.Text = "Employee No.";
                break;
        }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        clsDeclaration.sDateFrom = DateTime.Parse(dtpFrom.Text);
        clsDeclaration.sDateTo = DateTime.Parse(dtpTo.Text);
        clsDeclaration.sCode = txtCode.Text;
        clsDeclaration.bView = true;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }
}

