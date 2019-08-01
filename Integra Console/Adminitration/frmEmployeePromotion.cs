using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmEmployeePromotion : Form
{

    public static string _EmployeeCode;
    public static string _EmployeeName;
    public static string _Company;


    public frmEmployeePromotion()
    {
        InitializeComponent();
    }

    private void frmEmployeePromotion_Load(object sender, EventArgs e)
    {
        txtEmpCode.Text = _EmployeeCode;
        txtEmpName.Text = _EmployeeName;
        txtCompany.Text = _Company;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);
        string _empPromotion = "";
        _empPromotion = @"
                                                INSERT INTO [dbo].[EmployeePromotion]
                                                           ([EmployeeNo]
                                                           ,[Date]
                                                           ,[Position]
                                                           ,[LeaveAllowed]
                                                           ,[Remarks])
                                                     VALUES
                                                           ('" + txtEmpCode.Text + @"'
                                                           ,'" + dtDatePromoted.Value + @"'
                                                           ,'" + txtPosition.Text + @"'
                                                           ,'" + txtLeaveAllowed.Text + @"'
                                                           ,'" + txtRemarks.Text + @"')
                                              ";

        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _empPromotion);
        MessageBox.Show("Data Successfully Added");
        Close();
    }
}