using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Integra_Console.Properties;

using System.Configuration;

public partial class frmMainWindow : Form
{
    private static DataTable _CompanyList = new DataTable();

    private static frmLeaveGenParameter frmLeaveGenParameter = new frmLeaveGenParameter();
    private static frmPayrollPeriodParameter frmPayrollPeriodParameter = new frmPayrollPeriodParameter();
    private static frmEmployeeFilterParameter frmEmployeeFilterParameter = new frmEmployeeFilterParameter();
    private static frmReport frmReport = new frmReport();



    

    private static frmEmpDateAccountParameter frmEmpDateAccountParameter = new frmEmpDateAccountParameter();
    private static frmEmpDateParameter frmEmpDateParameter = new frmEmpDateParameter();
    private static frmEmpParameter frmEmpParameter = new frmEmpParameter();
    private static frmSILPrParameter frmSILPrParameter = new frmSILPrParameter();

    

    public frmMainWindow()
    {
        InitializeComponent();
    }

    public void InitializeFormData()
    {
        pnlModule.Visible = true;
        modulesToolStripMenuItem.Enabled = true;
    }

    private void frmMainWindow_Load(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Maximized;

        pnlModule.Visible = false;
        modulesToolStripMenuItem.Enabled = false;



        //string myConn = clsSQLClientFunctions.GlobalConnectionString(
        //    ConfigurationManager.AppSettings["DBServer"],
        //     "master",
        //    ConfigurationManager.AppSettings["DBUsername"],
        //    ConfigurationManager.AppSettings["DBPassword"]);


        //clsDeclaration.sSystemConnection = clsSQLClientFunctions.GlobalConnectionString(
        //    ConfigurationManager.AppSettings["DBServer"],
        //    ConfigurationManager.AppSettings["DBName"],
        //    ConfigurationManager.AppSettings["DBUsername"],
        //    ConfigurationManager.AppSettings["DBPassword"]);

            
        //if (clsSQLClientFunctions.CheckConnection(myConn) == true)
        //{
        //    clsDatabaseBuild.CreateDatabase(myConn);
        //}
            
        //if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sSystemConnection) == false)
        //{
        //    frmConnection frmConnection = new frmConnection();
        //    frmConnection.MdiParent = this;
        //    frmConnection.Show();

        //    return;
        //}

        //string _sqlAccountCode;
        //_sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VarID = '3'";
        //string _GUI = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlAccountCode, "VariableValue");

        //if(_GUI == "1")
        //{
        //    frmMainAdvance frmMainAdvance = new frmMainAdvance();
        //    frmMainAdvance.Show();

        //    this.Close();
        //    return;
        //}
        
        frmLogin frmLogin = new frmLogin();
        frmLogin.MdiParent = this;  
        frmLogin.Show();
        
        DataTable _CompanyList;
        string _SQLSyntax;
        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        clsDeclaration.sqlEmployeeList = @"";
        //_SQLDepartmentList = @"";
        int i = 0;
        foreach (DataRow rowDB in _CompanyList.Rows)
        {
            if (i > 0)
            {
                clsDeclaration.sqlEmployeeList = clsDeclaration.sqlEmployeeList + " UNION ALL ";
            }

            clsDeclaration.sServer = rowDB[3].ToString();
            clsDeclaration.sCompany = rowDB[4].ToString();
            clsDeclaration.sUsername = rowDB[5].ToString();
            clsDeclaration.sPassword = rowDB[6].ToString();
            clsDeclaration.sCompCode = rowDB[7].ToString();

            clsDeclaration.sqlEmployeeList = clsDeclaration.sqlEmployeeList + "SELECT *,'" + clsDeclaration.sCompCode + "' AS Company from [" + clsDeclaration.sServer + "].[" + clsDeclaration.sCompany + "].dbo.[Employees]";

            i = i + 1;
        }

        clsDeclaration.sqlDepartmentList = "SELECT * from [" + ConfigurationManager.AppSettings["DBServer"] + "].[" + ConfigurationManager.AppSettings["DBName"] + "].dbo.[vwsDepartmentList]";
        

        TreeNode treeNode;

        #region Menu Groups
        treeNode = new TreeNode("Administration");
        tvModules.Nodes.Add(treeNode);

        tvModules.Nodes[0].Nodes.Add("System Initialization");
        tvModules.Nodes[0].Nodes[0].Nodes.Add("Connection");
        tvModules.Nodes[0].Nodes[0].Nodes.Add("SAP Connection");

        tvModules.Nodes[0].Nodes[0].Nodes.Add("Users");
        tvModules.Nodes[0].Nodes[0].Nodes[2].Nodes.Add("Users Account");
        tvModules.Nodes[0].Nodes[0].Nodes[2].Nodes.Add("Users Authorization");

        tvModules.Nodes[0].Nodes[0].Nodes.Add("Company List");
        tvModules.Nodes[0].Nodes[0].Nodes.Add("Account Setup");



        tvModules.Nodes[0].Nodes.Add("Setup");
        tvModules.Nodes[0].Nodes[1].Nodes.Add("Master Data Setup");
        tvModules.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Custom Payroll Setup");
        tvModules.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Branch");
        tvModules.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Position");
        tvModules.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Department");
        tvModules.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Payroll Period Builder");

        tvModules.Nodes[0].Nodes[1].Nodes.Add("Tables");
        tvModules.Nodes[0].Nodes[1].Nodes[1].Nodes.Add("S.S.S.");
        tvModules.Nodes[0].Nodes[1].Nodes[1].Nodes.Add("Philhealth");
        tvModules.Nodes[0].Nodes[1].Nodes[1].Nodes.Add("Pag-Ibig");
        tvModules.Nodes[0].Nodes[1].Nodes[1].Nodes.Add("Annual Tax Table");
        tvModules.Nodes[0].Nodes[1].Nodes[1].Nodes.Add("Tax Status & Exemption");

        //treeNode = new TreeNode("Linux");
        //tvModules.Nodes.Add(treeNode);

        treeNode = new TreeNode("Uploading");
        tvModules.Nodes.Add(treeNode);
        tvModules.Nodes[1].Nodes.Add("Account Uploading");
        tvModules.Nodes[1].Nodes.Add("Goverment Deduction Uploading");
        tvModules.Nodes[1].Nodes.Add("Import Log Files");
        tvModules.Nodes[1].Nodes.Add("Working Hours");


        treeNode = new TreeNode("Master File");
        tvModules.Nodes.Add(treeNode);
        tvModules.Nodes[2].Nodes.Add("Employee Master Data");
        tvModules.Nodes[2].Nodes.Add("Loan File Uploading");
        tvModules.Nodes[2].Nodes.Add("Loan File");

        tvModules.Nodes[2].Nodes.Add("Leave Generation");
        tvModules.Nodes[2].Nodes.Add("Leave Approval");

        tvModules.Nodes[2].Nodes.Add("Manual Leave Calculation");

        tvModules.Nodes[2].Nodes.Add("Leave Credit Conversion");


        treeNode = new TreeNode("Timekeeping Processing");
        tvModules.Nodes.Add(treeNode);
        tvModules.Nodes[3].Nodes.Add("Update Daily Time Record");
        tvModules.Nodes[3].Nodes.Add("Overtime Approval");
        tvModules.Nodes[3].Nodes.Add("Under Time Deduction");
        tvModules.Nodes[3].Nodes.Add("Attendance Monitoring");


        treeNode = new TreeNode("Payroll");
        tvModules.Nodes.Add(treeNode);
        tvModules.Nodes[4].Nodes.Add("Payroll Account Entry By Employee");
        tvModules.Nodes[4].Nodes.Add("Payroll Account Entry By Account");
        tvModules.Nodes[4].Nodes.Add("Payroll Uploading");
        tvModules.Nodes[4].Nodes.Add("Payroll Processing");
        tvModules.Nodes[4].Nodes.Add("13th Month Processing");
        tvModules.Nodes[4].Nodes.Add("13th Month Preview");
        tvModules.Nodes[4].Nodes.Add("Process Performance Bonus");
        tvModules.Nodes[4].Nodes.Add("Process Performance Bonus Preview");

        tvModules.Nodes[4].Nodes.Add("Process Performance Bonus Uploading");

        tvModules.Nodes[4].Nodes.Add("Service Incentive Leave Pay");

        tvModules.Nodes[4].Nodes.Add("Manual Bonus Processing");
        tvModules.Nodes[4].Nodes.Add("Manual Payroll");
        tvModules.Nodes[4].Nodes.Add("Last Pay Processing");
        tvModules.Nodes[4].Nodes.Add("Payroll Period Locker");
        tvModules.Nodes[4].Nodes.Add("Deduction Uploader");
        tvModules.Nodes[4].Nodes.Add("SILP Uploader");


        treeNode = new TreeNode("Reports");
        tvModules.Nodes.Add(treeNode);
        tvModules.Nodes[5].Nodes.Add("Master File Report");
        tvModules.Nodes[5].Nodes[0].Nodes.Add("Employee Detailed Report");
        tvModules.Nodes[5].Nodes[0].Nodes.Add("Employee Summary Report");
        tvModules.Nodes[5].Nodes[0].Nodes.Add("Employee List And Loan Summary");
        tvModules.Nodes[5].Nodes[0].Nodes.Add("Loan Summary Per Employee");
        tvModules.Nodes[5].Nodes[0].Nodes.Add("Loan List Report");

        //tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Per Account By Branch");
        //tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Per Branch By Account");
        tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Employee Loan List Report");
        tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Employee Loan Report - Installment");
        tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Employee Loan Report - Goverment");

        tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Loan Payment Remitance");
        //tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Resigned Employee By Area");
        tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Loan For Relative Report");
        tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Loan Remitance - Eye Glasses");
        //tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Loan Remitance");
        tvModules.Nodes[5].Nodes[0].Nodes[4].Nodes.Add("Loan Remitance Per Area");
        //tvModules.Nodes[5].Nodes[0].Nodes.Add("Loan Ledger");
        tvModules.Nodes[5].Nodes[0].Nodes.Add("One Time Deduction Monitoring Report");
        tvModules.Nodes[5].Nodes[0].Nodes.Add("Over Deduction List");
        //tvModules.Nodes[5].Nodes[0].Nodes.Add("Service Incenstive Leave Pay Report");
        tvModules.Nodes[5].Nodes.Add("Timekeeping Report");
        tvModules.Nodes[5].Nodes[1].Nodes.Add("Incomplete In and Out");
        tvModules.Nodes[5].Nodes[1].Nodes.Add("Employee Absences / UnderTime");
        tvModules.Nodes[5].Nodes[1].Nodes.Add("Employee Tardiness");
        tvModules.Nodes[5].Nodes.Add("Payroll Report");

        //tvModules.Nodes[5].Nodes[2].Nodes.Add("Managers");
        //            tvModules.Nodes[5].Nodes[2].Nodes[0].Nodes.Add("Payroll Register Managers");
        //            tvModules.Nodes[5].Nodes[2].Nodes[0].Nodes.Add("Summary of Deduction Managers");
        //            tvModules.Nodes[5].Nodes[2].Nodes[0].Nodes.Add("Payroll Register Per Group");
        //            tvModules.Nodes[5].Nodes[2].Nodes[0].Nodes.Add("Summary of Deduction Per Group");
        //    tvModules.Nodes[5].Nodes[2].Nodes.Add("Rank And File");
        //            tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("Summary of Deduction");
        //            tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("Payroll Register");
        //            tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("Payroll Register Corrected");
        //            tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("Payroll Register Per Area");
        //            tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("Last Pay Register");
        //            tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("Last Pay Register Per Employee");
        //            tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("Payslip");

        tvModules.Nodes[5].Nodes[2].Nodes.Add("Goverment");
        tvModules.Nodes[5].Nodes[2].Nodes[0].Nodes.Add("Goverment Remittance");
        tvModules.Nodes[5].Nodes[2].Nodes[0].Nodes.Add("Per Company Gross Pay");
        tvModules.Nodes[5].Nodes[2].Nodes[0].Nodes.Add("Employee Gross Pay Report");
        tvModules.Nodes[5].Nodes[2].Nodes.Add("General");
        tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("ATM Bank Report Managers");
        tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("ATM Bank Report Non Managers");
        tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("ATM Bank Report Warehouse");
        tvModules.Nodes[5].Nodes[2].Nodes[1].Nodes.Add("CASH Report Non ATM Head Office");
        tvModules.Nodes[5].Nodes[2].Nodes.Add("Register");
        tvModules.Nodes[5].Nodes[2].Nodes.Add("Adjustment");
        tvModules.Nodes[5].Nodes[2].Nodes.Add("Payslip");
        tvModules.Nodes[5].Nodes[2].Nodes.Add("Quit Claims");
        tvModules.Nodes[5].Nodes[2].Nodes.Add("Bank Report");
        tvModules.Nodes[5].Nodes[2].Nodes.Add("Performance Bonus Report");
        tvModules.Nodes[5].Nodes[2].Nodes.Add("SILP Slip");
        //tvModules.Nodes[5].Nodes[2].Nodes.Add("Service Incentive Report");

        treeNode = new TreeNode("SAP");
        tvModules.Nodes.Add(treeNode);
        tvModules.Nodes[6].Nodes.Add("SAP Payroll Entries Uploader");

        tvModules.Nodes[6].Nodes.Add("SAP Payroll Entries Uploader 2019");
        //tvModules.Nodes[6].Nodes.Add("SAP Branch Payroll Deduction Uploader");
        tvModules.Nodes[6].Nodes.Add("SAP Loan Payment Uploader");
        tvModules.Nodes[6].Nodes.Add("SAP Loan File Uploader");
        tvModules.Nodes[6].Nodes.Add("SAP Employee Aging Report");
        tvModules.Nodes[6].Nodes.Add("IC Loan Uploader");
        tvModules.Nodes[6].Nodes.Add("IC VS SAP Loan Comparison Report");
        #endregion


        CallRecursive(tvModules);
    }

