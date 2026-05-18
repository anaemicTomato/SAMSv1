namespace SAMSv1.MainForms
{
    partial class AdminFormv2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminFormv2));
            this.basePanel = new DevExpress.XtraEditors.PanelControl();
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            this.topPanel = new DevExpress.XtraEditors.PanelControl();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.btnRegisterStudent = new DevExpress.XtraEditors.SimpleButton();
            this.btnAttendance = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.basePanel)).BeginInit();
            this.basePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topPanel)).BeginInit();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // basePanel
            // 
            this.basePanel.ContentImage = ((System.Drawing.Image)(resources.GetObject("basePanel.ContentImage")));
            this.basePanel.Controls.Add(this.mainPanel);
            this.basePanel.Controls.Add(this.topPanel);
            this.basePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basePanel.Location = new System.Drawing.Point(0, 0);
            this.basePanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.basePanel.Name = "basePanel";
            this.basePanel.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.basePanel.Size = new System.Drawing.Size(843, 587);
            this.basePanel.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.mainPanel.ContentImage = ((System.Drawing.Image)(resources.GetObject("mainPanel.ContentImage")));
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(8, 128);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(827, 450);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.tablePanel1);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(8, 9);
            this.topPanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(827, 119);
            this.topPanel.TabIndex = 0;
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 34.59F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 38.83F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 36.58F)});
            this.tablePanel1.ContentImage = ((System.Drawing.Image)(resources.GetObject("tablePanel1.ContentImage")));
            this.tablePanel1.Controls.Add(this.btnReport);
            this.tablePanel1.Controls.Add(this.btnRegisterStudent);
            this.tablePanel1.Controls.Add(this.btnAttendance);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(2, 2);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(823, 115);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.AppearanceHovered.BackColor = System.Drawing.Color.SeaGreen;
            this.btnReport.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnReport.AppearanceHovered.Options.UseBackColor = true;
            this.btnReport.AppearanceHovered.Options.UseFont = true;
            this.btnReport.AppearanceHovered.Options.UseForeColor = true;
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReport.Location = new System.Drawing.Point(540, 19);
            this.btnReport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(255, 76);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Reports";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnRegisterStudent
            // 
            this.btnRegisterStudent.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegisterStudent.Appearance.Options.UseFont = true;
            this.btnRegisterStudent.AppearanceHovered.BackColor = System.Drawing.Color.SeaGreen;
            this.btnRegisterStudent.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegisterStudent.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnRegisterStudent.AppearanceHovered.Options.UseBackColor = true;
            this.btnRegisterStudent.AppearanceHovered.Options.UseFont = true;
            this.btnRegisterStudent.AppearanceHovered.Options.UseForeColor = true;
            this.btnRegisterStudent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRegisterStudent.Location = new System.Drawing.Point(264, 19);
            this.btnRegisterStudent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRegisterStudent.Name = "btnRegisterStudent";
            this.btnRegisterStudent.Size = new System.Drawing.Size(271, 76);
            this.btnRegisterStudent.TabIndex = 1;
            this.btnRegisterStudent.Text = "Register Student";
            this.btnRegisterStudent.Click += new System.EventHandler(this.btnRegisterStudent_Click);
            // 
            // btnAttendance
            // 
            this.btnAttendance.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAttendance.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnAttendance.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendance.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnAttendance.Appearance.Options.UseBackColor = true;
            this.btnAttendance.Appearance.Options.UseBorderColor = true;
            this.btnAttendance.Appearance.Options.UseFont = true;
            this.btnAttendance.Appearance.Options.UseForeColor = true;
            this.btnAttendance.AppearanceDisabled.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAttendance.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendance.AppearanceDisabled.ForeColor = System.Drawing.Color.White;
            this.btnAttendance.AppearanceDisabled.Options.UseBackColor = true;
            this.btnAttendance.AppearanceDisabled.Options.UseFont = true;
            this.btnAttendance.AppearanceDisabled.Options.UseForeColor = true;
            this.btnAttendance.AppearanceHovered.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAttendance.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendance.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnAttendance.AppearanceHovered.Options.UseBackColor = true;
            this.btnAttendance.AppearanceHovered.Options.UseFont = true;
            this.btnAttendance.AppearanceHovered.Options.UseForeColor = true;
            this.btnAttendance.AppearancePressed.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAttendance.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendance.AppearancePressed.ForeColor = System.Drawing.Color.White;
            this.btnAttendance.AppearancePressed.Options.UseBackColor = true;
            this.btnAttendance.AppearancePressed.Options.UseFont = true;
            this.btnAttendance.AppearancePressed.Options.UseForeColor = true;
            this.btnAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAttendance.Location = new System.Drawing.Point(19, 19);
            this.btnAttendance.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.Size = new System.Drawing.Size(241, 76);
            this.btnAttendance.TabIndex = 0;
            this.btnAttendance.Text = "Attendance";
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // AdminFormv2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 587);
            this.Controls.Add(this.basePanel);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "AdminFormv2";
            this.Text = "AdminFormv2";
            ((System.ComponentModel.ISupportInitialize)(this.basePanel)).EndInit();
            this.basePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topPanel)).EndInit();
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl basePanel;
        private DevExpress.XtraEditors.PanelControl mainPanel;
        private DevExpress.XtraEditors.PanelControl topPanel;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.SimpleButton btnRegisterStudent;
        private DevExpress.XtraEditors.SimpleButton btnAttendance;
    }
}