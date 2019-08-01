using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
public partial class frmSAPEmployeeAging : Form
{
    private static DataTable _DataList = new DataTable();

    public static string _gCompany;
    public static string _gEmpCode;
    public static string _gEmpName;
    public static string _gPayrollPeriod;


    public static DateTime _PayrollDate;
    public static DateTime _FirstDay;
    public frmSAPEmployeeAging()
    {
        InitializeComponent();
    }

    private void frmSAPEmployeeAging_Load(object sender, EventArgs e)
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
                                DECLARE @DATEF DATE, @BPFROM VARCHAR(50), @BPTO VARCHAR(50), @BPTYPE VARCHAR(1), @BRANCH VARCHAR(25)         
                                         SELECT @DATEF = GETDATE(), @BPFROM = 'CAAAA00000', @BPTO = 'CZZZZ99999'
                                         , @BPTYPE = 'C', @BRANCH = ' & cmb_branch.Text & '
    
                                             DECLARE @P1 date,@P2 date,@P3 nvarchar(254),@P4 nvarchar(254),@P5 nvarchar(254) 
                                                 ,@P6 date,@P7 nvarchar(254),@P8 date,@P9 date,@P10 nvarchar(254),@P11 nvarchar(254)         
                                                 ,@P12 nvarchar(254),@P13 date,@P14 nvarchar(254),@P15 date,@P16 date,@P17 nvarchar(254)         
                                                 ,@P18 nvarchar(254),@P19 nvarchar(254),@P20 date
        
                                             SELECT @P1=@DATEF,@P2=@DATEF,@P3=@BPTYPE,@P4=@BPFROM, @P5= @BPTO,@P6=@DATEF,@P7=@BPTYPE,@P8=@DATEF,@P9=@DATEF,@P10=@BPTYPE  
                                                 ,@P11=@BPFROM,@P12=@BPTO,@P13=@DATEF,@P14='D',@P15=@DATEF,@P16=@DATEF,@P17=@BPTYPE,@P18=@BPFROM,@P19=@BPTO,@P20=@DATEF          
        
                                   SELECT
                                        , A.CARDNAME
		                                , A.NUMATCARD
                                                 --, ISNULL((SELECT MODEL FROM DBO.[usrFN_PenaltyReference] (@DATEF) X WHERE X.TransId = A.TransId ), '') MODEL           
                                                 , A.TAXDATE [RELEASED DATE]   
				                                 , A.TRANSTYPE 
				                                 , A.TransId
                                                 --, (SELECT DISTINCT TERMS FROM DBO.[usrFN_PenaltyReference] (@DATEF) X WHERE X.TransId = A.TransId )   TERMS     
                                                 --, (SELECT DISTINCT [SALES PRICE]   FROM DBO.[usrFN_PenaltyReference] (@DATEF) X WHERE X.TransId = A.TransId )   [SALES PRICE]           
                                                 --, (SELECT DISTINCT DOWNPAYMENT   FROM DBO.[usrFN_PenaltyReference] (@DATEF) X WHERE X.TransId = A.TransId ) DOWNPAYMENT    

                                             FROM [dbo].[usrFN_DBSI_AGING] (@P1 ,@P2 ,@P3 ,@P4 ,@P5 ,@P6 ,@P7 ,@P8 ,@P9 ,@P10 ,@P11 ,@P12 ,@P13 ,@P14 ,@P15 ,@P16 ,@P17 ,@P18 ,@P19 ,@P20  ) A   
                                             WHERE A.TRANSTYPE NOT IN (30)
	                                GROUP BY A.CARDNAME, A.NUMATCARD, A.TAXDATE, A.TransId  , A.TRANSTYPE                                 
                               ";

        _DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPayrollDisplay);
        clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);











        ////double parsedValue;
        //double _RowCount;
        //int _Count = 0;
        //_RowCount = _DataList.Rows.Count;

        //foreach (DataRow row in _DataList.Rows)
        //{
        //    Application.DoEvents();

        //    string _EmployeeNo = row["EmployeeNo"].ToString();
        //    string _LoanRefNo = row["LoanRefNo"].ToString();
        //    string _Company = row["Company"].ToString();
        //    string _AccountCode = row["AccountCode"].ToString();

        //    string _sqlSAPData = "SELECT DISTINCT Z.CardCode FROM [OCRD] Z WHERE Z.AddID = '" + _EmployeeNo + @"'";
        //    string _CardCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "CardCode");

        //    _sqlSAPData = @"SELECT  Z.NumAtCard,  Z.CardCode, MAX(Z.DocEntry) AS DocEntry,  COUNT(Z.DocEntry) AS [ARCount]
        //                    FROM 
        //                    (SELECT 
	       //                     A.DocNum
	       //                     , A.DocEntry
	       //                     , A.NumAtCard
	       //                     , A.CardCode
	       //                     , A.DocTotal
	       //                     , A.PaidToDate
	       //                     , ISNULL((	SELECT SUM(DOCTOTAL) 
		      //                      FROM ORIN X 
			     //                       INNER JOIN RIN1 XA ON X.DocEntry = XA.DocEntry 
		      //                      WHERE XA.BaseEntry = A.DocEntry AND XA.BaseType = A.ObjType AND X.CANCELED = 'N'),0) [CM AMT]
        //                    FROM OINV A
        //                    WHERE A.CANCELED = 'N') Z
        //                    WHERE (Z.DOCTOTAL - Z.[CM AMT]) <> 0
        //                    AND Z.NumAtCard  = '" + _LoanRefNo + @"' AND Z.CardCode = '" + _CardCode + @"' 
        //                    GROUP BY Z.NumAtCard,  Z.CardCode
        //                    ORDER BY COUNT(Z.DocEntry) DESC
        //                    ";

        //    string _DocEntry = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "DocEntry");
        //    int _ARCount = int.Parse(clsSQLClientFunctions.GetNumericValue(clsDeclaration.sSAPConnection, _sqlSAPData, "ARCount").ToString());

        //    // Excel Progress Monitoring
        //    Application.DoEvents();
        //    _Count++;

        //    //frmMainWindow frmMainWindow = new frmMainWindow();
        //    tssDataStatus.Text = "Payroll Data Uploading: (" + _Count + " / " + _RowCount + ") : " + _EmployeeNo + "  : Data Progress ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) ";
        //    pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        //}

        //_DataList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlPayrollDisplay);
        //clsFunctions.DataGridViewSetup(dgvDisplay, _DataList);

        MessageBox.Show("Payroll Loan Processing Complete");
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
