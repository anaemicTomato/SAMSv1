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
    public partial class ReportControl : DevExpress.XtraEditors.XtraUserControl
    {
        public ReportControl()
        {
            InitializeComponent();
        }

        private void ReportControl_Load(object sender, EventArgs e) // opacity sa pindot 
        {
            this.BackColor = Color.FromArgb(200, 230, 220);

        }

        protected override void OnPaintBackground(PaintEventArgs e) // opacity sa pindot
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(5, 255, 255, 255)))
           
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

      
    }
}
