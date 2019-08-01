partial class frmLeaveDetails
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
            this.btnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLeaveCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.txtTotalDays = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalHours = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNoOfHrs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoOfMins = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.brwLeaveCode = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLeaveAccountCode = new System.Windows.Forms.TextBox();
            this.brwEmployeeCode = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEmployeeNo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.txtDateFrom = new System.Windows.Forms.MaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDateTo = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(223, 510);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.TabIndex = 48;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Image = global::Integra_Console.Properties.Resources.Database_03;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(111, 510);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 25);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 114;
            this.label8.Text = "Leave Code";
            // 
            // txtLeaveCode
            // 
            this.txtLeaveCode.Location = new System.Drawing.Point(95, 78);
            this.txtLeaveCode.Name = "txtLeaveCode";
            this.txtLeaveCode.ReadOnly = true;
            this.txtLeaveCode.Size = new System.Drawing.Size(171, 22);
            this.txtLeaveCode.TabIndex = 115;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 116;
            this.label9.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(95, 104);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(234, 22);
            this.txtDescription.TabIndex = 117;
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Location = new System.Drawing.Point(6, 255);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(58, 13);
            this.lblEmployee.TabIndex = 119;
            this.lblEmployee.Text = "Total Days";
            // 
            // txtTotalDays
            // 
            this.txtTotalDays.Location = new System.Drawing.Point(95, 252);
            this.txtTotalDays.Name = "txtTotalDays";
            this.txtTotalDays.Size = new System.Drawing.Size(95, 22);
            this.txtTotalDays.TabIndex = 120;
            this.txtTotalDays.Text = "0.00";
            this.txtTotalDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalDays.TextChanged += new System.EventHandler(this.txtTotalDays_TextChanged);
            this.txtTotalDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.txtTotalDays.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 283);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 121;
            this.label2.Text = "Total Hours";
            // 
            // txtTotalHours
            // 
            this.txtTotalHours.Location = new System.Drawing.Point(95, 280);
            this.txtTotalHours.Name = "txtTotalHours";
            this.txtTotalHours.Size = new System.Drawing.Size(95, 22);
            this.txtTotalHours.TabIndex = 122;
            this.txtTotalHours.Text = "0.00";
            this.txtTotalHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.txtTotalHours.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 123;
            this.label3.Text = "No Of Hrs";
            // 
            // txtNoOfHrs
            // 
            this.txtNoOfHrs.Location = new System.Drawing.Point(95, 321);
            this.txtNoOfHrs.Name = "txtNoOfHrs";
            this.txtNoOfHrs.ReadOnly = true;
            this.txtNoOfHrs.Size = new System.Drawing.Size(63, 22);
            this.txtNoOfHrs.TabIndex = 124;
            this.txtNoOfHrs.Text = "0.00";
            this.txtNoOfHrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfHrs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 324);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 125;
            this.label4.Text = "No Of Mins";
            // 
            // txtNoOfMins
            // 
            this.txtNoOfMins.Location = new System.Drawing.Point(253, 321);
            this.txtNoOfMins.Name = "txtNoOfMins";
            this.txtNoOfMins.ReadOnly = true;
            this.txtNoOfMins.Size = new System.Drawing.Size(63, 22);
            this.txtNoOfMins.TabIndex = 126;
            this.txtNoOfMins.Text = "0.00";
            this.txtNoOfMins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfMins.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 352);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 129;
            this.label6.Text = "Amount";
            this.label6.Visible = false;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(95, 349);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(157, 22);
            this.txtAmount.TabIndex = 130;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.Visible = false;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 384);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 131;
            this.label7.Text = "Reamrks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(9, 400);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(320, 101);
            this.txtRemarks.TabIndex = 132;
            // 
            // brwLeaveCode
            // 
            this.brwLeaveCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.brwLeaveCode.Image = global::Integra_Console.Properties.Resources._16_view;
            this.brwLeaveCode.Location = new System.Drawing.Point(268, 78);
            this.brwLeaveCode.Name = "brwLeaveCode";
            this.brwLeaveCode.Size = new System.Drawing.Size(24, 22);
            this.brwLeaveCode.TabIndex = 194;
            this.brwLeaveCode.Click += new System.EventHandler(this.brw_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 195;
            this.label5.Text = "Leave Account";
            // 
            // txtLeaveAccountCode
            // 
            this.txtLeaveAccountCode.Location = new System.Drawing.Point(95, 130);
            this.txtLeaveAccountCode.Name = "txtLeaveAccountCode";
            this.txtLeaveAccountCode.ReadOnly = true;
            this.txtLeaveAccountCode.Size = new System.Drawing.Size(234, 22);
            this.txtLeaveAccountCode.TabIndex = 196;
            // 
            // brwEmployeeCode
            // 
            this.brwEmployeeCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.brwEmployeeCode.Image = global::Integra_Console.Properties.Resources._16_view;
            this.brwEmployeeCode.Location = new System.Drawing.Point(268, 12);
            this.brwEmployeeCode.Name = "brwEmployeeCode";
            this.brwEmployeeCode.Size = new System.Drawing.Size(24, 22);
            this.brwEmployeeCode.TabIndex = 201;
            this.brwEmployeeCode.Click += new System.EventHandler(this.brw_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 197;
            this.label11.Text = "EE No.";
            // 
            // txtEmployeeNo
            // 
            this.txtEmployeeNo.Location = new System.Drawing.Point(95, 12);
            this.txtEmployeeNo.Name = "txtEmployeeNo";
            this.txtEmployeeNo.ReadOnly = true;
            this.txtEmployeeNo.Size = new System.Drawing.Size(171, 22);
            this.txtEmployeeNo.TabIndex = 198;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 199;
            this.label12.Text = "Name";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(95, 38);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.ReadOnly = true;
            this.txtEmployeeName.Size = new System.Drawing.Size(234, 22);
            this.txtEmployeeName.TabIndex = 200;
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.Location = new System.Drawing.Point(95, 189);
            this.txtDateFrom.Mask = "00/00/0000";
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.Size = new System.Drawing.Size(112, 22);
            this.txtDateFrom.TabIndex = 206;
            this.txtDateFrom.ValidatingType = typeof(System.DateTime);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 192);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 205;
            this.label13.Text = "Date From";
            // 
            // txtDateTo
            // 
            this.txtDateTo.Location = new System.Drawing.Point(95, 215);
            this.txtDateTo.Mask = "00/00/0000";
            this.txtDateTo.Name = "txtDateTo";
            this.txtDateTo.Size = new System.Drawing.Size(112, 22);
            this.txtDateTo.TabIndex = 208;
            this.txtDateTo.ValidatingType = typeof(System.DateTime);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 218);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 207;
            this.label14.Text = "Date To";
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(95, 162);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(138, 21);
            this.cboYear.TabIndex = 210;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 209;
            this.label1.Text = "Year";
            // 
            // frmLeaveDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 542);
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDateTo);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtDateFrom);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.brwEmployeeCode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtEmployeeNo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtEmployeeName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLeaveAccountCode);
            this.Controls.Add(this.brwLeaveCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNoOfMins);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNoOfHrs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTotalHours);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.txtTotalDays);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtLeaveCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmLeaveDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leave Details";
            this.Load += new System.EventHandler(this.frmLeaveDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtLeaveCode;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.Label lblEmployee;
    private System.Windows.Forms.TextBox txtTotalDays;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTotalHours;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtNoOfHrs;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtNoOfMins;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtAmount;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtRemarks;
    private System.Windows.Forms.Label brwLeaveCode;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtLeaveAccountCode;
    private System.Windows.Forms.Label brwEmployeeCode;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtEmployeeNo;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox txtEmployeeName;
    private System.Windows.Forms.MaskedTextBox txtDateFrom;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.MaskedTextBox txtDateTo;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.ComboBox cboYear;
    private System.Windows.Forms.Label label1;
}