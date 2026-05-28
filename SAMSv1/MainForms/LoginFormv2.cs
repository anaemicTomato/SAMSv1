using Dapper;
using DevExpress.XtraWaitForm;
using SAMSv1.Data;
using System;
using System.Windows.Forms;
using SAMSv1.Models;


namespace SAMSv1.MainForms
{
    public partial class LoginFormv2 : DevExpress.XtraEditors.XtraForm
    {
        public LoginFormv2()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                using (var connection = DBHelper.GetConnection())
                {
                    string query = @"
                        SELECT *
                        FROM UserTable
                        WHERE Username = @Username
                        AND Password = @Password";

                    var user = connection.QueryFirstOrDefault<User>(
                        query,
                        new
                        {
                            Username = username,
                            Password = password
                        });

                    if (user != null)
                    {
                        MessageBox.Show("Login Successful!");

                        AdminFormV3 adminform = new AdminFormV3(user);

                        this.Hide();

                        adminform.ShowDialog();

                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Invalid username or password.",
                            "Login Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}