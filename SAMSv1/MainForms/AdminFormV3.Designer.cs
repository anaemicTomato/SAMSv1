namespace SAMSv1.MainForms
{
    partial class AdminFormV3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminFormV3));
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            this.btnLogout = new DevExpress.XtraEditors.SimpleButton();
            this.ManageUsersModule = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.RegisterStudentModule = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.AttendanceModule = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement8 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement5 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement6 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(275, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1423, 1058);
            this.mainPanel.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnLogout.Appearance.ForeColor = System.Drawing.Color.SeaGreen;
            this.btnLogout.Appearance.Options.UseFont = true;
            this.btnLogout.Appearance.Options.UseForeColor = true;
            this.btnLogout.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLogout.ImageOptions.SvgImage")));
            this.btnLogout.Location = new System.Drawing.Point(22, 969);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(217, 66);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // ManageUsersModule
            // 
            this.ManageUsersModule.Appearance.Default.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.ManageUsersModule.Appearance.Default.ForeColor = System.Drawing.Color.DarkGreen;
            this.ManageUsersModule.Appearance.Default.Options.UseFont = true;
            this.ManageUsersModule.Appearance.Default.Options.UseForeColor = true;
            this.ManageUsersModule.Appearance.Hovered.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ManageUsersModule.Appearance.Hovered.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManageUsersModule.Appearance.Hovered.ForeColor = System.Drawing.Color.SeaGreen;
            this.ManageUsersModule.Appearance.Hovered.Options.UseBackColor = true;
            this.ManageUsersModule.Appearance.Hovered.Options.UseFont = true;
            this.ManageUsersModule.Appearance.Hovered.Options.UseForeColor = true;
            this.ManageUsersModule.Height = 60;
            this.ManageUsersModule.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ManageUsersModule.ImageOptions.SvgImage")));
            this.ManageUsersModule.Name = "ManageUsersModule";
            this.ManageUsersModule.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ManageUsersModule.Text = "Manage Users";
            this.ManageUsersModule.Click += new System.EventHandler(this.ManageUsersModule_Click);
            // 
            // RegisterStudentModule
            // 
            this.RegisterStudentModule.Appearance.Default.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterStudentModule.Appearance.Default.ForeColor = System.Drawing.Color.DarkGreen;
            this.RegisterStudentModule.Appearance.Default.Options.UseFont = true;
            this.RegisterStudentModule.Appearance.Default.Options.UseForeColor = true;
            this.RegisterStudentModule.Appearance.Hovered.BackColor = System.Drawing.Color.MediumAquamarine;
            this.RegisterStudentModule.Appearance.Hovered.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterStudentModule.Appearance.Hovered.ForeColor = System.Drawing.Color.SeaGreen;
            this.RegisterStudentModule.Appearance.Hovered.Options.UseBackColor = true;
            this.RegisterStudentModule.Appearance.Hovered.Options.UseFont = true;
            this.RegisterStudentModule.Appearance.Hovered.Options.UseForeColor = true;
            this.RegisterStudentModule.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl, DevExpress.XtraBars.Navigation.HeaderElementAlignment.Left),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.RegisterStudentModule.Height = 60;
            this.RegisterStudentModule.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("RegisterStudentModule.ImageOptions.SvgImage")));
            this.RegisterStudentModule.Name = "RegisterStudentModule";
            this.RegisterStudentModule.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.RegisterStudentModule.Text = "Register Students";
            this.RegisterStudentModule.Click += new System.EventHandler(this.RegisterStudentModule_Click);
            // 
            // AttendanceModule
            // 
            this.AttendanceModule.Appearance.Default.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttendanceModule.Appearance.Default.ForeColor = System.Drawing.Color.DarkGreen;
            this.AttendanceModule.Appearance.Default.Options.UseFont = true;
            this.AttendanceModule.Appearance.Default.Options.UseForeColor = true;
            this.AttendanceModule.Appearance.Disabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AttendanceModule.Appearance.Disabled.Options.UseBackColor = true;
            this.AttendanceModule.Appearance.Hovered.BackColor = System.Drawing.Color.MediumAquamarine;
            this.AttendanceModule.Appearance.Hovered.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttendanceModule.Appearance.Hovered.ForeColor = System.Drawing.Color.SeaGreen;
            this.AttendanceModule.Appearance.Hovered.Options.UseBackColor = true;
            this.AttendanceModule.Appearance.Hovered.Options.UseFont = true;
            this.AttendanceModule.Appearance.Hovered.Options.UseForeColor = true;
            this.AttendanceModule.Appearance.Pressed.BackColor = System.Drawing.Color.SeaGreen;
            this.AttendanceModule.Appearance.Pressed.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttendanceModule.Appearance.Pressed.ForeColor = System.Drawing.Color.White;
            this.AttendanceModule.Appearance.Pressed.Options.UseBackColor = true;
            this.AttendanceModule.Appearance.Pressed.Options.UseFont = true;
            this.AttendanceModule.Appearance.Pressed.Options.UseForeColor = true;
            this.AttendanceModule.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl)});
            this.AttendanceModule.Height = 60;
            this.AttendanceModule.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("AttendanceModule.ImageOptions.SvgImage")));
            this.AttendanceModule.Name = "AttendanceModule";
            this.AttendanceModule.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.AttendanceModule.Text = "Attendance";
            this.AttendanceModule.Click += new System.EventHandler(this.AttendanceModule_Click);
            // 
            // accordionControl1
            // 
            this.accordionControl1.Appearance.AccordionControl.BackColor = System.Drawing.Color.White;
            this.accordionControl1.Appearance.AccordionControl.Options.UseBackColor = true;
            this.accordionControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement8,
            this.AttendanceModule,
            this.RegisterStudentModule,
            this.ManageUsersModule,
            this.accordionControlElement1});
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Margin = new System.Windows.Forms.Padding(4);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.Padding = new System.Windows.Forms.Padding(50, 150, 50, 50);
            this.accordionControl1.Size = new System.Drawing.Size(275, 1058);
            this.accordionControl1.TabIndex = 0;
            this.accordionControl1.Click += new System.EventHandler(this.accordionControl1_Click);
            // 
            // accordionControlElement8
            // 
            this.accordionControlElement8.Height = 160;
            this.accordionControlElement8.Name = "accordionControlElement8";
            this.accordionControlElement8.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement8.Text = "   ";
            this.accordionControlElement8.Click += new System.EventHandler(this.accordionControlElement8_Click);
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Appearance.Default.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlElement1.Appearance.Default.ForeColor = System.Drawing.Color.DarkGreen;
            this.accordionControlElement1.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement1.Appearance.Default.Options.UseForeColor = true;
            this.accordionControlElement1.Appearance.Hovered.BackColor = System.Drawing.Color.MediumAquamarine;
            this.accordionControlElement1.Appearance.Hovered.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlElement1.Appearance.Hovered.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.accordionControlElement1.Appearance.Hovered.Options.UseBackColor = true;
            this.accordionControlElement1.Appearance.Hovered.Options.UseFont = true;
            this.accordionControlElement1.Appearance.Hovered.Options.UseForeColor = true;
            this.accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement5,
            this.accordionControlElement6});
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons, DevExpress.XtraBars.Navigation.HeaderElementAlignment.Left),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl)});
            this.accordionControlElement1.Height = 60;
            this.accordionControlElement1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlElement1.ImageOptions.SvgImage")));
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Attendance Log";
            this.accordionControlElement1.Click += new System.EventHandler(this.accordionControlElement1_Click_1);
            // 
            // accordionControlElement5
            // 
            this.accordionControlElement5.Appearance.Default.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.accordionControlElement5.Appearance.Default.Options.UseForeColor = true;
            this.accordionControlElement5.Name = "accordionControlElement5";
            this.accordionControlElement5.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement5.Text = "View Attendance";
            // 
            // accordionControlElement6
            // 
            this.accordionControlElement6.Appearance.Default.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.accordionControlElement6.Appearance.Default.Options.UseForeColor = true;
            this.accordionControlElement6.Name = "accordionControlElement6";
            this.accordionControlElement6.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement6.Text = "Generate Report";
            // 
            // accordionControlElement2
            // 
            this.accordionControlElement2.Expanded = true;
            this.accordionControlElement2.Name = "accordionControlElement2";
            this.accordionControlElement2.Text = "Element2";
            // 
            // accordionControlElement4
            // 
            this.accordionControlElement4.Name = "accordionControlElement4";
            this.accordionControlElement4.Text = "Element4";
            // 
            // accordionControlElement3
            // 
            this.accordionControlElement3.Name = "accordionControlElement3";
            this.accordionControlElement3.Text = "Element3";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(12, 875);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Properties.ZoomPercent = 8D;
            this.pictureEdit1.Size = new System.Drawing.Size(227, 89);
            this.pictureEdit1.TabIndex = 3;
            this.pictureEdit1.EditValueChanged += new System.EventHandler(this.pictureEdit1_EditValueChanged);
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(12, 12);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit2.Properties.ZoomPercent = 9D;
            this.pictureEdit2.Size = new System.Drawing.Size(240, 194);
            this.pictureEdit2.TabIndex = 4;
            // 
            // AdminFormV3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1698, 1058);
            this.Controls.Add(this.pictureEdit2);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.accordionControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1700, 1018);
            this.Name = "AdminFormV3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminFormV3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminFormV3_FormClosing);
            this.Load += new System.EventHandler(this.AdminFormV3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl mainPanel;
        private DevExpress.XtraEditors.SimpleButton btnLogout;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ManageUsersModule;
        private DevExpress.XtraBars.Navigation.AccordionControlElement RegisterStudentModule;
        private DevExpress.XtraBars.Navigation.AccordionControlElement AttendanceModule;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement3;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement5;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement6;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement8;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
    }
}