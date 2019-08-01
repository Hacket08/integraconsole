using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmDataList : Form
{
    public static string _gListGroup;

    public static string _gCompany;
    public static string _gEmployeeNo;
    public static string _gEmployeeName;
    public static string _gAccountCode;
    public static string _gLoanRefNo;
    public static string _gPayrollPeriod;

    public static string _gBranchCode;
    public static string _gBranchName;
    public static string _gStatus;
    public static string _dataStatus;

    static int _HeaderColumn;

    public frmDataList()
    {
        InitializeComponent();
    }

    private void frmDataList_Load(object sender, EventArgs e)
    {
        chkActive.Visible = false;
        string _sqlList = "";
        DataTable _tblList;

        if (_gStatus == "True")
        {
            _dataStatus = "0";
        }
        else
        {
            _dataStatus = "1";
        }

        switch (_gListGroup)
        {
            case "EmployeeNo":
                _sqlList = @"SELECT B.Company, A.LoanRefNo , B.EmployeeNo, B.EmployeeName
                                    ,C.AccountCode, C.AccountDesc
                                    FROM vwsLoanFile A 
                                    INNER JOIN vwsEmployees B ON A.EmployeeNo COLLATE Latin1_General_CI_AS = B.EmployeeNo
                                    INNER JOIN vwsAccountCode C ON A.AccountCode COLLATE Latin1_General_CI_AS = C.AccountCode
                                    WHERE A.EmployeeNo = '" + _gEmployeeNo + @"' AND A.Status = '" + _dataStatus  + @"'
                                    ORDER BY B.EmployeeName ASC, A.CreateDate DESC ";

                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                if (_tblList.Rows.Count == 1)
                {
                    //dgvDisplay.Rows[0].Selected = true;
                    _gCompany = dgvDisplay.Rows[0].Cells[0].Value.ToString().Trim();
                    _gLoanRefNo = dgvDisplay.Rows[0].Cells[1].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[0].Cells[2].Value.ToString().Trim();
                    _gAccountCode = dgvDisplay.Rows[0].Cells[4].Value.ToString().Trim();
                    Close();
                    return;
                }
                break;
            case "EmployeeName":
                _sqlList = @"SELECT B.Company, A.LoanRefNo , B.EmployeeNo, B.EmployeeName
                                    ,C.AccountCode, C.AccountDesc
                                    FROM vwsLoanFile A 
                                    INNER JOIN vwsEmployees B ON A.EmployeeNo COLLATE Latin1_General_CI_AS = B.EmployeeNo
                                    INNER JOIN vwsAccountCode C ON A.AccountCode COLLATE Latin1_General_CI_AS = C.AccountCode
                                    WHERE B.EmployeeName LIKE '%" + _gEmployeeName + @"%' AND A.Status = '" + _dataStatus + @"'
                                    ORDER BY B.EmployeeName ASC, A.CreateDate DESC ";

                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                if (_tblList.Rows.Count == 1)
                {
                    //dgvDisplay.Rows[0].Selected = true;
                    _gCompany = dgvDisplay.Rows[0].Cells[0].Value.ToString().Trim();
                    _gLoanRefNo = dgvDisplay.Rows[0].Cells[1].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[0].Cells[2].Value.ToString().Trim();
                    _gAccountCode = dgvDisplay.Rows[0].Cells[4].Value.ToString().Trim();
                    Close();
                    return;
                }
                break;
            case "LoanAccountCode":
                _sqlList = @"SELECT B.Company, A.LoanRefNo , B.EmployeeNo, B.EmployeeName
                                    ,C.AccountCode, C.AccountDesc
                                    FROM vwsLoanFile A 
                                    INNER JOIN vwsEmployees B ON A.EmployeeNo COLLATE Latin1_General_CI_AS = B.EmployeeNo
                                    INNER JOIN vwsAccountCode C ON A.AccountCode COLLATE Latin1_General_CI_AS = C.AccountCode
                                    WHERE A.AccountCode = '" + _gAccountCode + @"' AND A.Status = '" + _dataStatus + @"'
                                    ORDER BY B.EmployeeName ASC, A.CreateDate DESC ";

                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                if (_tblList.Rows.Count == 1)
                {
                    //dgvDisplay.Rows[0].Selected = true;
                    _gCompany = dgvDisplay.Rows[0].Cells[0].Value.ToString().Trim();
                    _gLoanRefNo = dgvDisplay.Rows[0].Cells[1].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[0].Cells[2].Value.ToString().Trim();
                    _gAccountCode = dgvDisplay.Rows[0].Cells[4].Value.ToString().Trim();
                    Close();
                    return;
                }
                break;
            case "LoanReferenceNumber":
                _sqlList = @"SELECT B.Company, A.LoanRefNo , B.EmployeeNo, B.EmployeeName
                                    ,C.AccountCode, C.AccountDesc
                                    FROM vwsLoanFile A 
                                    INNER JOIN vwsEmployees B ON A.EmployeeNo COLLATE Latin1_General_CI_AS = B.EmployeeNo
                                    INNER JOIN vwsAccountCode C ON A.AccountCode COLLATE Latin1_General_CI_AS = C.AccountCode
                                    WHERE A.LoanRefNo = '" + _gLoanRefNo + @"' AND A.Status = '" + _dataStatus + @"'
                                    ORDER BY B.EmployeeName ASC, A.CreateDate DESC ";

                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                if (_tblList.Rows.Count == 1)
                {
                    //dgvDisplay.Rows[0].Selected = true;
                    _gCompany = dgvDisplay.Rows[0].Cells[0].Value.ToString().Trim();
                    _gLoanRefNo = dgvDisplay.Rows[0].Cells[1].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[0].Cells[2].Value.ToString().Trim();
                    _gAccountCode = dgvDisplay.Rows[0].Cells[4].Value.ToString().Trim();
                    Close();
                    return;
                }
                break;
            case "PayrollList":
                _sqlList = @"SELECT A.[PayrollPeriod], A.[EmployeeNo], CONCAT(B.LastName, ', ', B.FirstName, ' ', B.MiddleName) COLLATE Latin1_General_CI_AS AS [EmployeeName] 
                                     FROM vwsPayrollHeader A  INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo WHERE A.[PayrollPeriod] = '" + _gPayrollPeriod + @"'";

                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                break;
            case "PayrollEntry":
                _sqlList = @"SELECT A.Company, A.EmployeeNo, B.EmployeeName FROM vwsPayrollEntry A INNER JOIN vwsEmployees B ON A.EmployeeNo = B.EmployeeNo AND A.[PayrollPeriod] = '" + _gPayrollPeriod + @"'";

                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                break;
            case "AccountList2":
                _sqlList = @"
									 SELECT A.AccountCode, A.AccountDesc FROM vwsAccountCode A
                                     UNION ALL
                                     SELECT A.AccountCode, A.Description FROM vwsPayrollRegsCode A WHERE A.AccountCode IN ('004','005','006','007','008','009','010','011')
                                    ";
                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                break;

            case "AccountList1":
                _sqlList = @"SELECT A.AccountCode, A.AccountDesc FROM vwsAccountCode A";
                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                break;
            case "AccountList":
                _sqlList = @"SELECT A.AccountCode, A.AccountDesc FROM vwsAccountCode A WHERE A.[AccountType] IN (7,8)";
                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                break;
            case "EmployeeList":
                _sqlList = @"SELECT A.Company, A.EmployeeNo , A.EmployeeName
                            FROM vwsEmployees A WHERE A.EmpStatus NOT IN (3,4,6)";
                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                break;
            case "EmployeeLoanList":
                _sqlList = @"SELECT DISTINCT B.Company, B.EmployeeNo, B.EmployeeName
                                    FROM vwsLoanFile A 
                                    INNER JOIN vwsEmployees B ON A.EmployeeNo COLLATE Latin1_General_CI_AS = B.EmployeeNo
                                    INNER JOIN vwsAccountCode C ON A.AccountCode COLLATE Latin1_General_CI_AS = C.AccountCode
                                    WHERE A.Status = '0'";
                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                break;
            case "LoanList":
                chkActive.Visible = true;

                _sqlList = @"SELECT B.Company, A.LoanRefNo , B.EmployeeNo, B.EmployeeName
                                    ,C.AccountCode, C.AccountDesc
                                    FROM vwsLoanFile A 
                                    INNER JOIN vwsEmployees B ON A.EmployeeNo COLLATE Latin1_General_CI_AS = B.EmployeeNo
                                    INNER JOIN vwsAccountCode C ON A.AccountCode COLLATE Latin1_General_CI_AS = C.AccountCode
                                    WHERE A.Status = '0'
                                    ORDER BY B.EmployeeName ASC, A.CreateDate DESC";
                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                    break;
            case "BranchList":
                _sqlList = @"SELECT DISTINCT A.BCode , A.BName FROM dbo.[vwsDepartmentList] A ";
                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                break;

        }



    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        _gCompany = "";
        _gEmployeeNo = "";
        _gAccountCode = "";
        _gLoanRefNo = "";
        
        Close();
    }
    private void btnChoose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            _HeaderColumn = e.ColumnIndex;
        }
        catch
        {

        }
    }

    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            switch (_gListGroup)
            {
                case "PayrollList":
                    _gPayrollPeriod = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();

                    break;
                case "PayrollEntry":
                    _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();

                    break;
                case "AccountList2":
                    _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    break;
                case "AccountList1":
                    _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    break;
                case "AccountList":
                    _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    break;

                case "EmployeeLoanList":
                case "EmployeeList":
                    _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();

                    break;
                case "LoanList" : case "EmployeeNo" :  case "EmployeeName" : case "LoanAccountCode" : case "LoanReferenceNumber" :

                    _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gLoanRefNo = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                    _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();


                    break;

                case "BranchList":
                    _gBranchCode = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gBranchName = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();

                    break;
            }



        }
        catch
        {

        }
    }

    private void dgvDisplay_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            switch (_gListGroup)
            {
                case "PayrollList":
                    _gPayrollPeriod = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();

                    break;
                case "AccountList1":
                    _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    break;
                case "AccountList":
                    _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    break;

                case "EmployeeLoanList":
                case "EmployeeList":
                    _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();

                    break;

                case "LoanList":
                case "EmployeeNo":
                case "EmployeeName":
                case "LoanAccountCode":
                case "LoanReferenceNumber":

                    _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _gLoanRefNo = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                    _gEmployeeNo = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                    _gAccountCode = dgvDisplay.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();


                    break;
            }



            Close();
        }
        catch
        {

        }
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        string someText;
        someText = txtSearch.Text;

        int gridRow = 0;
        int gridColumn = _HeaderColumn;


        dgvDisplay.ClearSelection();
        dgvDisplay.CurrentCell = null;

        foreach (DataGridViewRow row in dgvDisplay.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dgvDisplay.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dgvDisplay.Rows[gridRow].Selected = true;
                dgvDisplay.FirstDisplayedScrollingRowIndex = gridRow;

                switch (_gListGroup)
                {
                    case "PayrollList":
                        _gPayrollPeriod = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();
                        _gEmployeeNo = dgvDisplay.Rows[gridRow].Cells[1].Value.ToString().Trim();

                        break;
                    case "AccountList":
                        _gAccountCode = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();
                        break;

                    case "EmployeeLoanList":
                    case "EmployeeList":
                        _gCompany = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();
                        _gEmployeeNo = dgvDisplay.Rows[gridRow].Cells[1].Value.ToString().Trim();

                        break;
                    case "LoanList":

                        _gCompany = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();
                        _gEmployeeNo = dgvDisplay.Rows[gridRow].Cells[1].Value.ToString().Trim();
                        _gAccountCode = dgvDisplay.Rows[gridRow].Cells[3].Value.ToString().Trim();
                        _gLoanRefNo = dgvDisplay.Rows[gridRow].Cells[5].Value.ToString().Trim();

                        break;
                }
                
                return;
            }
            gridRow++;
        }
    }

    private void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        string _sqlList = "";
        DataTable _tblList;

        switch (_gListGroup)
        {
            case "LoanList":
                chkActive.Visible = true;

                if (chkActive.Checked == true)
                {
                    _sqlList = @"SELECT B.Company, B.EmployeeNo, CONCAT(B.LastName, ', ', B.FirstName, ' ', B.MiddleName) AS [EmployeeName]
                                    ,C.AccountCode, C.AccountDesc, A.LoanRefNo 
                                    FROM vwsLoanFile A 
                                    INNER JOIN vwsEmployees B ON A.EmployeeNo COLLATE Latin1_General_CI_AS = B.EmployeeNo
                                    INNER JOIN vwsAccountCode C ON A.AccountCode COLLATE Latin1_General_CI_AS = C.AccountCode
                                    WHERE A.Status = '1'
                                    ORDER BY CONCAT(B.LastName, ', ', B.FirstName, ' ', B.MiddleName)";
                }
                else
                {
                    _sqlList = @"SELECT B.Company, B.EmployeeNo, CONCAT(B.LastName, ', ', B.FirstName, ' ', B.MiddleName) AS [EmployeeName]
                                    ,C.AccountCode, C.AccountDesc, A.LoanRefNo 
                                    FROM vwsLoanFile A 
                                    INNER JOIN vwsEmployees B ON A.EmployeeNo COLLATE Latin1_General_CI_AS = B.EmployeeNo
                                    INNER JOIN vwsAccountCode C ON A.AccountCode COLLATE Latin1_General_CI_AS = C.AccountCode
                                    WHERE A.Status = '0'
                                    ORDER BY CONCAT(B.LastName, ', ', B.FirstName, ' ', B.MiddleName)";
                }


                _tblList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlList);
                clsFunctions.DataGridViewSetup(dgvDisplay, _tblList);
                break;


        }
    }
}