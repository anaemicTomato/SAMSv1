using DevExpress.XtraEditors;
using SAMSv1.CtrlForms;
using SAMSv1.Data;
using SAMSv1.Services;
using System;
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

            try
            {
                FaceService.Init(DBHelper.ConnectionString);
                DeviceManager.Initialize(
                    new HikvisionDevice("192.168.1.65", 8000, "admin", "DMC2026#")
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization failed:\n{ex.Message}", "Startup Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Attendance module ──────────────────────────────────────────
        private void AttendanceModule_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            AttendanceControl page = new AttendanceControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        // ── Register Students module ───────────────────────────────────
        private void RegisterStudentModule_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            RegisterStudentsV2 page = new RegisterStudentsV2();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        // ── Attendance Log module ──────────────────────────────────────
        private void accordionControlElement1_Click_1(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            AttendanceLogControl page = new AttendanceLogControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            HikvisionDevice.SDKCleanup();
            base.OnFormClosing(e);
        }

        private void accordionControl1_Click(object sender, EventArgs e) { }
    }
}