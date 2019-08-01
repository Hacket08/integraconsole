partial class frmPayrollEntries
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
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openInManualPayrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePayrollTimeRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbDataProgress = new System.Windows.Forms.ProgressBar();
            this.tssDataStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCheckingDisplay = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(101, 31);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(295, 21);
            this.cboBranch.TabIndex = 50;
            this.cboBranch.SelectedIndexChanged += new System.EventHandler(this.cboBranch_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Branch";
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(101, 4);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(295, 21);
            this.cboArea.TabIndex = 48;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Area";
            // 
            // cboPayrolPeriod
            // 
            this.cboPayrolPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayrolPeriod.FormattingEnabled = true;
            this.cboPayrolPeriod.Location = new System.Drawing.Point(101, 89);
            this.cboPayrolPeriod.Name = "cboPayrolPeriod";
            this.cboPayrolPeriod.Size = new System.Drawing.Size(295, 21);
            this.cboPayrolPeriod.TabIndex = 44;
            this.cboPayrolPeriod.SelectedIndexChanged += new System.EventHandler(this.cboPayrolPeriod_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Payroll Period";
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.ContextMenuStrip = this.cmsMenu;
            this.dgvDisplay.Location = new System.Drawing.Point(12, 156);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(955, 367);
            this.dgvDisplay.TabIndex = 38;
            this.dgvDisplay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellClick);
            this.dgvDisplay.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDisplay_CellMouseDown);
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
            this.openInManualPayrollToolStripMenuItem.Click += new System.EventHandler(this.openInManualPayrollToolStripMenuItem_Click);
            // 
            // updatePayrollTimeRecordToolStripMenuItem
            // 
            this.updatePayrollTimeRecordToolStripMenuItem.Name = "updatePayrollTimeRecordToolStripMenuItem";
            this.updatePayrollTimeRecordToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.updatePayrollTimeRecordToolStripMenuItem.Text = "Update Payroll Time Record";
            this.updatePayrollTimeRecordToolStripMenuItem.Click += new System.EventHandler(this.updatePayrollTimeRecordToolStripMenuItem_Click);
            // 
            // pbDataProgress
            // 
            this.pbDataProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDataProgress.Location = new System.Drawing.Point(12, 577);
            this.pbDataProgress.Name = "pbDataProgress";
            this.pbDataProgress.Size = new System.Drawing.Size(955, 17);
            this.pbDataProgress.TabIndex = 58;
            // 
            // tssDataStatus
            // 
            this.tssDataStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tssDataStatus.Location = new System.Drawing.Point(9, 561);
            this.tssDataStatus.Name = "tssDataStatus";
            this.tssDataStatus.Size = new System.Drawing.Size(734, 13);
            this.tssDataStatus.TabIndex = 59;
            this.tssDataStatus.Text = "Status";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(861, 547);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.TabIndex = 46;
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
            this.btnUpload.Location = new System.Drawing.Point(749, 547);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(106, 25);
            this.btnUpload.TabIndex = 45;
            this.btnUpload.Text = "Upload";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Image = global::Integra_Console.Properties.Resources.Synchronize;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(826, 127);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(141, 23);
            this.btnGenerate.TabIndex = 62;
            this.btnGenerate.Text = "Process Payroll";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.DarkGreen;
            this.label7.Location = new System.Drawing.Point(-17, -9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1012, 13);
            this.label7.TabIndex = 63;
            // 
            // btnCheckingDisplay
            // 
            this.btnCheckingDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckingDisplay.Image = global::Integra_Console.Properties.Resources.Synchronize;
            this.btnCheckingDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckingDisplay.Location = new System.Drawing.Point(796, 12);
            this.btnCheckingDisplay.Name = "btnCheckingDisplay";
            this.btnCheckingDisplay.Size = new System.Drawing.Size(171, 23);
            this.btnCheckingDisplay.TabIndex = 64;
            this.btnCheckingDisplay.Text = "Check Per Payroll Period";
            this.btnCheckingDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCheckingDisplay.UseVisualStyleBackColor = true;
            this.btnCheckingDisplay.Visible = false;
            this.btnCheckingDisplay.Click += new System.EventHandler(this.btnCheckingDisplay_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(402, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "MM/DD/YYYY";
            this.label4.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboYear);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cboBranch);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboArea);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboPayrolPeriod);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 118);
            this.panel1.TabIndex = 66;
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(101, 62);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(295, 21);
            this.cboYear.TabIndex = 67;
            this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 66;
            this.label5.Text = "Year";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAmount.Location = new System.Drawing.Point(638, 526);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(329, 18);
            this.lblTotalAmount.TabIndex = 67;
            this.lblTotalAmount.Text = "Total Amount : 0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Image = global::Integra_Console.Properties.Resources.Synchronize;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(12, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 23);
            this.button1.TabIndex = 68;
            this.button1.Text = "Process All";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(9, 526);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(329, 18);
            this.label6.TabIndex = 69;
            this.label6.Text = "Branch : 0.00";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmPayrollEntries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 600);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.btnCheckingDisplay);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tssDataStatus);
            this.Controls.Add(this.pbDataProgress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.dgvDisplay);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmPayrollEntries";
            this.Text = "SAP Payroll Deduction Uploading";
            this.Load += new System.EventHandler(this.frmPayrollEntries_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.ComboBox cboBranch;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cboArea;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridView dgvDisplay;
    private System.Windows.Forms.ProgressBar pbDataProgress;
    private System.Windows.Forms.Label tssDataStatus;
    private System.Windows.Forms.ContextMenuStrip cmsMenu;
    private System.Windows.Forms.ToolStripMenuItem openInManualPayrollToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem updatePayrollTimeRecordToolStripMenuItem;
    private System.Windows.Forms.Button btnGenerate;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btnCheckingDisplay;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblTotalAmount;
    private System.Windows.Forms.ComboBox cboYear;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Label label6;
}