using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Configuration;


public partial class frmAccountSetup : Form
{
    public frmAccountSetup()
    {
        InitializeComponent();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {

        
        clsFunctions.SaveSettingValue("11", txt1.Text);
        clsFunctions.SaveSettingValue("14", txt2.Text);
        clsFunctions.SaveSettingValue("17", txt3.Text);
        clsFunctions.SaveSettingValue("20", txt4.Text);
        clsFunctions.SaveSettingValue("23", txt5.Text);
        clsFunctions.SaveSettingValue("26", txt6.Text);
        clsFunctions.SaveSettingValue("29", txt7.Text);
        clsFunctions.SaveSettingValue("32", txt8.Text);
        clsFunctions.SaveSettingValue("35", txt9.Text);
        clsFunctions.SaveSettingValue("38", txt10.Text);
        clsFunctions.SaveSettingValue("41", txt11.Text);
        clsFunctions.SaveSettingValue("44", txt12.Text);
        clsFunctions.SaveSettingValue("53", txt13.Text);
        clsFunctions.SaveSettingValue("50", txt14.Text);
        clsFunctions.SaveSettingValue("47", txt15.Text);
        clsFunctions.SaveSettingValue("59", txt16.Text);


        //clsFunctions.SetSetting("RegularDay", txt1.Text);
        //clsFunctions.SetSetting("RegularOvertime", txt2.Text);
        //clsFunctions.SetSetting("LeaveWithoutPay", txt3.Text);
        //clsFunctions.SetSetting("Tardiness", txt4.Text);
        //clsFunctions.SetSetting("UnderTime", txt5.Text);
        //clsFunctions.SetSetting("LegalHolidayPay", txt6.Text);
        //clsFunctions.SetSetting("LegalHolidayWork", txt7.Text);
        //clsFunctions.SetSetting("LegalHolidayExcess", txt8.Text);
        //clsFunctions.SetSetting("SpecialHolidayWork", txt9.Text);
        //clsFunctions.SetSetting("SpecialHolidayExcess", txt10.Text);
        //clsFunctions.SetSetting("RestdayWork", txt11.Text);
        //clsFunctions.SetSetting("RestdayExcess", txt12.Text);
        //clsFunctions.SetSetting("TotalHours", txt13.Text);
        //clsFunctions.SetSetting("TotalDays", txt14.Text);


        clsFunctions.SaveSettingValue("12", txtE1.Text);
       clsFunctions.SaveSettingValue("15", txtE2.Text);
       clsFunctions.SaveSettingValue("18", txtE3.Text);
       clsFunctions.SaveSettingValue("21", txtE4.Text);
       clsFunctions.SaveSettingValue("24", txtE5.Text);
       clsFunctions.SaveSettingValue("27", txtE6.Text);
       clsFunctions.SaveSettingValue("30", txtE7.Text);
       clsFunctions.SaveSettingValue("33", txtE8.Text);
       clsFunctions.SaveSettingValue("36", txtE9.Text);
       clsFunctions.SaveSettingValue("39", txtE10.Text);
       clsFunctions.SaveSettingValue("42", txtE11.Text);
       clsFunctions.SaveSettingValue("45", txtE12.Text);
       clsFunctions.SaveSettingValue("54", txtE13.Text);
       clsFunctions.SaveSettingValue("51", txtE14.Text);
       clsFunctions.SaveSettingValue("48", txtE15.Text);
        clsFunctions.SaveSettingValue("60", txtE16.Text);


        //clsFunctions.SetSetting("excelRegularDay", txtE1.Text);
        //clsFunctions.SetSetting("excelRegularOvertime", txtE2.Text);
        //clsFunctions.SetSetting("excelLeaveWithoutPay", txtE3.Text);
        //clsFunctions.SetSetting("excelTardiness", txtE4.Text);
        //clsFunctions.SetSetting("excelUnderTime", txtE5.Text);
        //clsFunctions.SetSetting("excelLegalHolidayPay", txtE6.Text);
        //clsFunctions.SetSetting("excelLegalHolidayWork", txtE7.Text);
        //clsFunctions.SetSetting("excelLegalHolidayExcess", txtE8.Text);
        //clsFunctions.SetSetting("excelSpecialHolidayWork", txtE9.Text);
        //clsFunctions.SetSetting("excelSpecialHolidayExcess", txtE10.Text);
        //clsFunctions.SetSetting("excelRestdayWork", txtE11.Text);
        //clsFunctions.SetSetting("excelRestdayExcess", txtE12.Text);
        //clsFunctions.SetSetting("excelTotalHours", txtE13.Text);
        //clsFunctions.SetSetting("excelTotalDays", txtE14.Text);

        clsFunctions.SaveSettingValue("13", txtF1.Text);
       clsFunctions.SaveSettingValue("16", txtF2.Text);
       clsFunctions.SaveSettingValue("19", txtF3.Text);
       clsFunctions.SaveSettingValue("22", txtF4.Text);
       clsFunctions.SaveSettingValue("25", txtF5.Text);
       clsFunctions.SaveSettingValue("28", txtF6.Text);
       clsFunctions.SaveSettingValue("31", txtF7.Text);
       clsFunctions.SaveSettingValue("34", txtF8.Text);
       clsFunctions.SaveSettingValue("37", txtF9.Text);
       clsFunctions.SaveSettingValue("40", txtF10.Text);
       clsFunctions.SaveSettingValue("43", txtF11.Text);
       clsFunctions.SaveSettingValue("46", txtF12.Text);
       clsFunctions.SaveSettingValue("55", txtF13.Text);
       clsFunctions.SaveSettingValue("52", txtF14.Text);
       clsFunctions.SaveSettingValue("49", txtF15.Text);
       clsFunctions.SaveSettingValue("61", txtF16.Text);

        //clsFunctions.SetSetting("timeRegularDay", txtF1.Text);
        //clsFunctions.SetSetting("timeRegularOvertime", txtF2.Text);
        //clsFunctions.SetSetting("timeLeaveWithoutPay", txtF3.Text);
        //clsFunctions.SetSetting("timeTardiness", txtF4.Text);
        //clsFunctions.SetSetting("timeUnderTime", txtF5.Text);
        //clsFunctions.SetSetting("timeLegalHolidayPay", txtF6.Text);
        //clsFunctions.SetSetting("timeLegalHolidayWork", txtF7.Text);
        //clsFunctions.SetSetting("timeLegalHolidayExcess", txtF8.Text);
        //clsFunctions.SetSetting("timeSpecialHolidayWork", txtF9.Text);
        //clsFunctions.SetSetting("timeSpecialHolidayExcess", txtF10.Text);
        //clsFunctions.SetSetting("timeRestdayWork", txtF11.Text);
        //clsFunctions.SetSetting("timeRestdayExcess", txtF12.Text);
        //clsFunctions.SetSetting("timeTotalHours", txtF13.Text);
        //clsFunctions.SetSetting("timeTotalDays", txtF14.Text);

        MessageBox.Show("New Settings Applied!", "Connection Settings", MessageBoxButtons.OK, MessageBoxIcon.None);
        Application.Restart();
    }

    private void frmAccountSetup_Load(object sender, EventArgs e)
    {


        //txt1.Text = ConfigurationManager.AppSettings["RegularDay"];
        //txt2.Text = ConfigurationManager.AppSettings["RegularOvertime"];
        //txt3.Text = ConfigurationManager.AppSettings["LeaveWithoutPay"];
        //txt4.Text = ConfigurationManager.AppSettings["Tardiness"];
        //txt5.Text = ConfigurationManager.AppSettings["UnderTime"];
        //txt6.Text = ConfigurationManager.AppSettings["LegalHolidayPay"];
        //txt7.Text = ConfigurationManager.AppSettings["LegalHolidayWork"];
        //txt8.Text = ConfigurationManager.AppSettings["LegalHolidayExcess"];
        //txt9.Text = ConfigurationManager.AppSettings["SpecialHolidayWork"];
        //txt10.Text = ConfigurationManager.AppSettings["SpecialHolidayExcess"];
        //txt11.Text = ConfigurationManager.AppSettings["RestdayWork"];
        //txt12.Text = ConfigurationManager.AppSettings["RestdayExcess"];
        //txt13.Text = ConfigurationManager.AppSettings["TotalHours"];
        //txt14.Text = ConfigurationManager.AppSettings["TotalDays"];

        txt1.Text = clsFunctions.SystemSettingValue("11");
        txt2.Text = clsFunctions.SystemSettingValue("14");
        txt3.Text = clsFunctions.SystemSettingValue("17"); 
        txt4.Text = clsFunctions.SystemSettingValue("20"); 
        txt5.Text = clsFunctions.SystemSettingValue("23"); 
        txt6.Text = clsFunctions.SystemSettingValue("26"); 
        txt7.Text = clsFunctions.SystemSettingValue("29"); 
        txt8.Text = clsFunctions.SystemSettingValue("32"); 
        txt9.Text = clsFunctions.SystemSettingValue("35"); 
        txt10.Text = clsFunctions.SystemSettingValue("38"); 
        txt11.Text = clsFunctions.SystemSettingValue("41"); 
        txt12.Text = clsFunctions.SystemSettingValue("44"); 
        txt13.Text = clsFunctions.SystemSettingValue("53"); 
        txt14.Text = clsFunctions.SystemSettingValue("50"); 
        txt15.Text = clsFunctions.SystemSettingValue("47");
        txt16.Text = clsFunctions.SystemSettingValue("59");


        //txtE1.Text = ConfigurationManager.AppSettings["excelRegularDay"];
        //txtE2.Text = ConfigurationManager.AppSettings["excelRegularOvertime"];
        //txtE3.Text = ConfigurationManager.AppSettings["excelLeaveWithoutPay"];
        //txtE4.Text = ConfigurationManager.AppSettings["excelTardiness"];
        //txtE5.Text = ConfigurationManager.AppSettings["excelUnderTime"];
        //txtE6.Text = ConfigurationManager.AppSettings["excelLegalHolidayPay"];
        //txtE7.Text = ConfigurationManager.AppSettings["excelLegalHolidayWork"];
        //txtE8.Text = ConfigurationManager.AppSettings["excelLegalHolidayExcess"];
        //txtE9.Text = ConfigurationManager.AppSettings["excelSpecialHolidayWork"];
        //txtE10.Text = ConfigurationManager.AppSettings["excelSpecialHolidayExcess"];
        //txtE11.Text = ConfigurationManager.AppSettings["excelRestdayWork"];
        //txtE12.Text = ConfigurationManager.AppSettings["excelRestdayExcess"];
        //txtE13.Text = ConfigurationManager.AppSettings["excelTotalHours"];
        //txtE14.Text = ConfigurationManager.AppSettings["excelTotalDays"];

        txtE1.Text = clsFunctions.SystemSettingValue("12");
        txtE2.Text = clsFunctions.SystemSettingValue("15");
        txtE3.Text = clsFunctions.SystemSettingValue("18");
        txtE4.Text = clsFunctions.SystemSettingValue("21");
        txtE5.Text = clsFunctions.SystemSettingValue("24");
        txtE6.Text = clsFunctions.SystemSettingValue("27");
        txtE7.Text = clsFunctions.SystemSettingValue("30");
        txtE8.Text = clsFunctions.SystemSettingValue("33");
        txtE9.Text = clsFunctions.SystemSettingValue("36");
        txtE10.Text = clsFunctions.SystemSettingValue("39");
        txtE11.Text = clsFunctions.SystemSettingValue("42");
        txtE12.Text = clsFunctions.SystemSettingValue("45");
        txtE13.Text = clsFunctions.SystemSettingValue("54");
        txtE14.Text = clsFunctions.SystemSettingValue("51");
        txtE15.Text = clsFunctions.SystemSettingValue("48");
        txtE16.Text = clsFunctions.SystemSettingValue("60");

        //txtF1.Text = ConfigurationManager.AppSettings["timeRegularDay"];
        //txtF2.Text = ConfigurationManager.AppSettings["timeRegularOvertime"];
        //txtF3.Text = ConfigurationManager.AppSettings["timeLeaveWithoutPay"];
        //txtF4.Text = ConfigurationManager.AppSettings["timeTardiness"];
        //txtF5.Text = ConfigurationManager.AppSettings["timeUnderTime"];
        //txtF6.Text = ConfigurationManager.AppSettings["timeLegalHolidayPay"];
        //txtF7.Text = ConfigurationManager.AppSettings["timeLegalHolidayWork"];
        //txtF8.Text = ConfigurationManager.AppSettings["timeLegalHolidayExcess"];
        //txtF9.Text = ConfigurationManager.AppSettings["timeSpecialHolidayWork"];
        //txtF10.Text = ConfigurationManager.AppSettings["timeSpecialHolidayExcess"];
        //txtF11.Text = ConfigurationManager.AppSettings["timeRestdayWork"];
        //txtF12.Text = ConfigurationManager.AppSettings["timeRestdayExcess"];
        //txtF13.Text = ConfigurationManager.AppSettings["timeTotalHours"];
        //txtF14.Text = ConfigurationManager.AppSettings["timeTotalDays"];


        txtF1.Text = clsFunctions.SystemSettingValue("13");
        txtF2.Text = clsFunctions.SystemSettingValue("16");
        txtF3.Text = clsFunctions.SystemSettingValue("19");
        txtF4.Text = clsFunctions.SystemSettingValue("22");
        txtF5.Text = clsFunctions.SystemSettingValue("25");
        txtF6.Text = clsFunctions.SystemSettingValue("28");
        txtF7.Text = clsFunctions.SystemSettingValue("31");
        txtF8.Text = clsFunctions.SystemSettingValue("34");
        txtF9.Text = clsFunctions.SystemSettingValue("37");
        txtF10.Text = clsFunctions.SystemSettingValue("40");
        txtF11.Text = clsFunctions.SystemSettingValue("43");
        txtF12.Text = clsFunctions.SystemSettingValue("46");
        txtF13.Text = clsFunctions.SystemSettingValue("55");
        txtF14.Text = clsFunctions.SystemSettingValue("52");
        txtF15.Text = clsFunctions.SystemSettingValue("49");
        txtF16.Text = clsFunctions.SystemSettingValue("61");

    }
}