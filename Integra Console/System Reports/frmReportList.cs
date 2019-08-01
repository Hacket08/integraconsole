using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;

using System.Configuration;
public partial class frmReportList : Form
{

    public static string _RequestType;
    public static string _sqlCrystal;
    public frmReportList()
    {
        InitializeComponent();
    }

    private void frmReportList_Load(object sender, EventArgs e)
    {
        //switch (clsDeclaration.sReportTag)
        //{
        //    case 1:

        //        frmAccountByRangeParameter frmAccountByRangeParameter = new frmAccountByRangeParameter();
        //        frmAccountByRangeParameter._RequestType = "Account";
        //        frmAccountByRangeParameter.ShowDialog();

        //        if (clsDeclaration.bView == false)
        //        {
        //            return;
        //        }

        //        break;

        //    case 2:

        //        break;
        //}

        ReportDocument cryRpt = new ReportDocument();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        
        string _Path = clsDeclaration.sReportPath;
        cryRpt.Load(_Path, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault);

        // Passing SQL Data Via C# Datatable
        string _ConString = clsDeclaration.sSystemConnection;
        // Passing tru Data Table
        DataTable _tblCrystal = new DataTable();


        switch (clsDeclaration.sReportID)
        {
            case 0:
                crConnectionInfo.ServerName = ConfigurationManager.AppSettings["DBServer"];
                crConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["DBName"];
                crConnectionInfo.UserID = ConfigurationManager.AppSettings["DBUsername"];
                crConnectionInfo.Password = ConfigurationManager.AppSettings["DBPassword"];

                break;

            default:

                crConnectionInfo.ServerName = clsDeclaration.sServer;
                crConnectionInfo.DatabaseName = clsDeclaration.sCompany;
                crConnectionInfo.UserID = clsDeclaration.sUsername;
                crConnectionInfo.Password = clsDeclaration.sPassword;

                break;
        }
        
        CrTables = cryRpt.Database.Tables;
        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        {
            crtableLogoninfo = CrTable.LogOnInfo;
            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
            CrTable.ApplyLogOnInfo(crtableLogoninfo);
        }

        _sqlCrystal = clsDeclaration.sQueryString;
        switch (clsDeclaration.sReportID)
        {
            case 10:
            case 9:
            case 8:
            case 7:
            case 6:
            case 5:
            case 4:
            case 3:
            case 2:
            case 1:
                _tblCrystal = clsSQLClientFunctions.DataList(_ConString, _sqlCrystal);
                cryRpt.SetDataSource(_tblCrystal);
                break;
        }
        cryRpt.Refresh();

        #region Comment Section
        //ParameterFieldDefinitions crParameterFieldDefinitions;
        //ParameterFieldDefinition crParameterFieldDefinition;
        //ParameterValues crParameterValues = new ParameterValues();
        //ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

        //switch (clsDeclaration.sReportTag)
        //{
        //    case 4:
        //        crParameterDiscreteValue.Value = clsDeclaration.sEmployeeNo;
        //        crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //        crParameterFieldDefinition = crParameterFieldDefinitions["EmployeeNo"];
        //        crParameterValues = crParameterFieldDefinition.CurrentValues;

        //        crParameterValues.Clear();
        //        crParameterValues.Add(crParameterDiscreteValue);
        //        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
        //        break;
        //    case 25:
        //    case 5:
        //    case 3:
        //        crParameterDiscreteValue.Value = clsDeclaration.sDateFrom;
        //        crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //        crParameterFieldDefinition = crParameterFieldDefinitions["DateFrom"];
        //        crParameterValues = crParameterFieldDefinition.CurrentValues;

        //        crParameterValues.Clear();
        //        crParameterValues.Add(crParameterDiscreteValue);
        //        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues); 


        //        crParameterDiscreteValue.Value = clsDeclaration.sDateTo;
        //        crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //        crParameterFieldDefinition = crParameterFieldDefinitions["DateTo"];
        //        crParameterValues = crParameterFieldDefinition.CurrentValues;

        //        crParameterValues.Clear();
        //        crParameterValues.Add(crParameterDiscreteValue);
        //        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

        //        break;
        //    case 2:
        //        crParameterDiscreteValue.Value = clsDeclaration.sBranch;
        //        crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //        crParameterFieldDefinition = crParameterFieldDefinitions["Branch"];
        //        crParameterValues = crParameterFieldDefinition.CurrentValues;

        //        crParameterValues.Clear();
        //        crParameterValues.Add(crParameterDiscreteValue);
        //        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

        //        crParameterDiscreteValue.Value = clsDeclaration.sYear;
        //        crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //        crParameterFieldDefinition = crParameterFieldDefinitions["Year"];
        //        crParameterValues = crParameterFieldDefinition.CurrentValues;

        //        crParameterValues.Clear();
        //        crParameterValues.Add(crParameterDiscreteValue);
        //        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

        //        crParameterDiscreteValue.Value = clsDeclaration.sConfiLevelSelection;
        //        crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //        crParameterFieldDefinition = crParameterFieldDefinitions["ConfiLevel"];
        //        crParameterValues = crParameterFieldDefinition.CurrentValues;

        //        crParameterValues.Clear();
        //        crParameterValues.Add(crParameterDiscreteValue);
        //        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);


        //        break;
        //    case 1:
        //        crParameterDiscreteValue.Value = clsDeclaration.sDateFrom;
        //        crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //        crParameterFieldDefinition = crParameterFieldDefinitions["DateFrom"];
        //        crParameterValues = crParameterFieldDefinition.CurrentValues;

        //        crParameterValues.Clear();
        //        crParameterValues.Add(crParameterDiscreteValue);
        //        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);


        //        crParameterDiscreteValue.Value = clsDeclaration.sDateTo;
        //        crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //        crParameterFieldDefinition = crParameterFieldDefinitions["DateTo"];
        //        crParameterValues = crParameterFieldDefinition.CurrentValues;

        //        crParameterValues.Clear();
        //        crParameterValues.Add(crParameterDiscreteValue);
        //        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);


        //        crParameterDiscreteValue.Value = clsDeclaration.sCode;
        //        crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //        crParameterFieldDefinition = crParameterFieldDefinitions["AccountCode"];
        //        crParameterValues = crParameterFieldDefinition.CurrentValues;

        //        crParameterValues.Clear();
        //        crParameterValues.Add(crParameterDiscreteValue);
        //        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
        //        break;
        //}
        #endregion


        crystalReportViewer1.Refresh();
        crystalReportViewer1.ReportSource = cryRpt;
    }

    private void frmReportList_FormClosing(object sender, FormClosingEventArgs e)
    {

    }
}
