using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

using System.Configuration;

public partial class frmSILPUploading : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();
    private static string _SQLEmployeeList;
    private static string _SQLDepartmentList;

    public frmSILPUploading()
    {
        InitializeComponent();



        DataTable _CompanyList;
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        _SQLEmployeeList = @"";
        //_SQLDepartmentList = @"";
        int i = 0;
        foreach (DataRow rowDB in _CompanyList.Rows)
        {
            if (i > 0)
            {
                _SQLEmployeeList = _SQLEmployeeList + " UNION ALL ";
            }

            clsDeclaration.sServer = rowDB[3].ToString();
            clsDeclaration.sCompany = rowDB[4].ToString();
            clsDeclaration.sUsername = rowDB[5].ToString();
            clsDeclaration.sPassword = rowDB[6].ToString();
            clsDeclaration.sCompCode = rowDB[7].ToString();

            _SQLEmployeeList = _SQLEmployeeList + "SELECT *,'" + clsDeclaration.sCompCode + "' AS Company from [" + clsDeclaration.sServer + "].[" + clsDeclaration.sCompany + "].dbo.[Employees]";

            i = i + 1;
        }

        _SQLDepartmentList = "SELECT * from [" + ConfigurationManager.AppSettings["DBServer"] + "].[" + ConfigurationManager.AppSettings["DBName"] + "].dbo.[vwsDepartmentList]";


    }

    private void frmSILPUploading_Load(object sender, EventArgs e)
    {
        //DataTable _DataTable;
        //string _SQLSyntax;
        //_SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        //_CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboCompany.Items.Add(row[0].ToString());

        //}

        for (int i = ((DateTime.Now).Year - 5); i < ((DateTime.Now).Year + 5); i++)
        {
            cboYear.Items.Add(i);
        }
        cboYear.Text = (DateTime.Now).Year.ToString();

        //DataTable _DataTable;
        //string _SQLSyntax;
        //_SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM  [PayrollPeriod] A";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);

        //cboPayrolPeriod.Items.Clear();
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboPayrolPeriod.Items.Add(row[0].ToString());
        //}
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }


        //txtEmployee.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
        //txtAccount.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
        //txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {


        _DataList.Clear();
        ofdExcel.Filter = "EXCEL files (*.xlsx)|*.xlsx|EXCEL files 2003 (*.xls)|*.xls|All files (*.*)|*.*";
        ofdExcel.FilterIndex = 3;


        DialogResult result = ofdExcel.ShowDialog();
        if (result == DialogResult.OK)
        {
            txtExcelFile.Text = ofdExcel.FileName;
        }

        if (result == DialogResult.Cancel)
        {
            return;
        }


        DataTable _DataTable;
        OleDbConnection MyConnection;
        string _ExcelPath = txtExcelFile.Text;
        string _ExcelCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _ExcelPath + ";Extended Properties=Excel 12.0;";
        MyConnection = new OleDbConnection(_ExcelCon);

        MyConnection.Open();
        // Get the data table containg the schema guid.
        _DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        if (_DataTable == null)
        {
            return;
        }

        cmbWorkSheet.Items.Clear();
        foreach (DataRow row in _DataTable.Rows)
        {
            cmbWorkSheet.Items.Add(row["TABLE_NAME"].ToString());
        }
        cmbWorkSheet.SelectedIndex = 0;
        MyConnection.Close();
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        if (txtExcelFile.Text == "")
        {
            MessageBox.Show("Excel File Not Define.");
            return;
        }

        if (cboYear.Text == "")
        {
            MessageBox.Show("Please select year");
            return;
        }

        double _RowCount;
        //int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {

            string _Year = row[0].ToString();
            string _EmployeeNo = row[1].ToString();
            string _EmployeeName = row[2].ToString();
            string _BankAccountNo = row[3].ToString();
            string _NetAmount = row[5].ToString();


            if (_EmployeeNo != "")
            {
                string _SQLInsert = @"

DELETE FROM [SILPDetails] WHERE [Year] = '" + _Year + @"' AND [EmployeeNo] = '" + _EmployeeNo + @"'

									INSERT INTO [SILPDetails]
												([Year]
												,[EmployeeNo]
												,[EmployeeName]
												,[BankAccount]
												,[NetAmount])
												VALUES
												(
												'" + _Year + @"',
												'" + _EmployeeNo + @"',
												'" + _EmployeeName + @"',
												'" + _BankAccountNo + @"',
												'" + _NetAmount + @"'
												)
                                     ";

                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLInsert);



            }
        }

        MessageBox.Show("SILP Successfully uplaoded");
    }

    private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clsDeclaration.sServer = _CompanyList.Rows[cboCompany.SelectedIndex][3].ToString();
        //clsDeclaration.sCompany = _CompanyList.Rows[cboCompany.SelectedIndex][4].ToString();
        //clsDeclaration.sUsername = _CompanyList.Rows[cboCompany.SelectedIndex][5].ToString();
        //clsDeclaration.sPassword = _CompanyList.Rows[cboCompany.SelectedIndex][6].ToString();

        //clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
        //                               clsDeclaration.sServer, clsDeclaration.sCompany,
        //                               clsDeclaration.sUsername, clsDeclaration.sPassword
        //                            );

        //if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == false)
        //{
        //    return;
        //}



        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM [PayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);

        foreach (DataRow row in _DataTable.Rows)
        {
            cboYear.Items.Add(row[0].ToString());

        }

    }

    //private void txtSearch_TextChanged(object sender, EventArgs e)
    //{

    //    String someText;
    //    someText = txtSearch.Text;

    //    int gridRow = 0;
    //    int gridColumn = 1;


    //    dataGridView1.ClearSelection();
    //    dataGridView1.CurrentCell = null;

    //    foreach (DataGridViewRow row in dataGridView1.Rows)
    //    {
    //        //cboPayrolPeriod.Items.Add(row[0].ToString());

    //        DataGridViewCell _cell = dataGridView1.Rows[gridRow].Cells[gridColumn];
    //        if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
    //        {
    //            dataGridView1.Rows[gridRow].Selected = true;
    //            dataGridView1.FirstDisplayedScrollingRowIndex = gridRow;
    //            return;
    //        }
    //        gridRow++;
    //    }
    //}

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnGen_Click(object sender, EventArgs e)
    {

        DataTable _DataTable;
        OleDbConnection MyConnection;
        string _ExcelPath = txtExcelFile.Text;
        string _ExcelCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _ExcelPath + ";Extended Properties=Excel 12.0;";
        MyConnection = new OleDbConnection(_ExcelCon);

        MyConnection.Open();
        // Get the data table containg the schema guid.
        _DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        if (_DataTable == null)
        {
            return;
        }

        //cmbWorkSheet.Items.Clear();
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cmbWorkSheet.Items.Add(row["TABLE_NAME"].ToString());
        //}
        //cmbWorkSheet.SelectedIndex = 0;


        DataSet DtSet;
        OleDbDataAdapter MyDataAdapte;

        MyDataAdapte = new OleDbDataAdapter("SELECT '' AS [Year],'' AS [EmpNo],'' AS [EmployeeName],'' AS [BankAccount], * FROM [" + cmbWorkSheet.Text + "]", MyConnection);
        DtSet = new DataSet();
        MyDataAdapte.Fill(DtSet);
        _DataTable = DtSet.Tables[0];
        MyConnection.Close();

        _DataList = _DataTable;
        clsFunctions.DataGridViewSetup(dataGridView1, _DataTable);

        //cmbColumn.Items.Clear();
        //foreach (DataColumn Column in _DataList.Columns)
        //{
        //    cmbColumn.Items.Add(Column.ColumnName.ToString());
        //}

        //double _Count = 0;
        double _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            try
            {
                Application.DoEvents();
                string sEmpNo = row[4].ToString();

                if (sEmpNo != "")
                {
                    //string _sqlSelect = @"SELECT A.EmployeeNo FROM Employees A WHERE A.EmployeeNo = '" + sEmpNo + "'";
                    string _sqlSelect = @"SELECT Z.EmployeeNo, replace(replace(replace(Z.EmployeeName,char(13),' '),char(9),' '),char(10),' ') AS EmployeeName, Z.BankAccountNo, Z.Company,Z.Category FROM VwsEmployees Z
                                                    WHERE Z.EmployeeNo = '" + sEmpNo + @"'
                                                    ORDER BY CAST(Z.EmployeeNo as int)";
                    DataTable _table = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
                    string sEmployee = clsSQLClientFunctions.GetData(_table, "EmployeeNo", "0");


                    row[0] = cboYear.Text ;
                    row[1] = sEmployee;
                    row[2] = clsSQLClientFunctions.GetData(_table, "EmployeeName", "0");
                    row[3] = clsSQLClientFunctions.GetData(_table, "BankAccountNo", "0");
                }
            }
            catch
            {
            }


            //    Application.DoEvents();
            //    _Count++;
            //    lblLabel.Text = "Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % )";

            //    pbDataProgress.Refresh();
            //    pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }

        MessageBox.Show("Data Ready To Upload");

        //DataTable _DataTable;
        //OleDbConnection MyConnection;
        //string _ExcelPath = txtExcelFile.Text;
        //string _ExcelCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _ExcelPath + ";Extended Properties=Excel 12.0;";
        //MyConnection = new OleDbConnection(_ExcelCon);


        //DataSet DtSet;
        //OleDbDataAdapter MyDataAdapte;

        //MyDataAdapte = new OleDbDataAdapter("SELECT * FROM [" + cmbWorkSheet.Text + "]", MyConnection);
        //DtSet = new DataSet();
        //MyDataAdapte.Fill(DtSet);
        //_DataTable = DtSet.Tables[0];
        //MyConnection.Close();


        //string _Display;
        //_Display = "SELECT * FROM (";

        //int _Count = _DataTable.Rows.Count;
        //int i = 0;
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    string _Employee = row[0].ToString();
        //    string _AcctCode = row[1].ToString();
        //    string _Amount = row[2].ToString();


        //    string _SQLGetSyntax;
        //    DataTable _GetDataTable;

        //    _SQLGetSyntax = @"SELECT CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName] FROM ( "+ _SQLEmployeeList + @" ) A WHERE A.EmployeeNo = '" + _Employee + "'";
        //    _GetDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLGetSyntax);
        //    string _EmployeeName = clsSQLClientFunctions.GetData(_GetDataTable, "EmployeeName", "0");


        //    _SQLGetSyntax = @"SELECT A.AccountDesc FROM AccountCode A WHERE A.AccountCode = '" + _AcctCode + "'";
        //    _GetDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLGetSyntax);
        //    string _AccountName = clsSQLClientFunctions.GetData(_GetDataTable, "AccountDesc", "0");


        //    _Display = _Display + @"
        //                                SELECT '" + _Employee + "' AS [],'" + _Employee + "' AS [EMPLOYEE CODE],'" + _EmployeeName + "' AS [EMPLOYEE NAME],'" + _AcctCode + "' AS [ACCOUNT CODE],'" + _AccountName + "' AS [ACCOUNT NAME],'" + _Amount + @"' [AMOUNT]
        //                                ";

        //    i++;

        //    if (i != _Count)
        //    {
        //        _Display = _Display + @"UNION ALL
        //                                ";
        //    }
        //}



        //_Display = _Display + ") Z";
        //DataTable _DisplayTable;
        //_DisplayTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Display);
        //_DataList = _DisplayTable;
        //clsFunctions.DataGridViewSetup(dataGridView1, _DisplayTable);
    }
}