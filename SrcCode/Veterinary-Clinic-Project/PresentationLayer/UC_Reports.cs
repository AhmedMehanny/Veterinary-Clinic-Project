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
//    public partial class UC_Reports : UserControl
//    {
//        public UC_Reports()
//        {
//            InitializeComponent();
//        }

//        private void UC_Reports_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}

using System;
using System.Windows.Forms;
using BusinessLogicLayer;

namespace PresentationLayer
{
    public partial class UC_Reports : UserControl
    {
        private readonly ReportManager _reportManager;
        private ComboBox cboReportType;
        private DateTimePicker dtpStart, dtpEnd;
        private Button btnGenerate;
        private DataGridView dgvReport;

        public UC_Reports()
        {
            InitializeComponent();
            _reportManager = new ReportManager();
            LoadReportTypes();
        }

        private void InitializeComponent()
        {
            this.cboReportType = new ComboBox();
            this.dtpStart = new DateTimePicker();
            this.dtpEnd = new DateTimePicker();
            this.btnGenerate = new Button();
            this.dgvReport = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            // cboReportType
            this.cboReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboReportType.Location = new System.Drawing.Point(30, 30);
            this.cboReportType.Size = new System.Drawing.Size(250, 28);
            this.cboReportType.SelectedIndexChanged += CboReportType_SelectedIndexChanged;

            // dtpStart, dtpEnd
            this.dtpStart.Format = DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(320, 30);
            this.dtpStart.Size = new System.Drawing.Size(120, 22);
            this.dtpEnd.Location = new System.Drawing.Point(460, 30);
            this.dtpEnd.Size = new System.Drawing.Size(120, 22);

            Label lblTo = new Label() { Text = "to", Location = new System.Drawing.Point(445, 33), AutoSize = true };
            this.Controls.Add(lblTo);

            // btnGenerate
            this.btnGenerate.Text = "Generate Report";
            this.btnGenerate.Location = new System.Drawing.Point(620, 28);
            this.btnGenerate.Size = new System.Drawing.Size(150, 30);
            this.btnGenerate.Click += BtnGenerate_Click;

            // dgvReport
            this.dgvReport.Location = new System.Drawing.Point(30, 80);
            this.dgvReport.Size = new System.Drawing.Size(900, 450);
            this.dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.ReadOnly = true;

            this.Controls.Add(cboReportType);
            this.Controls.Add(dtpStart);
            this.Controls.Add(dtpEnd);
            this.Controls.Add(btnGenerate);
            this.Controls.Add(dgvReport);

            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadReportTypes()
        {
            cboReportType.Items.Clear();
            cboReportType.Items.Add("Booster Due (Next 30 days)");
            cboReportType.Items.Add("Clinic Visit Statistics (Date Range)");
            cboReportType.Items.Add("Low Stock Inventory");
            cboReportType.Items.Add("Owner-Pet Summary");
            cboReportType.Items.Add("Clinic Revenue (Date Range)");
            cboReportType.SelectedIndex = 0;
        }

        private void CboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable/disable date pickers based on report type
            bool needsDateRange = cboReportType.SelectedIndex == 1 || cboReportType.SelectedIndex == 4;
            dtpStart.Enabled = dtpEnd.Enabled = needsDateRange;
            if (!needsDateRange)
            {
                dtpStart.Value = DateTime.Today.AddMonths(-1);
                dtpEnd.Value = DateTime.Today;
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                dgvReport.DataSource = null;
                switch (cboReportType.SelectedIndex)
                {
                    case 0: // Booster due
                        var boosterList = _reportManager.GetBoosterDueReport(30);
                        dgvReport.DataSource = boosterList;
                        break;
                    case 1: // Clinic visit statistics
                        var stats = _reportManager.GetClinicVisitStatistics(dtpStart.Value, dtpEnd.Value);
                        dgvReport.DataSource = stats;
                        break;
                    case 2: // Low stock
                        var lowStock = _reportManager.GetLowStockReport();
                        dgvReport.DataSource = lowStock;
                        break;
                    case 3: // Owner-Pet summary
                        var summary = _reportManager.GetOwnerPetSummary();
                        dgvReport.DataSource = summary;
                        break;
                    case 4: // Clinic revenue
                        var revenue = _reportManager.GetClinicRevenueReport(dtpStart.Value, dtpEnd.Value);
                        dgvReport.DataSource = revenue;
                        break;
                }
                if (dgvReport.DataSource != null)
                    ShowToast("Report generated", "success");
            }
            catch (Exception ex)
            {
                ShowToast($"Error: {ex.Message}", "error");
            }
        }

        private void ShowToast(string msg, string type) => new frmToast(msg, type).Show();
    }
}