using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmGlobalDataList : Form
{

    public static string _gListGroup;

    public static string _gCompany;
    public static string _gEmployeeNo;
    public static string _gEmployeeName;
    public static string _gAccountCode;
    public static string _gLoanRefNo;
    public static string _gPayrollPeriod;

    public static string _gBranchCode;
    public static string _gBranchName;
    public static string _gStatus;
    public static string _dataStatus;

    static int _HeaderColumn;


    public frmGlobalDataList()
    {
        InitializeComponent();
    }

    private void frmGlobalDataList_Load(object sender, EventArgs e)
    {
        string _sqlList = clsDeclaration.sDataDisplayQuery;
        DataTable _tblList;
        _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        String someText;
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

                switch (_gListGroup)
                {
                    case "EmployeeList":
                        _gEmployeeNo = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();
                        _gCompany = dgvDisplay.Rows[gridRow].Cells[4].Value.ToString().Trim();
                        break;
                }

                return;
            }
            gridRow++;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            switch (_gListGroup)
            {

                case "EmployeeList":
                    _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();

                    break;
        
            }



        }
        catch
        {

        }
    }

    private void dgvDisplay_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            switch (_gListGroup)
            {
                case "EmployeeList":
                    _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();

                    break;
            }



            Close();
        }
        catch
        {

        }
    }

    private void dgvDisplay_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            _HeaderColumn = e.ColumnIndex;
        }
        catch
        {

        }
    }
}