using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmAddAccountEntryByEmployee : Form
{
    public static string _gAddType;
    public static string _gEmployeeNo;
    public static string _gEmployeeName;
    public static string _gAmount;
    public static string _gRemarks;

    public static string _gPayrollPeriod;
    public static string _gAccountCode;
    public static string _gCompany;

    public frmAddAccountEntryByEmployee()
    {
        InitializeComponent();
    }

    private void frmAddAccountEntryByEmployee_Load(object sender, EventArgs e)
    {
        txtEmpCode.Text = _gEmployeeNo;
        txtEmpName.Text = _gEmployeeName;
        txtAmount.Text = _gAmount;
        txtRemarks.Text = _gRemarks;


        //string _sqlList = "";
        //_sqlList = @"SELECT A.[Company]
        //                    FROM vwsEmployees A WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"'";
        //_gCompany = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Company");
    }

    private void button1_Click(object sender, EventArgs e)
    {
        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "EmployeeList";
        frmDataList.ShowDialog();

        txtEmpCode.Text = frmDataList._gEmployeeNo;

        string _sqlList = "";

        _sqlList = @"SELECT A.[EmployeeName]
                            FROM vwsEmployees A WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"'";
        txtEmpName.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "EmployeeName");
    }

    private void btnSave_Click(object sender, EventArgs e)
    {

        string _sqlList = "";
        _sqlList = @"SELECT A.[Company]
                            FROM vwsEmployees A WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"'";
        _gCompany = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Company");
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_gCompany);

        switch (_gAddType)
        {
            case "ADD":
                string _sqlADD = @"
                                                       
                                    DELETE FROM [PayrollEntry] WHERE [EmployeeNo] = '" + txtEmpCode.Text + @"' AND [PayrollPeriod] = '" + _gPayrollPeriod + @"'

									INSERT INTO [PayrollEntry]
												([PayrollPeriod]
                                                  ,[EmployeeNo])
												VALUES
												(
												'" + _gPayrollPeriod + @"',
												'" + txtEmpCode.Text + @"'
												)

                                                        INSERT INTO [dbo].[PayrollTrans02]
                                                                   ([PayrollPeriod]
                                                                   ,[EmployeeNo]
                                                                   ,[AccountCode]
                                                                   ,[Amount]
                                                                   ,[Remarks])
                                                             VALUES
                                                                   ('" + _gPayrollPeriod  + @"'
                                                                   ,'" + txtEmpCode.Text + @"'
                                                                   ,'" + _gAccountCode + @"'
                                                                   ,'" + txtAmount.Text + @"'
                                                                   ,'" + txtRemarks.Text + @"')
                                                ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlADD);
                MessageBox.Show("Data Successfully Added");
                break;
            case "EDIT":
                string _sqlEDIT = @"
                                                    UPDATE [dbo].[PayrollTrans02]
                                                       SET [Amount] = '" + txtAmount.Text + @"'
                                                          ,[Remarks] = '" + txtRemarks.Text + @"'
                                                     WHERE [PayrollPeriod] = '" + _gPayrollPeriod + @"'
                                                          AND [EmployeeNo] = '" + _gEmployeeNo + @"'
                                                          AND [AccountCode] = '" + _gAccountCode + @"'
                                                ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlEDIT);
                MessageBox.Show("Data Successfully Updated");
                break;
        }



        Close();
    }
}