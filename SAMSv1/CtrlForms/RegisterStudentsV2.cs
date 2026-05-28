using SAMSv1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SAMSv1.Models;

namespace SAMSv1.CtrlForms
{
    public partial class RegisterStudentsV2 : DevExpress.XtraEditors.XtraUserControl
    {
        private AttendanceDevice _device => DeviceManager.Device;

        // Tracks which student is selected in the grid
        private int _selectedStudentID = -1;
        private string _selectedIdNumber = string.Empty;

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
            System.Threading.ThreadPool.QueueUserWorkItem(_ => SyncAndLoadStudents());

            // Wire grid click — fills form fields when row is selected
            gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
        }

        // ═════════════════════════════════════════════════════════
        // GRID ROW CLICK — auto-fill form fields
        // ═════════════════════════════════════════════════════════
        private void GridView1_FocusedRowChanged(
            object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int row = gridView1.FocusedRowHandle;
            if (row < 0) return;

            // Get the Student object bound to this row
            var student = gridView1.GetRow(row) as Student;
            if (student == null) return;

            // Store selected student for Update/Delete
            _selectedStudentID = student.StudentID;
            _selectedIdNumber = student.IdNumber;

            // Fill form fields
            txtFullName.Text = student.FullName;
            txtIdNumber.Text = student.IdNumber;
            cbProgram.EditValue = student.Course;
            cbYearLevel.EditValue = student.YearLevel;
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
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            { FieldName = "IdNumber", Caption = "ID Number", Visible = true, Width = 130 });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            { FieldName = "FullName", Caption = "Full Name", Visible = true, Width = 220 });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            { FieldName = "Course", Caption = "Course", Visible = true, Width = 120 });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            { FieldName = "YearLevel", Caption = "Year Level", Visible = true, Width = 100 });
        }

        // ═════════════════════════════════════════════════════════
        // SYNC FROM DEVICE + LOAD LOCAL DB
        // ═════════════════════════════════════════════════════════
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
            catch { }
            finally { RefreshGrid(); }
        }

        private void RefreshGrid()
        {
            var students = FaceService.GetAllStudents();
            var source = new BindingList<Student>(students);

            SafeInvoke(() =>
            {
                gcRegisteredStudents.DataSource = source;
                gcRegisteredStudents.RefreshDataSource();
            });
        }

        private void ClearForm()
        {
            txtFullName.Text = string.Empty;
            txtIdNumber.Text = string.Empty;
            cbProgram.EditValue = null;
            cbYearLevel.EditValue = null;
            _selectedStudentID = -1;
            _selectedIdNumber = string.Empty;
        }

        // ═════════════════════════════════════════════════════════
        // REGISTER BUTTON — adds a new student
        // ═════════════════════════════════════════════════════════
        private void btnRegisterStudent_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            string fullName = txtFullName.Text.Trim();
            string idNumber = txtIdNumber.Text.Trim();
            string course = cbProgram.Text.Trim();
            string yearLevel = cbYearLevel.Text.Trim();

            btnRegisterStudent.Enabled = false;
            btnRegisterStudent.Text = "Registering...";

            System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                RegisterStudent(idNumber, fullName, course, yearLevel));
        }

        private void RegisterStudent(string idNumber, string fullName,
                                     string course, string yearLevel)
        {
            bool deviceOk = false;
            try
            {
                if (_device != null)
                    deviceOk = _device.RegisterStudent(idNumber, fullName);

                FaceService.SaveStudentFull(idNumber, fullName, course, yearLevel);
                RefreshGrid();

                SafeInvoke(() =>
                {
                    string msg = deviceOk
                        ? $"✓ {fullName} registered successfully.\n" +
                          "They can now scan their face at the terminal."
                        : $"✓ {fullName} saved to database.\n" +
                          "⚠ Device registration failed — check connection.";

                    DevExpress.XtraEditors.XtraMessageBox.Show(msg, "Registration",
                        MessageBoxButtons.OK,
                        deviceOk ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                    ClearForm();
                    btnRegisterStudent.Enabled = true;
                    btnRegisterStudent.Text = "REGISTER";
                });
            }
            catch (Exception ex)
            {
                SafeInvoke(() =>
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Error: " + ex.Message, "Registration Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRegisterStudent.Enabled = true;
                    btnRegisterStudent.Text = "REGISTER";
                });
            }
        }

        // ═════════════════════════════════════════════════════════
        // UPDATE BUTTON — updates selected student
        // ═════════════════════════════════════════════════════════
        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            if (_selectedStudentID < 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Please select a student from the list first.",
                    "No Student Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm()) return;

            string fullName = txtFullName.Text.Trim();
            string idNumber = txtIdNumber.Text.Trim();
            string course = cbProgram.Text.Trim();
            string yearLevel = cbYearLevel.Text.Trim();

            btnUpdateStudent.Enabled = false;
            btnUpdateStudent.Text = "Updating...";

            string oldIdNumber = _selectedIdNumber;

            System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                UpdateStudent(oldIdNumber, idNumber, fullName, course, yearLevel));
        }

        private void UpdateStudent(string oldIdNumber, string newIdNumber,
                                   string fullName, string course, string yearLevel)
        {
            bool deviceOk = false;
            try
            {
                // Update on device — delete old + register new if ID changed
                if (_device != null)
                {
                    if (oldIdNumber != newIdNumber)
                        _device.DeleteStudent(oldIdNumber);
                    deviceOk = _device.RegisterStudent(newIdNumber, fullName);
                }

                // Update in local DB
                FaceService.UpdateStudent(newIdNumber, fullName, course, yearLevel, oldIdNumber);
                RefreshGrid();

                SafeInvoke(() =>
                {
                    string msg = deviceOk
                        ? $"✓ {fullName} updated successfully."
                        : $"✓ {fullName} updated in database.\n" +
                          "⚠ Device update failed — check connection.";

                    DevExpress.XtraEditors.XtraMessageBox.Show(msg, "Update",
                        MessageBoxButtons.OK,
                        deviceOk ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                    ClearForm();
                    btnUpdateStudent.Enabled = true;
                    btnUpdateStudent.Text = "UPDATE";
                });
            }
            catch (Exception ex)
            {
                SafeInvoke(() =>
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Error: " + ex.Message, "Update Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnUpdateStudent.Enabled = true;
                    btnUpdateStudent.Text = "UPDATE";
                });
            }
        }

        // ═════════════════════════════════════════════════════════
        // DELETE BUTTON — removes selected student
        // ═════════════════════════════════════════════════════════
        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (_selectedStudentID < 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Please select a student from the list first.",
                    "No Student Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtFullName.Text.Trim();
            var confirm = DevExpress.XtraEditors.XtraMessageBox.Show(
                $"Are you sure you want to delete {name}?\n\n" +
                "This will remove them from the device and the database.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            btnDeleteStudent.Enabled = false;
            btnDeleteStudent.Text = "Deleting...";

            string idToDelete = _selectedIdNumber;

            System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                DeleteStudent(idToDelete, name));
        }

        private void DeleteStudent(string idNumber, string fullName)
        {
            bool deviceOk = false;
            try
            {
                if (_device != null)
                    deviceOk = _device.DeleteStudent(idNumber);

                FaceService.DeleteStudent(idNumber);
                RefreshGrid();

                SafeInvoke(() =>
                {
                    string msg = deviceOk
                        ? $"✓ {fullName} deleted successfully."
                        : $"✓ {fullName} removed from database.\n" +
                          "⚠ Device deletion failed — check connection.";

                    DevExpress.XtraEditors.XtraMessageBox.Show(msg, "Delete",
                        MessageBoxButtons.OK,
                        deviceOk ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                    ClearForm();
                    btnDeleteStudent.Enabled = true;
                    btnDeleteStudent.Text = "DELETE";
                });
            }
            catch (Exception ex)
            {
                SafeInvoke(() =>
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Error: " + ex.Message, "Delete Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDeleteStudent.Enabled = true;
                    btnDeleteStudent.Text = "DELETE";
                });
            }
        }

        // ═════════════════════════════════════════════════════════
        // VALIDATION
        // ═════════════════════════════════════════════════════════
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtIdNumber.Text) ||
                string.IsNullOrWhiteSpace(cbProgram.Text) ||
                string.IsNullOrWhiteSpace(cbYearLevel.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "All fields are required.",
                    "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ═════════════════════════════════════════════════════════
        // HELPERS
        // ═════════════════════════════════════════════════════════
        private void SafeInvoke(Action action)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try { Invoke(action); }
            catch { }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
        }

        private void btnClearFields_Click(object sender, EventArgs e)
        {

        }
    }
}