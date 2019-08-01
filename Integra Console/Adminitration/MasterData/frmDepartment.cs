using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmDepartment : Form
{
    public frmDepartment()
    {
        InitializeComponent();
    }

    private void frmDepartment_Load(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompCode,' - ',A.CompanyName) AS Company FROM OCMP A WHERE A.Active = '1'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboCompany.Items.Clear();
        cboCompany.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }


        _SQLSyntax = "SELECT DISTINCT A.Area FROM [OBLP] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }

        _SQLSyntax = "SELECT CONCAT(A.Code,' - ',A.Name) AS Name FROM OBLP A ORDER BY A.Code";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboBranch.Items.Clear();
        cboBranch.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboBranch.Items.Add(row[0].ToString());
        }



        _SQLSyntax = "SELECT CONCAT(A.Code,' - ',A.Name) AS Name FROM OPST A ORDER BY A.Code";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboPosition.Items.Clear();
        cboPosition.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPosition.Items.Add(row[0].ToString());
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
                        SELECT * FROM ODPT A WHERE LEFT(A.Code, 2) = '" + cboCompany.SelectedIndex.ToString().PadLeft(2, '0') + @"'
                      ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable);


            _SQLSyntax = @"
                        SELECT (ISNULL(MAX(CAST(RIGHT(A.Code, 4) AS INT)),0) + 1) AS LastCode FROM ODPT A WHERE LEFT(A.Code, 2) = '" + cboCompany.SelectedIndex.ToString().PadLeft(2, '0') + @"'
                      ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

            txtCode.Text = cboCompany.SelectedIndex.ToString().PadLeft(2, '0')  + clsSQLClientFunctions.GetData(_DataTable, "LastCode", "1").PadLeft(4, '0');
            txtName.Text = cboCompany.Text.Substring(0, 4) + cboBranch.Text.Substring(0, 4) + cboPosition.Text.Substring(0, 4);
        }
        catch { }

    }

    private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void cboPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {

        if (cboBranch.Text == "" || cboCompany.Text == "" || cboPosition.Text == "")
        {
            MessageBox.Show("Missing Information");
            return;
        }


        string _SQLSyntax;
        DataTable _DataTable;

        _SQLSyntax = "SELECT 'TRUE' FROM ODPT A WHERE A.Name = '" + txtName.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        if (_DataTable.Rows.Count != 0)
        {
            MessageBox.Show("Data Already Exist");
            return;
        }






        string _InsertSyntax;

        _InsertSyntax = @"
                                    INSERT INTO ODPT
                                    (
	                                     [Code]
	                                    ,[Name]
                                    )
                                    VALUES
                                    (
	                                     '" + txtCode.Text + @"'
	                                    ,'" + txtName.Text + @"'
                                    )
                                 ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _InsertSyntax);

 
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = 1";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        foreach (DataRow row in _DataTable.Rows)
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

                _InsertSyntax = @"
                                    INSERT INTO Department
                                    (
	                                     [DepartmentCode]
	                                    ,[DepartmentDesc]
                                    )
                                    VALUES
                                    (
	                                     '" + txtCode.Text + @"'
	                                    ,'" + txtName.Text + @"'
                                    )
                                 ";

                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _InsertSyntax);
            }
        }
        

        displayData();
    }

    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT CONCAT(A.Code,' - ',A.Name) AS Branch FROM [OBLP] A WHERE A.Area = '" + cboArea.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboBranch.Items.Clear();
        cboBranch.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboBranch.Items.Add(row[0].ToString());
        }
    }
}