    private void PrintRecursive(TreeNode treeNode)
    {
        // Print the node.  
        System.Diagnostics.Debug.WriteLine(treeNode.Text);

        Add_Data(treeNode.FullPath, treeNode.Level.ToString(), treeNode.Text);
      
        // Print each node recursively.  
        foreach (TreeNode tn in treeNode.Nodes)
        {
            PrintRecursive(tn);
        }
    }

    // Call the procedure using the TreeView.  
    private void CallRecursive(TreeView treeView)
    {
        // Print each node recursively.  
        TreeNodeCollection nodes = treeView.Nodes;
        foreach (TreeNode n in nodes)
        {
            PrintRecursive(n);
        }
    }



    private void Add_Data(string oIndex, string oID, string Code)
    {

        string _InsertData = @"
                                    IF NOT EXISTS (SELECT 'TRUE' FROM [OUAM] Z WHERE Z.Module = '" + Code + @"')
                                    BEGIN
                                            INSERT INTO [dbo].[OUAM]
                                                        ([Index],[ModuleCode],[Module])
                                                    VALUES
                                                    (
	                                                        '" + oIndex + @"', '" + oID + @"', '" + Code + @"'
                                                    )
                                    END
                             ";


        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _InsertData);
    }


    private void tvModules_MouseDoubleClick(object sender, MouseEventArgs e)
    {

        TreeNode node = tvModules.SelectedNode;
        //MessageBox.Show(string.Format("You selected: {0}", node.Text));
        try
        {

            switch (node.Text)
            {
                #region Reports
                    #region Master Files
                        #region Employee Detailed Report
                        case "Employee Detailed Report":
                            if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                            {
                                MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                return;
                            }


                            foreach (Form form in Application.OpenForms)
                            {
                                if (form.GetType() == typeof(frmEmployeeDataParameter))
                                {
                                    form.Activate();
                                    return;
                                }
                            }

                            //clsDeclaration.sReportTag = 24;
                            clsDeclaration.sReportID = 2;

                            frmEmployeeDataParameter frmEmployeeDetailed = new frmEmployeeDataParameter();
                            frmEmployeeDetailed.MdiParent = this;
                            frmEmployeeDetailed.Show();

                            //if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                            //{
                            //    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                            //    return;
                            //}

                            //foreach (Form form in Application.OpenForms)
                            //{
                            //    if (form.GetType() == typeof(frmReportViewer))
                            //    {
                            //        form.Activate();
                            //        return;
                            //    }
                            //}

                            //clsDeclaration.sReportTag = 3;
                            //clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Detailed Report.rpt";

                            //frmReportViewer frmReportViewer4 = new frmReportViewer();
                            //frmReportViewer4.MdiParent = this;
                            //frmReportViewer4.Show();

                            break;
                        #endregion
                        #region Employee Summary Report
                        case "Employee Summary Report":
                            if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                            {
                                MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                return;
                            }


                            foreach (Form form in Application.OpenForms)
                            {
                                if (form.GetType() == typeof(frmEmployeeDataParameter))
                                {
                                    form.Activate();
                                    return;
                                }
                            }

                            //clsDeclaration.sReportTag = 24;
                            clsDeclaration.sReportID = 1;
                            frmEmployeeDataParameter frmEmployeeDataParameter = new frmEmployeeDataParameter();
                            frmEmployeeDataParameter.MdiParent = this;
                            frmEmployeeDataParameter.Show();

                            //foreach (Form form in Application.OpenForms)
                            //{
                            //    if (form.GetType() == typeof(frmReportViewer))
                            //    {
                            //        form.Activate();
                            //        return;
                            //    }
                            //}

                            //clsDeclaration.sReportTag = 24;

                            //frmReportViewer frmEmployeeList = new frmReportViewer();
                            //frmEmployeeList.MdiParent = this;
                            //frmEmployeeList.Show();

                            break;
                        #endregion
                        #region Employee List And Loan Summary
                        case "Employee List And Loan Summary":
                            if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                            {
                                MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                return;
                            }


                            foreach (Form form in Application.OpenForms)
                            {
                                if (form.GetType() == typeof(frmEmpDateParameter))
                                {
                                    form.Activate();
                                    return;
                                }
                            }

                            //clsDeclaration.sReportTag = 24;
                            clsDeclaration.sReportID = 3;
                            frmEmpDateParameter = new frmEmpDateParameter();
                            frmEmpDateParameter.MdiParent = this;
                            frmEmpDateParameter.Show();

                            break;

                #endregion
                        #region Loan Summary Per Employee
                case "Loan Summary Per Employee":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmEmployeeNoParameter))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmEmployeeNoParameter frmEmployeeNoParameter = new frmEmployeeNoParameter();
                    frmEmployeeNoParameter._RequestID = 4;
                    frmEmployeeNoParameter.MdiParent = this;
                    frmEmployeeNoParameter.Show();

                    break;
                #endregion
                        #region Loan Reports
                    
                            #region Employee Loan List Report
                            case "Employee Loan List Report":
                                if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                                {
                                    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                    return;
                                }


                                foreach (Form form in Application.OpenForms)
                                {
                                    if (form.GetType() == typeof(frmEmpDateParameter))
                                    {
                                        form.Activate();
                                        return;
                                    }
                                }

                                //clsDeclaration.sReportTag = 24;
                                clsDeclaration.sReportID = 6;
                                frmEmpDateParameter = new frmEmpDateParameter();
                                frmEmpDateParameter.MdiParent = this;
                                frmEmpDateParameter.Show();

                                //if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                                //{
                                //    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                //    return;
                                //}

                                //foreach (Form form in Application.OpenForms)
                                //{
                                //    if (form.GetType() == typeof(frmReportViewer))
                                //    {
                                //        form.Activate();
                                //        return;
                                //    }
                                //}

                                ////clsDeclaration.sReportTag = 5;
                                //clsDeclaration.sReportTag = 25;
                                //clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Loan Report.rpt";
                                //frmReportViewer frmEmployeeLoanReport = new frmReportViewer();
                                //frmEmployeeLoanReport.MdiParent = this;
                                //frmEmployeeLoanReport.Show();

                                break;
                            #endregion
                            #region Employee Loan Report - Installment

                            case "Employee Loan Report - Installment":
                                if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                                {
                                    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                    return;
                                }


                                foreach (Form form in Application.OpenForms)
                                {
                                    if (form.GetType() == typeof(frmEmpDateParameter))
                                    {
                                        form.Activate();
                                        return;
                                    }
                                }

                                //clsDeclaration.sReportTag = 24;
                                clsDeclaration.sReportID = 5;
                                frmEmpDateParameter = new frmEmpDateParameter();
                                //frmEmpDateParameter frmEmpDateInsParameter = new frmEmpDateParameter();
                                frmEmpDateParameter.MdiParent = this;
                                frmEmpDateParameter.Show();
                                //if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                                //{
                                //    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                //    return;
                                //}

                                //foreach (Form form in Application.OpenForms)
                                //{
                                //    if (form.GetType() == typeof(frmReportViewer))
                                //    {
                                //        form.Activate();
                                //        return;
                                //    }
                                //}

                                ////clsDeclaration.sReportTag = 5;
                                //clsDeclaration.sReportTag = 25;
                                //clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Loan Report Loans.rpt";
                                //frmReportViewer frmEmployeeLoanReportLoans = new frmReportViewer();
                                //frmEmployeeLoanReportLoans.MdiParent = this;
                                //frmEmployeeLoanReportLoans.Show();

                                break;
                            #endregion
                            #region Employee Loan Report - Goverment
                            case "Employee Loan Report - Goverment":
                                if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                                {
                                    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                    return;
                                }


                                foreach (Form form in Application.OpenForms)
                                {
                                    if (form.GetType() == typeof(frmEmpDateParameter))
                                    {
                                        form.Activate();
                                        return;
                                    }
                                }

                                //clsDeclaration.sReportTag = 24;
                                clsDeclaration.sReportID = 4;
                                frmEmpDateParameter = new frmEmpDateParameter();
                                //frmEmpDateParameter frmEmpDateGovParameter = new frmEmpDateParameter();
                                frmEmpDateParameter.MdiParent = this;
                                frmEmpDateParameter.Show();


                                //if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                                //{
                                //    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                //    return;
                                //}

                                //foreach (Form form in Application.OpenForms)
                                //{
                                //    if (form.GetType() == typeof(frmReportViewer))
                                //    {
                                //        form.Activate();
                                //        return;
                                //    }
                                //}

                                ////clsDeclaration.sReportTag = 5;
                                //clsDeclaration.sReportTag = 25;
                                //clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Loan Report Goverment.rpt";
                                //frmReportViewer frmEmployeeLoanReportGov = new frmReportViewer();
                                //frmEmployeeLoanReportGov.MdiParent = this;
                                //frmEmployeeLoanReportGov.Show();

                                break;
                            #endregion
                            #region Loan Payment Remitance
                            case "Loan Payment Remitance":
                                if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                                {
                                    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                    return;
                                }


                                foreach (Form form in Application.OpenForms)
                                {
                                    if (form.GetType() == typeof(frmEmpDateParameter))
                                    {
                                        form.Activate();
                                        return;
                                    }
                                }


                                clsDeclaration.sReportID = 7;
                                frmEmpDateAccountParameter = new frmEmpDateAccountParameter();
                                frmEmpDateAccountParameter.MdiParent = this;
                                frmEmpDateAccountParameter.Show();
                                break;
                            #endregion
                            #region Loan For Relative Report

                            #endregion
                            #region Loan Remitance - Eye Glasses
                            case "Loan Remitance - Eye Glasses":
                                if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                                {
                                    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                    return;
                                }

                                foreach (Form form in Application.OpenForms)
                                {
                                    if (form.GetType() == typeof(frmReport))
                                    {
                                        form.Activate();
                                        return;
                                    }
                                }


                                clsDeclaration.sReportTag = 3;
                                frmDateRangeParameter frmDateRangeParameter = new frmDateRangeParameter();
                                frmDateRangeParameter.MdiParent = this;
                                frmDateRangeParameter.Show();

                                break;
                            #endregion
                            #region Loan Remitance Per Area
                            case "Loan Remitance Per Area":
                                if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                                {
                                    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                                    return;
                                }

                                foreach (Form form in Application.OpenForms)
                                {
                                    if (form.GetType() == typeof(frmReport))
                                    {
                                        form.Activate();
                                        return;
                                    }
                                }

                                clsDeclaration.sReportTag = 1;
                                clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Loan Summary Remittance Area.rpt";
                                frmReport = new frmReport();
                                frmReport.MdiParent = this;
                                frmReport.Show();

                                break;
                #endregion

                #endregion

                        #region Loan Ledger
                case "Loan Ledger":

                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportDisplay))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    //clsDeclaration.sReportTag = 5;
                    clsDeclaration.sReportTag = 22;

                    frmReportDisplay frmLoanLedger = new frmReportDisplay();
                    frmReportDisplay._RequestType = "LoanLedger";
                    frmLoanLedger.MdiParent = this;
                    frmLoanLedger.Show();

                    break;
                //case "Per Branch By Account":

                //    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                //    {
                //        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                //        return;
                //    }

                //    foreach (Form form in Application.OpenForms)
                //    {
                //        if (form.GetType() == typeof(frmEmployeeNoParameter))
                //        {
                //            form.Activate();
                //            return;
                //        }
                //    }

                //    frmEmployeeNoParameter._RequestID = 6;
                //    frmEmployeeNoParameter.MdiParent = this;
                //    frmEmployeeNoParameter.Show();

                //    break;
                #endregion
                    #endregion
                    #region Time Keeping

                    #endregion
                    #region Payroll Processing
                        
                    #endregion
                #endregion

                #region Last Pay Processing
                case "Last Pay Processing":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLastPayProcessing))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmLastPayProcessing frmLastPayProcessing = new frmLastPayProcessing();
                    frmLastPayProcessing.MdiParent = this;
                    frmLastPayProcessing.Show();
                    break;
                #endregion
                #region Per Company Gross Pay
                case "Per Company Gross Pay":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 9;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Gross Pay Summary Report.rpt";

                    frmReportViewer frmReportViewer9 = new frmReportViewer();
                    frmReportViewer9.MdiParent = this;
                    frmReportViewer9.Show();
                    break;
                #endregion
                #region SAP Employee Aging Report
                case "SAP Employee Aging Report":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSAPEmployeeAging))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSAPEmployeeAging frmSAPEmployeeAging = new frmSAPEmployeeAging();
                    frmSAPEmployeeAging.MdiParent = this;
                    frmSAPEmployeeAging.Show();
                    break;

                #endregion
                #region SAP Loan File Uploader
                case "SAP Loan File Uploader":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSAPLoanDataUploading))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSAPLoanDataUploading frmSAPLoanDataUploading = new frmSAPLoanDataUploading();
                    frmSAPLoanDataUploading.MdiParent = this;
                    frmSAPLoanDataUploading.Show();
                    break;
                #endregion
                #region IC Loan Uploader
                case "IC Loan Uploader":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSAPLoanIntegration))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSAPLoanIntegration frmSAPLoanIntegration = new frmSAPLoanIntegration();
                    frmSAPLoanIntegration.MdiParent = this;
                    frmSAPLoanIntegration.Show();
                    break;
                #endregion

                #region IC VS SAP Loan Comparison Report
                case "IC VS SAP Loan Comparison Report":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSAPLoanComparisonReport))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSAPLoanComparisonReport frmSAPLoanComparisonReport = new frmSAPLoanComparisonReport();
                    frmSAPLoanComparisonReport.MdiParent = this;
                    frmSAPLoanComparisonReport.Show();
                    break;
                #endregion
                #region SAP Payroll Entries Uploader
                case "SAP Payroll Entries Uploader":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }



                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSAPBranchPayrollUploader))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSAPBranchPayrollUploader frmSAPBranchPayrollUploader = new frmSAPBranchPayrollUploader();
                    frmSAPBranchPayrollUploader.MdiParent = this;
                    frmSAPBranchPayrollUploader.Show();



                    //foreach (Form form in Application.OpenForms)
                    //{
                    //    if (form.GetType() == typeof(frmPayrollEntries))
                    //    {
                    //        form.Activate();
                    //        return;
                    //    }
                    //}

                    //frmPayrollEntries frmPayrollEntries = new frmPayrollEntries();
                    //frmPayrollEntries.MdiParent = this;
                    //frmPayrollEntries.Show();

                    break;
                #endregion
                #region Comment Group
                //case "SAP Payroll Entries Uploader":
                //    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                //    {
                //        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                //        return;
                //    }

                //    foreach (Form form in Application.OpenForms)
                //    {
                //        if (form.GetType() == typeof(frmSAPUploader))
                //        {
                //            form.Activate();
                //            return;
                //        }
                //    }

                //    frmSAPUploader frmSAPUploader = new frmSAPUploader();
                //    frmSAPUploader.MdiParent = this;
                //    frmSAPUploader.Show();

                //    break;
                #endregion
                #region SAP Payroll Entries Uploader 2019


                case "SAP Payroll Entries Uploader 2019":

                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSAPBranchPayrollUploader))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSAPPayrollEntriesUploader frmSAPPayrollEntriesUploader = new frmSAPPayrollEntriesUploader();
                    frmSAPPayrollEntriesUploader.MdiParent = this;
                    frmSAPPayrollEntriesUploader.Show();
                    break;
                #endregion
                #region SAP Loan Payment Uploader

                case "SAP Loan Payment Uploader":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSAPLoanUploader))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSAPLoanPayment frmSAPLoanPayment = new frmSAPLoanPayment();
                    frmSAPLoanPayment.MdiParent = this;
                    frmSAPLoanPayment.Show();
                    break;
                #endregion
                #region Goverment Remittance
                case "Goverment Remittance":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmGovermentRemittance))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmGovermentRemittance frmGovermentRemittance = new frmGovermentRemittance();
                    frmGovermentRemittance.MdiParent = this;
                    frmGovermentRemittance.Show();
                    break;
                #endregion
                #region Manual Bonus Processing
                case "Manual Bonus Processing":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmManualBonus))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmManualBonus frmManualBonus = new frmManualBonus();
                    frmManualBonus.MdiParent = this;
                    frmManualBonus.Show();
                    break;
                #endregion
                #region Manual Payroll
                case "Manual Payroll":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmManualPayroll))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmManualPayroll frmManualPayroll = new frmManualPayroll();
                    frmManualPayroll.MdiParent = this;
                    frmManualPayroll.Show();
                    break;
                #endregion
                #region Payroll Period Locker
                case "Payroll Period Locker":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLockPayroll))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmLockPayroll frmLockPayroll = new frmLockPayroll();
                    frmLockPayroll.MdiParent = this;
                    frmLockPayroll.Show();
                    break;
                #endregion
                #region Loan File Uploading
                case "Loan File Uploading":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLoanUploading))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmLoanUploading frmLoanUploading = new frmLoanUploading();
                    frmLoanUploading.MdiParent = this;
                    frmLoanUploading.Show();
                    break;
                #endregion
                #region Manual Leave Calculation
                case "Manual Leave Calculation":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLeaveConvertion))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmLeaveConvertion frmLeaveConvertion = new frmLeaveConvertion();
                    frmLeaveConvertion.MdiParent = this;
                    frmLeaveConvertion.Show();
                    break;
                #endregion
                #region Leave Approval
                case "Leave Approval":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLeaveApproved))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmLeaveApproved frmLeaveApproved = new frmLeaveApproved();
                    frmLeaveApproved.MdiParent = this;
                    frmLeaveApproved.Show();
                    break;
                #endregion
                #region Leave Credit Conversion
                case "Leave Credit Conversion":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLeaveCreditConversion))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmLeaveCreditConversion frmLeaveCreditConversion = new frmLeaveCreditConversion();
                    frmLeaveCreditConversion.MdiParent = this;
                    frmLeaveCreditConversion.Show();
                    break;
                #endregion


                #region Leave Generation
                case "Leave Generation":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLeaveGeneration))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmLeaveGeneration frmLeaveGeneration = new frmLeaveGeneration();
                    frmLeaveGeneration.MdiParent = this;
                    frmLeaveGeneration.Show();
                    break;
                #endregion
                #region Loan File
                case "Loan File":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLoanFileData))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmLoanFileData frmLoanFileData = new frmLoanFileData();
                    frmLoanFileData.MdiParent = this;
                    frmLoanFileData.Show();
                    break;
                #endregion
                #region Payroll Account Entry By Account
                case "Payroll Account Entry By Account":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmAccountEntryByAccount))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmAccountEntryByAccount frmAccountEntryByAccount = new frmAccountEntryByAccount();
                    frmAccountEntryByAccount.MdiParent = this;
                    frmAccountEntryByAccount.Show();
                    break;
                #endregion
                #region Payroll Account Entry By Employee
                case "Payroll Account Entry By Employee":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmAccountEntry))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmAccountEntry frmAccountEntry = new frmAccountEntry();
                    frmAccountEntry.MdiParent = this;
                    frmAccountEntry.Show();
                    break;
                #endregion
                #region Payroll Uploading
                case "Payroll Uploading":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmPayrollUploading))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmPayrollUploading frmPayrollUploading = new frmPayrollUploading();
                    frmPayrollUploading.MdiParent = this;
                    frmPayrollUploading.Show();
                    break;
                #endregion
                #region Process Performance Bonus Uploading
                case "Process Performance Bonus Uploading":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmBonusUploader))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmBonusUploader frmBonusUploader = new frmBonusUploader();
                    frmBonusUploader.MdiParent = this;
                    frmBonusUploader.Show();
                    break;
                #endregion
                #region Process Performance Bonus Preview
                case "Process Performance Bonus Preview":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmPerformanceBonusPreview))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmPerformanceBonusPreview frmPerformanceBonusPreview = new frmPerformanceBonusPreview();
                    frmPerformanceBonusPreview.MdiParent = this;
                    frmPerformanceBonusPreview.Show();

                    break;
                #endregion
                #region Process Performance Bonus
                case "Process Performance Bonus":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmPerformanceBonus))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmPerformanceBonus frmPerformanceBonus = new frmPerformanceBonus();
                    frmPerformanceBonus.MdiParent = this;
                    frmPerformanceBonus.Show();

                    break;
                #endregion
                #region 13th Month Processing
                case "13th Month Processing":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frm13thMonthProcessing))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frm13thMonthProcessing frm13thMonthProcessing = new frm13thMonthProcessing();
                    frm13thMonthProcessing.MdiParent = this;
                    frm13thMonthProcessing.Show();
                    //frm13thMonth frm13thMonth = new frm13thMonth();
                    //frm13thMonth.MdiParent = this;
                    //frm13thMonth.Show();

                    break;
                #endregion

                #region 13th Month Preview
                case "13th Month Preview":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frm13thMonthPreview))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frm13thMonthPreview frm13thMonthPreview = new frm13thMonthPreview();
                    frm13thMonthPreview.MdiParent = this;
                    frm13thMonthPreview.Show();
                    //frm13thMonth frm13thMonth = new frm13thMonth();
                    //frm13thMonth.MdiParent = this;
                    //frm13thMonth.Show();

                    break;
                #endregion
                #region Payroll Processing
                case "Payroll Processing":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmProcessPayroll))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmProcessPayroll frmProcessPayroll = new frmProcessPayroll();
                    frmProcessPayroll.MdiParent = this;
                    frmProcessPayroll.Show();

                    break;
                #endregion
                #region Account Setup
                case "Account Setup":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmAccountSetup))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmAccountSetup frmAccountSetup = new frmAccountSetup();
                    frmAccountSetup.MdiParent = this;
                    frmAccountSetup.Show();

                    break;
                #endregion
                #region Employee Master Data
                case "Employee Master Data":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }


                    if (clsDeclaration.sSuperUser == "1")
                    {
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form.GetType() == typeof(frmEmployeeMasterData))
                            {
                                form.Activate();
                                return;
                            }
                        }

                        frmEmployeeMasterData frmEmployeeMasterData = new frmEmployeeMasterData();
                        frmEmployeeMasterData.MdiParent = this;
                        frmEmployeeMasterData.Show();
                    }
                    else
                    {
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form.GetType() == typeof(frmMasterData))
                            {
                                form.Activate();
                                return;
                            }
                        }

                        frmMasterData frmMasterData = new frmMasterData();
                        frmMasterData.MdiParent = this;
                        frmMasterData.Show();
                    }



                    break;

                #endregion
                #region Users Account
                case "Users Account":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmUsers))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmUsers frmUsers = new frmUsers();
                    frmUsers.MdiParent = this;
                    frmUsers.Show();

                    break;
                #endregion
                #region Update Daily Time Record
                case "Update Daily Time Record":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmDTR))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmDTR frmDTR = new frmDTR();
                    frmDTR.MdiParent = this;
                    frmDTR.Show();

                    break;

                #endregion
                #region Payroll Period Builder
                case "Payroll Period Builder":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmPayrollPeriodBuilder))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmPayrollPeriodBuilder frmPayrollPeriodBuilder = new frmPayrollPeriodBuilder();
                    frmPayrollPeriodBuilder.MdiParent = this;
                    frmPayrollPeriodBuilder.Show();
                    break;
                #endregion
                #region Department
                case "Department":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmDepartment))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmDepartment frmDepartment = new frmDepartment();
                    frmDepartment.MdiParent = this;
                    frmDepartment.Show();

                    break;
                #endregion
                #region Position
                case "Position":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmPosition))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmPosition frmPosition = new frmPosition();
                    frmPosition.MdiParent = this;
                    frmPosition.Show();

                    break;
                #endregion
                #region Attendance Monitoring
                case "Attendance Monitoring":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmAttendanceMonitoring))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmAttendanceMonitoring frmAttendanceMonitoring = new frmAttendanceMonitoring();
                    frmAttendanceMonitoring.MdiParent = this;
                    frmAttendanceMonitoring.Show();

                    break;
                #endregion
                #region Under Time Deduction
                case "Under Time Deduction":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmUnderTime))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmUnderTime frmUnderTime = new frmUnderTime();
                    frmUnderTime.MdiParent = this;
                    frmUnderTime.Show();

                    break;
                #endregion
                #region Branch
                case "Branch":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmBranches))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmBranches frmBranches = new frmBranches();
                    frmBranches.MdiParent = this;
                    frmBranches.Show();

                    break;
                #endregion
                #region CASH Report Non ATM Head Office
                case "CASH Report Non ATM Head Office":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 8;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Cash Non Managers.rpt";

                    frmReportViewer frmReportViewer14a = new frmReportViewer();
                    frmReportViewer14a.MdiParent = this;
                    frmReportViewer14a.Show();

                    break;
                #endregion
                #region ATM Bank Report Warehouse
                case "ATM Bank Report Warehouse":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 8;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\ATM Bank Warehouse.rpt";

                    frmReportViewer frmReportViewer15 = new frmReportViewer();
                    frmReportViewer15.MdiParent = this;
                    frmReportViewer15.Show();
                    break;
                #endregion
                #region ATM Bank Report Non Managers
                case "ATM Bank Report Non Managers":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 8;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\ATM Bank Non Managers.rpt";

                    frmReportViewer frmReportViewer14 = new frmReportViewer();
                    frmReportViewer14.MdiParent = this;
                    frmReportViewer14.Show();

                    break;
                #endregion
                #region ATM Bank Report Managers
                case "ATM Bank Report Managers":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 8;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\ATM Bank Manager.rpt";

                    frmReportViewer frmReportViewer13 = new frmReportViewer();
                    frmReportViewer13.MdiParent = this;
                    frmReportViewer13.Show();

                    break;
                #endregion
                #region Employee Tardiness
                case "Employee Tardiness":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 1;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Tardiness.rpt";

                    frmReportViewer frmReportViewer3 = new frmReportViewer();
                    frmReportViewer3.MdiParent = this;
                    frmReportViewer3.Show();

                    break;
                #endregion
                #region Summary of Deduction Per Group
                case "Summary of Deduction Per Group":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 7;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Summary of Deduction Per Group.rpt";

                    frmReportViewer frmReportViewer12 = new frmReportViewer();
                    frmReportViewer12.MdiParent = this;
                    frmReportViewer12.Show();

                    break;
                #endregion
                #region Summary of Deduction Managers
                case "Summary of Deduction Managers":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 7;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Summary of Deduction Manager.rpt";

                    frmReportViewer frmReportViewer11 = new frmReportViewer();
                    frmReportViewer11.MdiParent = this;
                    frmReportViewer11.Show();

                    break;
                #endregion
                #region Summary of Deduction
                case "Summary of Deduction":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 7;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Summary of Deduction.rpt";

                    frmReportViewer frmReportViewer7 = new frmReportViewer();
                    frmReportViewer7.MdiParent = this;
                    frmReportViewer7.Show();

                    break;
                #endregion
                #region Payroll Register Corrected
                case "Payroll Register Corrected":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 7;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payroll Register Corrected.rpt";

                    frmReportViewer frmReportViewer8 = new frmReportViewer();
                    frmReportViewer8.MdiParent = this;
                    frmReportViewer8.Show();

                    break;
                #endregion
                #region Payroll Register Managers
                case "Payroll Register Managers":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 5;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payroll Register Manager.rpt";

                    frmReportViewer frmReportViewer10 = new frmReportViewer();
                    frmReportViewer10.MdiParent = this;
                    frmReportViewer10.Show();

                    break;
                #endregion
                #region Payroll Register Per Group
                case "Payroll Register Per Group":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 5;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payroll Register Per Group.rpt";

                    frmReportViewer frmReportViewer6 = new frmReportViewer();
                    frmReportViewer6.MdiParent = this;
                    frmReportViewer6.Show();

                    break;
                #endregion
                #region Last Pay Register Per Employee
                case "Last Pay Register Per Employee":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    //clsDeclaration.sReportTag = 5;
                    clsDeclaration.sReportTag = 10;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Last Pay Register Per Employee.rpt";

                    frmReportViewer frmReportViewer5e = new frmReportViewer();
                    frmReportViewer5e.MdiParent = this;
                    frmReportViewer5e.Show();

                    break;
                #endregion
                #region Last Pay Register
                case "Last Pay Register":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    //clsDeclaration.sReportTag = 5;
                    clsDeclaration.sReportTag = 7;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Last Pay Register.rpt";

                    frmReportViewer frmReportViewer5a = new frmReportViewer();
                    frmReportViewer5a.MdiParent = this;
                    frmReportViewer5a.Show();

                    break;
                #endregion
                #region Payslip

                case "Payslip":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportDisplay))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    ////clsDeclaration.sReportTag = 5;
                    //clsDeclaration.sReportTag = 7;
                    //clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payslip.rpt";

                    //frmReportViewer frmReportViewer5d = new frmReportViewer();
                    //frmReportViewer5d.MdiParent = this;
                    //frmReportViewer5d.Show();

                    frmPaySlipParameter frmPaySlipParameter = new frmPaySlipParameter();
                    frmPaySlipParameter.MdiParent = this;
                    frmPaySlipParameter.Show();


                    //clsDeclaration.sReportTag = 21;

                    //frmReportDisplay frmPaySlip = new frmReportDisplay();
                    //frmReportDisplay._RequestType = "Payslip";
                    //frmPaySlip.MdiParent = this;
                    //frmPaySlip.Show();

                    break;
                #endregion
                #region Payroll Register Per Area

                case "Payroll Register Per Area":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    //clsDeclaration.sReportTag = 5;
                    clsDeclaration.sReportTag = 11;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payroll Register Per Area.rpt";

                    frmReportViewer frmReportViewer11a = new frmReportViewer();
                    frmReportViewer11a.MdiParent = this;
                    frmReportViewer11a.Show();

                    break;
                #endregion
                #region Register

                case "Register":
                    //if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    //{
                    //    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                    //    return;
                    //}

                    //foreach (Form form in Application.OpenForms)
                    //{
                    //    if (form.GetType() == typeof(frmReportDisplay))
                    //    {
                    //        form.Activate();
                    //        return;
                    //    }
                    //}
                    //clsDeclaration.sReportTag = 20;

                    //frmReportDisplay frmRegister = new frmReportDisplay();
                    //frmReportDisplay._RequestType = "Register";
                    //frmRegister.MdiParent = this;
                    //frmRegister.Show();


                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmPayrollRegParameter))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 20;
                    frmPayrollRegParameter frmPayrollRegParameter = new frmPayrollRegParameter();
                    frmPayrollRegParameter._RequestType = "Register";
                    frmPayrollRegParameter.MdiParent = this;
                    frmPayrollRegParameter.Show();

                    break;
                #endregion
                #region Quit Claims
                case "Quit Claims":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    //clsDeclaration.sReportTag = 5;
                    clsDeclaration.sReportTag = 23;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\QuitClaims.rpt";
                    frmReportViewer frmQuiClaims = new frmReportViewer();
                    frmReportViewer._RequestType = "QuitClaims";
                    frmQuiClaims.MdiParent = this;
                    frmQuiClaims.Show();

                    break;

                #endregion
                #region Loan Remitance

                case "Loan Remitance":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReport))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 1;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Loan Summary Remittance.rpt";
                    frmReport.MdiParent = this;
                    frmReport.Show();

                    break;
                #endregion
                #region Employee Gross Pay Report

                case "Employee Gross Pay Report":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmGrossPayPreview))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    ////frmLeaveGenParameter frmLeaveGenParameter1 = new frmLeaveGenParameter();
                    //clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Gross Pay Report.rpt";

                    //frmGrossEmployeeParameter frmGrossEmployeeParameter = new frmGrossEmployeeParameter();
                    //frmGrossEmployeeParameter._RequestType = "GrossPay";
                    //frmGrossEmployeeParameter.MdiParent = this;
                    //frmGrossEmployeeParameter.Show();

                    frmGrossPayPreview frmGrossPayPreview = new frmGrossPayPreview();
                    frmGrossPayPreview.MdiParent = this;
                    frmGrossPayPreview.Show();
                    break;

                #endregion
                #region Performance Bonus Report
                case "Performance Bonus Report":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLeaveGenParameter))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    //frmLeaveGenParameter frmLeaveGenParameter3 = new frmLeaveGenParameter();
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Performace Report.rpt";
                    frmLeaveGenParameter = new frmLeaveGenParameter();
                    frmLeaveGenParameter._RequestType = "0";
                    frmLeaveGenParameter.MdiParent = this;
                    frmLeaveGenParameter.Show();
                    break;
                #endregion
                #region Bank Report
                case "Bank Report":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmEmployeeFilterParameter))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmEmployeeFilterParameter = new frmEmployeeFilterParameter();
                    frmEmployeeFilterParameter._RequestID = 7;
                    frmEmployeeFilterParameter.MdiParent = this;
                    frmEmployeeFilterParameter.Show();
                    break;
                #endregion
                #region Resigned Employee By Area
                case "Resigned Employee By Area":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmPayrollPeriodParameter))
                        {
                            form.Activate();
                            return;
                        }
                    }


                    frmPayrollPeriodParameter._RequestID = 5;
                    frmPayrollPeriodParameter.MdiParent = this;
                    frmPayrollPeriodParameter.Show();
                    break;
                #endregion
                #region Service Incentive Leave Pay

                case "Service Incentive Leave Pay":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLeavePayConversion))
                        {
                            form.Activate();
                            return;
                        }
                    }


                    frmLeavePayConversion frmLeavePayConversion = new frmLeavePayConversion();
                    frmLeavePayConversion.MdiParent = this;
                    frmLeavePayConversion.Show();

                    //clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Service Incentive Leave Pay - Branches.rpt";
                    //frmLeaveGenParameter = new frmLeaveGenParameter();
                    //frmLeaveGenParameter._RequestType = "1";
                    //frmLeaveGenParameter.MdiParent = this;
                    //frmLeaveGenParameter.Show();


                    //clsDeclaration.sReportTag = 2;
                    //clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Service Incentive Leave Pay.rpt";
                    //frmReport.MdiParent = this;
                    //frmReport.Show();

                    break;
                #endregion

                #region SILP Slip
                case "SILP Slip":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSILPrParameter))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSILPrParameter = new frmSILPrParameter();
                    clsDeclaration.sReportID = 1;
                    frmSILPrParameter.MdiParent = this;
                    frmSILPrParameter.Show();

                    break;

                #endregion
                #region One Time Deduction Monitoring Report
                case "One Time Deduction Monitoring Report":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmEmployeeFilterParameter))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmEmpParameter = new frmEmpParameter();
                    clsDeclaration.sReportID = 9;
                    frmEmpParameter.MdiParent = this;
                    frmEmpParameter.Show();

                    break;
                    #endregion
                #region Over Deduction List
                case "Over Deduction List":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportDisplay))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 0;

                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Over Deduction List.rpt";
                    frmReportDisplay frmOverDeduct = new frmReportDisplay();
                    frmReportDisplay._RequestType = "OverDeduct";
                    frmOverDeduct.MdiParent = this;
                    frmOverDeduct.Show();

                    break;
                #endregion
                #region Adjustment
                case "Adjustment":
                    //if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    //{
                    //    MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                    //    return;
                    //}

                    //foreach (Form form in Application.OpenForms)
                    //{
                    //    if (form.GetType() == typeof(frmReportDisplay))
                    //    {
                    //        form.Activate();
                    //        return;
                    //    }
                    //}

                    ////clsDeclaration.sReportTag = 5;
                    //clsDeclaration.sReportTag = 20;

                    //frmReportDisplay frmAdjustment = new frmReportDisplay();
                    //frmReportDisplay._RequestType = "Adjustment";
                    //frmAdjustment.MdiParent = this;
                    //frmAdjustment.Show();


                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmPayrollRegParameter))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 20;
                    frmPayrollRegParameter frmPayrollRegParameter1 = new frmPayrollRegParameter();
                    frmPayrollRegParameter._RequestType = "Adjustment";
                    frmPayrollRegParameter1.MdiParent = this;
                    frmPayrollRegParameter1.Show();

                    break;
                #endregion
                #region Payroll Register
                case "Payroll Register":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    //clsDeclaration.sReportTag = 5;
                    clsDeclaration.sReportTag = 7;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Payroll Register V2.rpt";

                    frmReportViewer frmReportViewer5 = new frmReportViewer();
                    frmReportViewer5.MdiParent = this;
                    frmReportViewer5.Show();

                    break;
                #endregion
                #region Employee Absences / UnderTime

                case "Employee Absences / UnderTime":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 1;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Employee Absences UnderTime.rpt";

                    frmReportViewer frmReportViewer1 = new frmReportViewer();
                    frmReportViewer1.MdiParent = this;
                    frmReportViewer1.Show();

                    break;
                #endregion
                #region Incomplete In and Out
                case "Incomplete In and Out":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmReportViewer))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    clsDeclaration.sReportTag = 1;
                    clsDeclaration.sReportPath = Application.StartupPath + @"\Reports\Incomplete Time In And Out.rpt";

                    frmReportViewer frmReportViewer2 = new frmReportViewer();
                    frmReportViewer2.MdiParent = this;
                    frmReportViewer2.Show();

                    break;
                #endregion
                #region Import Log Files

                case "Import Log Files":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmLogFiles))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmLogFiles frmLogFiles = new frmLogFiles();
                    frmLogFiles.MdiParent = this;
                    frmLogFiles.Show();

                    break;
                #endregion
                #region SILP Uploader
                case "SILP Uploader":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSILPUploading))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSILPUploading frmSILPUploading = new frmSILPUploading();
                    frmSILPUploading.MdiParent = this;
                    frmSILPUploading.Show();
                    break;
                #endregion
                #region Deduction Uploader
                case "Deduction Uploader":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmDeductionUploader))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmDeductionUploader frmDeductionUploader = new frmDeductionUploader();
                    frmDeductionUploader.MdiParent = this;
                    frmDeductionUploader.Show();
                    break;
                #endregion
                #region Working Hours
                case "Working Hours":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmWorkingHours))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmWorkingHours frmWorkingHours = new frmWorkingHours();
                    frmWorkingHours.MdiParent = this;
                    frmWorkingHours.Show();

                    break;
                #endregion
                #region Account Uploading
                case "Account Uploading":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmAccountUploading))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmAccountUploading frmAccountUploading = new frmAccountUploading();
                    frmAccountUploading.MdiParent = this;
                    frmAccountUploading.Show();

                    break;
                #endregion
                #region SAP Connection
                case "SAP Connection":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSystemConfig))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSystemConfig frmSystemConfig = new frmSystemConfig();
                    frmSystemConfig.MdiParent = this;
                    frmSystemConfig.Show();

                    break;
                #endregion
                #region Connection
                case "Connection":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmConnection))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmConnection frmConnection = new frmConnection();
                    frmConnection.MdiParent = this;
                    frmConnection.Show();

                    break;
                #endregion
                #region Company List
                case "Company List":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmCompanyList))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmCompanyList frmCompanyList = new frmCompanyList();
                    frmCompanyList.MdiParent = this;
                    frmCompanyList.Show();

                    break;
                #endregion
                #region Custom Payroll Setup
                case "Custom Payroll Setup":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmCustomPayrollSetup))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmCustomPayrollSetup frmCustomPayrollSetup = new frmCustomPayrollSetup();
                    frmCustomPayrollSetup.MdiParent = this;
                    frmCustomPayrollSetup.Show();

                    break;
                #endregion
                #region S.S.S.
                case "S.S.S.":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmSSSTable))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmSSSTable frmSSSTable = new frmSSSTable();
                    frmSSSTable.MdiParent = this;
                    frmSSSTable.Show();

                    break;
                #endregion
                #region Goverment Deduction Uploading
                case "Goverment Deduction Uploading":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmGovDeductionUploading))
                        {
                            form.Activate();
                            return;
                        }
                    }

                    frmGovDeductionUploading frmGovDeductionUploading = new frmGovDeductionUploading();
                    frmGovDeductionUploading.MdiParent = this;
                    frmGovDeductionUploading.Show();

                    break;
                #endregion
                #region Overtime Approval
                case "Overtime Approval":
                    if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, node.Text) == "No Access")
                    {
                        MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Adminitrator.");
                        return;
                    }

                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(frmOvertimeApproval))
                        {
                            form.Activate();
                            return;
                        }
                    }


                    frmOvertimeApproval frmOvertimeApproval = new frmOvertimeApproval();
                    frmOvertimeApproval.MdiParent = this;
                    frmOvertimeApproval.Show();

                    break;
                #endregion

                default:
                    Console.WriteLine("Opps! Still On Development! See in Next Vesion");
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
