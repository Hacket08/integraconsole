using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmGrossEmployeeParameter : Form
{
    public static string _RequestType;
    public static string _RequestDisplay;
    public static string _AdjustmentType;




    public frmGrossEmployeeParameter()
    {
        InitializeComponent();
    }

    private void frmGrossEmployeeParameter_Load(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        pnlEmployee.Enabled = false;
        chkFilterPerEmployee.Checked = false;
        clsDeclaration.sFilterByEmployee = false;

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
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



        chkRankAndFile.Checked = true;
        chkSupervisory.Checked = true;
        chkManagerial.Checked = true;

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }

    private void cboPayrolPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    private void btnUpload_Click(object sender, EventArgs e)
    {
        clsDeclaration.sArea = cboArea.Text;
        if (cboBranch.Text == "")
        {
            clsDeclaration.sBranch = "";
        }
        else
        {
            clsDeclaration.sBranch = cboBranch.Text.Substring(0, 8);
        }

        clsDeclaration.sPayrollPeriod = cboPayrolPeriod.Text;


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


        clsDeclaration.sEmployeeFrom = txtEmployeeFrom.Text;
        clsDeclaration.sEmployeeTo = txtEmployeeTo.Text;


        //switch (_RequestType)
        //{
         
        //    case "9":

        //        break;
        //}



        clsDeclaration.bView = true;

        frmReportDisplay frmReportDisplay = new frmReportDisplay();
        clsDeclaration.sReportTag = 20;
        frmReportDisplay._RequestType = _RequestType;
        //frmReportDisplay._AdjustmentType = _AdjustmentType;
        
        frmReportDisplay.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmReportDisplay.Show();



        //Close();
    }

    private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    private void chkFilterPerEmployee_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFilterPerEmployee.Checked ==  true)
        {
            pnlEmployee.Enabled = true;
            clsDeclaration.sFilterByEmployee = true;
        }
        else
        {
            pnlEmployee.Enabled = false;
            clsDeclaration.sFilterByEmployee = false;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {

    }
}
