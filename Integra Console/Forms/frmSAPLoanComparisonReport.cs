using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
public partial class frmSAPLoanComparisonReport : Form
{
    private static DataTable _DataList = new DataTable();

    public static string _gCompany;
    public static string _gEmpCode;
    public static string _gEmpName;
    public static string _gPayrollPeriod;


    public static DateTime _PayrollDate;
    public static DateTime _FirstDay;
    public frmSAPLoanComparisonReport()
    {
        InitializeComponent();
    }

    private void frmSAPLoanComparisonReport_Load(object sender, EventArgs e)
    {
        //btnUpload.Enabled = false;

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = @"
                             SELECT * FROM OUSR A WHERE A.[USERID] = '" + clsDeclaration.sLoginUserID + @"'
                      ";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        string _RankAndFile = clsSQLClientFunctions.GetData(_DataTable, "RankAndFile", "0");
        string _Supervisor = clsSQLClientFunctions.GetData(_DataTable, "Supervisor", "0");
        string _Manager = clsSQLClientFunctions.GetData(_DataTable, "Manager", "0");

        //_SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        //cboArea.Items.Clear();
        //cboArea.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboArea.Items.Add(row[0].ToString());
        //}

    }

    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataTable _DataTable;
        //string _SQLSyntax;

        //_SQLSyntax = "SELECT DISTINCT CONCAT(A.BCode,' - ',A.BName) AS Branch FROM dbo.[vwsDepartmentList] A WHERE A.Area = '" + cboArea.Text + "'";
        //_DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        //cboBranch.Items.Clear();
        //cboBranch.Items.Add("");
        //foreach (DataRow row in _DataTable.Rows)
        //{
        //    cboBranch.Items.Add(row[0].ToString());
        //}
    }

    private void button1_Click(object sender, EventArgs e)
    {

        //string _Category = "";
        //if (rbMonthly.Checked == true)
        //{
        //    _Category = "0";
        //}
        //else if (rbDaily.Checked == true)
        //{
        //    _Category = "1";
        //}

        
        MessageBox.Show("Data Ready To Upload");
    }
    
    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnReProcess_Click(object sender, EventArgs e)
    {
        
    }

    private void openInManualPayrollToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            _gEmpCode = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            _gEmpName = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
        }
        catch
        {

        }
    }

    private void dgvDisplay_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvDisplay.ClearSelection();
                dgvDisplay.Rows[e.RowIndex].Selected = true;

                _gCompany = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                _gEmpCode = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                _gEmpName = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            }
        }
        catch
        {

        }
    }

    private void updatePayrollTimeRecordToolStripMenuItem_Click(object sender, EventArgs e)
    {

        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(frmPayrollTransaction))
            {
                form.Activate();
                return;
            }
        }

        frmPayrollTransaction frmPayrollTransaction = new frmPayrollTransaction();
        frmPayrollTransaction.MdiParent = ((frmMainWindow)(this.MdiParent));

        frmPayrollTransaction._gCompany = _gCompany;
        frmPayrollTransaction._gEmpCode = _gEmpCode;
        frmPayrollTransaction._gEmpName = _gEmpName;
        frmPayrollTransaction.Show();
    }

    private void cboPayrolPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        //        string _BCode = "";
        //        if (cboBranch.Text == "")
        //        {
        //            _BCode = "";
        //        }
        //        else
        //        {
        //            _BCode = cboBranch.Text.Substring(0, 8);
        //        }

        //        string _sqlPayrollDisplay = "";
        //        _sqlPayrollDisplay = @"
        //                                      SELECT A.EmployeeNo , B.EmployeeName, A.AccountCode , A.LoanRefNo, A.AmountGranted , A.LoanInterest , A.LoanAmount 
        //, 0.00 AS [Loan Balance]

        //, A.SAPBPCode
        //, A.SAPDocEntry
        //, 0.00 AS [SAP Amount]
        //, 0.00 AS [SAP Balance]

        //FROM vwsLoanFile A INNER JOIN vwsEmployeeDetails B ON A.EmployeeNo = B.EmployeeNo
        //WHERE A.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')

        //                                                    ";

        //        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        //        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);

        //        //double parsedValue;
        //        double _RowCount;
        //        int _Count = 0;
        //        _RowCount = _DataList.Rows.Count;

        //        foreach (DataRow row in _DataList.Rows)
        //        {
        //            Application.DoEvents();

        //            //string _EmployeeNo = row["EmployeeNo"].ToString();
        //            //string _LoanRefNo = row["LoanRefNo"].ToString();
        //            //string _Company = row["Company"].ToString();
        //            //string _AccountCode = row["AccountCode"].ToString();

        //            //string _sqlSAPData = "SELECT DISTINCT Z.CardCode FROM [OCRD] Z WHERE Z.AddID = '" + _EmployeeNo + @"'";
        //            //string _CardCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "CardCode");

        //            //_sqlSAPData = @"SELECT  Z.NumAtCard,  Z.CardCode, MAX(Z.DocEntry) AS DocEntry,  COUNT(Z.DocEntry) AS [ARCount]
        //            //                FROM 
        //            //                (SELECT 
        //	           //                 A.DocNum
        //	           //                 , A.DocEntry
        //	           //                 , A.NumAtCard
        //	           //                 , A.CardCode
        //	           //                 , A.DocTotal
        //	           //                 , A.PaidToDate
        //	           //                 , ISNULL((	SELECT SUM(DOCTOTAL) 
        //		          //                  FROM ORIN X 
        //			         //                   INNER JOIN RIN1 XA ON X.DocEntry = XA.DocEntry 
        //		          //                  WHERE XA.BaseEntry = A.DocEntry AND XA.BaseType = A.ObjType AND X.CANCELED = 'N'),0) [CM AMT]
        //            //                FROM OINV A
        //            //                WHERE A.CANCELED = 'N') Z
        //            //                WHERE (Z.DOCTOTAL - Z.[CM AMT]) <> 0
        //            //                AND Z.NumAtCard  = '" + _LoanRefNo + @"' AND Z.CardCode = '" + _CardCode + @"' 
        //            //                GROUP BY Z.NumAtCard,  Z.CardCode
        //            //                ORDER BY COUNT(Z.DocEntry) DESC
        //            //                ";

        //            //string _DocEntry = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "DocEntry");
        //            //int _ARCount = int.Parse(clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlSAPData, "ARCount").ToString());




        //            //// Excel Progress Monitoring
        //            //Application.DoEvents();
        //            //_Count++;

        //            ////frmMainWindow frmMainWindow = new frmMainWindow();
        //            //tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeNo + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
        //            //pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        //        }

        //        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        //        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);


        //        btnUpload.Enabled = true;


        DataDisplay();
        MessageBox.Show("Payroll Loan Processing Complete");
    }


    public void DataDisplay()
    {
        string _BCode = "";
        if (cboBranch.Text == "")
        {
            _BCode = "";
        }
        else
        {
            _BCode = cboBranch.Text.Substring(0, 8);
        }

        string _sqlPayrollDisplay = "";
        _sqlPayrollDisplay = @"
   SELECT C.EmployeeNo, C.EmployeeName, B.AccountDesc, F.AccountCode, F.LoanRefNo
                                , F.SAPBPCode AS CardCode 
                                , F.SAPDocEntry AS DocEntry 
                                , C.Company
                                , C.BCode
                                , F.RelativeName 

                                , '' AS [SAP Date]
                                , 0.00 AS [AR DownPayment]
                                , 0.00 AS [AR Amount]
                                , 0.00 AS [Applied Amount]
                                , 0.00 AS [AR Balance]
                                , 0.00 AS [JE Amount]

                                , F.LoanDate
                                , F.LoanAmount
                                , F.DownPayment

                                , 0.00 AS [Cash Payment]
                                , 0.00 AS [Payroll Payment]

                                , 0.00 AS [Uploaded Payroll Amount]
                                , 0.00 AS [Pending Payroll Amount]


                                FROM [vwsLoanFile] F 
                                INNER JOIN [vwsEmployeeDetails] C ON F.EmployeeNo = C.EmployeeNo
                                INNER JOIN [vwsAccountCode] B ON F.AccountCode = B.AccountCode

                                  WHERE 
                                  B.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519')
AND ISNULL(F.SAPDocEntry , '') <> ''
                                                    ";

        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);

        double _RowCount;
        int _Count = 0;
        _RowCount = _DataList.Rows.Count;

        foreach (DataRow row in _DataList.Rows)
        {
            Application.DoEvents();


            string _EmployeeNo = row["EmployeeNo"].ToString();
            string _AccountCode = row["AccountCode"].ToString();
            string _LoanRefNo = row["LoanRefNo"].ToString();

            string _CardCode = row["CardCode"].ToString();
            string _DocEntry = row["DocEntry"].ToString().Trim();

            string _Company = row["Company"].ToString();


            string _sqlData = "";

            if (_CardCode != "" && _DocEntry != "")
            {
                _sqlData = @"SELECT A.DocEntry,A.DocDate,A.U_DP,A.DocTotal,(A.DocTotal - A.PaidToDate) AS Balance   FROM [OINV] A WHERE A.DocEntry = '" + _DocEntry + "' AND A.CardCode = '" + _CardCode + "' ";


                string _dateValue = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlData, "DocDate");
                if (_dateValue != "")
                {
                    row["SAP Date"] = DateTime.Parse(_dateValue).ToShortDateString();
                }


                //row["SAP Date"] = DateTime.Parse(clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlData, "DocDate")).ToShortDateString();
                row["AR DownPayment"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "U_DP").ToString("N2");
                row["AR Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "DocTotal").ToString("N2");
                row["AR Balance"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "Balance").ToString("N2");
                row["DocEntry"] = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlData, "DocEntry");

                _sqlData = @"SELECT SUM(A.SumApplied) AS SumApplied FROM [RCT2] A WHERE A.DocEntry = '" + _DocEntry + "'  AND InvType = '13'";
                row["Applied Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "SumApplied").ToString("N2");

                _sqlData = @"SELECT ISNULL(A.Debit,0) AS [Amount] FROM JDT1 A 
                                        INNER JOIN OJDT B ON A.TransId = B.TransId 
                                        WHERE A.ShortName = '" + _CardCode + @"'
                                        AND B.U_JETYPE = 'JE Adjustment' AND B.U_oID_OINV = '" + _DocEntry + "'";
                row["JE Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlData, "Amount").ToString("N2");

            }




            _sqlData = @"  SELECT SUM(X.Amount)  AS Amount FROM vwsLoanPayment X WHERE X.Type <> 'PAYROLL' AND
                           X.EmployeeNo = '" + _EmployeeNo + "' AND X.AccountCode = '" + _AccountCode + "' AND X.LoanRefNo = '" + _LoanRefNo + "'";
            row["Cash Payment"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");

            _sqlData = @"  SELECT SUM(CASE WHEN X.WithInterest = 1 THEN X.PrincipalAmt ELSE X.Amount END)  AS Amount FROM vwsPayrollDetails X WHERE
                           X.EmployeeNo = '" + _EmployeeNo + "' AND X.AccountCode = '" + _AccountCode + "' AND X.LoanRefenceNo = '" + _LoanRefNo + "'";
            row["Payroll Payment"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");




            _sqlData = @"  SELECT SUM(CASE WHEN X.WithInterest = 1 THEN X.PrincipalAmt ELSE X.Amount END)  AS Amount FROM vwsPayrollDetails X WHERE X.Uploaded = 'Y' AND
                           X.EmployeeNo = '" + _EmployeeNo + "' AND X.AccountCode = '" + _AccountCode + "' AND X.LoanRefenceNo = '" + _LoanRefNo + "'";
            row["Uploaded Payroll Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");

            _sqlData = @"  SELECT SUM(CASE WHEN X.WithInterest = 1 THEN X.PrincipalAmt ELSE X.Amount END)  AS Amount FROM vwsPayrollDetails X WHERE X.Uploaded = 'N' AND
                           X.EmployeeNo = '" + _EmployeeNo + "' AND X.AccountCode = '" + _AccountCode + "' AND X.LoanRefenceNo = '" + _LoanRefNo + "'";
            row["Pending Payroll Amount"] = clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSystemConnection, _sqlData, "Amount").ToString("N2");


            Application.DoEvents();
            _Count++;

            //frmMainWindow frmMainWindow = new frmMainWindow();
            tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeNo + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }
    }

    private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void button1_Click_1(object sender, EventArgs e)
    {

    }

    private void btnCheckingDisplay_Click(object sender, EventArgs e)
    {

    }
}
