partial class frmPaySlipParameter
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmployeeTo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmployeeFrom = new System.Windows.Forms.TextBox();
            this.rbBank = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.chkManagerial = new System.Windows.Forms.CheckBox();
            this.chkSupervisory = new System.Windows.Forms.CheckBox();
            this.chkFilterPerEmployee = new System.Windows.Forms.CheckBox();
            this.chkRankAndFile = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEmployee = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCash = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbPerGroup = new System.Windows.Forms.RadioButton();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.rbDetailed = new System.Windows.Forms.RadioButton();
            this.pnlEmployee.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
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
            // rbBank
            // 
            this.rbBank.AutoSize = true;
            this.rbBank.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rbBank.ForeColor = System.Drawing.Color.Black;
            this.rbBank.Location = new System.Drawing.Point(15, 46);
            this.rbBank.Name = "rbBank";
            this.rbBank.Size = new System.Drawing.Size(51, 17);
            this.rbBank.TabIndex = 1;
            this.rbBank.TabStop = true;
            this.rbBank.Text = "Bank";
            this.rbBank.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rbAll.ForeColor = System.Drawing.Color.Black;
            this.rbAll.Location = new System.Drawing.Point(15, 23);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(38, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
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
            this.chkFilterPerEmployee.Location = new System.Drawing.Point(12, 216);
            this.chkFilterPerEmployee.Name = "chkFilterPerEmployee";
            this.chkFilterPerEmployee.Size = new System.Drawing.Size(123, 17);
            this.chkFilterPerEmployee.TabIndex = 104;
            this.chkFilterPerEmployee.Text = "Filter Per Employee";
            this.chkFilterPerEmployee.UseVisualStyleBackColor = true;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(13, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "Employee No.";
            // 
            // pnlEmployee
            // 
            this.pnlEmployee.Controls.Add(this.label6);
            this.pnlEmployee.Controls.Add(this.txtEmployeeTo);
            this.pnlEmployee.Controls.Add(this.label5);
            this.pnlEmployee.Controls.Add(this.txtEmployeeFrom);
            this.pnlEmployee.Location = new System.Drawing.Point(12, 252);
            this.pnlEmployee.Name = "pnlEmployee";
            this.pnlEmployee.Size = new System.Drawing.Size(378, 60);
            this.pnlEmployee.TabIndex = 103;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(273, 388);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 22);
            this.btnCancel.TabIndex = 100;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpload.Location = new System.Drawing.Point(159, 388);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(108, 22);
            this.btnUpload.TabIndex = 99;
            this.btnUpload.Text = "OK";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCash);
            this.groupBox2.Controls.Add(this.rbBank);
            this.groupBox2.Controls.Add(this.rbAll);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.groupBox2.Location = new System.Drawing.Point(186, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 100);
            this.groupBox2.TabIndex = 97;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Payment Mode";
            // 
            // rbCash
            // 
            this.rbCash.AutoSize = true;
            this.rbCash.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rbCash.ForeColor = System.Drawing.Color.Black;
            this.rbCash.Location = new System.Drawing.Point(15, 69);
            this.rbCash.Name = "rbCash";
            this.rbCash.Size = new System.Drawing.Size(50, 17);
            this.rbCash.TabIndex = 2;
            this.rbCash.TabStop = true;
            this.rbCash.Text = "Cash";
            this.rbCash.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkManagerial);
            this.groupBox1.Controls.Add(this.chkSupervisory);
            this.groupBox1.Controls.Add(this.chkRankAndFile);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(12, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 100);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Confidentiality Level";
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(101, 52);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(289, 21);
            this.cboArea.TabIndex = 94;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 95;
            this.label4.Text = "Area";
            // 
            // cboPayrolPeriod
            // 
            this.cboPayrolPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayrolPeriod.FormattingEnabled = true;
            this.cboPayrolPeriod.Location = new System.Drawing.Point(101, 8);
            this.cboPayrolPeriod.Name = "cboPayrolPeriod";
            this.cboPayrolPeriod.Size = new System.Drawing.Size(289, 21);
            this.cboPayrolPeriod.TabIndex = 90;
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(101, 79);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(289, 21);
            this.cboBranch.TabIndex = 92;
            this.cboBranch.SelectedIndexChanged += new System.EventHandler(this.cboBranch_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 91;
            this.label2.Text = "Payroll Period";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 93;
            this.label3.Text = "Branch";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbDetailed);
            this.groupBox3.Controls.Add(this.rbPerGroup);
            this.groupBox3.Controls.Add(this.rbStandard);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.groupBox3.Location = new System.Drawing.Point(12, 329);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(378, 54);
            this.groupBox3.TabIndex = 105;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Report Format";
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
            // rbDetailed
            // 
            this.rbDetailed.AutoSize = true;
            this.rbDetailed.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rbDetailed.ForeColor = System.Drawing.Color.Black;
            this.rbDetailed.Location = new System.Drawing.Point(239, 23);
            this.rbDetailed.Name = "rbDetailed";
            this.rbDetailed.Size = new System.Drawing.Size(68, 17);
            this.rbDetailed.TabIndex = 2;
            this.rbDetailed.TabStop = true;
            this.rbDetailed.Text = "Detailed";
            this.rbDetailed.UseVisualStyleBackColor = true;
            // 
            // frmPaySlipParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 416);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chkFilterPerEmployee);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlEmployee);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPaySlipParameter";
            this.Text = "Parameter";
            this.Load += new System.EventHandler(this.frmPaySlipParameter_Load);
            this.pnlEmployee.ResumeLayout(false);
            this.pnlEmployee.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtEmployeeTo;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtEmployeeFrom;
    private System.Windows.Forms.RadioButton rbBank;
    private System.Windows.Forms.RadioButton rbAll;
    private System.Windows.Forms.CheckBox chkManagerial;
    private System.Windows.Forms.CheckBox chkSupervisory;
    private System.Windows.Forms.CheckBox chkFilterPerEmployee;
    private System.Windows.Forms.CheckBox chkRankAndFile;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel pnlEmployee;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ComboBox cboArea;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.ComboBox cboBranch;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.RadioButton rbCash;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.RadioButton rbPerGroup;
    private System.Windows.Forms.RadioButton rbStandard;
    private System.Windows.Forms.RadioButton rbDetailed;
}