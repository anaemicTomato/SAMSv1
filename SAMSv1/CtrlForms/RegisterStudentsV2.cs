using SAMSv1.Services;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SAMSv1.CtrlForms
{
    public partial class RegisterStudentsV2 : DevExpress.XtraEditors.XtraUserControl
    {
        // POLYMORPHISM: typed as abstract base — same pattern as AttendanceControl
        private AttendanceDevice _device => DeviceManager.Device;

        public RegisterStudentsV2()
        {
            InitializeComponent();
            this.Load += RegisterStudentsV2_Load;
        }

        // ═════════════════════════════════════════════════════════
        // LOAD
        // ═════════════════════════════════════════════════════════
        private void RegisterStudentsV2_Load(object sender, EventArgs e)
        {
            PopulateDropdowns();
            SetupGrid();

            // Sync from device + load local DB in background
            // so the UI doesn't freeze on startup
            System.Threading.ThreadPool.QueueUserWorkItem(_ => SyncAndLoadStudents());
        }

        // ═════════════════════════════════════════════════════════
        // DROPDOWNS
        // ═════════════════════════════════════════════════════════
        private void PopulateDropdowns()
        {
            cbProgram.Properties.Items.AddRange(new[]
            {
                "BSCS", "BSIT", "BSIS", "BSED", "BEED",
                "BSBA", "BSA", "BSCRIM", "BSN", "BSECE"
            });

            cbYearLevel.Properties.Items.AddRange(new[]
            {
                "1st Year", "2nd Year", "3rd Year", "4th Year"
            });
        }

        // ═════════════════════════════════════════════════════════
        // GRID SETUP
        // ═════════════════════════════════════════════════════════
        private void SetupGrid()
        {
            gridView1.Columns.Clear();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;

            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "StudentID",
                Caption = "#",
                Visible = true,
                Width = 50
            });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "IdNumber",
                Caption = "ID Number",
                Visible = true,
                Width = 130
            });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "FullName",
                Caption = "Full Name",
                Visible = true,
                Width = 220
            });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "Course",
                Caption = "Course",
                Visible = true,
                Width = 120
            });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "YearLevel",
                Caption = "Year Level",
                Visible = true,
                Width = 100
            });
        }

        // ═════════════════════════════════════════════════════════
        // SYNC FROM DEVICE + LOAD LOCAL DB
        // ═════════════════════════════════════════════════════════

        // POLYMORPHISM: calls GetEnrolledStudents() on abstract type —
        // resolves to HikvisionDevice.GetEnrolledStudents() at runtime
        private void SyncAndLoadStudents()
        {
            try
            {
                if (_device != null)
                {
                    var deviceStudents = _device.GetEnrolledStudents();
                    foreach (var s in deviceStudents)
                        FaceService.SaveStudentFromDevice(s.IdNumber, s.FullName);
                }
            }
            catch { /* device offline — still show local DB records */ }
            finally
            {
                RefreshGrid();
            }
        }

        private void RefreshGrid()
        {
            var students = FaceService.GetAllStudents();
            var source = new BindingList<FaceService.StudentGridRow>(students);

            Invoke((Action)(() =>
            {
                gcRegisteredStudents.DataSource = source;
                gcRegisteredStudents.RefreshDataSource();
            }));
        }

        // ═════════════════════════════════════════════════════════
        // REGISTER BUTTON
        // ═════════════════════════════════════════════════════════
        private void btnRegisterStudent_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string idNumber = txtIdNumber.Text.Trim();
            string course = cbProgram.Text.Trim();
            string yearLevel = cbYearLevel.Text.Trim();

            if (string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(idNumber) ||
                string.IsNullOrEmpty(course) ||
                string.IsNullOrEmpty(yearLevel))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "All fields are required.",
                    "Validation",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }

            btnRegisterStudent.Enabled = false;
            btnRegisterStudent.Text = "Registering...";

            // Capture values for background thread
            string capName = fullName;
            string capId = idNumber;
            string capCourse = course;
            string capYearLevel = yearLevel;

            System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                RegisterStudent(capId, capName, capCourse, capYearLevel));
        }

        private void RegisterStudent(string idNumber, string fullName,
                                     string course, string yearLevel)
        {
            bool deviceOk = false;

            try
            {
                // POLYMORPHISM: RegisterStudent() resolves to
                // HikvisionDevice.RegisterStudent() at runtime
                if (_device != null)
                    deviceOk = _device.RegisterStudent(idNumber, fullName);

                // Always save to local DB regardless of device response
                FaceService.SaveStudentFull(idNumber, fullName, course, yearLevel);

                // Refresh grid with new data
                RefreshGrid();

                Invoke((Action)(() =>
                {
                    string msg = deviceOk
                        ? $"✓ {fullName} registered successfully.\n" +
                          "They can now scan their face at the terminal."
                        : $"✓ {fullName} saved to database.\n" +
                          "⚠ Device registration failed — check connection.";

                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        msg,
                        "Registration",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        deviceOk
                            ? System.Windows.Forms.MessageBoxIcon.Information
                            : System.Windows.Forms.MessageBoxIcon.Warning);

                    // Clear form fields after success
                    txtFullName.Clear();
                    txtIdNumber.Clear();
                    cbProgram.EditValue = null;
                    cbYearLevel.EditValue = null;

                    btnRegisterStudent.Enabled = true;
                    btnRegisterStudent.Text = "Register Student";
                }));
            }
            catch (Exception ex)
            {
                Invoke((Action)(() =>
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Error: " + ex.Message,
                        "Registration Error",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);

                    btnRegisterStudent.Enabled = true;
                    btnRegisterStudent.Text = "Register Student";
                }));
            }
        }

        // ═════════════════════════════════════════════════════════
        // IMPORT IMAGE (reserved)
        // ═════════════════════════════════════════════════════════
        private void btnImportImage_Click(object sender, EventArgs e)
        {
            // Reserved for future implementation
        }
    }
}