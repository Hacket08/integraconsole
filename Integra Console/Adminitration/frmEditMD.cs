using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


public partial class frmEditMD : Form
{

    public static string _Code;
    public static string _Name;
    public static string _Department;

    public frmEditMD()
    {
        InitializeComponent();
    }

    private void frmEditMD_Load(object sender, EventArgs e)
    {


        cmbEmployeeStatus.Items.Clear();
        cmbEmployeeStatus.Items.Add("Regular");
        cmbEmployeeStatus.Items.Add("Probitionary");
        cmbEmployeeStatus.Items.Add("Contractual");
        cmbEmployeeStatus.Items.Add("Finished Contract");
        cmbEmployeeStatus.Items.Add("Resigned");
        cmbEmployeeStatus.Items.Add("Temporary");
        cmbEmployeeStatus.Items.Add("Terminated");
        cmbEmployeeStatus.Items.Add("AWOL");
        cmbEmployeeStatus.Items.Add("Retired");
        cmbEmployeeStatus.Items.Add("Deceased");
        cmbEmployeeStatus.Items.Add("Back - Out");


        txtEmpCode.Text = _Code;
        txtEmpName.Text = _Name;
        txtName.Text =  _Department;


        DataTable _DataTable;
        string _SQLSyntax;






        _SQLSyntax = "SELECT A.Code FROM ODPT A WHERE A.Name = '" + txtName.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        txtCode.Text = clsSQLClientFunctions.GetData(_DataTable, "Code", "0");

        
        
        _SQLSyntax = "SELECT DISTINCT A.Area FROM [OBLP] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }


        _SQLSyntax = "SELECT DISTINCT A.Area FROM [OBLP] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboAssignArea.Items.Clear();
        cboAssignArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboAssignArea.Items.Add(row[0].ToString());
        }


