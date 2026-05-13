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
//    public partial class UC_Vaccinations : UserControl
//    {
//        public UC_Vaccinations()
//        {
//            InitializeComponent();
//        }

//        private void UC_Vaccinations_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}


using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using Models;

namespace PresentationLayer
{
    public partial class UC_Vaccinations : UserControl
    {
        private readonly VaccinationManager _vaccinationManager;
        private readonly VisitManager _visitManager;
        private DataGridView dgvVaccinations;
        private ComboBox cboVisit;
        private TextBox txtInventoryId, txtVaccineType, txtBatchNumber, txtSupplier;
        private DateTimePicker dtpAdministered, dtpBoosterDue;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;

        public UC_Vaccinations()
        {
            InitializeComponent();
            _vaccinationManager = new VaccinationManager();
            _visitManager = new VisitManager();
            LoadVisits();
            LoadVaccinations();
        }

        private void InitializeComponent()
        {
            this.dgvVaccinations = new DataGridView();
            this.cboVisit = new ComboBox();
            this.txtInventoryId = new TextBox();
            this.txtVaccineType = new TextBox();
            this.txtBatchNumber = new TextBox();
            this.txtSupplier = new TextBox();
            this.dtpAdministered = new DateTimePicker();
            this.dtpBoosterDue = new DateTimePicker();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccinations)).BeginInit();
            this.SuspendLayout();

            this.dgvVaccinations.Location = new System.Drawing.Point(20, 20);
            this.dgvVaccinations.Size = new System.Drawing.Size(900, 280);
            this.dgvVaccinations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvVaccinations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVaccinations.SelectionChanged += DgvVaccinations_SelectionChanged;

            int y = 320;
            void AddField(string labelText, Control control, int yPos)
            {
                Label lbl = new Label() { Text = labelText, Location = new System.Drawing.Point(20, yPos), AutoSize = true };
                control.Location = new System.Drawing.Point(150, yPos - 3);
                control.Width = 220;
                this.Controls.Add(lbl);
                this.Controls.Add(control);
            }

            AddField("Visit:", cboVisit, y); y += 30;
            AddField("Inventory ID:", txtInventoryId, y); y += 30;
            AddField("Vaccine Type:", txtVaccineType, y); y += 30;
            AddField("Batch Number:", txtBatchNumber, y); y += 30;
            AddField("Supplier:", txtSupplier, y); y += 30;
            AddField("Administered Date:", dtpAdministered, y); y += 30;
            AddField("Next Booster Due:", dtpBoosterDue, y); y += 30;

            cboVisit.DisplayMember = "Display";
            cboVisit.ValueMember = "VisitId";
            dtpAdministered.Format = DateTimePickerFormat.Short;
            dtpBoosterDue.Format = DateTimePickerFormat.Short;

            this.btnAdd.Text = "Add"; this.btnAdd.Location = new System.Drawing.Point(420, 320); this.btnAdd.Click += BtnAdd_Click;
            this.btnUpdate.Text = "Update"; this.btnUpdate.Location = new System.Drawing.Point(420, 350); this.btnUpdate.Click += BtnUpdate_Click;
            this.btnDelete.Text = "Delete"; this.btnDelete.Location = new System.Drawing.Point(420, 380); this.btnDelete.Click += BtnDelete_Click;
            this.btnClear.Text = "Clear"; this.btnClear.Location = new System.Drawing.Point(420, 410); this.btnClear.Click += (s, e) => ClearFields();

            this.Controls.Add(this.dgvVaccinations);
            this.Controls.Add(btnAdd); this.Controls.Add(btnUpdate); this.Controls.Add(btnDelete); this.Controls.Add(btnClear);

            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccinations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadVisits()
        {
            var visits = _visitManager.GetAllVisits();
            var list = visits.ConvertAll(v => new { VisitId = v.VisitId, Display = $"Visit #{v.VisitId} - {v.PetName} ({v.VisitDate:yyyy-MM-dd})" });
            cboVisit.DataSource = list;
        }

