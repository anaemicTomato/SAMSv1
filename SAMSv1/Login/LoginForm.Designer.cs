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
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelBackground)).BeginInit();
            this.panelBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBackground
            // 
            this.panelBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBackground.Appearance.BackColor = System.Drawing.Color.White;
            this.panelBackground.Appearance.Options.UseBackColor = true;
            this.panelBackground.Controls.Add(this.btnLogin);
            this.panelBackground.Controls.Add(this.labelAdminLogin);
            this.panelBackground.Controls.Add(this.txtPassword);
            this.panelBackground.Controls.Add(this.txtUsername);
            this.panelBackground.Location = new System.Drawing.Point(225, 78);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(425, 392);
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
            this.btnLogin.Location = new System.Drawing.Point(167, 284);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(94, 29);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // labelAdminLogin
            // 
            this.labelAdminLogin.Appearance.Font = new System.Drawing.Font("Arial Narrow", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdminLogin.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelAdminLogin.Appearance.Options.UseFont = true;
            this.labelAdminLogin.Appearance.Options.UseForeColor = true;
            this.labelAdminLogin.Location = new System.Drawing.Point(141, 80);
            this.labelAdminLogin.Name = "labelAdminLogin";
            this.labelAdminLogin.Size = new System.Drawing.Size(148, 67);
            this.labelAdminLogin.TabIndex = 1;
            this.labelAdminLogin.Text = "LOGIN";
            this.labelAdminLogin.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new System.Drawing.Point(130, 229);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.Size = new System.Drawing.Size(175, 34);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.EditValueChanged += new System.EventHandler(this.tePassword_EditValueChanged);
            // 
            // txtUsername
            // 
            this.behaviorManager1.SetBehaviors(this.txtUsername, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.Behaviors.Common.FileIconBehavior.Create(typeof(DevExpress.XtraEditors.Behaviors.FileIconBehaviorSourceForTextEdit), DevExpress.Utils.Behaviors.Common.FileIconSize.Small, null, null)))});
            this.txtUsername.EditValue = "";
            this.txtUsername.Location = new System.Drawing.Point(130, 176);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.txtUsername.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtUsername.Properties.Appearance.Options.UseBackColor = true;
            this.txtUsername.Properties.Appearance.Options.UseFont = true;
            this.txtUsername.Properties.Appearance.Options.UseForeColor = true;
            this.txtUsername.Size = new System.Drawing.Size(175, 34);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.EditValueChanged += new System.EventHandler(this.teStudentID_EditValueChanged);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(878, 549);
            this.Controls.Add(this.panelBackground);
            this.MaximumSize = new System.Drawing.Size(880, 591);
            this.MinimumSize = new System.Drawing.Size(880, 591);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)(this.panelBackground)).EndInit();
            this.panelBackground.ResumeLayout(false);
            this.panelBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelBackground;
        private DevExpress.XtraEditors.LabelControl labelAdminLogin;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
    }
}