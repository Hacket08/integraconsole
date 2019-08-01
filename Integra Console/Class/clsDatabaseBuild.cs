using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Integra_Console.Properties;

using System.Configuration;

class clsDatabaseBuild
{
    public static void CreateDatabase(string myConn)
    {
        try
        {
            //clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, "DELETE FROM Employees");

            string _SQLSyntax = "IF NOT EXISTS ( SELECT name FROM sys.databases WHERE name = '" + ConfigurationManager.AppSettings["DBName"] + "' ) ";
            _SQLSyntax = _SQLSyntax + "BEGIN ";
            _SQLSyntax = _SQLSyntax + "    CREATE DATABASE [" + ConfigurationManager.AppSettings["DBName"] + "]; ";
            _SQLSyntax = _SQLSyntax + "END ";

            clsSQLClientFunctions.GlobalExecuteCommand(myConn, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'PerformanceBonusDetails' )
                           BEGIN
 

                                    CREATE TABLE [dbo].[PerformanceBonusDetails](
	                                    [oID] [int] IDENTITY(1,1) NOT NULL,
	                                    [Year] [int] NOT NULL,
	                                    [EmployeeNo] [nvarchar](25) NOT NULL,
	                                    [EmployeeName] [nvarchar](250) NULL,
	                                    [Company] [nvarchar](25) NULL,
	                                    [Branch] [nvarchar](25) NULL,
	                                    [Department] [nvarchar](25) NULL,
	                                    [DateHired] [datetime] NULL,
	                                    [DateRegular] [datetime] NULL,
	                                    [DailyRate] [numeric](19, 6) NULL,
	                                    [Service] [numeric](19, 6) NULL,
	                                    [Appraisal] [numeric](19, 6) NULL,
	                                    [Br Adj] [numeric](19, 6) NULL,
	                                    [Idv Adj] [numeric](19, 6) NULL,
	                                    [Final Rating] [numeric](19, 6) NULL,
	                                    [13th Month] [numeric](19, 6) NULL,
	                                    [Bonus Amt] [numeric](19, 6) NULL,
	                                    [Adjustment] [numeric](19, 6) NULL,
	                                    [TotalAmt] [numeric](19, 6) NULL,
	                                    [FinalAmt] [numeric](19, 6) NULL,
	                                    [Remarks] [nvarchar](1000) NULL,
                                    PRIMARY KEY CLUSTERED 
                                    (
	                                    [oID] ASC,
	                                    [Year] ASC,
	                                    [EmployeeNo] ASC
                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                    ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'SILPDetails' )
                           BEGIN
 

                                    CREATE TABLE [dbo].[SILPDetails](
	                                    [oID] [int] IDENTITY(1,1) NOT NULL,
	                                    [Year] [int] NOT NULL,
	                                    [EmployeeNo] [nvarchar](25) NOT NULL,
	                                    [EmployeeName] [nvarchar](250) NULL,
	                                    [BankAccount] [nvarchar](25) NULL,
	                                    [NetAmount] [numeric](19, 6) NULL,
                                    PRIMARY KEY CLUSTERED 
                                    (
	                                    [oID] ASC,
	                                    [Year] ASC,
	                                    [EmployeeNo] ASC
                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                    ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'LeaveTable' )
                           BEGIN
 
                                CREATE TABLE [dbo].[LeaveTable](
	                                [LeaveCode] [nvarchar](2) NOT NULL,
	                                [LeaveType] [nvarchar](255) NULL,
	                                [LeaveDesc] [nvarchar](60) NULL,
	                                [AccountCode] [nvarchar](5) NOT NULL,
                                PRIMARY KEY CLUSTERED 
                                (
	                                [LeaveCode] ASC
                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                ) ON [PRIMARY]

                                ALTER TABLE [dbo].[LeaveTable] ADD  DEFAULT ('') FOR [LeaveCode]
                                ALTER TABLE [dbo].[LeaveTable] ADD  DEFAULT ('') FOR [LeaveType]
                                ALTER TABLE [dbo].[LeaveTable] ADD  DEFAULT ('') FOR [LeaveDesc]
                                ALTER TABLE [dbo].[LeaveTable] ADD  DEFAULT ('') FOR [AccountCode]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'LeavesApproved' )
                           BEGIN
 
                                CREATE TABLE [dbo].[LeavesApproved](
	                                [oID] [int] IDENTITY(1,1) NOT NULL,
	                                [PayrollPeriod] [nvarchar](9) NOT NULL,
	                                [EmployeeNo] [nvarchar](12) NOT NULL,
	                                [AccountCode] [nvarchar](6) NOT NULL,
	                                [LeaveType] [nvarchar](6) NOT NULL,
	                                [Year] [int] NULL,
	                                [DateStart] [datetime] NULL,
	                                [DateEnd] [datetime] NULL,
	                                [DailyRate] [numeric](19, 6) NULL,
	                                [NoOfHours] [numeric](19, 6) NULL,
	                                [Amount] [numeric](19, 6) NULL,
	                                [NoOfMins] [numeric](19, 6) NULL,
	                                [TotalHrs] [numeric](19, 6) NULL,
	                                [TotalDays] [numeric](19, 6) NULL,
	                                [Branch] [nvarchar](50) NULL,
	                                [Department] [nvarchar](50) NULL,
	                                [Remarks] [nvarchar](1000) NULL,
                                PRIMARY KEY CLUSTERED 
                                (
	                                [oID] ASC,
	                                [PayrollPeriod] ASC,
	                                [EmployeeNo] ASC,
	                                [AccountCode] ASC,
	                                [LeaveType] ASC
                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                ) ON [PRIMARY]

                                ALTER TABLE [dbo].[LeavesApproved] ADD  DEFAULT ((0)) FOR [DailyRate]
                                ALTER TABLE [dbo].[LeavesApproved] ADD  DEFAULT ((0)) FOR [NoOfHours]
                                ALTER TABLE [dbo].[LeavesApproved] ADD  DEFAULT ((0)) FOR [Amount]
                                ALTER TABLE [dbo].[LeavesApproved] ADD  DEFAULT ((0)) FOR [NoOfMins]
                                ALTER TABLE [dbo].[LeavesApproved] ADD  DEFAULT ((0)) FOR [TotalHrs]
                                ALTER TABLE [dbo].[LeavesApproved] ADD  DEFAULT ((0)) FOR [TotalDays]


                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'LeaveRequest' )
                           BEGIN
 
                                    CREATE TABLE [dbo].[LeaveRequest](
	                                    [oID] [int] IDENTITY(1,1) NOT NULL,
	                                    [PayrollPeriod] [nvarchar](9) NOT NULL,
	                                    [EmployeeNo] [nvarchar](12) NOT NULL,
	                                    [AccountCode] [nvarchar](6) NOT NULL,
	                                    [LeaveType] [nvarchar](6) NOT NULL,
	                                    [Year] [int] NULL,
	                                    [DateStart] [datetime] NULL,
	                                    [DateEnd] [datetime] NULL,
	                                    [DailyRate] [numeric](19, 6) NULL,
	                                    [NoOfHours] [numeric](19, 6) NULL,
	                                    [NoOfMins] [numeric](19, 6) NULL,
	                                    [TotalHrs] [numeric](19, 6) NULL,
	                                    [TotalDays] [numeric](19, 6) NULL,
	                                    [Amount] [numeric](19, 6) NULL,
	                                    [Branch] [nvarchar](50) NULL,
	                                    [Department] [nvarchar](50) NULL,
	                                    [Remarks] [nvarchar](1000) NULL,
                                    PRIMARY KEY CLUSTERED 
                                    (
	                                    [oID] ASC,
	                                    [PayrollPeriod] ASC,
	                                    [EmployeeNo] ASC,
	                                    [AccountCode] ASC,
	                                    [LeaveType] ASC
                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                    ) ON [PRIMARY]

                                    ALTER TABLE [dbo].[LeaveRequest] ADD  DEFAULT ((0)) FOR [DailyRate]
                                    ALTER TABLE [dbo].[LeaveRequest] ADD  DEFAULT ((0)) FOR [NoOfHours]
                                    ALTER TABLE [dbo].[LeaveRequest] ADD  DEFAULT ((0)) FOR [NoOfMins]
                                    ALTER TABLE [dbo].[LeaveRequest] ADD  DEFAULT ((0)) FOR [TotalHrs]
                                    ALTER TABLE [dbo].[LeaveRequest] ADD  DEFAULT ((0)) FOR [TotalDays]
                                    ALTER TABLE [dbo].[LeaveRequest] ADD  DEFAULT ((0)) FOR [Amount]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'LeaveCreditConversion' )
                           BEGIN
                                CREATE TABLE [dbo].[LeaveCreditConversion](
	                                [oID] [int] IDENTITY(1,1) NOT NULL,
	                                [Year] [int] NOT NULL,
	                                [EmployeeNo] [nvarchar](25) NOT NULL,
	                                [EmployeeName] [nvarchar](250) NULL,
	                                [Company] [nvarchar](25) NULL,
	                                [Branch] [nvarchar](25) NULL,
	                                [Department] [nvarchar](25) NULL,
	                                [DateHired] [datetime] NULL,
	                                [DateFinish] [datetime] NULL,
	                                [DailyRate] [numeric](19, 6) NULL,
	                                [ServiceStart] [datetime] NULL,
	                                [ServiceEnd] [datetime] NULL,
	                                [Service] [numeric](19, 6) NULL,
	                                [NoOfLeaves] [numeric](19, 6) NULL,
	                                [NoOfLegalHoliday] [numeric](19, 6) NULL,
	                                [NoOfUsedLeave] [numeric](19, 6) NULL,
	                                [NoOfSOT] [numeric](19, 6) NULL,
                                PRIMARY KEY CLUSTERED 
                                (
	                                [oID] ASC,
	                                [Year] ASC,
	                                [EmployeeNo] ASC
                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'PayrollDetails' )
                           BEGIN
                                CREATE TABLE [dbo].[PayrollDetails](
                                                                                [USERID] [int] IDENTITY(1,1) NOT NULL,
                                                                                [PayrollPeriod] [nvarchar](9) NOT NULL,
                                                                                [EmployeeNo] [nvarchar](12) NOT NULL,
                                                                                [AccountCode] [nvarchar](6) NOT NULL,
                                                                                [LoanRefenceNo] [nvarchar](16) NOT NULL,
                                                                                [NoOfHours] [numeric](19,6) NULL,
                                                                                [Amount] [numeric](19,6) NULL,
                                                                                [NoOfMins] [numeric](19,6)  NULL,
                                                                                [BillingAmount] [numeric](19,6) NULL,
                                                                                 CONSTRAINT [KPayrollDetails_PR] PRIMARY KEY CLUSTERED 
                                                                                (
	                                                                                [USERID] ASC,
	                                                                                [PayrollPeriod] ASC,
	                                                                                [EmployeeNo] ASC,
	                                                                                [AccountCode] ASC,
	                                                                                [LoanRefenceNo] ASC
                                                                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'WorkDaysDetails' )
                           BEGIN
 
                                    CREATE TABLE [dbo].[WorkDaysDetails](
	                                    [EmployeeNo] [nvarchar](12) NOT NULL,
	                                    [EmployeeName] [nvarchar](250) NULL,
	                                    [Company] [nvarchar](25) NULL,
	                                    [Branch] [nvarchar](25) NULL,
	                                    [Department] [nvarchar](25) NULL,
	                                    [EmpStatus] [int] NULL,
	                                    [DailyRate] [numeric](19, 6) NULL,
	                                    [PayrollPeriod] [nvarchar](9) NOT NULL,
	                                    [Category] [int] NOT NULL,
	                                    [Regular] [numeric](19, 6) NULL,
	                                    [Absences] [numeric](19, 6) NULL,
	                                    [Tardiness] [numeric](19, 6) NULL,
	                                    [UnderTime] [numeric](19, 6) NULL,
	                                    [RegularOT] [numeric](19, 6) NULL,
	                                    [SundayOT] [numeric](19, 6) NULL,
	                                    [SpecialHoliday] [numeric](19, 6) NULL,
	                                    [LegalHoliday] [numeric](19, 6) NULL,
	                                    [Leave] [numeric](19, 6) NULL,
	                                    [LeaveCode] [nvarchar](25) NULL,
                                    PRIMARY KEY CLUSTERED 
                                    (
	                                    [EmployeeNo] ASC,
	                                    [PayrollPeriod] ASC
                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                    ) ON [PRIMARY]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ('') FOR [EmployeeNo]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [DailyRate]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ('') FOR [PayrollPeriod]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [Regular]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [Absences]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [Tardiness]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [UnderTime]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [RegularOT]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [SundayOT]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [SpecialHoliday]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [LegalHoliday]

                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ((0)) FOR [Leave]


                                    ALTER TABLE [dbo].[WorkDaysDetails] ADD  DEFAULT ('') FOR [LeaveCode]


                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = '13MonthDetails' )
                           BEGIN
                                CREATE TABLE [dbo].[13MonthDetails](
	                                [oID] [int] IDENTITY(1,1) NOT NULL,
	                                [Year] [int] NOT NULL,
	                                [EmployeeNo] [nvarchar](25) NOT NULL,
	                                [EmployeeName] [nvarchar](250) NULL,
	                                [Company] [nvarchar](25) NULL,
	                                [Branch] [nvarchar](25) NULL,
	                                [Department] [nvarchar](25) NULL,
	                                [EmpStatus] [int] NULL,
	                                [DailyRate] [numeric](19, 6) NULL,
	                                [DateStart] [datetime] NULL,
	                                [DateEnd] [datetime] NULL,
	                                [Service] [numeric](19, 6) NULL,
	                                [January] [numeric](19, 6) NULL,
	                                [February] [numeric](19, 6) NULL,
	                                [March] [numeric](19, 6) NULL,
	                                [April] [numeric](19, 6) NULL,
	                                [May] [numeric](19, 6) NULL,
	                                [June] [numeric](19, 6) NULL,
	                                [July] [numeric](19, 6) NULL,
	                                [August] [numeric](19, 6) NULL,
	                                [September] [numeric](19, 6) NULL,
	                                [October] [numeric](19, 6) NULL,
	                                [November] [numeric](19, 6) NULL,
	                                [December] [numeric](19, 6) NULL,
	                                [TotalAmt] [numeric](19, 6) NULL,
	                                [13thMon] [numeric](19, 6) NULL,
	                                [Validated] [int] NULL,
	                                [DateValidated] [datetime] NULL,
                                 CONSTRAINT [K13MonthDetails_PR] PRIMARY KEY CLUSTERED 
                                (
	                                [oID] ASC,
	                                [Year] ASC,
	                                [EmployeeNo] ASC
                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'OUSR' )
                           BEGIN
                                CREATE TABLE [dbo].[OUSR](
	                                [USERID] [int] IDENTITY(1,1) NOT NULL,
	                                [PASSWORD] [nvarchar](254) NULL,
	                                [RPASSWORD] [nvarchar](254) NULL,
	                                [INTERNAL_K] [smallint] NULL,
	                                [USER_CODE] [nvarchar](25) NULL,
	                                [U_NAME] [nvarchar](155) NULL,
	                                [SUPERUSER] [char](1) NULL,
	                                [Locked] [char](1) NULL,
	                                [lastLogin] [datetime] NULL,
	                                [LastPwds] [nvarchar](254) NULL,
	                                [RankAndFile] [nvarchar](1) NULL,
	                                [Supervisor] [nvarchar](1) NULL,
	                                [Manager] [nvarchar](1) NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'Installment' )
                           BEGIN
                                CREATE TABLE [dbo].[Installment](
	                                [oID] [int] IDENTITY(1,1) NOT NULL,
	                                [Emp No] [nvarchar](30) NULL,
	                                [Loan Ref No] [nvarchar](30) NULL,
	                                [Account Code] [nvarchar](15) NULL,
	                                [Amort No] [int] NULL,
	                                [Date Due] [datetime] NULL,
	                                [Payroll Period] [nvarchar](20) NULL,
	                                [Date Paid] [datetime] NULL,
	                                [Prin Amount] [numeric](19, 6) NULL,
	                                [Payment Amt] [numeric](19, 6) NULL,
	                                [Rebate] [numeric](19, 6) NULL,
	                                [Penalty] [numeric](19, 6) NULL,
	                                [Balance] [numeric](19, 6) NULL,
	                                [Remarks] [nvarchar](254) NULL,
	                                [Type] [nvarchar](15) NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'OUAS' )
                           BEGIN
                                CREATE TABLE [dbo].[OUAS](
	                                            [oID] [int] IDENTITY(1,1) NOT NULL,
	                                            [UserID] [nvarchar](30) NULL,
	                                            [ModuleID] [int] NULL,
	                                            [Module] [nvarchar](254) NULL,
	                                            [Access] [nvarchar](30) NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

            DataTable _DataTable;
            _SQLSyntax = @"SELECT * FROM OUSR A";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

            if (_DataTable.Rows.Count == 0)
            {


                string _Username = DataEncrypt.Encrypt("admin");
                string _Password = DataEncrypt.Encrypt("admin");


                _SQLSyntax = @"INSERT INTO [OUSR] ([PASSWORD]
                                               ,[RPASSWORD]
                                               ,[USER_CODE]
                                               ,[U_NAME]
                                               ,[SUPERUSER]
                                               ,[Locked]
                                               ,[lastLogin]
                                               ,[LastPwds]
                                               ,[RankAndFile]
                                               ,[Supervisor]
                                               ,[Manager]) VALUES 
                                                ('" + _Password + @"'
                                                ,'" + _Password + @"'
                                                ,'" + _Username + @"'
                                                ,'Administrator'
                                                ,'1'
                                                ,'0'
                                                ,NULL
                                                ,NULL
                                                ,'1'
                                                ,'1'
                                                ,'1'
                                                )";

                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);
            }


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'OUAM' )
                           BEGIN
                                CREATE TABLE [dbo].[OUAM](
	                                        [oID] [int] IDENTITY(1,1) NOT NULL,
	                                        [Index] [nvarchar](254) NULL,
	                                        [ModuleCode] [nvarchar](30) NULL,
	                                        [Module] [nvarchar](254) NULL,
	                                        [Full] [nvarchar](1) NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'OCMP' )
                           BEGIN
                                CREATE TABLE [dbo].[OCMP](
	                                [CompanyCode] [nvarchar](100) NULL,
	                                [CompanyName] [nvarchar](100) NULL,
	                                [DBServer] [nvarchar](100) NULL,
	                                [DBName] [nvarchar](100) NULL,
	                                [DBUsername] [nvarchar](100) NULL,
	                                [DBPassword] [nvarchar](100) NULL
                                ) ON [PRIMARY]
                           END";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'rptPaySlip' )
                           BEGIN
                                CREATE TABLE [dbo].[rptPaySlip](
	                                    [oID] [int] IDENTITY(1,1) NOT NULL,
	                                    [ComputerID] [nvarchar](100) NULL,
	                                    [User] [int] NULL,
	                                    [rptType] [nvarchar](30) NULL,

	                                    [CompanyName] [nvarchar](250) NULL,
	                                    [PayrollPeriod] [nvarchar](20) NULL,
	                                    [EmployeeNo] [nvarchar](20) NULL,

	                                    [EmployeeName] [nvarchar](250) NULL,
	                                    [BCode] [nvarchar](20) NULL,
	                                    [BName] [nvarchar](250) NULL,
	                                    [DateOne] [Datetime] NULL,
	                                    [DateTwo] [Datetime] NULL,

	                                    [DailyRate] [numeric](19, 6) NULL,
	                                    [BasicPay] [numeric](19, 6) NULL,
	                                    [OTPay] [numeric](19, 6) NULL,
	                                    [OtherIncome] [numeric](19, 6) NULL,
	                                    [SSSEmployee] [numeric](19, 6) NULL,
	                                    [PhilHealthEmployee] [numeric](19, 6) NULL,
	                                    [PagIbigEmployee] [numeric](19, 6) NULL,
	                                    [WitholdingTax] [numeric](19, 6) NULL,
	                                    [Gross] [numeric](19, 6) NULL,
	                                    [TotalDeductions] [numeric](19, 6) NULL,
	                                    [NetPay] [numeric](19, 6) NULL,
	                                    [TotalDays] [numeric](19, 6) NULL,
	                                    [SPLPay] [numeric](19, 6) NULL,
	                                    [LEGPay] [numeric](19, 6) NULL,
	                                    [SUNPay] [numeric](19, 6) NULL,

	                                    [SSSLoan] [numeric](19, 6) NULL,
	                                    [SSSLoanBalance] [numeric](19, 6) NULL,
	                                    [PagibigLoan] [numeric](19, 6) NULL,
	                                    [PagibigLoanBalance] [numeric](19, 6) NULL,
	                                    [CalamityLoan] [numeric](19, 6) NULL,
	                                    [CalamityLoanBalance] [numeric](19, 6) NULL,

	                                    [Advances] [numeric](19, 6) NULL,
	                                    [AdvancesBalance] [numeric](19, 6) NULL,
	                                    [Lending] [numeric](19, 6) NULL,
	                                    [LendingBalance] [numeric](19, 6) NULL,
	                                    [Applicance] [numeric](19, 6) NULL,
	                                    [ApplicanceBalance] [numeric](19, 6) NULL,
	                                    [Motorcycle] [numeric](19, 6) NULL,
	                                    [MotorcycleBalance] [numeric](19, 6) NULL,

	                                    [Others] [numeric](19, 6) NULL,

                                ) ON [PRIMARY]
                           END";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'OCMP' AND A.COLUMN_NAME = 'Active' )
                           BEGIN
                                ALTER TABLE dbo.OCMP ADD Active [nvarchar](1) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'OBLP' AND A.COLUMN_NAME = 'BranchCode' )
                           BEGIN
                                ALTER TABLE dbo.OBLP ADD BranchCode [nvarchar](10) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'OAMR' )
                           BEGIN
                                    CREATE TABLE [dbo].[OAMR](
	                                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                                    [EmployeeNo] [nvarchar](50) NULL,
	                                    [EmployeeName] [nvarchar](250) NULL,
	                                    [Type] [nvarchar](50) NULL,
	                                    [DAY1] [numeric](19, 6) NULL,
	                                    [DAY2] [numeric](19, 6) NULL,
	                                    [DAY3] [numeric](19, 6) NULL,
	                                    [DAY4] [numeric](19, 6) NULL,
	                                    [DAY5] [numeric](19, 6) NULL,
	                                    [DAY6] [numeric](19, 6) NULL,
	                                    [DAY7] [numeric](19, 6) NULL,
	                                    [DAY8] [numeric](19, 6) NULL,
	                                    [DAY9] [numeric](19, 6) NULL,
	                                    [DAY10] [numeric](19, 6) NULL,
	                                    [DAY11] [numeric](19, 6) NULL,
	                                    [DAY12] [numeric](19, 6) NULL,
	                                    [DAY13] [numeric](19, 6) NULL,
	                                    [DAY14] [numeric](19, 6) NULL,
	                                    [DAY15] [numeric](19, 6) NULL,
	                                    [DAY16] [numeric](19, 6) NULL,
	                                    [DAY17] [numeric](19, 6) NULL,
	                                    [DAY18] [numeric](19, 6) NULL,
	                                    [DAY19] [numeric](19, 6) NULL,
	                                    [DAY20] [numeric](19, 6) NULL,
	                                    [DAY21] [numeric](19, 6) NULL,
	                                    [DAY22] [numeric](19, 6) NULL,
	                                    [DAY23] [numeric](19, 6) NULL,
	                                    [DAY24] [numeric](19, 6) NULL,
	                                    [DAY25] [numeric](19, 6) NULL,
	                                    [DAY26] [numeric](19, 6) NULL,
	                                    [DAY27] [numeric](19, 6) NULL,
	                                    [DAY28] [numeric](19, 6) NULL,
	                                    [DAY29] [numeric](19, 6) NULL,
	                                    [DAY30] [numeric](19, 6) NULL,
	                                    [DAY31] [numeric](19, 6) NULL
                                    ) ON [PRIMARY]
                           END";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'DailyAttendance' )
                           BEGIN
                                CREATE TABLE [dbo].[DailyAttendance](
	                                [EmployeeNo] [nvarchar](50) NULL,
	                                [EmployeeName] [nvarchar](250) NULL,
	                                [TransDate] [datetime] NULL,
	                                [TimeIn] [datetime] NULL,
	                                [BTimeOut] [datetime] NULL,
	                                [BTimeIn] [datetime] NULL,
	                                [TimeOut] [datetime] NULL,
	                                [Absences] [int] NULL,
	                                [AbsencesMins] [int] NULL,
	                                [Tardiness] [int] NULL,
	                                [TardinessMins] [int] NULL,
	                                [TotalHrs] [int] NULL,
	                                [TotalMins] [int] NULL,
	                                [ScheduleCode] [nchar](10) NULL,
	                                [ActualSchedule] [nchar](10) NULL,
	                                [UTApproved] [nchar](10) NULL,
	                                [Category] [nchar](10) NULL,
	                                [Weekdays01] [datetime] NULL,
	                                [BreakTime01] [datetime] NULL,
	                                [BreakTime02] [datetime] NULL,
	                                [Weekdays02] [datetime] NULL,
	                                [Company] [nchar](10) NULL
                                ) ON [PRIMARY]
                           END";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyAttendance' AND A.COLUMN_NAME = 'Comment' )
                           BEGIN
                                ALTER TABLE dbo.DailyAttendance ADD Comment [nvarchar](250) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);




            _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
            foreach (DataRow row in _DataTable.Rows)
            {
                string _CampCode = row[7].ToString();
                clsDeclaration.sServer = row[3].ToString();
                clsDeclaration.sCompany = row[4].ToString();
                clsDeclaration.sUsername = row[5].ToString();
                clsDeclaration.sPassword = row[6].ToString();

                clsDeclaration.sCompanyConnection = clsSQLClientFunctions.GlobalConnectionString(
                                               clsDeclaration.sServer, clsDeclaration.sCompany,
                                               clsDeclaration.sUsername, clsDeclaration.sPassword
                                            );


                if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sCompanyConnection) == true)
                {


                    string _SyntaxData;

                    
                    _SyntaxData = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'EmployeePromotion' )
                           BEGIN
                                CREATE TABLE [dbo].[EmployeePromotion](
	                                                    [oID] [int] IDENTITY(1,1) NOT NULL,
	                                                    [EmployeeNo] [nvarchar](30) NULL,
	                                                    [Date] DateTime NULL,
	                                                    [Position] [nvarchar](100) NULL,
	                                                    [LeaveAllowed] [numeric](19,6) NULL
                                ) ON [PRIMARY]
                           END";

                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SyntaxData);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'EmployeePromotion' AND A.COLUMN_NAME = 'Remarks' )
                           BEGIN
                                ALTER TABLE dbo.EmployeePromotion ADD  [Remarks] [nvarchar](MAX) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SyntaxData = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'LeaveFileData' )
                           BEGIN
                                CREATE TABLE [dbo].[LeaveFileData](
	                                [EmployeeNo] [nvarchar](30) NULL,
	                                [DailyRate] [numeric](19,6) NULL,
	                                [Year] int NULL,
	                                [ServiceStart] DateTime NULL,
	                                [ServiceEnd] DateTime NULL,
	                                [DateFrom] DateTime NULL,
	                                [DateTo] DateTime NULL,
	                                [Branch] [nvarchar](50) NULL,
	                                [Department] [nvarchar](50) NULL
                                ) ON [PRIMARY]
                           END";

                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SyntaxData);

                    _SyntaxData = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'LeaveFileDetails' )
                           BEGIN
                                CREATE TABLE [dbo].[LeaveFileDetails](
	                                            [Year] int NULL,
	                                            [EmployeeNo] [nvarchar](12) NULL,
	                                            [AccountCode] [nvarchar](6) NULL,
	                                            [LoanRefenceNo] [nvarchar](16) NULL,
	                                            [Amount] [numeric](19, 6) NULL,
	                                            [LeaveCredit] [numeric](19, 6) NULL,
	                                            [NoOfService] [numeric](19, 6) NULL,
	                                            [NoOfDays] [numeric](19, 6) NULL,
	                                            [Branch] [nvarchar](50) NULL,
	                                            [Department] [nvarchar](50) NULL
                                ) ON [PRIMARY]
                           END";

                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SyntaxData);



                    _SyntaxData = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'usr_Branches' )
                           BEGIN
                                CREATE TABLE [dbo].[usr_Branches](
	                                [Code] [nvarchar](100) NULL,
	                                [Name] [nvarchar](100) NULL,
	                                [Area] [nvarchar](100) NULL,
	                                [Company] [nvarchar](100) NULL,
	                                [SchedCode] [nvarchar](100) NULL
                                ) ON [PRIMARY]
                           END";

                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SyntaxData);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'AccountCode' AND A.COLUMN_NAME = 'SAPAccount' )
                           BEGIN
                                ALTER TABLE dbo.AccountCode ADD SAPAccount [nvarchar](25) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'AccountCode' AND A.COLUMN_NAME = 'PayrollRegCode' )
                           BEGIN
                                ALTER TABLE dbo.AccountCode ADD PayrollRegCode [nvarchar](25) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollTrans02' AND A.COLUMN_NAME = 'Remarks' )
                           BEGIN
                                ALTER TABLE dbo.PayrollTrans02 ADD Remarks [nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollTrans01' AND A.COLUMN_NAME = 'Remarks' )
                           BEGIN
                                ALTER TABLE dbo.PayrollTrans01 ADD Remarks [nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'usr_Branches' AND A.COLUMN_NAME = 'BranchCode' )
                           BEGIN
                                ALTER TABLE dbo.usr_Branches ADD BranchCode [nvarchar](10) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SyntaxData = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTimeDetails' AND A.COLUMN_NAME = 'UnderApproved' )
                           BEGIN
                                ALTER TABLE dbo.DailyTimeDetails ADD UnderApproved [nvarchar](1) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SyntaxData);


                    _SyntaxData = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTimeDetails' AND A.COLUMN_NAME = 'ConfirmAtt' )
                           BEGIN
                                ALTER TABLE dbo.DailyTimeDetails ADD ConfirmAtt [nvarchar](1) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SyntaxData);


                    _SyntaxData = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTimeDetails' AND A.COLUMN_NAME = 'Comment' )
                           BEGIN
                                ALTER TABLE dbo.DailyTimeDetails ADD Comment [nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SyntaxData);


                    _SyntaxData = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTrans' AND A.COLUMN_NAME = 'Remarks' )
                           BEGIN
                                ALTER TABLE dbo.DailyTrans ADD Remarks [nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SyntaxData);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'DateRegular' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD DateRegular DATETIME NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'TerminationStatus' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD TerminationStatus [nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'Remarks' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD Remarks [nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollTrans01' AND A.COLUMN_NAME = 'TotalHrs' )
                           BEGIN
                                ALTER TABLE dbo.PayrollTrans01 ADD TotalHrs [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollTrans01' AND A.COLUMN_NAME = 'TotalDays' )
                           BEGIN
                                ALTER TABLE dbo.PayrollTrans01 ADD TotalDays [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    #region Update PayrollDetails

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'OPPosted' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD OPPosted [int] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'RCDocNum' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD RCDocNum [Nvarchar](30) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'OVDocNum' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD OVDocNum [Nvarchar](30) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);






                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'PaymentBalance' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD PaymentBalance [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'TotalHrs' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD TotalHrs [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'Branch' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD Branch [Nvarchar](50) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'Department' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD Department [Nvarchar](50) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'TotalDays' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD TotalDays [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'Uploaded' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD Uploaded [Nvarchar](5) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'SAPError' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD SAPError [Nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'SAPInsID' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD SAPInsID  [Numeric](19,0) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'SAPInsDate' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD SAPInsDate  [DateTime] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'SAPDocNumber' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD SAPDocNumber [Nvarchar](30) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'DateUploaded' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD DateUploaded [DateTime] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'WithInterest' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD WithInterest [int] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'PrincipalAmt' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD PrincipalAmt [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollDetails' AND A.COLUMN_NAME = 'InterestAmt' )
                           BEGIN
                                ALTER TABLE dbo.PayrollDetails ADD InterestAmt [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    #endregion

                    #region Update PayrollHeader


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'Validated' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD Validated [Nvarchar](5) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'TotalDays' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD TotalDays [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'TotalHrs' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD TotalHrs [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);




                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'SSSLoan' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD SSSLoan [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'PagibigLoan' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD PagibigLoan [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'OtherLoan' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD OtherLoan [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'OTHrs' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD OTHrs [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);





                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'SPLHrs' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD SPLHrs [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'LEGHrs' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD LEGHrs [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'SUNHrs' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD SUNHrs [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);




                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'SPLPay' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD SPLPay [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'LEGPay' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD LEGPay [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'SUNPay' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD SUNPay [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'CalamityLoan' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD CalamityLoan [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'Branch' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD Branch [Nvarchar](50) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'PayrollType' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD PayrollType [Nvarchar](20) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollHeader' AND A.COLUMN_NAME = 'IsHold' )
                           BEGIN
                                ALTER TABLE dbo.PayrollHeader ADD IsHold [Nvarchar](1) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);
                    #endregion

                    #region Update LoanFile



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'StartOfPosting' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD StartOfPosting [Nvarchar](15) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'AdviceNo' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD AdviceNo [Nvarchar](30) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'IsTransfered' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD IsTransfered [int] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'PostToSAP' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD PostToSAP [int] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'TranferredDate' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD TranferredDate [DateTime] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'BranchAccount' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD BranchAccount [int] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'SAPError' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD SAPError [Nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'FirstDueDate' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD FirstDueDate [DateTime] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'DueDate' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD DueDate [DateTime] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'MonthlyDueDate' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD MonthlyDueDate [DateTime] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'RelativeName' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD RelativeName [Nvarchar](50) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'SAPBPCode' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD SAPBPCode [Nvarchar](30) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'SAPDocEntry' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD SAPDocEntry [Nvarchar](30) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'SAPARCount' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD SAPARCount [Numeric](19, 0) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'Brand' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD Brand [Nvarchar](50) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'Terms' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD Terms [Nvarchar](50) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'Particular' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD Particular [Nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'OrigBCode' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD OrigBCode [Nvarchar](50) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'OrigBName' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD OrigBName [Nvarchar](250) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'LCPPrice' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD LCPPrice [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'SpotCashAmount' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD SpotCashAmount [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'DownPayment' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD DownPayment [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'Rebate' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD Rebate [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'LoanInterest' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD LoanInterest [Numeric](19, 6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'RebateApplication' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD RebateApplication [Nvarchar](5) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'CreateDate' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD CreateDate DATETIME NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'UpdateDate' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD UpdateDate DATETIME NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanFile' AND A.COLUMN_NAME = 'DateInActive' )
                           BEGIN
                                ALTER TABLE dbo.LoanFile ADD DateInActive DATETIME NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);
                    #endregion



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanCashPayment' AND A.COLUMN_NAME = 'CreateDate' )
                           BEGIN
                                ALTER TABLE dbo.LoanCashPayment ADD CreateDate DATETIME NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanCashPayment' AND A.COLUMN_NAME = 'UpdateDate' )
                           BEGIN
                                ALTER TABLE dbo.LoanCashPayment ADD UpdateDate DATETIME NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanCashPayment' AND A.COLUMN_NAME = 'Type' )
                           BEGIN
                                ALTER TABLE dbo.LoanCashPayment ADD Type [Nvarchar](100) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LoanCashPayment' AND A.COLUMN_NAME = 'PayrollPeriod' )
                           BEGIN
                                ALTER TABLE dbo.LoanCashPayment ADD PayrollPeriod [Nvarchar](15) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'AsBranchCode' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD AsBranchCode [Nvarchar](20) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'dftLeaveCredit' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD dftLeaveCredit [numeric](19,0) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);



                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'PayrollPeriod' AND A.COLUMN_NAME = 'WorkDays' )
                           BEGIN
                                ALTER TABLE dbo.PayrollPeriod ADD WorkDays [int] NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);


                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LeaveFileData' AND A.COLUMN_NAME = 'PaidAmount' )
                           BEGIN
                                ALTER TABLE dbo.LeaveFileData ADD PaidAmount [numeric](19,6) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);

                    _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'LeaveFileData' AND A.COLUMN_NAME = 'BatchNo' )
                           BEGIN
                                ALTER TABLE dbo.LeaveFileData ADD BatchNo [numeric](19,0) NULL
                           END";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SQLSyntax);




                    _SyntaxData = @"SELECT [Code],[Name],[Area],[Company],[SchedCode],[BranchCode]  FROM OBLP A";
                    DataTable _SyntaxTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SyntaxData);

                    foreach (DataRow row1 in _SyntaxTable.Rows)
                    {

                        string _InsertData = @"
                                                    IF EXISTS (SELECT 'TRUE' FROM usr_Branches Z WHERE Z.Code = '" + row1[0].ToString() + @"' AND Z.COMPANY = '" + row1[3].ToString() + @"')
                                                    BEGIN
	                                                    UPDATE A SET A.[Name] = '" + row1[1].ToString() + "',A.[Area] = '" + row1[2].ToString() + "',A.[SchedCode] = '" + row1[4].ToString() + "',A.[BranchCode] = '" + row1[5].ToString() + "'  FROM usr_Branches A WHERE A.CODE = '" + row1[0].ToString() + "' AND A.COMPANY = '" + row1[3].ToString() + @"'
              
                                                    END                                                   
                                                    ELSE
                                                    BEGIN
	                                                    INSERT INTO usr_Branches
	                                                    SELECT '" + row1[0].ToString() + @"'
                                                              ,'" + row1[1].ToString() + @"'
                                                              ,'" + row1[2].ToString() + @"'
                                                              ,'" + row1[3].ToString() + @"'
                                                              ,'" + row1[4].ToString() + @"'
                                                              ,'" + row1[5].ToString() + @"'
                                                    END
                                              ";

                        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _InsertData);
                    }


                    _SyntaxData = @"
                                    UPDATE B SET B.ScheduleCode = A.SchedCode
                                    FROM vwsDepartmentList A INNER JOIN Employees B ON A.DepartmentCode = B.Department
				                    WHERE ISNULL(B.ScheduleCode,'') <> ISNULL(A.SchedCode,'') 
                               ";
                    clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _SyntaxData);

                }
            }


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'DailyTrans' )
                           BEGIN
                                CREATE TABLE [dbo].[DailyTrans](
                                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                                [GroupCode] [nvarchar](10) NULL,
	                                [EmployeeNo] [nvarchar](12) NULL,
	                                [TransType] [nvarchar](1) NULL,
	                                [TransDate] [datetime] NULL,
	                                [TransTime] [datetime] NULL,
	                                [TransType02] [nchar](1) NULL,
                                    [Branch] [nvarchar](250) NULL
                                ) ON [PRIMARY]
                           END";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'PayrollLocker' )
                           BEGIN
                                CREATE TABLE [dbo].[PayrollLocker](
	                                                            [PayrollPeriod] [nchar](25) NULL,
	                                                            [Branch] [nchar](25) NULL,
	                                                            [Position] [nchar](25) NULL,
	                                                            [IsLocked] [nchar](1) NULL
                                                            ) ON [PRIMARY]
                           END";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTrans' AND A.COLUMN_NAME = 'Remarks' )
                           BEGIN
                                ALTER TABLE dbo.DailyTrans ADD Remarks [nvarchar](250) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'Employees' )
                           BEGIN
                                CREATE TABLE [dbo].[Employees](
	                                [EmployeeNo] [nvarchar](12) NULL,
	                                [FullName] [nvarchar](250) NULL,
	                                [Department] [nvarchar](20) NULL
                                ) ON [PRIMARY]
                           END";

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'OCMP' AND A.COLUMN_NAME = 'CompCode' )
                           BEGIN
                                ALTER TABLE dbo.OCMP ADD CompCode [nvarchar](10) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'CompCode' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD CompCode [nvarchar](10) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'Deleted' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD Deleted [nvarchar](5) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);




            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'OBLP' )
                           BEGIN
                                CREATE TABLE [dbo].[OBLP](
	                                [Code] [nvarchar](100) NULL,
	                                [Name] [nvarchar](100) NULL,
	                                [Area] [nvarchar](100) NULL,
	                                [Company] [nvarchar](100) NULL,
	                                [SchedCode] [nvarchar](100) NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'OPST' )
                           BEGIN
                                CREATE TABLE [dbo].[OPST](
	                                [Code] [nvarchar](100) NULL,
	                                [Name] [nvarchar](100) NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'OPST' AND A.COLUMN_NAME = 'CalcBreak' )
                           BEGIN
                                ALTER TABLE dbo.OPST ADD CalcBreak [nvarchar](1) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'ODPT' )
                           BEGIN
                                CREATE TABLE [dbo].[ODPT](
	                                [Code] [nvarchar](100) NULL,
	                                [Name] [nvarchar](100) NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'DailyInOut' )
                           BEGIN
                                CREATE TABLE [dbo].[DailyInOut](
	                                [EmployeeNo] [nvarchar](12) NOT NULL,
	                                [TransDate] [datetime] NOT NULL,
	                                [TimeOUT] [datetime] NULL,
	                                [TimeIN] [datetime] NULL,
	                                [TotalHrs] [float] NULL,
	                                [IsDeduct] [nchar](1) NULL,
	                                [IsType] [nchar](1) NULL,
	                                [IsBreakTime] [nchar](1) NULL,
	                                [TotalMins] [float] NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'DailyTimeDetails' )
                           BEGIN
                                CREATE TABLE [dbo].[DailyTimeDetails](
	                                [EmployeeNo] [nvarchar](12) NOT NULL,
	                                [EmployeeName] [nvarchar](250) NOT NULL,
	                                [TransDate] [datetime] NOT NULL,
	                                [PayrollPeriod] [nvarchar](9) NULL,
                                    [Weekdays01] [datetime] NULL,
	                                [BreakTime01] [datetime] NULL,
	                                [BreakTime02] [datetime] NULL,
	                                [Weekdays02] [datetime] NULL,
	                                [TimeIn] [datetime] NULL,
	                                [TimeOut] [datetime] NULL,
	                                [RegularHrs] [float] NULL,
	                                [Absences] [float] NULL,
	                                [Tardiness] [float] NULL,
	                                [ExcessHrs] [float] NULL,
	                                [ApprovedOT] [float] NULL,
	                                [Holiday] [float] NULL,
	                                [NightDiff] [float] NULL,
	                                [SickLeave] [float] NULL,
	                                [VacationLeave] [float] NULL,
	                                [PaternityLeave] [float] NULL,
	                                [InOutStatus] [nvarchar](1) NULL,
	                                [NightDiffOT] [float] NULL,
	                                [LeaveCode] [nvarchar](2) NULL,
	                                [OtherDeduction] [float] NULL,
	                                [DeductibleAmount] [money] NULL,
	                                [ProjectCode] [nvarchar](20) NULL,
	                                [BaseRegularHrs] [float] NULL,
	                                [RegularMins] [float] NULL,
	                                [AbsencesMins] [float] NULL,
	                                [TardinessMins] [float] NULL,
	                                [ExcessMins] [float] NULL,
	                                [ApprovedOTMins] [float] NULL,
	                                [HolidayMins] [float] NULL,
	                                [NightDiffMins] [float] NULL,
	                                [SickLeaveMins] [float] NULL,
	                                [VacationLeaveMins] [float] NULL,
	                                [PaternityLeaveMins] [float] NULL,
	                                [NightDiffOTMins] [float] NULL,
	                                [OtherDeductionMins] [float] NULL,
	                                [BaseRegularMins] [float] NULL,
	                                [ApprovedNDiffOTHrs] [float] NULL,
	                                [ApprovedNDiffOTMins] [float] NULL,
	                                [NextDayTime] [nchar](1) NULL,
	                                [EarlyOTHrs] [int] NULL,
	                                [EarlyOTMins] [int] NULL,
	                                [ApprovedEarlyOTHrs] [int] NULL,
	                                [ApprovedEarlyOTMins] [int] NULL,
	                                [DispTimeIn] [nvarchar](20) NULL,
	                                [DispTimeOut] [nvarchar](20) NULL,
	                                [UnderApproved] [nvarchar](1) NULL,
	                                [ConfirmAtt] [nvarchar](1) NULL,
	                                [Comment] [nvarchar](250) NULL,
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'DailyTransLogs' )
                           BEGIN
                                CREATE TABLE [dbo].[DailyTransLogs](
	                                [ID] [int] IDENTITY(1,1) NOT NULL,
	                                [GroupCode] [nvarchar](10) NULL,
	                                [EmployeeNo] [nvarchar](12) NULL,
	                                [TransType] [nvarchar](1) NULL,
	                                [TransDate] [datetime] NULL,
	                                [TransTime] [datetime] NULL,
	                                [TransType02] [nchar](1) NULL,
	                                [Branch] [nvarchar](250) NULL,
	                                [Remarks] [nvarchar](250) NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'ScheduleCalendarEmp' )
                           BEGIN
                                CREATE TABLE [dbo].[ScheduleCalendarEmp](
	                                [EmployeeNo] [nchar](12) NOT NULL,
	                                [MoYear] [nchar](10) NOT NULL,
	                                [AppDays] [datetime] NOT NULL,
	                                [ShiftCode] [nchar](10) NULL,
	                                [RestDay] [nchar](1) NULL,
	                                [Remarks] [nchar](10) NULL,
	                                [TimeIn] [datetime] NULL,
	                                [TimeOut] [datetime] NULL,
	                                [RegularHrs] [numeric](18, 4) NULL,
	                                [TotalBreak] [numeric](18, 4) NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'Schedule' )
                           BEGIN
                                CREATE TABLE [dbo].[Schedule](
	                               	[ScheduleCode] [nvarchar](3) NOT NULL,
	                                [Description] [nvarchar](50) NULL,
	                                [Weekdays01] [datetime] NULL,
	                                [Weekdays02] [datetime] NULL,
	                                [DefaultSched] [nvarchar](1) NULL,
	                                [BreakTimeHours] [float] NULL,
	                                [BreakTime01] [datetime] NULL,
	                                [BreakTime02] [datetime] NULL
                                ) ON [PRIMARY]
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTransLogs' AND A.COLUMN_NAME = 'Branch' )
                           BEGIN
                                ALTER TABLE dbo.DailyTransLogs ADD Branch [nvarchar](50) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTimeDetails' AND A.COLUMN_NAME = 'Branch' )
                           BEGIN
                                ALTER TABLE dbo.DailyTimeDetails ADD Branch [nvarchar](50) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyAttendance' AND A.COLUMN_NAME = 'Branch' )
                           BEGIN
                                ALTER TABLE dbo.DailyAttendance ADD Branch [nvarchar](50) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyInOut' AND A.COLUMN_NAME = 'Branch' )
                           BEGIN
                                ALTER TABLE dbo.DailyInOut ADD Branch [nvarchar](50) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'DateLocker' )
                           BEGIN
                                
                            CREATE TABLE [dbo].[DateLocker](
	                            [TransDate] [datetime] NOT NULL,
                                [Branch] [nchar](25) NULL,
	                            [IsLocked] [nchar](1) NULL
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'SysVariables' )
                           BEGIN
                                
                            CREATE TABLE [dbo].[SysVariables](
	                                        [VarID] [int] IDENTITY(1,1) NOT NULL,
	                                        [VariableName] [nvarchar](130) NULL,
	                                        [VariableValue] [nvarchar](100) NULL
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);



            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'SalaryTable' )
                           BEGIN
                                
                            CREATE TABLE [dbo].[SalaryTable](
	                                        [oID] [int] IDENTITY(1,1) NOT NULL,
	                                        [BracketFrom] [numeric](19,6) NULL,
	                                        [BracketTo] [numeric](19,6) NULL,
	                                        [BasicSalary] [numeric](19,6) NULL
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'TaxTableAnnual' )
                           BEGIN
                                
                            CREATE TABLE [dbo].[TaxTableAnnual](
	                                        [oID] [int] IDENTITY(1,1) NOT NULL,
	                                        [BracketFrom] [numeric](19,6) NULL,
	                                        [BracketTo] [numeric](19,6) NULL,
	                                        [TaxAmount] [numeric](19,6) NULL,
	                                        [Rate] [numeric](19,6) NULL,
	                                        [Excess] [numeric](19,6) NULL
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'TaxTableMonthly' )
                           BEGIN
                                
                            CREATE TABLE [dbo].[TaxTableMonthly](
	                                        [oID] [int] IDENTITY(1,1) NOT NULL,
	                                        [BracketFrom] [numeric](19,6) NULL,
	                                        [BracketTo] [numeric](19,6) NULL,
	                                        [TaxAmount] [numeric](19,6) NULL,
	                                        [Rate] [numeric](19,6) NULL,
	                                        [Excess] [numeric](19,6) NULL
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'SSSTable' )
                           BEGIN
                                
                            CREATE TABLE [dbo].[SSSTable](
	                                        [oID] [int] IDENTITY(1,1) NOT NULL,
	                                        [BracketFrom] [numeric](19,6) NULL,
	                                        [BracketTo] [numeric](19,6) NULL,
	                                        [Employer] [numeric](19,6) NULL,
	                                        [Employee] [numeric](19,6) NULL,
	                                        [ECC] [numeric](19,6) NULL,
	                                        [Status] [nvarchar](1) NULL,
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'PhilHealthTable' )
                           BEGIN
                                
                            CREATE TABLE [dbo].[PhilHealthTable](
	                                        [oID] [int] IDENTITY(1,1) NOT NULL,
	                                        [BracketFrom] [numeric](19,6) NULL,
	                                        [BracketTo] [numeric](19,6) NULL,
	                                        [Base] [numeric](19,6) NULL,
	                                        [Rate] [numeric](19,6) NULL,
	                                        [Employer] [numeric](19,6) NULL,
	                                        [Employee] [numeric](19,6) NULL,
	                                        [Status] [nvarchar](1) NULL,
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'PAGIBIGTable' )
                           BEGIN
                                
                            CREATE TABLE [dbo].[PAGIBIGTable](
	                                        [oID] [int] IDENTITY(1,1) NOT NULL,
	                                        [BracketFrom] [numeric](19,6) NULL,
	                                        [BracketTo] [numeric](19,6) NULL,
                                            [Employer] [numeric](19,6) NULL,
                                            [Employee] [numeric](19,6) NULL,
                                            [MaxContribution] [numeric](19,6) NULL,
                                            [Status] [nvarchar](1) NULL,
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS (SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'TaxStatus' )
                           BEGIN
                                
                            CREATE TABLE [dbo].[TaxStatus](
	                                        [oID] [int] IDENTITY(1,1) NOT NULL,
	                                        [StatusCode] [nvarchar](50) NULL,
	                                        [PersonalExempt] [numeric](19,6) NULL,
	                                        [Dependents] [numeric](19,6) NULL
                                ) ON [PRIMARY]

                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'EmpStatus' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD EmpStatus [nvarchar](1) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'ScheduleCode' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD ScheduleCode [nvarchar](10) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);
            

            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'Category' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD Category [nvarchar](1) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'DateRegular' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD DateRegular DATETIME NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'Employees' AND A.COLUMN_NAME = 'Remarks' )
                           BEGIN
                                ALTER TABLE dbo.Employees ADD Remarks [nvarchar](250) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTrans' AND A.COLUMN_NAME = 'Uploaded' )
                           BEGIN
                                ALTER TABLE dbo.DailyTrans ADD Uploaded [nvarchar](5) NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTrans' AND A.COLUMN_NAME = 'SysDate' )
                           BEGIN
                                ALTER TABLE dbo.DailyTrans ADD SysDate [DateTime] NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);


            _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'DailyTransLogs' AND A.COLUMN_NAME = 'SysDate' )
                           BEGIN
                                ALTER TABLE dbo.DailyTransLogs ADD SysDate [DateTime] NULL
                           END";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

        }
        catch
        {  }
    }



    public static void UpdatePayrollDB(string myConn)
    {
        string _SQLSyntax;
       _SQLSyntax = @"IF NOT EXISTS ( SELECT A.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS A WHERE A.TABLE_NAME = 'CustomPYSetup' AND A.COLUMN_NAME = 'CutOffDate' )
                           BEGIN
                                ALTER TABLE dbo.CustomPYSetup ADD CutOffDate [DateTime] NULL
                           END";
        clsSQLClientFunctions.GlobalExecuteCommand(myConn, _SQLSyntax);
    }

}
