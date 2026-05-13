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
//    public partial class frmToast : Form
//    {
//        public frmToast()
//        {
//            InitializeComponent();
//        }

//        private void frmToast_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}

using System;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmToast : Form
    {
        private Timer timer;

        public frmToast(string message, string type = "info")
        {
            InitializeComponent();
            lblMessage.Text = message;
            // Change color based on type
            switch (type.ToLower())
            {
                case "success": this.BackColor = System.Drawing.Color.FromArgb(40, 167, 69); break;
                case "error": this.BackColor = System.Drawing.Color.FromArgb(220, 53, 69); break;
                case "warning": this.BackColor = System.Drawing.Color.FromArgb(255, 193, 7); break;
                default: this.BackColor = System.Drawing.Color.FromArgb(23, 162, 184); break;
            }
            timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += (s, e) => { timer.Stop(); this.Close(); };
            timer.Start();
        }

        private void InitializeComponent()
        {
            this.lblMessage = new Label();
            this.SuspendLayout();
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(20, 20);
            this.lblMessage.MaximumSize = new System.Drawing.Size(350, 0);
            this.lblMessage.Text = "Message";
            this.ClientSize = new System.Drawing.Size(400, 70);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Controls.Add(this.lblMessage);
            this.ResumeLayout(false);
        }

        private Label lblMessage;
    }
}
