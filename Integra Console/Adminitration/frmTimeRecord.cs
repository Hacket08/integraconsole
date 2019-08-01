using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
public partial class frmTimeRecord : Form
{

 

    public static string _EmpCode;
    public static string _EmpName;
    public static string _TransDate;
    public static string _TransTime;
    public static string _TransType;
    public frmTimeRecord()
    {
        InitializeComponent();
    }

    private void frmTimeRecord_Load(object sender, EventArgs e)
    {

        string _Syntax = "";
        DataTable _DataTable;
        _Syntax = @"SELECT * FROM OCMP Z WHERE Z.Active = '1' AND Z.CompCode IN (SELECT A.CompCode FROM Employees A  WHERE A.EmployeeNo = '" + _EmpCode + "')";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Syntax);


        clsDeclaration.sServer = clsSQLClientFunctions.GetData(_DataTable, "DBServer", "0");
        clsDeclaration.sCompany = clsSQLClientFunctions.GetData(_DataTable, "DBName", "0");
        clsDeclaration.sUsername = clsSQLClientFunctions.GetData(_DataTable, "DBUsername", "0");
        clsDeclaration.sPassword = clsSQLClientFunctions.GetData(_DataTable, "DBPassword", "0");

        clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                       clsDeclaration.sServer, clsDeclaration.sCompany,
                                       clsDeclaration.sUsername, clsDeclaration.sPassword
                                    );

        txtEmpCode.Text = _EmpCode;
        txtEmpName.Text = _EmpName;
        dtpFrom.Text = _TransDate;
        dtpTo.Text = _TransDate;


        displayData();
    }

    private void displayData()
    {
        try
        {
            string _sysDB = ConfigurationManager.AppSettings["DBName"];
            string _sysDBServer = ConfigurationManager.AppSettings["DBServer"];

            DataTable _DataTable;
            string _SQLSyntax;
            _SQLSyntax = @"
                            SELECT A.[EmployeeNo]

                                  ,A.[TransDate]
                                  ,A.[TransTime]
                                  ,CASE WHEN A.[TransType] = '0' THEN 'IN' 
			                            WHEN A.[TransType] = '1' THEN 'OUT' 
			                            WHEN A.[TransType] = '2' THEN 'Manual Input' END AS [System Trans]
                                  ,CASE WHEN A.[TransType] = A.[TransType02] THEN '' ELSE (CASE WHEN A.[TransType02] = '0' THEN 'IN' ELSE 'OUT' END) END  AS [Edited Trans]
                                  ,A.Remarks
                              FROM  [" + _sysDBServer + @"].[" + _sysDB  + @"].dbo.[DailyTrans] A
                            WHERE A.[TransDate] BETWEEN '" + dtpFrom.Text  + @"' AND '" + dtpTo.Text + @"' AND A.[EmployeeNo] = '" + txtEmpCode.Text + @"' 
                            ORDER BY A.[TransTime] ASC, A.[TransType02] DESC
                          ";
            
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _SQLSyntax);
            clsFunctions.DataGridViewSetup(dataGridView1, _DataTable);
        }
        catch { }

    }

    private void dtpFrom_ValueChanged(object sender, EventArgs e)
    {
        displayData();
    }

    private void dtpTo_ValueChanged(object sender, EventArgs e)
    {
        displayData();
    }


    private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;

                _EmpCode = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                _TransDate = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                _TransTime = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                _TransType = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
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

    private void addToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmAddTransData frmAddTransData = new frmAddTransData();

        frmAddTransData._EmpCode = _EmpCode;
        frmAddTransData._EmpName = _EmpName;
        frmAddTransData._TransDate = _TransDate;
        frmAddTransData._TransTime = _TransTime;
        frmAddTransData._TransType = _TransType;
        
        frmAddTransData._AddType = "0";
        frmAddTransData.ShowDialog();

        displayData();
    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmAddTransData frmAddTransData = new frmAddTransData();

        frmAddTransData._EmpCode = _EmpCode;
        frmAddTransData._EmpName = _EmpName;
        frmAddTransData._TransDate = _TransDate;
        frmAddTransData._TransTime = _TransTime;
        frmAddTransData._TransType = _TransType;

        frmAddTransData._AddType = "1";
        frmAddTransData.ShowDialog();

        displayData();
    }

    private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
    {

        string _Syntax = "";
        DataTable _DataTable;

        _Syntax = @"SELECT * FROM DailyTrans  A
                  WHERE A.[EmployeeNo] = '" + _EmpCode + @"'
                    AND A.[TransDate] = '" + _TransDate + @"'
                    AND A.[TransTime] = '" + _TransTime + @"'
                        ";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sCompanyConnection, _Syntax);

       string _Type = clsSQLClientFunctions.GetData(_DataTable, "TransType", "0");

        if (_Type != "2")
        {
            MessageBox.Show("Cannot Delete This Transaction");
            return;
        }


        _Syntax = @"DELETE FROM DailyTrans 
                  WHERE [EmployeeNo] = '" + _EmpCode + @"'
                    AND [TransDate] = '" + _TransDate + @"'
                    AND [TransTime] = '" + _TransTime + @"'
                        ";
        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _Syntax);
        

        _Syntax = @"DELETE FROM DailyTrans 
                  WHERE [EmployeeNo] = '" + _EmpCode + @"'
                    AND [TransDate] = '" + _TransDate + @"'
                    AND [TransTime] = '" + _TransTime + @"'
                        ";
        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Syntax);


        displayData();
    }
}