partial class frmLastPayProcessing
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
            this.components = new System.ComponentModel.Container();
            this.tssDataStatus = new System.Windows.Forms.Label();
            this.pbDataProgress = new System.Windows.Forms.ProgressBar();
            this.cboPosition = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmpNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.openInManualPayrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updatePayrollTimeRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.btnReProcess = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkManagerial = new System.Windows.Forms.CheckBox();
            this.chkSupervisory = new System.Windows.Forms.CheckBox();
            this.chkRankAndFile = new System.Windows.Forms.CheckBox();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tssDataStatus
            // 
            this.tssDataStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tssDataStatus.AutoSize = true;
            this.tssDataStatus.Location = new System.Drawing.Point(10, 421);
            this.tssDataStatus.Name = "tssDataStatus";
            this.tssDataStatus.Size = new System.Drawing.Size(39, 13);
            this.tssDataStatus.TabIndex = 81;
            this.tssDataStatus.Text = "Status";
            // 
            // pbDataProgress
            // 
            this.pbDataProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDataProgress.Location = new System.Drawing.Point(13, 437);
            this.pbDataProgress.Name = "pbDataProgress";
            this.pbDataProgress.Size = new System.Drawing.Size(955, 17);
            this.pbDataProgress.TabIndex = 80;
            // 
            // cboPosition
            // 
            this.cboPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPosition.FormattingEnabled = true;
            this.cboPosition.Location = new System.Drawing.Point(102, 86);
            this.cboPosition.Name = "cboPosition";
            this.cboPosition.Size = new System.Drawing.Size(381, 21);
            this.cboPosition.TabIndex = 79;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 78;
            this.label6.Text = "Department";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(279, 127);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(351, 22);
            this.txtName.TabIndex = 77;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(237, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "Name";
            // 
            // txtEmpNo
            // 
            this.txtEmpNo.Location = new System.Drawing.Point(93, 127);
            this.txtEmpNo.Name = "txtEmpNo";
            this.txtEmpNo.Size = new System.Drawing.Size(138, 22);
            this.txtEmpNo.TabIndex = 75;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 74;
            this.label4.Text = "Employee No.";
            // 
            // openInManualPayrollToolStripMenuItem
            // 
            this.openInManualPayrollToolStripMenuItem.Name = "openInManualPayrollToolStripMenuItem";
            this.openInManualPayrollToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.openInManualPayrollToolStripMenuItem.Text = "Open in Manual Payroll";
            this.openInManualPayrollToolStripMenuItem.Click += new System.EventHandler(this.openInManualPayrollToolStripMenuItem_Click);
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInManualPayrollToolStripMenuItem,
            this.updatePayrollTimeRecordToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(222, 48);
            // 
            // updatePayrollTimeRecordToolStripMenuItem
            // 
            this.updatePayrollTimeRecordToolStripMenuItem.Name = "updatePayrollTimeRecordToolStripMenuItem";
            this.updatePayrollTimeRecordToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.updatePayrollTimeRecordToolStripMenuItem.Text = "Update Payroll Time Record";
            this.updatePayrollTimeRecordToolStripMenuItem.Click += new System.EventHandler(this.updatePayrollTimeRecordToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Branch";
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(102, 32);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(381, 21);
            this.cboArea.TabIndex = 70;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Area";
            // 
            // cboPayrolPeriod
            // 
            this.cboPayrolPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayrolPeriod.FormattingEnabled = true;
            this.cboPayrolPeriod.Location = new System.Drawing.Point(102, 5);
            this.cboPayrolPeriod.Name = "cboPayrolPeriod";
            this.cboPayrolPeriod.Size = new System.Drawing.Size(381, 21);
            this.cboPayrolPeriod.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Payroll Period";
            // 
            // cboCompany
            // 
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(784, 127);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(183, 21);
            this.cboCompany.TabIndex = 64;
            this.cboCompany.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(692, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 63;
            this.label8.Text = "Company";
            this.label8.Visible = false;
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.ContextMenuStrip = this.cmsMenu;
            this.dgvDisplay.Location = new System.Drawing.Point(13, 184);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(955, 219);
            this.dgvDisplay.TabIndex = 62;
            this.dgvDisplay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellClick);
            this.dgvDisplay.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDisplay_CellMouseDown);
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(102, 59);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(381, 21);
            this.cboBranch.TabIndex = 72;
            // 
            // btnReProcess
            // 
            this.btnReProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReProcess.Image = global::Integra_Console.Properties.Resources.Synchronize;
            this.btnReProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReProcess.Location = new System.Drawing.Point(826, 155);
            this.btnReProcess.Name = "btnReProcess";
            this.btnReProcess.Size = new System.Drawing.Size(141, 23);
            this.btnReProcess.TabIndex = 82;
            this.btnReProcess.Text = "Re-Process";
            this.btnReProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReProcess.UseVisualStyleBackColor = true;
            this.btnReProcess.Visible = false;
            this.btnReProcess.Click += new System.EventHandler(this.btnReProcess_Click);
            // 
            // button1
            // 
            this.button1.Image = global::Integra_Console.Properties.Resources.Synchronize;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(13, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 73;
            this.button1.Text = "Generate";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(862, 407);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.TabIndex = 68;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpload.Image = global::Integra_Console.Properties.Resources.Database_03;
            this.btnUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpload.Location = new System.Drawing.Point(750, 407);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(106, 25);
            this.btnUpload.TabIndex = 67;
            this.btnUpload.Text = "Upload";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkManagerial);
            this.groupBox1.Controls.Add(this.chkSupervisory);
            this.groupBox1.Controls.Add(this.chkRankAndFile);
            this.groupBox1.Location = new System.Drawing.Point(489, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 100);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Confidentiality Level";
            // 
            // chkManagerial
            // 
            this.chkManagerial.AutoSize = true;
            this.chkManagerial.Location = new System.Drawing.Point(11, 69);
            this.chkManagerial.Name = "chkManagerial";
            this.chkManagerial.Size = new System.Drawing.Size(84, 17);
            this.chkManagerial.TabIndex = 2;
            this.chkManagerial.Text = "Managerial";
            this.chkManagerial.UseVisualStyleBackColor = true;
            // 
            // chkSupervisory
            // 
            this.chkSupervisory.AutoSize = true;
            this.chkSupervisory.Location = new System.Drawing.Point(11, 46);
            this.chkSupervisory.Name = "chkSupervisory";
            this.chkSupervisory.Size = new System.Drawing.Size(85, 17);
            this.chkSupervisory.TabIndex = 1;
            this.chkSupervisory.Text = "Supervisory";
            this.chkSupervisory.UseVisualStyleBackColor = true;
            // 
            // chkRankAndFile
            // 
            this.chkRankAndFile.AutoSize = true;
            this.chkRankAndFile.Location = new System.Drawing.Point(11, 23);
            this.chkRankAndFile.Name = "chkRankAndFile";
            this.chkRankAndFile.Size = new System.Drawing.Size(97, 17);
            this.chkRankAndFile.TabIndex = 0;
            this.chkRankAndFile.Text = "Rank And File";
            this.chkRankAndFile.UseVisualStyleBackColor = true;
            // 
            // frmLastPayProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 458);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReProcess);
            this.Controls.Add(this.tssDataStatus);
            this.Controls.Add(this.pbDataProgress);
            this.Controls.Add(this.cboPosition);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmpNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboArea);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.cboPayrolPeriod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboCompany);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgvDisplay);
            this.Controls.Add(this.cboBranch);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmLastPayProcessing";
            this.Text = "frmLastPayProcessing";
            this.Load += new System.EventHandler(this.frmLastPayProcessing_Load);
            this.cmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button btnReProcess;
    private System.Windows.Forms.Label tssDataStatus;
    private System.Windows.Forms.ProgressBar pbDataProgress;
    private System.Windows.Forms.ComboBox cboPosition;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtEmpNo;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ToolStripMenuItem openInManualPayrollToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip cmsMenu;
    private System.Windows.Forms.ToolStripMenuItem updatePayrollTimeRecordToolStripMenuItem;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cboArea;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cboCompany;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.DataGridView dgvDisplay;
    private System.Windows.Forms.ComboBox cboBranch;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkManagerial;
    private System.Windows.Forms.CheckBox chkSupervisory;
    private System.Windows.Forms.CheckBox chkRankAndFile;
}