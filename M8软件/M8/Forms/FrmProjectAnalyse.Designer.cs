namespace M8.Forms
{
    partial class FrmProjectAnalyse
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
            this.btn_CrackD = new System.Windows.Forms.Button();
            this.btn_CrackF = new System.Windows.Forms.Button();
            this.btn_cheliang = new System.Windows.Forms.Button();
            this.btn_zhibei = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_CrackD
            // 
            this.btn_CrackD.Location = new System.Drawing.Point(73, 55);
            this.btn_CrackD.Name = "btn_CrackD";
            this.btn_CrackD.Size = new System.Drawing.Size(57, 31);
            this.btn_CrackD.TabIndex = 0;
            this.btn_CrackD.Text = "CrackD";
            this.btn_CrackD.UseVisualStyleBackColor = true;
            this.btn_CrackD.Click += new System.EventHandler(this.btn_CrackD_Click);
            // 
            // btn_CrackF
            // 
            this.btn_CrackF.Location = new System.Drawing.Point(254, 54);
            this.btn_CrackF.Name = "btn_CrackF";
            this.btn_CrackF.Size = new System.Drawing.Size(57, 32);
            this.btn_CrackF.TabIndex = 1;
            this.btn_CrackF.Text = "CrackF";
            this.btn_CrackF.UseVisualStyleBackColor = true;
            this.btn_CrackF.Click += new System.EventHandler(this.btn_CrackF_Click);
            // 
            // btn_cheliang
            // 
            this.btn_cheliang.Location = new System.Drawing.Point(73, 130);
            this.btn_cheliang.Name = "btn_cheliang";
            this.btn_cheliang.Size = new System.Drawing.Size(57, 34);
            this.btn_cheliang.TabIndex = 2;
            this.btn_cheliang.Text = "车辆";
            this.btn_cheliang.UseVisualStyleBackColor = true;
            this.btn_cheliang.Click += new System.EventHandler(this.btn_cheliang_Click);
            // 
            // btn_zhibei
            // 
            this.btn_zhibei.Location = new System.Drawing.Point(254, 130);
            this.btn_zhibei.Name = "btn_zhibei";
            this.btn_zhibei.Size = new System.Drawing.Size(57, 34);
            this.btn_zhibei.TabIndex = 3;
            this.btn_zhibei.Text = "植被";
            this.btn_zhibei.UseVisualStyleBackColor = true;
            this.btn_zhibei.Click += new System.EventHandler(this.btn_zhibei_Click);
            // 
            // FrmProjectAnalyse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 225);
            this.Controls.Add(this.btn_zhibei);
            this.Controls.Add(this.btn_cheliang);
            this.Controls.Add(this.btn_CrackF);
            this.Controls.Add(this.btn_CrackD);
            this.Name = "FrmProjectAnalyse";
            this.Text = "FrmProjectAnalyse";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProjectAnalyse_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_CrackD;
        private System.Windows.Forms.Button btn_CrackF;
        private System.Windows.Forms.Button btn_cheliang;
        private System.Windows.Forms.Button btn_zhibei;
    }
}