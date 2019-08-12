namespace M8.Forms
{
    partial class FrmSQLConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_SQLIP = new System.Windows.Forms.TextBox();
            this.txt_SQLPort = new System.Windows.Forms.TextBox();
            this.btn_OK_Click = new System.Windows.Forms.Button();
            this.btn_Cancel_Click = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口号";
            // 
            // txt_SQLIP
            // 
            this.txt_SQLIP.Location = new System.Drawing.Point(110, 35);
            this.txt_SQLIP.Name = "txt_SQLIP";
            this.txt_SQLIP.Size = new System.Drawing.Size(100, 21);
            this.txt_SQLIP.TabIndex = 2;
            // 
            // txt_SQLPort
            // 
            this.txt_SQLPort.Location = new System.Drawing.Point(110, 106);
            this.txt_SQLPort.Name = "txt_SQLPort";
            this.txt_SQLPort.Size = new System.Drawing.Size(100, 21);
            this.txt_SQLPort.TabIndex = 3;
            // 
            // btn_OK_Click
            // 
            this.btn_OK_Click.Location = new System.Drawing.Point(39, 159);
            this.btn_OK_Click.Name = "btn_OK_Click";
            this.btn_OK_Click.Size = new System.Drawing.Size(75, 23);
            this.btn_OK_Click.TabIndex = 4;
            this.btn_OK_Click.Text = "确定";
            this.btn_OK_Click.UseVisualStyleBackColor = true;
            this.btn_OK_Click.Click += new System.EventHandler(this.btn_OK_Click_Click);
            // 
            // btn_Cancel_Click
            // 
            this.btn_Cancel_Click.Location = new System.Drawing.Point(171, 159);
            this.btn_Cancel_Click.Name = "btn_Cancel_Click";
            this.btn_Cancel_Click.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel_Click.TabIndex = 5;
            this.btn_Cancel_Click.Text = "取消";
            this.btn_Cancel_Click.UseVisualStyleBackColor = true;
            this.btn_Cancel_Click.Click += new System.EventHandler(this.btn_Cancel_Click_Click);
            // 
            // FrmSQLConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 194);
            this.Controls.Add(this.btn_Cancel_Click);
            this.Controls.Add(this.btn_OK_Click);
            this.Controls.Add(this.txt_SQLPort);
            this.Controls.Add(this.txt_SQLIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmSQLConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库设置";
            this.Load += new System.EventHandler(this.FrmSQLConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_SQLIP;
        private System.Windows.Forms.TextBox txt_SQLPort;
        private System.Windows.Forms.Button btn_OK_Click;
        private System.Windows.Forms.Button btn_Cancel_Click;
    }
}