﻿partial class frmEmpParameter
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkManagerial = new System.Windows.Forms.CheckBox();
            this.chkSupervisory = new System.Windows.Forms.CheckBox();
            this.chkRankAndFile = new System.Windows.Forms.CheckBox();
            this.rbInActive = new System.Windows.Forms.RadioButton();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.chkFilterPerEmployee = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlEmployee = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmployeeTo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEmployeeFrom = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlEmployee.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(282, 419);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 22);
            this.btnCancel.TabIndex = 116;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpload.Location = new System.Drawing.Point(168, 419);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(108, 22);
            this.btnUpload.TabIndex = 115;
            this.btnUpload.Text = "OK";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(96, 75);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(289, 21);
            this.cboArea.TabIndex = 110;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 111;
            this.label4.Text = "Area";
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(96, 102);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(289, 21);
            this.cboBranch.TabIndex = 108;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 109;
            this.label3.Text = "Branch";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chkFilterPerEmployee);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pnlEmployee);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cboCompany);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboDepartment);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.rbInActive);
            this.panel1.Controls.Add(this.rbActive);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboBranch);
            this.panel1.Controls.Add(this.cboArea);
            this.panel1.Location = new System.Drawing.Point(2, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 408);
            this.panel1.TabIndex = 121;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 129;
            this.label5.Text = "Company";
            // 
            // cboCompany
            // 
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(96, 12);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(289, 21);
            this.cboCompany.TabIndex = 128;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 127;
            this.label1.Text = "Department";
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(96, 129);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(289, 21);
            this.cboDepartment.TabIndex = 126;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkManagerial);
            this.groupBox1.Controls.Add(this.chkSupervisory);
            this.groupBox1.Controls.Add(this.chkRankAndFile);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(9, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 100);
            this.groupBox1.TabIndex = 124;
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
            // rbInActive
            // 
            this.rbInActive.AutoSize = true;
            this.rbInActive.Location = new System.Drawing.Point(97, 262);
            this.rbInActive.Name = "rbInActive";
            this.rbInActive.Size = new System.Drawing.Size(69, 17);
            this.rbInActive.TabIndex = 121;
            this.rbInActive.TabStop = true;
            this.rbInActive.Text = "In-Active";
            this.rbInActive.UseVisualStyleBackColor = true;
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Location = new System.Drawing.Point(7, 262);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(55, 17);
            this.rbActive.TabIndex = 120;
            this.rbActive.TabStop = true;
            this.rbActive.Text = "Active";
            this.rbActive.UseVisualStyleBackColor = true;
            // 
            // chkFilterPerEmployee
            // 
            this.chkFilterPerEmployee.AutoSize = true;
            this.chkFilterPerEmployee.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkFilterPerEmployee.ForeColor = System.Drawing.Color.Black;
            this.chkFilterPerEmployee.Location = new System.Drawing.Point(7, 301);
            this.chkFilterPerEmployee.Name = "chkFilterPerEmployee";
            this.chkFilterPerEmployee.Size = new System.Drawing.Size(123, 17);
            this.chkFilterPerEmployee.TabIndex = 132;
            this.chkFilterPerEmployee.Text = "Filter Per Employee";
            this.chkFilterPerEmployee.UseVisualStyleBackColor = true;
            this.chkFilterPerEmployee.CheckedChanged += new System.EventHandler(this.chkFilterPerEmployee_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(8, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 130;
            this.label2.Text = "Employee No.";
            // 
            // pnlEmployee
            // 
            this.pnlEmployee.Controls.Add(this.label6);
            this.pnlEmployee.Controls.Add(this.txtEmployeeTo);
            this.pnlEmployee.Controls.Add(this.label7);
            this.pnlEmployee.Controls.Add(this.txtEmployeeFrom);
            this.pnlEmployee.Location = new System.Drawing.Point(7, 337);
            this.pnlEmployee.Name = "pnlEmployee";
            this.pnlEmployee.Size = new System.Drawing.Size(378, 60);
            this.pnlEmployee.TabIndex = 131;
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 75;
            this.label7.Text = "Employee From";
            // 
            // txtEmployeeFrom
            // 
            this.txtEmployeeFrom.Location = new System.Drawing.Point(114, 6);
            this.txtEmployeeFrom.Name = "txtEmployeeFrom";
            this.txtEmployeeFrom.Size = new System.Drawing.Size(158, 22);
            this.txtEmployeeFrom.TabIndex = 76;
            // 
            // frmEmpParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 448);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmEmpParameter";
            this.Text = "Parameter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEmpParameter_FormClosing);
            this.Load += new System.EventHandler(this.frmEmpParameter_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlEmployee.ResumeLayout(false);
            this.pnlEmployee.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.ComboBox cboArea;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cboBranch;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkManagerial;
    private System.Windows.Forms.CheckBox chkSupervisory;
    private System.Windows.Forms.CheckBox chkRankAndFile;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cboDepartment;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cboCompany;
    private System.Windows.Forms.RadioButton rbInActive;
    private System.Windows.Forms.RadioButton rbActive;
    private System.Windows.Forms.CheckBox chkFilterPerEmployee;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel pnlEmployee;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtEmployeeTo;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtEmployeeFrom;
}