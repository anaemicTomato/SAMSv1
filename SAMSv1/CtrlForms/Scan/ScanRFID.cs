using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMSv1.CtrlForms.Scan
{
    public partial class ScanRFID : DevExpress.XtraEditors.XtraUserControl
    {
        public ScanRFID()
        {
            InitializeComponent();
        }

        private void btnRFID_Click(object sender, EventArgs e)
        {

        }

        private void btnScanFace_Click(object sender, EventArgs e)
        {

        }

        private void panelRFID_Paint(object sender, PaintEventArgs e)
        {
           

        }

        private void paneltop_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Color.FromArgb(200, 230, 220);
        }

        private void ScanRFID_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(200, 230, 220);
        }
    }
}
