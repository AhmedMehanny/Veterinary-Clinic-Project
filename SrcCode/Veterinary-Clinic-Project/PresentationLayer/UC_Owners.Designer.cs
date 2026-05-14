namespace PresentationLayer
{
    partial class UC_Owners
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
            this.components = new System.ComponentModel.Container();
            this.dgvOwners = new System.Windows.Forms.DataGridView();
            this.oWNERIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oFRISTNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oLASTNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oPHONEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oEMAILDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bILLINGADDRESSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eMERGENCYCONTACTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oWNERBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vetClinicDataSet = new PresentationLayer.VetClinicDataSet();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblBillingAddress = new System.Windows.Forms.Label();
            this.lblEmergencyContact = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtBillingAddress = new System.Windows.Forms.TextBox();
            this.txtEmergencyContact = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.vetClinicDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.oWNERTableAdapter = new PresentationLayer.VetClinicDataSetTableAdapters.OWNERTableAdapter();
            this.tableAdapterManager = new PresentationLayer.VetClinicDataSetTableAdapters.TableAdapterManager();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOwners)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oWNERBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vetClinicDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vetClinicDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOwners
            // 
            this.dgvOwners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOwners.AutoGenerateColumns = false;
            this.dgvOwners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOwners.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oWNERIDDataGridViewTextBoxColumn,
            this.oFRISTNAMEDataGridViewTextBoxColumn,
            this.oLASTNAMEDataGridViewTextBoxColumn,
            this.oPHONEDataGridViewTextBoxColumn,
            this.oEMAILDataGridViewTextBoxColumn,
            this.bILLINGADDRESSDataGridViewTextBoxColumn,
            this.eMERGENCYCONTACTDataGridViewTextBoxColumn});
            this.dgvOwners.DataSource = this.oWNERBindingSource;
            this.dgvOwners.Location = new System.Drawing.Point(0, 0);
            this.dgvOwners.Name = "dgvOwners";
            this.dgvOwners.RowHeadersWidth = 51;
            this.dgvOwners.RowTemplate.Height = 24;
            this.dgvOwners.Size = new System.Drawing.Size(928, 250);
            this.dgvOwners.TabIndex = 0;
            // 
            // oWNERIDDataGridViewTextBoxColumn
            // 
            this.oWNERIDDataGridViewTextBoxColumn.DataPropertyName = "OWNERID";
            this.oWNERIDDataGridViewTextBoxColumn.HeaderText = "OWNERID";
            this.oWNERIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.oWNERIDDataGridViewTextBoxColumn.Name = "oWNERIDDataGridViewTextBoxColumn";
            this.oWNERIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // oFRISTNAMEDataGridViewTextBoxColumn
            // 
            this.oFRISTNAMEDataGridViewTextBoxColumn.DataPropertyName = "OFRISTNAME";
            this.oFRISTNAMEDataGridViewTextBoxColumn.HeaderText = "OFRISTNAME";
            this.oFRISTNAMEDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.oFRISTNAMEDataGridViewTextBoxColumn.Name = "oFRISTNAMEDataGridViewTextBoxColumn";
            this.oFRISTNAMEDataGridViewTextBoxColumn.Width = 125;
            // 
            // oLASTNAMEDataGridViewTextBoxColumn
            // 
            this.oLASTNAMEDataGridViewTextBoxColumn.DataPropertyName = "OLASTNAME";
            this.oLASTNAMEDataGridViewTextBoxColumn.HeaderText = "OLASTNAME";
            this.oLASTNAMEDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.oLASTNAMEDataGridViewTextBoxColumn.Name = "oLASTNAMEDataGridViewTextBoxColumn";
            this.oLASTNAMEDataGridViewTextBoxColumn.Width = 125;
            // 
            // oPHONEDataGridViewTextBoxColumn
            // 
            this.oPHONEDataGridViewTextBoxColumn.DataPropertyName = "OPHONE";
            this.oPHONEDataGridViewTextBoxColumn.HeaderText = "OPHONE";
            this.oPHONEDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.oPHONEDataGridViewTextBoxColumn.Name = "oPHONEDataGridViewTextBoxColumn";
            this.oPHONEDataGridViewTextBoxColumn.Width = 125;
            // 
            // oEMAILDataGridViewTextBoxColumn
            // 
            this.oEMAILDataGridViewTextBoxColumn.DataPropertyName = "OEMAIL";
            this.oEMAILDataGridViewTextBoxColumn.HeaderText = "OEMAIL";
            this.oEMAILDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.oEMAILDataGridViewTextBoxColumn.Name = "oEMAILDataGridViewTextBoxColumn";
            this.oEMAILDataGridViewTextBoxColumn.Width = 125;
            // 
            // bILLINGADDRESSDataGridViewTextBoxColumn
            // 
            this.bILLINGADDRESSDataGridViewTextBoxColumn.DataPropertyName = "BILLINGADDRESS";
            this.bILLINGADDRESSDataGridViewTextBoxColumn.HeaderText = "BILLINGADDRESS";
            this.bILLINGADDRESSDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.bILLINGADDRESSDataGridViewTextBoxColumn.Name = "bILLINGADDRESSDataGridViewTextBoxColumn";
            this.bILLINGADDRESSDataGridViewTextBoxColumn.Width = 125;
            // 
            // eMERGENCYCONTACTDataGridViewTextBoxColumn
            // 
            this.eMERGENCYCONTACTDataGridViewTextBoxColumn.DataPropertyName = "EMERGENCYCONTACT";
            this.eMERGENCYCONTACTDataGridViewTextBoxColumn.HeaderText = "EMERGENCYCONTACT";
            this.eMERGENCYCONTACTDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.eMERGENCYCONTACTDataGridViewTextBoxColumn.Name = "eMERGENCYCONTACTDataGridViewTextBoxColumn";
            this.eMERGENCYCONTACTDataGridViewTextBoxColumn.Width = 125;
            // 
            // oWNERBindingSource
            // 
            this.oWNERBindingSource.DataMember = "OWNER";
            this.oWNERBindingSource.DataSource = this.vetClinicDataSet;
            // 
            // vetClinicDataSet
            // 
            this.vetClinicDataSet.DataSetName = "VetClinicDataSet";
            this.vetClinicDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(89, 314);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(72, 16);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "First Name";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(89, 361);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(72, 16);
            this.lblLastName.TabIndex = 2;
            this.lblLastName.Text = "Last Name";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(89, 399);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(46, 16);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Phone";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(89, 443);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(41, 16);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email";
            // 
            // lblBillingAddress
            // 
            this.lblBillingAddress.AutoSize = true;
            this.lblBillingAddress.Location = new System.Drawing.Point(294, 399);
            this.lblBillingAddress.Name = "lblBillingAddress";
            this.lblBillingAddress.Size = new System.Drawing.Size(58, 16);
            this.lblBillingAddress.TabIndex = 5;
            this.lblBillingAddress.Text = "Address";
            // 
            // lblEmergencyContact
            // 
            this.lblEmergencyContact.AutoSize = true;
            this.lblEmergencyContact.Location = new System.Drawing.Point(294, 443);
            this.lblEmergencyContact.Name = "lblEmergencyContact";
            this.lblEmergencyContact.Size = new System.Drawing.Size(124, 16);
            this.lblEmergencyContact.TabIndex = 6;
            this.lblEmergencyContact.Text = "Emergency Contact";
            // 
            // txtFirstName
            // 
            this.txtFirstName.CausesValidation = false;
            this.txtFirstName.Location = new System.Drawing.Point(158, 308);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(100, 22);
            this.txtFirstName.TabIndex = 7;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(158, 358);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(100, 22);
            this.txtLastName.TabIndex = 8;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(158, 393);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(100, 22);
            this.txtPhone.TabIndex = 9;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(158, 443);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 22);
            this.txtEmail.TabIndex = 10;
            // 
            // txtBillingAddress
            // 
            this.txtBillingAddress.Location = new System.Drawing.Point(455, 393);
            this.txtBillingAddress.Name = "txtBillingAddress";
            this.txtBillingAddress.Size = new System.Drawing.Size(100, 22);
            this.txtBillingAddress.TabIndex = 11;
            // 
            // txtEmergencyContact
            // 
            this.txtEmergencyContact.Location = new System.Drawing.Point(455, 437);
            this.txtEmergencyContact.Name = "txtEmergencyContact";
            this.txtEmergencyContact.Size = new System.Drawing.Size(100, 22);
            this.txtEmergencyContact.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(125, 502);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(232, 502);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(343, 502);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(455, 502);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // vetClinicDataSetBindingSource
            // 
            this.vetClinicDataSetBindingSource.DataSource = this.vetClinicDataSet;
            this.vetClinicDataSetBindingSource.Position = 0;
            // 
            // oWNERTableAdapter
            // 
            this.oWNERTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.APPOINTMENT_SLOTTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CLINICAL_NOTETableAdapter = null;
            this.tableAdapterManager.CLINICTableAdapter = null;
            this.tableAdapterManager.MEDICAL_VISITTableAdapter = null;
            this.tableAdapterManager.OWNERTableAdapter = this.oWNERTableAdapter;
            this.tableAdapterManager.PETTableAdapter = null;
            this.tableAdapterManager.REMINDERTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = PresentationLayer.VetClinicDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.VACCINATIONTableAdapter = null;
            this.tableAdapterManager.VACCINE_INVENTORYTableAdapter = null;
            this.tableAdapterManager.VET_CLINICTableAdapter = null;
            this.tableAdapterManager.VETERINARIANTableAdapter = null;
            // 
            // UC_Owners
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtEmergencyContact);
            this.Controls.Add(this.txtBillingAddress);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblEmergencyContact);
            this.Controls.Add(this.lblBillingAddress);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.dgvOwners);
            this.Name = "UC_Owners";
            this.Size = new System.Drawing.Size(928, 567);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOwners)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oWNERBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vetClinicDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vetClinicDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOwners;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblBillingAddress;
        private System.Windows.Forms.Label lblEmergencyContact;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtBillingAddress;
        private System.Windows.Forms.TextBox txtEmergencyContact;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn oWNERIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oFRISTNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oLASTNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oPHONEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oEMAILDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bILLINGADDRESSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eMERGENCYCONTACTDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource oWNERBindingSource;
        private VetClinicDataSet vetClinicDataSet;
        private System.Windows.Forms.BindingSource vetClinicDataSetBindingSource;
        private VetClinicDataSetTableAdapters.OWNERTableAdapter oWNERTableAdapter;
        private VetClinicDataSetTableAdapters.TableAdapterManager tableAdapterManager;
    }
}
