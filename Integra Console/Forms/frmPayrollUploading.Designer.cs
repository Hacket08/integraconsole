partial class frmPayrollUploading
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
            this.ofdExcel = new System.Windows.Forms.OpenFileDialog();
            this.btnGen = new System.Windows.Forms.Button();
            this.cmbWorkSheet = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtExcelFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.tssDataStatus = new System.Windows.Forms.Label();
            this.pbDataProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdExcel
            // 
            this.ofdExcel.FileName = "ofdExcel";
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(292, 33);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(125, 21);
            this.btnGen.TabIndex = 54;
            this.btnGen.Text = "&Generate Data";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // cmbWorkSheet
            // 
            this.cmbWorkSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkSheet.FormattingEnabled = true;
            this.cmbWorkSheet.Location = new System.Drawing.Point(103, 33);
            this.cmbWorkSheet.Name = "cmbWorkSheet";
            this.cmbWorkSheet.Size = new System.Drawing.Size(183, 21);
            this.cmbWorkSheet.TabIndex = 53;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "Worksheet";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 88);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(756, 267);
            this.dataGridView1.TabIndex = 49;
            // 
            // txtExcelFile
            // 
            this.txtExcelFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcelFile.Location = new System.Drawing.Point(103, 5);
            this.txtExcelFile.Name = "txtExcelFile";
            this.txtExcelFile.Size = new System.Drawing.Size(638, 22);
            this.txtExcelFile.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Excel File";
            // 
            // cboPayrolPeriod
            // 
            this.cboPayrolPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayrolPeriod.FormattingEnabled = true;
            this.cboPayrolPeriod.Location = new System.Drawing.Point(103, 61);
            this.cboPayrolPeriod.Name = "cboPayrolPeriod";
            this.cboPayrolPeriod.Size = new System.Drawing.Size(183, 21);
            this.cboPayrolPeriod.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Payroll Period";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(664, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.TabIndex = 51;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Image = global::Integra_Console.Properties.Resources.Folder;
            this.btnBrowse.Location = new System.Drawing.Point(742, 5);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 22);
            this.btnBrowse.TabIndex = 48;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.Image = global::Integra_Console.Properties.Resources.Database_03;
            this.btnUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpload.Location = new System.Drawing.Point(552, 361);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(106, 25);
            this.btnUpload.TabIndex = 50;
            this.btnUpload.Text = "&Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // tssDataStatus
            // 
            this.tssDataStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tssDataStatus.AutoSize = true;
            this.tssDataStatus.Location = new System.Drawing.Point(11, 376);
            this.tssDataStatus.Name = "tssDataStatus";
            this.tssDataStatus.Size = new System.Drawing.Size(39, 13);
            this.tssDataStatus.TabIndex = 61;
            this.tssDataStatus.Text = "Status";
            // 
            // pbDataProgress
            // 
            this.pbDataProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDataProgress.Location = new System.Drawing.Point(14, 392);
            this.pbDataProgress.Name = "pbDataProgress";
            this.pbDataProgress.Size = new System.Drawing.Size(754, 17);
            this.pbDataProgress.TabIndex = 60;
            // 
            // frmPayrollUploading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 421);
            this.Controls.Add(this.tssDataStatus);
            this.Controls.Add(this.pbDataProgress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.cmbWorkSheet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtExcelFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboPayrolPeriod);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmPayrollUploading";
            this.Text = "Payroll Uploading";
            this.Load += new System.EventHandler(this.frmPayrollUploading_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.OpenFileDialog ofdExcel;
    private System.Windows.Forms.Button btnGen;
    private System.Windows.Forms.ComboBox cmbWorkSheet;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.TextBox txtExcelFile;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label tssDataStatus;
    private System.Windows.Forms.ProgressBar pbDataProgress;
}