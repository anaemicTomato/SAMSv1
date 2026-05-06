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
    public partial class RegisterStudentsControl : DevExpress.XtraEditors.XtraUserControl
    {
        public RegisterStudentsControl()
        {
            InitializeComponent();
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {


        }

        private void RegisterStudentsControl_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(200, 230, 220);
        }

        protected override void OnPaintBackground(PaintEventArgs e) // opacity sa pindot
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(10, 255, 255, 255)))

            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
