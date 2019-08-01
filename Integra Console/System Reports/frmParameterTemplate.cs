using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmParameterTemplate : Form
{

    public static string _RequestType;
    public static string _QueryGenerated;
    public frmParameterTemplate()
    {
        InitializeComponent();
    }

    private void frmParameterTemplate_Load(object sender, EventArgs e)
    {
        //rbAll.Checked = true;
        rbActive.Checked = true;


        clsDeclaration.bView = false;

        //pnlEmployee.Enabled = false;
        //chkFilterPerEmployee.Checked = false;
        clsDeclaration.sFilterByEmployee = false;

        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT CONCAT(A.CompCode,' - ',A.CompanyName) AS Company FROM OCMP A WHERE A.Active = '1'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        cboCompany.Items.Clear();
        cboCompany.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }

        _SQLSyntax = "SELECT DISTINCT A.Area FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboArea.Items.Clear();
        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }


        _SQLSyntax = "SELECT DISTINCT A.DepCode, A.DepName FROM dbo.[vwsDepartmentList] A";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboDepartment.Items.Clear();
        cboDepartment.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboDepartment.Items.Add(row[0].ToString() + " - " + row[1].ToString());
        }

        chkRankAndFile.Checked = true;
        chkSupervisory.Checked = true;
        chkManagerial.Checked = true;
        //chkFiltered.Checked = true;

    }

    private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable _DataTable;
        string _SQLSyntax;

        _SQLSyntax = "SELECT DISTINCT CONCAT(A.BCode,' - ',A.BName) AS Branch FROM dbo.[vwsDepartmentList] A WHERE A.Area = '" + cboArea.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        cboBranch.Items.Clear();
        cboBranch.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboBranch.Items.Add(row[0].ToString());
        }
    }


    private void btnCancel_Click(object sender, EventArgs e)
    {
        clsDeclaration.bView = false;
        Close();
    }

  

    private void btnUpload_Click(object sender, EventArgs e)
    {
        #region Parameter Selection

        string _Company = "";
        if (cboCompany.Text == "")
        {
            _Company = "";
        }
        else
        {
            _Company = cboCompany.Text.Substring(0, 4);
        }


        string _Branch = "";
        if (cboBranch.Text == "")
        {
            _Branch = "";
        }
        else
        {
            _Branch = cboBranch.Text.Substring(0, 8);
        }

        string _Deparment = "";
        if (cboDepartment.Text == "")
        {
            _Deparment = "";
        }
        else
        {
            _Deparment = cboDepartment.Text.Substring(0, 4);
        }


        clsDeclaration.sConfiLevelSelection = "0,1,2";
        if (chkRankAndFile.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0";
        }
        if (chkSupervisory.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "1";
        }
        if (chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "2";
        }
        if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0,1";
        }
        if (chkRankAndFile.Checked == true && chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0,2";
        }
        if (chkSupervisory.Checked == true && chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "1,2";
        }
        if (chkRankAndFile.Checked == true && chkSupervisory.Checked == true && chkManagerial.Checked == true)
        {
            clsDeclaration.sConfiLevelSelection = "0,1,2";
        }

        //string _selActive = "0";
        //if(rbActive.Checked == true)
        //{
        //    _selActive = "0";
        //}
        //else
        //{
        //    _selActive = "1";
        //}


        #endregion

        switch(clsDeclaration.sReportID)
        {
            case 7:

                break;
        }





        frmReportList frmReportList = new frmReportList();
        //frmReportList._RequestType = _RequestType;
        frmReportList.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmReportList.Show();
    }




    private string sqlQueryOutput(string _refBranch = "",
                                string _refArea = "",
                                string _refDepartment = "",
                                string _refActive = "",
                                string _refDateHiredFrom = "",
                                string _refDateHiredTo = "",
                                string _refComp = "",
                                string _refConfiLevel = "",
                                string _refConfiSelection = "")
    {



        string _sqlSyntax = "";

        _sqlSyntax = @"

                        SELECT * FROM (

                        SELECT 
                         1 AS [Count]
                        , A.Company
                        , B.DepCode AS [DepartmentCode]
                        , B.DepName AS [Position]
                        , B.BName
                        , (SELECT X.Name FROM OBLP X WHERE X.BranchCode = A.AsBranchCode) AS [AssignBranch]
                        , B.BranchCode
                        , B.BranchName
                        , B.AREA
                        , B.BCode
                        FROM [vwsEmployees] A 
                        INNER JOIN [vwsDepartmentList] B ON A.Department = B.DepartmentCode
                        WHERE A.ConfiLevel IN (" + _refConfiLevel + @")

                        ) XX";


        _sqlSyntax = _sqlSyntax + @" WHERE XX.ConfiLevel IN (" + _refConfiSelection + @") ";

        if (_refComp != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[Company] LIKE '%" + _refComp + @"%'";
        }


        if (_refDepartment != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[DepartmentCode] LIKE '%" + _refDepartment + @"%'";
        }

        if (_refBranch != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[BCode] LIKE '%" + _refBranch + @"%'";
        }

        if (_refArea != "")
        {
            _sqlSyntax = _sqlSyntax + @" AND XX.[AREA] LIKE '%" + _refArea + @"%'";
        }



        if (_refActive == "0")
        {
            _sqlSyntax = _sqlSyntax + @" 
                                        AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) IN (0,1,2)";


            _sqlSyntax = _sqlSyntax + @" AND XX.DateHired BETWEEN '" + _refDateHiredFrom + @"' AND '" + _refDateHiredTo + @"'";
        }

        if (_refActive == "1")
        {
            _sqlSyntax = _sqlSyntax + @" 
                                        AND (CASE WHEN ISNULL(XX.TerminationStatus,XX.EmpStatus) IN (7,8,9) THEN XX.TerminationStatus ELSE XX.EmpStatus END) NOT IN (0,1,2)";


            _sqlSyntax = _sqlSyntax + @" AND XX.DateFinish BETWEEN '" + _refDateHiredFrom + @"' AND '" + _refDateHiredTo + @"'";

        }



        return _sqlSyntax;
    }




    private void frmParameterTemplate_FormClosing(object sender, FormClosingEventArgs e)
    {
        clsDeclaration.sReportID = 0;
        clsDeclaration.sReportPath = "";
        clsDeclaration.sQueryString = "";
    }

}
