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
public partial class frmReportViewer : Form
{
    public static string _RequestType;

    public frmReportViewer()
    {
        InitializeComponent();
    }

    private void frmReportViewer_Load(object sender, EventArgs e)
    {
        
        if (clsDeclaration.sReportTag  != 3 && clsDeclaration.sReportTag != 6 && clsDeclaration.sReportTag != 9 && clsDeclaration.sReportTag != 8 
            && clsDeclaration.sReportTag != 10 && clsDeclaration.sReportTag != 23 && clsDeclaration.sReportTag != 24 && clsDeclaration.sReportTag != 25)
        {
            frmParameter frmParameter = new frmParameter();
            frmParameter.ShowDialog();

            if (clsDeclaration.bView == false)
            {
                return;
            }
        }

        if(clsDeclaration.sReportTag == 9)
        {
            frmMonthParameter frmMonthParameter = new frmMonthParameter();
            frmMonthParameter.ShowDialog();


            if (clsDeclaration.bView == false)
            {
                return;
            }
        }

        if (clsDeclaration.sReportTag == 8)
        {
            frmPayrollPeriodParameter frmPayrollPeriodParameter = new frmPayrollPeriodParameter();
            frmPayrollPeriodParameter.ShowDialog();


            if (clsDeclaration.bView == false)
            {
                return;
            }
        }


        if (clsDeclaration.sReportTag == 10)
        {
            frmEmployeeParameter frmEmployeeParameter = new frmEmployeeParameter();
            frmEmployeeParameter.ShowDialog();


            if (clsDeclaration.bView == false)
            {
                return;
            }
        }



        if (clsDeclaration.sReportTag == 23)
        {
            frmEmployeeNoParameter frmEmployeeNoParameter = new frmEmployeeNoParameter();
            frmEmployeeNoParameter.ShowDialog();


            if (clsDeclaration.bView == false)
            {
                return;
            }
        }

        if (clsDeclaration.sReportTag == 24)
        {
            frmEmployeeListParameter frmEmployeeListParameter = new frmEmployeeListParameter();
            frmEmployeeListParameter.ShowDialog();

            if (clsDeclaration.bView == false)
            {
                return;
            }
        }


        if (clsDeclaration.sReportTag == 25)
        {
            frmDateRangeParameter frmDateRangeParameter = new frmDateRangeParameter();
            frmDateRangeParameter.ShowDialog();

            if (clsDeclaration.bView == false)
            {
                return;
            }
        }

        ReportDocument cryRpt = new ReportDocument();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        


        string _Path = clsDeclaration.sReportPath;
        cryRpt.Load(_Path, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault);
    

        if (clsDeclaration.sReportTag != 3 && clsDeclaration.sReportTag != 6 && clsDeclaration.sReportTag != 5 
            && clsDeclaration.sReportTag != 7 && clsDeclaration.sReportTag != 8 && clsDeclaration.sReportTag != 9 
            && clsDeclaration.sReportTag != 10 && clsDeclaration.sReportTag != 11 && clsDeclaration.sReportTag != 23
            && clsDeclaration.sReportTag != 24 && clsDeclaration.sReportTag != 25)
        {
            crConnectionInfo.ServerName = clsDeclaration.sServer;
            crConnectionInfo.DatabaseName = clsDeclaration.sCompany;
            crConnectionInfo.UserID = clsDeclaration.sUsername;
            crConnectionInfo.Password = clsDeclaration.sPassword;
        }
        else
        {
            crConnectionInfo.ServerName = ConfigurationManager.AppSettings["DBServer"];
            crConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["DBName"];
            crConnectionInfo.UserID = ConfigurationManager.AppSettings["DBUsername"];
            crConnectionInfo.Password = ConfigurationManager.AppSettings["DBPassword"];
        }





        CrTables = cryRpt.Database.Tables;
        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        {
            crtableLogoninfo = CrTable.LogOnInfo;
            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
            CrTable.ApplyLogOnInfo(crtableLogoninfo);
        }

        cryRpt.Refresh();


        ParameterFieldDefinitions crParameterFieldDefinitions;
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues = new ParameterValues();
        ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();


        if (clsDeclaration.sReportTag != 3 && clsDeclaration.sReportTag != 6 &&
            clsDeclaration.sReportTag != 7 && clsDeclaration.sReportTag != 9 && 
            clsDeclaration.sReportTag != 8 && clsDeclaration.sReportTag != 10 &&
            clsDeclaration.sReportTag != 11 && clsDeclaration.sReportTag != 23 && 
            clsDeclaration.sReportTag != 24 && clsDeclaration.sReportTag != 25)
        {
            crParameterDiscreteValue.Value = clsDeclaration.sCompanyName;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["Company"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;

            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

        }
        
        switch (clsDeclaration.sReportTag)
        {
            case 1: case 6:
                ParameterFieldDefinitions crParameterFieldDefinitions1;
                ParameterFieldDefinition crParameterFieldDefinition1;
                ParameterValues crParameterValues1 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue1 = new ParameterDiscreteValue();

                crParameterDiscreteValue1.Value = clsDeclaration.sDateFrom;
                crParameterFieldDefinitions1 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition1 = crParameterFieldDefinitions1["DateFrom"];
                crParameterValues1 = crParameterFieldDefinition1.CurrentValues;

                crParameterValues1.Clear();
                crParameterValues1.Add(crParameterDiscreteValue1);
                crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1);




                ParameterFieldDefinitions crParameterFieldDefinitions2;
                ParameterFieldDefinition crParameterFieldDefinition2;
                ParameterValues crParameterValues2 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue2 = new ParameterDiscreteValue();


                crParameterDiscreteValue2.Value = clsDeclaration.sDateTo;
                crParameterFieldDefinitions2 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition2 = crParameterFieldDefinitions2["DateTo"];
                crParameterValues2 = crParameterFieldDefinition2.CurrentValues;
                
                crParameterValues2.Clear();
                crParameterValues2.Add(crParameterDiscreteValue2);
                crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2);
                
                if (clsDeclaration.sReportTag == 6)
                {
                    ParameterFieldDefinitions crParameterFieldDefinitions7;
                    ParameterFieldDefinition crParameterFieldDefinition7;
                    ParameterValues crParameterValues7 = new ParameterValues();
                    ParameterDiscreteValue crParameterDiscreteValue7 = new ParameterDiscreteValue();

                    crParameterDiscreteValue7.Value = clsDeclaration.sBranch;
                    crParameterFieldDefinitions7 = cryRpt.DataDefinition.ParameterFields;
                    crParameterFieldDefinition7 = crParameterFieldDefinitions7["Branch"];
                    crParameterValues7 = crParameterFieldDefinition7.CurrentValues;

                    crParameterValues7.Clear();
                    crParameterValues7.Add(crParameterDiscreteValue7);
                    crParameterFieldDefinition7.ApplyCurrentValues(crParameterValues7);
                }
                
                break;
            case 5:

                ParameterFieldDefinitions crParameterFieldDefinitions3;
                ParameterFieldDefinition crParameterFieldDefinition3;
                ParameterValues crParameterValues3 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue3 = new ParameterDiscreteValue();

                crParameterDiscreteValue3.Value = clsDeclaration.sBranch;
                crParameterFieldDefinitions3 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition3 = crParameterFieldDefinitions3["Branch"];
                crParameterValues3 = crParameterFieldDefinition3.CurrentValues;

                crParameterValues3.Clear();
                crParameterValues3.Add(crParameterDiscreteValue3);
                crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3);




