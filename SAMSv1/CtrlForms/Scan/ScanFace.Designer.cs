namespace SAMSv1.CtrlForms.Scan
{
    partial class ScanFace
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
            this.panelFace = new DevExpress.XtraEditors.PanelControl();
            this.cameraControl1 = new DevExpress.XtraEditors.Camera.CameraControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelFace)).BeginInit();
            this.panelFace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScanFace
            // 
            this.btnScanFace.Location = new System.Drawing.Point(151, 21);
            this.btnScanFace.Name = "btnScanFace";
            this.btnScanFace.Size = new System.Drawing.Size(120, 52);
            this.btnScanFace.TabIndex = 5;
            this.btnScanFace.Text = "SCAN FACE";
            // 
            // btnRFID
            // 
            this.btnRFID.Location = new System.Drawing.Point(25, 21);
            this.btnRFID.Name = "btnRFID";
            this.btnRFID.Size = new System.Drawing.Size(120, 52);
            this.btnRFID.TabIndex = 4;
            this.btnRFID.Text = "SCAN RFID";
            // 
            // panelFace
            // 
            this.panelFace.Controls.Add(this.cameraControl1);
            this.panelFace.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFace.Location = new System.Drawing.Point(0, 142);
            this.panelFace.Name = "panelFace";
            this.panelFace.Size = new System.Drawing.Size(748, 393);
            this.panelFace.TabIndex = 6;
            this.panelFace.Paint += new System.Windows.Forms.PaintEventHandler(this.panelFace_Paint);
            // 
            // cameraControl1
            // 
            this.cameraControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraControl1.Location = new System.Drawing.Point(2, 2);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(744, 389);
            this.cameraControl1.TabIndex = 0;
            this.cameraControl1.Text = "cameraControl1";
            this.cameraControl1.Click += new System.EventHandler(this.cameraControl1_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnRFID);
            this.panelControl1.Controls.Add(this.btnScanFace);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(748, 94);
            this.panelControl1.TabIndex = 7;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // ScanFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelFace);
            this.Name = "ScanFace";
            this.Size = new System.Drawing.Size(748, 535);
            this.Load += new System.EventHandler(this.ScanFace_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelFace)).EndInit();
            this.panelFace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnScanFace;
        private DevExpress.XtraEditors.SimpleButton btnRFID;
        private DevExpress.XtraEditors.PanelControl panelFace;
        private DevExpress.XtraEditors.Camera.CameraControl cameraControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}
