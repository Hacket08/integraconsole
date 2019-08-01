using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class frmBranches : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();


    public static string _Code;
    public static string _Name;
    public static string _Area;
    public static string _Company;
    public static string _Schedule;

    public frmBranches()
    {
        InitializeComponent();
    }

    private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsDeclaration.sServer = _CompanyList.Rows[cboCompany.SelectedIndex][3].ToString();
        clsDeclaration.sCompany = _CompanyList.Rows[cboCompany.SelectedIndex][4].ToString();
        clsDeclaration.sUsername = _CompanyList.Rows[cboCompany.SelectedIndex][5].ToString();
        clsDeclaration.sPassword = _CompanyList.Rows[cboCompany.SelectedIndex][6].ToString();
        clsDeclaration.sCompCode = _CompanyList.Rows[cboCompany.SelectedIndex][7].ToString();

        clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                       clsDeclaration.sServer, clsDeclaration.sCompany,
                                       clsDeclaration.sUsername, clsDeclaration.sPassword
                                    );


      
        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT DISTINCT A.Area FROM OBLP A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }



        _SQLSyntax = "SELECT DISTINCT A.Company FROM OBLP A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        cboGroup.Items.Clear();
        cboGroup.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboGroup.Items.Add(row[0].ToString());
        }

        displayData();
    }

    private void frmBranches_Load(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }

        
        _SQLSyntax = "SELECT DISTINCT A.Area FROM OBLP A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }



        _SQLSyntax = "SELECT DISTINCT A.Company FROM OBLP A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboGroup.Items.Clear();
        cboGroup.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboGroup.Items.Add(row[0].ToString());
        }
        displayData();
    }

    private void displayData()
    {
        try
        {
            DataTable _DataTable;
            string _SQLSyntax;
            _SQLSyntax = @"
                        SELECT * FROM OBLP A WHERE A.Area LIKE '%" + cboArea.Text + @"%' AND A.Company LIKE '%" + cboGroup.Text + @"%'
                      ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable, "Branches");
        }
        catch { }

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
                _Area = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                _Company = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                _Schedule = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                //clsDeclaration._CardName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                //clsDeclaration._TaxIdentificationNumber = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                //clsDeclaration._Name = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                //clsDeclaration._Address = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }
        catch
        {

        }

    }

    private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
    {

        frmAddData frmAddData = new frmAddData();

        frmAddData._Code = "";
        frmAddData._Name = "";
        frmAddData._Area = "";
        frmAddData._Company = "";
        frmAddData._Schedule = "";
        frmAddData._sConnection = clsDeclaration.sCompanyConnection;

        frmAddData.ShowDialog();
        displayData();
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmAddData frmAddData = new frmAddData();
        frmAddData._Code = _Code;
        frmAddData._Name = _Name;
        frmAddData._Area = _Area;
        frmAddData._Company = _Company;
        frmAddData._Schedule = _Schedule;
        frmAddData._sConnection = clsDeclaration.sCompanyConnection;

        frmAddData.ShowDialog();
        displayData();
    }

    private void cbo_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        DialogResult result;
        result = MessageBox.Show("Deleting This Data is Irreversable! Are yous Sure You Want To Continue?", "Delete Data", MessageBoxButtons.OKCancel);

        if (result == System.Windows.Forms.DialogResult.OK)
        {

            DataTable _DataTable;
           
            string _SQLSyntax;
            _SQLSyntax = "DELETE FROM OBLP WHERE CODE = '" + _Code + "' AND COMPANY = '" + _Company + "'";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);
            

            _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            foreach (DataRow row in _DataTable.Rows)
            {
                clsDeclaration.sServer = row[3].ToString();
                clsDeclaration.sCompany = row[4].ToString();
                clsDeclaration.sUsername = row[5].ToString();
                clsDeclaration.sPassword = row[6].ToString();

                clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                               clsDeclaration.sServer, clsDeclaration.sCompany,
                                               clsDeclaration.sUsername, clsDeclaration.sPassword
                                            );

                if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == true)
                {
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, "DELETE FROM usr_Branches WHERE CODE = '" + _Code + "' AND COMPANY = '" + _Company + "'");

                }
            }

            MessageBox.Show("Data Successfully Deleted");

        }

    }

    private void btnExportEmployee_Click(object sender, EventArgs e)
    {
        string _CompSyntax;
        DataTable _CompDataTable;
        _CompSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = 1";
        _CompDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _CompSyntax);
        foreach (DataRow row in _CompDataTable.Rows)
        {
            string _Code = row[7].ToString();
            clsDeclaration.sServer = row[3].ToString();
            clsDeclaration.sCompany = row[4].ToString();
            clsDeclaration.sUsername = row[5].ToString();
            clsDeclaration.sPassword = row[6].ToString();

            clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                           clsDeclaration.sServer, clsDeclaration.sCompany,
                                           clsDeclaration.sUsername, clsDeclaration.sPassword
                                        );

            if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == true)
            {
                
                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, "DELETE FROM Employees WHERE CompCode = '" + clsDeclaration.sCompCode + "'");

                string _InsertSyntax;
                DataTable _DataTable;
                string _SQLSyntax;
                _SQLSyntax = @"
                            SELECT
                            A.EmployeeNo, 
                            CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName],
                            B.DepartmentDesc
                            FROM 
                            Employees A INNER JOIN Department B ON A.Department = B.DepartmentCode

                      ";
                _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);


                foreach (DataRow row1 in _DataTable.Rows)
                {
                    string _EmployeeNo = row1[0].ToString();
                    string _EmployeeName = row1[1].ToString();
                    string _Department = row1[2].ToString();
                    string _CampCode = clsDeclaration.sCompCode;



                    _InsertSyntax = @"
                            INSERT INTO [dbo].[Employees]
                                       ([EmployeeNo]
                                       ,[FullName]
                                       ,[Department]
                                       ,[CompCode])
                                    VALUES
                                    (
	                                     '" + _EmployeeNo + @"'
	                                    ,'" + _EmployeeName.Replace("'", "''") + @"'
	                                    ,'" + _Department + @"'
	                                    ,'" + _CampCode + @"'
                                    )
                                 ";

                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _InsertSyntax);
                }

            }
        }
















        MessageBox.Show("Employee Successfully Exported");
    }

    private void button1_Click(object sender, EventArgs e)
    {



        string _CompSyntax;
        DataTable _CompDataTable;
        _CompSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = 1";
        _CompDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _CompSyntax);
        foreach (DataRow row in _CompDataTable.Rows)
        {
            //string _Code = row[7].ToString();
            clsDeclaration.sServer = row[3].ToString();
            clsDeclaration.sCompany = row[4].ToString();
            clsDeclaration.sUsername = row[5].ToString();
            clsDeclaration.sPassword = row[6].ToString();

            clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                           clsDeclaration.sServer, clsDeclaration.sCompany,
                                           clsDeclaration.sUsername, clsDeclaration.sPassword
                                        );

            if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == true)
            {

                DataTable _DataTable;
                string _SQLSyntax;
                
                _SQLSyntax = @"SELECT [Code],[Name],[Area],[Company],[SchedCode]  FROM OBLP A";
                _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

                foreach (DataRow row1 in _DataTable.Rows)
                {
                    
                        string _InsertData = @"
                                                    IF EXISTS (SELECT 'TRUE' FROM usr_Branches Z WHERE Z.Code = '" + row1[0].ToString() + @"' AND Z.COMPANY = '" + row1[3].ToString() + @"')
                                                    BEGIN
	                                                    UPDATE A SET A.[Name] = '" + row1[1].ToString() + "',A.[Area] = '" + row1[2].ToString() + "',A.[SchedCode] = '" + row1[4].ToString() + "'  FROM usr_Branches A WHERE A.CODE = '" + row1[0].ToString() + "' AND A.COMPANY = '" + row1[3].ToString() + @"'
              
                                                    END                                                   
                                                    ELSE
                                                    BEGIN
	                                                    INSERT INTO usr_Branches
	                                                    SELECT '" + row1[0].ToString() + @"'
                                                              ,'" + row1[1].ToString() + @"'
                                                              ,'" + row1[2].ToString() + @"'
                                                              ,'" + row1[3].ToString() + @"'
                                                              ,'" + row1[4].ToString() + @"'
                                                    END
                                              ";

                        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _InsertData);
                }



                 _SQLSyntax = @"
                                    UPDATE B SET B.ScheduleCode = A.SchedCode
                                    FROM vwsDepartmentList A INNER JOIN Employees B ON A.DepartmentCode = B.Department
				                    WHERE ISNULL(B.ScheduleCode,'') <> ISNULL(A.SchedCode,'') 
                               ";
                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);
            }
        }













    }
}