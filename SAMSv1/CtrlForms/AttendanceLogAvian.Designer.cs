namespace SAMSv1.CtrlForms
{
    partial class AttendanceLogAvian
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
            this.scSearchStudent = new DevExpress.XtraEditors.SearchControl();
            this.cbEvent = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cbCourse = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbYearLevel = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ccCalendar = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.gcStudentTable = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnGenerateReport = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnSetStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetEnd = new DevExpress.XtraEditors.SimpleButton();
            this.lblDateRange = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.scSearchStudent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEvent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCourse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYearLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccCalendar.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStudentTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // scSearchStudent
            // 
            this.scSearchStudent.Location = new System.Drawing.Point(39, 79);
            this.scSearchStudent.Name = "scSearchStudent";
            this.scSearchStudent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.scSearchStudent.Size = new System.Drawing.Size(258, 40);
            this.scSearchStudent.TabIndex = 0;
            // 
            // cbEvent
            // 
            this.cbEvent.Location = new System.Drawing.Point(39, 210);
            this.cbEvent.Name = "cbEvent";
            this.cbEvent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbEvent.Size = new System.Drawing.Size(258, 40);
            this.cbEvent.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(39, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(106, 19);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Search Student";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(39, 185);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(101, 19);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Filter by Event";
            // 
            // cbCourse
            // 
            this.cbCourse.Location = new System.Drawing.Point(341, 79);
            this.cbCourse.Name = "cbCourse";
            this.cbCourse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbCourse.Size = new System.Drawing.Size(245, 40);
            this.cbCourse.TabIndex = 4;
            // 
            // cbYearLevel
            // 
            this.cbYearLevel.Location = new System.Drawing.Point(341, 210);
            this.cbYearLevel.Name = "cbYearLevel";
            this.cbYearLevel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbYearLevel.Size = new System.Drawing.Size(245, 40);
            this.cbYearLevel.TabIndex = 5;
            // 
            // ccCalendar
            // 
            this.ccCalendar.AutoSize = false;
            this.ccCalendar.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccCalendar.Location = new System.Drawing.Point(649, 68);
            this.ccCalendar.Name = "ccCalendar";
            this.ccCalendar.Size = new System.Drawing.Size(359, 338);
            this.ccCalendar.TabIndex = 6;
            // 
            // gcStudentTable
            // 
            this.gcStudentTable.Location = new System.Drawing.Point(39, 320);
            this.gcStudentTable.MainView = this.gridView1;
            this.gcStudentTable.Name = "gcStudentTable";
            this.gcStudentTable.Size = new System.Drawing.Size(520, 459);
            this.gcStudentTable.TabIndex = 7;
            this.gcStudentTable.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.gcStudentTable;
            this.gridView1.Name = "gridView1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Student Name";
            this.gridColumn1.FieldName = "FullName";
            this.gridColumn1.MinWidth = 30;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 112;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Course";
            this.gridColumn2.FieldName = "Course";
            this.gridColumn2.MinWidth = 30;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 112;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(769, 529);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(162, 49);
            this.btnGenerateReport.TabIndex = 8;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(341, 185);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(129, 19);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Sort by Year Level";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(341, 54);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(105, 19);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Sort by Course";
            // 
            // btnSetStart
            // 
            this.btnSetStart.Location = new System.Drawing.Point(649, 456);
            this.btnSetStart.Name = "btnSetStart";
            this.btnSetStart.Size = new System.Drawing.Size(170, 35);
            this.btnSetStart.TabIndex = 12;
            this.btnSetStart.Text = "Set Start Date";
            // 
            // btnSetEnd
            // 
            this.btnSetEnd.Location = new System.Drawing.Point(838, 456);
            this.btnSetEnd.Name = "btnSetEnd";
            this.btnSetEnd.Size = new System.Drawing.Size(170, 35);
            this.btnSetEnd.TabIndex = 13;
            this.btnSetEnd.Text = "Set End Date";
            // 
            // lblDateRange
            // 
            this.lblDateRange.Location = new System.Drawing.Point(649, 415);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(246, 19);
            this.lblDateRange.TabIndex = 11;
            this.lblDateRange.Text = "Click a start date, then an end date";
            // 
            // AttendanceLogAvian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSetStart);
            this.Controls.Add(this.btnSetEnd);
            this.Controls.Add(this.lblDateRange);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.gcStudentTable);
            this.Controls.Add(this.ccCalendar);
            this.Controls.Add(this.cbYearLevel);
            this.Controls.Add(this.cbCourse);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cbEvent);
            this.Controls.Add(this.scSearchStudent);
            this.Name = "AttendanceLogAvian";
            this.Size = new System.Drawing.Size(1100, 782);
            ((System.ComponentModel.ISupportInitialize)(this.scSearchStudent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEvent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCourse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYearLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccCalendar.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStudentTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SearchControl scSearchStudent;
        private DevExpress.XtraEditors.ComboBoxEdit cbEvent;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cbCourse;
        private DevExpress.XtraEditors.ComboBoxEdit cbYearLevel;
        private DevExpress.XtraEditors.Controls.CalendarControl ccCalendar;
        private DevExpress.XtraGrid.GridControl gcStudentTable;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnGenerateReport;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.LabelControl lblDateRange;
        private DevExpress.XtraEditors.SimpleButton btnSetStart;
        private DevExpress.XtraEditors.SimpleButton btnSetEnd;
    }
}
