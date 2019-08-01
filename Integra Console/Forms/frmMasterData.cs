using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;

public partial class frmMasterData : Form
{
    public static string _Code;
    public static string _Name;
    public static string _EmployeeCompany;
    //private static string _SQLEmployeeList;
    //private static string _SQLDepartmentList;
    public frmMasterData()
    {
        InitializeComponent();

        //DataTable _CompanyList;
        //string _SQLSyntax;
        //_SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
        //_CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        //_SQLEmployeeList = @"";
        ////_SQLDepartmentList = @"";
        //int i = 0;
        //foreach (DataRow rowDB in _CompanyList.Rows)
        //{
        //    if (i > 0)
        //    {
        //        _SQLEmployeeList = _SQLEmployeeList + " UNION ALL ";
        //    }
            
        //    clsDeclaration.sServer = rowDB[3].ToString();
        //    clsDeclaration.sCompany = rowDB[4].ToString();
        //    clsDeclaration.sUsername = rowDB[5].ToString();
        //    clsDeclaration.sPassword = rowDB[6].ToString();
        //    clsDeclaration.sCompCode = rowDB[7].ToString();
            
        //    _SQLEmployeeList = _SQLEmployeeList + "SELECT *,'" + clsDeclaration.sCompCode + "' AS Company from [" + clsDeclaration.sServer + "].[" + clsDeclaration.sCompany + "].dbo.[Employees]";

        //    i = i + 1;
        //}
        
        //_SQLDepartmentList = "SELECT * from [" + ConfigurationManager.AppSettings["DBServer"] + "].[" + ConfigurationManager.AppSettings["DBName"] + "].dbo.[vwsDepartmentList]";
        
    }

