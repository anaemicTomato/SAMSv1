namespace SAMSv1.Login
{
    partial class LoginForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panelBackground = new DevExpress.XtraEditors.PanelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.labelAdminLogin = new DevExpress.XtraEditors.LabelControl();
            this.tePassword = new DevExpress.XtraEditors.TextEdit();
            this.teStudentID = new DevExpress.XtraEditors.TextEdit();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelBackground)).BeginInit();
            this.panelBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teStudentID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBackground
            // 
            this.panelBackground.Appearance.BackColor = System.Drawing.Color.White;
            this.panelBackground.Appearance.Options.UseBackColor = true;
            this.panelBackground.Controls.Add(this.simpleButton1);
            this.panelBackground.Controls.Add(this.btnLogin);
            this.panelBackground.Controls.Add(this.labelAdminLogin);
            this.panelBackground.Controls.Add(this.tePassword);
            this.panelBackground.Controls.Add(this.teStudentID);
            this.panelBackground.Location = new System.Drawing.Point(225, 78);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(379, 343);
            this.panelBackground.TabIndex = 0;
            this.panelBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelControl1_Paint);
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLogin.Appearance.BorderColor = System.Drawing.Color.DarkGreen;
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Appearance.Options.UseBackColor = true;
            this.btnLogin.Appearance.Options.UseBorderColor = true;
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Appearance.Options.UseForeColor = true;
            this.btnLogin.AppearanceHovered.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLogin.AppearanceHovered.BorderColor = System.Drawing.Color.DarkGreen;
            this.btnLogin.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnLogin.AppearanceHovered.Options.UseBackColor = true;
            this.btnLogin.AppearanceHovered.Options.UseBorderColor = true;
            this.btnLogin.AppearanceHovered.Options.UseForeColor = true;
            this.btnLogin.Location = new System.Drawing.Point(144, 258);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(94, 29);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // labelAdminLogin
            // 
            this.labelAdminLogin.Appearance.Font = new System.Drawing.Font("Bernard MT Condensed", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdminLogin.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelAdminLogin.Appearance.Options.UseFont = true;
            this.labelAdminLogin.Appearance.Options.UseForeColor = true;
            this.labelAdminLogin.Location = new System.Drawing.Point(76, 63);
            this.labelAdminLogin.Name = "labelAdminLogin";
            this.labelAdminLogin.Size = new System.Drawing.Size(238, 55);
            this.labelAdminLogin.TabIndex = 1;
            this.labelAdminLogin.Text = "ADMIN LOGIN";
            this.labelAdminLogin.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // tePassword
            // 
            this.tePassword.EditValue = "Password";
            this.tePassword.Location = new System.Drawing.Point(107, 203);
            this.tePassword.Name = "tePassword";
            this.tePassword.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.tePassword.Properties.Appearance.Options.UseForeColor = true;
            this.tePassword.Properties.UseSystemPasswordChar = true;
            this.tePassword.Size = new System.Drawing.Size(175, 34);
            this.tePassword.TabIndex = 3;
            this.tePassword.EditValueChanged += new System.EventHandler(this.tePassword_EditValueChanged);
            // 
            // teStudentID
            // 
            this.behaviorManager1.SetBehaviors(this.teStudentID, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.Behaviors.Common.FileIconBehavior.Create(typeof(DevExpress.XtraEditors.Behaviors.FileIconBehaviorSourceForTextEdit), DevExpress.Utils.Behaviors.Common.FileIconSize.Small, null, null)))});
            this.teStudentID.EditValue = "Username";
            this.teStudentID.Location = new System.Drawing.Point(107, 150);
            this.teStudentID.Name = "teStudentID";
            this.teStudentID.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.teStudentID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teStudentID.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.teStudentID.Properties.Appearance.Options.UseBackColor = true;
            this.teStudentID.Properties.Appearance.Options.UseFont = true;
            this.teStudentID.Properties.Appearance.Options.UseForeColor = true;
            this.teStudentID.Size = new System.Drawing.Size(175, 34);
            this.teStudentID.TabIndex = 2;
            this.teStudentID.EditValueChanged += new System.EventHandler(this.teStudentID_EditValueChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(21, 243);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(94, 29);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(832, 500);
            this.Controls.Add(this.panelBackground);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)(this.panelBackground)).EndInit();
            this.panelBackground.ResumeLayout(false);
            this.panelBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teStudentID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelBackground;
        private DevExpress.XtraEditors.LabelControl labelAdminLogin;
        private DevExpress.XtraEditors.TextEdit teStudentID;
        private DevExpress.XtraEditors.TextEdit tePassword;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}