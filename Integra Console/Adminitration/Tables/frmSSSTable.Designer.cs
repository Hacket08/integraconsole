partial class frmSSSTable
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
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBracketFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBracketTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmployerShare = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtECC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmployeeShare = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsTotalRecord = new System.Windows.Forms.ToolStripStatusLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Location = new System.Drawing.Point(370, 12);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(529, 326);
            this.dgvDisplay.TabIndex = 0;
            this.dgvDisplay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Income Bracket From";
            // 
            // txtBracketFrom
            // 
            this.txtBracketFrom.Location = new System.Drawing.Point(132, 60);
            this.txtBracketFrom.Name = "txtBracketFrom";
            this.txtBracketFrom.Size = new System.Drawing.Size(100, 22);
            this.txtBracketFrom.TabIndex = 2;
            this.txtBracketFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBracketFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._KeyPress);
            this.txtBracketFrom.Leave += new System.EventHandler(this._TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To";
            // 
            // txtBracketTo
            // 
            this.txtBracketTo.Location = new System.Drawing.Point(262, 60);
            this.txtBracketTo.Name = "txtBracketTo";
            this.txtBracketTo.Size = new System.Drawing.Size(100, 22);
            this.txtBracketTo.TabIndex = 4;
            this.txtBracketTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBracketTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._KeyPress);
            this.txtBracketTo.Leave += new System.EventHandler(this._TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Employer Share";
            // 
            // txtEmployerShare
            // 
            this.txtEmployerShare.Location = new System.Drawing.Point(132, 88);
            this.txtEmployerShare.Name = "txtEmployerShare";
            this.txtEmployerShare.Size = new System.Drawing.Size(100, 22);
            this.txtEmployerShare.TabIndex = 6;
            this.txtEmployerShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEmployerShare.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._KeyPress);
            this.txtEmployerShare.Leave += new System.EventHandler(this._TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "ECC";
            // 
            // txtECC
            // 
            this.txtECC.Location = new System.Drawing.Point(132, 116);
            this.txtECC.Name = "txtECC";
            this.txtECC.Size = new System.Drawing.Size(100, 22);
            this.txtECC.TabIndex = 8;
            this.txtECC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtECC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._KeyPress);
            this.txtECC.Leave += new System.EventHandler(this._TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Employee Share";
            // 
            // txtEmployeeShare
            // 
            this.txtEmployeeShare.Location = new System.Drawing.Point(132, 144);
            this.txtEmployeeShare.Name = "txtEmployeeShare";
            this.txtEmployeeShare.Size = new System.Drawing.Size(100, 22);
            this.txtEmployeeShare.TabIndex = 10;
            this.txtEmployeeShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEmployeeShare.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._KeyPress);
            this.txtEmployeeShare.Leave += new System.EventHandler(this._TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 315);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(83, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(101, 315);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(190, 315);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(83, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(279, 315);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsTotalRecord});
            this.statusStrip1.Location = new System.Drawing.Point(0, 345);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(909, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsTotalRecord
            // 
            this.tsTotalRecord.Name = "tsTotalRecord";
            this.tsTotalRecord.Size = new System.Drawing.Size(63, 17);
            this.tsTotalRecord.Text = "0 Record/s";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(12, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(350, 2);
            this.label6.TabIndex = 16;
            // 
            // cboCompany
            // 
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(79, 16);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(212, 21);
            this.cboCompany.TabIndex = 18;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Company :";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(12, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(350, 2);
            this.label8.TabIndex = 19;
            // 
            // frmSSSTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 367);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboCompany);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtEmployeeShare);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtECC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEmployerShare);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBracketTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBracketFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDisplay);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmSSSTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S.S.S. Table";
            this.Load += new System.EventHandler(this.frmSSSTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgvDisplay;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtBracketFrom;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtBracketTo;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtEmployerShare;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtECC;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtEmployeeShare;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox cboCompany;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ToolStripStatusLabel tsTotalRecord;
}