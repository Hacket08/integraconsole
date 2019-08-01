using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmSystemData : Form
{
    public static string _gListGroup;
    public static int _ColumnCount;

    public static string _gCode;
    public static string _gName;
    public static string _gColumn3;

    static int _HeaderColumn;

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Enter)
        {
            btn_Click(btnChoose, EventArgs.Empty);
            return true;
        }
        return false;
        //handle your keys here
    }
    public frmSystemData()
    {
        InitializeComponent();
    }

    private void frmSystemData_Load(object sender, EventArgs e)
    {
        string _sqlList = clsDeclaration.sQueryString;
        DataTable _tblList;

        _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
    }



    void ColumnDisplay(int _ColumnCount, int _RowIndex)
    {

        switch (_ColumnCount)
        {
            case 3:
                _gColumn3 = dgvDisplay.Rows[_RowIndex].Cells[2].Value.ToString().Trim();
                break;
            default:
                break;
        }
        _gCode = dgvDisplay.Rows[_RowIndex].Cells[0].Value.ToString().Trim();
        _gName = dgvDisplay.Rows[_RowIndex].Cells[1].Value.ToString().Trim();
    }

    private void btn_Click(object sender, EventArgs e)
    {
        Button btnName = sender as Button;
        switch (btnName.Text)
        {
            case "Cancel":

                _gCode = "";
                _gName = "";

                break;
            default:
                break;
        }

        Close();
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        string someText;
        someText = txtSearch.Text;

        int gridRow = 0;
        int gridColumn = _HeaderColumn;


        dgvDisplay.ClearSelection();
        dgvDisplay.CurrentCell = null;

        foreach (DataGridViewRow row in dgvDisplay.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dgvDisplay.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dgvDisplay.Rows[gridRow].Selected = true;
                dgvDisplay.FirstDisplayedScrollingRowIndex = gridRow;

                ColumnDisplay(_ColumnCount, gridRow);

                return;
            }
            gridRow++;
        }
    }



    private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            _HeaderColumn = e.ColumnIndex;
        }
        catch
        {

        }
    }

    private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            ColumnDisplay(_ColumnCount, e.RowIndex);

            
        }
        catch
        {

        }
    }

    private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            ColumnDisplay(_ColumnCount, e.RowIndex);


            Close();
        }
        catch
        {

        }
    }

}
