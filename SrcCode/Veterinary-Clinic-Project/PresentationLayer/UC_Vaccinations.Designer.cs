namespace PresentationLayer
{
    partial class UC_Vaccinations
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvVaccinations = new System.Windows.Forms.DataGridView();
            this.cboVisit = new System.Windows.Forms.ComboBox();
            this.txtInventoryId = new System.Windows.Forms.TextBox();
            this.txtVaccineType = new System.Windows.Forms.TextBox();
            this.dtpAdministered = new System.Windows.Forms.DateTimePicker();
            this.dtpBoosterDue = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccinations)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvVaccinations
            // 
            this.dgvVaccinations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVaccinations.Location = new System.Drawing.Point(216, 3);
            this.dgvVaccinations.Name = "dgvVaccinations";
            this.dgvVaccinations.RowHeadersWidth = 51;
            this.dgvVaccinations.RowTemplate.Height = 24;
            this.dgvVaccinations.Size = new System.Drawing.Size(845, 399);
            this.dgvVaccinations.TabIndex = 0;
            // 
            // cboVisit
            // 
            this.cboVisit.FormattingEnabled = true;
            this.cboVisit.Location = new System.Drawing.Point(39, 19);
            this.cboVisit.Name = "cboVisit";
            this.cboVisit.Size = new System.Drawing.Size(121, 24);
            this.cboVisit.TabIndex = 1;
            // 
            // txtInventoryId
            // 
            this.txtInventoryId.Location = new System.Drawing.Point(39, 80);
            this.txtInventoryId.Name = "txtInventoryId";
            this.txtInventoryId.Size = new System.Drawing.Size(100, 22);
            this.txtInventoryId.TabIndex = 2;
            // 
            // txtVaccineType
            // 
            this.txtVaccineType.Location = new System.Drawing.Point(39, 142);
            this.txtVaccineType.Name = "txtVaccineType";
            this.txtVaccineType.Size = new System.Drawing.Size(100, 22);
            this.txtVaccineType.TabIndex = 3;
            // 
            // dtpAdministered
            // 
            this.dtpAdministered.Location = new System.Drawing.Point(3, 220);
            this.dtpAdministered.Name = "dtpAdministered";
            this.dtpAdministered.Size = new System.Drawing.Size(200, 22);
            this.dtpAdministered.TabIndex = 4;
            // 
            // dtpBoosterDue
            // 
            this.dtpBoosterDue.Location = new System.Drawing.Point(3, 299);
            this.dtpBoosterDue.Name = "dtpBoosterDue";
            this.dtpBoosterDue.Size = new System.Drawing.Size(200, 22);
            this.dtpBoosterDue.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(288, 466);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(487, 466);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(670, 466);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(876, 466);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // UC_Vaccinations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dtpBoosterDue);
            this.Controls.Add(this.dtpAdministered);
            this.Controls.Add(this.txtVaccineType);
            this.Controls.Add(this.txtInventoryId);
            this.Controls.Add(this.cboVisit);
            this.Controls.Add(this.dgvVaccinations);
            this.Name = "UC_Vaccinations";
            this.Size = new System.Drawing.Size(1064, 573);
            //this.Load += new System.EventHandler(this.UC_Vaccinations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccinations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVaccinations;
        private System.Windows.Forms.ComboBox cboVisit;
        private System.Windows.Forms.TextBox txtInventoryId;
        private System.Windows.Forms.TextBox txtVaccineType;
        private System.Windows.Forms.DateTimePicker dtpAdministered;
        private System.Windows.Forms.DateTimePicker dtpBoosterDue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
    }
}
