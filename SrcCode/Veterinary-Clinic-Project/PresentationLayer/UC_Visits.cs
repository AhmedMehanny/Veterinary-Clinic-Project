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
//    public partial class UC_Visits : UserControl
//    {
//        public UC_Visits()
//        {
//            InitializeComponent();
//        }

//        private void UC_Visits_Load(object sender, EventArgs e)
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
    public partial class UC_Visits : UserControl
    {
        private readonly VisitManager _visitManager;
        private readonly PetManager _petManager;
        private DataGridView dgvVisits;
        private ComboBox cboPet;
        private DateTimePicker dtpVisitDate;
        private ComboBox cboStatus;
        private TextBox txtSlotId, txtNoteId;
        private Button btnAdd, btnUpdate, btnCancel, btnClear;

        public UC_Visits()
        {
            InitializeComponent();
            _visitManager = new VisitManager();
            _petManager = new PetManager();
            LoadPets();
            LoadVisits();
        }

        private void InitializeComponent()
        {
            this.dgvVisits = new DataGridView();
            this.cboPet = new ComboBox();
            this.dtpVisitDate = new DateTimePicker();
            this.cboStatus = new ComboBox();
            this.txtSlotId = new TextBox();
            this.txtNoteId = new TextBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnCancel = new Button();
            this.btnClear = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).BeginInit();
            this.SuspendLayout();

            // dgvVisits
            this.dgvVisits.Location = new System.Drawing.Point(20, 20);
            this.dgvVisits.Size = new System.Drawing.Size(900, 300);
            this.dgvVisits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvVisits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVisits.SelectionChanged += DgvVisits_SelectionChanged;

            int y = 340;
            void AddField(string labelText, Control control, int yPos)
            {
                Label lbl = new Label() { Text = labelText, Location = new System.Drawing.Point(20, yPos), AutoSize = true };
                control.Location = new System.Drawing.Point(140, yPos - 3);
                control.Width = 200;
                this.Controls.Add(lbl);
                this.Controls.Add(control);
            }

            AddField("Pet:", cboPet, y); y += 30;
            AddField("Visit Date:", dtpVisitDate, y); y += 30;
            AddField("Status:", cboStatus, y); y += 30;
            AddField("Slot ID:", txtSlotId, y); y += 30;
            AddField("Note ID:", txtNoteId, y); y += 30;

            // Setup ComboBoxes
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Items.AddRange(new string[] { "Scheduled", "Completed", "Cancelled" });
            dtpVisitDate.Format = DateTimePickerFormat.Custom;
            dtpVisitDate.CustomFormat = "yyyy-MM-dd HH:mm";

            // Buttons
            this.btnAdd.Text = "Schedule"; this.btnAdd.Location = new System.Drawing.Point(380, 340); this.btnAdd.Click += BtnAdd_Click;
            this.btnUpdate.Text = "Update"; this.btnUpdate.Location = new System.Drawing.Point(380, 370); this.btnUpdate.Click += BtnUpdate_Click;
            this.btnCancel.Text = "Cancel Visit"; this.btnCancel.Location = new System.Drawing.Point(380, 400); this.btnCancel.Click += BtnCancel_Click;
            this.btnClear.Text = "Clear"; this.btnClear.Location = new System.Drawing.Point(380, 430); this.btnClear.Click += (s, e) => ClearFields();

            this.Controls.Add(this.dgvVisits);
            this.Controls.Add(btnAdd); this.Controls.Add(btnUpdate); this.Controls.Add(btnCancel); this.Controls.Add(btnClear);

            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadPets()
        {
            var pets = _petManager.GetAllPets();
            var list = pets.ConvertAll(p => new { PetId = p.PetId, Name = $"{p.PetName} ({p.Species})" });
            cboPet.DisplayMember = "Name";
            cboPet.ValueMember = "PetId";
            cboPet.DataSource = list;
        }

        private void LoadVisits()
        {
            var visits = _visitManager.GetAllVisits();
            dgvVisits.DataSource = null;
            dgvVisits.DataSource = visits;
            dgvVisits.Columns["VisitId"].Visible = true;
            dgvVisits.Columns["VisitInfo"].Visible = false;
            dgvVisits.Columns["DisplayDate"].Visible = false;
            dgvVisits.Columns["OwnerFullName"].Visible = false;
            dgvVisits.Columns["VetFullName"].Visible = false;
        }

        private void DgvVisits_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVisits.SelectedRows.Count > 0)
            {
                var row = dgvVisits.SelectedRows[0];
                cboPet.SelectedValue = Convert.ToInt32(row.Cells["PetId"].Value);
                dtpVisitDate.Value = Convert.ToDateTime(row.Cells["VisitDate"].Value);
                cboStatus.Text = row.Cells["VisitStatus"].Value?.ToString();
                txtSlotId.Text = row.Cells["SlotId"].Value?.ToString();
                txtNoteId.Text = row.Cells["NoteId"].Value?.ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var visit = new Visit
                {
                    PetId = (int)cboPet.SelectedValue,
                    SlotId = int.Parse(txtSlotId.Text),
                    NoteId = int.Parse(txtNoteId.Text),
                    VisitDate = dtpVisitDate.Value,
                    VisitStatus = cboStatus.Text
                };
                if (_visitManager.ScheduleVisit(visit))
                {
                    ShowToast("Visit scheduled", "success");
                    LoadVisits();
                    ClearFields();
                }
            }
            catch (Exception ex) { ShowToast(ex.Message, "error"); }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvVisits.SelectedRows.Count == 0) return;
            int id = (int)dgvVisits.SelectedRows[0].Cells["VisitId"].Value;
            try
            {
                var visit = new Visit
                {
                    VisitId = id,
                    PetId = (int)cboPet.SelectedValue,
                    SlotId = int.Parse(txtSlotId.Text),
                    NoteId = int.Parse(txtNoteId.Text),
                    VisitDate = dtpVisitDate.Value,
                    VisitStatus = cboStatus.Text
                };
                if (_visitManager.UpdateVisit(visit))
                {
                    ShowToast("Visit updated", "success");
                    LoadVisits();
                }
            }
            catch (Exception ex) { ShowToast(ex.Message, "error"); }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (dgvVisits.SelectedRows.Count == 0) return;
            int id = (int)dgvVisits.SelectedRows[0].Cells["VisitId"].Value;
            if (MessageBox.Show("Cancel this visit?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (_visitManager.CancelVisit(id))
                    {
                        ShowToast("Visit cancelled", "success");
                        LoadVisits();
                        ClearFields();
                    }
                }
                catch (Exception ex) { ShowToast(ex.Message, "error"); }
            }
        }

        private void ClearFields()
        {
            cboPet.SelectedIndex = -1;
            dtpVisitDate.Value = DateTime.Now;
            cboStatus.SelectedIndex = -1;
            txtSlotId.Text = txtNoteId.Text = "";
        }

        private void ShowToast(string msg, string type) => new frmToast(msg, type).Show();
    }
}