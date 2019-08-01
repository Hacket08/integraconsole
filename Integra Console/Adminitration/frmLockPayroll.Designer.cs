partial class frmLockPayroll
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
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLocked = new System.Windows.Forms.Button();
            this.cboPosition = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(104, 39);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(381, 21);
            this.cboBranch.TabIndex = 56;
            this.cboBranch.SelectedIndexChanged += new System.EventHandler(this.cboBranch_SelectedIndexChanged);
            this.cboBranch.TextChanged += new System.EventHandler(this.cbo_Leave);
            this.cboBranch.Click += new System.EventHandler(this.cbo_Leave);
            this.cboBranch.Leave += new System.EventHandler(this.cbo_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Branch";
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(104, 12);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(381, 21);
            this.cboArea.TabIndex = 54;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            this.cboArea.TextChanged += new System.EventHandler(this.cbo_Leave);
            this.cboArea.Click += new System.EventHandler(this.cbo_Leave);
            this.cboArea.Leave += new System.EventHandler(this.cbo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Area";
            // 
            // cboPayrolPeriod
            // 
            this.cboPayrolPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayrolPeriod.FormattingEnabled = true;
            this.cboPayrolPeriod.Location = new System.Drawing.Point(104, 93);
            this.cboPayrolPeriod.Name = "cboPayrolPeriod";
            this.cboPayrolPeriod.Size = new System.Drawing.Size(269, 21);
            this.cboPayrolPeriod.TabIndex = 52;
            this.cboPayrolPeriod.SelectedIndexChanged += new System.EventHandler(this.cboPayrolPeriod_SelectedIndexChanged);
            this.cboPayrolPeriod.TextChanged += new System.EventHandler(this.cbo_Leave);
            this.cboPayrolPeriod.Click += new System.EventHandler(this.cbo_Leave);
            this.cboPayrolPeriod.Leave += new System.EventHandler(this.cbo_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Payroll Period";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(379, 123);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.TabIndex = 61;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLocked
            // 
            this.btnLocked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocked.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLocked.Image = global::Integra_Console.Properties.Resources.Database_03;
            this.btnLocked.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLocked.Location = new System.Drawing.Point(267, 123);
            this.btnLocked.Name = "btnLocked";
            this.btnLocked.Size = new System.Drawing.Size(106, 25);
            this.btnLocked.TabIndex = 60;
            this.btnLocked.Text = "Locked";
            this.btnLocked.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLocked.UseVisualStyleBackColor = true;
            this.btnLocked.Click += new System.EventHandler(this.btnLocked_Click);
            // 
            // cboPosition
            // 
            this.cboPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPosition.FormattingEnabled = true;
            this.cboPosition.Location = new System.Drawing.Point(104, 66);
            this.cboPosition.Name = "cboPosition";
            this.cboPosition.Size = new System.Drawing.Size(381, 21);
            this.cboPosition.TabIndex = 63;
            this.cboPosition.TextChanged += new System.EventHandler(this.cbo_Leave);
            this.cboPosition.Click += new System.EventHandler(this.cbo_Leave);
            this.cboPosition.Leave += new System.EventHandler(this.cbo_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 62;
            this.label6.Text = "Department";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(418, 95);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(67, 17);
            this.chkAll.TabIndex = 65;
            this.chkAll.Text = "View All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // frmLockPayroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 155);
            this.ControlBox = false;
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.cboPosition);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLocked);
            this.Controls.Add(this.cboBranch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboArea);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboPayrolPeriod);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLockPayroll";
            this.Text = "Payroll Period Locker";
            this.Load += new System.EventHandler(this.frmLockPayroll_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cboBranch;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cboArea;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnLocked;
    private System.Windows.Forms.ComboBox cboPosition;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.CheckBox chkAll;
}