using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class frmAddData : Form
{
    public static string _Type;

    public static string _Code;
    public static string _Name;
    public static string _Area;
    public static string _Company;
    public static string _Schedule;
    
    public static string _sConnection;
    private static DataTable _SchedList = new DataTable();
    public frmAddData()
    {
        InitializeComponent();
    }

    private void frmAddData_Load(object sender, EventArgs e)
    {
        if (_Code == "")
        {
            txtCode.ReadOnly = false;
            txtName.ReadOnly = false;
        }
        else
        {
            txtCode.ReadOnly = true;
            txtName.ReadOnly = true;
        }


        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT DISTINCT A.Area FROM usr_Branches A";
        _DataTable = clsSQLClientFunctions.DataList(_sConnection, _SQLSyntax);

        cboArea.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboArea.Items.Add(row[0].ToString());
        }

        _SQLSyntax = "SELECT DISTINCT A.Company FROM usr_Branches A";
        _DataTable = clsSQLClientFunctions.DataList(_sConnection, _SQLSyntax);

        cboCompany.Items.Add("");
        foreach (DataRow row in _DataTable.Rows)
        {
            cboCompany.Items.Add(row[0].ToString());
        }


        _SQLSyntax = @"SELECT DISTINCT CONCAT(A.ScheduleCode,' - ' ,A.Description) AS Sched 
                      ,A.[Weekdays01]
                      ,A.[Weekdays02]
                      ,A.[BreakTime01]
                      ,A.[BreakTime02]

                FROM Schedule A";
        _DataTable = clsSQLClientFunctions.DataList(_sConnection, _SQLSyntax);
        _SchedList = clsSQLClientFunctions.DataList(_sConnection, _SQLSyntax);

        foreach (DataRow row in _DataTable.Rows)
        {
            cboSchedule.Items.Add(row[0].ToString());
        }

        txtCode.Text = _Code;
        txtName.Text = _Name;
        cboArea.Text = _Area;
        cboCompany.Text = _Company;
        cboSchedule.Text = _Schedule;




    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void cboSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDate1.Text = DateTime.Parse(_SchedList.Rows[cboSchedule.SelectedIndex][1].ToString()).ToString("hh:mm:ss tt");
        txtDate2.Text = DateTime.Parse(_SchedList.Rows[cboSchedule.SelectedIndex][3].ToString()).ToString("hh:mm:ss tt");
        txtDate3.Text = DateTime.Parse(_SchedList.Rows[cboSchedule.SelectedIndex][4].ToString()).ToString("hh:mm:ss tt");
        txtDate4.Text = DateTime.Parse(_SchedList.Rows[cboSchedule.SelectedIndex][2].ToString()).ToString("hh:mm:ss tt");
    }

    private void btnSave_Click(object sender, EventArgs e)
    {


        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = "SELECT 'TRUE' FROM OBLP A WHERE A.CODE = '" + txtCode.Text + "' AND A.COMPANY = '" + cboCompany.Text + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        if (_DataTable.Rows.Count != 0)
        {
            MessageBox.Show("Data Already Exist");
            return;
        }


        if (_DataTable.Rows.Count != 0)
        {
            _SQLSyntax = @"UPDATE A SET A.[Name] = '" + txtName.Text + "',A.[Area] = '" + cboArea.Text + "',A.[SchedCode] = '" + cboSchedule.Text + "'  FROM OBLP A  WHERE A.CODE = '" + txtCode.Text + "' AND A.COMPANY = '" + cboCompany.Text + @"'
              ";
        }
        else
        {
            _SQLSyntax = @"INSERT INTO OBLP ([Code],[Name],[Area],[Company],[SchedCode], [BranchCode]) VALUES ('" + txtCode.Text + "','" + txtName.Text + "','" + cboArea.Text + "','" + cboCompany.Text + "','" + cboSchedule.Text + @"','" + cboCompany.Text + txtCode.Text + @"')
                ";
        }

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);




        string _CompanySyntax;
        if (_DataTable.Rows.Count != 0)
        {
            _CompanySyntax = @"UPDATE A SET A.[Name] = '" + txtName.Text + "',A.[Area] = '" + cboArea.Text + "',A.[SchedCode] = '" + cboSchedule.Text + "'  FROM usr_Branches A  WHERE A.CODE = '" + txtCode.Text + "' AND A.COMPANY = '" + cboCompany.Text + @"'

                UPDATE B SET B.ScheduleCode = A.SchedCode
                FROM vwsDepartmentList A INNER JOIN Employees B ON A.DepartmentCode = B.Department
                WHERE A.BranchCode = '" + txtCode.Text + "' AND A.Company = '" + cboCompany.Text + @"'
              ";
        }
        else
        {
            _CompanySyntax = @"INSERT INTO usr_Branches ([Code],[Name],[Area],[Company],[SchedCode]) VALUES ('" + txtCode.Text + "','" + txtName.Text + "','" + cboArea.Text + "','" + cboCompany.Text + "','" + cboSchedule.Text + @"')

                                UPDATE B SET B.ScheduleCode = A.SchedCode
                                FROM vwsDepartmentList A INNER JOIN Employees B ON A.DepartmentCode = B.Department
                                WHERE A.BranchCode = '" + txtCode.Text + "' AND A.Company = '" + cboCompany.Text + @"'

                ";
        }




        _SQLSyntax = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.Active = '1'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);
        foreach (DataRow row in _DataTable.Rows)
        {
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
                clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sCompanyConnection, _CompanySyntax);
            }
        }



        if (_DataTable.Rows.Count != 0)
        {
            MessageBox.Show("Data Successfully Updated");
        }
        else
        {
            MessageBox.Show("Data Successfully Added");
        }
        Close();

    }
}