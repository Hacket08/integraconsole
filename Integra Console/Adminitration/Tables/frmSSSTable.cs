using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class frmSSSTable : Form
{
    private static DataTable _CompanyList = new DataTable();
    public frmSSSTable()
    {
        InitializeComponent();
    }

    private void frmSSSTable_Load(object sender, EventArgs e)
    {
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A";
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        foreach (DataRow row in _CompanyList.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());

        }
        btnStatus();
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

        if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == false)
        {
            return;
        }


        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [BracketFrom] AS [From],[BracketTo] AS [To],[Employer] AS [Employer Share],[ECC] AS [ECC],[Employee] AS [Employee Share] FROM [SSSTable] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable, "SSS");

        tsTotalRecord.Text = _DataTable.Rows.Count.ToString() + " Record/s";
        btnStatus(0);

        dgvDisplay.Rows[0].Selected = true;
        dgvDisplay.FirstDisplayedScrollingRowIndex = 0;


        txtBracketFrom.Text = double.Parse(dgvDisplay.Rows[0].Cells[0].Value.ToString().Trim()).ToString("N2");
        txtBracketTo.Text = double.Parse(dgvDisplay.Rows[0].Cells[1].Value.ToString().Trim()).ToString("N2");
        txtEmployerShare.Text = double.Parse(dgvDisplay.Rows[0].Cells[2].Value.ToString().Trim()).ToString("N2");
        txtECC.Text = double.Parse(dgvDisplay.Rows[0].Cells[3].Value.ToString().Trim()).ToString("N2");
        txtEmployeeShare.Text = double.Parse(dgvDisplay.Rows[0].Cells[4].Value.ToString().Trim()).ToString("N2");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        //Close();
        btnStatus(0);
    }

    private void btnStatus(int type = 9999)
    {
        switch (type)
        {
            case 0: // Add Mode
                btnAdd.Enabled = true;
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnCancel.Enabled = false;
                break;
            case 1: // Edit Mode
                btnAdd.Enabled = false;
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnCancel.Enabled = true;
                break;
            default:
                btnAdd.Enabled = false;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;
                break;
        }
    }


    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        txtBracketFrom.Text = double.Parse(dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()).ToString("N2");
        txtBracketTo.Text = double.Parse(dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim()).ToString("N2");
        txtEmployerShare.Text = double.Parse(dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim()).ToString("N2");
        txtECC.Text = double.Parse(dgvDisplay.Rows[e.RowIndex].Cells[3].Value.ToString().Trim()).ToString("N2");
        txtEmployeeShare.Text = double.Parse(dgvDisplay.Rows[e.RowIndex].Cells[4].Value.ToString().Trim()).ToString("N2");
    }

    private void _KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
        {
            e.Handled = true;
        }

        // only allow one decimal point
        if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
        {
            e.Handled = true;
        }
    }

    private void _TextChanged(object sender, EventArgs e)
    {
        if (txtBracketFrom.Text != "") { txtBracketFrom.Text = string.Format("{0:#,##0.00}", double.Parse(txtBracketFrom.Text)); }
        if (txtBracketTo.Text != "") { txtBracketTo.Text = string.Format("{0:#,##0.00}", double.Parse(txtBracketTo.Text)); }
        if (txtECC.Text != "") { txtECC.Text = string.Format("{0:#,##0.00}", double.Parse(txtECC.Text)); }
        if (txtEmployeeShare.Text != "") { txtEmployeeShare.Text = string.Format("{0:#,##0.00}", double.Parse(txtEmployeeShare.Text)); }
        if (txtEmployerShare.Text != "") { txtEmployerShare.Text = string.Format("{0:#,##0.00}", double.Parse(txtEmployerShare.Text)); }
        //MessageBox.Show(sender);
        //textBox1.Text = string.Format("{0:#,##0.00}", double.Parse(textBox1.Text));
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        btnStatus(1);
    }
}