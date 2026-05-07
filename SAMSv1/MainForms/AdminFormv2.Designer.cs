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
            this.basePanel = new DevExpress.XtraEditors.PanelControl();
            this.topPanel = new DevExpress.XtraEditors.PanelControl();
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.btnAttendance = new DevExpress.XtraEditors.SimpleButton();
            this.btnRegisterStudent = new DevExpress.XtraEditors.SimpleButton();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.basePanel)).BeginInit();
            this.basePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topPanel)).BeginInit();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // basePanel
            // 
            this.basePanel.Controls.Add(this.mainPanel);
            this.basePanel.Controls.Add(this.topPanel);
            this.basePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basePanel.Location = new System.Drawing.Point(0, 0);
            this.basePanel.Name = "basePanel";
            this.basePanel.Padding = new System.Windows.Forms.Padding(8);
            this.basePanel.Size = new System.Drawing.Size(1072, 644);
            this.basePanel.TabIndex = 0;
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.tablePanel1);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(10, 10);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1052, 141);
            this.topPanel.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(10, 151);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1052, 483);
            this.mainPanel.TabIndex = 1;
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 34.59F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 38.83F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 36.58F)});
            this.tablePanel1.Controls.Add(this.btnReport);
            this.tablePanel1.Controls.Add(this.btnRegisterStudent);
            this.tablePanel1.Controls.Add(this.btnAttendance);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(2, 2);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(1048, 137);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // btnAttendance
            // 
            this.tablePanel1.SetColumn(this.btnAttendance, 0);
            this.btnAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAttendance.Location = new System.Drawing.Point(24, 23);
            this.btnAttendance.Name = "btnAttendance";
            this.tablePanel1.SetRow(this.btnAttendance, 0);
            this.btnAttendance.Size = new System.Drawing.Size(309, 90);
            this.btnAttendance.TabIndex = 0;
            this.btnAttendance.Text = "Attendance";
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // btnRegisterStudent
            // 
            this.tablePanel1.SetColumn(this.btnRegisterStudent, 1);
            this.btnRegisterStudent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRegisterStudent.Location = new System.Drawing.Point(341, 23);
            this.btnRegisterStudent.Name = "btnRegisterStudent";
            this.tablePanel1.SetRow(this.btnRegisterStudent, 0);
            this.btnRegisterStudent.Size = new System.Drawing.Size(348, 90);
            this.btnRegisterStudent.TabIndex = 1;
            this.btnRegisterStudent.Text = "Register Student";
            this.btnRegisterStudent.Click += new System.EventHandler(this.btnRegisterStudent_Click);
            // 
            // btnReport
            // 
            this.tablePanel1.SetColumn(this.btnReport, 2);
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReport.Location = new System.Drawing.Point(697, 23);
            this.btnReport.Name = "btnReport";
            this.tablePanel1.SetRow(this.btnReport, 0);
            this.btnReport.Size = new System.Drawing.Size(327, 90);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Reports";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // AdminFormv2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 644);
            this.Controls.Add(this.basePanel);
            this.Name = "AdminFormv2";
            this.Text = "AdminFormv2";
            ((System.ComponentModel.ISupportInitialize)(this.basePanel)).EndInit();
            this.basePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topPanel)).EndInit();
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
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