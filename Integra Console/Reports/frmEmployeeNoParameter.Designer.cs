partial class frmEmployeeNoParameter
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
            this.txtEmpCode = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblLabel = new System.Windows.Forms.Label();
            this.lblBrowseEmployeeNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.Location = new System.Drawing.Point(104, 7);
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.Size = new System.Drawing.Size(253, 22);
            this.txtEmpCode.TabIndex = 37;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(273, 34);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 22);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(159, 34);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(108, 22);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(12, 10);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(77, 13);
            this.lblLabel.TabIndex = 34;
            this.lblLabel.Text = "Employee No.";
            this.lblLabel.Click += new System.EventHandler(this.lblLabel_Click);
            // 
            // lblBrowseEmployeeNo
            // 
            this.lblBrowseEmployeeNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBrowseEmployeeNo.Image = global::Integra_Console.Properties.Resources._16_view;
            this.lblBrowseEmployeeNo.Location = new System.Drawing.Point(357, 7);
            this.lblBrowseEmployeeNo.Name = "lblBrowseEmployeeNo";
            this.lblBrowseEmployeeNo.Size = new System.Drawing.Size(24, 22);
            this.lblBrowseEmployeeNo.TabIndex = 194;
            this.lblBrowseEmployeeNo.Tag = "EmployeeLoanList";
            this.lblBrowseEmployeeNo.Click += new System.EventHandler(this.lblBrowseEmployeeNo_Click);
            // 
            // frmEmployeeNoParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 63);
            this.Controls.Add(this.lblBrowseEmployeeNo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.txtEmpCode);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmEmployeeNoParameter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee ";
            this.Load += new System.EventHandler(this.frmEmployeeNoParameter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtEmpCode;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label lblLabel;
    private System.Windows.Forms.Label lblBrowseEmployeeNo;
}