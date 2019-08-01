using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmPosition : Form
{
    public frmPosition()
    {
        InitializeComponent();
    }

    private void frmPosition_Load(object sender, EventArgs e)
    {
        displayData();

        btnSave.Enabled = false;
    }


    private void displayData()
    {
        try
        {
            DataTable _DataTable;
            string _SQLSyntax;
            _SQLSyntax = @"
                        SELECT [Code]
	                                    ,[Name]
	                                    ,CASE WHEN ISNULL([CalcBreak],'0') = '0' THEN 'N' ELSE 'Y' END AS CalcBreak  FROM OPST A
                      ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable);
        }
        catch { }

    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        txtCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
        txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();


        if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString().Trim() == "Y")
        {
            chkActive.Checked = true;
        }
        else
        {
            chkActive.Checked = false;
        }


        txtCode.ReadOnly = true;
        txtName.ReadOnly = true;


        btnSave.Enabled = false;
        btnAdd.Enabled = true;
        btnDelete.Enabled = true;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        txtCode.Text = "";
        txtName.Text = "";

        txtCode.ReadOnly = false;
        txtName.ReadOnly = false;


        btnSave.Enabled = true;
        btnAdd.Enabled = false;
        btnDelete.Enabled = false;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string _SQLSyntax;
        
        DataTable _DataTable;

        _SQLSyntax = "SELECT 'TRUE' FROM OPST A WHERE A.CODE = '" + txtCode.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        int _Check = chkActive.Checked ? 1 : 0;



        if (_DataTable.Rows.Count != 0)
        {
            _SQLSyntax = @"UPDATE A SET
                            A.[Name] = '" + txtName.Text + @"'
                        FROM OPST A  WHERE A.CODE = '" + txtCode.Text + @"'
              ";
        }
        else
        {

            _SQLSyntax = @"
                                    INSERT INTO OPST
                                    (
	                                     [Code]
	                                    ,[Name]
	                                    ,[CalcBreak]
                                    )
                                    VALUES
                                    (
	                                     '" + txtCode.Text + @"'
	                                    ,'" + txtName.Text + @"'
	                                    ,'" + _Check + @"'
                                    )
                                 ";
        }

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);
        displayData();

        txtCode.ReadOnly = true;
        txtName.ReadOnly = true;


        btnSave.Enabled = false;
        btnAdd.Enabled = true;
        btnDelete.Enabled = true;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        DialogResult result;
        result = MessageBox.Show("Deleting This Data is Irreversable! Are yous Sure You Want To Continue?", "Delete Data", MessageBoxButtons.OKCancel);

        if (result == System.Windows.Forms.DialogResult.OK)
        {
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, "DELETE FROM OPST WHERE CODE = '" + txtCode.Text + "'");
        }

        displayData();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
