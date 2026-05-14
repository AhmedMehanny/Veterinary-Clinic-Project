namespace PresentationLayer
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnVaccinations = new System.Windows.Forms.Button();
            this.btnVisits = new System.Windows.Forms.Button();
            this.btnPets = new System.Windows.Forms.Button();
            this.btnOwner = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pnlSidebar.Controls.Add(this.btnReports);
            this.pnlSidebar.Controls.Add(this.btnVaccinations);
            this.pnlSidebar.Controls.Add(this.btnVisits);
            this.pnlSidebar.Controls.Add(this.btnPets);
            this.pnlSidebar.Controls.Add(this.btnOwner);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(200, 450);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(12, 194);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(182, 23);
            this.btnReports.TabIndex = 5;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            // 
            // btnVaccinations
            // 
            this.btnVaccinations.Location = new System.Drawing.Point(12, 164);
            this.btnVaccinations.Name = "btnVaccinations";
            this.btnVaccinations.Size = new System.Drawing.Size(182, 23);
            this.btnVaccinations.TabIndex = 4;
            this.btnVaccinations.Text = "Vaccinations";
            this.btnVaccinations.UseVisualStyleBackColor = true;
            // 
            // btnVisits
            // 
            this.btnVisits.Location = new System.Drawing.Point(12, 134);
            this.btnVisits.Name = "btnVisits";
            this.btnVisits.Size = new System.Drawing.Size(182, 23);
            this.btnVisits.TabIndex = 3;
            this.btnVisits.Text = "Visits";
            this.btnVisits.UseVisualStyleBackColor = true;
            // 
            // btnPets
            // 
            this.btnPets.Location = new System.Drawing.Point(12, 104);
            this.btnPets.Name = "btnPets";
            this.btnPets.Size = new System.Drawing.Size(182, 23);
            this.btnPets.TabIndex = 2;
            this.btnPets.Text = "Pets";
            this.btnPets.UseVisualStyleBackColor = true;
            // 
            // btnOwner
            // 
            this.btnOwner.Location = new System.Drawing.Point(12, 75);
            this.btnOwner.Name = "btnOwner";
            this.btnOwner.Size = new System.Drawing.Size(182, 23);
            this.btnOwner.TabIndex = 1;
            this.btnOwner.Text = "Owners";
            this.btnOwner.UseVisualStyleBackColor = true;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(0, 10);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(200, 45);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(200, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(600, 450);
            this.pnlMain.TabIndex = 1;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "FormMain";
            this.Text = "Veterinary Clinic Management System";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnVaccinations;
        private System.Windows.Forms.Button btnVisits;
        private System.Windows.Forms.Button btnPets;
        private System.Windows.Forms.Button btnOwner;
        private System.Windows.Forms.Button btnDashboard;
    }
}