        private void LoadVaccinations()
        {
            var vaccs = _vaccinationManager.GetAllVaccinations();
            dgvVaccinations.DataSource = null;
            dgvVaccinations.DataSource = vaccs;
            dgvVaccinations.Columns["VaccinationId"].Visible = true;
            dgvVaccinations.Columns["VaccinationInfo"].Visible = false;
            dgvVaccinations.Columns["BoosterStatus"].Visible = false;
            dgvVaccinations.Columns["IsBoosterDue"].Visible = false;
            dgvVaccinations.Columns["IsBoosterExpiring"].Visible = false;
        }

        private void DgvVaccinations_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVaccinations.SelectedRows.Count > 0)
            {
                var row = dgvVaccinations.SelectedRows[0];
                cboVisit.SelectedValue = Convert.ToInt32(row.Cells["VisitId"].Value);
                txtInventoryId.Text = row.Cells["InventoryId"].Value.ToString();
                txtVaccineType.Text = row.Cells["VaccineType"].Value.ToString();
                txtBatchNumber.Text = row.Cells["BatchNumber"].Value?.ToString();
                txtSupplier.Text = row.Cells["SupplierName"].Value?.ToString();
                dtpAdministered.Value = Convert.ToDateTime(row.Cells["AdministeredDate"].Value);
                if (row.Cells["NextBoosterDue"].Value != DBNull.Value)
                    dtpBoosterDue.Value = Convert.ToDateTime(row.Cells["NextBoosterDue"].Value);
                else
                    dtpBoosterDue.Value = DateTime.Today.AddYears(1);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var vacc = new Vaccination
                {
                    VisitId = (int)cboVisit.SelectedValue,
                    InventoryId = int.Parse(txtInventoryId.Text),
                    VaccineType = txtVaccineType.Text,
                    AdministeredDate = dtpAdministered.Value,
                    NextBoosterDue = dtpBoosterDue.Checked ? dtpBoosterDue.Value : (DateTime?)null
                };
                if (_vaccinationManager.AddVaccination(vacc))
                {
                    ShowToast("Vaccination recorded", "success");
                    LoadVaccinations();
                    ClearFields();
                }
            }
            catch (Exception ex) { ShowToast(ex.Message, "error"); }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvVaccinations.SelectedRows.Count == 0) return;
            int id = (int)dgvVaccinations.SelectedRows[0].Cells["VaccinationId"].Value;
            try
            {
                var vacc = new Vaccination
                {
                    VaccinationId = id,
                    VisitId = (int)cboVisit.SelectedValue,
                    InventoryId = int.Parse(txtInventoryId.Text),
                    VaccineType = txtVaccineType.Text,
                    AdministeredDate = dtpAdministered.Value,
                    NextBoosterDue = dtpBoosterDue.Value
                };
                if (_vaccinationManager.UpdateVaccination(vacc))
                {
                    ShowToast("Vaccination updated", "success");
                    LoadVaccinations();
                }
            }
            catch (Exception ex) { ShowToast(ex.Message, "error"); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvVaccinations.SelectedRows.Count == 0) return;
            int id = (int)dgvVaccinations.SelectedRows[0].Cells["VaccinationId"].Value;
            if (MessageBox.Show("Delete this vaccination record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // No direct delete in manager – you may implement. For now, show not implemented.
                ShowToast("Delete not implemented in BLL – use admin tools", "warning");
            }
        }

        private void ClearFields()
        {
            cboVisit.SelectedIndex = -1;
            txtInventoryId.Text = txtVaccineType.Text = txtBatchNumber.Text = txtSupplier.Text = "";
            dtpAdministered.Value = DateTime.Today;
            dtpBoosterDue.Value = DateTime.Today.AddYears(1);
        }

        private void ShowToast(string msg, string type) => new frmToast(msg, type).Show();
    }
}