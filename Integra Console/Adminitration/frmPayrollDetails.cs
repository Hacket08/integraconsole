using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmPayrollDetails : Form
{

    public static string _gAddState;
    public static string _gCompany;
    public static string _gEmployeeNo;
    public static string _gAccountCode;
    public static string _gLoanRefNo;
    public static string _gPayrollPeriod;
    public static string _gAmount;

    public frmPayrollDetails()
    {
        InitializeComponent();
    }

    private void frmPayrollDetails_Load(object sender, EventArgs e)
    {
        checkBox1.Checked = false;
        panel1.Enabled = false;

        string _ConCompany = clsFunctions.GetCompanyConnectionString(_gCompany);
        DataTable _tblDisplay;

        switch (_gAddState)
        {
            case "b_Edit":
                txtLoanAccountCode.Text = _gAccountCode;
                string _sqlList = @"SELECT ISNULL(A.[AccountDesc],(SELECT Z.Description FROM PayrollRegsCode Z WHERE Z.AccountCode = A.[AccountCode])) AS AccountDesc
                                FROM AccountCode A WHERE A.AccountCode  = '" + txtLoanAccountCode.Text + @"'";

                _tblDisplay = clsSQLClientFunctions.DataList(_ConCompany, _sqlList);

                txtDescription.Text = clsSQLClientFunctions.GetData(_tblDisplay, "AccountDesc", "0");
                txtTotalHours.Text = "0.00";
                txtTotalDays.Text = "0.00";
                txtNoOfHrs.Text = "0.00";
                txtNoOfMins.Text = "0.00";
                txtAmount.Text = _gAmount;
                break;
            case "Edit":
                txtLoanAccountCode.Text = _gAccountCode;
                txtLoanRefNo.Text = _gLoanRefNo;
                _ConCompany = clsFunctions.GetCompanyConnectionString(_gCompany);


                //string _sqlList = "";
                //_sqlList = @"SELECT ISNULL(A.[AccountDesc],(SELECT Z.Description FROM PayrollRegsCode Z WHERE Z.AccountCode = A.[AccountCode])) AS AccountDesc
                //                FROM AccountCode A WHERE A.AccountCode  = '" + txtLoanAccountCode.Text + @"'";




                string _sqlSyntax = @"SELECT  A.[AccountCode]
                                               ,ISNULL(B.[AccountDesc],(SELECT Z.Description FROM PayrollRegsCode Z WHERE Z.AccountCode = A.[AccountCode])) AS AccountDesc
                                                ,A.[LoanRefenceNo]
                                                ,A.[NoOfHours]
                                                ,A.[NoOfMins]
                                                ,A.[Amount]
                                                ,A.[TotalHrs]
                                                ,A.[TotalDays]
                                                ,B.AccountType
                                                   ,ISNULL(A.[WithInterest],0) AS [WithInterest]
                                                   ,ISNULL(A.[PrincipalAmt],0) AS [PrincipalAmt]
                                                   ,ISNULL(A.[InterestAmt],0) AS [InterestAmt]
                                        FROM [PayrollDetails] A LEFT JOIN [AccountCode] B ON A.AccountCode = B.AccountCode
                                        WHERE A.EmployeeNo = '" + _gEmployeeNo + @"' AND A.PayrollPeriod = '" + _gPayrollPeriod + @"' 
                                        AND A.[AccountCode] = '" + txtLoanAccountCode.Text.Trim() + @"' AND A.[LoanRefenceNo] = '" + txtLoanRefNo.Text + @"'";


                //DataTable _tblDisplay;
                _tblDisplay = clsSQLClientFunctions.DataList(_ConCompany, _sqlSyntax);

                string _AccountType = clsSQLClientFunctions.GetData(_tblDisplay, "AccountType", "0");

                if (_AccountType == "0" || _AccountType == "1" || _AccountType == "2" || _AccountType == "3" || _AccountType == "4" || _AccountType == "5")
                {
                    txtLoanRefNo.ReadOnly = true;

                    txtTotalHours.ReadOnly = false;
                    txtTotalDays.ReadOnly = false;
                    txtNoOfHrs.ReadOnly = false;
                    txtNoOfMins.ReadOnly = false;
                }
                else
                {
                    txtLoanRefNo.ReadOnly = false;

                    txtTotalHours.ReadOnly = true;
                    txtTotalDays.ReadOnly = true;
                    txtNoOfHrs.ReadOnly = true;
                    txtNoOfMins.ReadOnly = true;
                }

                txtDescription.Text = clsSQLClientFunctions.GetData(_tblDisplay, "AccountDesc", "0");
                txtTotalHours.Text = clsSQLClientFunctions.GetData(_tblDisplay, "TotalHrs", "1");
                txtTotalDays.Text = clsSQLClientFunctions.GetData(_tblDisplay, "TotalDays", "1");
                txtNoOfHrs.Text = clsSQLClientFunctions.GetData(_tblDisplay, "NoOfHours", "1");
                txtNoOfMins.Text = clsSQLClientFunctions.GetData(_tblDisplay, "NoOfMins", "1");
                txtAmount.Text = clsSQLClientFunctions.GetData(_tblDisplay, "Amount", "1");

                checkBox1.Checked = false;
                if (clsSQLClientFunctions.GetData(_tblDisplay, "WithInterest", "1") == "1")
                {
                    checkBox1.Checked = true;
                }

                txtPrincipalAmt.Text = clsSQLClientFunctions.GetData(_tblDisplay, "PrincipalAmt", "1");
                txtInterestAmt.Text = clsSQLClientFunctions.GetData(_tblDisplay, "InterestAmt", "1");




                break;
            default:
                break;

        }


    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_gCompany);
        string _sqlEmployeeInfo = "SELECT A.Department FROM Employees A WHERE A.EmployeeNo = '" + _gEmployeeNo + @"'";
        string _pvDepartment = clsSQLClientFunctions.GetStringValue(_ConCompany, _sqlEmployeeInfo, "Department");



        string _sqlDepartmentCode = "SELECT A.BCode FROM vwsDepartmentList A WHERE A.DepartmentCode = '" + _pvDepartment + @"'";
        string _pvBCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlDepartmentCode, "BCode");

        int _WithInterest = 0;
        if (checkBox1.CheckState == CheckState.Checked)
        {
            _WithInterest = 1;
        }


        string _msg = "";
        string _sqlSyntax = "";
        if (_gAddState == "Edit" || _gAddState == "b_Edit")
        {

            if(_gAddState == "b_Edit")
            {
                if(clsDeclaration.sys13MonthAccount == txtLoanAccountCode.Text.Trim())
                {
                    _sqlSyntax = @"
                UPDATE A
                   SET A.[Amount] = '" + txtAmount.Text + @"'
                 FROM [13MonthDetails] A
                 WHERE A.EmployeeNo = '" + _gEmployeeNo + @"' AND A.Year = '" + _gPayrollPeriod + @"' 
                                        AND A.[AccountCode] = '" + txtLoanAccountCode.Text.Trim() + @"'
                                    ";
                    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlSyntax);
                    _msg = "Data Successfully Updated";
                }
                else
                {
                    _msg = "Invalid Account Please Update Master Setting Contact Your Administrator";
                }

                if (clsDeclaration.sysBonusAccount == txtLoanAccountCode.Text.Trim())
                {
                    _sqlSyntax = @"
                UPDATE A
                   SET A.[Amount] = '" + txtAmount.Text + @"'
                 FROM [PerformanceDetails] A
                 WHERE A.EmployeeNo = '" + _gEmployeeNo + @"' AND A.Year = '" + _gPayrollPeriod + @"' 
                                        AND A.[AccountCode] = '" + txtLoanAccountCode.Text.Trim() + @"'
                                    ";
                    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlSyntax);
                    _msg = "Data Successfully Updated";
                }
                else
                {
                    _msg = "Invalid Account Please Update Master Setting Contact Your Administrator";
                }


            }
            else
            {
                _sqlSyntax = @"
                UPDATE A
                   SET A.[NoOfHours] = '" + txtNoOfHrs.Text + @"'
                      ,A.[Amount] = '" + txtAmount.Text + @"'
                      ,A.[NoOfMins] = '" + txtNoOfMins.Text + @"'
                      ,A.[BillingAmount] = 0.00
                      ,A.[TotalHrs] = '" + txtTotalHours.Text + @"'
                      ,A.[TotalDays] = '" + txtTotalDays.Text + @"'
                      ,A.[WithInterest] = '" + _WithInterest + @"'
                      ,A.[PrincipalAmt] = '" + txtPrincipalAmt.Text + @"'
                      ,A.[InterestAmt] = '" + txtInterestAmt.Text + @"'
                 FROM [PayrollDetails] A
                 WHERE A.EmployeeNo = '" + _gEmployeeNo + @"' AND A.PayrollPeriod = '" + _gPayrollPeriod + @"' 
                                        AND A.[AccountCode] = '" + txtLoanAccountCode.Text.Trim() + @"' AND A.[LoanRefenceNo] = '" + txtLoanRefNo.Text + @"'
                                    ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlSyntax);
                _msg = "Data Successfully Updated";
            }



        }
        else
        {

            _sqlSyntax = @"
                                        INSERT INTO [dbo].[PayrollDetails]
                                                   ([PayrollPeriod]
                                                   ,[EmployeeNo]
                                                   ,[AccountCode]
                                                   ,[LoanRefenceNo]
                                                   ,[NoOfHours]
                                                   ,[Amount]
                                                   ,[NoOfMins]
                                                   ,[BillingAmount]
                                                   ,[TotalHrs]
                                                   ,[TotalDays]
                                                   ,[Branch]
                                                   ,[Department]
                                                   ,[WithInterest]
                                                   ,[PrincipalAmt]
                                                   ,[InterestAmt])
                                             VALUES
                                                   ('" + _gPayrollPeriod + @"' 
                                                   ,'" + _gEmployeeNo + @"' 
                                                   ,'" + txtLoanAccountCode.Text.Trim() + @"' 
                                                   , '" + txtLoanRefNo.Text + @"'
                                                   , '" + txtNoOfHrs.Text + @"'
                                                   ,'" + txtAmount.Text + @"'
                                                   ,'" + txtNoOfMins.Text + @"'
                                                   ,0.00
                                                   ,'" + txtTotalHours.Text + @"' 
                                                   ,'" + txtTotalDays.Text + @"'
                                                   ,'" + _pvBCode + @"'
                                                   ,'" + _pvDepartment + @"'
                                                   ,'" + _WithInterest + @"'
                                                   ,'" + txtPrincipalAmt.Text + @"'
                                                   ,'" + txtInterestAmt.Text + @"'  )
                                    ";

            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlSyntax);
            _msg = "Data Successfully Added";
        }
        MessageBox.Show(_msg);
        Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        frmDataList frmDataList = new frmDataList();
        frmDataList._gListGroup = "AccountList2";
        frmDataList.ShowDialog();

        txtLoanAccountCode.Text = frmDataList._gAccountCode;

        string _sqlList = "";
        _sqlList = @"SELECT A.AccountDesc
                            FROM (
									 SELECT A.AccountCode, A.AccountDesc FROM vwsAccountCode A
                                     UNION ALL
                                     SELECT A.AccountCode, A.Description FROM vwsPayrollRegsCode A WHERE A.AccountCode IN ('004','005','006','007','008','009','010','011')
) A WHERE A.AccountCode  = '" + txtLoanAccountCode.Text + @"'";
        txtDescription.Text = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlList, "AccountDesc");

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void txt_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (char.IsDigit(e.KeyChar))
        {
            return;
        }
        if (e.KeyChar == (char)Keys.Back)
        {
            return;
        }
        if (e.KeyChar == '.')
        {
            return;
        }
        if (e.KeyChar == '-')
        {
            return;
        }

        e.Handled = true;
    }

    private void txtNoOfHrs_TextChanged(object sender, EventArgs e)
    {

    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox1.CheckState == CheckState.Checked)
        {
            panel1.Enabled = true;
            txtAmount.ReadOnly = true;

            txtPrincipalAmt.Text = "0.00";
            txtInterestAmt.Text = "0.00";
        }
        else
        {
            panel1.Enabled = false;
            txtAmount.ReadOnly = false;

            txtPrincipalAmt.Text = "0.00";
            txtInterestAmt.Text = "0.00";
        }
    }

    private void txtPrincipalAmt_TextChanged(object sender, EventArgs e)
    {
        double _principalAmt = 0;
        double.TryParse(txtPrincipalAmt.Text, out _principalAmt);

        txtInterestAmt.Text = (double.Parse(txtAmount.Text) - _principalAmt).ToString("0.00");
    }


    public void TimeCalculate(string _TimeRecord, string _timeFormat)
    {

        string s;
        string[] parts;
        double i1;
        double i2;

        string _NoOfHrs = "0";
        string _NoOfMins = "0";

        string _TotalHours = "0";
        string _TotalDays = "0";


        double _ValueWH = 0;

        if (_TimeRecord != "")
        {
            double _TimeValue = 0;
            double.TryParse(_TimeRecord, out _TimeValue);
            _ValueWH = _TimeValue;

            switch (_timeFormat)
            {
                case "d":
                    s = (_ValueWH * 8).ToString("0.00000");
                    parts = s.Split('.');
                    i1 = double.Parse(parts[0]);
                    i2 = double.Parse("." + parts[1]) * 60;

                    _NoOfHrs = i1.ToString();
                    _NoOfMins = i2.ToString();
                    _TotalHours = (_ValueWH * 8).ToString("0.00000");
                    _TotalDays = _ValueWH.ToString();

                    break;
                case "h":
                    s = (_ValueWH).ToString("0.00000");
                    parts = s.Split('.');
                    i1 = double.Parse(parts[0]);
                    i2 = double.Parse("." + parts[1]) * 60;

                    _NoOfHrs = i1.ToString();
                    _NoOfMins = i2.ToString();

                    _TotalHours = _ValueWH.ToString();
                    _TotalDays = (_ValueWH / 8).ToString("0.00000");

                    break;
                case "m":
                    s = (_ValueWH / 60).ToString("0.00000");
                    parts = s.Split('.');
                    i1 = double.Parse(parts[0]);
                    i2 = double.Parse("." + parts[1]) * 60;

                    _NoOfHrs = i1.ToString();
                    _NoOfMins = i2.ToString();

                    _TotalHours = (_ValueWH / 60).ToString("0.00000");
                    _TotalDays = ((_ValueWH / 60) / 8).ToString("0.00000");
                    break;
            }


            //'" + _NoOfHrs + @"',
            //'" + _NoOfMins + @"',
            //'" + _TotalHours + @"',
            //'" + _TotalDays + @"'


            txtNoOfHrs.Text = _NoOfHrs;
            txtNoOfMins.Text = _NoOfMins;
            txtTotalHours.Text = _TotalHours;
            txtTotalDays.Text = _TotalDays;



            string _sqlEmployeeInfo = "SELECT A.DailyRate FROM vwsEmployees A WHERE A.EmployeeNo = '" + _gEmployeeNo + @"'";
            string _DailyRate = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlEmployeeInfo, "DailyRate");

            double _EERate = 0;
            double.TryParse(_DailyRate, out _EERate);
            txtAmount.Text = (_EERate * double.Parse(_TotalDays)).ToString("N2");
        }

    }

    private void txtTotalDays_Leave(object sender, EventArgs e)
    {
        TimeCalculate(txtTotalDays.Text, "d");
    }

    private void txtTotalHours_Leave(object sender, EventArgs e)
    {
        TimeCalculate(txtTotalHours.Text, "h");
    }

    private void txtTotalDays_TextChanged(object sender, EventArgs e)
    {

    }
}