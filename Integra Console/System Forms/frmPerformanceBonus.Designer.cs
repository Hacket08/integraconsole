partial class frmPerformanceBonus
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
            this.label7 = new System.Windows.Forms.Label();
            this.chkManagerial = new System.Windows.Forms.CheckBox();
            this.chkSupervisory = new System.Windows.Forms.CheckBox();
            this.chkRankAndFile = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tssDataStatus = new System.Windows.Forms.Label();
            this.pbDataProgress = new System.Windows.Forms.ProgressBar();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmpNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.updatePayrollTimeRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openInManualPayrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDateFrom = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDateTo = new System.Windows.Forms.TextBox();
            this.lblPreviewReport = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.DarkGreen;
            this.label7.Location = new System.Drawing.Point(-17, -9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1012, 13);
            this.label7.TabIndex = 87;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkManagerial);
            this.groupBox1.Controls.Add(this.chkSupervisory);
            this.groupBox1.Controls.Add(this.chkRankAndFile);
            this.groupBox1.Location = new System.Drawing.Point(488, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 91);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Confidentiality Level";
            // 
            // tssDataStatus
            // 
            this.tssDataStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tssDataStatus.AutoSize = true;
            this.tssDataStatus.Location = new System.Drawing.Point(9, 481);
            this.tssDataStatus.Name = "tssDataStatus";
            this.tssDataStatus.Size = new System.Drawing.Size(39, 13);
            this.tssDataStatus.TabIndex = 83;
            this.tssDataStatus.Text = "Status";
            // 
            // pbDataProgress
            // 
            this.pbDataProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDataProgress.Location = new System.Drawing.Point(12, 497);
            this.pbDataProgress.Name = "pbDataProgress";
            this.pbDataProgress.Size = new System.Drawing.Size(955, 17);
            this.pbDataProgress.TabIndex = 82;
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(101, 131);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(381, 21);
            this.cboDepartment.TabIndex = 81;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Department";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(278, 164);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(351, 22);
            this.txtName.TabIndex = 79;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 78;
            this.label5.Text = "Name";
            // 
            // txtEmpNo
            // 
            this.txtEmpNo.Location = new System.Drawing.Point(92, 164);
            this.txtEmpNo.Name = "txtEmpNo";
            this.txtEmpNo.Size = new System.Drawing.Size(138, 22);
            this.txtEmpNo.TabIndex = 77;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 76;
            this.label4.Text = "Employee No.";
            // 
            // updatePayrollTimeRecordToolStripMenuItem
            // 
            this.updatePayrollTimeRecordToolStripMenuItem.Name = "updatePayrollTimeRecordToolStripMenuItem";
            this.updatePayrollTimeRecordToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.updatePayrollTimeRecordToolStripMenuItem.Text = "Update Payroll Time Record";
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(101, 104);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(381, 21);
            this.cboBranch.TabIndex = 74;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInManualPayrollToolStripMenuItem,
            this.updatePayrollTimeRecordToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(222, 48);
            // 
            // openInManualPayrollToolStripMenuItem
            // 
            this.openInManualPayrollToolStripMenuItem.Name = "openInManualPayrollToolStripMenuItem";
            this.openInManualPayrollToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.openInManualPayrollToolStripMenuItem.Text = "Open in Manual Payroll";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 73;
            this.label3.Text = "Branch";
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(101, 77);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(381, 21);
            this.cboArea.TabIndex = 72;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Area";
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.ContextMenuStrip = this.cmsMenu;
            this.dgvDisplay.Location = new System.Drawing.Point(12, 221);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(955, 240);
            this.dgvDisplay.TabIndex = 64;
            this.dgvDisplay.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellEndEdit);
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(101, 12);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(138, 21);
            this.cboYear.TabIndex = 89;
            this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "Year";
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.Location = new System.Drawing.Point(101, 39);
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.ReadOnly = true;
            this.txtDateFrom.Size = new System.Drawing.Size(138, 22);
            this.txtDateFrom.TabIndex = 91;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 90;
            this.label8.Text = "Date Covered";
            // 
            // txtDateTo
            // 
            this.txtDateTo.Location = new System.Drawing.Point(245, 39);
            this.txtDateTo.Name = "txtDateTo";
            this.txtDateTo.ReadOnly = true;
            this.txtDateTo.Size = new System.Drawing.Size(138, 22);
            this.txtDateTo.TabIndex = 92;
            // 
            // lblPreviewReport
            // 
            this.lblPreviewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewReport.AutoSize = true;
            this.lblPreviewReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPreviewReport.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.lblPreviewReport.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblPreviewReport.Location = new System.Drawing.Point(882, 15);
            this.lblPreviewReport.Name = "lblPreviewReport";
            this.lblPreviewReport.Size = new System.Drawing.Size(85, 13);
            this.lblPreviewReport.TabIndex = 95;
            this.lblPreviewReport.Text = "Preview Report";
            this.lblPreviewReport.Visible = false;
            this.lblPreviewReport.Click += new System.EventHandler(this.lblPreviewReport_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Image = global::Integra_Console.Properties.Resources.Synchronize;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(12, 192);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(152, 23);
            this.btnGenerate.TabIndex = 86;
            this.btnGenerate.Text = "Display Data";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(861, 467);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.TabIndex = 70;
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
            this.btnUpload.Location = new System.Drawing.Point(749, 467);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(106, 25);
            this.btnUpload.TabIndex = 69;
            this.btnUpload.Text = "Save";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Visible = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // frmPerformanceBonus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 520);
            this.Controls.Add(this.lblPreviewReport);
            this.Controls.Add(this.txtDateTo);
            this.Controls.Add(this.txtDateFrom);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tssDataStatus);
            this.Controls.Add(this.pbDataProgress);
            this.Controls.Add(this.cboDepartment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmpNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboBranch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboArea);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.dgvDisplay);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPerformanceBonus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Performace Bonus Processing";
            this.Load += new System.EventHandler(this.frmPerformanceBonus_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.CheckBox chkManagerial;
    private System.Windows.Forms.CheckBox chkSupervisory;
    private System.Windows.Forms.CheckBox chkRankAndFile;
    private System.Windows.Forms.Button btnGenerate;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label tssDataStatus;
    private System.Windows.Forms.ProgressBar pbDataProgress;
    private System.Windows.Forms.ComboBox cboDepartment;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtEmpNo;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ToolStripMenuItem updatePayrollTimeRecordToolStripMenuItem;
    private System.Windows.Forms.ComboBox cboBranch;
    private System.Windows.Forms.ContextMenuStrip cmsMenu;
    private System.Windows.Forms.ToolStripMenuItem openInManualPayrollToolStripMenuItem;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cboArea;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.DataGridView dgvDisplay;
    private System.Windows.Forms.ComboBox cboYear;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtDateFrom;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtDateTo;
    private System.Windows.Forms.Label lblPreviewReport;
}