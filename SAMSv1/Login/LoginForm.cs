using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraReports.UI;
using SAMSv1.MainForms;
using SAMSv1.Models;
using SAMSv1.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMSv1.Login
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void PanelControl1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(0, 0, 20, 20, 180, 90);
            path.AddArc(panelBackground.Width - 20, 0, 20, 20, 270, 90);
            path.AddArc(panelBackground.Width - 20, panelBackground.Height - 20, 20, 20, 0, 90);
            path.AddArc(0, panelBackground.Height - 20, 20, 20, 90, 90);

            path.CloseAllFigures();

            panelBackground.Region = new Region(path);


            panelBackground.Appearance.BackColor = Color.FromArgb(100, 230, 255, 240);
            panelBackground.Appearance.Options.UseBackColor = true;

            panelBackground.Region = new Region(path);
            using (Pen borderPen = new Pen(Color.FromArgb(220, 0, 120, 60), 2.5f))

            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(borderPen, path);
            }


            panelBackground.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder; 
            // para ni sa glass effects sa background sa login
        }

      

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void teStudentID_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tePassword_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string studentID = teStudentID.Text.Trim();
            string password = tePassword.Text.Trim();

            if (studentID == "admin" && password == "1234")
            {
                MessageBox.Show(
                    "Login Successful!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                AdminFormv2 dashboard = new AdminFormv2();
                dashboard.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show(
                    "Invalid Student ID or Password",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);


            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            StudentRPT report = new StudentRPT();
            //report.DataSource = ""query
            report.ShowPreviewDialog();
        }
    }
}