using DevExpress.XtraEditors;
using SAMSv1.CtrlForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMSv1.MainForms
{
    public partial class AdminFormv2 : DevExpress.XtraEditors.XtraForm
    {
        public AdminFormv2()
        {
            InitializeComponent();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear(); // remove previous page
            AttendanceControl page = new AttendanceControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        private void btnRegisterStudent_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear(); // remove previous page
            RegisterStudentsControl page = new RegisterStudentsControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear(); // remove previous page
            ReportControl page = new ReportControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }
         
        public static implicit operator AdminFormv2(AdminForm v)
        {
            throw new NotImplementedException();
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}