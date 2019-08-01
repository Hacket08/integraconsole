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

public partial class frmLoanUploading : Form
{

    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();
    public frmLoanUploading()
    {
        InitializeComponent();
    }

    private void frmLoanUploading_Load(object sender, EventArgs e)
    {
        cboInsertType.Items.Add("Add");
        cboInsertType.Items.Add("Update");
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
                string sEmpNo = row[4].ToString();

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
        if (cboInsertType.Text == "")
        {
            MessageBox.Show("Please select Recording Type");
            return;
        }

        double _RowCount;
        int _Count;
        _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            string _Employee = row[0].ToString();
            string _Company = row[1].ToString();
            string _Category = row[2].ToString();
            string _EmployeeName = row[5].ToString();
            string _AccountCode = row["AccountCode"].ToString();


            if (_Employee != "")
            {
                string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                string _LoanRefNo = row["LoanRefNo"].ToString();


                if (cboInsertType.Text == "Add")
                {
                    string _sqlSave = @"SELECT 'TRUE' FROM LoanFile A WHERE A.LoanRefNo = '" + _LoanRefNo + @"'";
                    DataTable _tblSave;
                    _tblSave = clsSQLClientFunctions.DataList(_ConCompany, _sqlSave);

                    if (_tblSave.Rows.Count > 1)
                    {
                        MessageBox.Show("Reference Number Already Exists : " + _Employee + " : " + _EmployeeName + " : " + _LoanRefNo + " ");
                        return;
                    }

                    _sqlSave = @"SELECT 'TRUE' FROM AccountCode A WHERE A.AccountCode = '" + _AccountCode + @"'";
                    _tblSave = clsSQLClientFunctions.DataList(_ConCompany, _sqlSave);

                    if (_tblSave.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Account Code : " + _Employee + " : " + _EmployeeName + " : " + _LoanRefNo + " ");
                        return;
                    }
                }

            }





            //// Excel Progress Monitoring
            //Application.DoEvents();
            //_Count++;

            ////frmMainWindow frmMainWindow = new frmMainWindow();
            //tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            //pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }


        _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            string _Employee = row["EmployeeNo"].ToString();
            string _Company = row[1].ToString();
            string _Category = row[2].ToString();

            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
            string _PayrollPeriod = cboInsertType.Text;

            string _EmployeeName = row[5].ToString();
            string _AccountCode = row["AccountCode"].ToString();
            string _Brand = row["Brand"].ToString();
            string _Terms = row["Terms"].ToString();
            string _LoanRefNo = row["LoanRefNo"].ToString();
            string _LoanDate = row["LoanDate"].ToString();
            string _LCPPrice = row["LCPPrice"].ToString();
            string _SpotCashAmount = row["SpotCashAmount"].ToString();
            string _DownPayment = row["DownPayment"].ToString();
            string _AmountGranted = row["AmountGranted"].ToString();
            string _LoanAmount = row["LoanAmount"].ToString();
            string _Amortization = row["Amortization"].ToString();
            string _StartOfDeduction = row["StartOfDeduction"].ToString();
            string _TermsOfPayment = row["TermsOfPayment"].ToString();
            string _Status = row["Status"].ToString();
            string _Particular = row["Particular"].ToString();

            string _Rebate = row["Rebate"].ToString();
            string _RebateApplication = row["RebateApplication"].ToString();
            string _LoanInterest = row["LoanInterest"].ToString();

            

            string _SQLExecute = @"";
            if (_Employee != "")
            {

                switch (cboInsertType.Text)
                {
                    case "Add":


                        _SQLExecute = @"

                                    INSERT INTO [dbo].[LoanFile]
                                                ([EmployeeNo]
                                                ,[AccountCode]
                                                ,[LoanRefNo]
                                                ,[StartOfDeduction]
                                                ,[LoanAmount]
                                                ,[AmountGranted]
                                                ,[LoanDate]
                                                ,[TermsOfPayment]
                                                ,[Amortization]
                                                ,[Status]
                                                ,[Brand]
                                                ,[Terms]
                                                ,[Particular]
                                                ,[LCPPrice]
                                                ,[SpotCashAmount]
                                                ,[DownPayment])
                                            VALUES
                                                ( '" + _Employee + @"' 
                                                , '" + _AccountCode + @"' 
                                                , '" + _LoanRefNo + @"' 
                                                , '" + _StartOfDeduction + @"' 
                                                , CASE WHEN '" + _LoanAmount + @"' = '' THEN 0 ELSE " + _LoanAmount + @" END
                                                , CASE WHEN '" + _AmountGranted + @"' = '' THEN 0 ELSE " + _AmountGranted + @" END
                                                , '" + _LoanDate + @"' 
                                                , '" + _TermsOfPayment + @"' 
                                                , CASE WHEN '" + _Amortization + @"' = '' THEN 0 ELSE " + _Amortization + @" END
                                                , '" + _Status + @"' 
                                                , '" + _Brand + @"' 
                                                , '" + _Terms + @"' 
                                                , '" + _Particular + @"' 
                                                , CASE WHEN '" + _LCPPrice + @"' = '' THEN 0 ELSE '" + _LCPPrice + @"' END
                                                , CASE WHEN '" + _SpotCashAmount + @"' = '' THEN 0 ELSE '" + _SpotCashAmount + @"' END
                                                , CASE WHEN '" + _DownPayment + @"' = '' THEN 0 ELSE '" + _DownPayment + @"' END)

                                  ";

                        break;

                    case "Update":

                        if (_Rebate == "")
                        {
                            _Rebate = "0";
                        }
                        if (_LoanInterest == "")
                        {
                            _LoanInterest = "0";
                        }
                        if (_Amortization == "")
                        {
                            _Amortization = "0";
                        }
                        if (_LCPPrice == "")
                        {
                            _LCPPrice = "0";
                        }
                        if (_SpotCashAmount == "")
                        {
                            _SpotCashAmount = "0";
                        }
                        if (_DownPayment == "")
                        {
                            _DownPayment = "0";
                        }



                        _SQLExecute = @"

                                                        UPDATE A
                                                           SET [Amortization] = '" + _Amortization + @"' 
                                                              ,[Brand] = '" + _Brand + @"' 
                                                              ,[Terms] = '" + _Terms + @"' 
                                                              ,[Particular] = '" + _Particular.Replace("'","''") + @"' 
                                                              ,[LCPPrice] = '" + _LCPPrice + @"' 
                                                              ,[SpotCashAmount] = '" + _SpotCashAmount + @"' 
                                                              ,[DownPayment] = '" + _DownPayment + @"' 
                                                              ,[Rebate] = '" +  _Rebate + @"' 
                                                              ,[RebateApplication] = '" + _RebateApplication + @"' 
                                                              ,[LoanInterest] = '" + _LoanInterest + @"' 
                                                              ,[UpdateDate] = '" + DateTime.Today + @"' 
                                                         FROM [LoanFile] A
                                                         WHERE [EmployeeNo] =  '" + _Employee + @"' 
                                                              AND [AccountCode] =  '" + _AccountCode + @"' 
                                                              AND [LoanRefNo] =  '" + _LoanRefNo + @"' 
                                                     ";

                        break;
                }



                try
                {
                    if (_ConCompany != "")
                    {
                        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLExecute);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed in uploading data : " + ex.Message + " : " + _Employee + " : " + _EmployeeName + " : " + _LoanRefNo + " ");
                }



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

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}