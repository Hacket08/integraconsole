using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


public partial class frmAddOT : Form
{


    public static string _TransDate;

    public static string _EmpCode;
    public static string _EmpName;
    public static string _ExcessHr;
    public static string _ExcessMin;
    public static string _ApprovedHr;
    public static string _ApprovedMin;
    
    public static string _sConnection;
    private static DataTable _SchedList = new DataTable();
    public frmAddOT()
    {
        InitializeComponent();
    }

    private void frmAddOT_Load(object sender, EventArgs e)
    {

        txtEmpCode.Text = _EmpCode;
        txtEmpName.Text = _EmpName;
        txtExcessHr.Text = _ExcessHr;
        txtExcessMin.Text = _ExcessMin;
        txtApprovedHr.Text = _ApprovedHr;
        txtApprovedMin.Text = _ApprovedMin;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string UpdateSyntax = @"UPDATE A SET
            A.ApprovedOT = '" + txtApprovedHr.Text + @"' ,  
            A.ApprovedOTMins =  '" + txtApprovedMin.Text + @"'
            FROM DailyTimeDetails A
            WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.TransDate = '" + _TransDate + @"'";
        
        clsSQLClientFunctions.GlobalExecuteCommand(_sConnection, UpdateSyntax);

        MessageBox.Show("Overtime Approved");
        Close();
    }
}
