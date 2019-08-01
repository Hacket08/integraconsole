partial class frmPayrollDetails
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
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLoanAccountCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalDays = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalHours = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNoOfHrs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoOfMins = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLoanRefNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txtInterestAmt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPrincipalAmt = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(223, 379);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.TabIndex = 48;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Image = global::Integra_Console.Properties.Resources.Database_03;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(111, 379);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 25);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(277, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 22);
            this.button1.TabIndex = 118;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 114;
            this.label8.Text = "Code";
            // 
            // txtLoanAccountCode
            // 
            this.txtLoanAccountCode.Location = new System.Drawing.Point(108, 13);
            this.txtLoanAccountCode.Name = "txtLoanAccountCode";
            this.txtLoanAccountCode.ReadOnly = true;
            this.txtLoanAccountCode.Size = new System.Drawing.Size(167, 22);
            this.txtLoanAccountCode.TabIndex = 115;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 116;
            this.label9.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(108, 39);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(195, 22);
            this.txtDescription.TabIndex = 117;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "Total Days";
            // 
            // txtTotalDays
            // 
            this.txtTotalDays.Location = new System.Drawing.Point(108, 251);
            this.txtTotalDays.Name = "txtTotalDays";
            this.txtTotalDays.Size = new System.Drawing.Size(95, 22);
            this.txtTotalDays.TabIndex = 120;
            this.txtTotalDays.Text = "0.00";
            this.txtTotalDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalDays.TextChanged += new System.EventHandler(this.txtTotalDays_TextChanged);
            this.txtTotalDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.txtTotalDays.Leave += new System.EventHandler(this.txtTotalDays_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 121;
            this.label2.Text = "Total Hours";
            // 
            // txtTotalHours
            // 
            this.txtTotalHours.Location = new System.Drawing.Point(108, 279);
            this.txtTotalHours.Name = "txtTotalHours";
            this.txtTotalHours.Size = new System.Drawing.Size(95, 22);
            this.txtTotalHours.TabIndex = 122;
            this.txtTotalHours.Text = "0.00";
            this.txtTotalHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.txtTotalHours.Leave += new System.EventHandler(this.txtTotalHours_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 123;
            this.label3.Text = "No Of Hrs";
            // 
            // txtNoOfHrs
            // 
            this.txtNoOfHrs.Location = new System.Drawing.Point(108, 320);
            this.txtNoOfHrs.Name = "txtNoOfHrs";
            this.txtNoOfHrs.Size = new System.Drawing.Size(63, 22);
            this.txtNoOfHrs.TabIndex = 124;
            this.txtNoOfHrs.Text = "0.00";
            this.txtNoOfHrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfHrs.TextChanged += new System.EventHandler(this.txtNoOfHrs_TextChanged);
            this.txtNoOfHrs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 125;
            this.label4.Text = "No Of Mins";
            // 
            // txtNoOfMins
            // 
            this.txtNoOfMins.Location = new System.Drawing.Point(266, 320);
            this.txtNoOfMins.Name = "txtNoOfMins";
            this.txtNoOfMins.Size = new System.Drawing.Size(63, 22);
            this.txtNoOfMins.TabIndex = 126;
            this.txtNoOfMins.Text = "0.00";
            this.txtNoOfMins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfMins.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 127;
            this.label5.Text = "Reference No.";
            // 
            // txtLoanRefNo
            // 
            this.txtLoanRefNo.Location = new System.Drawing.Point(108, 67);
            this.txtLoanRefNo.Name = "txtLoanRefNo";
            this.txtLoanRefNo.Size = new System.Drawing.Size(157, 22);
            this.txtLoanRefNo.TabIndex = 128;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 351);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 129;
            this.label6.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(108, 348);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(157, 22);
            this.txtAmount.TabIndex = 130;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(3, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 13);
            this.label7.TabIndex = 131;
            this.label7.Text = "Loan Interest Details";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtInterestAmt);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtPrincipalAmt);
            this.panel1.Location = new System.Drawing.Point(6, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 65);
            this.panel1.TabIndex = 137;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 133;
            this.label11.Text = "Interest Amount";
            // 
            // txtInterestAmt
            // 
            this.txtInterestAmt.Location = new System.Drawing.Point(123, 35);
            this.txtInterestAmt.Name = "txtInterestAmt";
            this.txtInterestAmt.ReadOnly = true;
            this.txtInterestAmt.Size = new System.Drawing.Size(136, 22);
            this.txtInterestAmt.TabIndex = 134;
            this.txtInterestAmt.Text = "0.00";
            this.txtInterestAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtInterestAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 131;
            this.label10.Text = "Principal Amount";
            // 
            // txtPrincipalAmt
            // 
            this.txtPrincipalAmt.Location = new System.Drawing.Point(123, 7);
            this.txtPrincipalAmt.Name = "txtPrincipalAmt";
            this.txtPrincipalAmt.Size = new System.Drawing.Size(136, 22);
            this.txtPrincipalAmt.TabIndex = 132;
            this.txtPrincipalAmt.Text = "0.00";
            this.txtPrincipalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrincipalAmt.TextChanged += new System.EventHandler(this.txtPrincipalAmt_TextChanged);
            this.txtPrincipalAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 124);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 17);
            this.checkBox1.TabIndex = 138;
            this.checkBox1.Text = "Interest Payment";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmPayrollDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 416);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLoanRefNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNoOfMins);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNoOfHrs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTotalHours);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalDays);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtLoanAccountCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmPayrollDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payroll Details";
            this.Load += new System.EventHandler(this.frmPayrollDetails_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtLoanAccountCode;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtTotalDays;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTotalHours;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtNoOfHrs;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtNoOfMins;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtLoanRefNo;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtAmount;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtInterestAmt;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox txtPrincipalAmt;
}