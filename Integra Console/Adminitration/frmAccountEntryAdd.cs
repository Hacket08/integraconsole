using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmAccountEntryAdd : Form
{
    public static string _gAddType;
    public static string _gEmployeeNo;
    public static string _gEmployeeName;
    public static string _gAmount;
    public static string _gRemarks;

    public static string _gPayrollPeriod;
    public static string _gAccountCode;
    public static string _gAccountName;

    public static string _gCompany;

    public frmAccountEntryAdd()
    {
        InitializeComponent();
    }

    private void frmAccountEntryAdd_Load(object sender, EventArgs e)
    {
        txtLoanAccountCode.Text = _gAccountCode;
        txtDescription.Text = _gAccountName;
        txtAmount.Text = _gAmount;
        txtRemarks.Text = _gRemarks;


        //string _sqlList = "";
        //_sqlList = @"SELECT A.[Company]
        //                    FROM vwsEmployees A WHERE A.EmployeeNo = '" + _gEmployeeNo + @"'";
        //_gCompany = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Company");
    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string _sqlList = "";
        _sqlList = @"SELECT A.[Company]
                            FROM vwsEmployees A WHERE A.EmployeeNo = '" + _gEmployeeNo + @"'";
        _gCompany = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "Company");
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_gCompany);

        switch (_gAddType)
        {
            case "ADD":
                string _sqlADD = @"
                                                        INSERT INTO [dbo].[PayrollTrans02]
                                                                   ([PayrollPeriod]
                                                                   ,[EmployeeNo]
                                                                   ,[AccountCode]
                                                                   ,[Amount]
                                                                   ,[Remarks])
                                                             VALUES
                                                                   ('" + _gPayrollPeriod + @"'
                                                                   ,'" + _gEmployeeNo + @"'
                                                                   ,'" + txtLoanAccountCode.Text + @"'
                                                                   ,'" + txtAmount.Text + @"'
                                                                   ,'" + txtRemarks.Text + @"')
                                                ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlADD);
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
                break;
        }
    }
}
