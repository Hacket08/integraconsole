using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Integra_Console.Properties;

using System.Configuration;


public partial class frmConnection : Form
{
    public frmConnection()
    {
        InitializeComponent();


        txtDBServerName.Text = ConfigurationManager.AppSettings["DBServer"];
        txtDBName.Text = ConfigurationManager.AppSettings["DBName"];
        txtDBUsername.Text = ConfigurationManager.AppSettings["DBUsername"];
        txtDBPassword.Text = ConfigurationManager.AppSettings["DBPassword"];
    }

    private void frmConnection_Load(object sender, EventArgs e)
    {
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        clsFunctions.SetSetting("DBServer", txtDBServerName.Text);
        clsFunctions.SetSetting("DBName", txtDBName.Text);
        clsFunctions.SetSetting("DBUsername", txtDBUsername.Text);
        clsFunctions.SetSetting("DBPassword", txtDBPassword.Text);

        MessageBox.Show("New Settings Applied!", "Connection Settings", MessageBoxButtons.OK, MessageBoxIcon.None);
        Application.Restart();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
