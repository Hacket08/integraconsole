using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmPayrollRegParameter : Form
{
    public static string _RequestType;
    public static string _RequestDisplay;
    public static string _AdjustmentType;




    public frmPayrollRegParameter()
    {
        InitializeComponent();
    }

    private void frmPayrollRegParameter_Load(object sender, EventArgs e)
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


        cboReportType.Visible = true;
        cboReportType.Items.Clear();
        switch (_RequestType)
        {
            case "Register":
                cboReportType.Items.Add("Payroll Register");
                cboReportType.Items.Add("Last Pay Register");
                cboReportType.SelectedIndex = 0;
                break;
            case "Adjustment":
                cboReportType.Items.Add("Summary of Deduction");
                cboReportType.Items.Add("Summary of Other Income");
                cboReportType.SelectedIndex = 0;
                break;
            case "Bonus":
            case "Payslip":
                cboReportType.Visible = false;
                break;
        }



        chkRankAndFile.Checked = true;
        chkSupervisory.Checked = true;
        chkManagerial.Checked = true;

        rbStandard.Checked = true;
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


        clsDeclaration.sReportTitle = cboReportType.Text;
        clsDeclaration.sEmployeeFrom = txtEmployeeFrom.Text;
        clsDeclaration.sEmployeeTo = txtEmployeeTo.Text;


        switch (_RequestType)
        {
            case "Register":
                if (rbStandard.Checked == true)
                {
                    clsDeclaration.sReportFormat = "S";
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payroll Register Global.rpt";
                }

                if (rbPerGroup.Checked == true)
                {
                    clsDeclaration.sReportFormat = "G";
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payroll Register Global Group.rpt";
                }

                if (rbBranch.Checked == true)
                {
                    clsDeclaration.sReportFormat = "S";
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payroll Register Global With Branch.rpt";
                }

                break;
            case "Adjustment":
                switch (cboReportType.SelectedIndex)
                {
                    case 0:
                        _AdjustmentType = "Deduction";
                        break;

                    case 1:
                        _AdjustmentType = "Income";
                        break;
                }




                if (rbStandard.Checked == true)
                {
                    clsDeclaration.sReportFormat = "S";
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Summary of Deduction Global.rpt";
                }

                if (rbPerGroup.Checked == true)
                {
                    clsDeclaration.sReportFormat = "G";
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Summary of Deduction Global Group.rpt";
                }
                break;
            case "Bonus":
            case "Payslip":
                clsDeclaration.sReportTitle = "PAY SLIP";
                clsDeclaration.sReportFormat = "S";
                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payslip Global.rpt";
                break;
        }











        if (chkLastPay.Checked == false)
        {
            clsDeclaration.sReportDispaly = "Regular";
        }
        else
        {
            clsDeclaration.sReportDispaly = "LastPay";
        }

        clsDeclaration.bView = true;

        frmReportDisplay frmReportDisplay = new frmReportDisplay();
        clsDeclaration.sReportTag = 20;
        frmReportDisplay._RequestType = _RequestType;
        frmReportDisplay._AdjustmentType = _AdjustmentType;
        frmReportDisplay.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmReportDisplay.Show();



        //Close();
    }

    private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch(cboReportType.Text)
        {
            case "Last Pay Register":
                chkLastPay.Checked = true;
                chkLastPay.Enabled = false;
                break;
            case "Payroll Register":
                chkLastPay.Checked = false;
                chkLastPay.Enabled = false;
                break;
            default:
                chkLastPay.Checked = false;
                chkLastPay.Enabled = true;
                break;
        }

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
