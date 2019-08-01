using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

using System.IO;

public partial class frmLogFiles : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();

    public frmLogFiles()
    {
        InitializeComponent();
    }

    private void frmLogFiles_Load(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        if (cboCompany.Text == "")
        {
            MessageBox.Show("Company Not Define.");
            return;
        }

        ofdExcel.InitialDirectory = "c:\\";
        ofdExcel.Filter = "Attendance Log files (*.csv)|*.csv";
        ofdExcel.FilterIndex = 1;

        DialogResult result = ofdExcel.ShowDialog();
        if (result == DialogResult.OK)
        {
            txtExcelFile.Text = ofdExcel.FileName;
        }

        if (result == DialogResult.Cancel)
        {
            return;
        }

        DataTable _DisplayTable;
        _DisplayTable = GetDataTabletFromCSVFile(txtExcelFile.Text);
        _DataList = _DisplayTable;
        clsFunctions.DataGridViewSetup(dataGridView1, _DisplayTable);
        
        //string _Display;
        //_Display = "SELECT * FROM (";

        //int _Count = _DisplayTable.Rows.Count;
        //int i = 0;
        //foreach (DataRow row in _DisplayTable.Rows)
        //{
        //    string _Employee = row[0].ToString();
        //    string _Date = row[1].ToString();
        //    string _Time = row[2].ToString();
        //    string _Type = row[3].ToString();

        //    string _SQLGetSyntax;
        //    DataTable _GetDataTable;

        //    _SQLGetSyntax = @"SELECT CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName] FROM Employees A WHERE A.EmployeeNo = '" + _Employee + "'";
        //    _GetDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLGetSyntax);
        //    string _EmployeeName = clsSQLClientFunctions.GetData(_GetDataTable, "EmployeeName", "0");


        //    _Display = _Display + @"
        //                                SELECT '" + _Employee + "' AS [EMPLOYEE CODE],'" + _EmployeeName + "' AS [EMPLOYEE NAME],'" + _Date + "' AS [DATE],'" + _Date + " " + _Time + "' AS [TIME],'" + _Type + @"' [TYPE]
        //                                ";

        //    i++;

        //    if (i != _Count)
        //    {
        //        _Display = _Display + @"UNION ALL
        //                                ";
        //    }
        //}



        //_Display = _Display + ") Z";
        //DataTable _Table;
        //_Table = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Display);
        //clsFunctions.DataGridViewSetup(dataGridView1, _Table);
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
    }

    private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
    {
        DataTable csvData = new DataTable();

        try
        {

            using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();
                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn);
                }

                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }
                    csvData.Rows.Add(fieldData);
                }
            }
        }
        catch
        {
        }
        return csvData;
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        foreach (DataRow row in _DataList.Rows)
        {
            string _Syntax = "";
            _Syntax = "INSERT INTO [DailyTrans] ([EmployeeNo],[TransType],[TransDate],[TransTime],[TransType02]) VALUES ('" + row[0].ToString() + "','" + row[3].ToString() + "','" + row[1].ToString() + "','" + row[1].ToString() + " " + row[2].ToString() + "','" + row[3].ToString() + "')";


            string _SyntaxCheck = "";
            _SyntaxCheck = "SELECT 'TRUE' FROM [DailyTrans] WHERE [EmployeeNo] = '" + row[0].ToString() + "' and [TransTime] = '" + row[1].ToString() + " " + row[2].ToString() + "'";
            DataTable _DataTableCheck;
            _DataTableCheck = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SyntaxCheck);

            if(_DataTableCheck.Rows.Count == 0)
            {
                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _Syntax);
            }
        }

        MessageBox.Show("Done");
    }

    private void btnLocate_Click(object sender, EventArgs e)
    {
        if (txtEmployee.Text == "")
        {
            MessageBox.Show("Please Identify Employee");
            return;
        }
        
        string _DownloadPath = @"D:\DES\BACKUP\";
        
        System.IO.DirectoryInfo di = new DirectoryInfo(_DownloadPath);
        foreach (FileInfo file in di.GetFiles())
        {
            try
            {
                string _DataFile = Path.Combine(_DownloadPath, file.Name);

                DataTable _DisplayTable;
                _DisplayTable = GetDataTabletFromCSVFile(_DataFile);

                foreach (DataRow row in _DisplayTable.Rows)
                {
                    try
                    {
                        if(row[0].ToString() == txtEmployee.Text)
                        {
                            MessageBox.Show("Data Found @ " + _DataFile);
                        }
                    }
                    catch 
                    {
                    }
                }

            }
            catch
            {
            }
        }
    }
}
