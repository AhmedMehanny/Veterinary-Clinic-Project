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
//    public partial class UC_Dashboard : UserControl
//    {
//        public UC_Dashboard()
//        {
//            InitializeComponent();
//        }

//        private void UC_Dashboard_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}

using BusinessLogicLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class UC_Dashboard : UserControl
    {
        private readonly VisitManager visitManager;
        private readonly VaccinationManager vaccinationManager;
        private readonly ClinicManager clinicManager; // assume exists

        public UC_Dashboard()
        {
            InitializeComponent();
            visitManager = new VisitManager();
            vaccinationManager = new VaccinationManager();
            clinicManager = new ClinicManager();
            LoadDashboardData();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblTotalVisits = new Label();
            this.lblOverdueBoosters = new Label();
            this.lblTotalClinics = new Label();
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.panel3 = new Panel();
            this.SuspendLayout();

            this.lblTitle.Text = "Dashboard";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);

            // Stat cards
            void CreateCard(Panel panel, Label lblValue, string title, int x)
            {
                panel.BackColor = System.Drawing.Color.White;
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Size = new System.Drawing.Size(250, 120);
                panel.Location = new System.Drawing.Point(x, 100);
                Label lblTitle = new Label() { Text = title, Font = new System.Drawing.Font("Segoe UI", 12F), Location = new System.Drawing.Point(15, 15), AutoSize = true };
                lblValue.Font = new System.Drawing.Font("Segoe UI", 24F, FontStyle.Bold);
                lblValue.Location = new System.Drawing.Point(15, 50);
                lblValue.AutoSize = true;
                panel.Controls.Add(lblTitle);
                panel.Controls.Add(lblValue);
                this.Controls.Add(panel);
            }

            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            CreateCard(panel1, lblTotalVisits = new Label(), "Total Visits (Month)", 30);
            CreateCard(panel2, lblOverdueBoosters = new Label(), "Overdue Boosters", 300);
            CreateCard(panel3, lblTotalClinics = new Label(), "Clinics", 570);

            this.Controls.Add(lblTitle);
            this.ResumeLayout(false);
        }

        private void LoadDashboardData()
        {
            // Example: get total visits this month
            var visits = visitManager.GetAllVisits();
            int thisMonthVisits = visits.FindAll(v => v.VisitDate.Month == DateTime.Today.Month && v.VisitDate.Year == DateTime.Today.Year).Count;
            lblTotalVisits.Text = thisMonthVisits.ToString();

            var overdue = vaccinationManager.GetOverdueBoosters();
            lblOverdueBoosters.Text = overdue.Count.ToString();

            var clinics = clinicManager.GetAllClinics();
            lblTotalClinics.Text = clinics.Count.ToString();
        }

        private Label lblTitle, lblTotalVisits, lblOverdueBoosters, lblTotalClinics;
        private Panel panel1, panel2, panel3;
    }
}