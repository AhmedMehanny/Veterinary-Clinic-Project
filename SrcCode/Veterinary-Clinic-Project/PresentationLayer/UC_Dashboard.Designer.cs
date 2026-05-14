namespace PresentationLayer
{
    partial class UC_Dashboard
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
            this.SuspendLayout();
            // 
            // UC_Dashboard
            // 
            this.Name = "UC_Dashboard";
            this.Size = new System.Drawing.Size(1020, 588);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelTotalVisits;
        private System.Windows.Forms.Label lblTotalVisits;
        private System.Windows.Forms.Label lblTotalVisitsCaption;
        private System.Windows.Forms.Panel panelOverdueBoosters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOverdueBoosters;
        private System.Windows.Forms.Panel panelTotalClinics;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalClinics;
    }
}
