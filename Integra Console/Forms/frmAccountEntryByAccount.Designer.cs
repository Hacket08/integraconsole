partial class frmAccountEntryByAccount
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
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLoanAccountCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.btnRowDelete = new System.Windows.Forms.Button();
            this.btnRowEdit = new System.Windows.Forms.Button();
            this.btnRowAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(294, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 22);
            this.button1.TabIndex = 170;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 166;
            this.label8.Text = "Code";
            // 
            // txtLoanAccountCode
            // 
            this.txtLoanAccountCode.Location = new System.Drawing.Point(102, 39);
            this.txtLoanAccountCode.Name = "txtLoanAccountCode";
            this.txtLoanAccountCode.ReadOnly = true;
            this.txtLoanAccountCode.Size = new System.Drawing.Size(190, 22);
            this.txtLoanAccountCode.TabIndex = 167;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 168;
            this.label9.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(102, 65);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(218, 22);
            this.txtDescription.TabIndex = 169;
            // 
            // cboPayrolPeriod
            // 
            this.cboPayrolPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayrolPeriod.FormattingEnabled = true;
            this.cboPayrolPeriod.Location = new System.Drawing.Point(102, 12);
            this.cboPayrolPeriod.Name = "cboPayrolPeriod";
            this.cboPayrolPeriod.Size = new System.Drawing.Size(218, 21);
            this.cboPayrolPeriod.TabIndex = 172;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 171;
            this.label4.Text = "Payroll Period";
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Location = new System.Drawing.Point(3, 93);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(364, 363);
            this.dgvDisplay.TabIndex = 173;
            this.dgvDisplay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellClick);
            // 
            // btnRowDelete
            // 
            this.btnRowDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRowDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRowDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRowDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRowDelete.Location = new System.Drawing.Point(373, 431);
            this.btnRowDelete.Name = "btnRowDelete";
            this.btnRowDelete.Size = new System.Drawing.Size(106, 25);
            this.btnRowDelete.TabIndex = 176;
            this.btnRowDelete.Text = "Delete";
            this.btnRowDelete.UseVisualStyleBackColor = true;
            this.btnRowDelete.Click += new System.EventHandler(this.btnRowDelete_Click);
            // 
            // btnRowEdit
            // 
            this.btnRowEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRowEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRowEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRowEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRowEdit.Location = new System.Drawing.Point(373, 400);
            this.btnRowEdit.Name = "btnRowEdit";
            this.btnRowEdit.Size = new System.Drawing.Size(106, 25);
            this.btnRowEdit.TabIndex = 175;
            this.btnRowEdit.Text = "Edit";
            this.btnRowEdit.UseVisualStyleBackColor = true;
            this.btnRowEdit.Click += new System.EventHandler(this.btnRowEdit_Click);
            // 
            // btnRowAdd
            // 
            this.btnRowAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRowAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRowAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRowAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRowAdd.Location = new System.Drawing.Point(373, 369);
            this.btnRowAdd.Name = "btnRowAdd";
            this.btnRowAdd.Size = new System.Drawing.Size(106, 25);
            this.btnRowAdd.TabIndex = 174;
            this.btnRowAdd.Text = "Add";
            this.btnRowAdd.UseVisualStyleBackColor = true;
            this.btnRowAdd.Click += new System.EventHandler(this.btnRowAdd_Click);
            // 
            // frmAccountEntryByAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 458);
            this.Controls.Add(this.btnRowDelete);
            this.Controls.Add(this.btnRowEdit);
            this.Controls.Add(this.btnRowAdd);
            this.Controls.Add(this.dgvDisplay);
            this.Controls.Add(this.cboPayrolPeriod);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtLoanAccountCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDescription);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmAccountEntryByAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Entry By Account";
            this.Load += new System.EventHandler(this.frmAccountEntryByAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtLoanAccountCode;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.DataGridView dgvDisplay;
    private System.Windows.Forms.Button btnRowDelete;
    private System.Windows.Forms.Button btnRowEdit;
    private System.Windows.Forms.Button btnRowAdd;
}