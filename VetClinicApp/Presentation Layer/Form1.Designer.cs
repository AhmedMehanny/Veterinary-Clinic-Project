using System;
using System.Drawing;
using System.Windows.Forms;

namespace VetClinicApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ── Form ──
            this.Text = "🐾 VetClinic Manager";
            this.Size = new Size(1280, 800);
            this.MinimumSize = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.Font = new Font("Segoe UI", 9f);
            this.Load += new EventHandler(this.Form1_Load);

            // ── Sidebar ──
            pnlSidebar = new Panel();
            pnlSidebar.Width = 210;
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.BackColor = Color.FromArgb(27, 42, 74);

            lblLogo = new Label();
            lblLogo.Text = "🐾 VetClinic";
            lblLogo.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Height = 60;

            // Nav buttons (added in reverse so Dashboard appears on top with DockStyle.Top)
            btnReports = CreateNavButton("📊  Reports");
            btnVaccinations = CreateNavButton("💉  Vaccinations");
            btnVisits = CreateNavButton("🏥  Visits");
            btnPets = CreateNavButton("🐾  Pets");
            btnOwners = CreateNavButton("👤  Owners");
            btnDashboard = CreateNavButton("🏠  Dashboard");

            btnDashboard.Click += new EventHandler(this.btnDashboard_Click);
            btnOwners.Click += new EventHandler(this.btnOwners_Click);
            btnPets.Click += new EventHandler(this.btnPets_Click);
            btnVisits.Click += new EventHandler(this.btnVisits_Click);
            btnVaccinations.Click += new EventHandler(this.btnVaccinations_Click);
            btnReports.Click += new EventHandler(this.btnReports_Click);

            pnlSidebar.Controls.Add(btnReports);
            pnlSidebar.Controls.Add(btnVaccinations);
            pnlSidebar.Controls.Add(btnVisits);
            pnlSidebar.Controls.Add(btnPets);
            pnlSidebar.Controls.Add(btnOwners);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(lblLogo);

            // ── Main Content ──
            pnlContent = new Panel();
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.BackColor = Color.FromArgb(240, 244, 248);

            // Build all panels
            BuildDashboardPanel();
            BuildOwnersPanel();
            BuildPetsPanel();
            BuildVisitsPanel();
            BuildVaccinationsPanel();
            BuildReportsPanel();

            pnlContent.Controls.Add(pnlReports);
            pnlContent.Controls.Add(pnlVaccinations);
            pnlContent.Controls.Add(pnlVisits);
            pnlContent.Controls.Add(pnlPets);
            pnlContent.Controls.Add(pnlOwners);
            pnlContent.Controls.Add(pnlDashboard);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlSidebar);
        }

        // ─────────────────────────────────────
        // HELPER: Create Nav Button
        // ─────────────────────────────────────
        private Button CreateNavButton(string text)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Height = 52;
            btn.Dock = DockStyle.Top;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10f);
            btn.BackColor = Color.FromArgb(27, 42, 74);
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(16, 0, 0, 0);
            btn.MouseEnter += new EventHandler(this.NavButton_MouseEnter);
            btn.MouseLeave += new EventHandler(this.NavButton_MouseLeave);
            return btn;
        }

        // ─────────────────────────────────────
        // HELPER: Create Action Button
        // ─────────────────────────────────────
        private Button CreateActionButton(string text, Color backColor)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(110, 35);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.BackColor = backColor;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(5);
            return btn;
        }

        // ─────────────────────────────────────
        // HELPER: Style DataGridView
        // ─────────────────────────────────────
        private void StyleGrid(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.GridColor = Color.FromArgb(208, 216, 228);
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(27, 42, 74);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(27, 42, 74);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 36;
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 242, 247);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 134, 171);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        // ─────────────────────────────────────
        // HELPER: Create Input Label
        // ─────────────────────────────────────
        private Label CreateLabel(string text)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 9f);
            lbl.ForeColor = Color.FromArgb(68, 68, 68);
            lbl.AutoSize = true;
            return lbl;
        }

        // ─────────────────────────────────────
        // HELPER: Create TextBox
        // ─────────────────────────────────────
        private TextBox CreateTextBox(int width = 180)
        {
            TextBox txt = new TextBox();
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 9f);
            txt.Height = 28;
            txt.Width = width;
            return txt;
        }

        // ─────────────────────────────────────
        // DASHBOARD PANEL
        // ─────────────────────────────────────
        private void BuildDashboardPanel()
        {
            pnlDashboard = new Panel();
            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.BackColor = Color.FromArgb(240, 244, 248);
            pnlDashboard.Visible = false;

            Label lblHeader = new Label();
            lblHeader.Text = "🏠 Dashboard";
            lblHeader.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(27, 42, 74);
            lblHeader.Location = new Point(24, 24);
            lblHeader.AutoSize = true;

            // 4 stat cards
            Panel cardOwners = CreateDashCard("Total Owners", out lblOwnerCount, Color.FromArgb(46, 134, 171));
            Panel cardPets = CreateDashCard("Total Pets", out lblPetCount, Color.FromArgb(23, 165, 137));
            Panel cardVisits = CreateDashCard("Total Visits", out lblVisitCount, Color.FromArgb(231, 76, 60));
            Panel cardReminders = CreateDashCard("Upcoming Reminders", out lblReminderCount, Color.FromArgb(155, 89, 182));

            cardOwners.Location = new Point(24, 80);
            cardPets.Location = new Point(306, 80);
            cardVisits.Location = new Point(588, 80);
            cardReminders.Location = new Point(870, 80);

            pnlDashboard.Controls.Add(lblHeader);
            pnlDashboard.Controls.Add(cardOwners);
            pnlDashboard.Controls.Add(cardPets);
            pnlDashboard.Controls.Add(cardVisits);
            pnlDashboard.Controls.Add(cardReminders);
        }

        private Panel CreateDashCard(string title, out Label countLabel, Color accentColor)
        {
            Panel card = new Panel();
            card.Size = new Size(260, 110);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;

            Panel accent = new Panel();
            accent.Height = 4;
            accent.Dock = DockStyle.Top;
            accent.BackColor = accentColor;

            countLabel = new Label();
            countLabel.Text = "0";
            countLabel.Font = new Font("Segoe UI", 28f, FontStyle.Bold);
            countLabel.ForeColor = Color.FromArgb(27, 42, 74);
            countLabel.Location = new Point(16, 18);
            countLabel.AutoSize = true;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 9f);
            lblTitle.ForeColor = Color.FromArgb(136, 136, 136);
            lblTitle.Location = new Point(16, 70);
            lblTitle.AutoSize = true;

            card.Controls.Add(accent);
            card.Controls.Add(countLabel);
            card.Controls.Add(lblTitle);
            return card;
        }

        // ─────────────────────────────────────
        // OWNERS PANEL
        // ─────────────────────────────────────
        private void BuildOwnersPanel()
        {
            pnlOwners = new Panel();
            pnlOwners.Dock = DockStyle.Fill;
            pnlOwners.BackColor = Color.FromArgb(240, 244, 248);
            pnlOwners.Visible = false;

            Label lblHeader = new Label();
            lblHeader.Text = "👤 Owner Management";
            lblHeader.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(27, 42, 74);
            lblHeader.Location = new Point(16, 16);
            lblHeader.AutoSize = true;

            // ── Search bar row (Y=50, height ~36) ──
            txtOwnerSearch = CreateTextBox(220);
            txtOwnerSearch.Location = new Point(16, 54);

            btnOwnerSearch = CreateActionButton("Search", Color.FromArgb(46, 134, 171));
            btnOwnerSearch.Location = new Point(244, 50);
            btnOwnerSearch.Click += new EventHandler(this.btnOwnerSearch_Click);

            // ── Right-side input form ──
            Panel pnlForm = new Panel();
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            pnlForm.Size = new Size(280, 460);

            nudOwnerID = new NumericUpDown();
            nudOwnerID.Visible = false;
            nudOwnerID.Maximum = int.MaxValue;

            int y = 12;
            pnlForm.Controls.Add(CreateLabel("First Name"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            txtFirstName = CreateTextBox(240);
            txtFirstName.Location = new Point(12, y);
            pnlForm.Controls.Add(txtFirstName);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Last Name"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            txtLastName = CreateTextBox(240);
            txtLastName.Location = new Point(12, y);
            pnlForm.Controls.Add(txtLastName);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Phone"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            txtPhone = CreateTextBox(240);
            txtPhone.Location = new Point(12, y);
            pnlForm.Controls.Add(txtPhone);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Email"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            txtEmail = CreateTextBox(240);
            txtEmail.Location = new Point(12, y);
            pnlForm.Controls.Add(txtEmail);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Billing Address"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            txtBilling = CreateTextBox(240);
            txtBilling.Location = new Point(12, y);
            pnlForm.Controls.Add(txtBilling);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Emergency Contact"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            txtEmergency = CreateTextBox(240);
            txtEmergency.Location = new Point(12, y);
            pnlForm.Controls.Add(txtEmergency);
            y += 44;

            // Action buttons
            btnOwnerAdd = CreateActionButton("Add", Color.FromArgb(46, 134, 171));
            btnOwnerAdd.Location = new Point(12, y);
            btnOwnerAdd.Click += new EventHandler(this.btnOwnerAdd_Click);

            btnOwnerUpdate = CreateActionButton("Update", Color.FromArgb(46, 134, 171));
            btnOwnerUpdate.Location = new Point(130, y);
            btnOwnerUpdate.Click += new EventHandler(this.btnOwnerUpdate_Click);
            y += 44;

            btnOwnerDelete = CreateActionButton("Delete", Color.FromArgb(230, 57, 70));
            btnOwnerDelete.Location = new Point(12, y);
            btnOwnerDelete.Click += new EventHandler(this.btnOwnerDelete_Click);

            btnOwnerClear = CreateActionButton("Clear", Color.FromArgb(108, 117, 125));
            btnOwnerClear.Location = new Point(130, y);
            btnOwnerClear.Click += new EventHandler(this.btnOwnerClear_Click);

            pnlForm.Controls.Add(nudOwnerID);
            pnlForm.Controls.Add(btnOwnerAdd);
            pnlForm.Controls.Add(btnOwnerUpdate);
            pnlForm.Controls.Add(btnOwnerDelete);
            pnlForm.Controls.Add(btnOwnerClear);

            // ── Grid panel ──
            dgvOwners = new DataGridView();
            StyleGrid(dgvOwners);
            dgvOwners.SelectionChanged += new EventHandler(this.dgvOwners_SelectionChanged);

            Panel pnlGrid = new Panel();
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;

            // ── SizeChanged: grid starts at Y=90 to clear the search bar ──
            pnlOwners.SizeChanged += (s, ev) =>
            {
                int formLeft = pnlOwners.Width - 300;
                // Grid: starts below search bar (Y=90), right edge stops before form
                pnlGrid.Location = new Point(16, 90);
                pnlGrid.Size = new Size(formLeft - 24, pnlOwners.Height - 106);
                dgvOwners.Size = new Size(pnlGrid.Width - 2, pnlGrid.Height - 2);
                dgvOwners.Location = new Point(0, 0);
                // Form: starts at Y=90 as well, aligned with grid top
                pnlForm.Location = new Point(formLeft, 90);
                pnlForm.Height = pnlOwners.Height - 106;
            };

            pnlGrid.Controls.Add(dgvOwners);

            // ── BUG FIX: pnlForm was never added to pnlOwners ──
            pnlOwners.Controls.Add(lblHeader);
            pnlOwners.Controls.Add(txtOwnerSearch);
            pnlOwners.Controls.Add(btnOwnerSearch);
            pnlOwners.Controls.Add(pnlGrid);
            pnlOwners.Controls.Add(pnlForm);   // ← THIS LINE WAS MISSING
        }   

        // ─────────────────────────────────────
        // PETS PANEL
        // ─────────────────────────────────────
        private void BuildPetsPanel()
        {
            pnlPets = new Panel();
            pnlPets.Dock = DockStyle.Fill;
            pnlPets.BackColor = Color.FromArgb(240, 244, 248);
            pnlPets.Visible = false;

            Label lblHeader = new Label();
            lblHeader.Text = "🐾 Pet Management";
            lblHeader.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(27, 42, 74);
            lblHeader.Location = new Point(16, 16);
            lblHeader.AutoSize = true;

            Panel pnlForm = new Panel();
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            pnlForm.Size = new Size(280, 420);

            nudPetID = new NumericUpDown();
            nudPetID.Visible = false;
            nudPetID.Maximum = int.MaxValue;

            int y = 12;
            pnlForm.Controls.Add(CreateLabel("Pet Name"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            txtPetName = CreateTextBox(240);
            txtPetName.Location = new Point(12, y);
            pnlForm.Controls.Add(txtPetName);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Species"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            cmbSpecies = new ComboBox();
            cmbSpecies.Items.AddRange(new string[] { "Dog", "Cat", "Bird", "Rabbit", "Hamster", "Other" });
            cmbSpecies.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSpecies.Font = new Font("Segoe UI", 9f);
            cmbSpecies.Width = 240;
            cmbSpecies.Location = new Point(12, y);
            pnlForm.Controls.Add(cmbSpecies);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Breed"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            txtBreed = CreateTextBox(240);
            txtBreed.Location = new Point(12, y);
            pnlForm.Controls.Add(txtBreed);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Age"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            nudAge = new NumericUpDown();
            nudAge.Minimum = 0;
            nudAge.Maximum = 50;
            nudAge.Font = new Font("Segoe UI", 9f);
            nudAge.Width = 100;
            nudAge.Location = new Point(12, y);
            pnlForm.Controls.Add(nudAge);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Owner"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            cmbOwnerID = new ComboBox();
            cmbOwnerID.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOwnerID.Font = new Font("Segoe UI", 9f);
            cmbOwnerID.Width = 240;
            cmbOwnerID.Location = new Point(12, y);
            pnlForm.Controls.Add(cmbOwnerID);
            y += 44;

            btnPetAdd = CreateActionButton("Add", Color.FromArgb(46, 134, 171));
            btnPetAdd.Location = new Point(12, y);
            btnPetAdd.Click += new EventHandler(this.btnPetAdd_Click);

            btnPetUpdate = CreateActionButton("Update", Color.FromArgb(46, 134, 171));
            btnPetUpdate.Location = new Point(130, y);
            btnPetUpdate.Click += new EventHandler(this.btnPetUpdate_Click);
            y += 44;

            btnPetDelete = CreateActionButton("Delete", Color.FromArgb(230, 57, 70));
            btnPetDelete.Location = new Point(12, y);
            btnPetDelete.Click += new EventHandler(this.btnPetDelete_Click);

            btnPetClear = CreateActionButton("Clear", Color.FromArgb(108, 117, 125));
            btnPetClear.Location = new Point(130, y);
            btnPetClear.Click += new EventHandler(this.btnPetClear_Click);

            pnlForm.Controls.Add(nudPetID);
            pnlForm.Controls.Add(btnPetAdd);
            pnlForm.Controls.Add(btnPetUpdate);
            pnlForm.Controls.Add(btnPetDelete);
            pnlForm.Controls.Add(btnPetClear);

            dgvPets = new DataGridView();
            StyleGrid(dgvPets);
            dgvPets.SelectionChanged += new EventHandler(this.dgvPets_SelectionChanged);

            Panel pnlGrid = new Panel();
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;

            pnlPets.SizeChanged += (s, ev) =>
            {
                int formLeft = pnlPets.Width - 300;
                pnlForm.Location = new Point(formLeft, 50);
                pnlGrid.Location = new Point(16, 50);
                pnlGrid.Size = new Size(formLeft - 24, pnlPets.Height - 66);
                pnlForm.Height = pnlPets.Height - 66;
                dgvPets.Size = new Size(pnlGrid.Width - 2, pnlGrid.Height - 2);
                dgvPets.Location = new Point(0, 0);
            };

            pnlGrid.Controls.Add(dgvPets);
            pnlPets.Controls.Add(lblHeader);
            pnlPets.Controls.Add(pnlGrid);
            pnlPets.Controls.Add(pnlForm);
        }

        // ─────────────────────────────────────
        // VISITS PANEL
        // ─────────────────────────────────────
        private void BuildVisitsPanel()
        {
            pnlVisits = new Panel();
            pnlVisits.Dock = DockStyle.Fill;
            pnlVisits.BackColor = Color.FromArgb(240, 244, 248);
            pnlVisits.Visible = false;

            Label lblHeader = new Label();
            lblHeader.Text = "🏥 Visit Management";
            lblHeader.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(27, 42, 74);
            lblHeader.Location = new Point(16, 16);
            lblHeader.AutoSize = true;

            Panel pnlForm = new Panel();
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            pnlForm.Size = new Size(280, 460);

            nudVisitID = new NumericUpDown();
            nudVisitID.Visible = false;
            nudVisitID.Maximum = int.MaxValue;

            int y = 12;
            pnlForm.Controls.Add(CreateLabel("Pet"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            cmbVisitPet = new ComboBox();
            cmbVisitPet.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVisitPet.Font = new Font("Segoe UI", 9f);
            cmbVisitPet.Width = 240;
            cmbVisitPet.Location = new Point(12, y);
            pnlForm.Controls.Add(cmbVisitPet);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Appointment Slot"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            cmbVisitSlot = new ComboBox();
            cmbVisitSlot.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVisitSlot.Font = new Font("Segoe UI", 9f);
            cmbVisitSlot.Width = 240;
            cmbVisitSlot.Location = new Point(12, y);
            pnlForm.Controls.Add(cmbVisitSlot);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Visit Date"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            dtpVisitDate = new DateTimePicker();
            dtpVisitDate.Font = new Font("Segoe UI", 9f);
            dtpVisitDate.Width = 240;
            dtpVisitDate.Location = new Point(12, y);
            pnlForm.Controls.Add(dtpVisitDate);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Status"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            cmbVisitStatus = new ComboBox();
            cmbVisitStatus.Items.AddRange(new string[] { "Scheduled", "Completed", "Cancelled", "No-Show" });
            cmbVisitStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVisitStatus.Font = new Font("Segoe UI", 9f);
            cmbVisitStatus.Width = 240;
            cmbVisitStatus.Location = new Point(12, y);
            pnlForm.Controls.Add(cmbVisitStatus);
            y += 44;

            btnVisitAdd = CreateActionButton("Add", Color.FromArgb(46, 134, 171));
            btnVisitAdd.Location = new Point(12, y);
            btnVisitAdd.Click += new EventHandler(this.btnVisitAdd_Click);

            btnVisitUpdate = CreateActionButton("Update", Color.FromArgb(46, 134, 171));
            btnVisitUpdate.Location = new Point(130, y);
            btnVisitUpdate.Click += new EventHandler(this.btnVisitUpdate_Click);
            y += 44;

            btnVisitUpdateStatus = CreateActionButton("Upd. Status", Color.FromArgb(23, 165, 137));
            btnVisitUpdateStatus.Location = new Point(12, y);
            btnVisitUpdateStatus.Click += new EventHandler(this.btnVisitUpdateStatus_Click);

            btnVisitDelete = CreateActionButton("Delete", Color.FromArgb(230, 57, 70));
            btnVisitDelete.Location = new Point(130, y);
            btnVisitDelete.Click += new EventHandler(this.btnVisitDelete_Click);
            y += 44;

            btnVisitClear = CreateActionButton("Clear", Color.FromArgb(108, 117, 125));
            btnVisitClear.Location = new Point(12, y);
            btnVisitClear.Click += new EventHandler(this.btnVisitClear_Click);

            pnlForm.Controls.Add(nudVisitID);
            pnlForm.Controls.Add(btnVisitAdd);
            pnlForm.Controls.Add(btnVisitUpdate);
            pnlForm.Controls.Add(btnVisitUpdateStatus);
            pnlForm.Controls.Add(btnVisitDelete);
            pnlForm.Controls.Add(btnVisitClear);

            dgvVisits = new DataGridView();
            StyleGrid(dgvVisits);
            dgvVisits.SelectionChanged += new EventHandler(this.dgvVisits_SelectionChanged);

            Panel pnlGrid = new Panel();
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;

            pnlVisits.SizeChanged += (s, ev) =>
            {
                int formLeft = pnlVisits.Width - 300;
                pnlForm.Location = new Point(formLeft, 50);
                pnlGrid.Location = new Point(16, 50);
                pnlGrid.Size = new Size(formLeft - 24, pnlVisits.Height - 66);
                pnlForm.Height = pnlVisits.Height - 66;
                dgvVisits.Size = new Size(pnlGrid.Width - 2, pnlGrid.Height - 2);
                dgvVisits.Location = new Point(0, 0);
            };

            pnlGrid.Controls.Add(dgvVisits);
            pnlVisits.Controls.Add(lblHeader);
            pnlVisits.Controls.Add(pnlGrid);
            pnlVisits.Controls.Add(pnlForm);
        }

        // ─────────────────────────────────────
        // VACCINATIONS PANEL
        // ─────────────────────────────────────
        private void BuildVaccinationsPanel()
        {
            pnlVaccinations = new Panel();
            pnlVaccinations.Dock = DockStyle.Fill;
            pnlVaccinations.BackColor = Color.FromArgb(240, 244, 248);
            pnlVaccinations.Visible = false;

            Label lblHeader = new Label();
            lblHeader.Text = "💉 Vaccination Management";
            lblHeader.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(27, 42, 74);
            lblHeader.Location = new Point(16, 16);
            lblHeader.AutoSize = true;

            Panel pnlForm = new Panel();
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            pnlForm.Size = new Size(280, 440);

            nudVaccinationID = new NumericUpDown();
            nudVaccinationID.Visible = false;
            nudVaccinationID.Maximum = int.MaxValue;

            int y = 12;
            pnlForm.Controls.Add(CreateLabel("Visit"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            cmbVacVisit = new ComboBox();
            cmbVacVisit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVacVisit.Font = new Font("Segoe UI", 9f);
            cmbVacVisit.Width = 240;
            cmbVacVisit.Location = new Point(12, y);
            pnlForm.Controls.Add(cmbVacVisit);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Vaccine Inventory"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            cmbVacInventory = new ComboBox();
            cmbVacInventory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVacInventory.Font = new Font("Segoe UI", 9f);
            cmbVacInventory.Width = 240;
            cmbVacInventory.Location = new Point(12, y);
            pnlForm.Controls.Add(cmbVacInventory);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Vaccine Type"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            txtVaccineType = CreateTextBox(240);
            txtVaccineType.Location = new Point(12, y);
            pnlForm.Controls.Add(txtVaccineType);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Administered Date"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            dtpAdministered = new DateTimePicker();
            dtpAdministered.Font = new Font("Segoe UI", 9f);
            dtpAdministered.Width = 240;
            dtpAdministered.Location = new Point(12, y);
            pnlForm.Controls.Add(dtpAdministered);
            y += 36;

            pnlForm.Controls.Add(CreateLabel("Next Booster Due"));
            pnlForm.Controls[pnlForm.Controls.Count - 1].Location = new Point(12, y);
            y += 20;
            dtpNextBooster = new DateTimePicker();
            dtpNextBooster.Font = new Font("Segoe UI", 9f);
            dtpNextBooster.Width = 240;
            dtpNextBooster.Value = DateTime.Now.AddMonths(12);
            dtpNextBooster.Location = new Point(12, y);
            pnlForm.Controls.Add(dtpNextBooster);
            y += 44;

            btnVacAdd = CreateActionButton("Add", Color.FromArgb(46, 134, 171));
            btnVacAdd.Location = new Point(12, y);
            btnVacAdd.Click += new EventHandler(this.btnVacAdd_Click);

            btnVacUpdate = CreateActionButton("Update", Color.FromArgb(46, 134, 171));
            btnVacUpdate.Location = new Point(130, y);
            btnVacUpdate.Click += new EventHandler(this.btnVacUpdate_Click);
            y += 44;

            btnVacDelete = CreateActionButton("Delete", Color.FromArgb(230, 57, 70));
            btnVacDelete.Location = new Point(12, y);
            btnVacDelete.Click += new EventHandler(this.btnVacDelete_Click);

            btnVacClear = CreateActionButton("Clear", Color.FromArgb(108, 117, 125));
            btnVacClear.Location = new Point(130, y);
            btnVacClear.Click += new EventHandler(this.btnVacClear_Click);

            pnlForm.Controls.Add(nudVaccinationID);
            pnlForm.Controls.Add(btnVacAdd);
            pnlForm.Controls.Add(btnVacUpdate);
            pnlForm.Controls.Add(btnVacDelete);
            pnlForm.Controls.Add(btnVacClear);

            dgvVaccinations = new DataGridView();
            StyleGrid(dgvVaccinations);
            dgvVaccinations.SelectionChanged += new EventHandler(this.dgvVaccinations_SelectionChanged);

            Panel pnlGrid = new Panel();
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;

            pnlVaccinations.SizeChanged += (s, ev) =>
            {
                int formLeft = pnlVaccinations.Width - 300;
                pnlForm.Location = new Point(formLeft, 50);
                pnlGrid.Location = new Point(16, 50);
                pnlGrid.Size = new Size(formLeft - 24, pnlVaccinations.Height - 66);
                pnlForm.Height = pnlVaccinations.Height - 66;
                dgvVaccinations.Size = new Size(pnlGrid.Width - 2, pnlGrid.Height - 2);
                dgvVaccinations.Location = new Point(0, 0);
            };

            pnlGrid.Controls.Add(dgvVaccinations);
            pnlVaccinations.Controls.Add(lblHeader);
            pnlVaccinations.Controls.Add(pnlGrid);
            pnlVaccinations.Controls.Add(pnlForm);
        }

        // ─────────────────────────────────────
        // REPORTS PANEL
        // ─────────────────────────────────────
        private void BuildReportsPanel()
        {
            pnlReports = new Panel();
            pnlReports.Dock = DockStyle.Fill;
            pnlReports.BackColor = Color.FromArgb(240, 244, 248);
            pnlReports.Visible = false;

            Label lblHeader = new Label();
            lblHeader.Text = "📊 Reports";
            lblHeader.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(27, 42, 74);
            lblHeader.Location = new Point(16, 16);
            lblHeader.AutoSize = true;

            // Left button column
            Panel pnlButtons = new Panel();
            pnlButtons.BackColor = Color.White;
            pnlButtons.BorderStyle = BorderStyle.FixedSingle;
            pnlButtons.Size = new Size(220, 420);
            pnlButtons.Location = new Point(16, 50);

            string[] reportNames = new string[]
            {
                "1. Pets with Owners",
                "2. Visits Details",
                "3. Vaccinations/Pet",
                "4. No Visit 6mo",
                "5. Search Owners",
                "6. Reminders",
                "7. Low Stock",
                "8. Visits/Clinic"
            };

            EventHandler[] reportHandlers = new EventHandler[]
            {
                btnReport1_Click, btnReport2_Click, btnReport3_Click, btnReport4_Click,
                btnReport5_Click, btnReport6_Click, btnReport7_Click, btnReport8_Click
            };

            btnReport1 = null; btnReport2 = null; btnReport3 = null; btnReport4 = null;
            btnReport5 = null; btnReport6 = null; btnReport7 = null; btnReport8 = null;

            Button[] reportBtns = new Button[8];
            for (int i = 0; i < 8; i++)
            {
                Button rb = new Button();
                rb.Text = reportNames[i];
                rb.Size = new Size(200, 40);
                rb.Location = new Point(10, 10 + i * 48);
                rb.FlatStyle = FlatStyle.Flat;
                rb.FlatAppearance.BorderSize = 0;
                rb.BackColor = Color.FromArgb(23, 165, 137);
                rb.ForeColor = Color.White;
                rb.Font = new Font("Segoe UI", 9f);
                rb.Cursor = Cursors.Hand;
                rb.TextAlign = ContentAlignment.MiddleLeft;
                rb.Padding = new Padding(8, 0, 0, 0);
                rb.Click += new EventHandler(reportHandlers[i]);
                reportBtns[i] = rb;
                pnlButtons.Controls.Add(rb);
            }

            btnReport1 = reportBtns[0]; btnReport2 = reportBtns[1];
            btnReport3 = reportBtns[2]; btnReport4 = reportBtns[3];
            btnReport5 = reportBtns[4]; btnReport6 = reportBtns[5];
            btnReport7 = reportBtns[6]; btnReport8 = reportBtns[7];

            // Search for report 5
            txtReportSearch = CreateTextBox(200);
            txtReportSearch.Location = new Point(16, pnlButtons.Bottom + 10);
            //txtReportSearch.PlaceholderText = "Phone or email for Report 5...";

            // Report title
            lblReportTitle = new Label();
            lblReportTitle.Text = "Select a report";
            lblReportTitle.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblReportTitle.ForeColor = Color.FromArgb(27, 42, 74);
            lblReportTitle.AutoSize = true;

            // Grid
            dgvReports = new DataGridView();
            StyleGrid(dgvReports);
            dgvReports.ReadOnly = true;

            Panel pnlGridReport = new Panel();
            pnlGridReport.BackColor = Color.White;
            pnlGridReport.BorderStyle = BorderStyle.FixedSingle;

            pnlReports.SizeChanged += (s, ev) =>
            {
                pnlButtons.Location = new Point(16, 50);
                pnlButtons.Height = pnlReports.Height - 66;
                txtReportSearch.Location = new Point(16, pnlButtons.Bottom + 8);
                lblReportTitle.Location = new Point(252, 50);
                pnlGridReport.Location = new Point(252, 80);
                pnlGridReport.Size = new Size(pnlReports.Width - 268, pnlReports.Height - 96);
                dgvReports.Size = new Size(pnlGridReport.Width - 2, pnlGridReport.Height - 2);
                dgvReports.Location = new Point(0, 0);
            };

            pnlGridReport.Controls.Add(dgvReports);
            pnlReports.Controls.Add(lblHeader);
            pnlReports.Controls.Add(pnlButtons);
            pnlReports.Controls.Add(txtReportSearch);
            pnlReports.Controls.Add(lblReportTitle);
            pnlReports.Controls.Add(pnlGridReport);
        }

        #endregion

        // ── Controls Declaration ──
        private Panel pnlSidebar;
        private Panel pnlContent;
        private Label lblLogo;
        private Button btnDashboard;
        private Button btnOwners;
        private Button btnPets;
        private Button btnVisits;
        private Button btnVaccinations;
        private Button btnReports;

        // Dashboard
        private Panel pnlDashboard;
        private Label lblOwnerCount;
        private Label lblPetCount;
        private Label lblVisitCount;
        private Label lblReminderCount;

        // Owners
        private Panel pnlOwners;
        private DataGridView dgvOwners;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private TextBox txtBilling;
        private TextBox txtEmergency;
        private NumericUpDown nudOwnerID;
        private TextBox txtOwnerSearch;
        private Button btnOwnerAdd;
        private Button btnOwnerUpdate;
        private Button btnOwnerDelete;
        private Button btnOwnerClear;
        private Button btnOwnerSearch;

        // Pets
        private Panel pnlPets;
        private DataGridView dgvPets;
        private TextBox txtPetName;
        private TextBox txtBreed;
        private NumericUpDown nudAge;
        private ComboBox cmbSpecies;
        private ComboBox cmbOwnerID;
        private NumericUpDown nudPetID;
        private Button btnPetAdd;
        private Button btnPetUpdate;
        private Button btnPetDelete;
        private Button btnPetClear;

        // Visits
        private Panel pnlVisits;
        private DataGridView dgvVisits;
        private ComboBox cmbVisitPet;
        private ComboBox cmbVisitSlot;
        private DateTimePicker dtpVisitDate;
        private ComboBox cmbVisitStatus;
        private NumericUpDown nudVisitID;
        private Button btnVisitAdd;
        private Button btnVisitUpdate;
        private Button btnVisitDelete;
        private Button btnVisitUpdateStatus;
        private Button btnVisitClear;

        // Vaccinations
        private Panel pnlVaccinations;
        private DataGridView dgvVaccinations;
        private ComboBox cmbVacVisit;
        private ComboBox cmbVacInventory;
        private TextBox txtVaccineType;
        private DateTimePicker dtpAdministered;
        private DateTimePicker dtpNextBooster;
        private NumericUpDown nudVaccinationID;
        private Button btnVacAdd;
        private Button btnVacUpdate;
        private Button btnVacDelete;
        private Button btnVacClear;

        // Reports
        private Panel pnlReports;
        private Button btnReport1;
        private Button btnReport2;
        private Button btnReport3;
        private Button btnReport4;
        private Button btnReport5;
        private Button btnReport6;
        private Button btnReport7;
        private Button btnReport8;
        private TextBox txtReportSearch;
        private Label lblReportTitle;
        private DataGridView dgvReports;
    }
}
