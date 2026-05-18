namespace SAMSv1.CtrlForms.Scan
{
    partial class ScanRFID
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
            this.btnScanFace = new DevExpress.XtraEditors.SimpleButton();
            this.btnRFID = new DevExpress.XtraEditors.SimpleButton();
            this.panelbottomRFID = new DevExpress.XtraEditors.PanelControl();
            this.paneltopRFID = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelbottomRFID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneltopRFID)).BeginInit();
            this.paneltopRFID.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScanFace
            // 
            this.btnScanFace.Location = new System.Drawing.Point(146, 27);
            this.btnScanFace.Name = "btnScanFace";
            this.btnScanFace.Size = new System.Drawing.Size(120, 52);
            this.btnScanFace.TabIndex = 7;
            this.btnScanFace.Text = "SCAN FACE";
            this.btnScanFace.Click += new System.EventHandler(this.btnScanFace_Click);
            // 
            // btnRFID
            // 
            this.btnRFID.Location = new System.Drawing.Point(20, 27);
            this.btnRFID.Name = "btnRFID";
            this.btnRFID.Size = new System.Drawing.Size(120, 52);
            this.btnRFID.TabIndex = 6;
            this.btnRFID.Text = "SCAN RFID";
            this.btnRFID.Click += new System.EventHandler(this.btnRFID_Click);
            // 
            // panelbottomRFID
            // 
            this.panelbottomRFID.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelbottomRFID.Location = new System.Drawing.Point(0, 136);
            this.panelbottomRFID.Name = "panelbottomRFID";
            this.panelbottomRFID.Size = new System.Drawing.Size(748, 399);
            this.panelbottomRFID.TabIndex = 8;
            this.panelbottomRFID.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRFID_Paint);
            // 
            // paneltopRFID
            // 
            this.paneltopRFID.Controls.Add(this.btnRFID);
            this.paneltopRFID.Controls.Add(this.btnScanFace);
            this.paneltopRFID.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltopRFID.Location = new System.Drawing.Point(0, 0);
            this.paneltopRFID.Name = "paneltopRFID";
            this.paneltopRFID.Size = new System.Drawing.Size(748, 100);
            this.paneltopRFID.TabIndex = 9;
            this.paneltopRFID.Paint += new System.Windows.Forms.PaintEventHandler(this.paneltop_Paint);
            // 
            // ScanRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.paneltopRFID);
            this.Controls.Add(this.panelbottomRFID);
            this.Name = "ScanRFID";
            this.Size = new System.Drawing.Size(748, 535);
            this.Load += new System.EventHandler(this.ScanRFID_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelbottomRFID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneltopRFID)).EndInit();
            this.paneltopRFID.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnScanFace;
        private DevExpress.XtraEditors.SimpleButton btnRFID;
        private DevExpress.XtraEditors.PanelControl panelbottomRFID;
        private DevExpress.XtraEditors.PanelControl paneltopRFID;
    }
}