        _SQLSyntax = "SELECT CONCAT(A.Code,' - ',A.Name) AS Name FROM OBLP A ORDER BY A.Code";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboBranch.Items.Clear();
        cboBranch.Items.Add("");
        cboAssignBranch.Items.Clear();
        cboAssignBranch.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboAssignBranch.Items.Add(row[0].ToString());
            cboBranch.Items.Add(row[0].ToString());
        }



        _SQLSyntax = "SELECT CONCAT(A.Code,' - ',A.Name) AS Name FROM OPST A ORDER BY A.Code";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboPosition.Items.Clear();
        cboPosition.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboPosition.Items.Add(row[0].ToString());
        }


        try
        {
            _SQLSyntax = "SELECT  A.Area FROM OBLP A WHERE CONCAT(A.Company,A.Code) = '" + txtName.Text.Substring(0, 8) + "'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            cboArea.Text = clsSQLClientFunctions.GetData(_DataTable, "Area", "0");


            _SQLSyntax = "SELECT  CONCAT(A.Company,A.Code,' - ',A.Name) AS Name FROM OBLP A WHERE CONCAT(A.Company,A.Code) = '" + _Department.Substring(0, 8) + "'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            cboBranch.Text = clsSQLClientFunctions.GetData(_DataTable, "Name", "0");

            _SQLSyntax = "SELECT CONCAT(A.Code,' - ',A.Name) AS Name FROM OPST A WHERE A.Code = '" + _Department.Substring(8, 4) + "'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            cboPosition.Text = clsSQLClientFunctions.GetData(_DataTable, "Name", "0");


        }
        catch { }



        _SQLSyntax = "SELECT A.Remarks, A.AsBranchCode, A.DateRegular, ISNULL(A.TerminationStatus, A.EmpStatus) AS EmpStatus, A.DateFinish FROM vwsEmployees A WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        txtRemarks.Text = clsSQLClientFunctions.GetData(_DataTable, "Remarks", "0");
        string _AsBranchCode = clsSQLClientFunctions.GetData(_DataTable, "AsBranchCode", "0");


        if(clsSQLClientFunctions.GetData(_DataTable, "DateRegular", "0") != "")
        {
            txtRegularDate.Text = DateTime.Parse(clsSQLClientFunctions.GetData(_DataTable, "DateRegular", "0")).ToString("MM/dd/yyyy");
        }


        if (clsSQLClientFunctions.GetData(_DataTable, "DateFinish", "0") != "")
        {
            txtLastDate.Text = DateTime.Parse(clsSQLClientFunctions.GetData(_DataTable, "DateFinish", "0")).ToString("MM/dd/yyyy");
        }


        cmbEmployeeStatus.SelectedIndex = int.Parse(clsSQLClientFunctions.GetData(_DataTable, "EmpStatus", "1"));


        _SQLSyntax = "SELECT  A.Area FROM OBLP A  WHERE A.BranchCode =  '" + _AsBranchCode + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboAssignArea.Text = clsSQLClientFunctions.GetData(_DataTable, "Area", "0");


        _SQLSyntax = "SELECT  CONCAT(A.Company,A.Code,' - ',A.Name) AS Name FROM OBLP A WHERE CONCAT(A.Company,A.Code) = '" + _AsBranchCode  + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboAssignBranch.Text = clsSQLClientFunctions.GetData(_DataTable, "Name", "0");


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

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        GenerateCode();

    }


    private void GenerateCode()
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

        string _Position;
        if (cboPosition.Text == "")
        {
            _Position = "";
        }
        else
        {
            _Position = cboPosition.Text.Substring(0, 4);
        }


        txtName.Text = _Branch + _Position;

        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT A.Code FROM ODPT A WHERE A.Name = '" + txtName.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        txtCode.Text = clsSQLClientFunctions.GetData(_DataTable, "Code", "0");
    }

    private void cboPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateCode();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string _Syntax = "";
        DataTable _DataTable;
        _Syntax = @"SELECT * FROM OCMP Z WHERE Z.Active = '1' AND Z.CompCode IN (SELECT A.Company FROM vwsEmployees A  WHERE A.EmployeeNo = '" + txtEmpCode.Text + "')";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _Syntax);


        clsDeclaration.sServer = clsSQLClientFunctions.GetData(_DataTable, "DBServer", "0");
        clsDeclaration.sCompany = clsSQLClientFunctions.GetData(_DataTable, "DBName", "0");
        clsDeclaration.sUsername = clsSQLClientFunctions.GetData(_DataTable, "DBUsername", "0");
        clsDeclaration.sPassword = clsSQLClientFunctions.GetData(_DataTable, "DBPassword", "0");

        string strConnection = clsSQLClientFunctions.GlobalConnectionString(
                                       clsDeclaration.sServer, clsDeclaration.sCompany,
                                       clsDeclaration.sUsername, clsDeclaration.sPassword
                                    );

        string _AssignBranch;
        if (cboAssignBranch.Text == "")
        {
            _AssignBranch = "";
        }
        else
        {
            _AssignBranch = cboAssignBranch.Text.Substring(0, 8);
        }




        string strUpdateDTR;


        strUpdateDTR = @"UPDATE A SET  A.[Department] = '" + txtCode.Text + @"', A.[Remarks] = '" + txtRemarks.Text + @"', A.[AsBranchCode] = '" + _AssignBranch + @"'
                                ,A.TerminationStatus = '" + cmbEmployeeStatus.SelectedIndex + @"'";
        

        switch (cmbEmployeeStatus.SelectedIndex)
        {
            case 7:
            case 8:
            case 9:
            case 10:
                strUpdateDTR = strUpdateDTR + @" 
                                            ,A.EmpStatus = '4' ";
                break;

            default:
                strUpdateDTR = strUpdateDTR + @" 
                                            ,A.EmpStatus = '" + cmbEmployeeStatus.SelectedIndex + @"' ";
                break;
        }

        if (clsFunctions.getDateValue(txtRegularDate.Text) != "")
        {
            strUpdateDTR = strUpdateDTR + @" 
                                            ,A.DateRegular = '" + txtRegularDate.Text + @"' ";
        }

        if (clsFunctions.getDateValue(txtLastDate.Text) != "")
        {
            strUpdateDTR = strUpdateDTR + @" 
                                            ,A.DateFinish = '" + txtLastDate.Text + @"' ";
        }

        strUpdateDTR = strUpdateDTR + @" 
                                          FROM  [Employees] A
                                          WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"'
                                      ";

        clsSQLClientFunctions.GlobalExecuteCommand(strConnection, strUpdateDTR);

        

        strUpdateDTR = @"UPDATE A SET  A.[Department] = '" + txtName.Text + @"', A.[Remarks] = '" + txtRemarks.Text + @"'
                              FROM  [Employees] A
                              WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"'
                          ";
        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, strUpdateDTR);

        
        MessageBox.Show("Employee Successfully Updated");
        Close();

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void cboAssignArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT CONCAT(A.Company,A.Code,' - ',A.Name) AS Branch FROM [OBLP] A WHERE A.Area = '" + cboAssignArea.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboAssignBranch.Items.Clear();
        cboAssignBranch.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboAssignBranch.Items.Add(row[0].ToString());
        }
    }
}