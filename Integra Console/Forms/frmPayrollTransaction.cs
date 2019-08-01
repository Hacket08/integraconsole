using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmPayrollTransaction : Form
{
    public static string _gCompany;
    public static string _gEmpCode;
    public static string _gEmpName;
    public static string _gPayrollPeriod;


    public frmPayrollTransaction()
    {
        InitializeComponent();
    }

    private void frmPayrollTransaction_Load(object sender, EventArgs e)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_gCompany);


        txtCompany.Text = _gCompany;
        txtEmpCode.Text = _gEmpCode;
        txtEmpName.Text = _gEmpName;
        txtPayrollPeriod.Text = _gPayrollPeriod;


        string _sqlSyntax = @"SELECT A.AccountCode, B.AccountDesc, A.NoOfHrs, A.NoOfMins, A.TotalHrs, A.TotalDays 
                                        FROM PayrollTrans01 A INNER JOIN AccountCode B ON A.AccountCode = B.AccountCode
                                        WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.PayrollPeriod = '" + txtPayrollPeriod.Text + @"'";
        DataTable _tblDisplay;
        _tblDisplay = clsSQLClientFunctions.DataList(_ConCompany, _sqlSyntax);

        clsFunctions.DataGridViewSetup(dgvDisplay, _tblDisplay, "PayrollTransaction");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnUpload_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in dgvDisplay.Rows)
        {
            string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);

            string _AccountCode = row.Cells[0].Value.ToString();
            string _TotalHrs = row.Cells[4].Value.ToString();
            string _TotalDays = row.Cells[5].Value.ToString();

            string _UpdateSQL;
            _UpdateSQL = @"UPDATE A SET A.TotalHrs = '" + _TotalHrs + @"', A.TotalDays = '" + _TotalDays + @"'
                                    FROM PayrollTrans01 A INNER JOIN AccountCode B ON A.AccountCode = B.AccountCode
                                    WHERE A.EmployeeNo = '" + txtEmpCode.Text + @"' AND A.PayrollPeriod = '" + txtPayrollPeriod.Text + @"'
                                    AND A.AccountCode = '" + _AccountCode + @"'";

            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _UpdateSQL);
        }

        MessageBox.Show("Data successfully added!");
    }
}