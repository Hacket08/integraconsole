using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmUnderTime : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();

    public frmUnderTime()
    {
        InitializeComponent();
    }

    private void frmUnderTime_Load(object sender, EventArgs e)
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

    private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
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
                            ,A.TransDate, A.TimeOUT, A.TimeIN
                            ,A.TotalHrs AS TotalMins, A.IsDeduct
                            FROM DailyInOut A
                            LEFT JOIN Employees B ON A.EmployeeNo = B.EmployeeNo 
                            LEFT JOIN vwsDepartmentList D ON B.Department = D.DepartmentCode
                            WHERE A.TotalHrs <> 0
                            AND A.TransDate BETWEEN '" + dtpFrom.Value + @"' AND '" + dtpTo.Value + @"'
                            AND ISNULL(A.TimeIN,'') <> '' AND ISNULL(A.TimeOUT,'') <> ''
                            AND D.Area LIKE '%" + cboArea.Text + @"%' AND D.Company LIKE '%" + cboGroup.Text + @"%'
                      ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
            dataGridView1.Columns.Clear();
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable, "UnderTime");
        }
        catch { }

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {

        foreach (DataGridViewRow Row in dataGridView1.Rows)
        {
            DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)Row.Cells[0];
            bool isChecked = (bool)checkbox.EditedFormattedValue;

            if (isChecked)
            {
                string _Code = Row.Cells[3].Value.ToString();
                string _Date = Row.Cells[5].Value.ToString();
                string _Min = Row.Cells[8].Value.ToString();
                string _Deduct = Row.Cells[9].Value.ToString();
                
                if (_Deduct == "0")
                {
                    string _Syntax;
                    _Syntax = @"
    
                                UPDATE A SET A.OtherDeduction = (A.OtherDeduction + " + _Min + @")
                                FROM DailyTimeDetails A
                                WHERE A.EmployeeNo = '" + _Code + "' AND A.TransDate = '" + _Date + @"'

                                UPDATE A SET A.IsDeduct = '1'
                                FROM DailyInOut A 
                                WHERE A.EmployeeNo = '" + _Code  + "' AND A.TransDate = '" + _Date + "'";

                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _Syntax);

                }
            }

        }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {

        foreach (DataGridViewRow Row in dataGridView1.Rows)
        {
            DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)Row.Cells[0];
            bool isChecked = (bool)checkbox.EditedFormattedValue;

            if (isChecked)
            {
                string _Code = Row.Cells[3].Value.ToString();
                string _Date = Row.Cells[5].Value.ToString();
                string _Min = Row.Cells[8].Value.ToString();
                string _Deduct = Row.Cells[9].Value.ToString();

                if (_Deduct == "1")
                {
                    string _Syntax;
                    _Syntax = @"
    
                                UPDATE A SET A.OtherDeduction = (A.OtherDeduction - " + _Min + @")
                                FROM DailyTimeDetails A
                                WHERE A.EmployeeNo = '" + _Code + "' AND A.TransDate = '" + _Date + @"'

                                UPDATE A SET A.IsDeduct = '0'
                                FROM DailyInOut A 
                                WHERE A.EmployeeNo = '" + _Code + "' AND A.TransDate = '" + _Date + "'";

                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _Syntax);

                }
            }

        }
    }
}