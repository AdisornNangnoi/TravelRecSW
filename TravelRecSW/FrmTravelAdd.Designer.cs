namespace TravelRecSW
{
    partial class FrmTravelAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTravelAdd));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbTravelImage = new System.Windows.Forms.PictureBox();
            this.btSelectTravelImage = new System.Windows.Forms.Button();
            this.dtpTravelEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpTravelStartDate = new System.Windows.Forms.DateTimePicker();
            this.tbTravelCostTotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTravelPlace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtToFrmLogin = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTravelImage)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pbTravelImage);
            this.panel1.Location = new System.Drawing.Point(295, 113);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 152);
            this.panel1.TabIndex = 41;
            // 
            // pbTravelImage
            // 
            this.pbTravelImage.Image = global::TravelRecSW.Properties.Resources.logo;
            this.pbTravelImage.Location = new System.Drawing.Point(3, 3);
            this.pbTravelImage.Name = "pbTravelImage";
            this.pbTravelImage.Size = new System.Drawing.Size(115, 144);
            this.pbTravelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTravelImage.TabIndex = 1;
            this.pbTravelImage.TabStop = false;
            // 
            // btSelectTravelImage
            // 
            this.btSelectTravelImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSelectTravelImage.Location = new System.Drawing.Point(295, 271);
            this.btSelectTravelImage.Name = "btSelectTravelImage";
            this.btSelectTravelImage.Size = new System.Drawing.Size(123, 33);
            this.btSelectTravelImage.TabIndex = 40;
            this.btSelectTravelImage.Text = "เลือกรูปการเดินทาง";
            this.btSelectTravelImage.UseVisualStyleBackColor = true;
            this.btSelectTravelImage.Click += new System.EventHandler(this.btSelectTravelImage_Click);
            // 
            // dtpTravelEndDate
            // 
            this.dtpTravelEndDate.Location = new System.Drawing.Point(66, 226);
            this.dtpTravelEndDate.Name = "dtpTravelEndDate";
            this.dtpTravelEndDate.Size = new System.Drawing.Size(188, 20);
            this.dtpTravelEndDate.TabIndex = 39;
            // 
            // dtpTravelStartDate
            // 
            this.dtpTravelStartDate.Location = new System.Drawing.Point(66, 173);
            this.dtpTravelStartDate.Name = "dtpTravelStartDate";
            this.dtpTravelStartDate.Size = new System.Drawing.Size(188, 20);
            this.dtpTravelStartDate.TabIndex = 38;
            // 
            // tbTravelCostTotal
            // 
            this.tbTravelCostTotal.Location = new System.Drawing.Point(66, 284);
            this.tbTravelCostTotal.Name = "tbTravelCostTotal";
            this.tbTravelCostTotal.Size = new System.Drawing.Size(188, 20);
            this.tbTravelCostTotal.TabIndex = 37;
            this.tbTravelCostTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTravelCostTotal_KeyPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(66, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 20);
            this.label4.TabIndex = 36;
            this.label4.Text = "ค่าใช้จ่ายตลอดการเดินทาง (บาท)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(66, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 20);
            this.label5.TabIndex = 35;
            this.label5.Text = "วันที่เดินทางกลับ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTravelPlace
            // 
            this.tbTravelPlace.Location = new System.Drawing.Point(66, 126);
            this.tbTravelPlace.Name = "tbTravelPlace";
            this.tbTravelPlace.Size = new System.Drawing.Size(188, 20);
            this.tbTravelPlace.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(66, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 20);
            this.label3.TabIndex = 33;
            this.label3.Text = "วันที่เดินทางไป";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(66, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 20);
            this.label2.TabIndex = 32;
            this.label2.Text = "สถานที่ที่เดินทางไป";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(35, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 48);
            this.label1.TabIndex = 31;
            this.label1.Text = "การเดินทางของฉัน (เพิ่ม)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtSave,
            this.tsbtCancel,
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.tsbtToFrmLogin});
            this.toolStrip1.Location = new System.Drawing.Point(473, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(128, 335);
            this.toolStrip1.TabIndex = 42;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtSave
            // 
            this.tsbtSave.Image = global::TravelRecSW.Properties.Resources.save;
            this.tsbtSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtSave.Name = "tsbtSave";
            this.tsbtSave.Size = new System.Drawing.Size(126, 20);
            this.tsbtSave.Text = "บันทึก";
            this.tsbtSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbtSave.Click += new System.EventHandler(this.tsbtSave_Click);
            // 
            // tsbtCancel
            // 
            this.tsbtCancel.Image = global::TravelRecSW.Properties.Resources.cancel;
            this.tsbtCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtCancel.Name = "tsbtCancel";
            this.tsbtCancel.Size = new System.Drawing.Size(126, 20);
            this.tsbtCancel.Text = "ยกเลิก";
            this.tsbtCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbtCancel.Click += new System.EventHandler(this.tsbtCancel_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripLabel1.Size = new System.Drawing.Size(126, 0);
            this.toolStripLabel1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(126, 6);
            // 
            // tsbtToFrmLogin
            // 
            this.tsbtToFrmLogin.Image = global::TravelRecSW.Properties.Resources.logo;
            this.tsbtToFrmLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtToFrmLogin.Name = "tsbtToFrmLogin";
            this.tsbtToFrmLogin.Size = new System.Drawing.Size(126, 20);
            this.tsbtToFrmLogin.Text = "การเดินทางของฉัน";
            this.tsbtToFrmLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbtToFrmLogin.Click += new System.EventHandler(this.tsbtToFrmLogin_Click);
            // 
            // FrmTravelAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 335);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btSelectTravelImage);
            this.Controls.Add(this.dtpTravelEndDate);
            this.Controls.Add(this.dtpTravelStartDate);
            this.Controls.Add(this.tbTravelCostTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbTravelPlace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmTravelAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เพิ่มการเดินทาง - Travel Rec SW V.1.0";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTravelImage)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbTravelImage;
        private System.Windows.Forms.Button btSelectTravelImage;
        private System.Windows.Forms.DateTimePicker dtpTravelEndDate;
        private System.Windows.Forms.DateTimePicker dtpTravelStartDate;
        private System.Windows.Forms.TextBox tbTravelCostTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTravelPlace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtSave;
        private System.Windows.Forms.ToolStripButton tsbtCancel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtToFrmLogin;
    }
}