namespace SAMSv1
{
    partial class AdminForm
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
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.txtSTUDENTNAME = new DevExpress.XtraEditors.TextEdit();
            this.txtIDNUMBER = new DevExpress.XtraEditors.TextEdit();
            this.cbCOURSE = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbYEARLEVEL = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnADDSTUDENT = new DevExpress.XtraEditors.SimpleButton();
            this.txtEVENTNAME = new DevExpress.XtraEditors.TextEdit();
            this.btnADDEVENT = new DevExpress.XtraEditors.SimpleButton();
            this.cbSTUDENTID = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbEVENTID = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbATTENDANCETYPE = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtEVENTDESC = new DevExpress.XtraEditors.TextEdit();
            this.btnADDATTENDANCE = new DevExpress.XtraEditors.SimpleButton();
            this.cbSESSION = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTUDENTNAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIDNUMBER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCOURSE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYEARLEVEL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEVENTNAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSTUDENTID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEVENTID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbATTENDANCETYPE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEVENTDESC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSESSION.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablePanel1.SetColumn(this.simpleButton1, 0);
            this.simpleButton1.Location = new System.Drawing.Point(17, 16);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.tablePanel1.SetRow(this.simpleButton1, 0);
            this.simpleButton1.Size = new System.Drawing.Size(287, 65);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Register Student";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablePanel1.SetColumn(this.simpleButton2, 1);
            this.simpleButton2.Location = new System.Drawing.Point(310, 16);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.tablePanel1.SetRow(this.simpleButton2, 0);
            this.simpleButton2.Size = new System.Drawing.Size(304, 65);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "Attendance";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tablePanel1.SetColumn(this.simpleButton3, 2);
            this.simpleButton3.Location = new System.Drawing.Point(620, 16);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton3.Name = "simpleButton3";
            this.tablePanel1.SetRow(this.simpleButton3, 0);
            this.simpleButton3.Size = new System.Drawing.Size(287, 65);
            this.simpleButton3.TabIndex = 3;
            this.simpleButton3.Text = "Report";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton4.Location = new System.Drawing.Point(745, 511);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(152, 30);
            this.simpleButton4.TabIndex = 7;
            this.simpleButton4.Text = "Logout";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // tablePanel1
            // 
            this.tablePanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.Appearance.Options.UseBackColor = true;
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 52F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 52F)});
            this.tablePanel1.Controls.Add(this.simpleButton3);
            this.tablePanel1.Controls.Add(this.simpleButton2);
            this.tablePanel1.Controls.Add(this.simpleButton1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(924, 98);
            this.tablePanel1.TabIndex = 8;
            this.tablePanel1.UseSkinIndents = true;
            this.tablePanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tablePanel1_Paint);
            // 
            // txtSTUDENTNAME
            // 
            this.txtSTUDENTNAME.Location = new System.Drawing.Point(40, 150);
            this.txtSTUDENTNAME.Name = "txtSTUDENTNAME";
            this.txtSTUDENTNAME.Size = new System.Drawing.Size(100, 28);
            this.txtSTUDENTNAME.TabIndex = 9;
            // 
            // txtIDNUMBER
            // 
            this.txtIDNUMBER.Location = new System.Drawing.Point(40, 200);
            this.txtIDNUMBER.Name = "txtIDNUMBER";
            this.txtIDNUMBER.Size = new System.Drawing.Size(100, 28);
            this.txtIDNUMBER.TabIndex = 10;
            // 
            // cbCOURSE
            // 
            this.cbCOURSE.Location = new System.Drawing.Point(40, 252);
            this.cbCOURSE.Name = "cbCOURSE";
            this.cbCOURSE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbCOURSE.Size = new System.Drawing.Size(100, 28);
            this.cbCOURSE.TabIndex = 11;
            // 
            // cbYEARLEVEL
            // 
            this.cbYEARLEVEL.Location = new System.Drawing.Point(40, 305);
            this.cbYEARLEVEL.Name = "cbYEARLEVEL";
            this.cbYEARLEVEL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbYEARLEVEL.Size = new System.Drawing.Size(100, 28);
            this.cbYEARLEVEL.TabIndex = 12;
            // 
            // btnADDSTUDENT
            // 
            this.btnADDSTUDENT.Location = new System.Drawing.Point(55, 365);
            this.btnADDSTUDENT.Name = "btnADDSTUDENT";
            this.btnADDSTUDENT.Size = new System.Drawing.Size(75, 23);
            this.btnADDSTUDENT.TabIndex = 13;
            this.btnADDSTUDENT.Text = "simpleButton5";
            this.btnADDSTUDENT.Click += new System.EventHandler(this.btnADDSTUDENT_Click);
            // 
            // txtEVENTNAME
            // 
            this.txtEVENTNAME.Location = new System.Drawing.Point(324, 200);
            this.txtEVENTNAME.Name = "txtEVENTNAME";
            this.txtEVENTNAME.Size = new System.Drawing.Size(100, 28);
            this.txtEVENTNAME.TabIndex = 14;
            // 
            // btnADDEVENT
            // 
            this.btnADDEVENT.Location = new System.Drawing.Point(337, 243);
            this.btnADDEVENT.Name = "btnADDEVENT";
            this.btnADDEVENT.Size = new System.Drawing.Size(75, 23);
            this.btnADDEVENT.TabIndex = 15;
            this.btnADDEVENT.Text = "simpleButton6";
            this.btnADDEVENT.Click += new System.EventHandler(this.btnADDEVENT_Click);
            // 
            // cbSTUDENTID
            // 
            this.cbSTUDENTID.Location = new System.Drawing.Point(620, 150);
            this.cbSTUDENTID.Name = "cbSTUDENTID";
            this.cbSTUDENTID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSTUDENTID.Size = new System.Drawing.Size(100, 28);
            this.cbSTUDENTID.TabIndex = 16;
            // 
            // cbEVENTID
            // 
            this.cbEVENTID.Location = new System.Drawing.Point(620, 205);
            this.cbEVENTID.Name = "cbEVENTID";
            this.cbEVENTID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbEVENTID.Size = new System.Drawing.Size(100, 28);
            this.cbEVENTID.TabIndex = 17;
            // 
            // cbATTENDANCETYPE
            // 
            this.cbATTENDANCETYPE.Location = new System.Drawing.Point(620, 252);
            this.cbATTENDANCETYPE.Name = "cbATTENDANCETYPE";
            this.cbATTENDANCETYPE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbATTENDANCETYPE.Size = new System.Drawing.Size(100, 28);
            this.cbATTENDANCETYPE.TabIndex = 19;
            // 
            // txtEVENTDESC
            // 
            this.txtEVENTDESC.Location = new System.Drawing.Point(620, 358);
            this.txtEVENTDESC.Name = "txtEVENTDESC";
            this.txtEVENTDESC.Size = new System.Drawing.Size(100, 28);
            this.txtEVENTDESC.TabIndex = 20;
            // 
            // btnADDATTENDANCE
            // 
            this.btnADDATTENDANCE.Location = new System.Drawing.Point(635, 404);
            this.btnADDATTENDANCE.Name = "btnADDATTENDANCE";
            this.btnADDATTENDANCE.Size = new System.Drawing.Size(75, 23);
            this.btnADDATTENDANCE.TabIndex = 21;
            this.btnADDATTENDANCE.Text = "simpleButton7";
            this.btnADDATTENDANCE.Click += new System.EventHandler(this.btnADDATTENDANCE_Click);
            // 
            // cbSESSION
            // 
            this.cbSESSION.Location = new System.Drawing.Point(620, 305);
            this.cbSESSION.Name = "cbSESSION";
            this.cbSESSION.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSESSION.Size = new System.Drawing.Size(100, 28);
            this.cbSESSION.TabIndex = 22;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 551);
            this.Controls.Add(this.cbSESSION);
            this.Controls.Add(this.btnADDATTENDANCE);
            this.Controls.Add(this.txtEVENTDESC);
            this.Controls.Add(this.cbATTENDANCETYPE);
            this.Controls.Add(this.cbEVENTID);
            this.Controls.Add(this.cbSTUDENTID);
            this.Controls.Add(this.btnADDEVENT);
            this.Controls.Add(this.txtEVENTNAME);
            this.Controls.Add(this.btnADDSTUDENT);
            this.Controls.Add(this.cbYEARLEVEL);
            this.Controls.Add(this.cbCOURSE);
            this.Controls.Add(this.txtIDNUMBER);
            this.Controls.Add(this.txtSTUDENTNAME);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.simpleButton4);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminForm";
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSTUDENTNAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIDNUMBER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCOURSE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYEARLEVEL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEVENTNAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSTUDENTID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEVENTID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbATTENDANCETYPE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEVENTDESC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSESSION.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.TextEdit txtSTUDENTNAME;
        private DevExpress.XtraEditors.TextEdit txtIDNUMBER;
        private DevExpress.XtraEditors.ComboBoxEdit cbCOURSE;
        private DevExpress.XtraEditors.ComboBoxEdit cbYEARLEVEL;
        private DevExpress.XtraEditors.SimpleButton btnADDSTUDENT;
        private DevExpress.XtraEditors.TextEdit txtEVENTNAME;
        private DevExpress.XtraEditors.SimpleButton btnADDEVENT;
        private DevExpress.XtraEditors.ComboBoxEdit cbSTUDENTID;
        private DevExpress.XtraEditors.ComboBoxEdit cbEVENTID;
        private DevExpress.XtraEditors.ComboBoxEdit cbATTENDANCETYPE;
        private DevExpress.XtraEditors.TextEdit txtEVENTDESC;
        private DevExpress.XtraEditors.SimpleButton btnADDATTENDANCE;
        private DevExpress.XtraEditors.ComboBoxEdit cbSESSION;
    }
}