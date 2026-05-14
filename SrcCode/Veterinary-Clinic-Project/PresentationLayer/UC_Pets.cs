using System;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogicLayer;
using Models;

namespace PresentationLayer
{
    public partial class UC_Pets : UserControl
    {
        private readonly PetManager _petManager = new PetManager();
        private readonly OwnerManager _ownerManager = new OwnerManager();

        public UC_Pets()
        {
            InitializeComponent(); // ضروري
            LoadOwners();
            LoadPets();
            AttachEvents();
        }

        private void AttachEvents()
        {
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += (s, e) => ClearFields();
            dgvPets.SelectionChanged += DgvPets_SelectionChanged;
        }

        private void LoadOwners()
        {
            try
            {
                var owners = _ownerManager.GetAllOwners();
                cboOwner.DataSource = owners;
                cboOwner.DisplayMember = "FullName";  // تأكد من وجود خاصية FullName في Owner.cs (أو استخدم $"{FirstName} {LastName}")
                cboOwner.ValueMember = "OwnerId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading owners: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPets()
        {
            try
            {
                dgvPets.DataSource = null;
                dgvPets.DataSource = _petManager.GetAllPets();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pets: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPets_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPets.SelectedRows.Count == 0) return;
            var row = dgvPets.SelectedRows[0];
            cboOwner.SelectedValue = (int)row.Cells["OwnerId"].Value;
            txtPetName.Text = row.Cells["PetName"].Value?.ToString() ?? "";
            txtSpecies.Text = row.Cells["Species"].Value?.ToString() ?? "";
            txtBreed.Text = row.Cells["Breed"].Value?.ToString() ?? "";
            txtAge.Text = row.Cells["Age"].Value?.ToString() ?? "";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboOwner.SelectedValue == null)
                {
                    MessageBox.Show("Please select an owner.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var pet = new Pet
                {
                    OwnerId = (int)cboOwner.SelectedValue,
                    PetName = txtPetName.Text.Trim(),
                    Species = txtSpecies.Text.Trim(),
                    Breed = txtBreed.Text.Trim(),
                    Age = int.TryParse(txtAge.Text, out int age) ? age : 0
                };
                if (_petManager.AddPet(pet))
                {
                    MessageBox.Show("Pet added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPets();
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
            if (dgvPets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a pet to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = (int)dgvPets.SelectedRows[0].Cells["PetId"].Value;
            try
            {
                var pet = new Pet
                {
                    PetId = id,
                    OwnerId = (int)cboOwner.SelectedValue,
                    PetName = txtPetName.Text.Trim(),
                    Species = txtSpecies.Text.Trim(),
                    Breed = txtBreed.Text.Trim(),
                    Age = int.TryParse(txtAge.Text, out int age) ? age : 0
                };
                if (_petManager.UpdatePet(pet))
                {
                    MessageBox.Show("Pet updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPets();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a pet to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = (int)dgvPets.SelectedRows[0].Cells["PetId"].Value;
            if (MessageBox.Show("Delete this pet? All related visits and vaccinations will be lost.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (_petManager.DeletePet(id))
                    {
                        MessageBox.Show("Pet deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPets();
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
            cboOwner.SelectedIndex = -1;
            txtPetName.Text = "";
            txtSpecies.Text = "";
            txtBreed.Text = "";
            txtAge.Text = "";
        }

        private void dgvPets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}