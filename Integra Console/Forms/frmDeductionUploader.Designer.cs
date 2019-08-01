partial class frmDeductionUploader
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ofdExcel = new System.Windows.Forms.OpenFileDialog();
            this.btnGen = new System.Windows.Forms.Button();
            this.cmbWorkSheet = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExcelFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbColumn = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAccountCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtARCode = new System.Windows.Forms.TextBox();
            this.txtCACode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAPPLCOde = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMOTRCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtALCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFURNCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCELPCode = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPPROCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCOMPCode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSPCode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCLCode = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMP2Code = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtSSSLCode = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPIBIGLCode = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtCALMLCode = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtSSS = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtPAGIBIG = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtPHILHEALTH = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtWTAX = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.lblBrowseLoanAccountCode = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(397, 92);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(534, 429);
            this.dataGridView1.TabIndex = 49;
            // 
            // ofdExcel
            // 
            this.ofdExcel.FileName = "ofdExcel";
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(751, 33);
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
            this.cmbWorkSheet.Size = new System.Drawing.Size(642, 21);
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
            // txtExcelFile
            // 
            this.txtExcelFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcelFile.Location = new System.Drawing.Point(103, 5);
            this.txtExcelFile.Name = "txtExcelFile";
            this.txtExcelFile.Size = new System.Drawing.Size(803, 22);
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
            // cmbColumn
            // 
            this.cmbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColumn.FormattingEnabled = true;
            this.cmbColumn.Location = new System.Drawing.Point(716, 64);
            this.cmbColumn.Name = "cmbColumn";
            this.cmbColumn.Size = new System.Drawing.Size(136, 21);
            this.cmbColumn.TabIndex = 56;
            this.cmbColumn.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(617, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Selected Column";
            this.label3.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(373, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "Account";
            this.label5.Visible = false;
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Location = new System.Drawing.Point(472, 64);
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.ReadOnly = true;
            this.txtAccountCode.Size = new System.Drawing.Size(116, 22);
            this.txtAccountCode.TabIndex = 58;
            this.txtAccountCode.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 197;
            this.label6.Text = "Account Receivable";
            // 
            // txtARCode
            // 
            this.txtARCode.Location = new System.Drawing.Point(142, 94);
            this.txtARCode.Name = "txtARCode";
            this.txtARCode.Size = new System.Drawing.Size(56, 22);
            this.txtARCode.TabIndex = 198;
            // 
            // txtCACode
            // 
            this.txtCACode.Location = new System.Drawing.Point(142, 122);
            this.txtCACode.Name = "txtCACode";
            this.txtCACode.Size = new System.Drawing.Size(56, 22);
            this.txtCACode.TabIndex = 200;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 199;
            this.label7.Text = "Cash Advance";
            // 
            // txtAPPLCOde
            // 
            this.txtAPPLCOde.Location = new System.Drawing.Point(142, 178);
            this.txtAPPLCOde.Name = "txtAPPLCOde";
            this.txtAPPLCOde.Size = new System.Drawing.Size(56, 22);
            this.txtAPPLCOde.TabIndex = 202;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 201;
            this.label8.Text = "Appliance Loan";
            // 
            // txtMOTRCode
            // 
            this.txtMOTRCode.Location = new System.Drawing.Point(142, 206);
            this.txtMOTRCode.Name = "txtMOTRCode";
            this.txtMOTRCode.Size = new System.Drawing.Size(56, 22);
            this.txtMOTRCode.TabIndex = 204;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 203;
            this.label9.Text = "Motorcycle Loan";
            // 
            // txtALCode
            // 
            this.txtALCode.Location = new System.Drawing.Point(142, 150);
            this.txtALCode.Name = "txtALCode";
            this.txtALCode.Size = new System.Drawing.Size(56, 22);
            this.txtALCode.TabIndex = 206;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 205;
            this.label10.Text = "Agora Lending";
            // 
            // txtFURNCode
            // 
            this.txtFURNCode.Location = new System.Drawing.Point(142, 234);
            this.txtFURNCode.Name = "txtFURNCode";
            this.txtFURNCode.Size = new System.Drawing.Size(56, 22);
            this.txtFURNCode.TabIndex = 208;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 237);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 207;
            this.label11.Text = "Furniture Loan";
            // 
            // txtCELPCode
            // 
            this.txtCELPCode.Location = new System.Drawing.Point(142, 262);
            this.txtCELPCode.Name = "txtCELPCode";
            this.txtCELPCode.Size = new System.Drawing.Size(56, 22);
            this.txtCELPCode.TabIndex = 210;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 265);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 209;
            this.label12.Text = "Cellphone Loan";
            // 
            // txtPPROCode
            // 
            this.txtPPROCode.Location = new System.Drawing.Point(142, 290);
            this.txtPPROCode.Name = "txtPPROCode";
            this.txtPPROCode.Size = new System.Drawing.Size(56, 22);
            this.txtPPROCode.TabIndex = 212;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 293);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 13);
            this.label13.TabIndex = 211;
            this.label13.Text = "Power Products Loan";
            // 
            // txtCOMPCode
            // 
            this.txtCOMPCode.Location = new System.Drawing.Point(142, 318);
            this.txtCOMPCode.Name = "txtCOMPCode";
            this.txtCOMPCode.Size = new System.Drawing.Size(56, 22);
            this.txtCOMPCode.TabIndex = 214;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 321);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 213;
            this.label14.Text = "Computer Loan";
            // 
            // txtSPCode
            // 
            this.txtSPCode.Location = new System.Drawing.Point(142, 346);
            this.txtSPCode.Name = "txtSPCode";
            this.txtSPCode.Size = new System.Drawing.Size(56, 22);
            this.txtSPCode.TabIndex = 216;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 349);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 13);
            this.label15.TabIndex = 215;
            this.label15.Text = "Spare Parts";
            // 
            // txtCLCode
            // 
            this.txtCLCode.Location = new System.Drawing.Point(142, 374);
            this.txtCLCode.Name = "txtCLCode";
            this.txtCLCode.Size = new System.Drawing.Size(56, 22);
            this.txtCLCode.TabIndex = 218;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 377);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 13);
            this.label16.TabIndex = 217;
            this.label16.Text = "Car Loan";
            // 
            // txtMP2Code
            // 
            this.txtMP2Code.Location = new System.Drawing.Point(335, 94);
            this.txtMP2Code.Name = "txtMP2Code";
            this.txtMP2Code.Size = new System.Drawing.Size(56, 22);
            this.txtMP2Code.TabIndex = 220;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(204, 97);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(109, 13);
            this.label17.TabIndex = 219;
            this.label17.Text = "Modified Pag-Ibig II";
            // 
            // txtSSSLCode
            // 
            this.txtSSSLCode.Location = new System.Drawing.Point(335, 122);
            this.txtSSSLCode.Name = "txtSSSLCode";
            this.txtSSSLCode.Size = new System.Drawing.Size(56, 22);
            this.txtSSSLCode.TabIndex = 222;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(204, 125);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 13);
            this.label18.TabIndex = 221;
            this.label18.Text = "SSS Loan";
            // 
            // txtPIBIGLCode
            // 
            this.txtPIBIGLCode.Location = new System.Drawing.Point(335, 150);
            this.txtPIBIGLCode.Name = "txtPIBIGLCode";
            this.txtPIBIGLCode.Size = new System.Drawing.Size(56, 22);
            this.txtPIBIGLCode.TabIndex = 224;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(204, 153);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(78, 13);
            this.label19.TabIndex = 223;
            this.label19.Text = "Pag-Ibig Loan";
            // 
            // txtCALMLCode
            // 
            this.txtCALMLCode.Location = new System.Drawing.Point(335, 178);
            this.txtCALMLCode.Name = "txtCALMLCode";
            this.txtCALMLCode.Size = new System.Drawing.Size(56, 22);
            this.txtCALMLCode.TabIndex = 226;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(204, 181);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(78, 13);
            this.label20.TabIndex = 225;
            this.label20.Text = "Calamity Loan";
            // 
            // txtSSS
            // 
            this.txtSSS.Location = new System.Drawing.Point(335, 234);
            this.txtSSS.Name = "txtSSS";
            this.txtSSS.Size = new System.Drawing.Size(56, 22);
            this.txtSSS.TabIndex = 228;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(204, 237);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(25, 13);
            this.label21.TabIndex = 227;
            this.label21.Text = "SSS";
            // 
            // txtPAGIBIG
            // 
            this.txtPAGIBIG.Location = new System.Drawing.Point(335, 262);
            this.txtPAGIBIG.Name = "txtPAGIBIG";
            this.txtPAGIBIG.Size = new System.Drawing.Size(56, 22);
            this.txtPAGIBIG.TabIndex = 230;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(204, 265);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 13);
            this.label22.TabIndex = 229;
            this.label22.Text = "PAGIBIG";
            // 
            // txtPHILHEALTH
            // 
            this.txtPHILHEALTH.Location = new System.Drawing.Point(335, 290);
            this.txtPHILHEALTH.Name = "txtPHILHEALTH";
            this.txtPHILHEALTH.Size = new System.Drawing.Size(56, 22);
            this.txtPHILHEALTH.TabIndex = 232;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(204, 293);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(67, 13);
            this.label23.TabIndex = 231;
            this.label23.Text = "PHILHEALTH";
            // 
            // txtWTAX
            // 
            this.txtWTAX.Location = new System.Drawing.Point(335, 318);
            this.txtWTAX.Name = "txtWTAX";
            this.txtWTAX.Size = new System.Drawing.Size(56, 22);
            this.txtWTAX.TabIndex = 234;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(204, 321);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(35, 13);
            this.label24.TabIndex = 233;
            this.label24.Text = "WTAX";
            // 
            // lblBrowseLoanAccountCode
            // 
            this.lblBrowseLoanAccountCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBrowseLoanAccountCode.Image = global::Integra_Console.Properties.Resources._16_view;
            this.lblBrowseLoanAccountCode.Location = new System.Drawing.Point(587, 64);
            this.lblBrowseLoanAccountCode.Name = "lblBrowseLoanAccountCode";
            this.lblBrowseLoanAccountCode.Size = new System.Drawing.Size(24, 22);
            this.lblBrowseLoanAccountCode.TabIndex = 196;
            this.lblBrowseLoanAccountCode.Visible = false;
            this.lblBrowseLoanAccountCode.Click += new System.EventHandler(this.lblBrowseLoanAccountCode_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(825, 527);
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
            this.btnBrowse.Location = new System.Drawing.Point(908, 5);
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
            this.btnUpload.Location = new System.Drawing.Point(713, 527);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(106, 25);
            this.btnUpload.TabIndex = 50;
            this.btnUpload.Text = "&Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // frmDeductionUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 556);
            this.Controls.Add(this.txtWTAX);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtPHILHEALTH);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtPAGIBIG);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtSSS);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtCALMLCode);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtPIBIGLCode);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtSSSLCode);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtMP2Code);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtCLCode);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtSPCode);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtCOMPCode);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtPPROCode);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtCELPCode);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtFURNCode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtALCode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMOTRCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAPPLCOde);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCACode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtARCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAccountCode);
            this.Controls.Add(this.lblBrowseLoanAccountCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbColumn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.cmbWorkSheet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtExcelFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboPayrolPeriod);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmDeductionUploader";
            this.Text = "Deduction Uploader";
            this.Load += new System.EventHandler(this.frmDeductionUploader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.OpenFileDialog ofdExcel;
    private System.Windows.Forms.Button btnGen;
    private System.Windows.Forms.ComboBox cmbWorkSheet;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.TextBox txtExcelFile;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbColumn;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtAccountCode;
    private System.Windows.Forms.Label lblBrowseLoanAccountCode;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtARCode;
    private System.Windows.Forms.TextBox txtCACode;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtAPPLCOde;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtMOTRCode;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtALCode;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox txtFURNCode;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtCELPCode;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox txtPPROCode;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox txtCOMPCode;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox txtSPCode;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TextBox txtCLCode;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.TextBox txtMP2Code;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.TextBox txtSSSLCode;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.TextBox txtPIBIGLCode;
    private System.Windows.Forms.Label label19;
    private System.Windows.Forms.TextBox txtCALMLCode;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.TextBox txtSSS;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.TextBox txtPAGIBIG;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.TextBox txtPHILHEALTH;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.TextBox txtWTAX;
    private System.Windows.Forms.Label label24;
}