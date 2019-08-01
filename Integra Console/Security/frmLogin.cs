using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class frmLogin : Form
{
    //bool _ctrlSecurity = false;

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Enter)
        {
            btnLogin_Click("", EventArgs.Empty);
            return true;
        }
        return false;
        //handle your keys here
    }

    public frmLogin()
    {
        InitializeComponent();
    }

    private void frmLogin_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "Payroll Integra Console Version 6.0.3";
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        string _Username = DataEncrypt.Encrypt(txtUserName.Text);
        string _Password = DataEncrypt.Encrypt(txtPassword.Text);

        DataTable _DataTable;
        string _SQLSyntax;
        _SQLSyntax = @"
                             SELECT * FROM OUSR A WHERE A.[USER_CODE] = '" + _Username + @"' AND A.[PASSWORD] = '" + _Password + @"'
                      ";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);

        clsDeclaration.sComputerID = clsFunctions.GetHardwareKey();
        clsDeclaration.sLoginUserID = clsSQLClientFunctions.GetData(_DataTable, "USERID", "0");
        clsDeclaration.sLoginUserName = clsSQLClientFunctions.GetData(_DataTable, "U_NAME", "0");
        clsDeclaration.sSuperUser = clsSQLClientFunctions.GetData(_DataTable, "SUPERUSER", "0");

        string _RankAndFile = clsSQLClientFunctions.GetData(_DataTable, "RankAndFile", "0");
        string _Supervisor = clsSQLClientFunctions.GetData(_DataTable, "Supervisor", "0");
        string _Manager = clsSQLClientFunctions.GetData(_DataTable, "Manager", "0");

        if (_DataTable.Rows.Count != 0)
        {
            if(_RankAndFile == "1")
            {
                clsDeclaration.sConfiLevel = "0";
            }
            if (_Supervisor == "1")
            {
                clsDeclaration.sConfiLevel = "1";
            }
            if (_Manager == "1")
            {
                clsDeclaration.sConfiLevel = "2";
            }
            if (_RankAndFile == "1" && _Supervisor == "1")
            {
                clsDeclaration.sConfiLevel = "0,1";
            }
            if (_RankAndFile == "1" && _Manager == "1")
            {
                clsDeclaration.sConfiLevel = "0.2";
            }
            if (_Supervisor == "1" && _Manager == "1")
            {
                clsDeclaration.sConfiLevel = "1,2";
            }
            if (_RankAndFile == "1" && _Supervisor == "1" && _Manager == "1")
            {
                clsDeclaration.sConfiLevel = "0,1,2";
            }

            this.Hide();

            if (clsDeclaration.sysGUI == "1")
            {
                ((frmMainAdvance)this.MdiParent).InitializeFormData();
                this.Close();
            }
            else
            {
                ((frmMainWindow)this.MdiParent).lblLogin.Text = "Current User : " + clsDeclaration.sLoginUserName;
                ((frmMainWindow)this.MdiParent).InitializeFormData();
                this.Close();
            }

        }
        else
        {
            MessageBox.Show("Invalid Username and Password");
        }

    }

    private void button1_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
