using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmUsers : Form
{

    public static string _Code;
    public static string _Name;

    public frmUsers()
    {
        InitializeComponent();
    }

    private void frmUsers_Load(object sender, EventArgs e)
    {
        displayData();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        frmAddUser frmAddUser = new frmAddUser();
        frmAddUser.ShowDialog();

        displayData();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }


    private void displayData()
    {
        try
        {
            DataTable _DataTable;
            string _SQLSyntax;
            _SQLSyntax = @"
                        SELECT [USERID],[U_NAME] FROM OUSR ORDER BY [USERID]
                      ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable);
        }
        catch { }

    }

    private void btnPermission_Click(object sender, EventArgs e)
    {

        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(frmPermission))
            {
                form.Activate();
                return;
            }
        }
        
        frmPermission frmPermission = new frmPermission();


        frmPermission._Code = _Code;
        frmPermission._Name = _Name;


        frmPermission.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmPermission.Show();
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            _Code = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            _Name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        catch
        {

        }
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
