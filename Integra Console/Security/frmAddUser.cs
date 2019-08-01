using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmAddUser : Form
{
    public frmAddUser()
    {
        InitializeComponent();
    }

    private void frmAddUser_Load(object sender, EventArgs e)
    {

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {

        string _Code = DataEncrypt.Encrypt(txtCode.Text);
        string _Password = DataEncrypt.Encrypt(txtPassword.Text);
        string _RPassword = DataEncrypt.Encrypt(txtRPassword.Text);


        int _chkAdmin = chkAdmin.Checked ? 1 : 0;
        int _chkLock  = chkLock.Checked ? 1 : 0;
        int _chkRankAndFile = chkRankAndFile.Checked ? 1 : 0;
        int _chkSupervisory = chkSupervisory.Checked ? 1 : 0;
        int _chkManagerial = chkManagerial.Checked ? 1 : 0;


        string _Syntax;
        _Syntax = @"INSERT INTO [OUSR] ([PASSWORD]
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
                                                ,'" + _RPassword + @"'
                                                ,'" + _Code + @"'
                                                ,'" + txtName.Text + @"'
                                                ,'" + _chkAdmin + @"'
                                                ,'" + _chkLock + @"'
                                                ,NULL
                                                ,NULL
                                                ,'" + _chkRankAndFile + @"'
                                                ,'" + _chkSupervisory + @"'
                                                ,'" + _chkManagerial + @"'
                                                )";

        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _Syntax);
        MessageBox.Show("User Successfully Created");

        Close();
    }
}