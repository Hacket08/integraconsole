using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmAttendanceMonitoring : Form
{
    private static DataTable _CompanyList = new DataTable();
    private static DataTable _DataList = new DataTable();
    private static DataTable dtDataTimeRecord = new DataTable();

    private static string sEmployeeNo;
    private static string sEmployeeName;
    private static string sTransDate;

    public frmAttendanceMonitoring()
    {
        InitializeComponent();
    }

    private void frmAttendanceMonitoring_Load(object sender, EventArgs e)
    {
        clsDeclaration.sCompanyConnection = "";

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }

        dtpDate.Value = DateTime.Today.AddDays(-1);
        dtpFrom.Value = DateTime.Today.AddDays(-1);
        dtpTo.Value = DateTime.Today.AddDays(-1);



        _SQLSyntax = "SELECT DISTINCT A.Area FROM [OBLP] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
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

        //DataTable _DataTable;
        //string _SQLSyntax;
        //_SQLSyntax = "SELECT DISTINCT A.Area FROM usr_Branches A";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        //cboArea.Items.Clear();
        //cboArea.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboArea.Items.Add(row[0].ToString());
        //}



        //_SQLSyntax = "SELECT DISTINCT A.Company FROM usr_Branches A";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
        //cboGroup.Items.Clear();
        //cboGroup.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboGroup.Items.Add(row[0].ToString());
        //}

        //displayData();
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

                //sExcessHr = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                //sExcessMin = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                //sApprovedHr = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                //sApprovedMin = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();


            }
        }
        catch
        {

        }

    }
    private void displayData()
    {
        //if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == false)
        //{
        //    return;
        //}

        try
        {
            DataTable _DataTable;
            string _SQLSyntax;
            string _Branch;
            if (cboBranch.Text == "")
            {
                _Branch = "";
            }
            else
            {
                _Branch = cboBranch.Text.Substring(0, 8);
            }


            _SQLSyntax = @"
                              SELECT A.*
                                
    						  ,CASE WHEN RIGHT(B.Department,4) IN (SELECT Z.[Code] FROM OPST Z WHERE ISNULL(Z.[CalcBreak],'0') = '1')  AND ISNULL(A.TimeIn,'') <> '' THEN ISNULL(A.BTimeOut,A.BreakTime01) ELSE A.BTimeOut END AS [BreakOut]
							  ,CASE WHEN RIGHT(B.Department,4) IN (SELECT Z.[Code] FROM OPST Z WHERE ISNULL(Z.[CalcBreak],'0') = '1')  AND ISNULL(A.TimeOut,'') <> '' THEN ISNULL(A.BTimeIn,A.BreakTime02) ELSE A.BTimeIn END AS [BreakIn]
                              ,C.Name AS BranchName
                              ,(SELECT Z.Name FROM OPST Z WHERE Z.Code = RIGHT(B.Department,4)) AS Position
                              FROM DailyAttendance A 
                              INNER JOIN [Employees] B ON A.[EmployeeNo] = B.[EmployeeNo]
                              LEFT JOIN [OBLP] C ON LEFT(B.Department,8) = CONCAT(C.Company, C.Code)
                              WHERE A.Branch LIKE '%" + _Branch + @"%' AND C.Area LIKE '%" + cboArea.Text + @"%'
                              AND A.TransDate BETWEEN '" + dtpFrom.Text + @"' AND '" + dtpTo.Text + @"' AND B.EmpStatus IN (0,1,2)
                          ";


            //   _SQLSyntax = @"
            //                     SELECT A.*

            //	  ,A.BTimeOut AS [BreakOut]
            //,A.BTimeIn AS [BreakIn]
            //                     ,C.Name AS BranchName
            //                     ,(SELECT Z.Name FROM OPST Z WHERE Z.Code = RIGHT(B.Department,4)) AS Position
            //                     FROM DailyAttendance A 
            //                     INNER JOIN [Employees] B ON A.[EmployeeNo] = B.[EmployeeNo]
            //                     LEFT JOIN [OBLP] C ON LEFT(B.Department,8) = CONCAT(C.Company, C.Code)
            //                     WHERE CONCAT(C.Company,C.Code) LIKE '%" + _Branch + @"%' AND C.Area LIKE '%" + cboArea.Text + @"%'
            //                     AND A.TransDate BETWEEN '" + dtpFrom.Text + @"' AND '" + dtpTo.Text + @"' AND B.EmpStatus IN (0,1,2)
            //                 ";

            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable, "Attendance");


         
            }
        catch { }

    }

    private void button1_Click(object sender, EventArgs e)
    {
        displayData();
        button2_Click(sender, e);
        btnGenRecord_Click(sender, e);
    }

    private void button2_Click(object sender, EventArgs e)
    {

        if (_DataList.Rows.Count == 0)
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
        int _Count = _DataList.Rows.Count;
        int i = 0;
        _RowCount = _Count;
        foreach (DataRow row in _DataList.Rows)
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


            if (_BTimeIN == "" || _TimeOut  == "")
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
                    _AMTotalHr_UD = _AMHr + _AMMin;
                    _AMTotalHr = 0;
                    _AMTotalHr_Tardy = 0;
                }
            }
            if (_PMTotalHr_Tardy > .5)
            {
                if (_UTApproved.Trim() == "N")
                {
                    _PMTotalHr_UD= _PMHr + _PMMin;
                    _PMTotalHr = 0;
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

                                              ,CAST('" + (8 -(_AMTotalHr + _PMTotalHr + _AMTotalHr_Tardy + _PMTotalHr_Tardy + _AMTotalHr_UD + _PMTotalHr_UD)) + @"' AS NUMERIC(19,6)) AS [Total Work Hours]



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


            int _day = DateTime.Parse(_TransDate).Day;
            Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Absent AM", _day.ToString(), _AMTotalHr.ToString());
            Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Absent PM", _day.ToString(), _PMTotalHr.ToString());
            Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Tardy AM", _day.ToString(), _AMTotalHr_Tardy.ToString());
            Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Tardy PM", _day.ToString(), _PMTotalHr_Tardy.ToString());
            Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Under AM", _day.ToString(), _AMTotalHr_UD.ToString());
            Add_Data(_EmployeeNo, _EmployeeName.Replace("'", "''"), "Under PM", _day.ToString(), _PMTotalHr_UD.ToString());

            i++;
            if (i != _Count)
            {
                _Display = _Display + @"UNION ALL
                                        ";
            }
            
            // Excel Progress Monitoring
            Application.DoEvents();
            //_Count++;
            tssDataStatus.Text = "Reading Data : (" + i + " / " + _RowCount + ") : " + _EmployeeName + "  : Data Progress ( " + Math.Round(((i / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((i / _RowCount) * 100));
        }
        
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(frmReportViewer))
            {
                form.Activate();
                return;
            }
        }
        
        _Display = _Display + ") Z GROUP BY  Z.[Employee No],Z.[Employee Name], Z.[Trans Date], Z.[Company],Z.[Time In],Z.[Time Out]";
        DataTable _Table;
        _Table = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Display);
        dtDataTimeRecord = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Display);
        clsFunctions.DataGridViewSetup(dataGridView2, _Table);
        
    }


    private void Add_Data(string EmployeeNo, string EmployeeName, string Type, string Day, string Value)
    {

        string _InsertData = @"
                                    INSERT INTO [dbo].[OAMR]
                                               ([EmployeeNo]
                                               ,[EmployeeName]
                                               ,[Type]
                                               ,[DAY" + Day + @"])
                                            VALUES
                                            (
	                                             '" + EmployeeNo + @"'
	                                            ,'" + EmployeeName.Replace("'", "''") + @"'
	                                            ,'" + Type + @"'
	                                            ,'" + Value + @"'
                                            )
                    
                                  ";


        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _InsertData);
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        string _SQLSyntax;
        DataTable _DataTable;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = 1";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        foreach (DataRow row in _DataTable.Rows)
        {
            string _Code = row[7].ToString();
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
                DataTable _SetDataTable;
                string _SetSQLSyntax;
                _SetSQLSyntax = @"
                                SELECT X.* 
                                , X2.Weekdays01, X2.BreakTime01, X2.BreakTime02 , X2.Weekdays02
                                FROM (

                                SELECT A.EmployeeNo
                                , CONCAT(B.LastName, ', ', B.FirstName, ' ', B.MiddleName) AS [EmployeeName]
                                , A.TransDate, A.TimeIn, C.TimeOUT AS BTimeOUT, C.TimeIn AS BTimeIn, A.TimeOut
                                , A.Absences
                                ,A.AbsencesMins, A.Tardiness, A.TardinessMins, C.TotalHrs, C.TotalMins
                                ,CASE WHEN ISNULL(B.ScheduleCode,'') = '' THEN (SELECT Z.ScheduleCode FROM Schedule Z WHERE Z.DefaultSched = 1) ELSE B.ScheduleCode END AS ScheduleCode
                                ,(SELECT Z.ShiftCode FROM ScheduleCalendarEmp Z WHERE Z.EmployeeNo = A.EmployeeNo AND Z.AppDays = A.TransDate) AS [Actual Schedule]
                                ,ISNULL(A.UnderApproved,'N') AS [UT Approved]
                                ,B.Category
                                ,A.Comment
                                FROM DailyTimeDetails A 
                                INNER JOIN Employees B ON A.EmployeeNo = B.EmployeeNo
                                LEFT JOIN DailyInOut C ON A.EmployeeNo = C.EmployeeNo AND A.TransDate = C.TransDate AND ISNULL(C.TimeOut,'') <> '' AND ISNULL(C.TimeIn,'') <> ''
                                WHERE A.TransDate = '" + dtpDate.Text + @"'  AND B.EmpStatus IN (0,1,2)
                                --AND ISNULL(A.TimeOut,'') <> ''
                                --AND ISNULL(A.TimeIn,'') <> ''
                                --AND ISNULL(C.TimeOut,'') <> ''
                                --AND ISNULL(C.TimeIn,'') <> ''

                                ) X INNER JOIN Schedule X2 ON ISNULL(X.[Actual Schedule], X.ScheduleCode) = X2.ScheduleCode
							    --WHERE X.EmployeeNo = '1430'
                                ORDER BY X.[EmployeeName]
                          ";
                _SetDataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SetSQLSyntax);
            



                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, "DELETE FROM DailyAttendance WHERE [Company] = '" + _Code + @"' AND [TransDate] = '" + dtpDate.Text + @"'");


                int i = 0;
                double _RowCount = _SetDataTable.Rows.Count;
                foreach (DataRow row1 in _SetDataTable.Rows)
                {

                        string _InsertData = @"
                                       INSERT INTO DailyAttendance
                                       SELECT   CASE WHEN '" + row1[0].ToString() + @"' = '' THEN NULL ELSE '" + row1[0].ToString() + @"' END
                                               ,CASE WHEN '" + row1[1].ToString().Replace("'","''") + @"' = '' THEN NULL ELSE '" + row1[1].ToString().Replace("'", "''") + @"' END
                                               ,CASE WHEN '" + row1[2].ToString() + @"' = '' THEN NULL ELSE '" + row1[2].ToString() + @"' END
                                               ,CASE WHEN '" + row1[3].ToString() + @"' = '' THEN NULL ELSE '" + row1[3].ToString() + @"' END
                                               ,CASE WHEN '" + row1[4].ToString() + @"' = '' THEN NULL ELSE '" + row1[4].ToString() + @"' END
                                               ,CASE WHEN '" + row1[5].ToString() + @"' = '' THEN NULL ELSE '" + row1[5].ToString() + @"' END
                                               ,CASE WHEN '" + row1[6].ToString() + @"' = '' THEN NULL ELSE '" + row1[6].ToString() + @"' END
                                               ,CASE WHEN '" + row1[7].ToString() + @"' = '' THEN NULL ELSE '" + row1[7].ToString() + @"' END
                                               ,CASE WHEN '" + row1[8].ToString() + @"' = '' THEN NULL ELSE '" + row1[8].ToString() + @"' END
                                               ,CASE WHEN '" + row1[9].ToString() + @"' = '' THEN NULL ELSE '" + row1[9].ToString() + @"' END
                                               ,CASE WHEN '" + row1[10].ToString() + @"' = '' THEN NULL ELSE '" + row1[10].ToString() + @"' END
                                               ,CASE WHEN '" + row1[11].ToString() + @"' = '' THEN NULL ELSE '" + row1[11].ToString() + @"' END
                                               ,CASE WHEN '" + row1[12].ToString() + @"' = '' THEN NULL ELSE '" + row1[12].ToString() + @"' END
                                               ,CASE WHEN '" + row1[13].ToString() + @"' = '' THEN NULL ELSE '" + row1[13].ToString() + @"' END
                                               ,CASE WHEN '" + row1[14].ToString() + @"' = '' THEN NULL ELSE '" + row1[14].ToString() + @"' END
                                               ,CASE WHEN '" + row1[15].ToString() + @"' = '' THEN NULL ELSE '" + row1[15].ToString() + @"' END
                                               ,CASE WHEN '" + row1[16].ToString() + @"' = '' THEN NULL ELSE '" + row1[16].ToString() + @"' END
                                               ,CASE WHEN '" + row1[18].ToString() + @"' = '' THEN NULL ELSE '" + row1[18].ToString() + @"' END
                                               ,CASE WHEN '" + row1[19].ToString() + @"' = '' THEN NULL ELSE '" + row1[19].ToString() + @"' END
                                               ,CASE WHEN '" + row1[20].ToString() + @"' = '' THEN NULL ELSE '" + row1[20].ToString() + @"' END
                                               ,CASE WHEN '" + row1[21].ToString() + @"' = '' THEN NULL ELSE '" + row1[21].ToString() + @"' END
                                               ,CASE WHEN '" + _Code + @"' = '' THEN NULL ELSE '" + _Code + @"' END
                                               ,CASE WHEN '" + row1[17].ToString() + @"' = '' THEN NULL ELSE '" + row1[17].ToString() + @"' END
                                      ";
                
                        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _InsertData);


                    i++;
                    // Excel Progress Monitoring
                    Application.DoEvents();
                    //_Count++;
                    tssDataStatus.Text = "Reading Data : (" + i + " / " + _RowCount + ") : " + row1[1].ToString().Replace("'", "''") + "  : Data Progress ( " + Math.Round(((i / _RowCount) * 100), 2) + " % ) ";
                    pbDataProgress.Value = Convert.ToInt32(((i / _RowCount) * 100));
                }

            }
        }




        displayData();
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

    private void btnPrint_Click(object sender, EventArgs e)
    {

        if (cboBranch.Text == "")
        {
            clsDeclaration.sBranch = "";
        }
        else
        {
            clsDeclaration.sBranch = cboBranch.Text.Substring(0, 8);
        }

        clsDeclaration.sDateFrom = dtpFrom.Value.Date;
        clsDeclaration.sDateTo = dtpTo.Value.Date;

        clsDeclaration.sReportTag = 6;





        clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Attendance Monitoring A.rpt";

        frmReportViewer frmReportViewer = new frmReportViewer();
        //frmReportViewer.MdiParent = frmMainWindow;
        frmReportViewer.Show();


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

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void confirmUndertimeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //string input = "";
        //clsFunctions.ShowInputDialog(ref input);
        //MessageBox.Show(input);

        //string _SQLSyntax;
        //_SQLSyntax = @"UPDATE A SET
        //                   ,A.[UnderApproved] = 'Y'
        //                FROM DailyAttendance A
        //                WHERE A.EmployeeNo = '" + sEmployeeNo + @"' AND A.TransDate = '" + sTransDate + @"'
        //      ";

        //clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);
        //MessageBox.Show("Data Updated");
        //button1_Click(sender, e);



    }

    private void confirmAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
    {
 
        string input = "";
        if (clsFunctions.ShowInputDialog(ref input) == DialogResult.Cancel)
        {
            return;
        }

        if (input == "")
        {
            return;
        }

        string _SQLSyntax;
        _SQLSyntax = @"UPDATE A SET
                            A.[Comment] = '" + input  + @"'
                        FROM DailyAttendance A
                        WHERE A.EmployeeNo = '" + sEmployeeNo + @"' AND A.TransDate = '" + sTransDate + @"'
              ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


        string _Syntax = "";
        DataTable _DataTable;
        _Syntax = @"SELECT * FROM OCMP Z WHERE Z.Active = '1' AND Z.CompCode IN (SELECT A.CompCode FROM Employees A  WHERE A.EmployeeNo = '" + sEmployeeNo + "')";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Syntax);


        clsDeclaration.sServer = clsSQLClientFunctions.GetData(_DataTable, "DBServer", "0");
        clsDeclaration.sCompany = clsSQLClientFunctions.GetData(_DataTable, "DBName", "0");
        clsDeclaration.sUsername = clsSQLClientFunctions.GetData(_DataTable, "DBUsername", "0");
        clsDeclaration.sPassword = clsSQLClientFunctions.GetData(_DataTable, "DBPassword", "0");

        clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                       clsDeclaration.sServer, clsDeclaration.sCompany,
                                       clsDeclaration.sUsername, clsDeclaration.sPassword
                                    );

        _SQLSyntax = @"UPDATE A SET
                            A.[Comment] = '" + input + @"'
                        FROM DailyTimeDetails A
                        WHERE A.EmployeeNo = '" + sEmployeeNo + @"' AND A.TransDate = '" + sTransDate + @"'
              ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



        MessageBox.Show("Data Updated");
        button1_Click(sender, e);
    }

    private void approvedToolStripMenuItem_Click(object sender, EventArgs e)
    {



        string _SQLSyntax;
        _SQLSyntax = @"UPDATE A SET
                            A.[UTApproved] = 'Y'
                        FROM DailyAttendance A
                        WHERE A.EmployeeNo = '" + sEmployeeNo + @"' AND A.TransDate = '" + sTransDate + @"'
              ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


        string _Syntax = "";
        DataTable _DataTable;
        _Syntax = @"SELECT * FROM OCMP Z WHERE Z.Active = '1' AND Z.CompCode IN (SELECT A.CompCode FROM Employees A  WHERE A.EmployeeNo = '" + sEmployeeNo + "')";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Syntax);


        clsDeclaration.sServer = clsSQLClientFunctions.GetData(_DataTable, "DBServer", "0");
        clsDeclaration.sCompany = clsSQLClientFunctions.GetData(_DataTable, "DBName", "0");
        clsDeclaration.sUsername = clsSQLClientFunctions.GetData(_DataTable, "DBUsername", "0");
        clsDeclaration.sPassword = clsSQLClientFunctions.GetData(_DataTable, "DBPassword", "0");

        clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                       clsDeclaration.sServer, clsDeclaration.sCompany,
                                       clsDeclaration.sUsername, clsDeclaration.sPassword
                                    );

        _SQLSyntax = @"UPDATE A SET
                            A.[UnderApproved] = 'Y'
                        FROM DailyTimeDetails A
                        WHERE A.EmployeeNo = '" + sEmployeeNo + @"' AND A.TransDate = '" + sTransDate + @"'
              ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



        MessageBox.Show("Under Time Approved");
        button1_Click(sender, e);

    }

    private void disApproveToolStripMenuItem_Click(object sender, EventArgs e)
    {



        string _SQLSyntax;
        _SQLSyntax = @"UPDATE A SET
                            A.[UTApproved] = 'N'
                        FROM DailyAttendance A
                        WHERE A.EmployeeNo = '" + sEmployeeNo + @"' AND A.TransDate = '" + sTransDate + @"'
              ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


        string _Syntax = "";
        DataTable _DataTable;
        _Syntax = @"SELECT * FROM OCMP Z WHERE Z.Active = '1' AND Z.CompCode IN (SELECT A.CompCode FROM Employees A  WHERE A.EmployeeNo = '" + sEmployeeNo + "')";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Syntax);


        clsDeclaration.sServer = clsSQLClientFunctions.GetData(_DataTable, "DBServer", "0");
        clsDeclaration.sCompany = clsSQLClientFunctions.GetData(_DataTable, "DBName", "0");
        clsDeclaration.sUsername = clsSQLClientFunctions.GetData(_DataTable, "DBUsername", "0");
        clsDeclaration.sPassword = clsSQLClientFunctions.GetData(_DataTable, "DBPassword", "0");

        clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                       clsDeclaration.sServer, clsDeclaration.sCompany,
                                       clsDeclaration.sUsername, clsDeclaration.sPassword
                                    );

        _SQLSyntax = @"UPDATE A SET
                            A.[UnderApproved] = 'N'
                        FROM DailyTimeDetails A
                        WHERE A.EmployeeNo = '" + sEmployeeNo + @"' AND A.TransDate = '" + sTransDate + @"'
              ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



        MessageBox.Show("Under Time Approved");
        button1_Click(sender, e);

    }

    private void btnGenRecord_Click(object sender, EventArgs e)
    {
        foreach (DataRow row in dtDataTimeRecord.Rows)
        {
            string srEmployeeNo;
            string srTransDate;
            srEmployeeNo = row[0].ToString();
            srTransDate = row[2].ToString();

            double dRegHr = double.Parse(row[13].ToString());
            double dRegMin = double.Parse(row[14].ToString());
            double dAbsentHr = double.Parse(row[15].ToString());
            double dAbsentMin = double.Parse(row[16].ToString());
            double dTardyHr = double.Parse(row[17].ToString());
            double dTardyMin = double.Parse(row[18].ToString());
            double dUTHr = double.Parse(row[19].ToString());
            double dUTMin = double.Parse(row[20].ToString());
            double dExceesHr = double.Parse(row[21].ToString());
            double dExceesMin = double.Parse(row[22].ToString());

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
            
            string strUpdateDTR;
            strUpdateDTR = @"UPDATE A SET

                               [ApprovedOT] = 0
                              ,[Tardiness] = 0
                              ,[Holiday] = 0
                              ,[ApprovedOTMins] = 0
                              ,[HolidayMins] = 0
                              ,[NightDiffMins] = 0
                              ,[SickLeaveMins] = 0
                              ,[VacationLeaveMins] = 0
                              ,[PaternityLeaveMins] = 0
                              ,[NightDiffOTMins] = 0
                              ,[BaseRegularMins] = 0
                              ,[ApprovedNDiffOTHrs] = 0
                              ,[ApprovedNDiffOTMins] = 0
                              ,[NextDayTime] = 0
                              ,[EarlyOTHrs] = 0
                              ,[EarlyOTMins] = 0
                              ,[ApprovedEarlyOTHrs] = 0
                              ,[ApprovedEarlyOTMins] = 0
                              ,[DispTimeIn] = ''
                              ,[DispTimeOut] = ''

                              ,[NightDiff] = 0
                              ,[SickLeave] = 0
                              ,[VacationLeave] = 0
                              ,[PaternityLeave] = 0
                              ,[InOutStatus] = 0
                              ,[NightDiffOT] = 0
                              ,[LeaveCode] = ''
                              ,[OtherDeduction] = 0
                              ,[DeductibleAmount] = 0
                              ,[ProjectCode] = ''

                            ,A.[RegularHrs] = '" + dRegHr + @"'
                            ,A.[Absences] = '" + dAbsentHr + @"'
                            ,A.[ExcessHrs] = '" + dExceesHr + @"'
                            --,A.[Holiday] = '" + "" + @"'
                            ,A.[BaseRegularHrs] = '8'
                            ,A.[RegularMins] = '" + dRegMin + @"'
                            ,A.[AbsencesMins] = '" + dAbsentMin + @"'
                            ,A.[TardinessMins] = '" + ((dTardyHr * 60) + dTardyMin)  + @"'
                            ,A.[ExcessMins] = '" + dExceesMin + @"'
                            --,A.[HolidayMins] = '" + "" + @"'
                            ,[OtherDeductionMins] = '" + ((dUTHr * 60) + dUTMin) + @"'

                        FROM DailyTimeDetails A
                        WHERE A.EmployeeNo = '" + srEmployeeNo + @"' AND A.TransDate = '" + srTransDate + @"'
                          ";

            clsSQLClientFunctions.GlobalExecuteCommand(strConnection, strUpdateDTR);
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, strUpdateDTR);
        }
    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void label6_Click(object sender, EventArgs e)
    {

    }
}