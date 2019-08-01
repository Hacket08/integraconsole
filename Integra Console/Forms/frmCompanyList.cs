using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class frmCompanyList : Form
{
    public frmCompanyList()
    {
        InitializeComponent();
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        string _SQLSyntax;

        int _Check = chkActive.Checked ? 1 : 0;


        if (txtCompCode.Enabled == true)
        {
            _SQLSyntax = @"
                                    INSERT INTO OCMP
                                    (
	                                     [CompanyCode]
	                                    ,[CompanyName]
	                                    ,[DBServer]
	                                    ,[DBName]
	                                    ,[DBUsername]
	                                    ,[DBPassword]
                                        ,[CompCode]
                                        ,[Active]
                                    )
                                    VALUES
                                    (
	                                     '" + txtCompCode.Text + @"'
	                                    ,'" + txtCompName.Text + @"'
	                                    ,'" + txtServer.Text + @"'
	                                    ,'" + txtDBName.Text + @"'
	                                    ,'" + txtServerUsername.Text + @"'
	                                    ,'" + txtServerPassword.Text + @"'
	                                    ,'" + txtCode.Text + @"'
	                                    ,'" + _Check + @"'
        
                                    )
                                 ";
        }
        else
        {
            _SQLSyntax = @"
                                    UPDATE OCMP SET
	                                     [CompanyName] ='" + txtCompName.Text + @"'
	                                    ,[DBServer] ='" + txtServer.Text + @"'
	                                    ,[DBName] = '" + txtDBName.Text + @"'
	                                    ,[DBUsername] = '" + txtServerUsername.Text + @"'
	                                    ,[DBPassword] = '" + txtServerPassword.Text + @"'
	                                    ,[CompCode] = '" + txtCode.Text + @"'
	                                    ,[Active] = '" + _Check + @"'
                                    WHERE [CompanyCode] = '" + txtCompCode.Text + @"'
                                 ";
        }


        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

        _SQLSyntax = "SELECT * FROM OCMP A";
        DataTable _DataTable;
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dataGridView1, _DataTable);

        MessageBox.Show("Company List Successfully Updated.");
        FieldClear();
    }


    private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        txtCompCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
        txtCompCode.Enabled = false;
        txtCompName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
        txtServer.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
        txtDBName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
        txtServerUsername.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
        txtServerPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();
        txtCode.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString().Trim();

        if (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString().Trim() == "1")
        {
            chkActive.Checked = true;
        }
        else
        {
            chkActive.Checked = false;
        }

     
    }
    private void frmCompanyList_Load(object sender, EventArgs e)
    {

        string _SQLSyntax = "SELECT * FROM OCMP A";
        DataTable _DataTable;
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dataGridView1, _DataTable);

    }


    private void FieldClear()
    {
        txtCompCode.Text = "";
        txtCompName.Text = "";
        txtServer.Text = "";
        txtDBName.Text = "";
        txtServerUsername.Text = "";
        txtServerPassword.Text = "";
        txtCode.Text = "";
    }

    private void newDataToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
        FieldClear();
        txtCompCode.Enabled = true;
    }

    private void deleteDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string _SQLSyntax = "DELETE FROM OCMP A WHERE [CompanyCode] = '" + txtCompCode.Text + @"'";
        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

        MessageBox.Show("Data Deleted.");
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {

    }
}