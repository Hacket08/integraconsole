using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class frmOvertimeApproval : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();


    private static string sEmployeeNo;
    private static string sEmployeeName;
    private static string sTransDate;
    private static string sExcessHr;
    private static string sExcessMin;
    private static string sApprovedHr;
    private static string sApprovedMin;
    public frmOvertimeApproval()
    {
        InitializeComponent();
    }

    private void frmOvertimeApproval_Load(object sender, EventArgs e)
    {
        clsDeclaration.sCompanyConnection = "";

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }

        dtpFrom.Value = DateTime.Today.AddDays(-1);
        dtpTo.Value = DateTime.Today.AddDays(-1);

    }



    private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                sEmployeeNo = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                sEmployeeName = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                sTransDate = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                sExcessHr = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                sExcessMin = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                sApprovedHr = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                sApprovedMin = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();


            }
        }
        catch
        {

        }

    }


    private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsDeclaration.sServer = _CompanyList.Rows[cboCompany.SelectedIndex][3].ToString();
        clsDeclaration.sCompany = _CompanyList.Rows[cboCompany.SelectedIndex][4].ToString();
        clsDeclaration.sUsername = _CompanyList.Rows[cboCompany.SelectedIndex][5].ToString();
        clsDeclaration.sPassword = _CompanyList.Rows[cboCompany.SelectedIndex][6].ToString();

        clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                       clsDeclaration.sServer, clsDeclaration.sCompany,
                                       clsDeclaration.sUsername, clsDeclaration.sPassword
                                    );

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT DISTINCT A.Area FROM usr_Branches A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }



        _SQLSyntax = "SELECT DISTINCT A.Company FROM usr_Branches A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        cboGroup.Items.Clear();
        cboGroup.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboGroup.Items.Add(row[0].ToString());
        }

        displayData();
    }

    private void dtpFrom_ValueChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void dtpTo_ValueChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void displayData()
    {
        if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == false)
        {
            return;
        }

        try
        {
            DataTable _DataTable;
            string _SQLSyntax;
            _SQLSyntax = @"
                        SELECT 
                        D.BranchCode, D.BranchName, A.EmployeeNo
                        ,CONCAT(B.LastName ,', ' ,B.FirstName,' ',B.SuffixName,' ',B.MiddleInitial) AS FullName
                        ,A.TransDate
                        , A.ExcessHrs, A.ExcessMins, A.ApprovedOT, A.ApprovedOTMins
                        FROM DailyTimeDetails A
                        LEFT JOIN Employees B ON A.EmployeeNo = B.EmployeeNo 
                        LEFT JOIN LeaveTable C ON A.LeaveCode = C.LeaveCode 
                        LEFT JOIN vwsDepartmentList D ON B.Department = D.DepartmentCode
                        WHERE 
                        A.TransDate BETWEEN '" + dtpFrom.Value + @"' AND '" + dtpTo.Value + @"'
                        AND (A.ExcessHrs <> 0 or A.ExcessMins <> 0)
                        AND D.Area LIKE '%" + cboArea.Text + @"%' AND D.Company LIKE '%" + cboGroup.Text + @"%'
                      ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
            _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable, "ApprovedOT");
        }
        catch { }

    }

    private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void approvedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string UpdateSyntax = @"UPDATE A SET
            A.ApprovedOT = A.ExcessHrs ,  
            A.ApprovedOTMins =  A.ExcessMins
            FROM DailyTimeDetails A
            WHERE A.EmployeeNo = '" + sEmployeeNo + @"' AND A.TransDate = '" + sTransDate + @"'";



        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, UpdateSyntax);
        displayData();
    }

    private void resetToolStripMenuItem_Click(object sender, EventArgs e)
    {

        string UpdateSyntax = @"UPDATE A SET
            A.ApprovedOT = 0 ,  
            A.ApprovedOTMins =  0
            FROM DailyTimeDetails A
            WHERE A.EmployeeNo = '" + sEmployeeNo + @"' AND A.TransDate = '" + sTransDate + @"'";



        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, UpdateSyntax);
        displayData();
    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {

        frmAddOT frmAddOT = new frmAddOT();

        frmAddOT._EmpCode = sEmployeeNo;
        frmAddOT._EmpName = sEmployeeName;
        frmAddOT._TransDate = sTransDate;
        frmAddOT._ExcessHr = sExcessHr;
        frmAddOT._ExcessMin = sExcessMin;
        frmAddOT._ApprovedHr = sApprovedHr;
        frmAddOT._ApprovedMin = sApprovedMin;
        frmAddOT._sConnection = clsDeclaration.sCompanyConnection;

        frmAddOT.ShowDialog();
        displayData();
    }
}