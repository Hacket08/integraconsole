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

public partial class frmAccountUploading : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();
    private static string _SQLEmployeeList;
    private static string _SQLDepartmentList;

    public frmAccountUploading()
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

    private void frmAccountUploading_Load(object sender, EventArgs e)
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

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM  [PayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());
        }
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }


        txtEmployee.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
        txtAccount.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
        txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
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



        //if (cboCompany.Text == "")
        //{
        //    MessageBox.Show("Company Not Define.");
        //    return;
        //}

        //if (cboPayrolPeriod.Text == "")
        //{
        //    MessageBox.Show("Payroll Period Not Define.");
        //    return;
        //}



        //ofdExcel.InitialDirectory = "c:\\";
        //ofdExcel.Filter = "EXCEL files (*.xlsx)|*.xlsx";
        //ofdExcel.FilterIndex = 1;


        //DialogResult result = ofdExcel.ShowDialog();
        //if (result == DialogResult.OK)
        //{
        //    txtExcelFile.Text = ofdExcel.FileName;
        //}

        //if (result == DialogResult.Cancel)
        //{
        //    return;
        //}


        //DataTable _DataTable;
        //OleDbConnection MyConnection;
        //string _ExcelPath = txtExcelFile.Text;
        //string _ExcelCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _ExcelPath + ";Extended Properties=Excel 12.0;";
        //MyConnection = new OleDbConnection(_ExcelCon);


        //DataSet DtSet;
        //OleDbDataAdapter MyDataAdapte;

        //MyDataAdapte = new OleDbDataAdapter("SELECT * FROM [DATA$]", MyConnection);
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

        //    _SQLGetSyntax = @"SELECT CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName] FROM Employees A WHERE A.EmployeeNo = '" + _Employee + "'";
        //    _GetDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLGetSyntax);
        //    string _EmployeeName = clsSQLClientFunctions.GetData(_GetDataTable, "EmployeeName", "0");


        //    _SQLGetSyntax = @"SELECT A.AccountDesc FROM AccountCode A WHERE A.AccountCode = '" + _AcctCode + "'";
        //    _GetDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLGetSyntax);
        //    string _AccountName = clsSQLClientFunctions.GetData(_GetDataTable, "AccountDesc", "0");


        //    _Display = _Display + @"
        //                                SELECT '" + _Employee + "' AS [EMPLOYEE CODE],'" + _EmployeeName + "' AS [EMPLOYEE NAME],'" + _AcctCode + "' AS [ACCOUNT CODE],'" + _AccountName + "' AS [ACCOUNT NAME],'" + _Amount + @"' [AMOUNT]
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

    private void btnUpload_Click(object sender, EventArgs e)
    {



        if (txtExcelFile.Text == "")
        {
            MessageBox.Show("Excel File Not Define.");
            return;
        }

        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please select payroll period");
            return;
        }

        double _RowCount;
        //int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            string _Employee = row[0].ToString();
            string _Company = row[1].ToString();
            string _Category = row[2].ToString();

            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
            string _PayrollPeriod = cboPayrolPeriod.Text;

            if (_Employee != "")
            {
                string _AcctCode = row[4].ToString();
                string _Amount = row[5].ToString();
                
                string _SQLInsert = @"

									INSERT INTO [PayrollTrans02]
												([PayrollPeriod]
												,[EmployeeNo]
												,[AccountCode]
												,[Amount])
												VALUES
												(
												'" + cboPayrolPeriod.Text + @"',
												'" + _Employee + @"',
												'" + _AcctCode + @"',
												'" + _Amount + @"'
												)

                                     ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLInsert);



            }
        }

        MessageBox.Show("Deductions / Incomes are successfuly uplaoded");















        //string _Display;
        //_Display = "SELECT * FROM (";
        //int _Count = _DataList.Rows.Count;
        //int i = 0;

        //foreach (DataRow row in _DataList.Rows)
        //{
        //    string _Employee = row[3].ToString();
        //    string _AcctCode = row[4].ToString();
        //    string _Amount = row[5].ToString();






        //    string _Remrks;

        //    string _SQLSelect = "SELECT * FROM PayrollTrans02 A WHERE A.[PayrollPeriod] = '" + cboPayrolPeriod.Text + "' AND A.[EmployeeNo] = '" + _Employee + "' AND A.[AccountCode] = '" + _AcctCode + "'";
        //    DataTable _DataTable;
        //    _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSelect);




        //    string _SQLGetSyntax;
        //    DataTable _GetDataTable;

        //    _SQLGetSyntax = @"SELECT CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName] FROM Employees A WHERE A.EmployeeNo = '" + _Employee + "'";
        //    _GetDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLGetSyntax);
        //    string _EmployeeName = clsSQLClientFunctions.GetData(_GetDataTable, "EmployeeName", "0");



        //    _SQLGetSyntax = @"SELECT A.AccountDesc FROM AccountCode A WHERE A.AccountCode = '" + _AcctCode + "'";
        //    _GetDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLGetSyntax);
        //    string _AccountName = clsSQLClientFunctions.GetData(_GetDataTable, "AccountDesc", "0");


        //    if (_DataTable.Rows.Count == 0)
        //    {

        //        if (_EmployeeName != "" || _AccountName != "")
        //        {
        //            string _SQLInsert = @"

        //	INSERT INTO [PayrollTrans02]
        //				([PayrollPeriod]
        //				,[EmployeeNo]
        //				,[AccountCode]
        //				,[Amount])
        //				VALUES
        //				(
        //				'" + cboPayrolPeriod.Text + @"',
        //				'" + _Employee + @"',
        //				'" + _AcctCode + @"',
        //				'" + _Amount + @"'
        //				)

        //                             ";
        //            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLInsert);
        //            _Remrks = "Successfully Uploaded";
        //        }
        //        else
        //        {
        //            _Remrks = "Employee or Account Not Found";
        //        }

        //    }
        //    else
        //    {
        //        _Remrks = "Already Uploaded";
        //    }






        //    _Display = _Display + @"

        //                                SELECT '" + _Employee + "' AS [EMPLOYEE CODE],'" + _EmployeeName + "' AS [EMPLOYEE NAME],'" + _AcctCode + "' AS [ACCOUNT CODE],'" + _AccountName + "' AS [ACCOUNT NAME],'" + _Amount + @"' [AMOUNT]
        //                                ,'" + _Remrks + @"' [REMARKS]
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
        //clsFunctions.DataGridViewSetup(dataGridView1, _DisplayTable);


        MessageBox.Show("Deductions / Incomes are successfuly uplaoded");
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
            cboPayrolPeriod.Items.Add(row[0].ToString());

        }

    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {

        String someText;
        someText = txtSearch.Text;

        int gridRow = 0;
        int gridColumn = 1;


        dataGridView1.ClearSelection();
        dataGridView1.CurrentCell = null;

        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dataGridView1.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dataGridView1.Rows[gridRow].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = gridRow;
                return;
            }
            gridRow++;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnGen_Click(object sender, EventArgs e)
    {
        string _SQLEmployeeList;

        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        _SQLEmployeeList = @"";
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

        MyDataAdapte = new OleDbDataAdapter("SELECT '' AS EmpNo,'' AS Company,'' AS Category,* FROM [" + cmbWorkSheet.Text + "]", MyConnection);
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
                string sEmpNo = row[3].ToString();

                if (sEmpNo != "")
                {
                    //string _sqlSelect = @"SELECT A.EmployeeNo FROM Employees A WHERE A.EmployeeNo = '" + sEmpNo + "'";
                    string _sqlSelect = @"SELECT Z.EmployeeNo, Z.lastname, Z.FirstName, Z.MiddleName, Z.Company,Z.Category FROM (
                                                    " + _SQLEmployeeList + @"
                                                    ) Z
                                                    WHERE Z.EmployeeNo = '" + sEmpNo + @"'
                                                    ORDER BY CAST(Z.EmployeeNo as int)";
                    DataTable _table = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _sqlSelect);
                    string sEmployee = clsSQLClientFunctions.GetData(_table, "EmployeeNo", "0");

                    row[0] = sEmployee;
                    row[1] = clsSQLClientFunctions.GetData(_table, "Company", "0");
                    row[2] = clsSQLClientFunctions.GetData(_table, "Category", "0");
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