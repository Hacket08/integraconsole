﻿partial class frmSAPLoanComparisonReport
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
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openInManualPayrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePayrollTimeRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbDataProgress = new System.Windows.Forms.ProgressBar();
            this.tssDataStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(101, 36);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(381, 21);
            this.cboBranch.TabIndex = 50;
            this.cboBranch.Visible = false;
            this.cboBranch.SelectedIndexChanged += new System.EventHandler(this.cboBranch_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Branch";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Area";
            this.label2.Visible = false;
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.ContextMenuStrip = this.cmsMenu;
            this.dgvDisplay.Location = new System.Drawing.Point(12, 36);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(955, 365);
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
            this.pbDataProgress.Location = new System.Drawing.Point(12, 435);
            this.pbDataProgress.Name = "pbDataProgress";
            this.pbDataProgress.Size = new System.Drawing.Size(955, 17);
            this.pbDataProgress.TabIndex = 58;
            // 
            // tssDataStatus
            // 
            this.tssDataStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tssDataStatus.AutoSize = true;
            this.tssDataStatus.Location = new System.Drawing.Point(9, 419);
            this.tssDataStatus.Name = "tssDataStatus";
            this.tssDataStatus.Size = new System.Drawing.Size(39, 13);
            this.tssDataStatus.TabIndex = 59;
            this.tssDataStatus.Text = "Status";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(861, 405);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.TabIndex = 46;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Image = global::Integra_Console.Properties.Resources.Synchronize;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(12, 9);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(193, 23);
            this.btnGenerate.TabIndex = 62;
            this.btnGenerate.Text = "Map Loan File To SAP B1";
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
            // frmSAPLoanComparisonReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 458);
            this.Controls.Add(this.dgvDisplay);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tssDataStatus);
            this.Controls.Add(this.pbDataProgress);
            this.Controls.Add(this.cboBranch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSAPLoanComparisonReport";
            this.Text = "Loan File Uploading";
            this.Load += new System.EventHandler(this.frmSAPLoanComparisonReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.ComboBox cboBranch;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.DataGridView dgvDisplay;
    private System.Windows.Forms.ProgressBar pbDataProgress;
    private System.Windows.Forms.Label tssDataStatus;
    private System.Windows.Forms.ContextMenuStrip cmsMenu;
    private System.Windows.Forms.ToolStripMenuItem openInManualPayrollToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem updatePayrollTimeRecordToolStripMenuItem;
    private System.Windows.Forms.Button btnGenerate;
    private System.Windows.Forms.Label label7;
}