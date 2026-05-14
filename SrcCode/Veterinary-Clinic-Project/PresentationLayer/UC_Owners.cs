using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using Models;

namespace PresentationLayer
{
    public partial class UC_Owners : UserControl
    {
        private readonly OwnerManager _ownerManager = new OwnerManager();

        public UC_Owners()
        {
            InitializeComponent();
            LoadOwners();
            AttachEvents();
        }

        private void AttachEvents()
        {
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearFields();
            dgvOwners.SelectionChanged += DgvOwners_SelectionChanged;
        }

        private void LoadOwners()
        {
            try
            {
                dgvOwners.DataSource = null;
                dgvOwners.DataSource = _ownerManager.GetAllOwners();

                // تنسيق الأعمدة (اختياري)
                if (dgvOwners.Columns.Contains("OwnerId"))
                    dgvOwners.Columns["OwnerId"].HeaderText = "ID";
                if (dgvOwners.Columns.Contains("FirstName"))
                    dgvOwners.Columns["FirstName"].HeaderText = "First Name";
                if (dgvOwners.Columns.Contains("LastName"))
                    dgvOwners.Columns["LastName"].HeaderText = "Last Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading owners: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvOwners_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOwners.SelectedRows.Count == 0) return;

            var row = dgvOwners.SelectedRows[0];
            txtFirstName.Text = row.Cells["FirstName"].Value?.ToString() ?? "";
            txtLastName.Text = row.Cells["LastName"].Value?.ToString() ?? "";
            txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";
            txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            txtBillingAddress.Text = row.Cells["BillingAddress"].Value?.ToString() ?? "";
            txtEmergencyContact.Text = row.Cells["EmergencyContact"].Value?.ToString() ?? "";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var owner = new Owner
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    BillingAddress = txtBillingAddress.Text.Trim(),
                    EmergencyContact = txtEmergencyContact.Text.Trim()
                };

                if (_ownerManager.AddOwner(owner))
                {
                    MessageBox.Show("Owner added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOwners();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvOwners.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an owner to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dgvOwners.SelectedRows[0].Cells["OwnerId"].Value;
            try
            {
                var owner = new Owner
                {
                    OwnerId = id,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    BillingAddress = txtBillingAddress.Text.Trim(),
                    EmergencyContact = txtEmergencyContact.Text.Trim()
                };

                if (_ownerManager.UpdateOwner(owner))
                {
                    MessageBox.Show("Owner updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOwners();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvOwners.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an owner to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dgvOwners.SelectedRows[0].Cells["OwnerId"].Value;
            DialogResult result = MessageBox.Show("Delete this owner? All associated pets will be affected.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_ownerManager.DeleteOwner(id))
                    {
                        MessageBox.Show("Owner deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOwners();
                        ClearFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtBillingAddress.Text = "";
            txtEmergencyContact.Text = "";
        }
    }
}