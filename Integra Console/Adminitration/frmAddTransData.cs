using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
public partial class frmAddTransData : Form
{

    public static string _EmpCode;
    public static string _EmpName;
    public static string _TransDate;
    public static string _TransTime;
    public static string _TransType;


    public static string _AddType;
    public frmAddTransData()
    {
        InitializeComponent();
    }

    private void frmAddTransData_Load(object sender, EventArgs e)
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

        if (_AddType == "0") //ADD
        {
            cboType.Items.Add("IN");
            cboType.Items.Add("OUT");

            txtEmpCode.Text = _EmpCode;
            txtEmpName.Text = _EmpName;
            dtpDate.Enabled = true;
            dtpTime.Enabled = true;

            txtLog.Text = "Manual Input";
            dtpDate.Text = _TransDate;
            dtpTime.Text = "12:00:00 AM";
        }


        if (_AddType == "1" || _AddType == "2") //UPDATE
        {
            cboType.Items.Add("IN");
            cboType.Items.Add("OUT");

            txtEmpCode.Text = _EmpCode;
            txtEmpName.Text = _EmpName;
            dtpDate.Enabled = false;
            dtpTime.Enabled = false;

            dtpDate.Text = _TransDate;
            dtpTime.Text = _TransTime;

            txtLog.Text = _TransType;
            txtLog.Text = _TransType;


            if (_AddType == "2")
            {
                txtLog.Text = "Manual Input";
                dtpTime.Enabled = true;
                dtpTime.Text = "12:00:00 AM";
            }



     

        }





    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string _sysDB = ConfigurationManager.AppSettings["DBName"];
        string _sysDBServer = ConfigurationManager.AppSettings["DBServer"];


        string _Syntax = "";

        if (_AddType == "0" || _AddType == "2") //ADD
        {
            _Syntax = "INSERT INTO [" + _sysDBServer + @"].[" + _sysDB + @"].dbo.[DailyTrans] ([EmployeeNo],[TransType],[TransDate],[TransTime],[TransType02]) VALUES ('" + txtEmpCode.Text + "','2','" + dtpDate.Text + "','" + dtpDate.Text + " " + dtpTime.Text + "','" + cboType.SelectedIndex + "')";
        }


        if (_AddType == "1") //UPDATE
        {
            _Syntax = @"
                UPDATE A SET A.[TransType02] = '" + cboType.SelectedIndex + @"'
                  FROM  [" + _sysDBServer + @"].[" + _sysDB + @"].dbo.DailyTrans A
                  WHERE A.[EmployeeNo] = '" + txtEmpCode.Text + @"'
                    AND A.[TransDate] = '" + dtpDate.Text + @"'
                    AND A.[TransTime] = '" + dtpDate.Text + " " + dtpTime.Text + @"'
                        ";
        }


        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _Syntax);

        if (_AddType == "0")
        {
            MessageBox.Show("Data Successfully Updated");
        }
        else
        {
            MessageBox.Show("Data Successfully Added");
        }
        Close();



    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
