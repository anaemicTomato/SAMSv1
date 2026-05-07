namespace SAMSv1.CtrlForms
{
    partial class RegisterStudentsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFullname = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtIdNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCourse = new DevExpress.XtraEditors.TextEdit();
            this.btnRegisterStudent = new DevExpress.XtraEditors.SimpleButton();
            this.gcStudent = new DevExpress.XtraGrid.GridControl();
            this.gvStudent = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCourse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStudent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStudent)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFullname
            // 
            this.txtFullname.Location = new System.Drawing.Point(92, 75);
            this.txtFullname.Name = "txtFullname";
            this.txtFullname.Size = new System.Drawing.Size(294, 40);
            this.txtFullname.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(92, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 19);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Full Name:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(92, 163);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 19);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "ID Number:";
            // 
            // txtIdNumber
            // 
            this.txtIdNumber.Location = new System.Drawing.Point(92, 188);
            this.txtIdNumber.Name = "txtIdNumber";
            this.txtIdNumber.Size = new System.Drawing.Size(280, 40);
            this.txtIdNumber.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(452, 50);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 19);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Course:";
            // 
            // txtCourse
            // 
            this.txtCourse.Location = new System.Drawing.Point(452, 75);
            this.txtCourse.Name = "txtCourse";
            this.txtCourse.Size = new System.Drawing.Size(258, 40);
            this.txtCourse.TabIndex = 4;
            // 
            // btnRegisterStudent
            // 
            this.btnRegisterStudent.Location = new System.Drawing.Point(525, 198);
            this.btnRegisterStudent.Name = "btnRegisterStudent";
            this.btnRegisterStudent.Size = new System.Drawing.Size(112, 34);
            this.btnRegisterStudent.TabIndex = 6;
            this.btnRegisterStudent.Text = "Register";
            this.btnRegisterStudent.Click += new System.EventHandler(this.btnRegisterStudent_Click);
            // 
            // gcStudent
            // 
            this.gcStudent.Location = new System.Drawing.Point(92, 261);
            this.gcStudent.MainView = this.gvStudent;
            this.gcStudent.Name = "gcStudent";
            this.gcStudent.Size = new System.Drawing.Size(899, 351);
            this.gcStudent.TabIndex = 7;
            this.gcStudent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStudent});
            // 
            // gvStudent
            // 
            this.gvStudent.GridControl = this.gcStudent;
            this.gvStudent.Name = "gvStudent";
            // 
            // RegisterStudentsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcStudent);
            this.Controls.Add(this.btnRegisterStudent);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtCourse);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtIdNumber);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtFullname);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "RegisterStudentsControl";
            this.Size = new System.Drawing.Size(1135, 669);
            this.Load += new System.EventHandler(this.RegisterStudentsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtFullname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCourse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStudent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStudent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtFullname;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtIdNumber;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtCourse;
        private DevExpress.XtraEditors.SimpleButton btnRegisterStudent;
        private DevExpress.XtraGrid.GridControl gcStudent;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStudent;
    }
}
