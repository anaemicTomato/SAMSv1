using DevExpress.XtraEditors;
using SAMSv1.CtrlForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SAMSv1
{
    public partial class AdminForm : DevExpress.XtraEditors.XtraForm
    {
        public AdminForm()
        {
            InitializeComponent();
            panelControl2.Dock = DockStyle.Fill;  
            panelControl2.Padding = new Padding(20);
        }

        private void LoadControl(XtraUserControl control)
        {
            panelControl2.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelControl2.Controls.Add(control);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadControl(new AttendanceControl());
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            LoadControl(new RegisterStudentsControl());
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            LoadControl(new ReportControl());
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = panelControl2.ClientRectangle; // gradient sa background
            using (System.Drawing.Drawing2D.LinearGradientBrush brush =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    rect,
                    Color.FromArgb(15, 80, 90),    
                    Color.FromArgb(140, 200, 100),  
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, rect);
            }

        }

        private void tablePanel1_Paint(object sender, PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(10, 255, 255, 255)))
            // 👆 changed 30 to 10 = very transparent
            {
                e.Graphics.FillRectangle(brush, tablePanel1.ClientRectangle);
            }

        }

        private void ReportControl_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            tablePanel1.BackColor = Color.Transparent; // 👈 fully transparent
        }

    }
}