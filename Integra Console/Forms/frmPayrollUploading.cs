using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;



public partial class frmPayrollUploading : Form
{

    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();
    public frmPayrollUploading()
    {
        InitializeComponent();
    }

    private void frmPayrollUploading_Load(object sender, EventArgs e)
    {
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

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
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
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please select payroll period");
            return;
        }

        double _RowCount;
        int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            string _Employee = row[0].ToString();
            string _Company = row[1].ToString();
            string _Category = row[2].ToString();
            string _EmployeeName = row[4].ToString();

            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
            string _PayrollPeriod = cboPayrolPeriod.Text;



            if (_Employee != "")
            {

                //double _DailyRate = double.Parse(row[3].ToString());
                //double _MonthlyRate = double.Parse(row[4].ToString());

                double _TotalDays = double.Parse(row[6].ToString());
                double _BasicPay = double.Parse(row[7].ToString());

                double _OTHrs = double.Parse(row[8].ToString());
                double _OTPay = double.Parse(row[9].ToString());
                double _SPLHrs = double.Parse(row[10].ToString());
                double _SPLPay = double.Parse(row[11].ToString());
                double _LEGHrs = double.Parse(row[12].ToString());
                double _LEGPay = double.Parse(row[13].ToString());

                double _OtherIncome = double.Parse(row[14].ToString());
                double _GrossPay = double.Parse(row[15].ToString());


                double _SSSLoan = double.Parse(row[17].ToString());
                double _PagibigLoan = double.Parse(row[19].ToString());
                double _OtherDeduction = double.Parse(row[22].ToString());
                double _NetPay = double.Parse(row[23].ToString());
                

                string _SQLExecute = @"
                                        DECLARE @Employee NVARCHAR(30)
                                        DECLARE @PayPeriod NVARCHAR(30)

                                        SET @Employee = '" + _Employee + @"'
                                        SET @PayPeriod = '" + _PayrollPeriod + @"'

                                        IF EXISTS (SELECT 'TRUE' FROM [PayrollHeader] A WHERE A.[EmployeeNo] = @Employee AND A.[PayrollPeriod] = @PayPeriod)
                                        BEGIN
                                                    UPDATE A
                                                       SET A.[BasicPay] = '" + _BasicPay  + @"'
                                                          ,A.[OTPay] = '" + _OTPay + @"'
                                                          ,A.[OtherIncome] =  '" + _OtherIncome + @"'
                                                          ,A.[OtherDeduction] = '" + _OtherDeduction + @"'
                                                          ,A.[Gross] =  '" + _GrossPay + @"'
                                                          ,A.[TotalDeductions] =  (A.[SSSLoan] + A.[PagibigLoan] + A.[OtherLoan] + A.[OtherDeduction] + A.[SSSEmployee] + A.[PhilHealthEmployee] + A.[PagIbigEmployee] + A.[WitholdingTax] )
                                                          ,A.[NetPay] = '" + _NetPay + @"'
                                                          ,A.[TotalDays] = '" + _TotalDays + @"'
                                                          ,A.[SSSLoan] = '" + _SSSLoan + @"'
                                                          ,A.[PagibigLoan] = '" + _PagibigLoan + @"'
                                                          ,A.[OTHrs] = '" + _OTHrs + @"'
                                                          ,A.[SPLHrs] = '" + _SPLHrs + @"'
                                                          ,A.[LEGHrs] = '" + _LEGHrs + @"'
                                                          ,A.[SPLPay] = '" + _SPLPay + @"'
                                                          ,A.[LEGPay] = '" + _LEGPay + @"'
                                                     FROM [PayrollHeader] A WHERE A.[EmployeeNo] = @Employee AND A.[PayrollPeriod] = @PayPeriod
                                        END
                                                  ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLExecute);


                // Excel Progress Monitoring
                Application.DoEvents();
                _Count++;

                //frmMainWindow frmMainWindow = new frmMainWindow();
                tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
                pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));


            }
        }
        
        MessageBox.Show("Payroll Data Uploaded");
    }
}