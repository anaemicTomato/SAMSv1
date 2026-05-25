using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SAMSv1.Data;
using SAMSv1.Models;
using System;
using System.Windows.Forms;

namespace SAMSv1.CtrlForms
{
    public partial class UserManagementControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly UserRepository _userRepo = new UserRepository();
        private int selectedUserId = 0;

        private void LoadUsers()
        {
            gcUsers.DataSource = _userRepo.GetAllUsers();
        }

        public UserManagementControl()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            User user = new User
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Role = cbRole.Text
            };

            _userRepo.AddUser(user);

            LoadUsers();

            MessageBox.Show("User Added");
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            User user = new User
            {
                UserID = selectedUserId,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Role = cbRole.Text
            };

            _userRepo.UpdateUser(user);

            LoadUsers();

            MessageBox.Show("User Updated");
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Select a user first.");
                return;
            }

            _userRepo.DeleteUser(selectedUserId);

            LoadUsers();

            ClearFields();

            MessageBox.Show("User Deleted");
        }

        private void btnClearFields_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            selectedUserId = 0;

            txtUsername.Clear();
            txtPassword.Clear();

            cbRole.SelectedIndex = -1;
        }

        private void gvUsers_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = gvUsers.GetFocusedRow() as User;

            if (row != null)
            {
                selectedUserId = row.UserID;

                txtUsername.Text = row.Username;
                txtPassword.Text = row.Password;
                cbRole.Text = row.Role;
            }
        }
    }
}
