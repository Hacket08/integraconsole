partial class frmManualPayroll
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
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmpCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPayrollPeriod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBasicPay = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOTPay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPaidLeaves = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOtherIncome = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGovLoans = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOtherLoan = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOtherDeduction = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTotalDeduction = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNetPay = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSSSEmployee = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPhilHealthEmployee = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPAGIBIGEmployee = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtWtaxAmount = new System.Windows.Forms.TextBox();
            this.txtPAGIBIGEmployer = new System.Windows.Forms.TextBox();
            this.txtPhilHealthEmployer = new System.Windows.Forms.TextBox();
            this.txtSSSEmployer = new System.Windows.Forms.TextBox();
            this.txtSSSECC = new System.Windows.Forms.TextBox();
            this.txtWtaxBaseAmount = new System.Windows.Forms.TextBox();
            this.txtPagibigBaseAmount = new System.Windows.Forms.TextBox();
            this.txtPhiheahltBaseAmount = new System.Windows.Forms.TextBox();
            this.txtSSSBaseAmount = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnRowDelete = new System.Windows.Forms.Button();
            this.btnRowEdit = new System.Windows.Forms.Button();
            this.btnRowAdd = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cboPayrolPeriod = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtHolidayPay = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtGrossPay = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.chkLastPay = new System.Windows.Forms.CheckBox();
            this.chkHold = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Location = new System.Drawing.Point(6, 288);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(831, 206);
            this.dgvDisplay.TabIndex = 39;
            this.dgvDisplay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Code";
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.Location = new System.Drawing.Point(101, 33);
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.Size = new System.Drawing.Size(167, 22);
            this.txtEmpCode.TabIndex = 50;
            this.txtEmpCode.TextChanged += new System.EventHandler(this.txtEmpCode_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Employee Name";
            // 
            // txtEmpName
            // 
            this.txtEmpName.Location = new System.Drawing.Point(101, 59);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Size = new System.Drawing.Size(195, 22);
            this.txtEmpName.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(388, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Payroll Period";
            // 
            // txtPayrollPeriod
            // 
            this.txtPayrollPeriod.Location = new System.Drawing.Point(477, 59);
            this.txtPayrollPeriod.Name = "txtPayrollPeriod";
            this.txtPayrollPeriod.ReadOnly = true;
            this.txtPayrollPeriod.Size = new System.Drawing.Size(195, 22);
            this.txtPayrollPeriod.TabIndex = 54;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(388, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Company";
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(477, 33);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(195, 22);
            this.txtCompany.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Basic Pay";
            // 
            // txtBasicPay
            // 
            this.txtBasicPay.Location = new System.Drawing.Point(116, 120);
            this.txtBasicPay.Name = "txtBasicPay";
            this.txtBasicPay.ReadOnly = true;
            this.txtBasicPay.Size = new System.Drawing.Size(118, 22);
            this.txtBasicPay.TabIndex = 58;
            this.txtBasicPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "OT Pay";
            // 
            // txtOTPay
            // 
            this.txtOTPay.Location = new System.Drawing.Point(116, 148);
            this.txtOTPay.Name = "txtOTPay";
            this.txtOTPay.ReadOnly = true;
            this.txtOTPay.Size = new System.Drawing.Size(118, 22);
            this.txtOTPay.TabIndex = 60;
            this.txtOTPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 61;
            this.label5.Text = "Paid Leaves";
            // 
            // txtPaidLeaves
            // 
            this.txtPaidLeaves.Location = new System.Drawing.Point(116, 204);
            this.txtPaidLeaves.Name = "txtPaidLeaves";
            this.txtPaidLeaves.ReadOnly = true;
            this.txtPaidLeaves.Size = new System.Drawing.Size(118, 22);
            this.txtPaidLeaves.TabIndex = 62;
            this.txtPaidLeaves.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 63;
            this.label6.Text = "Other Income";
            // 
            // txtOtherIncome
            // 
            this.txtOtherIncome.Location = new System.Drawing.Point(116, 232);
            this.txtOtherIncome.Name = "txtOtherIncome";
            this.txtOtherIncome.ReadOnly = true;
            this.txtOtherIncome.Size = new System.Drawing.Size(118, 22);
            this.txtOtherIncome.TabIndex = 64;
            this.txtOtherIncome.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 65;
            this.label7.Text = "Gov. Loans";
            // 
            // txtGovLoans
            // 
            this.txtGovLoans.Location = new System.Drawing.Point(344, 101);
            this.txtGovLoans.Name = "txtGovLoans";
            this.txtGovLoans.ReadOnly = true;
            this.txtGovLoans.Size = new System.Drawing.Size(118, 22);
            this.txtGovLoans.TabIndex = 66;
            this.txtGovLoans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(240, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 69;
            this.label11.Text = "Other Loans";
            // 
            // txtOtherLoan
            // 
            this.txtOtherLoan.Location = new System.Drawing.Point(344, 129);
            this.txtOtherLoan.Name = "txtOtherLoan";
            this.txtOtherLoan.ReadOnly = true;
            this.txtOtherLoan.Size = new System.Drawing.Size(118, 22);
            this.txtOtherLoan.TabIndex = 70;
            this.txtOtherLoan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(240, 160);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 13);
            this.label12.TabIndex = 71;
            this.label12.Text = "Other Deduction";
            // 
            // txtOtherDeduction
            // 
            this.txtOtherDeduction.Location = new System.Drawing.Point(344, 157);
            this.txtOtherDeduction.Name = "txtOtherDeduction";
            this.txtOtherDeduction.ReadOnly = true;
            this.txtOtherDeduction.Size = new System.Drawing.Size(118, 22);
            this.txtOtherDeduction.TabIndex = 72;
            this.txtOtherDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(240, 235);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 73;
            this.label13.Text = "Total Deduction";
            // 
            // txtTotalDeduction
            // 
            this.txtTotalDeduction.Location = new System.Drawing.Point(344, 232);
            this.txtTotalDeduction.Name = "txtTotalDeduction";
            this.txtTotalDeduction.ReadOnly = true;
            this.txtTotalDeduction.Size = new System.Drawing.Size(118, 22);
            this.txtTotalDeduction.TabIndex = 74;
            this.txtTotalDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(240, 263);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 75;
            this.label14.Text = "Net Pay";
            // 
            // txtNetPay
            // 
            this.txtNetPay.Location = new System.Drawing.Point(344, 260);
            this.txtNetPay.Name = "txtNetPay";
            this.txtNetPay.ReadOnly = true;
            this.txtNetPay.Size = new System.Drawing.Size(118, 22);
            this.txtNetPay.TabIndex = 76;
            this.txtNetPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(501, 137);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 77;
            this.label15.Text = "SSS";
            // 
            // txtSSSEmployee
            // 
            this.txtSSSEmployee.Location = new System.Drawing.Point(581, 134);
            this.txtSSSEmployee.Name = "txtSSSEmployee";
            this.txtSSSEmployee.ReadOnly = true;
            this.txtSSSEmployee.Size = new System.Drawing.Size(81, 22);
            this.txtSSSEmployee.TabIndex = 78;
            this.txtSSSEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(501, 165);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 13);
            this.label16.TabIndex = 79;
            this.label16.Text = "PhilHealth";
            // 
            // txtPhilHealthEmployee
            // 
            this.txtPhilHealthEmployee.Location = new System.Drawing.Point(581, 162);
            this.txtPhilHealthEmployee.Name = "txtPhilHealthEmployee";
            this.txtPhilHealthEmployee.ReadOnly = true;
            this.txtPhilHealthEmployee.Size = new System.Drawing.Size(81, 22);
            this.txtPhilHealthEmployee.TabIndex = 80;
            this.txtPhilHealthEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(501, 193);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 13);
            this.label17.TabIndex = 81;
            this.label17.Text = "Pag-Ibig";
            // 
            // txtPAGIBIGEmployee
            // 
            this.txtPAGIBIGEmployee.Location = new System.Drawing.Point(581, 190);
            this.txtPAGIBIGEmployee.Name = "txtPAGIBIGEmployee";
            this.txtPAGIBIGEmployee.ReadOnly = true;
            this.txtPAGIBIGEmployee.Size = new System.Drawing.Size(81, 22);
            this.txtPAGIBIGEmployee.TabIndex = 82;
            this.txtPAGIBIGEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(501, 221);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 13);
            this.label18.TabIndex = 83;
            this.label18.Text = "W/ Tax";
            // 
            // txtWtaxAmount
            // 
            this.txtWtaxAmount.Location = new System.Drawing.Point(581, 218);
            this.txtWtaxAmount.Name = "txtWtaxAmount";
            this.txtWtaxAmount.ReadOnly = true;
            this.txtWtaxAmount.Size = new System.Drawing.Size(81, 22);
            this.txtWtaxAmount.TabIndex = 84;
            this.txtWtaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPAGIBIGEmployer
            // 
            this.txtPAGIBIGEmployer.Location = new System.Drawing.Point(668, 190);
            this.txtPAGIBIGEmployer.Name = "txtPAGIBIGEmployer";
            this.txtPAGIBIGEmployer.ReadOnly = true;
            this.txtPAGIBIGEmployer.Size = new System.Drawing.Size(81, 22);
            this.txtPAGIBIGEmployer.TabIndex = 87;
            this.txtPAGIBIGEmployer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPhilHealthEmployer
            // 
            this.txtPhilHealthEmployer.Location = new System.Drawing.Point(668, 162);
            this.txtPhilHealthEmployer.Name = "txtPhilHealthEmployer";
            this.txtPhilHealthEmployer.ReadOnly = true;
            this.txtPhilHealthEmployer.Size = new System.Drawing.Size(81, 22);
            this.txtPhilHealthEmployer.TabIndex = 86;
            this.txtPhilHealthEmployer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSSSEmployer
            // 
            this.txtSSSEmployer.Location = new System.Drawing.Point(668, 134);
            this.txtSSSEmployer.Name = "txtSSSEmployer";
            this.txtSSSEmployer.ReadOnly = true;
            this.txtSSSEmployer.Size = new System.Drawing.Size(81, 22);
            this.txtSSSEmployer.TabIndex = 85;
            this.txtSSSEmployer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSSSECC
            // 
            this.txtSSSECC.Location = new System.Drawing.Point(755, 134);
            this.txtSSSECC.Name = "txtSSSECC";
            this.txtSSSECC.ReadOnly = true;
            this.txtSSSECC.Size = new System.Drawing.Size(81, 22);
            this.txtSSSECC.TabIndex = 89;
            this.txtSSSECC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtWtaxBaseAmount
            // 
            this.txtWtaxBaseAmount.Location = new System.Drawing.Point(842, 218);
            this.txtWtaxBaseAmount.Name = "txtWtaxBaseAmount";
            this.txtWtaxBaseAmount.ReadOnly = true;
            this.txtWtaxBaseAmount.Size = new System.Drawing.Size(81, 22);
            this.txtWtaxBaseAmount.TabIndex = 96;
            this.txtWtaxBaseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPagibigBaseAmount
            // 
            this.txtPagibigBaseAmount.Location = new System.Drawing.Point(842, 190);
            this.txtPagibigBaseAmount.Name = "txtPagibigBaseAmount";
            this.txtPagibigBaseAmount.ReadOnly = true;
            this.txtPagibigBaseAmount.Size = new System.Drawing.Size(81, 22);
            this.txtPagibigBaseAmount.TabIndex = 95;
            this.txtPagibigBaseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPhiheahltBaseAmount
            // 
            this.txtPhiheahltBaseAmount.Location = new System.Drawing.Point(842, 162);
            this.txtPhiheahltBaseAmount.Name = "txtPhiheahltBaseAmount";
            this.txtPhiheahltBaseAmount.ReadOnly = true;
            this.txtPhiheahltBaseAmount.Size = new System.Drawing.Size(81, 22);
            this.txtPhiheahltBaseAmount.TabIndex = 94;
            this.txtPhiheahltBaseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSSSBaseAmount
            // 
            this.txtSSSBaseAmount.Location = new System.Drawing.Point(842, 134);
            this.txtSSSBaseAmount.Name = "txtSSSBaseAmount";
            this.txtSSSBaseAmount.ReadOnly = true;
            this.txtSSSBaseAmount.Size = new System.Drawing.Size(81, 22);
            this.txtSSSBaseAmount.TabIndex = 93;
            this.txtSSSBaseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(581, 115);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(81, 16);
            this.label19.TabIndex = 97;
            this.label19.Text = "Employee";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(668, 115);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(81, 16);
            this.label20.TabIndex = 98;
            this.label20.Text = "Employer";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(755, 115);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(81, 16);
            this.label21.TabIndex = 99;
            this.label21.Text = "EC";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(842, 98);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(81, 33);
            this.label22.TabIndex = 100;
            this.label22.Text = "Basis of Computation";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRowDelete
            // 
            this.btnRowDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRowDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRowDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRowDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRowDelete.Location = new System.Drawing.Point(842, 350);
            this.btnRowDelete.Name = "btnRowDelete";
            this.btnRowDelete.Size = new System.Drawing.Size(106, 25);
            this.btnRowDelete.TabIndex = 112;
            this.btnRowDelete.Text = "Delete";
            this.btnRowDelete.UseVisualStyleBackColor = true;
            this.btnRowDelete.Click += new System.EventHandler(this.btnRowDelete_Click);
            // 
            // btnRowEdit
            // 
            this.btnRowEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRowEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRowEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRowEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRowEdit.Location = new System.Drawing.Point(842, 319);
            this.btnRowEdit.Name = "btnRowEdit";
            this.btnRowEdit.Size = new System.Drawing.Size(106, 25);
            this.btnRowEdit.TabIndex = 111;
            this.btnRowEdit.Text = "Edit";
            this.btnRowEdit.UseVisualStyleBackColor = true;
            this.btnRowEdit.Click += new System.EventHandler(this.btnRowEdit_Click);
            // 
            // btnRowAdd
            // 
            this.btnRowAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRowAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRowAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRowAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRowAdd.Location = new System.Drawing.Point(842, 288);
            this.btnRowAdd.Name = "btnRowAdd";
            this.btnRowAdd.Size = new System.Drawing.Size(106, 25);
            this.btnRowAdd.TabIndex = 110;
            this.btnRowAdd.Text = "Add";
            this.btnRowAdd.UseVisualStyleBackColor = true;
            this.btnRowAdd.Click += new System.EventHandler(this.btnRowAdd_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(270, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 22);
            this.button1.TabIndex = 113;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboPayrolPeriod
            // 
            this.cboPayrolPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayrolPeriod.FormattingEnabled = true;
            this.cboPayrolPeriod.Location = new System.Drawing.Point(101, 6);
            this.cboPayrolPeriod.Name = "cboPayrolPeriod";
            this.cboPayrolPeriod.Size = new System.Drawing.Size(195, 21);
            this.cboPayrolPeriod.TabIndex = 115;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 9);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(77, 13);
            this.label23.TabIndex = 114;
            this.label23.Text = "Payroll Period";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(12, 179);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(66, 13);
            this.label24.TabIndex = 116;
            this.label24.Text = "Holiday Pay";
            // 
            // txtHolidayPay
            // 
            this.txtHolidayPay.Location = new System.Drawing.Point(116, 176);
            this.txtHolidayPay.Name = "txtHolidayPay";
            this.txtHolidayPay.ReadOnly = true;
            this.txtHolidayPay.Size = new System.Drawing.Size(118, 22);
            this.txtHolidayPay.TabIndex = 117;
            this.txtHolidayPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(12, 263);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(56, 13);
            this.label25.TabIndex = 118;
            this.label25.Text = "Gross Pay";
            // 
            // txtGrossPay
            // 
            this.txtGrossPay.Location = new System.Drawing.Point(116, 260);
            this.txtGrossPay.Name = "txtGrossPay";
            this.txtGrossPay.ReadOnly = true;
            this.txtGrossPay.Size = new System.Drawing.Size(118, 22);
            this.txtGrossPay.TabIndex = 119;
            this.txtGrossPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Image = global::Integra_Console.Properties.Resources.Database_03;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(118, 500);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(106, 25);
            this.btnDelete.TabIndex = 120;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Integra_Console.Properties.Resources.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(731, 500);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 25);
            this.btnCancel.TabIndex = 48;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpload.Image = global::Integra_Console.Properties.Resources.Database_03;
            this.btnUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpload.Location = new System.Drawing.Point(6, 500);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(106, 25);
            this.btnUpload.TabIndex = 47;
            this.btnUpload.Text = "Save";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 121;
            this.label10.Text = "Rate";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(116, 92);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(118, 22);
            this.txtRate.TabIndex = 122;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkLastPay
            // 
            this.chkLastPay.AutoSize = true;
            this.chkLastPay.Location = new System.Drawing.Point(391, 9);
            this.chkLastPay.Name = "chkLastPay";
            this.chkLastPay.Size = new System.Drawing.Size(107, 17);
            this.chkLastPay.TabIndex = 123;
            this.chkLastPay.Text = "Process Last Pay";
            this.chkLastPay.UseVisualStyleBackColor = true;
            this.chkLastPay.Visible = false;
            // 
            // chkHold
            // 
            this.chkHold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHold.AutoSize = true;
            this.chkHold.Location = new System.Drawing.Point(835, 9);
            this.chkHold.Name = "chkHold";
            this.chkHold.Size = new System.Drawing.Size(88, 17);
            this.chkHold.TabIndex = 124;
            this.chkHold.Text = "Hold Payroll";
            this.chkHold.UseVisualStyleBackColor = true;
            // 
            // frmManualPayroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 537);
            this.Controls.Add(this.chkHold);
            this.Controls.Add(this.chkLastPay);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtRate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtGrossPay);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtHolidayPay);
            this.Controls.Add(this.cboPayrolPeriod);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRowDelete);
            this.Controls.Add(this.btnRowEdit);
            this.Controls.Add(this.btnRowAdd);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtWtaxBaseAmount);
            this.Controls.Add(this.txtPagibigBaseAmount);
            this.Controls.Add(this.txtPhiheahltBaseAmount);
            this.Controls.Add(this.txtSSSBaseAmount);
            this.Controls.Add(this.txtSSSECC);
            this.Controls.Add(this.txtPAGIBIGEmployer);
            this.Controls.Add(this.txtPhilHealthEmployer);
            this.Controls.Add(this.txtSSSEmployer);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtWtaxAmount);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtPAGIBIGEmployee);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtPhilHealthEmployee);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtSSSEmployee);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtNetPay);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtTotalDeduction);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtOtherDeduction);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtOtherLoan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtGovLoans);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtOtherIncome);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPaidLeaves);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOTPay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBasicPay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPayrollPeriod);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtEmpCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEmpName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.dgvDisplay);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "frmManualPayroll";
            this.Text = "Manual Payroll";
            this.Load += new System.EventHandler(this.frmManualPayroll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgvDisplay;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtEmpCode;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtEmpName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPayrollPeriod;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtCompany;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtBasicPay;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtOTPay;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtPaidLeaves;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtOtherIncome;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtGovLoans;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtOtherLoan;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox txtOtherDeduction;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox txtTotalDeduction;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox txtNetPay;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TextBox txtSSSEmployee;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.TextBox txtPhilHealthEmployee;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.TextBox txtPAGIBIGEmployee;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.TextBox txtWtaxAmount;
    private System.Windows.Forms.TextBox txtPAGIBIGEmployer;
    private System.Windows.Forms.TextBox txtPhilHealthEmployer;
    private System.Windows.Forms.TextBox txtSSSEmployer;
    private System.Windows.Forms.TextBox txtSSSECC;
    private System.Windows.Forms.TextBox txtWtaxBaseAmount;
    private System.Windows.Forms.TextBox txtPagibigBaseAmount;
    private System.Windows.Forms.TextBox txtPhiheahltBaseAmount;
    private System.Windows.Forms.TextBox txtSSSBaseAmount;
    private System.Windows.Forms.Label label19;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.Button btnRowDelete;
    private System.Windows.Forms.Button btnRowEdit;
    private System.Windows.Forms.Button btnRowAdd;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.ComboBox cboPayrolPeriod;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.TextBox txtHolidayPay;
    private System.Windows.Forms.Label label25;
    private System.Windows.Forms.TextBox txtGrossPay;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox txtRate;
    private System.Windows.Forms.CheckBox chkLastPay;
    private System.Windows.Forms.CheckBox chkHold;
}