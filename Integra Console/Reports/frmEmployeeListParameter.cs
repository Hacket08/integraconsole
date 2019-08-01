using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmEmployeeListParameter : Form
{
    public frmEmployeeListParameter()
    {
        InitializeComponent();
    }

    private void frmEmployeeListParameter_Load(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;

        cmbStatus.Items.Add("Regular");
        cmbStatus.Items.Add("Probitionary");
        cmbStatus.Items.Add("Contractual");
        cmbStatus.Items.Add("Finished Contract");
        cmbStatus.Items.Add("Resigned");
        cmbStatus.Items.Add("Temporary");
        cmbStatus.Items.Add("Terminated");
        cmbStatus.Items.Add("AWOL");
        cmbStatus.Items.Add("Retired");
        cmbStatus.Items.Add("Back - Out");

        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }

        chkRankAndFile.Checked = true;
        chkSupervisory.Checked = true;
        chkManagerial.Checked = true;

        rbStandard.Checked = true;
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        string _Branch = "";
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }


        clsDeclaration.sConfiLevelSelection = "0,1,2";
        if (chkRankAndFile.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0";
        }
        if (chkSupervisory.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "1";
        }
        if (chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "2";
        }
        if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0,1";
        }
        if (chkRankAndFile.Checked == true && chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0,2";
        }
        if (chkSupervisory.Checked == true && chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "1,2";
        }
        if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true && chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0,1,2";
        }

        if (rbStandard.Checked == true)
        {
            clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\List of Employees.rpt";
        }
        if (rbPerGroup.Checked == true)
        {
            clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\List of Employees Per Group.rpt";
        }

        clsDeclaration.sBranch = _Branch;
        clsDeclaration.sEmpStatus = cmbStatus.SelectedIndex.ToString();
        clsDeclaration.bView = true;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
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
    }
}