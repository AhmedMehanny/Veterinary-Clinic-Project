//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace PresentationLayer
//{
//    public partial class FormMain : Form
//    {
//        public FormMain()
//        {
//            InitializeComponent();
//        }

//        private void FormMain_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}

using System;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class FormMain : Form
    {
        private UC_Dashboard ucDashboard;
        private UC_Owners ucOwners;
        private UC_Pets ucPets;
        private UC_Visits ucVisits;
        private UC_Vaccinations ucVaccinations;
        private UC_Reports ucReports;

        public FormMain()
        {
            InitializeComponent();
            LoadUserControls();
            ShowDashboard();
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new Panel();
            this.btnReports = new Button();
            this.btnVaccinations = new Button();
            this.btnVisits = new Button();
            this.btnPets = new Button();
            this.btnOwners = new Button();
            this.btnDashboard = new Button();
            this.pnlMain = new Panel();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();

            // pnlSidebar
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.pnlSidebar.Controls.Add(this.btnReports);
            this.pnlSidebar.Controls.Add(this.btnVaccinations);
            this.pnlSidebar.Controls.Add(this.btnVisits);
            this.pnlSidebar.Controls.Add(this.btnPets);
            this.pnlSidebar.Controls.Add(this.btnOwners);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Size = new System.Drawing.Size(200, this.ClientSize.Height);

            // Buttons styling
            void StyleButton(Button btn, string text, int y)
            {
                btn.Text = text;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
                btn.ForeColor = System.Drawing.Color.White;
                btn.Font = new System.Drawing.Font("Segoe UI", 10F);
                btn.Size = new System.Drawing.Size(200, 45);
                btn.Location = new System.Drawing.Point(0, y);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(20, 0, 0, 0);
                btn.Click += (s, e) => LoadUserControl(s);
            }

            StyleButton(this.btnDashboard, " Dashboard", 10);
            StyleButton(this.btnOwners, " Owners", 60);
            StyleButton(this.btnPets, " Pets", 110);
            StyleButton(this.btnVisits, " Visits", 160);
            StyleButton(this.btnVaccinations, " Vaccinations", 210);
            StyleButton(this.btnReports, " Reports", 260);

            // pnlMain
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.BackColor = System.Drawing.Color.White;

            // FormMain
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSidebar);
            this.Text = "Veterinary Clinic Management System";
            this.WindowState = FormWindowState.Maximized;

            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void LoadUserControls()
        {
            ucDashboard = new UC_Dashboard();
            ucOwners = new UC_Owners();
            ucPets = new UC_Pets();
            ucVisits = new UC_Visits();
            ucVaccinations = new UC_Vaccinations();
            ucReports = new UC_Reports();
        }

        private void ShowDashboard()
        {
            pnlMain.Controls.Clear();
            ucDashboard.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(ucDashboard);
        }

        private void LoadUserControl(object sender)
        {
            Button btn = sender as Button;
            pnlMain.Controls.Clear();
            if (btn.Text.Contains("Dashboard")) pnlMain.Controls.Add(ucDashboard);
            else if (btn.Text.Contains("Owners")) pnlMain.Controls.Add(ucOwners);
            else if (btn.Text.Contains("Pets")) pnlMain.Controls.Add(ucPets);
            else if (btn.Text.Contains("Visits")) pnlMain.Controls.Add(ucVisits);
            else if (btn.Text.Contains("Vaccinations")) pnlMain.Controls.Add(ucVaccinations);
            else if (btn.Text.Contains("Reports")) pnlMain.Controls.Add(ucReports);

            if (pnlMain.Controls.Count > 0)
                pnlMain.Controls[0].Dock = DockStyle.Fill;
        }

        private Panel pnlSidebar;
        private Button btnDashboard, btnOwners, btnPets, btnVisits, btnVaccinations, btnReports;
        private Panel pnlMain;
    }
}