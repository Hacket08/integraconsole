using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.Data;

using System.Configuration;

using System.Data.SqlClient;
using System.Text.RegularExpressions;

class clsSAPFunctions
{
    public static SAPbobsCOM.Company oCompany;


    public static SAPbobsCOM.Company SAPConnection(string dftDBName, string SAPUser, string SAPPass, out bool isConnected, out string _ErrorMsg)
    {
        int lRetCode;
        SAPbobsCOM.Company newCompany;
        newCompany = new SAPbobsCOM.Company();

        newCompany.LicenseServer = ConfigurationManager.AppSettings["sysLicenseServer"];

        newCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
        newCompany.Server = ConfigurationManager.AppSettings["sysDBServer"];
        newCompany.DbUserName = ConfigurationManager.AppSettings["sysDBUsername"];
        newCompany.DbPassword = ConfigurationManager.AppSettings["sysDBPassword"];

        //oCompany.CompanyDB = ConfigurationManager.AppSettings["sysDftDBCompany"];
        //oCompany.UserName = ConfigurationManager.AppSettings["sysSAPUsername"];
        //oCompany.Password = ConfigurationManager.AppSettings["sysSAPPassword"];

        newCompany.CompanyDB = dftDBName;
        newCompany.UserName = SAPUser;
        newCompany.Password = SAPPass;

        newCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;


        lRetCode = newCompany.Connect();
        //DIErrorHandler(lRetCode, "Connecting To SAP", "SAP Connection");

        string sErrMsg;
        int lErrCode;

        if (lRetCode != 0)
        {
            newCompany.GetLastError(out lErrCode, out sErrMsg);
            _ErrorMsg = lErrCode + " " + sErrMsg;
            isConnected = false;
        }
        else
        {
            _ErrorMsg = "Conneted To SAP";
            isConnected = true;
        }

        return newCompany;
        //return 
    }

    
    public static bool DIErrorHandler(int lRetCode, string Action, string MsgTitle)
    {
        string sErrMsg;
        int lErrCode;

        if (lRetCode != 0)
        {
            oCompany.GetLastError(out lErrCode, out sErrMsg);
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool JEAllocation(string _Branch, string _PayrolPeriod)
    {

        string sysDftDBCompany = ConfigurationManager.AppSettings["sysDftDBCompany"];
        string sysDBUsername = ConfigurationManager.AppSettings["sysSAPUsername"];
        string sysDBPassword = ConfigurationManager.AppSettings["sysSAPPassword"];

        bool isConnected = false;
        string _Msg = "";
        oCompany = SAPConnection(sysDftDBCompany, sysDBUsername, sysDBPassword, out isConnected, out _Msg );
        MessageBox.Show(_Msg);
        if (isConnected == true)
        {

            string _sqlSelect;
            DataTable _tblSelect;

            _sqlSelect = @"
                                        SELECT A.Account, A.Credit, A.Debit, A.AccountName, A.EmployeeName, A.DocDate
                                        FROM [dbo].[fnSAPTransaction]('" + _Branch + @"','" + _PayrolPeriod + @"') A
                                    ";
            _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);

            string _DocDate = clsSQLClientFunctions.GetData(_tblSelect, "DocDate", "0");


            SAPbobsCOM.JournalEntries _JournalEntries;
            _JournalEntries = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);

            _JournalEntries.ReferenceDate = DateTime.Parse(_DocDate);
            _JournalEntries.TaxDate = DateTime.Parse(_DocDate);
            _JournalEntries.DueDate = DateTime.Parse(_DocDate);
            _JournalEntries.Memo = "PAYROLL PERIOD " + _PayrolPeriod + @" FOR " + _Branch;
            _JournalEntries.UserFields.Fields.Item("U_NSAPADVISENO").Value = _PayrolPeriod;


            foreach (DataRow row in _tblSelect.Rows)
            {
                {
                    string _Account = row["Account"].ToString();
                    string _Credit = row["Credit"].ToString();
                    string _Debit = row["Debit"].ToString();
                    string _AccountName = row["AccountName"].ToString();
                    string _EmployeeName = row["EmployeeName"].ToString();

                    if (_Account.Substring(0,1) == "V")
                    {
                        _JournalEntries.Lines.ShortName = _Account;
                    }
                    else
                    {
                        _JournalEntries.Lines.AccountCode = _Account;
                    }
        
                    _JournalEntries.Lines.LineMemo = _AccountName;

                    _JournalEntries.Lines.Credit = double.Parse(_Credit);
                    _JournalEntries.Lines.Debit = double.Parse(_Debit);
                    _JournalEntries.Lines.UserFields.Fields.Item("U_NSAPADVISENO").Value = _PayrolPeriod;
                    _JournalEntries.Lines.UserFields.Fields.Item("U_EMPLOYEE").Value = _EmployeeName;
                    _JournalEntries.Lines.Add();
                }
            }


            int lRetCode;
            string sErrMsg;
            int lErrCode;
            Application.DoEvents();
            lRetCode = _JournalEntries.Add();

            if (lRetCode != 0)
            {
                oCompany.GetLastError(out lErrCode, out sErrMsg);
                MessageBox.Show(lErrCode + " " + sErrMsg);
                return false;
            }
            else
            {
                MessageBox.Show("Payroll Successfully Posted");
                return true;
            }

            //oCompany.Disconnect();
        }
        else
        {
            return false;
        }



    }

