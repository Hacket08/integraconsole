partial class frmAccountEntry
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
            this.btnEmployee = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmpCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvDisplayTimeSheet = new System.Windows.Forms.DataGridView();
            this.dgvDisplayOthrs = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplayTimeSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplayOthrs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEmployee
            // 
            this.btnEmployee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmployee.Location = new System.Drawing.Point(299, 33);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Size = new System.Drawing.Size(26, 22);
            this.btnEmployee.TabIndex = 156;
            this.btnEmployee.UseVisualStyleBackColor = true;
            this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 152;
            this.label2.Text = "Employee No";
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.Location = new System.Drawing.Point(107, 33);
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.ReadOnly = true;
            this.txtEmpCode.Size = new System.Drawing.Size(190, 22);
            this.txtEmpCode.TabIndex = 153;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 154;
            this.label3.Text = "Employee Name";
            // 
            // txtEmpName
            // 
            this.txtEmpName.Location = new System.Drawing.Point(107, 59);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.ReadOnly = true;
            this.txtEmpName.Size = new System.Drawing.Size(218, 22);
            this.txtEmpName.TabIndex = 155;
            // 
            // cboPayrolPeriod
            // 
            this.cboPayrolPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayrolPeriod.FormattingEnabled = true;
            this.cboPayrolPeriod.Location = new System.Drawing.Point(107, 6);
            this.cboPayrolPeriod.Name = "cboPayrolPeriod";
            this.cboPayrolPeriod.Size = new System.Drawing.Size(218, 21);
            this.cboPayrolPeriod.TabIndex = 158;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 157;
            this.label4.Text = "Payroll Period";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(746, 216);
            this.tabControl1.TabIndex = 159;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvDisplayTimeSheet);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(738, 190);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Time Sheet";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvDisplayOthrs);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(738, 190);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Other Income / Deduction";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvDisplayTimeSheet
            // 
            this.dgvDisplayTimeSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplayTimeSheet.Location = new System.Drawing.Point(7, 6);
            this.dgvDisplayTimeSheet.Name = "dgvDisplayTimeSheet";
            this.dgvDisplayTimeSheet.Size = new System.Drawing.Size(625, 181);
            this.dgvDisplayTimeSheet.TabIndex = 0;
            this.dgvDisplayTimeSheet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplayTimeSheet_CellClick);
            // 
            // dgvDisplayOthrs
            // 
            this.dgvDisplayOthrs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplayOthrs.Location = new System.Drawing.Point(7, 6);
            this.dgvDisplayOthrs.Name = "dgvDisplayOthrs";
            this.dgvDisplayOthrs.Size = new System.Drawing.Size(625, 181);
            this.dgvDisplayOthrs.TabIndex = 1;
            this.dgvDisplayOthrs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplayOthrs_CellClick);
            // 
            // frmAccountEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 306);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cboPayrolPeriod);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnEmployee);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEmpCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEmpName);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmAccountEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Entry";
            this.Load += new System.EventHandler(this.frmAccountEntry_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplayTimeSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplayOthrs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button btnEmployee;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtEmpCode;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtEmpName;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.DataGridView dgvDisplayTimeSheet;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.DataGridView dgvDisplayOthrs;
}