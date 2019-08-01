using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;



static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);


        string myConn = clsSQLClientFunctions.GlobalConnectionString(
            ConfigurationManager.AppSettings["DBServer"],
             "master",
            ConfigurationManager.AppSettings["DBUsername"],
            ConfigurationManager.AppSettings["DBPassword"]);


        clsDeclaration.sSystemConnection = clsSQLClientFunctions.GlobalConnectionString(
            ConfigurationManager.AppSettings["DBServer"],
            ConfigurationManager.AppSettings["DBName"],
            ConfigurationManager.AppSettings["DBUsername"],
            ConfigurationManager.AppSettings["DBPassword"]);



        clsDeclaration.sSAPConnection = clsSQLClientFunctions.GlobalConnectionString(
            ConfigurationManager.AppSettings["sysDBServer"],
            ConfigurationManager.AppSettings["sysDftDBCompany"],
            ConfigurationManager.AppSettings["sysDBUsername"],
            ConfigurationManager.AppSettings["sysDBPassword"]);


        if (clsSQLClientFunctions.CheckConnection(myConn) == true)
        {
            clsDatabaseBuild.CreateDatabase(myConn);
        }

        if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sSystemConnection) == false)
        {
            frmConnection frmConnection = new frmConnection();
            frmConnection.ShowDialog();

            return;
        }

        string _sqlAccountCode;
        _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VarID = '3'";
        clsDeclaration.sysGUI = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlAccountCode, "VariableValue");

        //Leave Cut-off Month
        _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VarID = '4'";
        clsDeclaration.sysLeaveCutoffMonth = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlAccountCode, "VariableValue");

        //Leave Cut-off Day
        _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VarID = '5'";
        clsDeclaration.sysLeaveCutoffDay = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlAccountCode, "VariableValue");


        //VL Cash Conversion
        _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VarID = '6'";
        clsDeclaration.sysVLCashConversionAccount = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlAccountCode, "VariableValue");
        
        //Leave Divisor
        _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VarID = '10'";
        clsDeclaration.sysLeaveDivisor = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlAccountCode, "VariableValue");



        //VL Cash Conversion
        _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VarID = '57'";
        clsDeclaration.sys13MonthAccount = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlAccountCode, "VariableValue");



        //VL Cash Conversion
        _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VarID = '58'";
        clsDeclaration.sysBonusAccount = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlAccountCode, "VariableValue");


        if (clsDeclaration.sysGUI == "1")
        {
            Application.Run(new frmMainAdvance());
        }
        else
        {
            Application.Run(new frmMainWindow());
        }



    }
}
