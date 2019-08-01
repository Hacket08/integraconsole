using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmMainAdvance : Form
{
    //
    // source code 
    // Code Snippet
    private const int CP_NOCLOSE_BUTTON = 0x200;
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams myCp = base.CreateParams;
            myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
            return myCp;
        }
    }

    public frmMainAdvance()
    {
        InitializeComponent();
    }

    public void InitializeFormData()
    {
        frmNotification frmNotification = new frmNotification();
        frmNotification.MdiParent = this;
        frmNotification.Show();

        frmMainMenu frmMainMenu = new frmMainMenu();
        frmMainMenu.MdiParent = this;
        frmMainMenu.Show();
    }

    private void frmMainAdvance_Load(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Maximized;

        frmLogin frmLogin = new frmLogin();
        frmLogin.MdiParent = this;
        frmLogin.Show();

    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
