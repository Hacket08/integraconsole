using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class frmPagIbigTable : Form
{
    private static DataTable _CompanyList = new DataTable();
    public frmPagIbigTable()
    {
        InitializeComponent();
    }

    private void frmPagIbigTable_Load(object sender, EventArgs e)
    {
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A";
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        foreach (DataRow row in _CompanyList.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());

        }
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
        _SQLSyntax = "SELECT [CustomPYCode],[CustomPYDesc] FROM [CustomPYSetup] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataTable);

        tsTotalRecord.Text = _DataTable.Rows.Count.ToString() + " Record/s";
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}