using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmParameter : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();

    public frmParameter()
    {
        InitializeComponent();
    }

    private void frmParameter_Load(object sender, EventArgs e)
    {
        panel1.Visible = true;
        panel2.Visible = true;
        switch (clsDeclaration.sReportTag)
        {
            case 2:
                panel2.Visible = false;
                break;
        }


        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = 1";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }

    private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsDeclaration.sCompanyName = _CompanyList.Rows[cboCompany.SelectedIndex][2].ToString();
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
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM dbo.[vwsPayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());

        }

        _SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }

        //_SQLSyntax = "SELECT DISTINCT CONCAT(A.BCode,' - ',A.BName) AS Branch FROM dbo.[vwsDepartmentList] A";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        //cboBranch.Items.Clear();
        //cboBranch.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboBranch.Items.Add(row[0].ToString());
        //}

    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        clsDeclaration.sDateFrom = dtpFrom.Value.Date;
        clsDeclaration.sDateTo = dtpTo.Value.Date;
        clsDeclaration.sArea = cboArea.Text;

        if (cboBranch.Text == "")
        {
            clsDeclaration.sBranch = "";
        }
        else
        {
            clsDeclaration.sBranch = cboBranch.Text.Substring(0, 8);
        }

        clsDeclaration.sPayrollPeriod = cboPayrolPeriod.Text;

        clsDeclaration.bView = true;
        Close();
    }

    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT CONCAT(A.BCode,' - ',A.BName) AS Branch FROM dbo.[vwsDepartmentList] A WHERE A.Area = '" + cboArea.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboBranch.Items.Clear();
        cboBranch.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboBranch.Items.Add(row[0].ToString());
        }
    }
}
