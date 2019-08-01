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




public partial class frmWorkingHours : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();
    public frmWorkingHours()
    {
        InitializeComponent();
    }

    private void frmWorkingHours_Load(object sender, EventArgs e)
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
            if (!row["TABLE_NAME"].ToString().Contains("#"))
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
        _DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        if (_DataTable == null)
        {
            return;
        }

        DataSet DtSet;
        OleDbDataAdapter MyDataAdapte;

        MyDataAdapte = new OleDbDataAdapter("SELECT '' AS EmpNo,'' AS Company,'' AS Category,* FROM [" + cmbWorkSheet.Text + "]", MyConnection);
        DtSet = new DataSet();
        MyDataAdapte.Fill(DtSet);
        _DataTable = DtSet.Tables[0];
        MyConnection.Close();

        _DataList = _DataTable;
        clsFunctions.DataGridViewSetup(dataGridView1, _DataTable);

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
                    string _sqlSelect = @"SELECT Z.EmployeeNo, Z.lastname, Z.FirstName, Z.MiddleName, Z.Company,Z.Category,Z.AttendanceExempt FROM (
                                                    " + _SQLEmployeeList + @"
                                                    ) Z
                                                    WHERE Z.EmployeeNo = '" + sEmpNo + @"'

                                                    ORDER BY CAST(Z.EmployeeNo as int)";
                    DataTable _table = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _sqlSelect);
                    string sEmployee = clsSQLClientFunctions.GetData(_table, "EmployeeNo", "0");

                    row[0] = sEmployee;
                    row[1] = clsSQLClientFunctions.GetData(_table, "Company", "0");
                    row[2] = clsSQLClientFunctions.GetData(_table, "AttendanceExempt", "0");
                }

            }
            catch
            {
            }
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

        double parsedValue;

        foreach (DataRow row in _DataList.Rows)
        {
            string _Employee = row[0].ToString();
            string _Company = row[1].ToString();
            string _Category = row[2].ToString().Trim();

            string sNoOfHrs;
            string sNoOfMins;
            string sTotalHours;
            string sTotalDays;

            double dblRegular = 0;
            double dblAbsences = 0;
            double dblTardiness = 0;
            double dblUnderTime = 0;
            double dblRegularOT = 0;
            double dblSundayOT = 0;
            double dblSpecialHoliday = 0;
            double dblLegalHoliday = 0;
            double dblLeave = 0;
            string strLeaveType = "";


            #region Time Conversion

            if (_Category == "0")
            {
                if (_Employee != "")
                {
                    string _SQLSyntax;
                    _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.CompanyCode = '" + _Company + "'";
                    _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

                    foreach (DataRow rowDB in _CompanyList.Rows)
                    {
                        clsDeclaration.sServer = rowDB[3].ToString();
                        clsDeclaration.sCompany = rowDB[4].ToString();
                        clsDeclaration.sUsername = rowDB[5].ToString();
                        clsDeclaration.sPassword = rowDB[6].ToString();

                        clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                           clsDeclaration.sServer, clsDeclaration.sCompany,
                                           clsDeclaration.sUsername, clsDeclaration.sPassword
                                        );
                    }

                    string _SQLInsert;
                    _SQLInsert = @"
                                    DELETE FROM [PayrollEntry] WHERE [EmployeeNo] = '" + _Employee + @"' AND [PayrollPeriod] = '" + cboPayrolPeriod.Text + @"'
                                    DELETE FROM [PayrollTrans01] WHERE [EmployeeNo] = '" + _Employee + @"' AND [PayrollPeriod] = '" + cboPayrolPeriod.Text + @"'


									INSERT INTO [PayrollEntry]
												([PayrollPeriod]
                                                  ,[EmployeeNo])
												VALUES
												(
												'" + cboPayrolPeriod.Text + @"',
												'" + _Employee + @"'
												)

                                     ";

                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLInsert);

                    string _valueRead = "";
                    string _Account = "";
                    string cellIndex;
                    string timeFormat;

                    //timeFormat = ConfigurationManager.AppSettings["timeRegularDay"];
                    //cellIndex = ConfigurationManager.AppSettings["excelRegularDay"];
                    //_Account = ConfigurationManager.AppSettings["RegularDay"];
                    timeFormat = clsFunctions.SystemSettingValue("13");
                    cellIndex = clsFunctions.SystemSettingValue("12");
                    _Account = clsFunctions.SystemSettingValue("11");

                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();

                        clsFunctions.DataTimeConversion(_valueRead, timeFormat, out sNoOfHrs, out sNoOfMins, out sTotalHours, out sTotalDays);
                        double.TryParse(sTotalDays,out dblRegular);


                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }

                    timeFormat = clsFunctions.SystemSettingValue("16");
                    cellIndex = clsFunctions.SystemSettingValue("15");
                    _Account = clsFunctions.SystemSettingValue("14");
                    //timeFormat = ConfigurationManager.AppSettings["timeRegularOvertime"];
                    //cellIndex = ConfigurationManager.AppSettings["excelRegularOvertime"];
                    //_Account = ConfigurationManager.AppSettings["RegularOvertime"];
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();

                        clsFunctions.DataTimeConversion(_valueRead, timeFormat, out sNoOfHrs, out sNoOfMins, out sTotalHours, out sTotalDays);
                        double.TryParse(sTotalDays, out dblRegularOT);


                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }
                    timeFormat = clsFunctions.SystemSettingValue("19");
                    cellIndex = clsFunctions.SystemSettingValue("18");
                    _Account = clsFunctions.SystemSettingValue("17");
                    //timeFormat = ConfigurationManager.AppSettings["timeLeaveWithoutPay"];
                    //cellIndex = ConfigurationManager.AppSettings["excelLeaveWithoutPay"];
                    //_Account = ConfigurationManager.AppSettings["LeaveWithoutPay"];
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();

                        clsFunctions.DataTimeConversion(_valueRead, timeFormat, out sNoOfHrs, out sNoOfMins, out sTotalHours, out sTotalDays);
                        double.TryParse(sTotalDays, out dblAbsences);

                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }
                    timeFormat = clsFunctions.SystemSettingValue("22");
                    cellIndex = clsFunctions.SystemSettingValue("21");
                    _Account = clsFunctions.SystemSettingValue("20");
                    //timeFormat = ConfigurationManager.AppSettings["timeTardiness"];
                    //cellIndex = ConfigurationManager.AppSettings["excelTardiness"];
                    //_Account = ConfigurationManager.AppSettings["Tardiness"];
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();

                        clsFunctions.DataTimeConversion(_valueRead, timeFormat, out sNoOfHrs, out sNoOfMins, out sTotalHours, out sTotalDays);
                        double.TryParse(sTotalDays, out dblTardiness);


                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }

                    timeFormat = clsFunctions.SystemSettingValue("25");
                    cellIndex = clsFunctions.SystemSettingValue("24");
                    _Account = clsFunctions.SystemSettingValue("23");
                    //timeFormat = ConfigurationManager.AppSettings["timeUnderTime"];
                    //cellIndex = ConfigurationManager.AppSettings["excelUnderTime"];
                    //_Account = ConfigurationManager.AppSettings["UnderTime"];
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();

                        clsFunctions.DataTimeConversion(_valueRead, timeFormat, out sNoOfHrs, out sNoOfMins, out sTotalHours, out sTotalDays);
                        double.TryParse(sTotalDays, out dblUnderTime);

                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }
                    timeFormat = clsFunctions.SystemSettingValue("31");
                    cellIndex = clsFunctions.SystemSettingValue("30");
                    _Account = clsFunctions.SystemSettingValue("29");
                    //timeFormat = ConfigurationManager.AppSettings["timeLegalHolidayWork"];
                    //cellIndex = ConfigurationManager.AppSettings["excelLegalHolidayWork"];
                    //_Account = ConfigurationManager.AppSettings["LegalHolidayWork"];
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();

                        clsFunctions.DataTimeConversion(_valueRead, timeFormat, out sNoOfHrs, out sNoOfMins, out sTotalHours, out sTotalDays);
                        double.TryParse(sTotalDays, out dblLegalHoliday);


                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }
                    timeFormat = clsFunctions.SystemSettingValue("34");
                    cellIndex = clsFunctions.SystemSettingValue("33");
                    _Account = clsFunctions.SystemSettingValue("32");
                    //timeFormat = ConfigurationManager.AppSettings["timeLegalHolidayExcess"];
                    //cellIndex = ConfigurationManager.AppSettings["excelLegalHolidayExcess"];
                    //_Account = ConfigurationManager.AppSettings["LegalHolidayExcess"];



                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();


                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }
                    timeFormat = clsFunctions.SystemSettingValue("37");
                    cellIndex = clsFunctions.SystemSettingValue("36");
                    _Account = clsFunctions.SystemSettingValue("35");
                    //timeFormat = ConfigurationManager.AppSettings["timeSpecialHolidayWork"];
                    //cellIndex = ConfigurationManager.AppSettings["excelSpecialHolidayWork"];
                    //_Account = ConfigurationManager.AppSettings["SpecialHolidayWork"];
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();
                        clsFunctions.DataTimeConversion(_valueRead, timeFormat, out sNoOfHrs, out sNoOfMins, out sTotalHours, out sTotalDays);
                        double.TryParse(sTotalDays, out dblSpecialHoliday);

                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }
                    timeFormat = clsFunctions.SystemSettingValue("40");
                    cellIndex = clsFunctions.SystemSettingValue("39");
                    _Account = clsFunctions.SystemSettingValue("38");
                    //timeFormat = ConfigurationManager.AppSettings["timeSpecialHolidayExcess"];
                    //cellIndex = ConfigurationManager.AppSettings["excelSpecialHolidayExcess"];
                    //_Account = ConfigurationManager.AppSettings["SpecialHolidayExcess"];
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();

                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }
                    timeFormat = clsFunctions.SystemSettingValue("43");
                    cellIndex = clsFunctions.SystemSettingValue("42");
                    _Account = clsFunctions.SystemSettingValue("41");
                    //timeFormat = ConfigurationManager.AppSettings["timeRestdayWork"];
                    //cellIndex = ConfigurationManager.AppSettings["excelRestdayWork"];
                    //_Account = ConfigurationManager.AppSettings["RestdayWork"];
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();
                        clsFunctions.DataTimeConversion(_valueRead, timeFormat, out sNoOfHrs, out sNoOfMins, out sTotalHours, out sTotalDays);
                        double.TryParse(sTotalDays, out dblSundayOT);


                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }


                    timeFormat = clsFunctions.SystemSettingValue("46");
                    cellIndex = clsFunctions.SystemSettingValue("45");
                    _Account = clsFunctions.SystemSettingValue("44");
                    //timeFormat = ConfigurationManager.AppSettings["timeRestdayExcess"];
                    //cellIndex = ConfigurationManager.AppSettings["excelRestdayExcess"];
                    //_Account = ConfigurationManager.AppSettings["RestdayExcess"];
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();

                        if (double.TryParse(_valueRead, out parsedValue) == true)
                        {
                            if (double.Parse(_valueRead) != 0)
                            {
                                clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                            }
                        }
                    }


                    _Account = clsFunctions.SystemSettingValue("47");
                    cellIndex = clsFunctions.SystemSettingValue("48");
                    timeFormat = clsFunctions.SystemSettingValue("49");
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();
                        clsFunctions.DataTimeConversion(_valueRead, timeFormat, out sNoOfHrs, out sNoOfMins, out sTotalHours, out sTotalDays);
                        double.TryParse(sTotalDays, out dblLeave);
                    }




                    _Account = clsFunctions.SystemSettingValue("59");
                    cellIndex = clsFunctions.SystemSettingValue("60");
                    timeFormat = clsFunctions.SystemSettingValue("61");
                    if (cellIndex != "")
                    {
                        _valueRead = row[int.Parse(cellIndex)].ToString();
                        strLeaveType = _valueRead;
                    }



                    //Leave Uploading
                    //timeFormat = clsFunctions.SystemSettingValue("46");
                    //cellIndex = clsFunctions.SystemSettingValue("45");
                    //_Account = clsFunctions.SystemSettingValue("44");
                    ////timeFormat = ConfigurationManager.AppSettings["timeRestdayExcess"];
                    ////cellIndex = ConfigurationManager.AppSettings["excelRestdayExcess"];
                    ////_Account = ConfigurationManager.AppSettings["RestdayExcess"];
                    //if (cellIndex != "")
                    //{
                    //    _valueRead = row[int.Parse(cellIndex)].ToString();

                    //    if (double.TryParse(_valueRead, out parsedValue) == true)
                    //    {
                    //        if (double.Parse(_valueRead) != 0)
                    //        {
                    //            clsFunctions.InsertTimeRecord(_valueRead, _Employee, _Account, timeFormat, cboPayrolPeriod.Text, clsDeclaration.sCompanyConnection);
                    //        }
                    //    }
                    //}




                    string _sqlList = "";
                    _sqlList = @"SELECT A.EmployeeName, A.DailyRate, A.Company, A.BCode, A.Department, A.EmploymentStat FROM vwsEmployeeDetails A WHERE A.EmployeeNo = '" + _Employee + @"'";
                    string _EmployeeName = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "EmployeeName");
                    string _DailyRate = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "DailyRate");
                    string _BCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "BCode");
                    string _Department = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Department");
                    string _EmploymentStat = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "EmploymentStat");
                    string _EECompany = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Company");



                    _sqlList = @"

                                    DELETE FROM [WorkDaysDetails] WHERE [EmployeeNo] = '" + _Employee + @"' AND [PayrollPeriod] = '" + cboPayrolPeriod.Text + @"'

