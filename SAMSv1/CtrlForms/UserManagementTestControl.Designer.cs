namespace SAMSv1.CtrlForms
{
    partial class UserManagementTestControl
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
            this.gcUsers = new DevExpress.XtraGrid.GridControl();
            this.gvUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cbRole = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnAddUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdateUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearFields = new DevExpress.XtraEditors.SimpleButton();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sbUserList = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbUserList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcUsers
            // 
            this.gcUsers.Location = new System.Drawing.Point(39, 161);
            this.gcUsers.MainView = this.gvUsers;
            this.gcUsers.Name = "gcUsers";
            this.gcUsers.Size = new System.Drawing.Size(445, 421);
            this.gcUsers.TabIndex = 0;
            this.gcUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUsers});
            // 
            // gvUsers
            // 
            this.gvUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gvUsers.GridControl = this.gcUsers;
            this.gvUsers.Name = "gvUsers";
            this.gvUsers.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvUsers_FocusedRowChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(755, 151);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(150, 40);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(755, 246);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(150, 40);
            this.txtPassword.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(755, 124);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 19);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Username:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(755, 325);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 19);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Role:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(753, 224);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(73, 19);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Password:";
            // 
            // cbRole
            // 
            this.cbRole.Location = new System.Drawing.Point(755, 350);
            this.cbRole.Name = "cbRole";
            this.cbRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbRole.Properties.Items.AddRange(new object[] {
            "Admin",
            "SSG Officer"});
            this.cbRole.Size = new System.Drawing.Size(150, 40);
            this.cbRole.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(188, 47);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(77, 19);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "USER LIST";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(777, 47);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(108, 19);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "USER DETAILS";
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(626, 461);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(112, 34);
            this.btnAddUser.TabIndex = 9;
            this.btnAddUser.Text = "Add";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.Location = new System.Drawing.Point(773, 461);
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Size = new System.Drawing.Size(112, 34);
            this.btnUpdateUser.TabIndex = 10;
            this.btnUpdateUser.Text = "Update";
            this.btnUpdateUser.Click += new System.EventHandler(this.btnUpdateUser_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(904, 461);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(112, 34);
            this.btnDeleteUser.TabIndex = 11;
            this.btnDeleteUser.Text = "Delete";
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnClearFields
            // 
            this.btnClearFields.Location = new System.Drawing.Point(801, 524);
            this.btnClearFields.Name = "btnClearFields";
            this.btnClearFields.Size = new System.Drawing.Size(112, 34);
            this.btnClearFields.TabIndex = 12;
            this.btnClearFields.Text = "Clear";
            this.btnClearFields.Click += new System.EventHandler(this.btnClearFields_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "UserID";
            this.gridColumn1.MinWidth = 30;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 112;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Username";
            this.gridColumn2.FieldName = "Username";
            this.gridColumn2.MinWidth = 30;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 112;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Role";
            this.gridColumn3.FieldName = "Role";
            this.gridColumn3.MinWidth = 30;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 112;
            // 
            // sbUserList
            // 
            this.sbUserList.Location = new System.Drawing.Point(39, 112);
            this.sbUserList.Name = "sbUserList";
            this.sbUserList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.sbUserList.Size = new System.Drawing.Size(192, 40);
            this.sbUserList.TabIndex = 13;
            // 
            // UserManagementTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sbUserList);
            this.Controls.Add(this.btnClearFields);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.btnUpdateUser);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.cbRole);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.gcUsers);
            this.Name = "UserManagementTestControl";
            this.Size = new System.Drawing.Size(1076, 638);
            ((System.ComponentModel.ISupportInitialize)(this.gcUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbUserList.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUsers;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cbRole;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnAddUser;
        private DevExpress.XtraEditors.SimpleButton btnUpdateUser;
        private DevExpress.XtraEditors.SimpleButton btnDeleteUser;
        private DevExpress.XtraEditors.SimpleButton btnClearFields;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SearchControl sbUserList;
    }
}
