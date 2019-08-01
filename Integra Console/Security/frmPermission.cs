using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmPermission : Form
{

    public static string _Code;
    public static string _Name;

    public static string _Module;

    public frmPermission()
    {
        InitializeComponent();
    }

    private void frmPermission_Load(object sender, EventArgs e)
    {

        txtCode.Text = _Code;
        txtName.Text = _Name;



        dataGridView1.ColumnCount = 3;

        dataGridView1.Columns[0].Name = "ID";
        dataGridView1.Columns[1].Name = "Modules";
        dataGridView1.Columns[2].Name = "Path";

        DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
        cmb.HeaderText = "Access";
        cmb.Name = "cmb";
        cmb.MaxDropDownItems = 3;
        cmb.Items.Add("Full");
        cmb.Items.Add("No Access");
        dataGridView1.Columns.Add(cmb);


        displayData();
    }



    private void displayData()
    {
        try
        {
            dataGridView1.Rows.Clear();

            DataTable _DataTable;
            string _SQLSyntax;
            _SQLSyntax = @"
                            SELECT A.oID,A.[Index],A.Module,ISNULL(B.Access,'No Access') AS Access
                            ,
                                                    CASE 
                                                    WHEN A.ModuleCode = 0 THEN '' + A.Module
                                                    WHEN A.ModuleCode = 1 THEN '     ' + A.Module
                                                    WHEN A.ModuleCode = 2 THEN '          ' + A.Module
                                                    WHEN A.ModuleCode = 3 THEN '               ' + A.Module
                                                    WHEN A.ModuleCode = 4 THEN '                    ' + A.Module
                                                    WHEN A.ModuleCode = 5 THEN '                         ' + A.Module
                                                    WHEN A.ModuleCode = 6 THEN '                              ' + A.Module
                                                    END [Module Name]
                            FROM OUAM A LEFT JOIN OUAS B ON A.oID = B.ModuleID AND B.[UserID] = '" + txtCode.Text + @"'
ORDER BY A.[Index]
                      ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable, "Permission");

            foreach (DataRow row in _DataTable.Rows)
            {
                string oID = row["oID"].ToString();
                string oMod = row["Module"].ToString();
                string oModule = row["Module Name"].ToString();
                string oIndex = row["Index"].ToString();
                string oAccess = row["Access"].ToString();
                dataGridView1.Rows.Add(oID, oModule, oIndex, oAccess);



                string _Syntax;
                _Syntax = @"IF NOT EXISTS (SELECT 'TRUE' FROM [OUAS] Z WHERE Z.[UserID] = '" + txtCode.Text + @"' AND Z.[ModuleID] = '" + oID + @"')
                        BEGIN
		                        INSERT INTO [dbo].[OUAS]
				                           ([UserID]
				                           ,[ModuleID]
				                           ,[Module]
				                           ,[Access])
			                         VALUES
				                           ('" + txtCode.Text + @"'
				                           ,'" + oID  + @"'
				                           ,'" + oMod + @"'
				                           ,'No Access')
                        END
                    ";
                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Syntax);

            }

        }
        catch
        {
        }
    }



    //private void dataGridView1_EditingControlShowing(object sender,
    //DataGridViewEditingControlShowingEventArgs e)
    //{
    //    try
    //    {
    //        if (dataGridView1.CurrentCell.ColumnIndex == 3)
    //        {
    //            // Check box column
    //            ComboBox comboBox = e.Control as ComboBox;
    //            comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
    //        }
    //    }
    //    catch { }

    //}

    //void comboBox_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selectedIndex = ((ComboBox)sender).SelectedIndex;
    //        //MessageBox.Show("Selected Index = " + selectedIndex);


    //        String someText;
    //        someText = _Module;

    //        string _Syntax = @"UPDATE B SET B.Access  = '" + ((ComboBox)sender).Text + @"' FROM OUAM A INNER JOIN OUAS B ON A.oID = B.ModuleID
    //                        WHERE A.[Index] LIKE '%" + _Module + @"%' AND B.[UserID] = '" + txtCode.Text + @"'
    //                        ";
    //        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Syntax);

    //        displayData();

    //    }
    //    catch { }
     
    //}


    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        //try
        //{
        //    _Module = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
        //}
        //catch { }
    }

    private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            _Module = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
           string _Access = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();

            string _Syntax = @"UPDATE B SET B.Access  = '" + _Access + @"' FROM OUAM A INNER JOIN OUAS B ON A.oID = B.ModuleID
                                    WHERE A.[Index] LIKE '%" + _Module + @"%' AND B.[UserID] = '" + txtCode.Text + @"'
                                    ";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Syntax);

            displayData();
        }
        catch { }
    }
}