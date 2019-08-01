using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmLeaveConvertion : Form
{
    public static frmGlobalDataList frmGlobalDataList = new frmGlobalDataList();
    public frmLeaveConvertion()
    {
        InitializeComponent();
    }

    private void frmLeaveConvertion_Load(object sender, EventArgs e)
    {
        cboYear.Items.Clear();
        int _year = DateTime.Now.Year - 5;
        while (_year <= (DateTime.Now.Year + 10))
        {
            cboYear.Items.Add(_year);
            _year++;
        }
    }

    private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtFrom = DateTime.Parse(clsDeclaration.sysLeaveCutoffMonth + "/" + clsDeclaration.sysLeaveCutoffDay + "/" + cboYear.Text);
        txtDateTo.Text = dtFrom.ToString("MM/dd/yyyy");
        txtDateFrom.Text = dtFrom.AddDays(1).AddYears(-1).ToString("MM/dd/yyyy");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lblSearchEmployeeName_Click(object sender, EventArgs e)
    {

        if (txtEmpCode.Text == "")
        {
            clsDeclaration.sDataDisplayQuery = @"
                                                                            SELECT XX.EmployeeNo, XX.EmployeeName, XX.BranchCode, XX.BranchName , XX.Company  FROM (
                                                                                 " + clsSystemQuery._qryLeaveGeneratedList(cboYear.Text) + @"       
                                                                            ) XX        
                                                                          ";
        }
        else
        {
            clsDeclaration.sDataDisplayQuery = @"
                                                                            SELECT XX.EmployeeNo, XX.EmployeeName, XX.BranchCode, XX.BranchName , XX.Company  FROM (
                                                                                 " + clsSystemQuery._qryLeaveGeneratedList(cboYear.Text) + @"       
                                                                            ) XX        
                                                                               WHERE XX.EmployeeNo = '" + txtEmpCode.Text + @"'
                                                                          ";
        }

        string lblTag = ((Label)sender).Tag.ToString();
        frmGlobalDataList._gListGroup = lblTag;
        frmGlobalDataList.ShowDialog();
        txtEmpCode.Text = frmGlobalDataList._gEmployeeNo;
    }

    private void pvDisplayData()
    {
        try
        {
            string _sqlList;
            _sqlList = @"
                                                                            SELECT XX.* FROM (
                                                                                 " + clsSystemQuery._qryLeaveGeneratedList(cboYear.Text)  + @"       
                                                                            ) XX        
                                                                               WHERE XX.EmployeeNo = '" + txtEmpCode.Text + @"'
                                                                          ";

            DataTable _tblDispaly = new DataTable();
            _tblDispaly = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);

            txtEmpName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "EmployeeName", "0");


            _sqlList = @"SELECT A.AccountCode as [Code], B.AccountDesc AS [Description], A.Amount, A.Freq AS [Frequency], A.Status
                                        FROM [vwsEmployeesRecurring] A INNER JOIN [vwsAccountCode] B ON A.[AccountCode] = B.[AccountCode]
                                        WHERE A.[EmployeeNo]  = '" + txtEmpCode.Text + @"'";
            _tblDispaly = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
            clsFunctions.DataGridViewSetup(dgvDisplayPromotion, _tblDispaly);

        }
        catch
        {

        }
    }

    private void label2_Click(object sender, EventArgs e)
    {

        clsDeclaration.sDataDisplayQuery = @"
                                                                            SELECT XX.EmployeeNo, XX.EmployeeName, XX.BranchCode, XX.BranchName , XX.Company  FROM (
                                                                                 " + clsSystemQuery._qryEmployeeList() + @"       
                                                                            ) XX        
                                                                               WHERE XX.EmployeeName LIKE '%" + txtEmpName.Text + @"%'
                                                                          ";

        string lblTag = ((Label)sender).Tag.ToString();
        frmGlobalDataList._gListGroup = lblTag;
        frmGlobalDataList.ShowDialog();
        txtEmpCode.Text = frmGlobalDataList._gEmployeeNo;
    }
}