INSERT INTO [dbo].[WorkDaysDetails]
           ([EmployeeNo]
           ,[EmployeeName]
           ,[Company]
           ,[Branch]
           ,[Department]
           ,[EmpStatus]
           ,[DailyRate]
           ,[PayrollPeriod]
           ,[Category]
           ,[Regular]
           ,[Absences]
           ,[Tardiness]
           ,[UnderTime]
           ,[RegularOT]
           ,[SundayOT]
           ,[SpecialHoliday]
           ,[LegalHoliday]
           ,[Leave]
           ,[LeaveCode])
     VALUES
           ('" + _Employee + @"'
           ,'" + _EmployeeName + @"'
           ,'" + _EECompany + @"'
           ,'" + _BCode + @"'
           ,'" + _Department + @"'
           ,'" + _EmploymentStat + @"'
           ,'" + _DailyRate + @"'
           ,'" + cboPayrolPeriod.Text + @"'
           ,'" + _Category + @"'

           ,'" + dblRegular + @"'
           ,'" + dblAbsences + @"'
           ,'" + dblTardiness + @"'
           ,'" + dblUnderTime + @"'
           ,'" + dblRegularOT + @"'
           ,'" + dblSundayOT + @"'
           ,'" + dblSpecialHoliday + @"'
           ,'" + dblLegalHoliday + @"'
           ,'" + dblLeave + @"'
           ,'" + strLeaveType + @"'

           )
    
                        ";


                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlList);

                }
            }
            #endregion



        }
        MessageBox.Show("Data Uploaded");
    }



    private void WorkDaysUpload(string sPayrollPeriod, string sEmployeeNo, string sAccountCode, string sNoOfHrs, string sNoOfMins)
    {
        string _SQLInsert = @"

									INSERT INTO [PayrollTrans01]
												([PayrollPeriod]
												,[EmployeeNo]
												,[AccountCode]
                                                ,[NoOfHrs]
                                                ,[NoOfMins]
                                                )
												VALUES
												(
												'" + sPayrollPeriod + @"',
												'" + sEmployeeNo + @"',
												'" + sAccountCode + @"',
												'" + sNoOfHrs + @"',
												'" + sNoOfMins + @"'
												)

                                     ";
        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLInsert);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}