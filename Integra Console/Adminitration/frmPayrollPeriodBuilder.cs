using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;


public partial class frmPayrollPeriodBuilder : Form
{
    public frmPayrollPeriodBuilder()
    {
        InitializeComponent();
    }

    private void frmPayrollPeriodBuilder_Load(object sender, EventArgs e)
    {
        for (int i = ((DateTime.Now).Year - 10); i < ((DateTime.Now).Year + 20); i++)
        {
            cboYear.Items.Add(i);
        }
        cboYear.Text = (DateTime.Now).Year.ToString();


        for (int i = 1; i < 13; i++)
        {
            cboMonth.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
        }
        cboMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Now).Month);



        cboType.Items.Add("  -- Monthly");
        cboType.Items.Add("");
        cboType.Items.Add("  -- Semi Monthly");
        cboType.Items.Add("       -- First Payroll");
        cboType.Items.Add("       -- Second Payroll");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void cboType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _sDate;
        string _eDate;


        string _sqlList;
        _sqlList = "SELECT A.[VariableValue] FROM [SysVariables] A WHERE A.[VariableName] = 'PayrollSemi01'";
        string _PayrollSemi01 = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "VariableValue");
        _sqlList = "SELECT A.[VariableValue] FROM [SysVariables] A WHERE A.[VariableName] = 'PayrollSemi02'";
        string _PayrollSemi02 = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "VariableValue");


        dtpStartDate.Enabled = true;
        dtpEndDate.Enabled = true;

        switch (cboType.Text)
        {
            case "  -- Monthly":
                txtPayrollCode.Text = cboYear.Text + "-" + (cboMonth.SelectedIndex + 1).ToString("00");

                DateTime startDate = new DateTime(int.Parse(cboYear.Text), int.Parse((cboMonth.SelectedIndex + 1).ToString()), 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);


                dtpStartDate.Checked = true;
                dtpEndDate.Checked = true;
                dtpStartDate.Text = startDate.ToString();
                dtpEndDate.Text = endDate.ToString();

                break;
            case "       -- First Payroll":
                txtPayrollCode.Text = cboYear.Text + "-" + (cboMonth.SelectedIndex + 1).ToString("00") + "-A";



                if ((cboMonth.SelectedIndex + 1) == 1)
                {
                    _sDate = "12" + "/" + _PayrollSemi01 + "/" + (double.Parse(cboYear.Text) - 1);
                }
                else
                {
                    _sDate = cboMonth.SelectedIndex + "/" + _PayrollSemi01 + "/" + cboYear.Text;
                }

                _eDate = (cboMonth.SelectedIndex + 1) + "/" + _PayrollSemi02 + "/" + cboYear.Text;
                
                dtpStartDate.Text = _sDate.ToString();
                dtpEndDate.Text = _eDate.ToString();

                break;
            case "       -- Second Payroll":
                txtPayrollCode.Text = cboYear.Text + "-" + (cboMonth.SelectedIndex + 1).ToString("00") + "-B";
   

                _sDate = (cboMonth.SelectedIndex + 1) + "/" + (double.Parse(_PayrollSemi02) + 1) + "/" + cboYear.Text;
                _eDate = (cboMonth.SelectedIndex + 1) + "/" + (double.Parse(_PayrollSemi01) - 1) + "/" + cboYear.Text;

                dtpStartDate.Text = _sDate.ToString();
                dtpEndDate.Text = _eDate.ToString();
                break;
            default:
                txtPayrollCode.Text = "";
                dtpStartDate.Checked = false;
                dtpEndDate.Checked = false;

                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;

                break;
        }

        double _NooOfRestDay = CountDaysOfWeek(DayOfWeek.Sunday, DateTime.Parse(dtpStartDate.Text), DateTime.Parse(dtpEndDate.Text));
        double _WorkDays = (DateTime.Parse(dtpEndDate.Text) - DateTime.Parse(dtpStartDate.Text)).TotalDays;

        txtWorkDays.Text =  (_WorkDays - _NooOfRestDay).ToString();
    }

    private int CountDaysOfWeek(DayOfWeek dayOfWeek, DateTime date1, DateTime date2)
    {
        TimeSpan ts = date2 - date1;                       // Total duration
        int count = (int)Math.Floor(ts.TotalDays / 7);   // Number of whole weeks
        int remainder = (int)(ts.TotalDays % 7);         // Number of remaining days
        int sinceLastDay = (int)(date2.DayOfWeek - dayOfWeek);   // Number of days since last [day]
        if (sinceLastDay < 0) sinceLastDay += 7;         // Adjust for negative days since last [day]

        // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
        if (remainder >= sinceLastDay) count++;

        return count;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string _sqlInsert = "EXEC usrPayrollPeriod '"  + txtPayrollCode.Text + "','" + dtpStartDate.Text + "','" + dtpEndDate.Text + "','" + txtWorkDays.Text + "'";
        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlInsert);

        MessageBox.Show("Payroll Period Successfully Added.");
    }
}
