using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace VetClinicApp
{
    public partial class Form1 : Form
    {
        private readonly OwnerManager _ownerMgr = new OwnerManager();
        private readonly PetManager _petMgr = new PetManager();
        private readonly VisitManager _visitMgr = new VisitManager();
        private readonly VaccinationManager _vacMgr = new VaccinationManager();
        private readonly ReportManager _reportMgr = new ReportManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDashboard();
            LoadOwners();
            LoadPets();
            LoadOwnerCombo();
            LoadVisits();
            LoadVisitCombos();
            LoadVaccinations();
            LoadVaccinationCombos();
            ShowPanel(pnlDashboard);
        }

        // ─────────────────────────────────────
        // NAVIGATION
        // ─────────────────────────────────────
        private void ShowPanel(Panel panel)
        {
            pnlDashboard.Visible = false;
            pnlOwners.Visible = false;
            pnlPets.Visible = false;
            pnlVisits.Visible = false;
            pnlVaccinations.Visible = false;
            pnlReports.Visible = false;
            panel.Visible = true;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadDashboard();
            ShowPanel(pnlDashboard);
        }

        private void btnOwners_Click(object sender, EventArgs e)
        {
            LoadOwners();
            ShowPanel(pnlOwners);
        }

        private void btnPets_Click(object sender, EventArgs e)
        {
            LoadPets();
            ShowPanel(pnlPets);
        }

        private void btnVisits_Click(object sender, EventArgs e)
        {
            LoadVisits();
            ShowPanel(pnlVisits);
        }

        private void btnVaccinations_Click(object sender, EventArgs e)
        {
            LoadVaccinations();
            ShowPanel(pnlVaccinations);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlReports);
        }

        // ─────────────────────────────────────
        // SIDEBAR HOVER EFFECTS
        // ─────────────────────────────────────
        private void NavButton_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.FromArgb(46, 74, 122);
        }

        private void NavButton_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.FromArgb(27, 42, 74);
        }

        // ─────────────────────────────────────
        // DASHBOARD
        // ─────────────────────────────────────
        private void LoadDashboard()
        {
            try
            {
                lblOwnerCount.Text = DBHandler.ExecuteScalar("SELECT COUNT(*) FROM OWNER")?.ToString() ?? "0";
                lblPetCount.Text = DBHandler.ExecuteScalar("SELECT COUNT(*) FROM PET")?.ToString() ?? "0";
                lblVisitCount.Text = DBHandler.ExecuteScalar("SELECT COUNT(*) FROM MEDICAL_VISIT")?.ToString() ?? "0";
                lblReminderCount.Text = DBHandler.ExecuteScalar("SELECT COUNT(*) FROM REMINDER WHERE SCHEDULEDDATE >= GETDATE()")?.ToString() ?? "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────
        // OWNERS
        // ─────────────────────────────────────
        private void LoadOwners()
        {
            try
            {
                dgvOwners.DataSource = _ownerMgr.GetAllOwners();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearOwnerFields()
        {
            nudOwnerID.Value = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtBilling.Text = string.Empty;
            txtEmergency.Text = string.Empty;
            txtOwnerSearch.Text = string.Empty;
        }

        private void dgvOwners_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOwners.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvOwners.SelectedRows[0];
            try
            {
                nudOwnerID.Value = Convert.ToDecimal(row.Cells["OWNERID"].Value);
                txtFirstName.Text = row.Cells["OFRISTNAME"].Value?.ToString();
                txtLastName.Text = row.Cells["OLASTNAME"].Value?.ToString();
                txtPhone.Text = row.Cells["OPHONE"].Value?.ToString();
                txtEmail.Text = row.Cells["OEMAIL"].Value?.ToString();
                txtBilling.Text = row.Cells["BILLINGADDRESS"].Value?.ToString();
                txtEmergency.Text = row.Cells["EMERGENCYCONTACT"].Value?.ToString();
            }
            catch { }
        }

        private void btnOwnerAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Owner o = new Owner
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    BillingAddress = txtBilling.Text.Trim(),
                    EmergencyContact = txtEmergency.Text.Trim()
                };
                string result = _ownerMgr.AddOwner(o);
                if (result.Contains("successfully"))
                {
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOwners();
                    ClearOwnerFields();
                }
                else
                {
                    MessageBox.Show(result, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOwnerUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Owner o = new Owner
                {
                    OwnerID = (int)nudOwnerID.Value,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    BillingAddress = txtBilling.Text.Trim(),
                    EmergencyContact = txtEmergency.Text.Trim()
                };
                string result = _ownerMgr.UpdateOwner(o);
                if (result.Contains("successfully"))
                {
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOwners();
                    ClearOwnerFields();
                }
                else
                {
                    MessageBox.Show(result, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOwnerDelete_Click(object sender, EventArgs e)
        {
            if (nudOwnerID.Value == 0)
            {
                MessageBox.Show("Please select an owner to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this owner?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string result = _ownerMgr.DeleteOwner((int)nudOwnerID.Value);
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOwners();
                    ClearOwnerFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOwnerClear_Click(object sender, EventArgs e)
        {
            ClearOwnerFields();
        }

        private void btnOwnerSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string search = txtOwnerSearch.Text.Trim();
                string phone = string.Empty;
                string email = string.Empty;
                if (search.Contains("@"))
                    email = search;
                else
                    phone = search;

                dgvOwners.DataSource = _ownerMgr.SearchOwners(phone, email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────
        // PETS
        // ─────────────────────────────────────
        private void LoadPets()
        {
            try
            {
                dgvPets.DataSource = _petMgr.GetPetsWithOwners();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOwnerCombo()
        {
            try
            {
                DataTable owners = _ownerMgr.GetAllOwners();
                DataTable display = new DataTable();
                display.Columns.Add("OWNERID");
                display.Columns.Add("OwnerName");
                foreach (DataRow row in owners.Rows)
                {
                    display.Rows.Add(
                        row["OWNERID"].ToString(),
                        row["OFRISTNAME"].ToString() + " " + row["OLASTNAME"].ToString()
                    );
                }
                cmbOwnerID.DataSource = display;
                cmbOwnerID.DisplayMember = "OwnerName";
                cmbOwnerID.ValueMember = "OWNERID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearPetFields()
        {
            nudPetID.Value = 0;
            txtPetName.Text = string.Empty;
            txtBreed.Text = string.Empty;
            nudAge.Value = 0;
            cmbSpecies.SelectedIndex = -1;
            if (cmbOwnerID.Items.Count > 0) cmbOwnerID.SelectedIndex = 0;
        }

        private void dgvPets_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPets.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvPets.SelectedRows[0];
            try
            {
                nudPetID.Value = Convert.ToDecimal(row.Cells["PETID"].Value);
                txtPetName.Text = row.Cells["PETNAME"].Value?.ToString();
                txtBreed.Text = row.Cells["BREED"].Value?.ToString();
                nudAge.Value = Convert.ToDecimal(row.Cells["AGE"].Value);
                string species = row.Cells["SPECIES"].Value?.ToString();
                int speciesIdx = cmbSpecies.FindStringExact(species);
                if (speciesIdx >= 0) cmbSpecies.SelectedIndex = speciesIdx;

                string ownerId = row.Cells["OWNERID"].Value?.ToString();
                foreach (DataRowView item in cmbOwnerID.Items)
                {
                    if (item["OWNERID"].ToString() == ownerId)
                    {
                        cmbOwnerID.SelectedItem = item;
                        break;
                    }
                }
            }
            catch { }
        }

        private void btnPetAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Pet p = new Pet
                {
                    OwnerID = cmbOwnerID.SelectedValue != null ? Convert.ToInt32(cmbOwnerID.SelectedValue) : 0,
                    PetName = txtPetName.Text.Trim(),
                    Species = cmbSpecies.SelectedItem?.ToString(),
                    Breed = txtBreed.Text.Trim(),
                    Age = (int)nudAge.Value
                };
                string result = _petMgr.AddPet(p);
                if (result.Contains("successfully"))
                {
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPets();
                    ClearPetFields();
                }
                else
                {
                    MessageBox.Show(result, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPetUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Pet p = new Pet
                {
                    PetID = (int)nudPetID.Value,
                    OwnerID = cmbOwnerID.SelectedValue != null ? Convert.ToInt32(cmbOwnerID.SelectedValue) : 0,
                    PetName = txtPetName.Text.Trim(),
                    Species = cmbSpecies.SelectedItem?.ToString(),
                    Breed = txtBreed.Text.Trim(),
                    Age = (int)nudAge.Value
                };
                string result = _petMgr.UpdatePet(p);
                if (result.Contains("successfully"))
                {
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPets();
                    ClearPetFields();
                }
                else
                {
                    MessageBox.Show(result, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPetDelete_Click(object sender, EventArgs e)
        {
            if (nudPetID.Value == 0)
            {
                MessageBox.Show("Please select a pet to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this pet?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string result = _petMgr.DeletePet((int)nudPetID.Value);
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPets();
                    ClearPetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPetClear_Click(object sender, EventArgs e)
        {
            ClearPetFields();
        }

        // ─────────────────────────────────────
        // VISITS
        // ─────────────────────────────────────
        private void LoadVisits()
        {
            try
            {
                dgvVisits.DataSource = _visitMgr.GetVisitsWithDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVisitCombos()
        {
            try
            {
                // Load pets combo
                DataTable pets = _petMgr.GetAllPets();
                cmbVisitPet.DataSource = pets;
                cmbVisitPet.DisplayMember = "PETNAME";
                cmbVisitPet.ValueMember = "PETID";

                // Load slots combo
                DataTable slots = DBHandler.ExecuteQuery("SELECT SLOTID, SLOTDATETIME FROM APPOINTMENT_SLOT ORDER BY SLOTDATETIME");
                DataTable slotDisplay = new DataTable();
                slotDisplay.Columns.Add("SLOTID");
                slotDisplay.Columns.Add("SlotLabel");
                foreach (DataRow row in slots.Rows)
                {
                    slotDisplay.Rows.Add(
                        row["SLOTID"].ToString(),
                        "Slot #" + row["SLOTID"].ToString() + " — " + Convert.ToDateTime(row["SLOTDATETIME"]).ToString("yyyy-MM-dd HH:mm")
                    );
                }
                cmbVisitSlot.DataSource = slotDisplay;
                cmbVisitSlot.DisplayMember = "SlotLabel";
                cmbVisitSlot.ValueMember = "SLOTID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearVisitFields()
        {
            nudVisitID.Value = 0;
            dtpVisitDate.Value = DateTime.Now;
            cmbVisitStatus.SelectedIndex = -1;
            if (cmbVisitPet.Items.Count > 0) cmbVisitPet.SelectedIndex = 0;
            if (cmbVisitSlot.Items.Count > 0) cmbVisitSlot.SelectedIndex = 0;
        }

        private void dgvVisits_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVisits.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvVisits.SelectedRows[0];
            try
            {
                nudVisitID.Value = Convert.ToDecimal(row.Cells["VISITID"].Value);
                dtpVisitDate.Value = Convert.ToDateTime(row.Cells["VISITDATE"].Value);
                string status = row.Cells["VISITSTATUS"].Value?.ToString();
                int statusIdx = cmbVisitStatus.FindStringExact(status);
                if (statusIdx >= 0) cmbVisitStatus.SelectedIndex = statusIdx;
            }
            catch { }
        }

        private void btnVisitAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Visit v = new Visit
                {
                    PetID = cmbVisitPet.SelectedValue != null ? Convert.ToInt32(cmbVisitPet.SelectedValue) : 0,
                    SlotID = cmbVisitSlot.SelectedValue != null ? Convert.ToInt32(cmbVisitSlot.SelectedValue) : 0,
                    VisitDate = dtpVisitDate.Value,
                    VisitStatus = cmbVisitStatus.SelectedItem?.ToString()
                };
                string result = _visitMgr.AddVisit(v);
                if (result.Contains("successfully"))
                {
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVisits();
                    ClearVisitFields();
                }
                else
                {
                    MessageBox.Show(result, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVisitUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Visit v = new Visit
                {
                    VisitID = (int)nudVisitID.Value,
                    PetID = cmbVisitPet.SelectedValue != null ? Convert.ToInt32(cmbVisitPet.SelectedValue) : 0,
                    SlotID = cmbVisitSlot.SelectedValue != null ? Convert.ToInt32(cmbVisitSlot.SelectedValue) : 0,
                    VisitDate = dtpVisitDate.Value,
                    VisitStatus = cmbVisitStatus.SelectedItem?.ToString()
                };
                string result = _visitMgr.UpdateVisit(v);
                if (result.Contains("successfully"))
                {
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVisits();
                    ClearVisitFields();
                }
                else
                {
                    MessageBox.Show(result, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVisitUpdateStatus_Click(object sender, EventArgs e)
        {
            if (nudVisitID.Value == 0)
            {
                MessageBox.Show("Please select a visit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string result = _visitMgr.UpdateVisitStatus((int)nudVisitID.Value, cmbVisitStatus.SelectedItem?.ToString());
                if (result.Contains("successfully"))
                {
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVisits();
                }
                else
                {
                    MessageBox.Show(result, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVisitDelete_Click(object sender, EventArgs e)
        {
            if (nudVisitID.Value == 0)
            {
                MessageBox.Show("Please select a visit to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this visit?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string result = _visitMgr.DeleteVisit((int)nudVisitID.Value);
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVisits();
                    ClearVisitFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVisitClear_Click(object sender, EventArgs e)
        {
            ClearVisitFields();
        }

        // ─────────────────────────────────────
        // VACCINATIONS
        // ─────────────────────────────────────
        private void LoadVaccinations()
        {
            try
            {
                dgvVaccinations.DataSource = _vacMgr.GetAllVaccinations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVaccinationCombos()
        {
            try
            {
                // Load visits combo
                DataTable visits = DBHandler.ExecuteQuery("SELECT VISITID, VISITDATE FROM MEDICAL_VISIT ORDER BY VISITDATE DESC");
                DataTable visitDisplay = new DataTable();
                visitDisplay.Columns.Add("VISITID");
                visitDisplay.Columns.Add("VisitLabel");
                foreach (DataRow row in visits.Rows)
                {
                    visitDisplay.Rows.Add(
                        row["VISITID"].ToString(),
                        "Visit #" + row["VISITID"].ToString() + " — " + Convert.ToDateTime(row["VISITDATE"]).ToString("yyyy-MM-dd")
                    );
                }
                cmbVacVisit.DataSource = visitDisplay;
                cmbVacVisit.DisplayMember = "VisitLabel";
                cmbVacVisit.ValueMember = "VISITID";

                // Load inventory combo
                DataTable inventory = DBHandler.ExecuteQuery("SELECT INVENTORYID, VACCINEINVENTORYTYPE, BATCHNUMBER FROM VACCINE_INVENTORY ORDER BY VACCINEINVENTORYTYPE");
                DataTable invDisplay = new DataTable();
                invDisplay.Columns.Add("INVENTORYID");
                invDisplay.Columns.Add("InventoryLabel");
                foreach (DataRow row in inventory.Rows)
                {
                    invDisplay.Rows.Add(
                        row["INVENTORYID"].ToString(),
                        row["VACCINEINVENTORYTYPE"].ToString() + " [" + row["BATCHNUMBER"].ToString() + "]"
                    );
                }
                cmbVacInventory.DataSource = invDisplay;
                cmbVacInventory.DisplayMember = "InventoryLabel";
                cmbVacInventory.ValueMember = "INVENTORYID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearVaccinationFields()
        {
            nudVaccinationID.Value = 0;
            txtVaccineType.Text = string.Empty;
            dtpAdministered.Value = DateTime.Now;
            dtpNextBooster.Value = DateTime.Now.AddMonths(12);
            if (cmbVacVisit.Items.Count > 0) cmbVacVisit.SelectedIndex = 0;
            if (cmbVacInventory.Items.Count > 0) cmbVacInventory.SelectedIndex = 0;
        }

        private void dgvVaccinations_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVaccinations.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvVaccinations.SelectedRows[0];
            try
            {
                nudVaccinationID.Value = Convert.ToDecimal(row.Cells["VACCINATIONID"].Value);
                txtVaccineType.Text = row.Cells["VACCINETYPE"].Value?.ToString();
                dtpAdministered.Value = Convert.ToDateTime(row.Cells["ADMINISTEREDDATE"].Value);
                if (row.Cells["NEXTBOOSTERDUE"].Value != DBNull.Value && row.Cells["NEXTBOOSTERDUE"].Value != null)
                    dtpNextBooster.Value = Convert.ToDateTime(row.Cells["NEXTBOOSTERDUE"].Value);

                string visitId = row.Cells["VISITID"].Value?.ToString();
                foreach (DataRowView item in cmbVacVisit.Items)
                {
                    if (item["VISITID"].ToString() == visitId)
                    {
                        cmbVacVisit.SelectedItem = item;
                        break;
                    }
                }

                string invId = row.Cells["INVENTORYID"].Value?.ToString();
                foreach (DataRowView item in cmbVacInventory.Items)
                {
                    if (item["INVENTORYID"].ToString() == invId)
                    {
                        cmbVacInventory.SelectedItem = item;
                        break;
                    }
                }
            }
            catch { }
        }

        private void btnVacAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Vaccination v = new Vaccination
                {
                    VisitID = cmbVacVisit.SelectedValue != null ? Convert.ToInt32(cmbVacVisit.SelectedValue) : 0,
                    InventoryID = cmbVacInventory.SelectedValue != null ? Convert.ToInt32(cmbVacInventory.SelectedValue) : 0,
                    VaccineType = txtVaccineType.Text.Trim(),
                    AdministeredDate = dtpAdministered.Value,
                    NextBoosterDue = dtpNextBooster.Value
                };
                string result = _vacMgr.AddVaccination(v);
                if (result.Contains("successfully"))
                {
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVaccinations();
                    ClearVaccinationFields();
                }
                else
                {
                    MessageBox.Show(result, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVacUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Vaccination v = new Vaccination
                {
                    VaccinationID = (int)nudVaccinationID.Value,
                    VisitID = cmbVacVisit.SelectedValue != null ? Convert.ToInt32(cmbVacVisit.SelectedValue) : 0,
                    InventoryID = cmbVacInventory.SelectedValue != null ? Convert.ToInt32(cmbVacInventory.SelectedValue) : 0,
                    VaccineType = txtVaccineType.Text.Trim(),
                    AdministeredDate = dtpAdministered.Value,
                    NextBoosterDue = dtpNextBooster.Value
                };
                string result = _vacMgr.UpdateVaccination(v);
                if (result.Contains("successfully"))
                {
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVaccinations();
                    ClearVaccinationFields();
                }
                else
                {
                    MessageBox.Show(result, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVacDelete_Click(object sender, EventArgs e)
        {
            if (nudVaccinationID.Value == 0)
            {
                MessageBox.Show("Please select a vaccination to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this vaccination?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string result = _vacMgr.DeleteVaccination((int)nudVaccinationID.Value);
                    MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVaccinations();
                    ClearVaccinationFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVacClear_Click(object sender, EventArgs e)
        {
            ClearVaccinationFields();
        }

        // ─────────────────────────────────────
        // REPORTS
        // ─────────────────────────────────────
        private void btnReport1_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Report 1: Pets with Owners";
                dgvReports.DataSource = _reportMgr.Report1_PetsWithOwners();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport2_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Report 2: Visits with Details";
                dgvReports.DataSource = _reportMgr.Report2_VisitsWithDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport3_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Report 3: Vaccinations per Pet";
                dgvReports.DataSource = _reportMgr.Report3_VaccinationsPerPet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport4_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Report 4: Pets with No Visit in 6 Months";
                dgvReports.DataSource = _reportMgr.Report4_PetsNoVisit6Months();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport5_Click(object sender, EventArgs e)
        {
            try
            {
                string search = txtReportSearch.Text.Trim();
                string phone = string.Empty;
                string email = string.Empty;
                if (search.Contains("@"))
                    email = search;
                else
                    phone = search;

                lblReportTitle.Text = "Report 5: Owner Search Results";
                dgvReports.DataSource = _reportMgr.Report5_SearchOwners(phone, email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport6_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Report 6: Upcoming Vaccination Reminders";
                dgvReports.DataSource = _reportMgr.Report6_UpcomingReminders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport7_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Report 7: Low Stock Vaccine Inventory";
                dgvReports.DataSource = _reportMgr.Report7_LowStockInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport8_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Report 8: Visits per Clinic";
                dgvReports.DataSource = _reportMgr.Report8_VisitsPerClinic();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