    public static bool CreateBranchJournalEntry(string _Branch, string _PayrolPeriod, DateTime _DocDate, DataTable _tblSelect, bool isConnected)
    {

        if (isConnected == true)
        {

            SAPbobsCOM.JournalEntries _JournalEntries;
            _JournalEntries = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);

            _JournalEntries.ReferenceDate = _DocDate;
            _JournalEntries.TaxDate = _DocDate;
            _JournalEntries.DueDate = _DocDate;
            _JournalEntries.Memo = _Branch + " | Payroll Period : " + _PayrolPeriod;
            _JournalEntries.UserFields.Fields.Item("U_NSAPADVISENO").Value = _PayrolPeriod;
            _JournalEntries.UserFields.Fields.Item("U_Branch").Value = _Branch;

            string _sqlSAPData = "SELECT A.AcctCode FROM OACT A WHERE A.AccntntCod = '" + _Branch + @"'";
            string _SAPDBCAcctCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "AcctCode");

            foreach (DataRow row in _tblSelect.Rows)
            {
                {
                    string _Account = row["JEACCT"].ToString();
                    string _Credit = row["Credit"].ToString();
                    string _Debit = row["Debit"].ToString();
                    string _AccountName = row["ACCOUNTNAME"].ToString();
                    string _EmployeeName = row["EmployeeName"].ToString();
                    string _SUDENTRY = row["SUDENTRY"].ToString();



                    if (_Account.Substring(0, 1) == "V")
                    {
                        _JournalEntries.Lines.ShortName = _Account;
                    }
                    else
                    {
                        _JournalEntries.Lines.AccountCode = _Account;
                    }


                    _JournalEntries.Lines.LineMemo = _Branch + " | Payroll Period : " + _PayrolPeriod;

                    _JournalEntries.Lines.Credit = double.Parse(_Credit);
                    _JournalEntries.Lines.Debit = double.Parse(_Debit);
                    //_JournalEntries.Lines.UserFields.Fields.Item("U_Branch").Value = _Branch;
                    _JournalEntries.Lines.UserFields.Fields.Item("U_NSAPADVISENO").Value = _PayrolPeriod;
                    _JournalEntries.Lines.UserFields.Fields.Item("U_EMPLOYEE").Value = _EmployeeName;
                    _JournalEntries.Lines.UserFields.Fields.Item("U_AcctType").Value = "CoA";
                    _JournalEntries.Lines.UserFields.Fields.Item("U_ItemGroup").Value = _AccountName;

                    if (_Account != "131152")
                    {
                        _JournalEntries.Lines.Reference2 = _AccountName;
                    }

                    if (_SAPDBCAcctCode == _Account)
                    {
                        if (_SUDENTRY == "")
                        {
                            _SUDENTRY = "601034";
                        }
                        _JournalEntries.Lines.UserFields.Fields.Item("U_API_Vendor").Value = _SUDENTRY;

                        _sqlSAPData = "SELECT A.AcctName FROM OACT A WHERE A.AcctCode = '" + _SUDENTRY + @"'";
                        string _AcctName = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "AcctName");
                        _JournalEntries.Lines.UserFields.Fields.Item("U_API_Address").Value = _AcctName;
                    }
                    else
                    {
        
                    }




                    _JournalEntries.Lines.Add();
                }
            }


            int lRetCode;
            string sErrMsg;
            int lErrCode;
            Application.DoEvents();
            lRetCode = _JournalEntries.Add();

            if (lRetCode != 0)
            {
                oCompany.GetLastError(out lErrCode, out sErrMsg);
                //MessageBox.Show(lErrCode + " " + sErrMsg);

                clsLogWriter.LogWriter(_Branch, Microsoft.VisualBasic.Strings.Left(_PayrolPeriod, 4) , lErrCode + " " + sErrMsg);
                return false;
            }
            else
            {
                //MessageBox.Show("Payroll Successfully Posted");

                clsLogWriter.LogWriter(_Branch, Microsoft.VisualBasic.Strings.Left(_PayrolPeriod, 4) , _Branch + " Payroll Successfully Posted : " + _PayrolPeriod);
                return true;
            }

            //oCompany.Disconnect();
        }
        else
        {
            return false;
        }
    }

    public static void CreateIncomingPayment(string _Branch , int _DocEntry
        , string _CardCode, string _CardName, string _CashAccount , DateTime _DocDate, string _EmployeeNo, string _AccountCode, string _ReferenceNo
        , string _Company , bool isConnected)
    {
        
        DateTime _PayrollDate;
        DateTime _FirstDay;

        if (isConnected == true)
        {
            string _GetLoanPaymentAmount = @"
                                                SELECT XX.PayrollPeriod, XX.Amount FROM 
                                                (
                                                SELECT 
                                                A.PayrollPeriod,
                                                (CASE WHEN ISNULL(A.WithInterest, 0) = 0 THEN A.Amount ELSE A.PrincipalAmt END)

                                                AS Amount 
                                                ,A.LoanRefenceNo, B.BranchAccount , B.StartOfPosting
                                                ,
                                                CASE WHEN  B.BranchAccount = 1 THEN 

	                                                CASE WHEN B.PostToSAP = 1 THEN 
			                                                CASE WHEN 
					                                                (A.PayrollPeriod < (CASE WHEN B.PostToSAP = 1 THEN B.StartOfPosting ELSE A.PayrollPeriod END)) 
					                                                THEN 
					                                                'BC'
			                                                ELSE 
					                                                'LS' 
			                                                END
	                                                ELSE 
			                                                'BC'
	                                                END

                                                ELSE 'LS' END AS [Type]


                                                FROM vwsPayrollDetails A INNER JOIN vwsLoanFile B on  A.EmployeeNo = B.EmployeeNo AND A.AccountCode = B.AccountCode AND A.LoanRefenceNo = B.LoanRefNo
                                                INNER JOIN vwsPayrollHeader C on A.EmployeeNo = C.EmployeeNo AND A.PayrollPeriod = C.PayrollPeriod
                                                                                                WHERE
												                                                A.EmployeeNo = '" + _EmployeeNo + @"' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _ReferenceNo + @"'  
                                                                                                AND (A.Uploaded = 'N' OR A.Uploaded IS NULL) 
				                                                                                AND A.Amount <> 0
                                                                                                AND C.PayrollType = 'PAYROLL'
                                                ) XX
                                                WHERE XX.Type = 'LS' AND XX.Amount <> 0
                                                ORDER BY XX.PayrollPeriod
                                            ";

            //string _GetLoanPaymentAmount = @"
            //                                SELECT A.PayrollPeriod, (CASE WHEN ISNULL(A.WithInterest, 0) = 0 THEN A.Amount ELSE A.PrincipalAmt END) AS Amount 
            //                                FROM vwsPayrollDetails A 
            //                                INNER JOIN vwsLoanFile B ON A.EmployeeNo = B.EmployeeNo 
            //                                        AND A.AccountCode = B.AccountCode 
            //                                        AND A.LoanRefenceNo = B.LoanRefNo
            //                                WHERE 
            //	A.EmployeeNo = '" + _EmployeeNo + @"' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _ReferenceNo + @"'  
            //                                        AND (A.Uploaded = 'N' OR A.Uploaded IS NULL)
            //                                        AND A.Amount <> 0
            //	AND A.PayrollPeriod >= (CASE WHEN B.BranchAccount = 1 THEN (CASE WHEN B.PostToSAP = 1 THEN B.StartOfPosting ELSE A.PayrollPeriod END) ELSE A.PayrollPeriod END)
            //                                ORDER BY A.PayrollPeriod
            //                                ";

            DataTable _tblPayments = new DataTable();
            _tblPayments = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _GetLoanPaymentAmount);
            foreach (DataRow rPayment in _tblPayments.Rows)
            {

                string _vAmountValue = rPayment["Amount"].ToString();
                string _vPayrollPeriod = rPayment["PayrollPeriod"].ToString();


                string _Year = Microsoft.VisualBasic.Strings.Left(_vPayrollPeriod, 4);
                string _Month = Microsoft.VisualBasic.Strings.Mid(_vPayrollPeriod, 6, 2);
                string _DayTag = Microsoft.VisualBasic.Strings.Right(_vPayrollPeriod, 1);

                _FirstDay = DateTime.Parse(_Month + "/1/" + _Year);
                if (_DayTag == "A")
                {
                    _PayrollDate = DateTime.Parse(_FirstDay.AddDays(14).ToString("MM/dd/yyyy"));
                }
                else
                {
                    if (_Month == "02")
                    {
                        _PayrollDate = DateTime.Parse(_FirstDay.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        _PayrollDate = DateTime.Parse(_FirstDay.AddDays(29).ToString("MM/dd/yyyy"));
                    }

                }


                string _splPaymentChecking = "";
                DataTable _tblPaymentChecking = new DataTable();

                _splPaymentChecking = @"
                                        SELECT A.DocNum FROM ORCT A WHERE A.Canceled = 'N' AND A.U_DocNum = '" + _vPayrollPeriod + @"' AND A.U_AR = '" + _ReferenceNo + @"'
                                                        AND A.CardCode = '" + _CardCode + @"'
                                      ";

                _tblPaymentChecking = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _splPaymentChecking);


                if (_tblPaymentChecking.Rows.Count == 0)
                {
                    #region Payment Uplodaing

                    string _sqlRebate = @"SELECT A.Amount FROM vwsLoanPayment A WHERE A.Type = 'REBATE' 
                                        AND A.ORNo LIKE '%" + _vPayrollPeriod + @"%' AND A.EmployeeNo = '" + _EmployeeNo + @"' 
                                        AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefNo = '" + _ReferenceNo + @"'";
                    DataTable _tblRebate = new DataTable();
                    _tblRebate = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlRebate);
                    double _Rebate = 0.00;
                    double.TryParse(clsSQLClientFunctions.GetData(_tblRebate, "Amount", "0"), out _Rebate);



                    string _sqlPayrollDisplay = "";
                    DataTable _tblData = new DataTable();

                    _sqlPayrollDisplay = @"
                                        SELECT  'TRUE' FROM OINV A WHERE A.DOCENTRY  = '" + _DocEntry + @"' AND A.DocStatus = 'O'
                                      ";

                    _tblData = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPayrollDisplay);
                    if (_tblData.Rows.Count > 0)
                    {
                        #region LOAN RC Transaction
                        SAPbobsCOM.Payments _Payments;
                        _Payments = (SAPbobsCOM.Payments)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);


                        _Payments.CardCode = _CardCode;
                        _Payments.CardName = _CardName;
                        _Payments.CashAccount = _CashAccount;
                        //_Payments.CashSum = double.Parse(_vAmountValue);
                        _Payments.DocDate = _PayrollDate;
                        _Payments.Remarks = "Auto Created By Add-on : " + _vPayrollPeriod + @" Branch : " + _Branch + " | RF# |" + _ReferenceNo;
                        _Payments.JournalRemarks = Microsoft.VisualBasic.Strings.Left(_Branch + " | Payroll Period : " + _vPayrollPeriod + "|" + _ReferenceNo, 50);
                        _Payments.CounterReference = Microsoft.VisualBasic.Strings.Right(_ReferenceNo, 8);
                        _Payments.UserFields.Fields.Item("U_Branch").Value = _Branch;
                        _Payments.UserFields.Fields.Item("U_DocNum").Value = _vPayrollPeriod;
                        _Payments.UserFields.Fields.Item("U_AR").Value = _ReferenceNo;
                        _Payments.TaxDate = _PayrollDate;



                        _sqlPayrollDisplay = @"
                                        SELECT A.InstlmntID, (A.InsTotal - A.PaidToDate) AS [Balance], 13 AS [ObjType], A.DocEntry 
                                        FROM INV6 A WHERE A.DOCENTRY  = '" + _DocEntry + @"' AND A.Status = 'O'
                                      ";
                        _tblData = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPayrollDisplay);


                        double _CurrentAmount = double.Parse(_vAmountValue) + _Rebate;
                        double _CurrentRebate = _Rebate;
                        double _ApplyRebateAmt = 0;
                        double _ApplyAmt = 0;
                        //bool _rebateApply = false;


                        double _SumAppliedAmount = 0;
                        foreach (DataRow row in _tblData.Rows)
                        {
                            string _ObjType = row["ObjType"].ToString();
                            string _getDocEntry = row["DocEntry"].ToString();

                            _Payments.Invoices.DocEntry = int.Parse(_getDocEntry);
                            //_Payments.Invoices.DocLine = 0;
                            _Payments.Invoices.InvoiceType = (SAPbobsCOM.BoRcptInvTypes)int.Parse(_ObjType);

                            string _Balance = row["Balance"].ToString();
                            string _InstID = row["InstlmntID"].ToString();
                            _Payments.Invoices.InstallmentId = int.Parse(_InstID);


                            if (double.Parse(_Balance) >= _CurrentRebate)
                            {
                                _ApplyRebateAmt = _CurrentRebate;
                                _CurrentRebate = _CurrentRebate - _ApplyRebateAmt;
                            }
                            else
                            {
                                _ApplyRebateAmt = double.Parse(_Balance);
                                _CurrentRebate = _CurrentRebate - _ApplyRebateAmt;
                            }


                            if (double.Parse(_Balance) >= _CurrentAmount)
                            {
                                _ApplyAmt = _CurrentAmount;
                            }
                            else
                            {
                                _ApplyAmt = double.Parse(_Balance);
                            }


                            _CurrentAmount = double.Parse((_CurrentAmount - _ApplyAmt).ToString("N2"));
                            _Payments.Invoices.TotalDiscount = _ApplyRebateAmt;
                            _Payments.Invoices.SumApplied = double.Parse((_ApplyAmt - _ApplyRebateAmt).ToString("N2"));

                            _SumAppliedAmount = double.Parse((_SumAppliedAmount + (_ApplyAmt - _ApplyRebateAmt)).ToString("N2"));
                            _Payments.Invoices.Add();

                            if (_CurrentAmount == 0)
                            {
                                break;
                            }
                        }



                        _Payments.CashSum = _SumAppliedAmount;


                        int lRetCode;
                        string sErrMsg;
                        int lErrCode;
                        Application.DoEvents();
                        lRetCode = _Payments.Add();

                        if (lRetCode != 0)
                        {
                            oCompany.GetLastError(out lErrCode, out sErrMsg);
                            MessageBox.Show(lErrCode + " " + sErrMsg + " EMPLOYEE : " + _CardName + " PAYROLL PERIOD : " + _vPayrollPeriod + " LOAN REFERENCE NO : " + _ReferenceNo + " Amount : " + _vAmountValue);

                            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                            string _SQLSyntax = @"UPDATE A SET A.[SAPError] = '" + (lErrCode + " " + sErrMsg).Replace("'", "''") + @"'  
                                            FROM PayrollDetails A  WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.PayrollPeriod = '" + _vPayrollPeriod + @"' 
                                            AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _ReferenceNo + @"'
                                         ";

                            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);

                            break;
                        }
                        else
                        {
                            //MessageBox.Show("Payroll Successfully Posted");

                            double _OverPaymentAmount = 0;
                            _OverPaymentAmount = double.Parse(_vAmountValue) - _SumAppliedAmount;




                            string _SQLSyntax = @"SELECT A.DocNum FROM ORCT A WHERE A.Canceled = 'N' AND A.U_DocNum = '" + _vPayrollPeriod + @"' AND A.U_AR = '" + _ReferenceNo + @"' AND  A.CardCode = '" + _CardCode + @"'";
                            DataTable _dt = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _SQLSyntax);
                            string _RCDocnum = clsSQLClientFunctions.GetData(_dt, "DocNum", "0");



                            string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                            _SQLSyntax = @"UPDATE A SET A.[Uploaded] = 'Y', A.[SAPError] = '' , A.DateUploaded = GETDATE() 
                                                 , A.PaymentBalance = '" + _OverPaymentAmount + @"'
                                                 , A.RCDocNum = '" + _RCDocnum + @"'
                                            FROM PayrollDetails A  WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.PayrollPeriod = '" + _vPayrollPeriod + @"' 
                                            AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _ReferenceNo + @"'
                                         ";

                            clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);
                        }




                        #endregion
                    }

                    #endregion
                }
                else
                {
                    string _SQLSyntax = @"SELECT A.DocNum FROM ORCT A WHERE A.Canceled = 'N' AND A.U_DocNum = '" + _vPayrollPeriod + @"' AND A.U_AR = '" + _ReferenceNo + @"' AND  A.CardCode = '" + _CardCode + @"'";
                    DataTable _dt = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _SQLSyntax);
                    string _RCDocnum = clsSQLClientFunctions.GetData(_dt, "DocNum", "0");


                    string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                    _SQLSyntax = @"UPDATE A SET A.[Uploaded] = 'Y', A.[SAPError] = 'Data Already Uploaded' , A.DateUploaded = GETDATE() 
                                                 , A.RCDocNum = '" + _RCDocnum + @"'
                                            FROM PayrollDetails A  WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.PayrollPeriod = '" + _vPayrollPeriod + @"' 
                                            AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _ReferenceNo + @"'
                                         ";

                    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);
                }
            }
        }

    }
    

    public static void CreateARInvoice(DateTime _LoanDate, DateTime _FirstDueDate, string _CardCode, string _EmployeeNo
        , string _EmployeeName, string _SAPAccount, string _AccountCode , string _AccountName, string _LoanRefNo
        , double _LoanAmount, string _Branch, string _Brand, int _Terms, int _Months, int _IsTransfered, string _TranferredDate, string _Company, bool isConnected)
    {
        if (isConnected == true)
        {
            SAPbobsCOM.Documents _ARInvoice;
            _ARInvoice = (SAPbobsCOM.Documents)clsSAPFunctions.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);



            if (_IsTransfered == 1)
            {
                _ARInvoice.DocDate = DateTime.Parse(_TranferredDate);
            }
            else
            {
                _ARInvoice.DocDate = DateTime.Parse(_LoanDate.ToString("yyyy/MM/dd HH:mm:ss"));
            }


            _ARInvoice.TaxDate = DateTime.Parse(_FirstDueDate.ToString("yyyy/MM/dd HH:mm:ss"));

            _ARInvoice.CardCode = _CardCode;

            _ARInvoice.NumAtCard = _LoanRefNo;
            _ARInvoice.UserFields.Fields.Item("U_Branch").Value = _Branch;
            _ARInvoice.Comments = "Auto Created By Add-on | Employee Name : " + _EmployeeName + "  | Account : " + _AccountCode + " | Loan Reference Number  : " + _LoanRefNo ;
            _ARInvoice.JournalMemo = "Loan Ref: " + _LoanRefNo + @" | " + _AccountName.ToUpper();
            _ARInvoice.PaymentGroupCode = _Terms;




            string _syn = @"SELECT * FROM vwsLoanFile A WHERE A.AccountCode  IN ('8-510','8-511','8-514','8-515','8-517','8-518','8-519') AND ISNULL(A.SAPDocEntry,'') = ''
                                                AND A.EmployeeNo = '" + _EmployeeNo + "'  AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefNo = '" + _LoanRefNo + @"' ";
            DataTable _dt = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _syn);


            //string _RCDocnum = clsSQLClientFunctions.GetData(_dt, "DocNum", "0");


            _ARInvoice.UserFields.Fields.Item("U_CustName").Value = _EmployeeName;

            double _MonthlyAmount = double.Parse(clsSQLClientFunctions.GetData(_dt, "Amortization", "0")) * 2;
            _ARInvoice.UserFields.Fields.Item("U_REBATE").Value = clsSQLClientFunctions.GetData(_dt, "Rebate", "0");
            _ARInvoice.UserFields.Fields.Item("U_MA").Value = _MonthlyAmount;

            int _loanTerms = 0;
            int.TryParse(clsSQLClientFunctions.GetData(_dt, "Terms", "0"), out _loanTerms);

            _ARInvoice.UserFields.Fields.Item("U_TERM").Value = clsSQLClientFunctions.GetData(_dt, "Terms", "0");
            _ARInvoice.UserFields.Fields.Item("U_Remarks").Value = clsSQLClientFunctions.GetData(_dt, "Particular", "0");
            _ARInvoice.UserFields.Fields.Item("U_LCP").Value = clsSQLClientFunctions.GetData(_dt, "LCPPrice", "0");
            _ARInvoice.UserFields.Fields.Item("U_DP").Value = clsSQLClientFunctions.GetData(_dt, "DownPayment", "0");
            _ARInvoice.UserFields.Fields.Item("U_RelDate").Value = DateTime.Parse(_LoanDate.ToString("yyyy/MM/dd HH:mm:ss"));
            _ARInvoice.UserFields.Fields.Item("U_TYPE").Value = "Employee";

            _ARInvoice.UserFields.Fields.Item("U_GSP").Value = clsSQLClientFunctions.GetData(_dt, "AmountGranted", "0");

            double _GetDP = 0;
            double.TryParse(clsSQLClientFunctions.GetData(_dt, "DownPayment", "0"), out _GetDP);

            _ARInvoice.UserFields.Fields.Item("U_TATBA").Value = double.Parse(clsSQLClientFunctions.GetData(_dt, "AmountGranted", "0")) - _GetDP;


            _syn = @"SELECT A.LASTNAME, A.FIRSTNAME, A.MIDDLENAME FROM vwsEmployees A WHERE  A.EmployeeNo = '" + _EmployeeNo + "'";
            _dt = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _syn);

            _ARInvoice.UserFields.Fields.Item("U_LASTNAME").Value = clsSQLClientFunctions.GetData(_dt, "LASTNAME", "0");
            _ARInvoice.UserFields.Fields.Item("U_FIRSTNAME").Value = clsSQLClientFunctions.GetData(_dt, "FIRSTNAME", "0");
            _ARInvoice.UserFields.Fields.Item("U_MIDDLENAME").Value = clsSQLClientFunctions.GetData(_dt, "MIDDLENAME", "0");

            string ItemCategory = "";

            switch (_AccountCode)
            {
                case "8-510":
                    ItemCategory = "APPLIANCE";
                    break;
                case "8-511":
                    ItemCategory = "MOTORCYCLE";
                    break;
                case "8-517":
                    ItemCategory = "POWERPRODUCTS";
                    break;
                case "8-514":
                    ItemCategory = "FURNITURE";
                    break;
                case "8-518":
                    ItemCategory = "COMPUTER";
                    break;
                case "8-515":
                    ItemCategory = "CELLULAR";
                    break;
                case "8-519":
                    ItemCategory = "SPGENUINE";
                    break;
            }

            _ARInvoice.UserFields.Fields.Item("U_ItemGroup").Value = ItemCategory;


            _ARInvoice.Lines.ItemCode = ItemCategory;
            _ARInvoice.Lines.ItemDescription = _AccountName;
            _ARInvoice.Lines.Quantity = 1;
            _ARInvoice.Lines.PriceAfterVAT = _LoanAmount;
            _ARInvoice.Lines.AccountCode = _SAPAccount;
            _ARInvoice.Lines.UserFields.Fields.Item("U_Branch").Value = _Branch;
            _ARInvoice.Lines.UserFields.Fields.Item("U_ADDDESCRIPTION").Value = "Loan Ref: " + _LoanRefNo + @" | " + ItemCategory.ToUpper();


            _ARInvoice.Lines.UserFields.Fields.Item("U_SPOTCASH").Value = clsSQLClientFunctions.GetData(_dt, "SpotCashAmount", "0");

            if (_Terms == -1)
            {
                _ARInvoice.NumberOfInstallments = _Months;
            }


            double _totalMonthly = 0;
            double _AmountBalance = 0;

            int i = 0;
            do
            {
                _AmountBalance = double.Parse((_LoanAmount - _totalMonthly).ToString("N2"));
                if (_AmountBalance == 0)
                {
                    break;
                }

                if (i == 0)
                {
                    _ARInvoice.Installments.DueDate = _FirstDueDate;
                }
                else
                {
                    _ARInvoice.Installments.DueDate = _FirstDueDate.AddMonths(i);
                }

                if (_AmountBalance < _MonthlyAmount)
                {
                    _MonthlyAmount = _AmountBalance;
                    _ARInvoice.Installments.Total = _MonthlyAmount;
                }
                else
                {
                    _ARInvoice.Installments.Total = _MonthlyAmount;
                }

                _ARInvoice.Installments.Add();
                _totalMonthly = _totalMonthly + _MonthlyAmount;

            
                i++;

            } while (_AmountBalance != 0);


            //for (int i = 0; i < _loanTerms; i++)
            //{
            //    _AmountBalance = double.Parse((_LoanAmount - _totalMonthly).ToString("N2"));

            //    if (_AmountBalance == 0)
            //    {
            //        break;
            //    }

            //    if (i == 0)
            //    {
            //        _ARInvoice.Installments.DueDate = _FirstDueDate;
            //    }
            //    else
            //    {
            //        _ARInvoice.Installments.DueDate = _FirstDueDate.AddMonths(i);
            //    }



            //    if (_AmountBalance < _MonthlyAmount)
            //    {
            //        _MonthlyAmount = _AmountBalance;
            //        _ARInvoice.Installments.Total = _MonthlyAmount;
            //    }
            //    else
            //    {
            //        _ARInvoice.Installments.Total = _MonthlyAmount;
            //    }

            //    _ARInvoice.Installments.Add();
            //    _totalMonthly = _totalMonthly + _MonthlyAmount;
            //}



            int lRetCode;
            string sErrMsg;
            int lErrCode;
            Application.DoEvents();
            lRetCode = _ARInvoice.Add();

            if (lRetCode != 0)
            {
                oCompany.GetLastError(out lErrCode, out sErrMsg);
                //MessageBox.Show(lErrCode + " " + sErrMsg);

                string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                string _SQLSyntax = @"UPDATE A SET A.SAPBPCode = '" + _CardCode + @"', A.[SAPError] = '" + (lErrCode + " " + sErrMsg).Replace("'", "''") + "'  FROM LoanFile A  WHERE A.EmployeeNo = '" + _EmployeeNo + "'  AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefNo = '" + _LoanRefNo + @"'
              ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);
            }
            else
            {
                //MessageBox.Show("Payroll Successfully Posted");

                _syn = @"SELECT A.DocEntry FROM OINV A WHERE A.Canceled = 'N' AND A.U_Branch = '" + _Branch + @"' AND A.NumAtCard = '" + _LoanRefNo + @"' AND  A.CardCode = '" + _CardCode + @"'";
                _dt = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _syn);
                string _INV_DocEntry = clsSQLClientFunctions.GetData(_dt, "DocEntry", "0");


                string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                string _SQLSyntax = @"UPDATE A SET A.SAPBPCode = '" + _CardCode + @"', A.SAPDocEntry = '" + _INV_DocEntry + @"' , A.[SAPError] = '' FROM LoanFile A  WHERE A.EmployeeNo = '" + _EmployeeNo + "'  AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefNo = '" + _LoanRefNo + @"'
                                ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);
            }

        }
    }


    public static void CreateBP(string _CardCode, string _EmployeeNo , string _EmployeeName, string _AccountCode, string _LoanRefNo, string _Company, bool isConnected)
    {
        if (isConnected == true)
        {
            SAPbobsCOM.BusinessPartners _BusinessPartners;
            _BusinessPartners = (SAPbobsCOM.BusinessPartners)clsSAPFunctions.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);


            _BusinessPartners.CardCode = _CardCode;
            _BusinessPartners.CardName = _EmployeeName;
            _BusinessPartners.CardType = SAPbobsCOM.BoCardTypes.cCustomer;
            _BusinessPartners.GroupCode = 105;
            _BusinessPartners.AdditionalID = _EmployeeNo;
            _BusinessPartners.DebitorAccount = "103012";
            _BusinessPartners.Notes = "BP Created by Addon";

            int lRetCode;
            string sErrMsg;
            int lErrCode;
            Application.DoEvents();
            lRetCode = _BusinessPartners.Add();

            if (lRetCode != 0)
            {
                oCompany.GetLastError(out lErrCode, out sErrMsg);
                //MessageBox.Show(lErrCode + " " + sErrMsg);


                string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                string _SQLSyntax = @"UPDATE A SET A.SAPBPCode = '" + _CardCode + @"', A.[SAPError] = '" + (lErrCode + " " + sErrMsg).Replace("'", "''") + "'  FROM LoanFile A  WHERE A.EmployeeNo = '" + _EmployeeNo + "'  AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefNo = '" + _LoanRefNo + @"'
              ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);
            }
            else
            {
                //MessageBox.Show("Payroll Successfully Posted");

                string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                string _SQLSyntax = @"UPDATE A SET A.SAPBPCode = '" + _CardCode + @"', A.[SAPError] = ''  FROM LoanFile A  WHERE A.EmployeeNo = '" + _EmployeeNo + "'  AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefNo = '" + _LoanRefNo + @"'
              ";

                clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);
            }
        }

 


    }

    public static void OverPaymentJE(string _Branch, string _EmployeeNo, string _EmployeeName, string _AccountCode, string _ReferenceNo, string _CardCode, string _DocEntry, string _Company, bool isConnected)
    {

        DateTime _PayrollDate;
        DateTime _FirstDay;


        if (isConnected == true)
        {
            string _GetLoanPaymentAmount = @"
                                                SELECT A.PayrollPeriod, ((CASE WHEN ISNULL(A.Uploaded ,'N') = 'Y' THEN 0 ELSE (CASE WHEN ISNULL(A.WithInterest, 0) = 0 THEN A.Amount ELSE A.PrincipalAmt END) END)  + 
				                                        CASE WHEN ISNULL(A.OPPosted ,'0') = '1' THEN 0 ELSE ISNULL([PaymentBalance], 0) END
				                                        )  AS Amount
                                                FROM vwsPayrollDetails A 
                                                INNER JOIN vwsLoanFile B ON A.EmployeeNo = B.EmployeeNo 
                                                        AND A.AccountCode = B.AccountCode 
                                                        AND A.LoanRefenceNo = B.LoanRefNo
                                                INNER JOIN vwsPayrollHeader C on A.EmployeeNo = C.EmployeeNo AND A.PayrollPeriod = C.PayrollPeriod
                                                WHERE  
													                                        A.EmployeeNo = '" + _EmployeeNo + @"' AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _ReferenceNo + @"'  
                                                        AND ((CASE WHEN ISNULL(A.Uploaded ,'N') = 'Y' THEN 0 ELSE (CASE WHEN ISNULL(A.WithInterest, 0) = 0 THEN A.Amount ELSE A.PrincipalAmt END) END)  + 
				                                        CASE WHEN ISNULL(A.OPPosted ,'0') = '1' THEN 0 ELSE ISNULL([PaymentBalance], 0) END
				                                        ) <> 0
				                                        AND A.PayrollPeriod >= (CASE WHEN B.BranchAccount = 1 THEN (CASE WHEN B.PostToSAP = 1 THEN B.StartOfPosting ELSE A.PayrollPeriod END) ELSE A.PayrollPeriod END)
                                                        AND C.PayrollType = 'PAYROLL'
                                                ORDER BY A.PayrollPeriod
                                            ";

            DataTable _tblPayments = new DataTable();
            _tblPayments = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _GetLoanPaymentAmount);
            foreach (DataRow rPayment in _tblPayments.Rows)
            {

                string _vAmountValue = rPayment["Amount"].ToString();
                string _vPayrollPeriod = rPayment["PayrollPeriod"].ToString();


                string _Year = Microsoft.VisualBasic.Strings.Left(_vPayrollPeriod, 4);
                string _Month = Microsoft.VisualBasic.Strings.Mid(_vPayrollPeriod, 6, 2);
                string _DayTag = Microsoft.VisualBasic.Strings.Right(_vPayrollPeriod, 1);

                _FirstDay = DateTime.Parse(_Month + "/1/" + _Year);
                if (_DayTag == "A")
                {
                    _PayrollDate = DateTime.Parse(_FirstDay.AddDays(14).ToString("MM/dd/yyyy"));
                }
                else
                {
                    if (_Month == "02")
                    {
                        _PayrollDate = DateTime.Parse(_FirstDay.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        _PayrollDate = DateTime.Parse(_FirstDay.AddDays(29).ToString("MM/dd/yyyy"));
                    }

                }

                SAPbobsCOM.JournalEntries _JournalEntries;
                _JournalEntries = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);

                _JournalEntries.ReferenceDate = _PayrollDate;
                _JournalEntries.TaxDate = _PayrollDate;
                _JournalEntries.DueDate = _PayrollDate;


                _JournalEntries.Memo = Microsoft.VisualBasic.Strings.Left("Over Pay Payroll Period " + _vPayrollPeriod + @" | " + _Branch + "|" + _ReferenceNo, 50);
                _JournalEntries.UserFields.Fields.Item("U_NSAPADVISENO").Value = _vPayrollPeriod;
                _JournalEntries.UserFields.Fields.Item("U_JETYPE").Value = "Over Payment";
                _JournalEntries.UserFields.Fields.Item("U_oID_OINV").Value = _DocEntry;


                //string _sqlSAPData = "SELECT CASE WHEN '" + _Branch + @"' = 'DESIHOFC' THEN '201013' ELSE A.AcctCode END AS AcctCode FROM OACT A WHERE A.AccntntCod = '" + _Branch + @"'";
                string _sqlSAPData = "SELECT A.AcctCode FROM OACT A WHERE A.AccntntCod = '" + _Branch + @"'";
                string _SAPAcctCode = clsSQLClientFunctions.GetStringValue(clsDeclaration.sSAPConnection, _sqlSAPData, "AcctCode");


                _JournalEntries.Lines.AccountCode = _SAPAcctCode;
                _JournalEntries.Lines.LineMemo = Microsoft.VisualBasic.Strings.Left("Over Pay Payroll Period " + _vPayrollPeriod + @" | " + _Branch + "|" + _ReferenceNo, 50);

                _JournalEntries.Lines.Debit = double.Parse(_vAmountValue);
                _JournalEntries.Lines.Credit = 0.00;
                _JournalEntries.Lines.UserFields.Fields.Item("U_NSAPADVISENO").Value = _vPayrollPeriod;
                _JournalEntries.Lines.UserFields.Fields.Item("U_AcctType").Value = "BP";
                _JournalEntries.Lines.UserFields.Fields.Item("U_API_Vendor").Value = _CardCode;
                _JournalEntries.Lines.UserFields.Fields.Item("U_API_Address").Value = _EmployeeName;

                _JournalEntries.Lines.Add();

                _JournalEntries.Lines.ShortName = _CardCode;
                _JournalEntries.Lines.LineMemo = Microsoft.VisualBasic.Strings.Left("Over Pay Payroll Period " + _vPayrollPeriod + @" | " + _Branch + "|" + _ReferenceNo, 50);

                _JournalEntries.Lines.Debit = 0.00;
                _JournalEntries.Lines.Credit = double.Parse(_vAmountValue);
                _JournalEntries.Lines.UserFields.Fields.Item("U_NSAPADVISENO").Value = _vPayrollPeriod;
                _JournalEntries.Lines.UserFields.Fields.Item("U_AcctType").Value = "BP";
                _JournalEntries.Lines.UserFields.Fields.Item("U_API_Vendor").Value = _CardCode;
                _JournalEntries.Lines.UserFields.Fields.Item("U_API_Address").Value = _EmployeeName;
                _JournalEntries.Lines.Add();


                int lRetCode;
                string sErrMsg;
                int lErrCode;
                Application.DoEvents();
                lRetCode = _JournalEntries.Add();

                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out lErrCode, out sErrMsg);
                    MessageBox.Show(lErrCode + " " + sErrMsg);
                }
                else
                {




                    string _sqlData = @"SELECT B.TransID FROM JDT1 A 
                                        INNER JOIN OJDT B ON A.TransId = B.TransId 
                                        WHERE A.ShortName = '" + _CardCode + @"' AND A.U_NSAPADVISENO = '" + _vPayrollPeriod + @"'
                                        AND B.U_JETYPE = 'Over Payment' AND B.U_oID_OINV = '" + _DocEntry + "'";
                    DataTable _dt = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlData);
                    string _OVDocNum = clsSQLClientFunctions.GetData(_dt, "TransID", "0");


                    string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);
                    string _SQLSyntax = @"UPDATE A SET A.[Uploaded] = 'Y', A.[SAPError] = '' , A.DateUploaded = GETDATE() 
                                                 , A.OPPosted = 1
                                                 , A.OVDocNum = '" + _OVDocNum + @"'
                                            FROM PayrollDetails A  WHERE A.EmployeeNo = '" + _EmployeeNo + "' AND A.PayrollPeriod = '" + _vPayrollPeriod + @"' 
                                            AND A.AccountCode = '" + _AccountCode + @"' AND A.LoanRefenceNo = '" + _ReferenceNo + @"'
                                         ";

                    clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _SQLSyntax);

                    //MessageBox.Show("Payroll Successfully Posted");
                }


            }





        }













    }

}