    private void frmMasterData_Load(object sender, EventArgs e)
    {
        lblDisplayDuplicate.Visible = false;

        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT A.Area FROM [OBLP] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }

        
        _SQLSyntax = "SELECT Z.EmployeeNo FROM vwsEmployees Z GROUP BY Z.EmployeeNo HAVING COUNT(Z.EmployeeNo) > 1";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        if (_DataTable.Rows.Count != 0)
        {
            lblDisplayDuplicate.Visible = true;
        }
    }

    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT CONCAT(A.Company,A.Code,' - ',A.Name) AS Branch FROM [OBLP] A WHERE A.Area = '" + cboArea.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboBranch.Items.Clear();
        cboBranch.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboBranch.Items.Add(row[0].ToString());
        }
    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayData();
    }
    private void displayData()
    {
        try
        {
            DataTable _DataTable;
            string _SQLSyntax;
            string _Branch;

            if (cboBranch.Text == "")
            {
                _Branch = "";
            }
            else
            {
                _Branch = cboBranch.Text.Substring(0, 8);
            }
            

            _SQLSyntax = @"
                            SELECT A.EmployeeNo AS Code, CONCAT(A.LastName, ', ', A.FirstName, ' ' ,A.MiddleInitial) AS Name,B.DepartmentDesc, A.Company ,B.Area
                            FROM vwsEmployees A LEFT JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
                            WHERE ISNULL(B.BCode,'') LIKE '%" + _Branch + @"%' AND ISNULL(B.Area,'') LIKE '%" + cboArea.Text + @"%'
                          ";
            
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable, "MasterData");


            txtBranch.Text = "";
            txtCurrComp.Text = "";
            txtDepartment.Text = "";
            txtEmpCode.Text = "";
            txtEmpName.Text = "";
            txtMainComp.Text = "";
            txtPosition.Text = "";
            txtRemarks.Text = "";

            txtAssignBranch.Text = "";
            txtRegularDate.Text = "";
            txtEmpStatus.Text = "";

        }
        catch { }

    }
    private void displayDataDuplicateEmployee()
    {
        try
        {
            DataTable _DataTable;
            string _SQLSyntax;
            string _Branch;

            if (cboBranch.Text == "")
            {
                _Branch = "";
            }
            else
            {
                _Branch = cboBranch.Text.Substring(0, 8);
            }


            _SQLSyntax = @"
                            SELECT A.EmployeeNo AS Code, CONCAT(A.LastName, ', ', A.FirstName, ' ' ,A.MiddleInitial) AS Name,B.DepartmentDesc, A.Company ,B.Area
                            FROM vwsEmployees A LEFT JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
                            WHERE A.EmployeeNo IN (SELECT A.EmployeeNo FROM vwsEmployees  A GROUP BY A.EmployeeNo HAVING COUNT(A.EmployeeNo) > 1)
                          ";

            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable, "MasterData");


            txtBranch.Text = "";
            txtCurrComp.Text = "";
            txtDepartment.Text = "";
            txtEmpCode.Text = "";
            txtEmpName.Text = "";
            txtMainComp.Text = "";
            txtPosition.Text = "";
            txtRemarks.Text = "";

            txtAssignBranch.Text = "";
            txtRegularDate.Text = "";
            txtEmpStatus.Text = "";

        }
        catch { }

    }
    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {

            if (e.RowIndex < 0)
            {
                return;
            }

            txtEmpCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            txtEmpName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            txtDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();


            string _Company = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);






            txtMainComp.Text = "";
            txtCurrComp.Text = "";
            txtBranch.Text = "";
            txtPosition.Text = "";
            txtRemarks.Text = "";

            txtAssignBranch.Text = "";
            txtRegularDate.Text = "";
            txtEmpStatus.Text = "";

            string strComp = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
            _EmployeeCompany = strComp;


            DataTable _DataTable;
            string _SQLSyntax;
            _SQLSyntax = "SELECT A.AsBranchCode FROM Employees A WHERE A.EmployeeNo = '" + txtEmpCode.Text + "'";
            _DataTable = clsSQLClientFunctions.DataList(_ConCompany, _SQLSyntax);
            txtAssignBranch.Text = clsSQLClientFunctions.GetData(_DataTable, "AsBranchCode", "0");


    
            _SQLSyntax = "SELECT  A.CompanyName FROM OCMP A WHERE A.CompanyCode = '" + strComp + "' AND A.Active = '1'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            txtMainComp.Text = clsSQLClientFunctions.GetData(_DataTable, "CompanyName", "0");


            _SQLSyntax = "SELECT  A.CompanyName FROM OCMP A WHERE A.CompanyCode = '" + txtDepartment.Text.Substring(0, 4) + "' AND A.Active = '1'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            txtCurrComp.Text = clsSQLClientFunctions.GetData(_DataTable, "CompanyName", "0");


            _SQLSyntax = "SELECT  A.BranchName FROM ( " + clsDeclaration.sqlDepartmentList + @" ) A WHERE A.DepartmentDesc = '" + txtDepartment.Text + "'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            txtBranch.Text = clsSQLClientFunctions.GetData(_DataTable, "BranchName", "0");



            _SQLSyntax = "SELECT A.Name FROM OPST A WHERE A.Code = '" + txtDepartment.Text.Substring(8, 4) + "'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            txtPosition.Text = clsSQLClientFunctions.GetData(_DataTable, "Name", "0");



            _SQLSyntax = "SELECT A.Remarks, A.AsBranchCode, A.DateRegular, ISNULL(A.TerminationStatus, A.EmpStatus) AS EmpStatus FROM vwsEmployees  A WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            txtRemarks.Text = clsSQLClientFunctions.GetData(_DataTable, "Remarks", "0");
            txtAssignBranch.Text = clsSQLClientFunctions.GetData(_DataTable, "AsBranchCode", "0");
            if(clsSQLClientFunctions.GetData(_DataTable, "DateRegular", "0") != "")
            {
                txtRegularDate.Text = DateTime.Parse(clsSQLClientFunctions.GetData(_DataTable, "DateRegular", "0")).ToString("MM/dd/yyyy");
            }



            switch(int.Parse(clsSQLClientFunctions.GetData(_DataTable, "EmpStatus", "1")))
            {
                case 0:
                    txtEmpStatus.Text = "Regular";
                    break;
                case 1:
                    txtEmpStatus.Text = "Probitionary";
                    break;
                case 2:
                    txtEmpStatus.Text = "Contractual";
                    break;
                case 3:
                    txtEmpStatus.Text = "Finished Contract";
                    break;
                case 4:
                    txtEmpStatus.Text = "Resigned";
                    break;
                case 5:
                    txtEmpStatus.Text = "Temporary";
                    break;
                case 6:
                    txtEmpStatus.Text = "Terminated";
                    break;
                case 7:
                    txtEmpStatus.Text = "AWOL";
                    break;
                case 8:
                    txtEmpStatus.Text = "Retired";
                    break;

                case 9:
                    txtEmpStatus.Text = "Deceased";
                    break;
                case 10:
                    txtEmpStatus.Text = "Back - Out";
                    break;
            }
        }
        catch
        { }
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        String someText;
        someText = txtSearch.Text;

        int gridRow = 0;
        int gridColumn = 1;


        dataGridView1.ClearSelection();
        dataGridView1.CurrentCell = null;

        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dataGridView1.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dataGridView1.Rows[gridRow].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = gridRow;



                txtEmpCode.Text = dataGridView1.Rows[gridRow].Cells[0].Value.ToString().Trim();
                txtEmpName.Text = dataGridView1.Rows[gridRow].Cells[1].Value.ToString().Trim();
                txtDepartment.Text = dataGridView1.Rows[gridRow].Cells[2].Value.ToString().Trim();
                  
                string strComp = dataGridView1.Rows[gridRow].Cells[3].Value.ToString().Trim();

                DataTable _DataTable;
                string _SQLSyntax;
                _SQLSyntax = "SELECT  A.CompanyName FROM OCMP A WHERE A.CompanyCode = '" + strComp + "' AND A.Active = '1'";
                _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                txtMainComp.Text = clsSQLClientFunctions.GetData(_DataTable, "CompanyName", "0");

                txtCurrComp.Text = "";
                txtBranch.Text = "";
                txtPosition.Text = "";

                if (txtDepartment.Text != "")
                {
                    _SQLSyntax = "SELECT  A.CompanyName FROM OCMP A WHERE A.CompanyCode = '" + txtDepartment.Text.Substring(0, 4) + "' AND A.Active = '1'";
                    _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                    txtCurrComp.Text = clsSQLClientFunctions.GetData(_DataTable, "CompanyName", "0");
                    
                    _SQLSyntax = "SELECT  A.Name FROM OBLP A WHERE CONCAT(A.Company,A.Code) = '" + txtDepartment.Text.Substring(0, 8) + "'";
                    _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                    txtBranch.Text = clsSQLClientFunctions.GetData(_DataTable, "Name", "0");
                    
                    _SQLSyntax = "SELECT A.Name FROM OPST A WHERE A.Code = '" + txtDepartment.Text.Substring(8, 4) + "'";
                    _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                    txtPosition.Text = clsSQLClientFunctions.GetData(_DataTable, "Name", "0");
                }
                
                _SQLSyntax = "SELECT A.Remarks FROM ( " + clsDeclaration.sqlEmployeeList + @" ) A WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"'";
                _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                txtRemarks.Text = clsSQLClientFunctions.GetData(_DataTable, "Remarks", "0");

                return;
            }
            gridRow++;
        }
    }

    private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                _Code = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                _Name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                
                txtEmpCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                txtEmpName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                txtDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();

                string strComp = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();

                DataTable _DataTable;
                string _SQLSyntax;
                _SQLSyntax = "SELECT  A.CompanyName FROM OCMP A WHERE A.CompanyCode = '" + strComp + "' AND A.Active = '1'";
                _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                txtMainComp.Text = clsSQLClientFunctions.GetData(_DataTable, "CompanyName", "0");


                _SQLSyntax = "SELECT  A.CompanyName FROM OCMP A WHERE A.CompanyCode = '" + txtDepartment.Text.Substring(0, 4) + "' AND A.Active = '1'";
                _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                txtCurrComp.Text = clsSQLClientFunctions.GetData(_DataTable, "CompanyName", "0");


                _SQLSyntax = "SELECT  A.Name FROM OBLP A WHERE CONCAT(A.Company,A.Code) = '" + txtDepartment.Text.Substring(0, 8) + "'";
                _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                txtBranch.Text = clsSQLClientFunctions.GetData(_DataTable, "Name", "0");


                _SQLSyntax = "SELECT A.Name FROM OPST A WHERE A.Code = '" + txtDepartment.Text.Substring(8, 4) + "'";
                _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                txtPosition.Text = clsSQLClientFunctions.GetData(_DataTable, "Name", "0");
                

                _SQLSyntax = "SELECT A.Remarks FROM ( " + clsDeclaration.sqlEmployeeList + @" ) A WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"'";
                _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
                txtRemarks.Text = clsSQLClientFunctions.GetData(_DataTable, "Remarks", "0");
            }
        }
        catch
        {

        }
    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmEditMD frmEditMD = new frmEditMD();
        frmEditMD._Code = _Code;
        frmEditMD._Name = _Name;
        frmEditMD._Department = txtDepartment.Text;


        frmEditMD.ShowDialog();
        displayData();
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {

    }

    private void lblDisplayDuplicate_Click(object sender, EventArgs e)
    {
        cboBranch.Text = "";
        cboArea.Text = "";
        displayDataDuplicateEmployee();
    }

    private void lblDeleteRecord_Click(object sender, EventArgs e)
    {

        string _Syntax = "";
        DataTable _DataTable;
        _Syntax = @"SELECT * FROM OCMP Z WHERE Z.Active = '1' AND Z.CompCode = '" + _EmployeeCompany + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Syntax);


        clsDeclaration.sServer = clsSQLClientFunctions.GetData(_DataTable, "DBServer", "0");
        clsDeclaration.sCompany = clsSQLClientFunctions.GetData(_DataTable, "DBName", "0");
        clsDeclaration.sUsername = clsSQLClientFunctions.GetData(_DataTable, "DBUsername", "0");
        clsDeclaration.sPassword = clsSQLClientFunctions.GetData(_DataTable, "DBPassword", "0");

        string strConnection = clsSQLClientFunctions.GlobalConnectionString(
                                       clsDeclaration.sServer, clsDeclaration.sCompany,
                                       clsDeclaration.sUsername, clsDeclaration.sPassword
                                    );


        string _sqlDelete = @"
                                            DELETE FROM [DailyInOut] WHERE EMPLOYEENO = '" + txtEmpCode.Text + @"'
                                            DELETE FROM [DailyTimeDetails] WHERE EMPLOYEENO = '" + txtEmpCode.Text + @"'
                                            DELETE FROM [DailyTrans] WHERE EMPLOYEENO = '" + txtEmpCode.Text + @"'
                                            DELETE FROM [PayrollHeader] WHERE EMPLOYEENO  = '" + txtEmpCode.Text + @"'
                                            DELETE FROM [PayrollDetails] WHERE EMPLOYEENO  = '" + txtEmpCode.Text + @"'
                                            DELETE FROM [PayrollTrans01] WHERE EMPLOYEENO  = '" + txtEmpCode.Text + @"'
                                            DELETE FROM [PayrollTrans02] WHERE EMPLOYEENO  = '" + txtEmpCode.Text + @"'
                                            DELETE FROM [PayrollEntry] WHERE EMPLOYEENO  = '" + txtEmpCode.Text + @"'
                                           ";

        clsSQLClientFunctions.GlobalExecuteCommand(strConnection, _sqlDelete);
        MessageBox.Show("Data Deleted for Employee " + txtEmpName.Text);
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void label10_Click(object sender, EventArgs e)
    {

    }
}
