using System;
using System.Windows.Forms;
using BusinessLogicLayer;

namespace PresentationLayer
{
    public partial class FormMain : Form
    {
        // مراجع لشاشات المستخدم (User Controls) – سننشئها لاحقاً
        private UC_Dashboard ucDashboard;
        private UC_Owners ucOwners;
        private UC_Pets ucPets;
        private UC_Visits ucVisits;
        private UC_Vaccinations ucVaccinations;
        private UC_Reports ucReports;

        public FormMain()
        {
            InitializeComponent(); // مهم جداً – هذا الكود أنشأه المصمم
            LoadUserControls();
            ShowDashboard();
            AttachButtonEvents();
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

        private void AttachButtonEvents()
        {
            btnDashboard.Click += (s, e) => ShowControl(ucDashboard);
            btnOwner.Click += (s, e) => ShowControl(ucOwners);// خلي بالك من الاسم دلوقتي
            btnPets.Click += (s, e) => ShowControl(ucPets);
            btnVisits.Click += (s, e) => ShowControl(ucVisits);
            btnVaccinations.Click += (s, e) => ShowControl(ucVaccinations);
            btnReports.Click += (s, e) => ShowControl(ucReports);
        }

        private void ShowDashboard()
        {
            ShowControl(ucDashboard);
        }

        private void ShowControl(UserControl uc)
        {
            pnlMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(uc);
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}