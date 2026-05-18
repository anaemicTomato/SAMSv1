using DevExpress.XtraEditors;
using SAMSv1.Data;
using SAMSv1.Models;
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
        private readonly StudentRepository _repo = new StudentRepository();

        public RegisterStudentsControl()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void RegisterStudentsControl_Load(object sender, EventArgs e)
        {
 
        }


        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
        //=== Kyle things ======

        private void LoadStudents()
        {
            //gcStudent.DataSource = _repo.GetAllStudents().ToList();
        }

        private void btnRegisterStudent_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtFullname.Text) ||
            //    string.IsNullOrWhiteSpace(txtCourse.Text) ||
            //    string.IsNullOrWhiteSpace(txtIdNumber.Text))
            //{
            //    MessageBox.Show("Please fill in all fields.");
            //    return;
            //}

            //var student = new Student
            //{
            //    FullName = txtFullname.Text.Trim(),
            //    Course = txtCourse.Text.Trim(),
            //    IdNumber = txtIdNumber.Text.Trim()
            //};

            //_repo.RegisterStudent(student);

            //txtFullname.Clear();
            //txtCourse.Clear();
            //txtIdNumber.Clear();

            LoadStudents();
        }

    }
}