                ParameterFieldDefinitions crParameterFieldDefinitions5;
                ParameterFieldDefinition crParameterFieldDefinition5;
                ParameterValues crParameterValues5 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue5 = new ParameterDiscreteValue();

                crParameterDiscreteValue5.Value = clsDeclaration.sPayrollPeriod;
                crParameterFieldDefinitions5 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition5 = crParameterFieldDefinitions5["PayrollPeriod"];
                crParameterValues5 = crParameterFieldDefinition5.CurrentValues;
                
                crParameterValues5.Clear();
                crParameterValues5.Add(crParameterDiscreteValue5);
                crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5);




                ParameterFieldDefinitions crParameterFieldDefinitions6;
                ParameterFieldDefinition crParameterFieldDefinition6;
                ParameterValues crParameterValues6 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue6 = new ParameterDiscreteValue();
                
                crParameterDiscreteValue6.Value = clsDeclaration.sConfiLevel;
                crParameterFieldDefinitions6 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition6 = crParameterFieldDefinitions6["ConfiLevel"];
                crParameterValues6 = crParameterFieldDefinition6.CurrentValues;
                
                crParameterValues6.Clear();
                crParameterValues6.Add(crParameterDiscreteValue6);
                crParameterFieldDefinition6.ApplyCurrentValues(crParameterValues6);

                break;
            case 7: case 11:

                switch (clsDeclaration.sReportTag)
                {
                    case 7:

                        ParameterFieldDefinitions crParameterFieldDefinitions7c;
                        ParameterFieldDefinition crParameterFieldDefinition7c;
                        ParameterValues crParameterValues7c = new ParameterValues();
                        ParameterDiscreteValue crParameterDiscreteValue7c = new ParameterDiscreteValue();

                        crParameterDiscreteValue7c.Value = clsDeclaration.sBranch;
                        crParameterFieldDefinitions7c = cryRpt.DataDefinition.ParameterFields;
                        crParameterFieldDefinition7c = crParameterFieldDefinitions7c["Branch"];
                        crParameterValues7c = crParameterFieldDefinition7c.CurrentValues;

                        crParameterValues7c.Clear();
                        crParameterValues7c.Add(crParameterDiscreteValue7c);
                        crParameterFieldDefinition7c.ApplyCurrentValues(crParameterValues7c);

                        break;
                    case 11:

                        ParameterFieldDefinitions crParameterFieldDefinitions7d;
                        ParameterFieldDefinition crParameterFieldDefinition7d;
                        ParameterValues crParameterValues7d = new ParameterValues();
                        ParameterDiscreteValue crParameterDiscreteValue7d = new ParameterDiscreteValue();

                        crParameterDiscreteValue7d.Value = clsDeclaration.sArea;
                        crParameterFieldDefinitions7d = cryRpt.DataDefinition.ParameterFields;
                        crParameterFieldDefinition7d = crParameterFieldDefinitions7d["Area"];
                        crParameterValues7d = crParameterFieldDefinition7d.CurrentValues;

                        crParameterValues7d.Clear();
                        crParameterValues7d.Add(crParameterDiscreteValue7d);
                        crParameterFieldDefinition7d.ApplyCurrentValues(crParameterValues7d);

                        break;
                }




                ParameterFieldDefinitions crParameterFieldDefinitions7a;
                ParameterFieldDefinition crParameterFieldDefinition7a;
                ParameterValues crParameterValues7a = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue7a = new ParameterDiscreteValue();

                crParameterDiscreteValue7a.Value = clsDeclaration.sPayrollPeriod;
                crParameterFieldDefinitions7a = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition7a = crParameterFieldDefinitions7a["PayrollPeriod"];
                crParameterValues7a = crParameterFieldDefinition7a.CurrentValues;

                crParameterValues7a.Clear();
                crParameterValues7a.Add(crParameterDiscreteValue7a);
                crParameterFieldDefinition7a.ApplyCurrentValues(crParameterValues7a);




                ParameterFieldDefinitions crParameterFieldDefinitions7b;
                ParameterFieldDefinition crParameterFieldDefinition7b;
                ParameterValues crParameterValues7b = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue7b = new ParameterDiscreteValue();

                crParameterDiscreteValue7b.Value = clsDeclaration.sConfiLevel;
                crParameterFieldDefinitions7b = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition7b = crParameterFieldDefinitions7b["ConfiLevel"];
                crParameterValues7b = crParameterFieldDefinition7b.CurrentValues;

                crParameterValues7b.Clear();
                crParameterValues7b.Add(crParameterDiscreteValue7b);
                crParameterFieldDefinition7b.ApplyCurrentValues(crParameterValues7b);

                break;
            case 8:
                ParameterFieldDefinitions crParameterFieldDefinitions8c;
                ParameterFieldDefinition crParameterFieldDefinition8c;
                ParameterValues crParameterValues8c = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue8c = new ParameterDiscreteValue();

                crParameterDiscreteValue8c.Value = clsDeclaration.sPayrollPeriod;
                crParameterFieldDefinitions8c = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition8c = crParameterFieldDefinitions8c["Payroll Period"];
                crParameterValues8c = crParameterFieldDefinition8c.CurrentValues;

                crParameterValues8c.Clear();
                crParameterValues8c.Add(crParameterDiscreteValue8c);
                crParameterFieldDefinition8c.ApplyCurrentValues(crParameterValues8c);
                break;
            case 9:
                ParameterFieldDefinitions crParameterFieldDefinitions9c;
                ParameterFieldDefinition crParameterFieldDefinition9c;
                ParameterValues crParameterValues9c = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue9c = new ParameterDiscreteValue();

                crParameterDiscreteValue9c.Value = clsDeclaration.sPayrollPeriod;
                crParameterFieldDefinitions9c = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition9c = crParameterFieldDefinitions9c["Payroll Month"];
                crParameterValues9c = crParameterFieldDefinition9c.CurrentValues;

                crParameterValues9c.Clear();
                crParameterValues9c.Add(crParameterDiscreteValue9c);
                crParameterFieldDefinition9c.ApplyCurrentValues(crParameterValues9c);
                break;
            case 10:

                ParameterFieldDefinitions crParameterFieldDefinitions10c;
                ParameterFieldDefinition crParameterFieldDefinition10c;
                ParameterValues crParameterValues10c = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue10c = new ParameterDiscreteValue();

                crParameterDiscreteValue10c.Value = clsDeclaration.sEmployeeNo;
                crParameterFieldDefinitions10c = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition10c = crParameterFieldDefinitions10c["Employee"];
                crParameterValues10c = crParameterFieldDefinition10c.CurrentValues;

                crParameterValues10c.Clear();
                crParameterValues10c.Add(crParameterDiscreteValue10c);
                crParameterFieldDefinition10c.ApplyCurrentValues(crParameterValues10c);




                ParameterFieldDefinitions crParameterFieldDefinitions10a;
                ParameterFieldDefinition crParameterFieldDefinition10a;
                ParameterValues crParameterValues10a = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue10a = new ParameterDiscreteValue();

                crParameterDiscreteValue10a.Value = clsDeclaration.sPayrollPeriod;
                crParameterFieldDefinitions10a = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition10a = crParameterFieldDefinitions10a["PayrollPeriod"];
                crParameterValues10a = crParameterFieldDefinition10a.CurrentValues;

                crParameterValues10a.Clear();
                crParameterValues10a.Add(crParameterDiscreteValue10a);
                crParameterFieldDefinition10a.ApplyCurrentValues(crParameterValues10a);




                ParameterFieldDefinitions crParameterFieldDefinitions10b;
                ParameterFieldDefinition crParameterFieldDefinition10b;
                ParameterValues crParameterValues10b = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue10b = new ParameterDiscreteValue();

                crParameterDiscreteValue10b.Value = clsDeclaration.sConfiLevel;
                crParameterFieldDefinitions10b = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition10b = crParameterFieldDefinitions10b["ConfiLevel"];
                crParameterValues10b = crParameterFieldDefinition10b.CurrentValues;

                crParameterValues10b.Clear();
                crParameterValues10b.Add(crParameterDiscreteValue10b);
                crParameterFieldDefinition10b.ApplyCurrentValues(crParameterValues10b);
                break;
            case 23:
                ParameterFieldDefinitions crParameterFieldDefinitions23;
                ParameterFieldDefinition crParameterFieldDefinition23;
                ParameterValues crParameterValues23 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue23 = new ParameterDiscreteValue();

                crParameterDiscreteValue23.Value = clsDeclaration.sEmployeeNo;
                crParameterFieldDefinitions23 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition23 = crParameterFieldDefinitions23["EmployeeNo"];
                crParameterValues23 = crParameterFieldDefinition23.CurrentValues;

                crParameterValues23.Clear();
                crParameterValues23.Add(crParameterDiscreteValue23);
                crParameterFieldDefinition23.ApplyCurrentValues(crParameterValues23);

                break;
            case 24:
                crParameterDiscreteValue.Value = clsDeclaration.sBranch;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["Branch"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);


                crParameterDiscreteValue.Value = clsDeclaration.sEmpStatus;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["EmpStatus"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                crParameterDiscreteValue.Value = clsDeclaration.sConfiLevel;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["ConfiLevelUser"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                crParameterDiscreteValue.Value = clsDeclaration.sConfiLevelSelection;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["ConfiLevelParam"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
                break;
            case 25:
                crParameterDiscreteValue.Value = clsDeclaration.sDateFrom;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["DateFrom"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);


                crParameterDiscreteValue.Value = clsDeclaration.sDateTo;
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["DateTo"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                break;
        }


   
        crystalReportViewer1.Refresh();
        crystalReportViewer1.ReportSource = cryRpt;
        
        //ReportDocument cryRpt = new ReportDocument();

        //cryRpt.Load(@"D:\Incomplete Time In And Out.rpt");
        
        //cryRpt.SetDatabaseLogon("sa", "B1Admin", "192.168.126.1", "PayrollMaster");
        //crystalReportViewer1.ReportSource = cryRpt;
        //crystalReportViewer1.Refresh();

    }

    private void crystalReportViewer1_Load(object sender, EventArgs e)
    {

    }
}
