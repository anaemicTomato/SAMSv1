using DevExpress.XtraEditors;
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
    public partial class StudentAttendanceControl : DevExpress.XtraEditors.XtraUserControl
    {
        public StudentAttendanceControl()
        {
            InitializeComponent();
        }

        private void btnStartAttendance_Click(object sender, EventArgs e)
        {
            btnStartAttendance.Visible = false;
            btnStopAttendance.Visible = true;
        }

        private void btnStopAttendance_Click(object sender, EventArgs e)
        {
            btnStartAttendance.Visible = true;
            btnStopAttendance.Visible = false;
        }
    }
}
