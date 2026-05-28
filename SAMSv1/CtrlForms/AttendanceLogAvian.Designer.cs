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
            this.cbSemester = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbSession = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbAttendanceType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.scSearchStudent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEvent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCourse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYearLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccCalendar.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStudentTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSemester.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSession.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAttendanceType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // scSearchStudent
            // 
            this.scSearchStudent.Location = new System.Drawing.Point(188, 350);
            this.scSearchStudent.Name = "scSearchStudent";
            this.scSearchStudent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.scSearchStudent.Size = new System.Drawing.Size(258, 40);
            this.scSearchStudent.TabIndex = 0;
            // 
            // cbEvent
            // 
            this.cbEvent.Location = new System.Drawing.Point(39, 79);
            this.cbEvent.Name = "cbEvent";
            this.cbEvent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbEvent.Size = new System.Drawing.Size(258, 40);
            this.cbEvent.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(58, 360);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(106, 19);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Search Student";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(39, 54);
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
            this.cbYearLevel.EditValue = "All";
            this.cbYearLevel.Location = new System.Drawing.Point(341, 168);
            this.cbYearLevel.Name = "cbYearLevel";
            this.cbYearLevel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbYearLevel.Properties.Items.AddRange(new object[] {
            "All",
            "1st Year",
            "2nd Year",
            "3rd Year",
            "4th Year"});
            this.cbYearLevel.Size = new System.Drawing.Size(245, 40);
            this.cbYearLevel.TabIndex = 5;
            // 
            // ccCalendar
            // 
            this.ccCalendar.AutoSize = false;
            this.ccCalendar.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccCalendar.HighlightTodayCell = DevExpress.Utils.DefaultBoolean.False;
            this.ccCalendar.Location = new System.Drawing.Point(822, 20);
            this.ccCalendar.Name = "ccCalendar";
            this.ccCalendar.Size = new System.Drawing.Size(359, 408);
            this.ccCalendar.TabIndex = 6;
            // 
            // gcStudentTable
            // 
            this.gcStudentTable.Location = new System.Drawing.Point(39, 396);
            this.gcStudentTable.MainView = this.gridView1;
            this.gcStudentTable.Name = "gcStudentTable";
            this.gcStudentTable.Size = new System.Drawing.Size(707, 459);
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
            this.btnGenerateReport.Location = new System.Drawing.Point(896, 602);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(162, 49);
            this.btnGenerateReport.TabIndex = 8;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(341, 143);
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
            this.btnSetStart.Location = new System.Drawing.Point(797, 495);
            this.btnSetStart.Name = "btnSetStart";
            this.btnSetStart.Size = new System.Drawing.Size(170, 35);
            this.btnSetStart.TabIndex = 12;
            this.btnSetStart.Text = "Set Start Date";
            this.btnSetStart.Click += new System.EventHandler(this.btnSetStart_Click);
            // 
            // btnSetEnd
            // 
            this.btnSetEnd.Location = new System.Drawing.Point(1011, 495);
            this.btnSetEnd.Name = "btnSetEnd";
            this.btnSetEnd.Size = new System.Drawing.Size(170, 35);
            this.btnSetEnd.TabIndex = 13;
            this.btnSetEnd.Text = "Set End Date";
            this.btnSetEnd.Click += new System.EventHandler(this.btnSetEnd_Click);
            // 
            // lblDateRange
            // 
            this.lblDateRange.Location = new System.Drawing.Point(822, 453);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(246, 19);
            this.lblDateRange.TabIndex = 11;
            this.lblDateRange.Text = "Click a start date, then an end date";
            this.lblDateRange.Click += new System.EventHandler(this.lblDateRange_Click);
            // 
            // cbSemester
            // 
            this.cbSemester.EditValue = "All";
            this.cbSemester.Location = new System.Drawing.Point(39, 267);
            this.cbSemester.Name = "cbSemester";
            this.cbSemester.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSemester.Properties.Items.AddRange(new object[] {
            "All",
            "1st",
            "2nd"});
            this.cbSemester.Size = new System.Drawing.Size(245, 40);
            this.cbSemester.TabIndex = 15;
            // 
            // cbSession
            // 
            this.cbSession.EditValue = "All";
            this.cbSession.Location = new System.Drawing.Point(39, 168);
            this.cbSession.Name = "cbSession";
            this.cbSession.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSession.Properties.Items.AddRange(new object[] {
            "All",
            "Morning",
            "Afternoon",
            "Evening"});
            this.cbSession.Size = new System.Drawing.Size(258, 40);
            this.cbSession.TabIndex = 14;
            // 
            // cbAttendanceType
            // 
            this.cbAttendanceType.EditValue = "Both";
            this.cbAttendanceType.Location = new System.Drawing.Point(341, 267);
            this.cbAttendanceType.Name = "cbAttendanceType";
            this.cbAttendanceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbAttendanceType.Properties.Items.AddRange(new object[] {
            "Both",
            "Time-In",
            "Time-Out"});
            this.cbAttendanceType.Size = new System.Drawing.Size(245, 40);
            this.cbAttendanceType.TabIndex = 16;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(39, 143);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(53, 19);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "Session";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(39, 242);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(65, 19);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "Semester";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(341, 242);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(119, 19);
            this.labelControl7.TabIndex = 19;
            this.labelControl7.Text = "Attendance Type";
            // 
            // AttendanceLogAvian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.cbAttendanceType);
            this.Controls.Add(this.cbSemester);
            this.Controls.Add(this.cbSession);
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
            this.Size = new System.Drawing.Size(1307, 782);
            ((System.ComponentModel.ISupportInitialize)(this.scSearchStudent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEvent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCourse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYearLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccCalendar.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStudentTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSemester.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSession.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAttendanceType.Properties)).EndInit();
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
        private DevExpress.XtraEditors.ComboBoxEdit cbSemester;
        private DevExpress.XtraEditors.ComboBoxEdit cbSession;
        private DevExpress.XtraEditors.ComboBoxEdit cbAttendanceType;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
    }
}
