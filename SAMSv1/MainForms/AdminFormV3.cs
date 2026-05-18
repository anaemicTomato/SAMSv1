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
    public partial class AdminFormV3 : DevExpress.XtraEditors.XtraForm
    {
        public AdminFormV3()
        {
            InitializeComponent();
        }
        private void AdminFormV3_Load(object sender, EventArgs e)
        {

        }

        private void AttendanceModule_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear(); // remove previous page
            AttendanceControl page = new AttendanceControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        private void RegisterStudentModule_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear(); // remove previous page
            RegisterStudentsControl page = new RegisterStudentsControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

       

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement1_Click_1(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear(); // remove previous page
            AttendanceLogControl page = new AttendanceLogControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }
    }
}