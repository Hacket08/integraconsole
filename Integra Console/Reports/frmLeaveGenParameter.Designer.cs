partial class frmLeaveGenParameter
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
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEmployee = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmployeeTo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmployeeFrom = new System.Windows.Forms.TextBox();
            this.chkManagerial = new System.Windows.Forms.CheckBox();
            this.chkSupervisory = new System.Windows.Forms.CheckBox();
            this.chkFilterPerEmployee = new System.Windows.Forms.CheckBox();
            this.chkRankAndFile = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDateTo = new System.Windows.Forms.TextBox();
            this.txtDateFrom = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlEmployee.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(13, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 124;
            this.label1.Text = "Employee No.";
            this.label1.Visible = false;
            // 
            // pnlEmployee
            // 
            this.pnlEmployee.Controls.Add(this.label6);
            this.pnlEmployee.Controls.Add(this.txtEmployeeTo);
            this.pnlEmployee.Controls.Add(this.label5);
            this.pnlEmployee.Controls.Add(this.txtEmployeeFrom);
            this.pnlEmployee.Location = new System.Drawing.Point(12, 262);
            this.pnlEmployee.Name = "pnlEmployee";
            this.pnlEmployee.Size = new System.Drawing.Size(378, 60);
            this.pnlEmployee.TabIndex = 127;
            this.pnlEmployee.Visible = false;
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
            // txtEmployeeTo
            // 
            this.txtEmployeeTo.Location = new System.Drawing.Point(114, 32);
            this.txtEmployeeTo.Name = "txtEmployeeTo";
            this.txtEmployeeTo.Size = new System.Drawing.Size(158, 22);
            this.txtEmployeeTo.TabIndex = 78;
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
            // txtEmployeeFrom
            // 
            this.txtEmployeeFrom.Location = new System.Drawing.Point(114, 6);
            this.txtEmployeeFrom.Name = "txtEmployeeFrom";
            this.txtEmployeeFrom.Size = new System.Drawing.Size(158, 22);
            this.txtEmployeeFrom.TabIndex = 76;
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
            // chkFilterPerEmployee
            // 
            this.chkFilterPerEmployee.AutoSize = true;
            this.chkFilterPerEmployee.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkFilterPerEmployee.ForeColor = System.Drawing.Color.Black;
            this.chkFilterPerEmployee.Location = new System.Drawing.Point(12, 226);
            this.chkFilterPerEmployee.Name = "chkFilterPerEmployee";
            this.chkFilterPerEmployee.Size = new System.Drawing.Size(123, 17);
            this.chkFilterPerEmployee.TabIndex = 128;
            this.chkFilterPerEmployee.Text = "Filter Per Employee";
            this.chkFilterPerEmployee.UseVisualStyleBackColor = true;
            this.chkFilterPerEmployee.Visible = false;
            this.chkFilterPerEmployee.CheckedChanged += new System.EventHandler(this.chkFilterPerEmployee_CheckedChanged);
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
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(279, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 22);
            this.btnCancel.TabIndex = 126;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(101, 89);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(289, 21);
            this.cboBranch.TabIndex = 119;
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpload.Location = new System.Drawing.Point(165, 222);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(108, 22);
            this.btnUpload.TabIndex = 125;
            this.btnUpload.Text = "OK";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkManagerial);
            this.groupBox1.Controls.Add(this.chkSupervisory);
            this.groupBox1.Controls.Add(this.chkRankAndFile);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(12, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 100);
            this.groupBox1.TabIndex = 123;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Confidentiality Level";
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(101, 62);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(289, 21);
            this.cboArea.TabIndex = 121;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 122;
            this.label4.Text = "Area";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 120;
            this.label3.Text = "Branch";
            // 
            // txtDateTo
            // 
            this.txtDateTo.Location = new System.Drawing.Point(245, 33);
            this.txtDateTo.Name = "txtDateTo";
            this.txtDateTo.ReadOnly = true;
            this.txtDateTo.Size = new System.Drawing.Size(138, 22);
            this.txtDateTo.TabIndex = 133;
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.Location = new System.Drawing.Point(101, 33);
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.ReadOnly = true;
            this.txtDateFrom.Size = new System.Drawing.Size(138, 22);
            this.txtDateFrom.TabIndex = 132;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 131;
            this.label8.Text = "Date Covered";
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(101, 6);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(138, 21);
            this.cboYear.TabIndex = 130;
            this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 129;
            this.label2.Text = "Year";
            // 
            // frmLeaveGenParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 251);
            this.Controls.Add(this.txtDateTo);
            this.Controls.Add(this.txtDateFrom);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlEmployee);
            this.Controls.Add(this.chkFilterPerEmployee);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboBranch);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboArea);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmLeaveGenParameter";
            this.Text = "Parameter";
            this.Load += new System.EventHandler(this.frmLeaveGenParameter_Load);
            this.pnlEmployee.ResumeLayout(false);
            this.pnlEmployee.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel pnlEmployee;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtEmployeeTo;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtEmployeeFrom;
    private System.Windows.Forms.CheckBox chkManagerial;
    private System.Windows.Forms.CheckBox chkSupervisory;
    private System.Windows.Forms.CheckBox chkFilterPerEmployee;
    private System.Windows.Forms.CheckBox chkRankAndFile;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ComboBox cboBranch;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ComboBox cboArea;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtDateTo;
    private System.Windows.Forms.TextBox txtDateFrom;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ComboBox cboYear;
    private System.Windows.Forms.Label label2;
}