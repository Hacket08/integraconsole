using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmEmployeeMasterData : Form
{
    public static frmGlobalDataList frmGlobalDataList = new frmGlobalDataList();

    public static string _PromotionID;

    public static string _LoanPayrollType;
    public static string _LoanPaymentDate;
    public static string _LoanORNumber;
    public static string _LoanPaymentAmount;
    public static string _LoanRemarks;
    public static string _AddType;
    public static string _Status;

    #region Function Group
    private void ReadOnlyText(bool _value)
    {
        txtCompany.ReadOnly = _value;
        txtEmpCode.ReadOnly = _value;
        txtEmpName.ReadOnly = _value;
    }

    private void ClearData()
    {
        txtEmpCode.Text = "";
        txtEmpName.Text = "";
        txtCompany.Text = "";
    }

    private void LoadGroup()
    {
        PerFormControl(false);

        txtEmpCode.BackColor = SystemColors.Control;
        txtEmpName.BackColor = SystemColors.Control;

        btnSearch.Enabled = true;
        btnNew.Enabled = true;
        btnSave.Enabled = false;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;
    }


    private void SearchGroup()
    {

     
        PerFormControl(false);
        txtEmpCode.ReadOnly = false;
        txtEmpName.ReadOnly = false;
        txtEmpCode.BackColor = Color.LightGoldenrodYellow;
        txtEmpName.BackColor = Color.LightGoldenrodYellow;
        lblSearchEmployeeName.Visible = true;
        lblSearchEmployeeCode.Visible = true;


        btnSearch.Enabled = true;
        btnNew.Enabled = true;
        btnSave.Enabled = false;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;
    }
    private void NewGroup()
    {
        PerFormControl(true);

        txtEmpCode.BackColor = SystemColors.Window;
        txtEmpName.BackColor = SystemColors.Window;



        lblSearchEmployeeCode.Visible = false;
        lblSearchEmployeeName.Visible = false;
        lblPassReset.Visible = false;


        txtBranchCode.ReadOnly = true;
        txtBranchName.ReadOnly = true;
        txtCompany.ReadOnly = true;
        txtCompanyName.ReadOnly = true;
        txtAssignBranchCode.ReadOnly = true;
        txtAssignBranchName.ReadOnly = true;
        txtSchedPass.ReadOnly = true;


        btnSearch.Enabled = false;
        btnNew.Enabled = false;
        btnSave.Enabled = true;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;
    }

    private void SavedGroup()
    {
        btnNew.Enabled = false;
        btnSearch.Enabled = false;
        btnSave.Enabled = true;
        btnDelete.Enabled = true;
        btnCancel.Enabled = true;
    }
    #endregion

    
    private void PerFormControl(bool _value)
    {
        #region Form Control
        int i = 0;
        foreach (Control cLayer1 in this.Controls)
        {
            if (cLayer1 is TextBox)
            {
                cLayer1.Text = "";
                if (_value == true)
                {
                    ((TextBox)cLayer1).ReadOnly = false;
                }
                else
                {
                    ((TextBox)cLayer1).ReadOnly = true;
                }

            }
            if (cLayer1 is ComboBox)
            {
                ((ComboBox)cLayer1).SelectedIndex = 0;
                cLayer1.Enabled = _value;
            }
            if (cLayer1 is Button)
            {
                cLayer1.Enabled = _value;
            }
            if (cLayer1 is DateTimePicker)
            {
                ((DateTimePicker)cLayer1).Value = DateTime.Today;
                cLayer1.Enabled = _value;
            }
            if (cLayer1 is Label)
            {
                if (cLayer1.Name.StartsWith("lbl"))
                {
                    cLayer1.Visible = _value;
                }
            }

            // tab control
            foreach (Control cLayer2 in cLayer1.Controls)
            {
                foreach (Control cLayer3 in cLayer2.Controls)
                {
                    if (cLayer3 is TextBox)
                    {
                        cLayer3.Text = "";
                        if (_value == true)
                        {
                            ((TextBox)cLayer3).ReadOnly = false;
                        }
                        else
                        {
                            ((TextBox)cLayer3).ReadOnly = true;
                        }
                    }

                    if (cLayer3 is ComboBox)
                    {
                        ((ComboBox)cLayer3).SelectedIndex = 0;
                        cLayer3.Enabled = _value;
                    }

                    if (cLayer3 is Button)
                    {
                        cLayer3.Enabled = _value;
                    }
                    if (cLayer3 is DateTimePicker)
                    {
                        ((DateTimePicker)cLayer3).Value = DateTime.Today;
                        cLayer3.Enabled = _value;
                    }
                    if (cLayer3 is CheckBox)
                    {
                        cLayer3.Enabled = _value;
                    }
                    if (cLayer3 is Label)
                    {
                        if (cLayer3.Name.StartsWith("lbl"))
                        {
                            cLayer3.Visible = _value;
                        }
                    }
                }
            }
        }

        i++;
        #endregion
    }

    public frmEmployeeMasterData()
    {
        InitializeComponent();
    }

    private void frmEmployeeMasterData_Load(object sender, EventArgs e)
    {


        string _SQLSyntax = "SELECT A.[StatusCode] FROM [TaxStatus] A";
        DataTable _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cmbTaxStatus.Items.Clear();
        foreach (DataRow row in _DataTable.Rows)
        {
            cmbTaxStatus.Items.Add(row[0].ToString());
        }

        cmbCategory.Items.Clear();
        cmbCategory.Items.Add("Monthly");
        cmbCategory.Items.Add("Daily");
        cmbCategory.Items.Add("Piece-rate");


        cmbPayrollTerms.Items.Clear();
        cmbPayrollTerms.Items.Add("Semi-monthly");
        cmbPayrollTerms.Items.Add("Weekly");
        cmbPayrollTerms.Items.Add("Monthly");

        cmbPayrollMode.Items.Clear();
        cmbPayrollMode.Items.Add("Bank");
        cmbPayrollMode.Items.Add("Cash");


        cmbConfiLevel.Items.Clear();
        cmbConfiLevel.Items.Add("Rank and File");
        cmbConfiLevel.Items.Add("Supervisory");
        cmbConfiLevel.Items.Add("Managerial");

        //cmbTaxStatus.Items.Add("Regular");


        //cmbPosition.Items.Add("Regular");

        cmbCivilStatus.Items.Clear();
        cmbCivilStatus.Items.Add("Single");
        cmbCivilStatus.Items.Add("Married");
        cmbCivilStatus.Items.Add("Widow/er");
        cmbCivilStatus.Items.Add("Separated");


        cmbGender.Items.Clear();
        cmbGender.Items.Add("Male");
        cmbGender.Items.Add("Female");


        cmbEmployeeStatus.Items.Clear();
        cmbEmployeeStatus.Items.Add("Regular");
        cmbEmployeeStatus.Items.Add("Probitionary");
        cmbEmployeeStatus.Items.Add("Contractual");
        cmbEmployeeStatus.Items.Add("Finished Contract");
        cmbEmployeeStatus.Items.Add("Resigned");
        cmbEmployeeStatus.Items.Add("Temporary");
        cmbEmployeeStatus.Items.Add("Terminated");
        cmbEmployeeStatus.Items.Add("AWOL");
        cmbEmployeeStatus.Items.Add("Retired");
        cmbEmployeeStatus.Items.Add("Back - Out");
        
        LoadGroup();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        _AddType = "EDIT";
        SearchGroup();
    }

    private void lblSearchEmployeeCode_Click(object sender, EventArgs e)
    {

        if(txtEmpCode.Text == "")
        {
            clsDeclaration.sDataDisplayQuery = @"
                                                                            SELECT XX.EmployeeNo, XX.EmployeeName, XX.BranchCode, XX.BranchName , XX.Company  FROM (
                                                                                 " + clsSystemQuery._qryEmployeeList() + @"       
                                                                            ) XX        
                                                                          ";
        }
        else
        {
            clsDeclaration.sDataDisplayQuery = @"
                                                                            SELECT XX.EmployeeNo, XX.EmployeeName, XX.BranchCode, XX.BranchName , XX.Company  FROM (
                                                                                 " + clsSystemQuery._qryEmployeeList() + @"       
                                                                            ) XX        
                                                                               WHERE XX.EmployeeNo = '" + txtEmpCode.Text + @"'
                                                                          ";
        }

        string lblTag = ((Label)sender).Tag.ToString();
        frmGlobalDataList._gListGroup = lblTag;
        frmGlobalDataList.ShowDialog();
        NewGroup();
        lblPassReset.Visible = true;

        txtEmpCode.BackColor = SystemColors.Control;
        txtEmpName.BackColor = SystemColors.Control;
        txtDepartment.BackColor = SystemColors.Control;
        txtDepName.BackColor = SystemColors.Control;
        
        txtEmpCode.ReadOnly = true;
        txtEmpName.ReadOnly = true;
        txtDepartment.ReadOnly = true;
        txtDepName.ReadOnly = true;
        txtAge.ReadOnly = true;

        lblBrowseCompany.Visible = false;

        txtCompany.Text = frmGlobalDataList._gCompany;
        txtEmpCode.Text = frmGlobalDataList._gEmployeeNo;

        pvDisplayData();
    }

    private void pvDisplayData()
    {
        _PromotionID = "";

        try
        {
            string _sqlList;
            _sqlList = @"
                                                                            SELECT XX.* FROM (
                                                                                 " + clsSystemQuery._qryEmployeeList() + @"       
                                                                            ) XX        
                                                                               WHERE XX.EmployeeNo = '" + txtEmpCode.Text + @"'
                                                                          ";

            DataTable _tblDispaly = new DataTable();
            _tblDispaly = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);

            txtDepName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "DepName", "0");
            txtEmpCode.Text = clsSQLClientFunctions.GetData(_tblDispaly, "EmployeeNo", "0");
            txtEmpName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "EmployeeName", "0");
            txtLastName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "LastName", "0").Trim();
            txtFirstName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "FirstName", "0").Trim();
            txtMiddleName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "MiddleName", "0").Trim();
            txtMiddleInitial.Text = clsSQLClientFunctions.GetData(_tblDispaly, "MiddleInitial", "0").Trim();
            txtSuffixName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "SuffixName", "0").Trim();
            txtSchedPass.Text = clsSQLClientFunctions.GetData(_tblDispaly, "SchedPass", "0");
            txtCompany.Text = clsSQLClientFunctions.GetData(_tblDispaly, "Company", "0");

            txtParticulars.Text = clsSQLClientFunctions.GetData(_tblDispaly, "Remarks", "0");
            txtdlfLeaveCredit.Text = clsSQLClientFunctions.GetData(_tblDispaly, "dftLeaveCredit", "0");
            txtCompanyName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "CompanyName", "0");

            txtBranchCode.Text = clsSQLClientFunctions.GetData(_tblDispaly, "BCode", "0");
            txtBranchName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "BName", "0");
            txtAssignBranchCode.Text = clsSQLClientFunctions.GetData(_tblDispaly, "AsBranchCode", "0");
            txtPosition.Text = clsSQLClientFunctions.GetData(_tblDispaly, "EmpPosition", "0");

            if (txtAssignBranchCode.Text == "")
            {
                txtAssignBranchCode.Text = clsSQLClientFunctions.GetData(_tblDispaly, "BCode", "0");
                txtAssignBranchName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "BName", "0");
            }
            else
            {
                txtAssignBranchName.Text = clsSQLClientFunctions.GetData(_tblDispaly, "AsBranchName", "0");
            }

            txtDepartment.Text = clsSQLClientFunctions.GetData(_tblDispaly, "Department", "0");

            txtMonthlyRate.Text = clsSQLClientFunctions.GetData(_tblDispaly, "MonthlyRate", "0");
            txtDailyRate.Text = clsSQLClientFunctions.GetData(_tblDispaly, "DailyRate", "0");
            txtRateDivisor.Text = clsSQLClientFunctions.GetData(_tblDispaly, "RateDivisor", "0");
            txtMonthlyAllowance.Text = clsSQLClientFunctions.GetData(_tblDispaly, "MonthlyAllowance", "0");
            txtDailyAllowance.Text = clsSQLClientFunctions.GetData(_tblDispaly, "DailyAllowance", "0");
            txtCOLA.Text = clsSQLClientFunctions.GetData(_tblDispaly, "COLAAmount", "0");


            txtdlfLeaveCredit.Text = clsSQLClientFunctions.GetData(_tblDispaly, "dftLeaveCredit", "0");

            txtBankAccount.Text = clsSQLClientFunctions.GetData(_tblDispaly, "BankAccountNo", "0");
            txtAddress1.Text = clsSQLClientFunctions.GetData(_tblDispaly, "Address01", "0");
            txtAddress2.Text = clsSQLClientFunctions.GetData(_tblDispaly, "Address02", "0");
            txtPhone1.Text = clsSQLClientFunctions.GetData(_tblDispaly, "Telephone01", "0");
            txtPhone2.Text = clsSQLClientFunctions.GetData(_tblDispaly, "Telephone02", "0");

            txtCustomPYCode.Text = clsSQLClientFunctions.GetData(_tblDispaly, "CustomPYCode", "0");




            cmbCategory.SelectedIndex = int.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "Category", "0"));
            cmbPayrollTerms.SelectedIndex = int.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "PayrollTerms", "0"));
            cmbPayrollMode.SelectedIndex = int.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "PayrollMode", "0"));
            cmbEmployeeStatus.SelectedIndex = int.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "EmpStatus", "0"));
            cmbConfiLevel.SelectedIndex = int.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "ConfiLevel", "0"));


            cmbCivilStatus.SelectedIndex = int.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "CivilStatus", "0"));
            cmbGender.SelectedIndex = int.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "Gender", "0"));



            dtDateHired.Value = DateTime.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "DateHired", "0"));
            if(clsSQLClientFunctions.GetData(_tblDispaly, "DateRegular", "0") != "")
            {
                dtDateRegular.Value = DateTime.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "DateRegular", "0"));
            }
            if (clsSQLClientFunctions.GetData(_tblDispaly, "Birthday", "0") != "")
            {
                dtBirthday.Value = DateTime.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "Birthday", "0"));
            }
            if (clsSQLClientFunctions.GetData(_tblDispaly, "DateFinish", "0") != "")
            {
                dtDateFinish.Value = DateTime.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "DateFinish", "0"));
            }
            if (clsSQLClientFunctions.GetData(_tblDispaly, "DateOfClearance", "0") != "")
            {
                dtDateClearance.Value = DateTime.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "DateOfClearance", "0"));
            }

            txtAge.Text = clsFunctions.GetAge(dtBirthday.Value).ToString() + " YRS OLD";
            cmbTaxStatus.Text = clsSQLClientFunctions.GetData(_tblDispaly, "TaxStatus", "0");

            switch (int.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "EmpStatus", "0")))
            {
                case 1:
                    dtDateRegular.Enabled = false;
                    dtDateFinish.Enabled = false;
                    dtDateClearance.Enabled = false;
                    break;
                case 3:
                case 4:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    dtDateFinish.Enabled = true;
                    dtDateClearance.Enabled = true;
                    break;
                default:
                    dtDateFinish.Enabled = false;
                    dtDateClearance.Enabled = false;
                    break;
            }


            //chkSSS.Checked = bool.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "WithSSS", "0"));
            //chkPhilHealth.Checked = bool.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "WithPhilHealth", "0"));
            //chkPagibig.Checked = bool.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "WithPagibig", "0"));
            //chkTAX.Checked = bool.Parse(clsSQLClientFunctions.GetData(_tblDispaly, "WithTIN", "0"));


            //// Convert bool to int.
            //int y = Convert.ToInt32(f);

            chkSSS.Checked = Convert.ToBoolean(Convert.ToInt16(clsSQLClientFunctions.GetData(_tblDispaly, "WithSSS", "0")));
            chkPhilHealth.Checked = Convert.ToBoolean(Convert.ToInt16(clsSQLClientFunctions.GetData(_tblDispaly, "WithPhilHealth", "0")));
            chkPagibig.Checked = Convert.ToBoolean(Convert.ToInt16(clsSQLClientFunctions.GetData(_tblDispaly, "WithPagibig", "0")));
            chkTAX.Checked = Convert.ToBoolean(Convert.ToInt16(clsSQLClientFunctions.GetData(_tblDispaly, "WithTIN", "0")));


            txtSSS.Text = clsSQLClientFunctions.GetData(_tblDispaly, "SSSNo", "0").Trim();
            txtPhilHealth.Text = clsSQLClientFunctions.GetData(_tblDispaly, "PhilHealthNo", "0").Trim();
            txtPagibig.Text = clsSQLClientFunctions.GetData(_tblDispaly, "PagIbigNo", "0").Trim();
            txtTAX.Text = clsSQLClientFunctions.GetData(_tblDispaly, "TaxIDNo", "0").Trim();
            

            _sqlList = @"SELECT A.oID, A.[Date]
                                            ,A.[Position]
                                            ,A.[LeaveAllowed] AS [Leave Credit]
                                        FROM [vwsEmployeePromotion] A
                                        WHERE A.[EmployeeNo]  = '" + txtEmpCode.Text + @"'";
            _tblDispaly = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
            clsFunctions.DataGridViewSetup(dgvDisplayPromotion, _tblDispaly, "Promotion");


            _sqlList = @"SELECT A.AccountCode as [Code], B.AccountDesc AS [Description], A.Amount, A.Freq AS [Frequency], A.Status
                                        FROM [vwsEmployeesRecurring] A INNER JOIN [vwsAccountCode] B ON A.[AccountCode] = B.[AccountCode]
                                        WHERE A.[EmployeeNo]  = '" + txtEmpCode.Text + @"'";
            _tblDispaly = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
            clsFunctions.DataGridViewSetup(dgvDisplayRecurring, _tblDispaly);

        }
        catch
        {

        }
    }

    private void lblPassReset_Click(object sender, EventArgs e)
    {

        DialogResult res = MessageBox.Show("Password will be deleted. Do you want to Proceed??" + "\r" + "This process is irreversable." , "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        if (res == DialogResult.No)
        {
            return;
        }

        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);
        
        string _sqlInsertLeaveDelete = "";
        _sqlInsertLeaveDelete = @"
                                                        UPDATE A SET A.SchedPass = '' FROM Employees A WHERE A.[EmployeeNo] = '" + txtEmpCode.Text + @"'
                                                        ";

        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertLeaveDelete);

        MessageBox.Show("Password Reset.");
        pvDisplayData();
    }



    private void btnDelete_Click(object sender, EventArgs e)
    {

    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        NewGroup();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        if (btnSave.Enabled == true)
        {
            LoadGroup();
        }
        else
        {
            Close();
        }
    }

    private void lblSearchEmployeeName_Click(object sender, EventArgs e)
    {

        clsDeclaration.sDataDisplayQuery = @"
                                                                            SELECT XX.EmployeeNo, XX.EmployeeName, XX.BranchCode, XX.BranchName , XX.Company  FROM (
                                                                                 " + clsSystemQuery._qryEmployeeList() + @"       
                                                                            ) XX        
                                                                               WHERE XX.EmployeeName LIKE '%" + txtEmpName.Text + @"%'
                                                                          ";

        string lblTag = ((Label)sender).Tag.ToString();
        frmGlobalDataList._gListGroup = lblTag;
        frmGlobalDataList.ShowDialog();
        NewGroup();
        lblPassReset.Visible = true;

        txtEmpCode.BackColor = SystemColors.Control;
        txtEmpName.BackColor = SystemColors.Control;
        txtDepartment.BackColor = SystemColors.Control;
        txtDepName.BackColor = SystemColors.Control;

        txtEmpCode.ReadOnly = true;
        txtEmpName.ReadOnly = true;
        txtDepartment.ReadOnly = true;
        txtDepName.ReadOnly = true;
        txtAge.ReadOnly = true;

        lblBrowseCompany.Visible = false;

        txtCompany.Text = frmGlobalDataList._gCompany;
        txtEmpCode.Text = frmGlobalDataList._gEmployeeNo;

        pvDisplayData();
    }

    private void btnPromDelete_Click(object sender, EventArgs e)
    {
        if (_PromotionID == "")
        {
            MessageBox.Show("Please Select Promotion");
            return;
        }

        DialogResult res = MessageBox.Show("Are you sure you want to Delete the Selected Promotion", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        if (res == DialogResult.Cancel)
        {
            return;
        }

        string _ConCompany = clsFunctions.GetCompanyConnectionString(txtCompany.Text);
        string _sqlDelete = "";
        _sqlDelete = "DELETE FROM EmployeePromotion WHERE oID = '" + _PromotionID + "'";
        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlDelete);

        MessageBox.Show("Data Successfully Deleted");
        string _sqlList;
        DataTable _tblDispaly = new DataTable();

        _sqlList = @"SELECT A.oID, A.[Date]
                                            ,A.[Position]
                                            ,A.[LeaveAllowed] AS [Leave Credit]
                                        FROM [vwsEmployeePromotion] A
                                        WHERE A.[EmployeeNo]  = '" + txtEmpCode.Text + @"'";
        _tblDispaly = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplayPromotion, _tblDispaly, "Promotion");
    }

    private void btnPromAdd_Click(object sender, EventArgs e)
    {
        frmEmployeePromotion frmEmployeePromotion = new frmEmployeePromotion();

        frmEmployeePromotion._EmployeeCode = txtEmpCode.Text;
        frmEmployeePromotion._EmployeeName = txtEmpName.Text;
        frmEmployeePromotion._Company = txtCompany.Text;

        frmEmployeePromotion.ShowDialog();
        string _sqlList;
        DataTable _tblDispaly = new DataTable();

        _sqlList = @"SELECT A.oID, A.[Date]
                                            ,A.[Position]
                                            ,A.[LeaveAllowed] AS [Leave Credit]
                                        FROM [vwsEmployeePromotion] A
                                        WHERE A.[EmployeeNo]  = '" + txtEmpCode.Text + @"'";
        _tblDispaly = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
        clsFunctions.DataGridViewSetup(dgvDisplayPromotion, _tblDispaly, "Promotion");
    }

    private void cmbEmployeeStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch(cmbEmployeeStatus.SelectedIndex)
        {
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                dtDateFinish.Enabled = true;
                dtDateClearance.Enabled = true;
                break;
            default:
                dtDateFinish.Enabled = false;
                dtDateClearance.Enabled = false;
                break;
        }
    }

    private void dgvDisplayPromotion_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            _PromotionID = dgvDisplayPromotion.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();

        }
        catch
        {

        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {

    }
}