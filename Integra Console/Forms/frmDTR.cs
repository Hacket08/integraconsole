using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;


public partial class frmDTR : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();

    
    private static DataTable dtRecordList = new DataTable();
    private static DataTable dtDataTimeRecord = new DataTable();
    

    private static string sEmployeeNo;
    private static string sEmployeeName;
    private static string sTransDate;
    public frmDTR()
    {
        InitializeComponent();
    }

    private void frmDTR_Load(object sender, EventArgs e)
    {
        btnLocked.Enabled = false;
        btnUnlocked.Enabled = false;

        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT A.Area FROM [OBLP] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }
    }

    private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                sEmployeeNo = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                sEmployeeName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                sTransDate = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }
        catch
        {

        }

    }
    private void btnUpdatedDTR_Click(object sender, EventArgs e)
    {
        string _sysDB = ConfigurationManager.AppSettings["DBName"];
        string _sysDBServer = ConfigurationManager.AppSettings["DBServer"];

        string _Branch;
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }


        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, "DELETE FROM [DailyTimeDetails] WHERE [TransDate] = '" + dtpFrom.Text + @"'");
        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, "DELETE FROM [DailyTransLogs] WHERE [TransDate] = '" + dtpFrom.Text + @"'");

        string sCompanySQL;
        DataTable dtCompanyTable;

        sCompanySQL = @"SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
        dtCompanyTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, sCompanySQL);

        foreach (DataRow row in dtCompanyTable.Rows)
        {
            clsDeclaration.sServer = row[3].ToString();
            clsDeclaration.sCompany = row[4].ToString();
            clsDeclaration.sUsername = row[5].ToString();
            clsDeclaration.sPassword = row[6].ToString();

            clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                           clsDeclaration.sServer, clsDeclaration.sCompany,
                                           clsDeclaration.sUsername, clsDeclaration.sPassword
                                        );

            if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == true)
            {

                // Download Daily Time Record

                string sTransData;
                DataTable dtTransData;

                sTransData = @"
                                        SELECT A.[EmployeeNo]
                                              ,A.[TransType]
                                              ,A.[TransDate]
                                              ,A.[TransTime]
                                              ,A.[TransType02]
                                              ,A.[Remarks]  
                                              ,LEFT(C.DepartmentDesc,8) AS BranchCode
                                          FROM [" + _sysDBServer + @"].[" + _sysDB + @"].dbo.[DailyTrans] A INNER JOIN [" + clsDeclaration.sServer + @"].[" + clsDeclaration.sCompany + @"].dbo.[Employees] B ON A.EmployeeNo = B.EmployeeNo
										  INNER JOIN  [" + clsDeclaration.sServer + @"].[" + clsDeclaration.sCompany + @"].dbo.[Department] C ON B.Department = C.DepartmentCode
                                          WHERE A.[TransDate] = '" + dtpFrom.Text + @"' AND LEFT(C.DepartmentDesc,8) LIKE '%" + _Branch + @"%' 
                                                AND (SELECT Z.Area FROM  [" + clsDeclaration.sServer + @"].[" + clsDeclaration.sCompany + @"].dbo.usr_Branches Z WHERE CONCAT(Z.Company,Z.Code) = LEFT(C.DepartmentDesc,8)) LIKE '%" + cboArea.Text + @"%'
                              ";
                dtTransData = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, sTransData);

                int i = 0;
                double _RowCount = dtTransData.Rows.Count;
                foreach (DataRow row1 in dtTransData.Rows)
                {
                    string sInsertSQL;
                    sInsertSQL = @"
                                    
                                        INSERT INTO [dbo].[DailyTransLogs]
                                        (
                                             [EmployeeNo]
                                            ,[TransType]
                                            ,[TransDate]
                                            ,[TransTime]
                                            ,[TransType02]
                                            ,[Remarks]
                                            ,[Branch]
                                        )
                                        VALUES
                                        (
                                             CASE WHEN '" + row1[0].ToString() + @"' = '' THEN NULL ELSE '" + row1[0].ToString() + @"' END
                                            ,CASE WHEN '" + row1[1].ToString() + @"' = '' THEN NULL ELSE '" + row1[1].ToString() + @"' END
                                            ,CASE WHEN '" + row1[2].ToString() + @"' = '' THEN NULL ELSE '" + row1[2].ToString() + @"' END
                                            ,CASE WHEN '" + row1[3].ToString() + @"' = '' THEN NULL ELSE '" + row1[3].ToString() + @"' END
                                            ,CASE WHEN '" + row1[4].ToString() + @"' = '' THEN NULL ELSE '" + row1[4].ToString() + @"' END
                                            ,CASE WHEN '" + row1[5].ToString().Replace("'", "''") + @"' = '' THEN NULL ELSE '" + row1[5].ToString().Replace("'", "''") + @"' END
                                            ,CASE WHEN '" + row1[6].ToString() + @"' = '' THEN NULL ELSE '" + row1[6].ToString() + @"' END
                                        )
                                  ";



                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, sInsertSQL);



                    Application.DoEvents();
                    i++;
                    tssDataStatus.Text = "Reading Data : (" + i + " / " + _RowCount + ") : Data Progress ( " + Math.Round(((i / _RowCount) * 100), 2) + " % ) : " + row1[0].ToString();
                    pbDataProgress.Value = Convert.ToInt32(((i / _RowCount) * 100));
                }




                // Daily Time Record Preparation
                string sTransRecord;
                DataTable dtTransRecord;

                sTransRecord = @"
                                    DECLARE @Employee Nvarchar(30)
                                    DECLARE @Branch Nvarchar(30)
                                    DECLARE @Area Nvarchar(30)
                                    DECLARE @Date Datetime
                                    SET @Date = '" + dtpFrom.Text + @"'
                                    SET @Branch = '" + _Branch + @"'
                                    SET @Area = '" + cboArea.Text + @"'


                                    SELECT X.EmployeeNo,X.EmployeeName, @Date AS [TransDate]
                                    ,X2.Weekdays01,X2.BreakTime01,X2.BreakTime02,X2.Weekdays02
                                    ,X.[Time In],X.[Time Out], X.BranchCode
                                    FROM (
		                                    SELECT 
		                                    A.EmployeeNo 
		                                    ,CONCAT(A.LastName, ', ', A.FirstName, ' ', A.MiddleName) AS [EmployeeName]
		                                    ,CASE WHEN ISNULL(A.ScheduleCode,'') = '' THEN (SELECT Z.ScheduleCode FROM  [" + clsDeclaration.sServer + @"].[" + clsDeclaration.sCompany + @"].dbo.Schedule Z WHERE Z.DefaultSched = 1) ELSE A.ScheduleCode END AS ScheduleCode
		                                    ,(SELECT Z.ShiftCode FROM  [" + clsDeclaration.sServer + @"].[" + clsDeclaration.sCompany + @"].dbo.ScheduleCalendarEmp Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AppDays = @Date) AS [Actual Schedule]

		                                    ,(SELECT MIN([TransTime]) FROM [" + _sysDBServer + @"].[" + _sysDB + @"].dbo.[DailyTrans] Z WHERE Z.TransDate = @Date AND Z.EmployeeNo =  A.EmployeeNo AND Z.[TransType02] = '0') AS [Time In]
		                                    ,(SELECT MAX([TransTime]) FROM [" + _sysDBServer + @"].[" + _sysDB + @"].dbo.[DailyTrans] Z WHERE Z.TransDate = @Date AND Z.EmployeeNo =  A.EmployeeNo AND Z.[TransType02] = '1') AS [Time Out]
		                                    ,LEFT(C.DepartmentDesc,8) AS BranchCode
                                            ,(SELECT Z.Area FROM  [" + clsDeclaration.sServer + @"].[" + clsDeclaration.sCompany + @"].dbo.usr_Branches Z WHERE CONCAT(Z.Company,Z.Code) = LEFT(C.DepartmentDesc,8)) AS Area
		                                    FROM  [" + clsDeclaration.sServer + @"].[" + clsDeclaration.sCompany + @"].dbo.Employees A 
		                                    INNER JOIN  [" + clsDeclaration.sServer + @"].[" + clsDeclaration.sCompany + @"].dbo.[Department] C ON A.Department = C.DepartmentCode
		                                    WHERE LEFT(C.DepartmentDesc,8) LIKE '%' + @Branch + '%' 
		                                    AND (SELECT Z.Area FROM  [" + clsDeclaration.sServer + @"].[" + clsDeclaration.sCompany + @"].dbo.usr_Branches Z WHERE CONCAT(Z.Company,Z.Code) = LEFT(C.DepartmentDesc,8))  LIKE '%' + @Area + '%' 
		                                    AND A.EmpStatus IN (0,1,2)
	                                      ) X
                                    INNER JOIN Schedule X2 ON ISNULL(X.[Actual Schedule], X.ScheduleCode) = X2.ScheduleCode
                                    ORDER BY X.EmployeeName
                                ";

                dtTransRecord = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, sTransRecord);


                i = 0;
                _RowCount = dtTransRecord.Rows.Count;
                foreach (DataRow row2 in dtTransRecord.Rows)
                {
                    string sInsertSQL;
                    sInsertSQL = @"
                                                INSERT INTO [dbo].[DailyTimeDetails]
                                                           (
                                                             [EmployeeNo],
                                                             [EmployeeName],
                                                             [TransDate],
                                                             [Weekdays01],
                                                             [BreakTime01],
                                                             [BreakTime02],
                                                             [Weekdays02],
                                                             [TimeIn],
                                                             [TimeOut],
                                                             [Branch]
                                                            )
                                                        VALUES
                                                        (
                                                          CASE WHEN '" + row2[0].ToString() + @"' = '' THEN NULL ELSE '" + row2[0].ToString() + @"' END
                                                         ,CASE WHEN '" + row2[1].ToString().Replace("'", "''") + @"' = '' THEN NULL ELSE '" + row2[1].ToString().Replace("'", "''") + @"' END
                                                         ,CASE WHEN '" + row2[2].ToString() + @"' = '' THEN NULL ELSE '" + row2[2].ToString() + @"' END
                                                         ,CASE WHEN '" + row2[3].ToString() + @"' = '' THEN NULL ELSE '" + row2[3].ToString() + @"' END
                                                         ,CASE WHEN '" + row2[4].ToString() + @"' = '' THEN NULL ELSE '" + row2[4].ToString() + @"' END
                                                         ,CASE WHEN '" + row2[5].ToString() + @"' = '' THEN NULL ELSE '" + row2[5].ToString() + @"' END
                                                         ,CASE WHEN '" + row2[6].ToString() + @"' = '' THEN NULL ELSE '" + row2[6].ToString() + @"' END
                                                         ,CASE WHEN '" + row2[7].ToString() + @"' = '' THEN NULL ELSE '" + row2[7].ToString() + @"' END
                                                         ,CASE WHEN '" + row2[8].ToString() + @"' = '' THEN NULL ELSE '" + row2[8].ToString() + @"' END
                                                         ,CASE WHEN '" + row2[9].ToString() + @"' = '' THEN NULL ELSE '" + row2[9].ToString() + @"' END
                                                        )

                                              ";

                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, sInsertSQL);

                    Application.DoEvents();
                    i++;
                    tssDataStatus.Text = "Reading Data : (" + i + " / " + _RowCount + ") : Data Progress ( " + Math.Round(((i / _RowCount) * 100), 2) + " % ) : " + row2[1].ToString().Replace("'", "''");
                    pbDataProgress.Value = Convert.ToInt32(((i / _RowCount) * 100));
                }



            }

            string sDisplayData;
            DataTable dtDisplayData;
            sDisplayData = @"SELECT 
                                         [EmployeeNo],
                                         [EmployeeName],
                                         [TransDate],
                                         [Weekdays01],
                                         [BreakTime01],
                                         [BreakTime02],
                                         [Weekdays02],
                                         [TimeIn],
                                         [TimeOut],
                                         [Branch]         
                                        FROM DailyTimeDetails WHERE [TransDate] = '" + dtpFrom.Text + @"' ORDER BY EmployeeName
                                    ";
            dtDisplayData = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, sDisplayData);
            _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, sDisplayData);
            clsFunctions.DataGridViewSetup(dataGridView1, dtDisplayData);
        }
    }




    private void button2_Click(object sender, EventArgs e)
    {
        string _Branch;
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }



        DataTable _DataTable;
        string _SQLSyntax;

        int i = 0;
        double _RowCount = _DataList.Rows.Count;
        foreach (DataRow row in _DataList.Rows)
        {
            string _EmployeeNo = row[0].ToString();
            string _TransDate = row[2].ToString();
            string _TimeIn = row[7].ToString();
            string _TimeOut = row[8].ToString();

            string _BCode  = row[9].ToString();

            _SQLSyntax = @"
                            SELECT [GroupCode]
                                  ,[EmployeeNo]
                                  ,[TransType]
                                  ,[TransDate]
                                  ,[TransTime]
                                  ,[TransType02]
                                  ,[Branch]
                              FROM [DailyTransLogs]
                              WHERE [EmployeeNo] = '" + _EmployeeNo + @"'
                              AND [TransDate] = '" + _TransDate + @"'
                              AND [TransTime] BETWEEN '" + _TimeIn + @"' AND '" + _TimeOut + @"'
                              AND [TransTime] NOT IN ('" + _TimeIn + @"','" + _TimeOut + @"')
                              AND Branch Like '%" + _BCode + @"%'
                              ORDER BY [TransTime] ASC, [TransType02] DESC
                          ";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView3, _DataTable);


            string _Delete;
            _Delete = @"DELETE FROM DailyInOut WHERE EmployeeNo = '" + _EmployeeNo + @"' AND  TransDate = '" + _TransDate + @"'";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Delete);
            
            string _TransTimeIN = "";
            string _TransTimeOUT = "";

            foreach (DataRow row1 in _DataTable.Rows)
            {
                string _Type = row1[5].ToString();

                if (_Type == "1")
                {
                    if (_TransTimeOUT != "")
                    {
                        string _Insert;
                        _Insert = @"INSERT INTO [DailyInOut] ([EmployeeNo],[TransDate],[TimeOUT],[IsDeduct],[IsType],[IsBreakTime],[TotalMins],[Branch]) 
                                    VALUES ('" + _EmployeeNo + @"', '" + _TransDate + @"', '" + _TransTimeOUT + @"','0','1','0','0', '" + _BCode + @"')";
                        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Insert);

                        _TransTimeOUT = row1[4].ToString();
                        _TransTimeIN = "";
                    }
                    else
                    {
                        _TransTimeOUT = row1[4].ToString();
                    }
                }
                
                if (_Type == "0")
                {
                    _TransTimeIN = row1[4].ToString();
                    if(_TransTimeOUT != "")
                    {
                        if(_TransTimeIN != "")
                        {




                            string _Insert;
                            _Insert = @"INSERT INTO [DailyInOut] ([EmployeeNo],[TransDate],[TimeOUT],[TimeIN],[IsDeduct],[IsType],[IsBreakTime],[TotalMins],[Branch]) 
                                    VALUES ('" + _EmployeeNo + @"', '" + _TransDate + @"', '" + _TransTimeOUT + @"', '" + _TransTimeIN + @"','0','2','0','0', '" + _BCode + @"')";
                            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Insert);

                            _TransTimeOUT = "";
                            _TransTimeIN = "";
                        }
                        else
                        {
                            string _Insert;
                            _Insert = @"INSERT INTO [DailyInOut] ([EmployeeNo],[TransDate],[TimeOUT],[IsDeduct],[IsType],[IsBreakTime],[TotalMins],[Branch]) 
                                    VALUES ('" + _EmployeeNo + @"', '" + _TransDate + @"', '" + _TransTimeOUT + @"','0','1','0','0', '" + _BCode + @"')";
                            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Insert);

                            _TransTimeOUT = "";
                            _TransTimeIN = "";
                        }
                    }
                    else
                    {
                        string _Insert;
                        _Insert = @"INSERT INTO [DailyInOut] ([EmployeeNo],[TransDate],[TimeIN],[IsDeduct],[IsType],[IsBreakTime],[TotalMins],[Branch]) 
                                    VALUES ('" + _EmployeeNo + @"', '" + _TransDate + @"', '" + _TransTimeIN + @"','0','1','0','0', '" + _BCode + @"')";
                        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Insert);

                        _TransTimeOUT = "";
                        _TransTimeIN = "";
                    }
                }


                
            }



            if (_TransTimeOUT != "" || _TransTimeIN != "")
            {
                string _SqlInsert;
                _SqlInsert = @"INSERT INTO [DailyInOut] ([EmployeeNo],[TransDate],[TimeOUT],[TimeIN],[IsDeduct],[IsType],[IsBreakTime],[TotalMins],[Branch]) 
                                    VALUES ('" + _EmployeeNo + @"', '" + _TransDate + @"', 
                                            CASE WHEN '" + _TransTimeOUT + @"' = '' THEN NULL ELSE '" + _TransTimeOUT + @"' END, 
                                            CASE WHEN '" + _TransTimeIN + @"' = '' THEN NULL ELSE '" + _TransTimeIN + @"' END,'0','1','0','0', '" + _BCode + @"')";
                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SqlInsert);
            }





            // Excel Progress Monitoring
            Application.DoEvents();
            i++;
            tssDataStatus.Text = "Reading Data : (" + i + " / " + _RowCount + ") : " + _EmployeeNo + "  : Data Progress ( " + Math.Round(((i / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((i / _RowCount) * 100));

        }


 
        _SQLSyntax = @"SELECT * FROM [DailyInOut]
                              WHERE Branch Like '%" + _Branch + @"%'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        clsFunctions.DataGridViewSetup(dataGridView1, _DataTable);
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void button3_Click(object sender, EventArgs e)
    {
        string _Branch;
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }


        string _SQLSyntax;
        DataTable _DataTable;


        DataTable _SetDataTable;
        string _SetSQLSyntax;
        _SetSQLSyntax = @"
                                SELECT 
								A.EmployeeNo
                                , B.FullName
                                , A.TransDate, A.TimeIn, 
                                 C.TimeOUT AS BTimeOUT, C.TimeIn AS BTimeIn, 
                                 A.TimeOUT
                                ,ISNULL(A.UnderApproved,'N') AS [UT Approved]
								,A.Weekdays01
, CONVERT(DATETIME, CONVERT(CHAR(8), A.TransDate, 112) + ' ' + CONVERT(CHAR(8), A.BreakTime01, 108)) BreakTime01
, CONVERT(DATETIME, CONVERT(CHAR(8), A.TransDate, 112) + ' ' + CONVERT(CHAR(8), A.BreakTime02, 108)) BreakTime02
, A.Weekdays02
, A.Comment,B.CompCode, A.Branch
                                FROM DailyTimeDetails A 
                                INNER JOIN Employees B ON A.EmployeeNo = B.EmployeeNo
                                LEFT JOIN DailyInOut C ON A.EmployeeNo = C.EmployeeNo AND A.TransDate = C.TransDate AND ISNULL(C.TimeOut,'') <> '' AND ISNULL(C.TimeIn,'') <> '' 
								WHERE A.TransDate = '" + dtpFrom.Text + @"'
                                 AND A.Branch Like '%" + _Branch + @"%' AND B.EmpStatus IN (0,1,2)
                                ORDER BY B.FullName
                          ";
        _SetDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SetSQLSyntax);




        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, "DELETE FROM DailyAttendance WHERE [TransDate] = '" + dtpFrom.Text + @"' AND Branch Like '%" + _Branch + @"%'");


        int i = 0;
        double _RowCount = _SetDataTable.Rows.Count;
        foreach (DataRow row1 in _SetDataTable.Rows)
        {
            string _BrakeTimeOutValue = "" ;
            string _TimeoutValue = "";

            string _BrakeTimeIN = row1[10].ToString();
            string _Timeout = row1[6].ToString();

            if (_Timeout != "")
            {
                if (DateTime.Parse(_BrakeTimeIN) > DateTime.Parse(_Timeout))
                {
                    _BrakeTimeOutValue = _Timeout;
                    _TimeoutValue = "";
                }
                else
                {
                    _BrakeTimeOutValue = row1[4].ToString();
                    _TimeoutValue = _Timeout;
                }
            }


            string _BrakeTimeINValue = "";
            string _TimeINValue = "";

            string _BrakeTimeOut= row1[9].ToString();
            string _TimeIn = row1[3].ToString();


            if (_TimeIn != "")
            {
                if (DateTime.Parse(_BrakeTimeOut) < DateTime.Parse(_TimeIn))
                {
                    _BrakeTimeINValue = _TimeIn;
                    _TimeINValue = "";
                }
                else
                {
                    _BrakeTimeINValue = row1[5].ToString();
                    _TimeINValue = _TimeIn;
                }
            }


            //if (_Timeout != "")
            //{
            //    if (DateTime.Parse(_BrakeTimeIN) > DateTime.Parse(_Timeout))
            //    {
            //        _BrakeTimeOutValue = _Timeout;
            //        _TimeoutValue = "";
            //    }
            //    else
            //    {
            //        _BrakeTimeOutValue = row1[4].ToString();
            //        _TimeoutValue = _Timeout;
            //    }
            //}


            string _InsertData = @"
                                       INSERT INTO DailyAttendance ([EmployeeNo]
                                                                  ,[EmployeeName]
                                                                  ,[TransDate]
                                                                  ,[TimeIn]
                                                                  ,[BTimeOut]
                                                                  ,[BTimeIn]
                                                                  ,[TimeOut]
                                                                  ,[UTApproved]
                                                                  ,[Weekdays01]
                                                                  ,[BreakTime01]
                                                                  ,[BreakTime02]
                                                                  ,[Weekdays02]
                                                                  ,[Comment], [Company], [Branch])
                                       SELECT   CASE WHEN '" + row1[0].ToString() + @"' = '' THEN NULL ELSE '" + row1[0].ToString() + @"' END
                                               ,CASE WHEN '" + row1[1].ToString().Replace("'", "''") + @"' = '' THEN NULL ELSE '" + row1[1].ToString().Replace("'", "''") + @"' END
                                               ,CASE WHEN '" + row1[2].ToString() + @"' = '' THEN NULL ELSE '" + row1[2].ToString() + @"' END
                                               ,CASE WHEN '" + _TimeINValue + @"' = '' THEN NULL ELSE '" + _TimeINValue + @"' END
                                               ,CASE WHEN '" + _BrakeTimeOutValue + @"' = '' THEN NULL ELSE '" + _BrakeTimeOutValue + @"' END
                                               ,CASE WHEN '" + _BrakeTimeINValue + @"' = '' THEN NULL ELSE '" + _BrakeTimeINValue + @"' END
                                               ,CASE WHEN '" + _TimeoutValue + @"' = '' THEN NULL ELSE '" + _TimeoutValue + @"' END
                                               ,CASE WHEN '" + row1[7].ToString() + @"' = '' THEN NULL ELSE '" + row1[7].ToString() + @"' END
                                               ,CASE WHEN '" + row1[8].ToString() + @"' = '' THEN NULL ELSE '" + row1[8].ToString() + @"' END
                                               ,CASE WHEN '" + row1[9].ToString() + @"' = '' THEN NULL ELSE '" + row1[9].ToString() + @"' END
                                               ,CASE WHEN '" + row1[10].ToString() + @"' = '' THEN NULL ELSE '" + row1[10].ToString() + @"' END
                                               ,CASE WHEN '" + row1[11].ToString() + @"' = '' THEN NULL ELSE '" + row1[11].ToString() + @"' END
                                               ,CASE WHEN '" + row1[12].ToString() + @"' = '' THEN NULL ELSE '" + row1[12].ToString() + @"' END
                                               ,CASE WHEN '" + row1[13].ToString() + @"' = '' THEN NULL ELSE '" + row1[13].ToString() + @"' END
                                               ,CASE WHEN '" + row1[14].ToString() + @"' = '' THEN NULL ELSE '" + row1[14].ToString() + @"' END
                                      ";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _InsertData);


            i++;
            // Excel Progress Monitoring
            Application.DoEvents();
            //_Count++;
            tssDataStatus.Text = "Reading Data : (" + i + " / " + _RowCount + ") : " + row1[1].ToString().Replace("'", "''") + "  : Data Progress ( " + Math.Round(((i / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((i / _RowCount) * 100));
        }




        //_SQLSyntax = @"
        //                      SELECT A.*
                                
    				//		  ,CASE WHEN RIGHT(B.Department,4) IN (SELECT Z.[Code] FROM OPST Z WHERE ISNULL(Z.[CalcBreak],'0') = '1')  AND ISNULL(A.TimeIn,'') <> '' THEN ISNULL(A.BTimeOut,A.BreakTime01) ELSE A.BTimeOut END AS [BreakOut]
							 // ,CASE WHEN RIGHT(B.Department,4) IN (SELECT Z.[Code] FROM OPST Z WHERE ISNULL(Z.[CalcBreak],'0') = '1')  AND ISNULL(A.TimeOut,'') <> '' THEN ISNULL(A.BTimeIn,A.BreakTime02) ELSE A.BTimeIn END AS [BreakIn]
        //                      ,C.Name AS Branch
        //                      ,(SELECT Z.Name FROM OPST Z WHERE Z.Code = RIGHT(B.Department,4)) AS Position
        //                      FROM DailyAttendance A 
        //                      INNER JOIN [Employees] B ON A.[EmployeeNo] = B.[EmployeeNo]
        //                      LEFT JOIN [OBLP] C ON LEFT(B.Department,8) = CONCAT(C.Company, C.Code)
        //                      WHERE A.TransDate = '" + dtpFrom.Text + @"' AND A.Branch Like '%" + _Branch + @"%'  AND B.EmpStatus IN (0,1,2)
        //                  ";





        _SQLSyntax = @"
                              SELECT A.*
                                
    						  ,CASE WHEN RIGHT(B.Department,4) IN (SELECT Z.[Code] FROM OPST Z WHERE ISNULL(Z.[CalcBreak],'0') = '1')  AND ISNULL(A.TimeIn,'') <> '' THEN ISNULL(A.BTimeOut,A.BreakTime01) ELSE A.BTimeOut END AS [BreakOut]
							  ,CASE WHEN RIGHT(B.Department,4) IN (SELECT Z.[Code] FROM OPST Z WHERE ISNULL(Z.[CalcBreak],'0') = '1')  AND ISNULL(A.TimeOut,'') <> '' THEN ISNULL(A.BTimeIn,A.BreakTime02) ELSE A.BTimeIn END AS [BreakIn]
                              ,C.Name AS BranchName
                              ,(SELECT Z.Name FROM OPST Z WHERE Z.Code = RIGHT(B.Department,4)) AS Position
                              FROM DailyAttendance A 
                              INNER JOIN [Employees] B ON A.[EmployeeNo] = B.[EmployeeNo]
                              LEFT JOIN [OBLP] C ON LEFT(B.Department,8) = CONCAT(C.Company, C.Code)
                              WHERE A.Branch LIKE '%" + _Branch + @"%' 
                              AND A.TransDate = '" + dtpFrom.Text + @"' AND B.EmpStatus IN (0,1,2)
                          ";


        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        dtRecordList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        dataGridView1.Columns.Clear();
        Application.DoEvents();
        clsFunctions.DataGridViewSetup(dataGridView1, _DataTable, "DTR");

    }


    private void DispalyDailyTimeRecord()
    {

        if (dtRecordList.Rows.Count == 0)
        {
            MessageBox.Show("No Record Found");
            return;
        }


        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, "DELETE FROM OAMR");

        string _Display;
        _Display = @"SELECT Z.[Employee No],Z.[Employee Name], Z.[Trans Date], Z.[Company],Z.[Time In],Z.[Time Out]
                    ,SUM(Z.[Work Hours]) AS [Work Hours]
                    ,SUM(Z.[Absent AM]) AS [AM]
                    ,SUM(Z.[Absent PM]) AS [PM]
                    ,SUM(Z.[Tardy AM]) AS [Tardy AM]
                    ,SUM(Z.[Tardy PM]) AS [Tardy PM]
                    ,SUM(Z.[Under AM]) AS [Under AM]
                    ,SUM(Z.[Under PM]) AS [Under PM]

					
                    ,SUM(Z.[Total Time Deduction]) AS [Total Time Deduction]
                    ,SUM(Z.[Total Work Hours]) AS [Total Work Hours]

                    ,SUM(Z.[Regular Hr]) AS [Regular Hr]
                    ,SUM(Z.[Regular Min]) AS [Regular Min]

                    ,SUM(Z.[Absent Hr]) AS [Absent Hr]
                    ,SUM(Z.[Absent Min]) AS [Absent Min]
                    ,SUM(Z.[Tardy Hr]) AS [Tardy Hr]
                    ,SUM(Z.[Tardy Min]) AS [Tardy Min]
                    ,SUM(Z.[UT Hr]) AS [UT Hr]
                    ,SUM(Z.[UT Min]) AS [UT Min]
                    ,SUM(Z.[Excess Hr]) AS [Excess Hr]
                    ,SUM(Z.[Excess Min]) AS [Excess Min]

                     FROM (";
        double _RowCount;
        int _Count = dtRecordList.Rows.Count;
        int i = 0;
        _RowCount = _Count;
        foreach (DataRow row in dtRecordList.Rows)
        {
            string _EmployeeNo = row[0].ToString();

            //if (_EmployeeNo == "2393")
            //{
            //    string _0 = "";
            //}

            string _EmployeeName = row[1].ToString();
            string _TransDate = DateTime.Parse(row[2].ToString()).ToString("MM/dd/yyyy");
            string _Company = row[21].ToString();


            string _TimeIN = row[3].ToString();
            string _BTimeOut = row[24].ToString();
            string _BTimeIN = row[25].ToString();
            string _TimeOut = row[6].ToString();

            if (_TimeIN != "") { _TimeIN = DateTime.Parse(row[3].ToString()).ToString("HH:mm tt"); }
            if (_BTimeOut != "") { _BTimeOut = DateTime.Parse(row[24].ToString()).ToString("HH:mm tt"); }
            if (_BTimeIN != "") { _BTimeIN = DateTime.Parse(row[25].ToString()).ToString("HH:mm tt"); }
            if (_TimeOut != "") { _TimeOut = DateTime.Parse(row[6].ToString()).ToString("HH:mm tt"); }

            string _SchedTimeIN = row[17].ToString();
            string _SchedBTimeOut = row[18].ToString();
            string _SchedBTimeIN = row[19].ToString();
            string _SchedTimeOut = row[20].ToString();

            if (_SchedTimeIN != "") { _SchedTimeIN = DateTime.Parse(row[17].ToString()).ToString("HH:mm tt"); }
            if (_SchedBTimeOut != "") { _SchedBTimeOut = DateTime.Parse(row[18].ToString()).ToString("HH:mm tt"); }
            if (_SchedBTimeIN != "") { _SchedBTimeIN = DateTime.Parse(row[19].ToString()).ToString("HH:mm tt"); }
            if (_SchedTimeOut != "") { _SchedTimeOut = DateTime.Parse(row[20].ToString()).ToString("HH:mm tt"); }


            string _UTApproved = row[15].ToString();


            //GET Actual Time Per Shift
            double _AMHr = double.Parse((DateTime.Parse(_TransDate + " " + _SchedBTimeOut) - DateTime.Parse(_TransDate + " " + _SchedTimeIN)).ToString("hh"));
            double _PMHr = double.Parse((DateTime.Parse(_TransDate + " " + _SchedTimeOut) - DateTime.Parse(_TransDate + " " + _SchedBTimeIN)).ToString("hh"));


            double _AMMin = (double.Parse((DateTime.Parse(_TransDate + " " + _SchedBTimeOut) - DateTime.Parse(_TransDate + " " + _SchedTimeIN)).ToString("mm")) / 60);
            double _PMMin = (double.Parse((DateTime.Parse(_TransDate + " " + _SchedTimeOut) - DateTime.Parse(_TransDate + " " + _SchedBTimeIN)).ToString("mm")) / 60);

            double _AMTotalHr = 0;
            double _PMTotalHr = 0;


            double _AbsentHr = 0;
            double _AbsentMin = 0;


            if (_TimeIN == "" || _BTimeOut == "")
            {
                _AMTotalHr = _AMHr + _AMMin;
            }


            if (_BTimeIN == "" || _TimeOut == "")
            {
                _PMTotalHr = _PMHr + _PMMin;
            }






            //GET Tardiness Record
            double _TAMHr = double.Parse((DateTime.Parse(_TransDate + " " + _SchedTimeIN) - DateTime.Parse(_TransDate + " " + _TimeIN)).ToString("hh"));
            double _TPMHr = double.Parse((DateTime.Parse(_TransDate + " " + _SchedBTimeIN) - DateTime.Parse(_TransDate + " " + _BTimeIN)).ToString("hh"));


            double _TAMMin = (double.Parse((DateTime.Parse(_TransDate + " " + _SchedTimeIN) - DateTime.Parse(_TransDate + " " + _TimeIN)).ToString("mm")) / 60);
            double _TPMMin = (double.Parse((DateTime.Parse(_TransDate + " " + _SchedBTimeIN) - DateTime.Parse(_TransDate + " " + _BTimeIN)).ToString("mm")) / 60);



            double _AMTotalHr_Tardy = 0;
            double _PMTotalHr_Tardy = 0;



            double _TardyHr = 0;
            double _TardyMin = 0;




            if (_TimeIN != "")
            {
                if (_BTimeOut != "")
                {
                    if (DateTime.Parse(_TransDate + " " + _TimeIN) > DateTime.Parse(_TransDate + " " + _SchedTimeIN))
                    {
                        _AMTotalHr_Tardy = _TAMHr + _TAMMin;
                    }
                }
            }


            if (_BTimeIN != "")
            {
                if (_TimeOut != "")
                {
                    if (DateTime.Parse(_TransDate + " " + _BTimeIN) > DateTime.Parse(_TransDate + " " + _SchedBTimeIN))
                    {
                        _PMTotalHr_Tardy = _TPMHr + _TPMMin;
                    }
                }
            }


            //_TardyHr = _AMTotalHr_Tardy + _PMTotalHr_Tardy;
            //_TardyMin = ((_AMTotalHr_Tardy + _PMTotalHr_Tardy) - Math.Truncate((_AMTotalHr_Tardy + _PMTotalHr_Tardy))) * 60;

            //GET Undertime Record
            double _UDPMHr = double.Parse((DateTime.Parse(_TransDate + " " + _SchedTimeOut) - DateTime.Parse(_TransDate + " " + _TimeOut)).ToString("hh"));
            double _UDAMHr = double.Parse((DateTime.Parse(_TransDate + " " + _SchedBTimeOut) - DateTime.Parse(_TransDate + " " + _BTimeOut)).ToString("hh"));


            double _UDPMMin = (double.Parse((DateTime.Parse(_TransDate + " " + _SchedTimeOut) - DateTime.Parse(_TransDate + " " + _TimeOut)).ToString("mm")) / 60);
            double _UDAMMin = (double.Parse((DateTime.Parse(_TransDate + " " + _SchedBTimeOut) - DateTime.Parse(_TransDate + " " + _BTimeOut)).ToString("mm")) / 60);


            double _OTHHr = double.Parse((DateTime.Parse(_TransDate + " " + _BTimeIN) - DateTime.Parse(_TransDate + " " + _BTimeOut)).ToString("hh"));
            double _OTHMin = (double.Parse((DateTime.Parse(_TransDate + " " + _BTimeIN) - DateTime.Parse(_TransDate + " " + _BTimeOut)).ToString("mm")) / 60);


            //Get Other Time Record

            double _AMTotalHr_UD = 0;
            double _PMTotalHr_UD = 0;



            double _UnderTimeHr = 0;
            double _UnderTimeMin = 0;


            if (_TimeIN != "" && _BTimeIN != "")
            {
                if (_BTimeOut != "")
                {
                    if (DateTime.Parse(_TransDate + " " + _BTimeOut) < DateTime.Parse(_TransDate + " " + _SchedBTimeOut)
                        && DateTime.Parse(_TransDate + " " + _BTimeOut) > DateTime.Parse(_TransDate + " " + _SchedTimeIN)
                        && DateTime.Parse(_TransDate + " " + _BTimeIN) > DateTime.Parse(_TransDate + " " + _SchedBTimeOut))
                    {
                        _AMTotalHr_UD = _UDAMHr + _UDAMMin;
                    }

                    if (DateTime.Parse(_TransDate + " " + _BTimeIN) < DateTime.Parse(_TransDate + " " + _SchedBTimeOut))
                    {
                        _AMTotalHr_UD = _OTHHr + _OTHMin;
                    }

                    if (DateTime.Parse(_TransDate + " " + _BTimeOut) < DateTime.Parse(_TransDate + " " + _SchedTimeIN))
                    {
                        _AMTotalHr_UD = _AMHr + _AMMin;
                    }
                }
            }


            if (_BTimeIN != "")
            {
                if (_TimeOut != "")
                {
                    if (DateTime.Parse(_TransDate + " " + _TimeOut) < DateTime.Parse(_TransDate + " " + _SchedTimeOut)
                        && DateTime.Parse(_TransDate + " " + _TimeOut) > DateTime.Parse(_TransDate + " " + _SchedBTimeIN)
                        && DateTime.Parse(_TransDate + " " + _TimeOut) > DateTime.Parse(_TransDate + " " + _SchedTimeOut))
                    {
                        _PMTotalHr_UD = _UDPMHr + _UDPMMin;
                    }

                    if (DateTime.Parse(_TransDate + " " + _TimeOut) < DateTime.Parse(_TransDate + " " + _SchedTimeOut))
                    {
                        _PMTotalHr_UD = _OTHHr + _OTHMin;
                    }

                    if (DateTime.Parse(_TransDate + " " + _TimeOut) < DateTime.Parse(_TransDate + " " + _SchedTimeOut))
                    {
                        _PMTotalHr_UD = _OTHHr + _OTHMin;
                    }
                    if (DateTime.Parse(_TransDate + " " + _TimeOut) < DateTime.Parse(_TransDate + " " + _SchedBTimeIN))
                    {
                        _PMTotalHr_UD = _AMHr + _AMMin;
                    }
                }
            }



            if (_AMTotalHr_Tardy > .5)
            {
                if (_UTApproved.Trim() == "N")
                {
                    _AMTotalHr = _AMHr + _AMMin;
                    _AMTotalHr_Tardy = 0;
                }
            }
            if (_PMTotalHr_Tardy > .5)
            {
                if (_UTApproved.Trim() == "N")
                {
                    _PMTotalHr = _PMHr + _PMMin;
                    _PMTotalHr_Tardy = 0;
                }

            }


            double _TotalRegualrHr;
            double _RegualrHr;
            double _RegualrMin;

            _AbsentHr = (_AMTotalHr + _PMTotalHr) - ((_AMTotalHr + _PMTotalHr) - Math.Truncate((_AMTotalHr + _PMTotalHr)));
            _AbsentMin = ((_AMTotalHr + _PMTotalHr) - Math.Truncate((_AMTotalHr + _PMTotalHr))) * 60;


            _TardyHr = (_AMTotalHr_Tardy + _PMTotalHr_Tardy) - ((_AMTotalHr_Tardy + _PMTotalHr_Tardy) - Math.Truncate((_AMTotalHr_Tardy + _PMTotalHr_Tardy)));
            _TardyMin = ((_AMTotalHr_Tardy + _PMTotalHr_Tardy) - Math.Truncate((_AMTotalHr_Tardy + _PMTotalHr_Tardy))) * 60;

            _UnderTimeHr = (_AMTotalHr_UD + _PMTotalHr_UD) - ((_AMTotalHr_UD + _PMTotalHr_UD) - Math.Truncate((_AMTotalHr_UD + _PMTotalHr_UD)));
            _UnderTimeMin = ((_AMTotalHr_UD + _PMTotalHr_UD) - Math.Truncate((_AMTotalHr_UD + _PMTotalHr_UD))) * 60;



            _TotalRegualrHr = (8 - (_AMTotalHr + _PMTotalHr + _AMTotalHr_Tardy + _PMTotalHr_Tardy + _AMTotalHr_UD + _PMTotalHr_UD));


            _RegualrHr = (_TotalRegualrHr) - ((_TotalRegualrHr) - Math.Truncate(_TotalRegualrHr));
            _RegualrMin = ((_TotalRegualrHr) - Math.Truncate(_TotalRegualrHr)) * 60;


            double _ExcessHr = 0;
            double _ExcessMin = 0;

            double _TotalExcessHr = 0;
            double _TotalExcessMin = 0;
            double _TotalExcess = 0;

            if (_TimeOut != "")
            {
                _ExcessHr = double.Parse((DateTime.Parse(_TransDate + " " + _SchedTimeOut) - DateTime.Parse(_TransDate + " " + _TimeOut)).ToString("hh"));
                _ExcessMin = (double.Parse((DateTime.Parse(_TransDate + " " + _SchedTimeOut) - DateTime.Parse(_TransDate + " " + _TimeOut)).ToString("mm")) / 60);

                _TotalExcess = _ExcessHr + _ExcessMin;

                if ((_TotalExcess * 60) > 60)
                {
                    _TotalExcessHr = (_TotalExcess) - ((_TotalExcess) - Math.Truncate((_TotalExcess)));
                    _TotalExcessMin = ((_TotalExcess) - Math.Truncate((_TotalExcess))) * 60;
                }

            }






            _Display = _Display + @"
                                        SELECT '" + _EmployeeNo + @"' AS [Employee No]
                                              ,'" + _EmployeeName.Replace("'", "''") + @"' AS [Employee Name]
                                              ,'" + _Company + @"' AS [Company]


                                              ,'" + _TransDate + @"' AS [Trans Date]
                                              ,'" + DateTime.Parse(_TransDate + " " + _TimeIN) + @"' AS [Time In]
                                              ,'" + DateTime.Parse(_TransDate + " " + _TimeOut) + @"' AS [Time Out]


                                              ,CAST('8' AS NUMERIC(19,6)) AS [Work Hours]

                                              ,CAST('" + _AMTotalHr + @"' AS NUMERIC(19,6)) AS [Absent AM]
                                              ,CAST('" + _PMTotalHr + @"' AS NUMERIC(19,6)) AS [Absent PM]
                                              ,CAST('" + _AMTotalHr_Tardy + @"' AS NUMERIC(19,6)) AS [Tardy AM]
                                              ,CAST('" + _PMTotalHr_Tardy + @"' AS NUMERIC(19,6)) AS [Tardy PM]
                                              ,CAST('" + _AMTotalHr_UD + @"' AS NUMERIC(19,6)) AS [Under AM]
                                              ,CAST('" + _PMTotalHr_UD + @"' AS NUMERIC(19,6)) AS [Under PM]


                                              ,CAST('" + (_AMTotalHr + _PMTotalHr + _AMTotalHr_Tardy + _PMTotalHr_Tardy + _AMTotalHr_UD + _PMTotalHr_UD) + @"' AS NUMERIC(19,6)) AS [Total Time Deduction]

                                              ,CAST('" + (8 - (_AMTotalHr + _PMTotalHr + _AMTotalHr_Tardy + _PMTotalHr_Tardy + _AMTotalHr_UD + _PMTotalHr_UD)) + @"' AS NUMERIC(19,6)) AS [Total Work Hours]



                                              ,CAST('" + _RegualrHr + @"' AS NUMERIC(19,6)) AS [Regular Hr]
                                              ,CAST('" + _RegualrMin + @"' AS NUMERIC(19,6)) AS [Regular Min]


                                              ,CAST('" + _AbsentHr + @"' AS NUMERIC(19,6)) AS [Absent Hr]
                                              ,CAST('" + _AbsentMin + @"' AS NUMERIC(19,6)) AS [Absent Min]

                                              ,CAST('" + _TardyHr + @"' AS NUMERIC(19,6)) AS [Tardy Hr]
                                              ,CAST('" + _TardyMin + @"' AS NUMERIC(19,6)) AS [Tardy Min]

                                              ,CAST('" + _UnderTimeHr + @"' AS NUMERIC(19,6)) AS [UT Hr]
                                              ,CAST('" + _UnderTimeMin + @"' AS NUMERIC(19,6)) AS [UT Min]

                                              ,CAST('" + _TotalExcessHr + @"' AS NUMERIC(19,6)) AS [Excess Hr]
                                              ,CAST('" + _TotalExcessMin + @"' AS NUMERIC(19,6)) AS [Excess Min]

                                        ";


            //int _day = DateTime.Parse(_TransDate).Day;
            //Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Absent AM", _day.ToString(), _AMTotalHr.ToString());
            //Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Absent PM", _day.ToString(), _PMTotalHr.ToString());
            //Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Tardy AM", _day.ToString(), _AMTotalHr_Tardy.ToString());
            //Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Tardy PM", _day.ToString(), _PMTotalHr_Tardy.ToString());
            //Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Under AM", _day.ToString(), _AMTotalHr_UD.ToString());
            //Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Under PM", _day.ToString(), _PMTotalHr_UD.ToString());

            i++;
            if (i != _Count)
            {
                _Display = _Display + @"UNION ALL
                                        ";
            }





            // Excel Progress Monitoring
            Application.DoEvents();
            //_Count++;
            tssDataStatus.Text = "Reading Data : (" + i + " / " + _RowCount + ") : Data Progress ( " + Math.Round(((i / _RowCount) * 100), 2) + " % ) : " + _EmployeeNo + " : " + _EmployeeName;
            pbDataProgress.Value = Convert.ToInt32(((i / _RowCount) * 100));
        }

        
        _Display = _Display + ") Z GROUP BY  Z.[Employee No],Z.[Employee Name], Z.[Trans Date], Z.[Company],Z.[Time In],Z.[Time Out]";
        DataTable _Table;
        _Table = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Display);
        dtDataTimeRecord = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Display);
        clsFunctions.DataGridViewSetup(dataGridView2, _Table);
    }

    private void GenerateRecord()
    {
        double _RowCount;
        int _Count = dtDataTimeRecord.Rows.Count;
        int i = 0;
        _RowCount = _Count;


        foreach (DataRow row in dtDataTimeRecord.Rows)
        {
            string srEmployeeNo;
            string srTransDate;
            srEmployeeNo = row[0].ToString();
            srTransDate = row[2].ToString();

            DateTime dtTimeIN = DateTime.Parse(row[4].ToString());
            DateTime dtTimeOUT = DateTime.Parse(row[5].ToString());

            double dRegHr = double.Parse(row[15].ToString());
            double dRegMin = double.Parse(row[16].ToString());
            double dAbsentHr = double.Parse(row[17].ToString());
            double dAbsentMin = double.Parse(row[18].ToString());
            double dTardyHr = double.Parse(row[19].ToString());
            double dTardyMin = double.Parse(row[20].ToString());
            double dUTHr = double.Parse(row[21].ToString());
            double dUTMin = double.Parse(row[22].ToString());
            double dExceesHr = double.Parse(row[23].ToString());
            double dExceesMin = double.Parse(row[24].ToString());

            double dHolidayHr = 0;
            double dHolidayMin = 0;


            string _Syntax = "";
            DataTable _DataTable;
            _Syntax = @"SELECT * FROM OCMP Z WHERE Z.Active = '1' AND Z.CompCode IN (SELECT A.CompCode FROM Employees A  WHERE A.EmployeeNo = '" + srEmployeeNo + "')";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Syntax);


            clsDeclaration.sServer = clsSQLClientFunctions.GetData(_DataTable, "DBServer", "0");
            clsDeclaration.sCompany = clsSQLClientFunctions.GetData(_DataTable, "DBName", "0");
            clsDeclaration.sUsername = clsSQLClientFunctions.GetData(_DataTable, "DBUsername", "0");
            clsDeclaration.sPassword = clsSQLClientFunctions.GetData(_DataTable, "DBPassword", "0");

            string strConnection = clsSQLClientFunctions.GlobalConnectionString(
                                           clsDeclaration.sServer, clsDeclaration.sCompany,
                                           clsDeclaration.sUsername, clsDeclaration.sPassword
                                        );

            clsSQLClientFunctions.GlobalExecuteCommand(strConnection, "DELETE FROM DailyTimeDetails WHERE EmployeeNo = '" + srEmployeeNo + @"' AND TransDate = '" + srTransDate + @"'");


            string strHolidateSQL;
            strHolidateSQL = @" SELECT 'TRUE' FROM HolidayTable A WHERE CONCAT(A.HolidayCode, '/', YEAR('" + srTransDate + "')) = '" + srTransDate  + "'";
            int _HolidayCount;
            _HolidayCount = clsSQLClientFunctions.DataList(strConnection, strHolidateSQL).Rows.Count;

            if(_HolidayCount > 0)
            {
                dHolidayHr = dRegHr;
                dHolidayMin = dRegMin;

                dRegHr = 0;
                dRegMin = 0;
            }


            string strUpdateDTR;
            strUpdateDTR = @"UPDATE A SET
                               A.[ApprovedOT] = 0
                              ,A.[Tardiness] = 0
                              ,A.[ApprovedOTMins] = 0
                              ,A.[NightDiffMins] = 0
                              ,A.[SickLeaveMins] = 0
                              ,A.[VacationLeaveMins] = 0
                              ,A.[PaternityLeaveMins] = 0
                              ,A.[NightDiffOTMins] = 0
                              ,A.[BaseRegularMins] = 0
                              ,A.[ApprovedNDiffOTHrs] = 0
                              ,A.[ApprovedNDiffOTMins] = 0
                              ,A.[NextDayTime] = 0
                              ,A.[EarlyOTHrs] = 0
                              ,A.[EarlyOTMins] = 0
                              ,A.[ApprovedEarlyOTHrs] = 0
                              ,A.[ApprovedEarlyOTMins] = 0
                              ,A.[DispTimeIn] = ''
                              ,A.[DispTimeOut] = ''

                              ,A.[NightDiff] = 0
                              ,A.[SickLeave] = 0
                              ,A.[VacationLeave] = 0
                              ,A.[PaternityLeave] = 0
                              ,A.[InOutStatus] = 0
                              ,A.[NightDiffOT] = 0
                              ,A.[LeaveCode] = ''
                              ,A.[OtherDeduction] = 0
                              ,A.[DeductibleAmount] = 0
                              ,A.[ProjectCode] = ''

                            ,A.[TimeIn] = '" + dtTimeIN + @"'
                            ,A.[TimeOut] = '" + dtTimeOUT + @"'

                            ,A.[RegularHrs] = '" + dRegHr + @"'
                            ,A.[Absences] = '" + dAbsentHr + @"'
                            ,A.[ExcessHrs] = '" + dExceesHr + @"'
                            ,A.[Holiday] = '" + dHolidayHr  + @"'
                            ,A.[BaseRegularHrs] = '8'
                            ,A.[RegularMins] = '" + dRegMin + @"'
                            ,A.[AbsencesMins] = '" + dAbsentMin + @"'
                            ,A.[TardinessMins] = '" + ((dTardyHr * 60) + dTardyMin) + @"'
                            ,A.[ExcessMins] = '" + dExceesMin + @"'
                            ,A.[HolidayMins] = '" + dHolidayMin + @"'
                            ,[OtherDeductionMins] = '" + ((dUTHr * 60) + dUTMin) + @"'

                        FROM DailyTimeDetails A
                        WHERE A.EmployeeNo = '" + srEmployeeNo + @"' AND A.TransDate = '" + srTransDate + @"'
                          ";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, strUpdateDTR);


            strUpdateDTR = @"
                                INSERT INTO DailyTimeDetails ( 
[EmployeeNo]
,[TransDate]
,[ApprovedOT]
                                                                ,[Tardiness]
                                                                ,[ApprovedOTMins] 
                                                                ,[NightDiffMins] 
                                                                ,[SickLeaveMins] 
                                                                ,[VacationLeaveMins]
                                                                ,[PaternityLeaveMins] 
                                                                ,[NightDiffOTMins]
                                                                ,[BaseRegularMins] 
                                                                ,[ApprovedNDiffOTHrs] 
                                                                ,[ApprovedNDiffOTMins] 
                                                                ,[NextDayTime]
                                                                ,[EarlyOTHrs]
                                                                ,[EarlyOTMins] 
                                                                ,[ApprovedEarlyOTHrs] 
                                                                ,[ApprovedEarlyOTMins]
                                                                ,[DispTimeIn] 
                                                                ,[DispTimeOut]

                                                                ,[NightDiff]
                                                                ,[SickLeave]
                                                                ,[VacationLeave]
                                                                ,[PaternityLeave]
                                                                ,[InOutStatus]
                                                                ,[NightDiffOT]
                                                                ,[LeaveCode]
                                                                ,[OtherDeduction] 
                                                                ,[DeductibleAmount] 
                                                                ,[ProjectCode]

                                                                ,[TimeIn] 
                                                                ,[TimeOut]

                                                                ,[RegularHrs]
                                                                ,[Absences] 
                                                                ,[ExcessHrs]
                                                                ,[Holiday] 
                                                                ,[BaseRegularHrs] 
                                                                ,[RegularMins]
                                                                ,[AbsencesMins]
                                                                ,[TardinessMins] 
                                                                ,[ExcessMins]
                                                                ,[HolidayMins]
                                                                ,[OtherDeductionMins] 
                                                                )
                                                                VALUES 
                                                                ( 
'" + srEmployeeNo + @"'
,'" + srTransDate + @"'
,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,''
                                                                ,''

                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,0
                                                                ,''
                                                                ,0
                                                                ,0
                                                                ,''

                                                                ,'" + dtTimeIN + @"'
                                                                ,'" + dtTimeOUT + @"'

                                                                ,'" + dRegHr + @"'
                                                                ,'" + dAbsentHr + @"'
                                                                ,'" + dExceesHr + @"'
                                                                ,'" + dHolidayHr + @"'
                                                                ,'8'
                                                                ,'" + dRegMin + @"'
                                                                ,'" + dAbsentMin + @"'
                                                                ,'" + ((dTardyHr * 60) + dTardyMin) + @"'
                                                                ,'" + dExceesMin + @"'
                                                                ,'" + dHolidayMin + @"'
                                                                ,'" + ((dUTHr * 60) + dUTMin) + @"'
                                                                )

                            ";

            clsSQLClientFunctions.GlobalExecuteCommand(strConnection, strUpdateDTR);

            i++;
            // Excel Progress Monitoring
            Application.DoEvents();
            //_Count++;
            tssDataStatus.Text = "Reading Data : (" + i + " / " + _RowCount + ") : Data Progress ( " + Math.Round(((i / _RowCount) * 100), 2) + " % ) : " + srEmployeeNo + " : " + srTransDate;
            pbDataProgress.Value = Convert.ToInt32(((i / _RowCount) * 100));
        }
    }



    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT CONCAT(A.Company,A.Code,' - ',A.Name) AS Branch FROM [OBLP] A WHERE A.Area = '" + cboArea.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboBranch.Items.Clear();
        cboBranch.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboBranch.Items.Add(row[0].ToString());
        }
    }

    private void btnDisplay_Click(object sender, EventArgs e)
    {
        string _Branch;
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }


        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT 'TRUE' FROM [DateLocker] A WHERE A.[TransDate] = '" + dtpFrom.Text + "' AND A.[Branch] LIKE '%" + _Branch + "%' AND [IsLocked] = '1'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        if (_DataTable.Rows.Count != 0)
        {
            MessageBox.Show("This Branch is already lock for this date");
            return;
        }


        if (cboBranch.Text == "")
        {
            MessageBox.Show("Please Identify Branch");
            return;
        }

        btnUpdatedDTR_Click(sender, e);
        button2_Click(sender, e);
        button3_Click(sender, e);
        DispalyDailyTimeRecord();
        GenerateRecord();

        MessageBox.Show("Update Complete");
    }

    private void addToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmAddTransData frmAddTransData = new frmAddTransData();

        frmAddTransData._EmpCode = sEmployeeNo;
        frmAddTransData._EmpName = sEmployeeName;
        frmAddTransData._TransDate = sTransDate;
        frmAddTransData._AddType = "2";

        //frmAddOT._ExcessHr = sExcessHr;
        //frmAddOT._ExcessMin = sExcessMin;
        //frmAddOT._ApprovedHr = sApprovedHr;
        //frmAddOT._ApprovedMin = sApprovedMin;
        //frmAddOT._sConnection = clsDeclaration.sCompanyConnection;

        frmAddTransData.ShowDialog();
    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmTimeRecord frmTimeRecord = new frmTimeRecord();

        frmTimeRecord._EmpCode = sEmployeeNo;
        frmTimeRecord._EmpName = sEmployeeName;
        frmTimeRecord._TransDate = sTransDate;
        //frmAddOT._EmpCode = sEmployeeNo;
        //frmAddOT._EmpName = sEmployeeName;
        //frmAddOT._TransDate = sTransDate;
        //frmAddOT._ExcessHr = sExcessHr;
        //frmAddOT._ExcessMin = sExcessMin;
        //frmAddOT._ApprovedHr = sApprovedHr;
        //frmAddOT._ApprovedMin = sApprovedMin;
        //frmAddOT._sConnection = clsDeclaration.sCompanyConnection;

        frmTimeRecord.ShowDialog();
    }

    private void dtpFrom_ValueChanged(object sender, EventArgs e)
    {
        Locker();
    }

    private void Locker()
    {

        string _Branch;
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }
        
        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT 'TRUE' FROM [DateLocker] A WHERE A.[TransDate] = '" + dtpFrom.Text + "' AND A.[Branch] LIKE '%" + _Branch + "%' AND [IsLocked] = '1'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        btnLocked.Enabled = true;
        btnUnlocked.Enabled = true;

        if (_DataTable.Rows.Count != 0)
        {
            btnLocked.Enabled = false;
        }
        else
        {
            btnUnlocked.Enabled = false;
        }

    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locker();
    }

    private void btnLocked_Click(object sender, EventArgs e)
    {

        string _Branch;
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }


        string _InsertData = @"
                                        IF EXISTS(SELECT 'TRUE' FROM [DateLocker] A  WHERE A.[TransDate] = '" + dtpFrom.Text + @"'
                                                                                  AND A.[Branch] LIKE '%" + _Branch + @"%' )
                                        BEGIN
						                                        UPDATE [dbo].[DateLocker]
								                                        SET [IsLocked] = '1'
								                                        WHERE [TransDate] = '" + dtpFrom.Text + @"'
									                                        AND [Branch] LIKE '%" + _Branch + @"%'
                                        END
                                        ELSE
                                        BEGIN
                                                             INSERT INTO DateLocker ([TransDate]
											                                        ,[Branch]
											                                        ,[IsLocked]) VALUES ('" + dtpFrom.Text + @"','" + _Branch + @"','1')
                                        END
                                      ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _InsertData);

        MessageBox.Show("Branch " + cboBranch.Text + " is Lock for the Date " + dtpFrom.Text);
        Locker();
    }

    private void btnUnlocked_Click(object sender, EventArgs e)
    {

        string _Branch;
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }

        string _UpdateData = @"
                                     UPDATE [dbo].[DateLocker]
                                       SET [IsLocked] = '0'
                                     WHERE [TransDate] = '" + dtpFrom.Text + @"'
                                          AND [Branch] LIKE '%" + _Branch + @"%'
                              ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _UpdateData);
        MessageBox.Show("Branch " + cboBranch.Text + " is UnlLock for the Date " + dtpFrom.Text);
        Locker();
    }

    private void button1_Click(object sender, EventArgs e)
    {
       
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
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
}