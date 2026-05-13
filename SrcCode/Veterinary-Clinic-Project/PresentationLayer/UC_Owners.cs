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
//    public partial class UC_Owners : UserControl
//    {
//        public UC_Owners()
//        {
//            InitializeComponent();
//        }

//        private void UC_Owners_Load(object sender, EventArgs e)
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
    public partial class UC_Owners : UserControl
    {
        private readonly OwnerManager ownerManager;
        private DataGridView dgvOwners;
        private TextBox txtFirstName, txtLastName, txtPhone, txtEmail;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;

        public UC_Owners()
        {
            InitializeComponent();
            ownerManager = new OwnerManager();
            LoadOwners();
        }

        private void InitializeComponent()
        {
            this.dgvOwners = new DataGridView();
            this.txtFirstName = new TextBox();
            this.txtLastName = new TextBox();
            this.txtPhone = new TextBox();
            this.txtEmail = new TextBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOwners)).BeginInit();
            this.SuspendLayout();

            // dgvOwners
            this.dgvOwners.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOwners.Location = new System.Drawing.Point(20, 20);
            this.dgvOwners.Size = new System.Drawing.Size(700, 300);
            this.dgvOwners.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvOwners.MultiSelect = false;
            this.dgvOwners.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOwners.SelectionChanged += DgvOwners_SelectionChanged;

            // Labels and TextBoxes
            Label lblFirst = new Label() { Text = "First Name:", Location = new System.Drawing.Point(20, 340), AutoSize = true };
            Label lblLast = new Label() { Text = "Last Name:", Location = new System.Drawing.Point(20, 370), AutoSize = true };
            Label lblPhone = new Label() { Text = "Phone:", Location = new System.Drawing.Point(20, 400), AutoSize = true };
            Label lblEmail = new Label() { Text = "Email:", Location = new System.Drawing.Point(20, 430), AutoSize = true };

            this.txtFirstName.Location = new System.Drawing.Point(120, 337); this.txtFirstName.Width = 200;
            this.txtLastName.Location = new System.Drawing.Point(120, 367); this.txtLastName.Width = 200;
            this.txtPhone.Location = new System.Drawing.Point(120, 397); this.txtPhone.Width = 200;
            this.txtEmail.Location = new System.Drawing.Point(120, 427); this.txtEmail.Width = 200;

            // Buttons
            this.btnAdd.Text = "Add"; this.btnAdd.Location = new System.Drawing.Point(350, 340); this.btnAdd.Click += BtnAdd_Click;
            this.btnUpdate.Text = "Update"; this.btnUpdate.Location = new System.Drawing.Point(350, 370); this.btnUpdate.Click += BtnUpdate_Click;
            this.btnDelete.Text = "Delete"; this.btnDelete.Location = new System.Drawing.Point(350, 400); this.btnDelete.Click += BtnDelete_Click;
            this.btnClear.Text = "Clear"; this.btnClear.Location = new System.Drawing.Point(350, 430); this.btnClear.Click += (s, e) => ClearFields();

            this.Controls.Add(dgvOwners);
            this.Controls.Add(lblFirst); this.Controls.Add(txtFirstName);
            this.Controls.Add(lblLast); this.Controls.Add(txtLastName);
            this.Controls.Add(lblPhone); this.Controls.Add(txtPhone);
            this.Controls.Add(lblEmail); this.Controls.Add(txtEmail);
            this.Controls.Add(btnAdd); this.Controls.Add(btnUpdate); this.Controls.Add(btnDelete); this.Controls.Add(btnClear);

            ((System.ComponentModel.ISupportInitialize)(this.dgvOwners)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadOwners()
        {
            var owners = ownerManager.GetAllOwners();
            dgvOwners.DataSource = null;
            dgvOwners.DataSource = owners;
            dgvOwners.Columns["OwnerId"].Visible = true;
            dgvOwners.Columns["FirstName"].HeaderText = "First Name";
            dgvOwners.Columns["LastName"].HeaderText = "Last Name";
        }

        private void DgvOwners_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOwners.SelectedRows.Count > 0)
            {
                var row = dgvOwners.SelectedRows[0];
                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var owner = new Owner
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text
                };
                if (ownerManager.AddOwner(owner))
                {
                    ShowToast("Owner added successfully", "success");
                    LoadOwners();
                    ClearFields();
                }
            }
            catch (Exception ex) { ShowToast(ex.Message, "error"); }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvOwners.SelectedRows.Count == 0) return;
            int id = (int)dgvOwners.SelectedRows[0].Cells["OwnerId"].Value;
            try
            {
                var owner = new Owner
                {
                    OwnerId = id,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text
                };
                if (ownerManager.UpdateOwner(owner))
                {
                    ShowToast("Owner updated", "success");
                    LoadOwners();
                }
            }
            catch (Exception ex) { ShowToast(ex.Message, "error"); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvOwners.SelectedRows.Count == 0) return;
            int id = (int)dgvOwners.SelectedRows[0].Cells["OwnerId"].Value;
            if (MessageBox.Show("Delete this owner? All pets will be orphaned.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (ownerManager.DeleteOwner(id))
                    {
                        ShowToast("Owner deleted", "success");
                        LoadOwners();
                        ClearFields();
                    }
                }
                catch (Exception ex) { ShowToast(ex.Message, "error"); }
            }
        }

        private void ClearFields() { txtFirstName.Text = txtLastName.Text = txtPhone.Text = txtEmail.Text = ""; }
        private void ShowToast(string msg, string type) { new frmToast(msg, type).Show(); }
    }
}