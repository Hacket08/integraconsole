partial class frmMainMenu
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainMenu));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsEmployeeData = new System.Windows.Forms.ToolStripButton();
            this.tsTimekeeping = new System.Windows.Forms.ToolStripButton();
            this.tsMainMenuSetup = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsPayroll = new System.Windows.Forms.ToolStripButton();
            this.tsTableConfig = new System.Windows.Forms.ToolStripButton();
            this.tsReports = new System.Windows.Forms.ToolStripButton();
            this.tsSystemConfig = new System.Windows.Forms.ToolStripButton();
            this.pnlEmployeeData = new System.Windows.Forms.Panel();
            this.btnLeaveGeneration = new System.Windows.Forms.Button();
            this.btnLoanFile = new System.Windows.Forms.Button();
            this.btnEmployeeMasterData = new System.Windows.Forms.Button();
            this.pnlPayroll = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pnlTableConfig = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTimekeeping = new System.Windows.Forms.Panel();
            this.btnUploadWorkDays = new System.Windows.Forms.Button();
            this.pnlReports = new System.Windows.Forms.Panel();
            this.pnlRPayroll = new System.Windows.Forms.Panel();
            this.button10 = new System.Windows.Forms.Button();
            this.pnlRTimekeeping = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.pnlRLoanReport = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.pnlRMasterData = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsRMasterData = new System.Windows.Forms.ToolStripButton();
            this.tsRLoanReport = new System.Windows.Forms.ToolStripButton();
            this.tsRTimekeeping = new System.Windows.Forms.ToolStripButton();
            this.tsRPayroll = new System.Windows.Forms.ToolStripButton();
            this.pnlSystemConfig = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button11 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button12 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button13 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button14 = new System.Windows.Forms.Button();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.pnlEmployeeData.SuspendLayout();
            this.pnlPayroll.SuspendLayout();
            this.pnlTableConfig.SuspendLayout();
            this.pnlTimekeeping.SuspendLayout();
            this.pnlReports.SuspendLayout();
            this.pnlRPayroll.SuspendLayout();
            this.pnlRTimekeeping.SuspendLayout();
            this.pnlRLoanReport.SuspendLayout();
            this.pnlRMasterData.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.pnlSystemConfig.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEmployeeData,
            this.tsTimekeeping,
            this.tsMainMenuSetup,
            this.tsPayroll,
            this.tsTableConfig,
            this.tsReports,
            this.tsSystemConfig});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(884, 109);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // tsEmployeeData
            // 
            this.tsEmployeeData.AutoSize = false;
            this.tsEmployeeData.Image = global::Integra_Console.Properties.Resources._64_Employee_Master_Data_2;
            this.tsEmployeeData.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsEmployeeData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEmployeeData.Name = "tsEmployeeData";
            this.tsEmployeeData.Size = new System.Drawing.Size(106, 106);
            this.tsEmployeeData.Text = "Employee Data";
            this.tsEmployeeData.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsEmployeeData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsEmployeeData.Click += new System.EventHandler(this.tsEmployeeData_Click);
            // 
            // tsTimekeeping
            // 
            this.tsTimekeeping.AutoSize = false;
            this.tsTimekeeping.Image = global::Integra_Console.Properties.Resources._64_TimeKeeping;
            this.tsTimekeeping.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsTimekeeping.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsTimekeeping.Name = "tsTimekeeping";
            this.tsTimekeeping.Size = new System.Drawing.Size(106, 106);
            this.tsTimekeeping.Text = "Timekeeping";
            this.tsTimekeeping.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsTimekeeping.Click += new System.EventHandler(this.tsTimekeeping_Click);
            // 
            // tsMainMenuSetup
            // 
            this.tsMainMenuSetup.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsMainMenuSetup.AutoSize = false;
            this.tsMainMenuSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsMainMenuSetup.Image = global::Integra_Console.Properties.Resources._64_Config;
            this.tsMainMenuSetup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsMainMenuSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMainMenuSetup.Name = "tsMainMenuSetup";
            this.tsMainMenuSetup.Size = new System.Drawing.Size(90, 106);
            this.tsMainMenuSetup.Text = "Config";
            // 
            // tsPayroll
            // 
            this.tsPayroll.AutoSize = false;
            this.tsPayroll.Image = global::Integra_Console.Properties.Resources._64_Payroll;
            this.tsPayroll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsPayroll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPayroll.Name = "tsPayroll";
            this.tsPayroll.Size = new System.Drawing.Size(106, 106);
            this.tsPayroll.Text = "Payroll";
            this.tsPayroll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPayroll.Click += new System.EventHandler(this.tsPayroll_Click);
            // 
            // tsTableConfig
            // 
            this.tsTableConfig.AutoSize = false;
            this.tsTableConfig.Image = global::Integra_Console.Properties.Resources._64_Table_Config;
            this.tsTableConfig.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsTableConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsTableConfig.Name = "tsTableConfig";
            this.tsTableConfig.Size = new System.Drawing.Size(106, 106);
            this.tsTableConfig.Text = "Table Config";
            this.tsTableConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsTableConfig.Click += new System.EventHandler(this.tsTableConfig_Click);
            // 
            // tsReports
            // 
            this.tsReports.AutoSize = false;
            this.tsReports.Image = global::Integra_Console.Properties.Resources._64_Reports;
            this.tsReports.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsReports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsReports.Name = "tsReports";
            this.tsReports.Size = new System.Drawing.Size(106, 106);
            this.tsReports.Text = "Reports";
            this.tsReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsReports.Click += new System.EventHandler(this.tsReports_Click);
            // 
            // tsSystemConfig
            // 
            this.tsSystemConfig.AutoSize = false;
            this.tsSystemConfig.Image = global::Integra_Console.Properties.Resources._64_Window_Config;
            this.tsSystemConfig.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsSystemConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSystemConfig.Name = "tsSystemConfig";
            this.tsSystemConfig.Size = new System.Drawing.Size(106, 106);
            this.tsSystemConfig.Text = "System";
            this.tsSystemConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSystemConfig.Click += new System.EventHandler(this.tsSystemConfig_Click);
            // 
            // pnlEmployeeData
            // 
            this.pnlEmployeeData.Controls.Add(this.btnLeaveGeneration);
            this.pnlEmployeeData.Controls.Add(this.btnLoanFile);
            this.pnlEmployeeData.Controls.Add(this.btnEmployeeMasterData);
            this.pnlEmployeeData.Location = new System.Drawing.Point(474, 127);
            this.pnlEmployeeData.Name = "pnlEmployeeData";
            this.pnlEmployeeData.Size = new System.Drawing.Size(166, 102);
            this.pnlEmployeeData.TabIndex = 1;
            // 
            // btnLeaveGeneration
            // 
            this.btnLeaveGeneration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLeaveGeneration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLeaveGeneration.Image = global::Integra_Console.Properties.Resources._64_Leave;
            this.btnLeaveGeneration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLeaveGeneration.Location = new System.Drawing.Point(283, 3);
            this.btnLeaveGeneration.Name = "btnLeaveGeneration";
            this.btnLeaveGeneration.Size = new System.Drawing.Size(137, 73);
            this.btnLeaveGeneration.TabIndex = 5;
            this.btnLeaveGeneration.Text = "Leave\r\nGeneration";
            this.btnLeaveGeneration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLeaveGeneration.UseVisualStyleBackColor = true;
            this.btnLeaveGeneration.Click += new System.EventHandler(this.btnLeaveGeneration_Click);
            // 
            // btnLoanFile
            // 
            this.btnLoanFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLoanFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoanFile.Image = global::Integra_Console.Properties.Resources._64_Loan_Data_2;
            this.btnLoanFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoanFile.Location = new System.Drawing.Point(143, 3);
            this.btnLoanFile.Name = "btnLoanFile";
            this.btnLoanFile.Size = new System.Drawing.Size(137, 73);
            this.btnLoanFile.TabIndex = 4;
            this.btnLoanFile.Text = "Loan File";
            this.btnLoanFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoanFile.UseVisualStyleBackColor = true;
            this.btnLoanFile.Click += new System.EventHandler(this.btnLoanFile_Click);
            // 
            // btnEmployeeMasterData
            // 
            this.btnEmployeeMasterData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEmployeeMasterData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmployeeMasterData.Image = global::Integra_Console.Properties.Resources._64_Master_Data;
            this.btnEmployeeMasterData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmployeeMasterData.Location = new System.Drawing.Point(3, 3);
            this.btnEmployeeMasterData.Name = "btnEmployeeMasterData";
            this.btnEmployeeMasterData.Size = new System.Drawing.Size(137, 73);
            this.btnEmployeeMasterData.TabIndex = 3;
            this.btnEmployeeMasterData.Text = "Employee\r\nMaster\r\nData";
            this.btnEmployeeMasterData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEmployeeMasterData.UseVisualStyleBackColor = true;
            this.btnEmployeeMasterData.Click += new System.EventHandler(this.btnEmployeeMasterData_Click);
            // 
            // pnlPayroll
            // 
            this.pnlPayroll.Controls.Add(this.button6);
            this.pnlPayroll.Controls.Add(this.button4);
            this.pnlPayroll.Controls.Add(this.button3);
            this.pnlPayroll.Controls.Add(this.button1);
            this.pnlPayroll.Controls.Add(this.button2);
            this.pnlPayroll.Location = new System.Drawing.Point(12, 112);
            this.pnlPayroll.Name = "pnlPayroll";
            this.pnlPayroll.Size = new System.Drawing.Size(437, 117);
            this.pnlPayroll.TabIndex = 2;
            this.pnlPayroll.Visible = false;
            // 
            // button6
            // 
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Image = global::Integra_Console.Properties.Resources._64_Entry_Per_Employee;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(3, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(155, 73);
            this.button6.TabIndex = 4;
            this.button6.Text = "Payroll\r\nAccount Entry\r\nBy Employee";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Image = global::Integra_Console.Properties.Resources._64_Entry_Per_Account;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(161, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(155, 73);
            this.button4.TabIndex = 3;
            this.button4.Text = "Payroll\r\nAccount Entry\r\nBy Account";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Image = global::Integra_Console.Properties.Resources._64_Lock_Payroll;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(143, 77);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 73);
            this.button3.TabIndex = 2;
            this.button3.Text = "Lock\r\nPayroll";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Image = global::Integra_Console.Properties.Resources._64_Payroll_Process_3;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(319, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 73);
            this.button1.TabIndex = 0;
            this.button1.Text = "Payroll\r\nProcessing";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Image = global::Integra_Console.Properties.Resources._64_Manual_Payroll;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(3, 77);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 73);
            this.button2.TabIndex = 1;
            this.button2.Text = "Manual\r\nPayroll\r\nProcessing";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pnlTableConfig
            // 
            this.pnlTableConfig.Controls.Add(this.button5);
            this.pnlTableConfig.Location = new System.Drawing.Point(662, 157);
            this.pnlTableConfig.Name = "pnlTableConfig";
            this.pnlTableConfig.Size = new System.Drawing.Size(534, 146);
            this.pnlTableConfig.TabIndex = 2;
            this.pnlTableConfig.Visible = false;
            // 
            // button5
            // 
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Image = global::Integra_Console.Properties.Resources._64_Manual_Payroll;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(3, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(137, 73);
            this.button5.TabIndex = 3;
            this.button5.Text = "Manual\r\nPayroll\r\nProcessing";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(-39, -9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(931, 13);
            this.label1.TabIndex = 3;
            // 
            // pnlTimekeeping
            // 
            this.pnlTimekeeping.Controls.Add(this.btnUploadWorkDays);
            this.pnlTimekeeping.Location = new System.Drawing.Point(689, 112);
            this.pnlTimekeeping.Name = "pnlTimekeeping";
            this.pnlTimekeeping.Size = new System.Drawing.Size(165, 133);
            this.pnlTimekeeping.TabIndex = 2;
            this.pnlTimekeeping.Visible = false;
            // 
            // btnUploadWorkDays
            // 
            this.btnUploadWorkDays.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUploadWorkDays.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadWorkDays.Image = global::Integra_Console.Properties.Resources._64_Upload_Work_Days_2;
            this.btnUploadWorkDays.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUploadWorkDays.Location = new System.Drawing.Point(3, 3);
            this.btnUploadWorkDays.Name = "btnUploadWorkDays";
            this.btnUploadWorkDays.Size = new System.Drawing.Size(137, 73);
            this.btnUploadWorkDays.TabIndex = 3;
            this.btnUploadWorkDays.Text = "Upload\r\nWork Days";
            this.btnUploadWorkDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUploadWorkDays.UseVisualStyleBackColor = true;
            this.btnUploadWorkDays.Click += new System.EventHandler(this.btnUploadWorkDays_Click);
            // 
            // pnlReports
            // 
            this.pnlReports.Controls.Add(this.pnlRPayroll);
            this.pnlReports.Controls.Add(this.pnlRTimekeeping);
            this.pnlReports.Controls.Add(this.pnlRLoanReport);
            this.pnlReports.Controls.Add(this.pnlRMasterData);
            this.pnlReports.Controls.Add(this.toolStrip2);
            this.pnlReports.Location = new System.Drawing.Point(12, 235);
            this.pnlReports.Name = "pnlReports";
            this.pnlReports.Size = new System.Drawing.Size(204, 157);
            this.pnlReports.TabIndex = 4;
            this.pnlReports.Visible = false;
            // 
            // pnlRPayroll
            // 
            this.pnlRPayroll.Controls.Add(this.button10);
            this.pnlRPayroll.Location = new System.Drawing.Point(465, 28);
            this.pnlRPayroll.Name = "pnlRPayroll";
            this.pnlRPayroll.Size = new System.Drawing.Size(145, 115);
            this.pnlRPayroll.TabIndex = 6;
            // 
            // button10
            // 
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button10.Image = global::Integra_Console.Properties.Resources._64_PrintDoc;
            this.button10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button10.Location = new System.Drawing.Point(3, 3);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(86, 102);
            this.button10.TabIndex = 3;
            this.button10.Text = "Payroll\r\nRegister";
            this.button10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button10.UseVisualStyleBackColor = true;
            // 
            // pnlRTimekeeping
            // 
            this.pnlRTimekeeping.Controls.Add(this.button9);
            this.pnlRTimekeeping.Location = new System.Drawing.Point(314, 28);
            this.pnlRTimekeeping.Name = "pnlRTimekeeping";
            this.pnlRTimekeeping.Size = new System.Drawing.Size(145, 115);
            this.pnlRTimekeeping.TabIndex = 6;
            // 
            // button9
            // 
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button9.Image = global::Integra_Console.Properties.Resources._64_PrintDoc;
            this.button9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button9.Location = new System.Drawing.Point(3, 3);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(86, 102);
            this.button9.TabIndex = 3;
            this.button9.Text = "Work Days\r\n";
            this.button9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button9.UseVisualStyleBackColor = true;
            // 
            // pnlRLoanReport
            // 
            this.pnlRLoanReport.Controls.Add(this.button8);
            this.pnlRLoanReport.Location = new System.Drawing.Point(163, 28);
            this.pnlRLoanReport.Name = "pnlRLoanReport";
            this.pnlRLoanReport.Size = new System.Drawing.Size(148, 115);
            this.pnlRLoanReport.TabIndex = 6;
            // 
            // button8
            // 
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button8.Image = global::Integra_Console.Properties.Resources._64_PrintDoc;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button8.Location = new System.Drawing.Point(3, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(86, 102);
            this.button8.TabIndex = 3;
            this.button8.Text = "Loan\r\nList";
            this.button8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // pnlRMasterData
            // 
            this.pnlRMasterData.Controls.Add(this.button7);
            this.pnlRMasterData.Location = new System.Drawing.Point(12, 28);
            this.pnlRMasterData.Name = "pnlRMasterData";
            this.pnlRMasterData.Size = new System.Drawing.Size(148, 115);
            this.pnlRMasterData.TabIndex = 5;
            // 
            // button7
            // 
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button7.Image = global::Integra_Console.Properties.Resources._64_PrintDoc;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button7.Location = new System.Drawing.Point(3, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(86, 102);
            this.button7.TabIndex = 3;
            this.button7.Text = "Employee\r\nList";
            this.button7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button7.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRMasterData,
            this.tsRLoanReport,
            this.tsRTimekeeping,
            this.tsRPayroll});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(204, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsRMasterData
            // 
            this.tsRMasterData.Image = ((System.Drawing.Image)(resources.GetObject("tsRMasterData.Image")));
            this.tsRMasterData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRMasterData.Name = "tsRMasterData";
            this.tsRMasterData.Size = new System.Drawing.Size(90, 22);
            this.tsRMasterData.Text = "Master Data";
            this.tsRMasterData.Click += new System.EventHandler(this.tsRMasterData_Click);
            // 
            // tsRLoanReport
            // 
            this.tsRLoanReport.Image = ((System.Drawing.Image)(resources.GetObject("tsRLoanReport.Image")));
            this.tsRLoanReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRLoanReport.Name = "tsRLoanReport";
            this.tsRLoanReport.Size = new System.Drawing.Size(96, 22);
            this.tsRLoanReport.Text = "Loan Reports";
            this.tsRLoanReport.Click += new System.EventHandler(this.tsRLoanReport_Click);
            // 
            // tsRTimekeeping
            // 
            this.tsRTimekeeping.Image = ((System.Drawing.Image)(resources.GetObject("tsRTimekeeping.Image")));
            this.tsRTimekeeping.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRTimekeeping.Name = "tsRTimekeeping";
            this.tsRTimekeeping.Size = new System.Drawing.Size(96, 20);
            this.tsRTimekeeping.Text = "Timekeeping";
            this.tsRTimekeeping.Click += new System.EventHandler(this.tsRTimekeeping_Click);
            // 
            // tsRPayroll
            // 
            this.tsRPayroll.Image = ((System.Drawing.Image)(resources.GetObject("tsRPayroll.Image")));
            this.tsRPayroll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRPayroll.Name = "tsRPayroll";
            this.tsRPayroll.Size = new System.Drawing.Size(63, 20);
            this.tsRPayroll.Text = "Payroll";
            this.tsRPayroll.Click += new System.EventHandler(this.tsRPayroll_Click);
            // 
            // pnlSystemConfig
            // 
            this.pnlSystemConfig.Controls.Add(this.panel2);
            this.pnlSystemConfig.Controls.Add(this.panel3);
            this.pnlSystemConfig.Controls.Add(this.panel4);
            this.pnlSystemConfig.Controls.Add(this.panel5);
            this.pnlSystemConfig.Controls.Add(this.toolStrip3);
            this.pnlSystemConfig.Location = new System.Drawing.Point(266, 235);
            this.pnlSystemConfig.Name = "pnlSystemConfig";
            this.pnlSystemConfig.Size = new System.Drawing.Size(628, 176);
            this.pnlSystemConfig.TabIndex = 5;
            this.pnlSystemConfig.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button11);
            this.panel2.Location = new System.Drawing.Point(465, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(145, 115);
            this.panel2.TabIndex = 6;
            // 
            // button11
            // 
            this.button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button11.Image = global::Integra_Console.Properties.Resources._64_PrintDoc;
            this.button11.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button11.Location = new System.Drawing.Point(3, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(86, 102);
            this.button11.TabIndex = 3;
            this.button11.Text = "Payroll\r\nRegister";
            this.button11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button11.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button12);
            this.panel3.Location = new System.Drawing.Point(314, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(145, 115);
            this.panel3.TabIndex = 6;
            // 
            // button12
            // 
            this.button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button12.Image = global::Integra_Console.Properties.Resources._64_PrintDoc;
            this.button12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button12.Location = new System.Drawing.Point(3, 3);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(86, 102);
            this.button12.TabIndex = 3;
            this.button12.Text = "Work Days\r\n";
            this.button12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button12.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button13);
            this.panel4.Location = new System.Drawing.Point(163, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(148, 115);
            this.panel4.TabIndex = 6;
            // 
            // button13
            // 
            this.button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button13.Image = global::Integra_Console.Properties.Resources._64_PrintDoc;
            this.button13.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button13.Location = new System.Drawing.Point(3, 3);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(86, 102);
            this.button13.TabIndex = 3;
            this.button13.Text = "Loan\r\nList";
            this.button13.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button13.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button14);
            this.panel5.Location = new System.Drawing.Point(12, 28);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(148, 115);
            this.panel5.TabIndex = 5;
            // 
            // button14
            // 
            this.button14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button14.Image = global::Integra_Console.Properties.Resources._64_PrintDoc;
            this.button14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button14.Location = new System.Drawing.Point(3, 3);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(86, 102);
            this.button14.TabIndex = 3;
            this.button14.Text = "Employee\r\nList";
            this.button14.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button14.UseVisualStyleBackColor = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(628, 25);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(90, 22);
            this.toolStripButton1.Text = "Master Data";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton2.Text = "Loan Reports";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton3.Text = "Timekeeping";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(63, 22);
            this.toolStripButton4.Text = "Payroll";
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 423);
            this.ControlBox = false;
            this.Controls.Add(this.pnlSystemConfig);
            this.Controls.Add(this.pnlTableConfig);
            this.Controls.Add(this.pnlEmployeeData);
            this.Controls.Add(this.pnlReports);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlPayroll);
            this.Controls.Add(this.pnlTimekeeping);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlEmployeeData.ResumeLayout(false);
            this.pnlPayroll.ResumeLayout(false);
            this.pnlTableConfig.ResumeLayout(false);
            this.pnlTimekeeping.ResumeLayout(false);
            this.pnlReports.ResumeLayout(false);
            this.pnlReports.PerformLayout();
            this.pnlRPayroll.ResumeLayout(false);
            this.pnlRTimekeeping.ResumeLayout(false);
            this.pnlRLoanReport.ResumeLayout(false);
            this.pnlRMasterData.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.pnlSystemConfig.ResumeLayout(false);
            this.pnlSystemConfig.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton tsSystemConfig;
    private System.Windows.Forms.ToolStripDropDownButton tsMainMenuSetup;
    private System.Windows.Forms.ToolStripButton tsTableConfig;
    private System.Windows.Forms.ToolStripButton tsEmployeeData;
    private System.Windows.Forms.ToolStripButton tsTimekeeping;
    private System.Windows.Forms.ToolStripButton tsPayroll;
    private System.Windows.Forms.Panel pnlEmployeeData;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Panel pnlPayroll;
    private System.Windows.Forms.Panel pnlTableConfig;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button btnEmployeeMasterData;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Button btnLoanFile;
    private System.Windows.Forms.ToolStripButton tsReports;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button btnUploadWorkDays;
    private System.Windows.Forms.Panel pnlTimekeeping;
    private System.Windows.Forms.Panel pnlReports;
    private System.Windows.Forms.Button button7;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripButton tsRMasterData;
    private System.Windows.Forms.ToolStripButton tsRLoanReport;
    private System.Windows.Forms.ToolStripButton tsRTimekeeping;
    private System.Windows.Forms.ToolStripButton tsRPayroll;
    private System.Windows.Forms.Panel pnlRPayroll;
    private System.Windows.Forms.Button button10;
    private System.Windows.Forms.Panel pnlRTimekeeping;
    private System.Windows.Forms.Button button9;
    private System.Windows.Forms.Panel pnlRLoanReport;
    private System.Windows.Forms.Button button8;
    private System.Windows.Forms.Panel pnlRMasterData;
    private System.Windows.Forms.Button btnLeaveGeneration;
    private System.Windows.Forms.Panel pnlSystemConfig;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button button11;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Button button12;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Button button13;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Button button14;
    private System.Windows.Forms.ToolStrip toolStrip3;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
}