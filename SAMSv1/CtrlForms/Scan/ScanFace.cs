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

namespace SAMSv1.CtrlForms.Scan
{
    public partial class ScanFace : DevExpress.XtraEditors.XtraUserControl
    {
        public ScanFace()
        {
            InitializeComponent();
        }

        private void cameraControl1_Click(object sender, EventArgs e)
        {

        }

        private void panelFace_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Color.FromArgb(200, 230, 220);
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Color.FromArgb(200, 230, 220);
        }

        private void ScanFace_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(200, 230, 220);
        }
    }
}
