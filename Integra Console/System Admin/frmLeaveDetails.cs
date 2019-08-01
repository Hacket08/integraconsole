using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmLeaveDetails : Form
{

    public frmLeaveDetails()
    {
        InitializeComponent();
    }

    private void frmLeaveDetails_Load(object sender, EventArgs e)
    {
        cboYear.Items.Clear();
        int _year = DateTime.Now.Year - 5;
        while (_year <= (DateTime.Now.Year + 10))
        {
            cboYear.Items.Add(_year);
            _year++;
        }
    }

    private void label10_Click(object sender, EventArgs e)
    {

    }

    private void brw_Click(object sender, EventArgs e)
    {
        Label lbl = sender as Label;
        frmSystemData frmSystemData = new frmSystemData();
        switch (lbl.Name)
        {
            case "brwEmployeeCode":

                clsDeclaration.sQueryString = @"SELECT A.EmployeeNo AS [Code] ,A.EmployeeName AS [Name], A.[EEStat] AS [EE Status] FROM [vwsEmployeeDetails] A";
                frmSystemData._ColumnCount = 2;
                frmSystemData.ShowDialog();

                txtEmployeeNo.Text = frmSystemData._gCode;
                txtEmployeeName.Text = frmSystemData._gName;
                break;
            case "brwLeaveCode":

                clsDeclaration.sQueryString = @"SELECT A.[LeaveCode] , A.[LeaveDesc] , A.[AccountCode] FROM [LeaveTable] A";

                frmSystemData._ColumnCount = 3;
                frmSystemData.ShowDialog();
                txtLeaveCode.Text = frmSystemData._gCode;
                txtDescription.Text = frmSystemData._gName;
                txtLeaveAccountCode.Text = frmSystemData._gColumn3;
                break;
            default:
                break;
        }

    }



    public void TimeCalculate(string _TimeRecord, string _timeFormat)
    {
        
        string s;
        string[] parts;
        double i1;
        double i2;

        string _NoOfHrs = "0";
        string _NoOfMins = "0";

        string _TotalHours = "0";
        string _TotalDays = "0";


        double _ValueWH = 0;

        if (_TimeRecord != "")
        {
            double _TimeValue = 0;
            double.TryParse(_TimeRecord, out _TimeValue);
            _ValueWH = _TimeValue;

            switch (_timeFormat)
            {
                case "d":
                    s = (_ValueWH * 8).ToString("0.00000");
                    parts = s.Split('.');
                    i1 = double.Parse(parts[0]);
                    i2 = double.Parse("." + parts[1]) * 60;

                    _NoOfHrs = i1.ToString();
                    _NoOfMins = i2.ToString();
                    _TotalHours = (_ValueWH * 8).ToString("0.00000");
                    _TotalDays = _ValueWH.ToString();

                    break;
                case "h":
                    s = (_ValueWH).ToString("0.00000");
                    parts = s.Split('.');
                    i1 = double.Parse(parts[0]);
                    i2 = double.Parse("." + parts[1]) * 60;

                    _NoOfHrs = i1.ToString();
                    _NoOfMins = i2.ToString();

                    _TotalHours = _ValueWH.ToString();
                    _TotalDays = (_ValueWH / 8).ToString("0.00000");

                    break;
                case "m":
                    s = (_ValueWH / 60).ToString("0.00000");
                    parts = s.Split('.');
                    i1 = double.Parse(parts[0]);
                    i2 = double.Parse("." + parts[1]) * 60;

                    _NoOfHrs = i1.ToString();
                    _NoOfMins = i2.ToString();

                    _TotalHours = (_ValueWH / 60).ToString("0.00000");
                    _TotalDays = ((_ValueWH / 60) / 8).ToString("0.00000");
                    break;
            }


            //'" + _NoOfHrs + @"',
            //'" + _NoOfMins + @"',
            //'" + _TotalHours + @"',
            //'" + _TotalDays + @"'


            txtNoOfHrs.Text = _NoOfHrs;
            txtNoOfMins.Text = _NoOfMins;
            txtTotalHours.Text = _TotalHours;
            txtTotalDays.Text = _TotalDays;


            string _sqlEmployeeInfo = "SELECT A.DailyRate FROM vwsEmployees A WHERE A.EmployeeNo = '" + txtEmployeeNo.Text + @"'";
            string _DailyRate = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlEmployeeInfo, "DailyRate");

            double _EERate = 0;
            double.TryParse(_DailyRate, out _EERate);
            txtAmount.Text = (_EERate * double.Parse(_TotalDays)).ToString("N2");
        }

    }
    
    private void txt_Leave(object sender, EventArgs e)
    {
        TextBox txt = sender as TextBox;
        switch (txt.Name)
        {
            case "txtTotalDays":
                TimeCalculate(txtTotalDays.Text, "d");
                break;
            case "txtTotalHours":
                TimeCalculate(txtTotalHours.Text, "h");
                break;
            default:
                break;
        }


    }




    private void txt_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (char.IsDigit(e.KeyChar))
        {
            return;
        }
        if (e.KeyChar == (char)Keys.Back)
        {
            return;
        }
        if (e.KeyChar == '.')
        {
            return;
        }
        if (e.KeyChar == '-')
        {
            return;
        }

        e.Handled = true;
    }

    private void txtTotalDays_TextChanged(object sender, EventArgs e)
    {

    }

    private void btnSave_Click(object sender, EventArgs e)
    {

        string _sqlEmployeeInfo = "SELECT A.DailyRate      ,A.[BCode], A.[Department] FROM [vwsEmployeeDetails] A WHERE A.EmployeeNo = '" + txtEmployeeNo.Text + @"'";
        string _DailyRate = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlEmployeeInfo, "DailyRate");
        string _Department = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlEmployeeInfo, "Department");
        string _BCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlEmployeeInfo, "BCode");

        string _sqlInsertData = "";
        _sqlInsertData = @"

INSERT INTO [dbo].[LeavesApproved]
           ([PayrollPeriod]
           ,[EmployeeNo]
           ,[AccountCode]
           ,[LeaveType]
           ,[Year]
           ,[DateStart]
           ,[DateEnd]
           ,[DailyRate]
           ,[NoOfHours]
           ,[Amount]
           ,[NoOfMins]
           ,[TotalHrs]
           ,[TotalDays]
           ,[Branch]
           ,[Department]
           ,[Remarks])
     VALUES
           ('" + cboYear.Text + @"'
           ,'" + txtEmployeeNo.Text + @"'
           ,'" + txtLeaveAccountCode.Text + @"'
           ,'" + txtLeaveCode.Text + @"'
           ,'" + cboYear.Text + @"'
           ,'" + txtDateFrom.Text + @"'
           ,'" + txtDateTo.Text + @"'
           ,'" + _DailyRate + @"'
           ,'" + txtNoOfHrs.Text + @"'
           ,'" + double.Parse(txtAmount.Text) + @"'
           ,'" + txtNoOfMins.Text + @"'
           ,'" + txtTotalHours.Text + @"'
           ,'" + txtTotalDays.Text + @"'
           ,'" + _BCode + @"'
           ,'" + _Department + @"'
          ,'" + txtRemarks.Text + @"')
                                            ";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlInsertData);
        MessageBox.Show("Data Successfully Added");
    }


}
