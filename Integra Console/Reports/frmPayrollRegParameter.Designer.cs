partial class frmPayrollRegParameter
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
            this.btnUpload = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.chkRankAndFile = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkManagerial = new System.Windows.Forms.CheckBox();
            this.chkSupervisory = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPerGroup = new System.Windows.Forms.RadioButton();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.chkFilterPerEmployee = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cboReportType = new System.Windows.Forms.ComboBox();
            this.chkLastPay = new System.Windows.Forms.CheckBox();
            this.txtEmployeeFrom = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmployeeTo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlEmployee = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rbBranch = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlEmployee.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpload.Location = new System.Drawing.Point(167, 464);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(108, 22);
            this.btnUpload.TabIndex = 82;
            this.btnUpload.Text = "OK";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Branch";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Payroll Period";
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(100, 82);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(289, 21);
            this.cboBranch.TabIndex = 21;
            // 
            // cboPayrolPeriod
            // 
            this.cboPayrolPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayrolPeriod.FormattingEnabled = true;
            this.cboPayrolPeriod.Location = new System.Drawing.Point(100, 11);
            this.cboPayrolPeriod.Name = "cboPayrolPeriod";
            this.cboPayrolPeriod.Size = new System.Drawing.Size(289, 21);
            this.cboPayrolPeriod.TabIndex = 18;
            this.cboPayrolPeriod.SelectedIndexChanged += new System.EventHandler(this.cboPayrolPeriod_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Area";
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(100, 55);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(289, 21);
            this.cboArea.TabIndex = 23;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // chkRankAndFile
            // 
            this.chkRankAndFile.AutoSize = true;
            this.chkRankAndFile.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkRankAndFile.ForeColor = System.Drawing.Color.Black;
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
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(11, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 100);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Confidentiality Level";
            // 
            // chkManagerial
            // 
            this.chkManagerial.AutoSize = true;
            this.chkManagerial.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkManagerial.ForeColor = System.Drawing.Color.Black;
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
            this.chkSupervisory.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkSupervisory.ForeColor = System.Drawing.Color.Black;
            this.chkSupervisory.Location = new System.Drawing.Point(11, 46);
            this.chkSupervisory.Name = "chkSupervisory";
            this.chkSupervisory.Size = new System.Drawing.Size(85, 17);
            this.chkSupervisory.TabIndex = 1;
            this.chkSupervisory.Text = "Supervisory";
            this.chkSupervisory.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbBranch);
            this.groupBox2.Controls.Add(this.rbPerGroup);
            this.groupBox2.Controls.Add(this.rbStandard);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.groupBox2.Location = new System.Drawing.Point(11, 350);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 54);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Format";
            // 
            // rbPerGroup
            // 
            this.rbPerGroup.AutoSize = true;
            this.rbPerGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rbPerGroup.ForeColor = System.Drawing.Color.Black;
            this.rbPerGroup.Location = new System.Drawing.Point(133, 23);
            this.rbPerGroup.Name = "rbPerGroup";
            this.rbPerGroup.Size = new System.Drawing.Size(77, 17);
            this.rbPerGroup.TabIndex = 1;
            this.rbPerGroup.TabStop = true;
            this.rbPerGroup.Text = "Per Group";
            this.rbPerGroup.UseVisualStyleBackColor = true;
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rbStandard.ForeColor = System.Drawing.Color.Black;
            this.rbStandard.Location = new System.Drawing.Point(30, 23);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(72, 17);
            this.rbStandard.TabIndex = 0;
            this.rbStandard.TabStop = true;
            this.rbStandard.Text = "Standard";
            this.rbStandard.UseVisualStyleBackColor = true;
            // 
            // chkFilterPerEmployee
            // 
            this.chkFilterPerEmployee.AutoSize = true;
            this.chkFilterPerEmployee.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkFilterPerEmployee.ForeColor = System.Drawing.Color.Black;
            this.chkFilterPerEmployee.Location = new System.Drawing.Point(11, 217);
            this.chkFilterPerEmployee.Name = "chkFilterPerEmployee";
            this.chkFilterPerEmployee.Size = new System.Drawing.Size(123, 17);
            this.chkFilterPerEmployee.TabIndex = 87;
            this.chkFilterPerEmployee.Text = "Filter Per Employee";
            this.chkFilterPerEmployee.UseVisualStyleBackColor = true;
            this.chkFilterPerEmployee.CheckedChanged += new System.EventHandler(this.chkFilterPerEmployee_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(281, 464);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 22);
            this.btnCancel.TabIndex = 83;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 413);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 85;
            this.label7.Text = "Report Type";
            // 
            // cboReportType
            // 
            this.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportType.FormattingEnabled = true;
            this.cboReportType.Location = new System.Drawing.Point(100, 410);
            this.cboReportType.Name = "cboReportType";
            this.cboReportType.Size = new System.Drawing.Size(289, 21);
            this.cboReportType.TabIndex = 84;
            this.cboReportType.SelectedIndexChanged += new System.EventHandler(this.cboReportType_SelectedIndexChanged);
            // 
            // chkLastPay
            // 
            this.chkLastPay.AutoSize = true;
            this.chkLastPay.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkLastPay.ForeColor = System.Drawing.Color.Black;
            this.chkLastPay.Location = new System.Drawing.Point(22, 437);
            this.chkLastPay.Name = "chkLastPay";
            this.chkLastPay.Size = new System.Drawing.Size(127, 17);
            this.chkLastPay.TabIndex = 3;
            this.chkLastPay.Text = "Last Pay Calculation";
            this.chkLastPay.UseVisualStyleBackColor = true;
            // 
            // txtEmployeeFrom
            // 
            this.txtEmployeeFrom.Location = new System.Drawing.Point(114, 6);
            this.txtEmployeeFrom.Name = "txtEmployeeFrom";
            this.txtEmployeeFrom.Size = new System.Drawing.Size(158, 22);
            this.txtEmployeeFrom.TabIndex = 76;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "Employee From";
            // 
            // txtEmployeeTo
            // 
            this.txtEmployeeTo.Location = new System.Drawing.Point(114, 32);
            this.txtEmployeeTo.Name = "txtEmployeeTo";
            this.txtEmployeeTo.Size = new System.Drawing.Size(158, 22);
            this.txtEmployeeTo.TabIndex = 78;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 77;
            this.label6.Text = "Employee To";
            // 
            // pnlEmployee
            // 
            this.pnlEmployee.Controls.Add(this.label6);
            this.pnlEmployee.Controls.Add(this.txtEmployeeTo);
            this.pnlEmployee.Controls.Add(this.label5);
            this.pnlEmployee.Controls.Add(this.txtEmployeeFrom);
            this.pnlEmployee.Location = new System.Drawing.Point(11, 253);
            this.pnlEmployee.Name = "pnlEmployee";
            this.pnlEmployee.Size = new System.Drawing.Size(378, 60);
            this.pnlEmployee.TabIndex = 86;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(12, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 81;
            this.label1.Text = "Employee No.";
            // 
            // rbBranch
            // 
            this.rbBranch.AutoSize = true;
            this.rbBranch.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rbBranch.ForeColor = System.Drawing.Color.Black;
            this.rbBranch.Location = new System.Drawing.Point(237, 23);
            this.rbBranch.Name = "rbBranch";
            this.rbBranch.Size = new System.Drawing.Size(89, 17);
            this.rbBranch.TabIndex = 2;
            this.rbBranch.TabStop = true;
            this.rbBranch.Text = "With Branch";
            this.rbBranch.UseVisualStyleBackColor = true;
            // 
            // frmPayrollRegParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 490);
            this.Controls.Add(this.chkFilterPerEmployee);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlEmployee);
            this.Controls.Add(this.chkLastPay);
            this.Controls.Add(this.cboReportType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboArea);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboPayrolPeriod);
            this.Controls.Add(this.cboBranch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmPayrollRegParameter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parameter";
            this.Load += new System.EventHandler(this.frmPayrollRegParameter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlEmployee.ResumeLayout(false);
            this.pnlEmployee.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cboBranch;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cboArea;
    private System.Windows.Forms.CheckBox chkRankAndFile;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkManagerial;
    private System.Windows.Forms.CheckBox chkSupervisory;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton rbPerGroup;
    private System.Windows.Forms.RadioButton rbStandard;
    private System.Windows.Forms.CheckBox chkFilterPerEmployee;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cboReportType;
    private System.Windows.Forms.CheckBox chkLastPay;
    private System.Windows.Forms.TextBox txtEmployeeFrom;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtEmployeeTo;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Panel pnlEmployee;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.RadioButton rbBranch;
}