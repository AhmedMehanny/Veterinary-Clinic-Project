using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using Models;

namespace PresentationLayer
{
    public partial class UC_Vaccinations : UserControl
    {
        private readonly VaccinationManager _vaccManager = new VaccinationManager();
        private readonly VisitManager _visitManager = new VisitManager();

        public UC_Vaccinations()
        {
            InitializeComponent();
            LoadVisits();
            LoadVaccinations();
        }

        private void LoadVisits()
        {
            var visits = _visitManager.GetAllVisits();
            cboVisit.DataSource = visits;
            cboVisit.DisplayMember = "VisitId"; // أو "VisitInfo"
            cboVisit.ValueMember = "VisitId";
        }

        private void LoadVaccinations()
        {
            dgvVaccinations.DataSource = null;
            dgvVaccinations.DataSource = _vaccManager.GetAllVaccinations();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var vacc = new Vaccination
                {
                    VisitId = (int)cboVisit.SelectedValue,
                    InventoryId = int.Parse(txtInventoryId.Text),
                    VaccineType = txtVaccineType.Text,
                    AdministeredDate = dtpAdministered.Value,
                    NextBoosterDue = dtpBoosterDue.Value
                };
                if (_vaccManager.AddVaccination(vacc))
                {
                    MessageBox.Show("Vaccination added.");
                    LoadVaccinations();
                    ClearFields();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                if (_vaccManager.UpdateVaccination(vacc)) LoadVaccinations();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // يمكن تنفيذ الحذف إذا أضفت دالة Delete في VaccinationManager
            MessageBox.Show("Delete not implemented.");
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearFields();

        private void ClearFields()
        {
            cboVisit.SelectedIndex = -1;
            txtInventoryId.Text = "";
            txtVaccineType.Text = "";
            dtpAdministered.Value = DateTime.Today;
            dtpBoosterDue.Value = DateTime.Today.AddYears(1);
        }

        private void dgvVaccinations_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVaccinations.SelectedRows.Count == 0) return;
            var row = dgvVaccinations.SelectedRows[0];
            cboVisit.SelectedValue = (int)row.Cells["VisitId"].Value;
            txtInventoryId.Text = row.Cells["InventoryId"].Value.ToString();
            txtVaccineType.Text = row.Cells["VaccineType"].Value.ToString();
            dtpAdministered.Value = (DateTime)row.Cells["AdministeredDate"].Value;
            if (row.Cells["NextBoosterDue"].Value != DBNull.Value)
                dtpBoosterDue.Value = (DateTime)row.Cells["NextBoosterDue"].Value;
        }
    }
}