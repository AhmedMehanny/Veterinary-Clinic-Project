using System;
using System.Windows.Forms;
using BusinessLogicLayer;
using Models;

namespace PresentationLayer
{
    public partial class UC_Visits : UserControl
    {
        private readonly VisitManager _visitManager = new VisitManager();
        private readonly PetManager _petManager = new PetManager();

        public UC_Visits()
        {
            InitializeComponent();
            LoadPets();
            LoadVisits();
            SetupComboBoxes();
        }

        private void SetupComboBoxes()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new[] { "Scheduled", "Completed", "Cancelled" });
            cboStatus.SelectedIndex = 0;
        }

        private void LoadPets()
        {
            var pets = _petManager.GetAllPets();
            cboPet.DataSource = pets;
            cboPet.DisplayMember = "PetName"; // أو "PetInfo"
            cboPet.ValueMember = "PetId";
        }

        private void LoadVisits()
        {
            dgvVisits.DataSource = null;
            dgvVisits.DataSource = _visitManager.GetAllVisits();
        }

        private void btnAdd_Click(object sender, EventArgs e)
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
                if (_visitManager.AddVisit(visit))
                {
                    MessageBox.Show("Visit scheduled.");
                    LoadVisits();
                    ClearFields();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                if (_visitManager.UpdateVisit(visit)) LoadVisits();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvVisits.SelectedRows.Count == 0) return;
            int id = (int)dgvVisits.SelectedRows[0].Cells["VisitId"].Value;
            if (MessageBox.Show("Cancel this visit?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _visitManager.CancelVisit(id);
                LoadVisits();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearFields();

        private void ClearFields()
        {
            cboPet.SelectedIndex = -1;
            dtpVisitDate.Value = DateTime.Now;
            cboStatus.SelectedIndex = 0;
            txtSlotId.Text = "";
            txtNoteId.Text = "";
        }

        private void dgvVisits_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVisits.SelectedRows.Count == 0) return;
            var row = dgvVisits.SelectedRows[0];
            cboPet.SelectedValue = (int)row.Cells["PetId"].Value;
            dtpVisitDate.Value = (DateTime)row.Cells["VisitDate"].Value;
            cboStatus.Text = row.Cells["VisitStatus"].Value.ToString();
            txtSlotId.Text = row.Cells["SlotId"].Value.ToString();
            txtNoteId.Text = row.Cells["NoteId"].Value.ToString();
        }

        
    }
}