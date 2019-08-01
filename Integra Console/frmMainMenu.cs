using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmMainMenu : Form
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

    public frmMainMenu()
    {
        InitializeComponent();
        PerFormControl(false);

        //pnlVisible(false);
        //pnlPayroll.Dock = DockStyle.Fill;
        //pnlEmployeeData.Dock = DockStyle.Fill;
        //pnlTimekeeping.Dock = DockStyle.Fill;
        //pnlTableConfig.Dock = DockStyle.Fill;
    }

    //private void pnlVisible(bool _value)
    //{
    //    pnlEmployeeData.Visible = _value;
    //    pnlTimekeeping.Visible = _value;
    //    pnlPayroll.Visible = _value;
    //    pnlTableConfig.Visible = _value;
    //    pnlReports.Visible = _value;
    //}


    private void PerFormControl(bool _value)
    {
        #region Form Control
        //int i = 0;
        foreach (Control cLayer1 in this.Controls)
        {
            if (cLayer1 is Panel)
            {
                ((Panel)cLayer1).Visible = _value;
                ((Panel)cLayer1).Dock = DockStyle.Fill;
            }

            foreach (Control cLayer2 in cLayer1.Controls)
            {
                if (cLayer2 is Panel)
                {
                    ((Panel)cLayer2).Visible = _value;
                    ((Panel)cLayer2).Dock = DockStyle.Fill;
                }
            }
            
        }
        #endregion
    }

    private void PerFormControl_PanelReport(bool _value)
    {
        #region Form Control
        //int i = 0;
        foreach (Control cLayer1 in this.Controls)
        {
            foreach (Control cLayer2 in cLayer1.Controls)
            {
                if (cLayer2 is Panel)
                {
                    ((Panel)cLayer2).Visible = _value;
                    ((Panel)cLayer2).Dock = DockStyle.Fill;
                }
            }
        }
        #endregion
    }



    private void frmMainMenu_Load(object sender, EventArgs e)
    {


    }

    private void tsPayroll_Click(object sender, EventArgs e)
    {
        PerFormControl(false);
        pnlPayroll.Visible = true;
    }

    private void tsEmployeeData_Click(object sender, EventArgs e)
    {
        PerFormControl(false);
        pnlEmployeeData.Visible = true;

    }

    private void tsTimekeeping_Click(object sender, EventArgs e)
    {
        PerFormControl(false);
        pnlTimekeeping.Visible = true;
    }

    private void tsTableConfig_Click(object sender, EventArgs e)
    {
        PerFormControl(false);
        pnlTableConfig.Visible = true;

    }
    private void tsReports_Click(object sender, EventArgs e)
    {
        PerFormControl(false);
        pnlReports.Visible = true;

    }
    private void btnEmployeeMasterData_Click(object sender, EventArgs e)
    {
        if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, btnEmployeeMasterData.Text) == "No Access")
        {
            MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
            return;
        }

        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(frmEmployeeMasterData))
            {
                form.Activate();
                return;
            }
        }
        frmEmployeeMasterData frmEmployeeMasterData = new frmEmployeeMasterData();
        frmEmployeeMasterData.MdiParent = ((frmMainAdvance)(this.MdiParent));
        frmEmployeeMasterData.Show();

        //if (clsDeclaration.sSuperUser == "1")
        //{
        //    foreach (Form form in Application.OpenForms)
        //    {
        //        if (form.GetType() == typeof(frmEmployeeMasterData))
        //        {
        //            form.Activate();
        //            return;
        //        }
        //    }

        //    frmEmployeeMasterData frmEmployeeMasterData = new frmEmployeeMasterData();
        //    frmEmployeeMasterData.MdiParent = this;
        //    frmEmployeeMasterData.Show();
        //}
        //else
        //{
        //    foreach (Form form in Application.OpenForms)
        //    {
        //        if (form.GetType() == typeof(frmMasterData))
        //        {
        //            form.Activate();
        //            return;
        //        }
        //    }

        //    frmMasterData frmMasterData = new frmMasterData();
        //    frmMasterData.MdiParent = ((frmMainAdvance)(this.MdiParent));
        //    frmMasterData.Show();
        //}
    }

    private void btnLoanFile_Click(object sender, EventArgs e)
    {
        if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, "Loan File") == "No Access")
        {
            MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
            return;
        }

        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(frmLoanFileData))
            {
                form.Activate();
                return;
            }
        }

        frmLoanFileData frmLoanFileData = new frmLoanFileData();
        frmLoanFileData.MdiParent = ((frmMainAdvance)(this.MdiParent));
        frmLoanFileData.Show();
    }

    private void btnUploadWorkDays_Click(object sender, EventArgs e)
    {
        if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, "Working Hours") == "No Access")
        {
            MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
            return;
        }

        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(frmWorkingHours))
            {
                form.Activate();
                return;
            }
        }

        frmWorkingHours frmWorkingHours = new frmWorkingHours();
        frmWorkingHours.MdiParent = ((frmMainAdvance)(this.MdiParent));
        frmWorkingHours.Show();

    }

    private void btnLeaveGeneration_Click(object sender, EventArgs e)
    {
        if (clsFunctions.FormAccess(clsDeclaration.sLoginUserID, "Leave Generation") == "No Access")
        {
            MessageBox.Show("You Don't Have An Access To This Module. Please Contact Your System Addminitrator.");
            return;
        }

        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(frmLeaveGeneration))
            {
                form.Activate();
                return;
            }
        }

        frmLeaveGeneration frmLeaveGeneration = new frmLeaveGeneration();
        frmLeaveGeneration.MdiParent = ((frmMainAdvance)(this.MdiParent));
        frmLeaveGeneration.Show();
    }

    private void tsRMasterData_Click(object sender, EventArgs e)
    {
        PerFormControl_PanelReport(false);
        pnlRMasterData.Visible = true;
    }

    private void tsRLoanReport_Click(object sender, EventArgs e)
    {
        PerFormControl_PanelReport(false);
        pnlRLoanReport.Visible = true;
    }

    private void tsRTimekeeping_Click(object sender, EventArgs e)
    {
        PerFormControl_PanelReport(false);
        pnlRTimekeeping.Visible = true;
    }

    private void tsRPayroll_Click(object sender, EventArgs e)
    {
        PerFormControl_PanelReport(false);
        pnlRPayroll.Visible = true;
    }

    private void tsSystemConfig_Click(object sender, EventArgs e)
    {
        PerFormControl(false);
        pnlSystemConfig.Visible = true;
    }

    private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }
}