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
//    public partial class UC_Pets : UserControl
//    {
//        public UC_Pets()
//        {
//            InitializeComponent();
//        }

//        private void UC_Pets_Load(object sender, EventArgs e)
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
    public partial class UC_Pets : UserControl
    {
        private readonly PetManager _petManager;
        private readonly OwnerManager _ownerManager;
        private DataGridView dgvPets;
        private ComboBox cboOwner;
        private TextBox txtPetName, txtSpecies, txtBreed, txtAge;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;

        public UC_Pets()
        {
            InitializeComponent();
            _petManager = new PetManager();
            _ownerManager = new OwnerManager();
            LoadOwners();
            LoadPets();
        }

        private void InitializeComponent()
        {
            this.dgvPets = new DataGridView();
            this.cboOwner = new ComboBox();
            this.txtPetName = new TextBox();
            this.txtSpecies = new TextBox();
            this.txtBreed = new TextBox();
            this.txtAge = new TextBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPets)).BeginInit();
            this.SuspendLayout();

            // dgvPets
            this.dgvPets.Location = new System.Drawing.Point(20, 20);
            this.dgvPets.Size = new System.Drawing.Size(800, 300);
            this.dgvPets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPets.MultiSelect = false;
            this.dgvPets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPets.SelectionChanged += DgvPets_SelectionChanged;

            // Labels & Controls
            int y = 340;
            void AddField(string labelText, Control control, int yPos)
            {
                Label lbl = new Label() { Text = labelText, Location = new System.Drawing.Point(20, yPos), AutoSize = true };
                control.Location = new System.Drawing.Point(120, yPos - 3);
                control.Width = 200;
                this.Controls.Add(lbl);
                this.Controls.Add(control);
            }

            AddField("Owner:", cboOwner, y); y += 30;
            AddField("Pet Name:", txtPetName, y); y += 30;
            AddField("Species:", txtSpecies, y); y += 30;
            AddField("Breed:", txtBreed, y); y += 30;
            AddField("Age:", txtAge, y); y += 30;

            // Buttons
            this.btnAdd.Text = "Add"; this.btnAdd.Location = new System.Drawing.Point(350, 340); this.btnAdd.Click += BtnAdd_Click;
            this.btnUpdate.Text = "Update"; this.btnUpdate.Location = new System.Drawing.Point(350, 370); this.btnUpdate.Click += BtnUpdate_Click;
            this.btnDelete.Text = "Delete"; this.btnDelete.Location = new System.Drawing.Point(350, 400); this.btnDelete.Click += BtnDelete_Click;
            this.btnClear.Text = "Clear"; this.btnClear.Location = new System.Drawing.Point(350, 430); this.btnClear.Click += (s, e) => ClearFields();

            this.Controls.Add(this.dgvPets);
            this.Controls.Add(btnAdd); this.Controls.Add(btnUpdate); this.Controls.Add(btnDelete); this.Controls.Add(btnClear);

            ((System.ComponentModel.ISupportInitialize)(this.dgvPets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadOwners()
        {
            var owners = _ownerManager.GetAllOwners();
            cboOwner.DisplayMember = "FullName"; // Owner has FirstName+LastName, we'll use helper
            cboOwner.ValueMember = "OwnerId";
            // Create a list with full name
            var list = owners.ConvertAll(o => new { OwnerId = o.OwnerId, FullName = $"{o.FirstName} {o.LastName}" });
            cboOwner.DataSource = list;
        }

        private void LoadPets()
        {
            var pets = _petManager.GetAllPets();
            dgvPets.DataSource = null;
            dgvPets.DataSource = pets;
            dgvPets.Columns["PetId"].Visible = true;
            dgvPets.Columns["PetName"].HeaderText = "Name";
            dgvPets.Columns["OwnerId"].HeaderText = "Owner ID";
            dgvPets.Columns["OwnerFirstName"].Visible = false;
            dgvPets.Columns["OwnerLastName"].Visible = false;
            dgvPets.Columns["OwnerPhone"].Visible = false;
            dgvPets.Columns["OwnerFullName"].Visible = false;
            dgvPets.Columns["PetInfo"].Visible = false;
        }

        private void DgvPets_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPets.SelectedRows.Count > 0)
            {
                var row = dgvPets.SelectedRows[0];
                // Set owner combo by OwnerId
                int ownerId = Convert.ToInt32(row.Cells["OwnerId"].Value);
                cboOwner.SelectedValue = ownerId;
                txtPetName.Text = row.Cells["PetName"].Value.ToString();
                txtSpecies.Text = row.Cells["Species"].Value.ToString();
                txtBreed.Text = row.Cells["Breed"].Value?.ToString();
                txtAge.Text = row.Cells["Age"].Value?.ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var pet = new Pet
                {
                    OwnerId = (int)cboOwner.SelectedValue,
                    PetName = txtPetName.Text,
                    Species = txtSpecies.Text,
                    Breed = txtBreed.Text,
                    Age = int.TryParse(txtAge.Text, out int age) ? age : 0
                };
                if (_petManager.AddPet(pet))
                {
                    ShowToast("Pet added successfully", "success");
                    LoadPets();
                    ClearFields();
                }
            }
            catch (Exception ex) { ShowToast(ex.Message, "error"); }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPets.SelectedRows.Count == 0) return;
            int id = (int)dgvPets.SelectedRows[0].Cells["PetId"].Value;
            try
            {
                var pet = new Pet
                {
                    PetId = id,
                    OwnerId = (int)cboOwner.SelectedValue,
                    PetName = txtPetName.Text,
                    Species = txtSpecies.Text,
                    Breed = txtBreed.Text,
                    Age = int.TryParse(txtAge.Text, out int age) ? age : 0
                };
                if (_petManager.UpdatePet(pet))
                {
                    ShowToast("Pet updated", "success");
                    LoadPets();
                }
            }
            catch (Exception ex) { ShowToast(ex.Message, "error"); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPets.SelectedRows.Count == 0) return;
            int id = (int)dgvPets.SelectedRows[0].Cells["PetId"].Value;
            if (MessageBox.Show("Delete this pet? All related visits will be affected.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (_petManager.DeletePet(id))
                    {
                        ShowToast("Pet deleted", "success");
                        LoadPets();
                        ClearFields();
                    }
                }
                catch (Exception ex) { ShowToast(ex.Message, "error"); }
            }
        }

        private void ClearFields()
        {
            cboOwner.SelectedIndex = -1;
            txtPetName.Text = txtSpecies.Text = txtBreed.Text = txtAge.Text = "";
        }

        private void ShowToast(string msg, string type) => new frmToast(msg, type).Show();
    }
}
