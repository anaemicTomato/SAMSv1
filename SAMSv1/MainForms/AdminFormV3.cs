using DevExpress.XtraEditors;
using SAMSv1.CtrlForms;
using SAMSv1.Data;
using SAMSv1.Services;
using SAMSv1.Models;
using System;
using System.Windows.Forms;

namespace SAMSv1.MainForms
{
    public partial class AdminFormV3 : DevExpress.XtraEditors.XtraForm
    {
        private User _currentUser; //para ni sa role detection.
        private readonly FaceService _faceService = new FaceService();

        public AdminFormV3() { InitializeComponent(); } //temporary rani aron ma call ni nga form sa Program.cs kuhaon ra if ganahan namo mag login2

        public AdminFormV3(User user)
        {
            InitializeComponent();

            _currentUser = user;
        }


        private void AdminFormV3_Load(object sender, EventArgs e)
        {
            try
            {
                DeviceManager.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization failed:\n{ex.Message}", "Startup Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CanSwitchModule()
        {
            if (StudentAttendanceControl.IsLiveRunning)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Live attendance is currently running.\n\n" +
                    "Please click STOP on the Attendance module before switching.",
                    "Live Attendance Active",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ── Attendance module ──────────────────────────────────────────
        private void AttendanceModule_Click(object sender, EventArgs e)
        {
            // No guard here — user is going TO attendance, always allow
            mainPanel.Controls.Clear();
            StudentAttendanceControl page = new StudentAttendanceControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        // ── Register Students module ───────────────────────────────────
        private void RegisterStudentModule_Click(object sender, EventArgs e)
        {
            if (!CanSwitchModule()) return;
            mainPanel.Controls.Clear();
            RegisterStudentsV2 page = new RegisterStudentsV2();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        // ── Attendance Log module ──────────────────────────────────────
        private void accordionControlElement1_Click_1(object sender, EventArgs e)
        {
            if (!CanSwitchModule()) return;
            mainPanel.Controls.Clear();
            AttendanceLogControl page = new AttendanceLogControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        // ── User Management module ──────────────────────────────────────
        private void ManageUsersModule_Click(object sender, EventArgs e)
        {
            if (!CanSwitchModule()) return;
            mainPanel.Controls.Clear();
            UserManagementControl page = new UserManagementControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        private void acGenerateReport_Click(object sender, EventArgs e)
        {
            if (!CanSwitchModule()) return;
            mainPanel.Controls.Clear();
            ReportControl page = new ReportControl();
            page.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(page);
        }

        // ── Logout button ──────────────────────────────────────
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private async void AdminFormV3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // hold the close

            // Find the attendance control if it's currently loaded
            foreach (Control ctrl in mainPanel.Controls)
            {
                if (ctrl is StudentAttendanceControl attendance)
                {
                    await attendance.StopAsync();
                    break;
                }
            }

            DeviceManager.Shutdown();

            e.Cancel = false;
            Application.Exit();
        }

        private void accordionContentContainer1_Click(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {

        }

        
    }
}