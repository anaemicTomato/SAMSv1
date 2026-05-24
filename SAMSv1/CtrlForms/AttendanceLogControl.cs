using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SAMSv1.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMSv1.CtrlForms
{
    public partial class AttendanceLogControl : DevExpress.XtraEditors.XtraUserControl
    {
        public AttendanceLogControl()
        {
            InitializeComponent();
        }

        private void comboBoxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxYears_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        private void gridControl1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            var report = new StudentRPT();
            report.ShowPreviewDialog();
        }
    }
}
