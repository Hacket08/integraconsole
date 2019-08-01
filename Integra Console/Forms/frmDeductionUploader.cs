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




public partial class frmDeductionUploader : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();
    public frmDeductionUploader()
    {
        InitializeComponent();
    }

    private void frmDeductionUploader_Load(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT [PayrollPeriod],[DateOne],[DateTwo],[IsLocked] FROM  [vwsPayrollPeriod] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboPayrolPeriod.Items.Clear();
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPayrolPeriod.Items.Add(row[0].ToString());

        }
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
            if(!row["TABLE_NAME"].ToString().Contains("#"))
            {
                cmbWorkSheet.Items.Add(row["TABLE_NAME"].ToString());
            }

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

        MyDataAdapte = new OleDbDataAdapter("SELECT '' AS EmpNo,'' AS EmpName,'' AS Company,'' AS Category,* FROM [" + cmbWorkSheet.Text + "]", MyConnection);
        DtSet = new DataSet();
        MyDataAdapte.Fill(DtSet);
        _DataTable = DtSet.Tables[0];
        MyConnection.Close();

        _DataList = _DataTable;
        clsFunctions.DataGridViewSetup(dataGridView1, _DataTable, "Deduction");

        cmbColumn.Items.Clear();
        foreach (DataColumn Column in _DataList.Columns)
        {
            cmbColumn.Items.Add(Column.ColumnName.ToString());
        }

        //double _Count = 0;
        double _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            try
            {
                Application.DoEvents();
                string sEmpNo = row[5].ToString();

                if (sEmpNo != "")
                {
                    //string _sqlSelect = @"SELECT A.EmployeeNo FROM Employees A WHERE A.EmployeeNo = '" + sEmpNo + "'";
                    string _sqlSelect = @"SELECT Z.EmployeeNo, Z.EmployeeName, Z.Company,Z.Category,Z.AttendanceExempt FROM  vwsEmployees Z
                                                    WHERE dbo.[RemoveNonAlphaCharacters](Z.EmployeeName) LIKE '%' + dbo.[RemoveNonAlphaCharacters]('" + sEmpNo + @"') + '%'

                                                    ORDER BY CAST(Z.EmployeeNo as int)";
                    DataTable _table = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);

                    string sEmployee = clsSQLClientFunctions.GetData(_table, "EmployeeNo", "0");
                    string sEmployeeName = clsSQLClientFunctions.GetData(_table, "EmployeeName", "0");

                    row[0] = sEmployee;
                    row[1] = sEmployeeName;
                    row[2] = clsSQLClientFunctions.GetData(_table, "Company", "0");
                    row[3] = clsSQLClientFunctions.GetData(_table, "AttendanceExempt", "0");
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

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lblBrowseLoanAccountCode_Click(object sender, EventArgs e)
    {
        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "AccountList";
        frmDataList.ShowDialog();

        txtAccountCode.Text = frmDataList._gAccountCode;
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        if (cboPayrolPeriod.Text == "")
        {
            MessageBox.Show("Please select payroll period");
            return;
        }
        //if (cmbColumn.Text == "")
        //{
        //    MessageBox.Show("Please select Column");
        //    return;
        //}
        //if (txtAccountCode.Text == "")
        //{
        //    MessageBox.Show("Please select Account");
        //    return;
        //}

        foreach (DataRow row in _DataList.Rows)
        {
            string _Employee = row[0].ToString();
            string _EmployeeName = row[1].ToString();
            string _Company = row[2].ToString();
            string _Category = row[3].ToString().Trim();
            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);


            #region Upload Read Data

            if (_Employee != "")
            {

                string _sqlDeleteEmp = @"DELETE FROM PayrollDetails  WHERE EmployeeNo = '" + _Employee + @"' AND PayrollPeriod = '" + cboPayrolPeriod.Text + "'";
                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlDeleteEmp);


                string _sqlSelectEmp = @"SELECT A.Department, B.BCode FROM vwsEmployees A INNER JOIN vwsDepartmentList B ON A.Department = B.DepartmentCode
                                                       WHERE A.EmployeeNo = '" + _Employee + @"'";
                string _Branch = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlSelectEmp, "BCode");
                string _Department = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlSelectEmp, "Department");

                string _AccountCode = "";
                string _Amount = "0.00";
                double parsedValue;

                #region Accounts Receivable 9-523
                if (txtARCode.Text != "")
                {
                    _AccountCode = "9-523";
                    _Amount = row[txtARCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }

                #endregion
                #region Cash Advance 8-513
                if (txtCACode.Text != "")
                {
                    _AccountCode = "8-513";
                    _Amount = row[txtCACode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Appliance Loan 8-510
                if (txtAPPLCOde.Text != "")
                {
                    _AccountCode = "8-510";
                    _Amount = row[txtAPPLCOde.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Motorcycle Loan 8-511
                if (txtMOTRCode.Text != "")
                {
                    _AccountCode = "8-511";
                    _Amount = row[txtMOTRCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Agora Lending 8-512
                if (txtALCode.Text != "")
                {
                    _AccountCode = "8-512";
                    _Amount = row[txtALCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Furniture Loan 8-514
                if (txtFURNCode.Text != "")
                {
                    _AccountCode = "8-514";
                    _Amount = row[txtFURNCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Cellphone Loan 8-515
                if (txtCELPCode.Text != "")
                {
                    _AccountCode = "8-515";
                    _Amount = row[txtCELPCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Power Products Loan 8-517
                if (txtPPROCode.Text != "")
                {
                    _AccountCode = "8-517";
                    _Amount = row[txtPPROCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Computer Loan 8-518
                if (txtCOMPCode.Text != "")
                {
                    _AccountCode = "8-518";
                    _Amount = row[txtCOMPCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Spare Parts 8-519
                if (txtSPCode.Text != "")
                {
                    _AccountCode = "8-519";
                    _Amount = row[txtSPCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Car Loan 8-520
                if (txtCLCode.Text != "")
                {
                    _AccountCode = "8-520";
                    _Amount = row[txtCLCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion


                #region Modified Pag-Ibig II 8-521
                if (txtMP2Code.Text != "")
                {
                    _AccountCode = "8-521";
                    _Amount = row[txtMP2Code.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region SSS Loan 8-502
                if (txtSSSLCode.Text != "")
                {
                    _AccountCode = "8-502";
                    _Amount = row[txtSSSLCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Pag-Ibig Loan 8-504
                if (txtPIBIGLCode.Text != "")
                {
                    _AccountCode = "8-504";
                    _Amount = row[txtPIBIGLCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region Calamity Loan 8-516
                if (txtCALMLCode.Text != "")
                {
                    _AccountCode = "8-516";
                    _Amount = row[txtCALMLCode.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion

                #region SSS Cont 004
                if (txtSSS.Text != "")
                {
                    _AccountCode = "004";
                    _Amount = row[txtSSS.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region PAG-IBIG 009
                if (txtPAGIBIG.Text != "")
                {
                    _AccountCode = "009";
                    _Amount = row[txtPAGIBIG.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region PHILHEALTH Cont 007
                if (txtPHILHEALTH.Text != "")
                {
                    _AccountCode = "007";
                    _Amount = row[txtPHILHEALTH.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion
                #region WTAX Cont 011
                if (txtWTAX.Text != "")
                {
                    _AccountCode = "011";
                    _Amount = row[txtWTAX.Text].ToString().Trim();
                    if (double.TryParse(_Amount, out parsedValue) == true)
                    {
                        if (double.Parse(_Amount) != 0)
                        {
                            clsFunctions.InsertDetails(_Company
                                           , cboPayrolPeriod.Text
                                           , _Employee
                                           , _AccountCode
                                           , ""
                                           , 0
                                           , double.Parse(_Amount)
                                           , 0
                                           , 0
                                           , 0
                                           , _Branch
                                           , _Department);
                        }
                    }
                }
                #endregion

            }

            #endregion

        }
        MessageBox.Show("Data Uploaded");
    }
}
