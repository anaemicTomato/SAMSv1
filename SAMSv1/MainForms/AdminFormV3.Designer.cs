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
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.AttendanceModule = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.RegisterStudentModule = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ReportModule = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement5 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.AttendanceModule,
            this.RegisterStudentModule,
            this.ReportModule});
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.Size = new System.Drawing.Size(275, 744);
            this.accordionControl1.TabIndex = 0;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // AttendanceModule
            // 
            this.AttendanceModule.Appearance.Default.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttendanceModule.Appearance.Default.Options.UseFont = true;
            this.AttendanceModule.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("AttendanceModule.ImageOptions.SvgImage")));
            this.AttendanceModule.Name = "AttendanceModule";
            this.AttendanceModule.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.AttendanceModule.Text = "Attedance";
            this.AttendanceModule.Click += new System.EventHandler(this.AttendanceModule_Click);
            // 
            // RegisterStudentModule
            // 
            this.RegisterStudentModule.Appearance.Default.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterStudentModule.Appearance.Default.Options.UseFont = true;
            this.RegisterStudentModule.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("RegisterStudentModule.ImageOptions.SvgImage")));
            this.RegisterStudentModule.Name = "RegisterStudentModule";
            this.RegisterStudentModule.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.RegisterStudentModule.Text = "Register Students";
            this.RegisterStudentModule.Click += new System.EventHandler(this.RegisterStudentModule_Click);
            // 
            // ReportModule
            // 
            this.ReportModule.Appearance.Default.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportModule.Appearance.Default.Options.UseFont = true;
            this.ReportModule.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement4,
            this.accordionControlElement5});
            this.ReportModule.Expanded = true;
            this.ReportModule.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text, DevExpress.XtraBars.Navigation.HeaderElementAlignment.Right)});
            this.ReportModule.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ReportModule.ImageOptions.SvgImage")));
            this.ReportModule.Name = "ReportModule";
            this.ReportModule.Text = "Report";
            // 
            // accordionControlElement4
            // 
            this.accordionControlElement4.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlElement4.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement4.Name = "accordionControlElement4";
            this.accordionControlElement4.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement4.Text = "Attedance Log";
            // 
            // accordionControlElement5
            // 
            this.accordionControlElement5.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlElement5.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement5.Name = "accordionControlElement5";
            this.accordionControlElement5.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement5.Text = "Event Log";
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(275, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(925, 744);
            this.mainPanel.TabIndex = 1;
            // 
            // AdminFormV3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 744);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.accordionControl1);
            this.MinimumSize = new System.Drawing.Size(720, 600);
            this.Name = "AdminFormV3";
            this.Text = "AdminFormV3";
            this.Load += new System.EventHandler(this.AdminFormV3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraEditors.PanelControl mainPanel;
        private DevExpress.XtraBars.Navigation.AccordionControlElement AttendanceModule;
        private DevExpress.XtraBars.Navigation.AccordionControlElement RegisterStudentModule;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ReportModule;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement5;
    }
}