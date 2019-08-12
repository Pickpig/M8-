namespace M8.Forms
{
    partial class FrmSystemConfig
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
            this.grpLoginConfig = new System.Windows.Forms.GroupBox();
            this.txtDatabasePassword = new System.Windows.Forms.TextBox();
            this.txtDatabaseUserName = new System.Windows.Forms.TextBox();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.lblDatabasePassword = new System.Windows.Forms.Label();
            this.lblDatabaseUserName = new System.Windows.Forms.Label();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.databaseIP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.grpLoginConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLoginConfig
            // 
            this.grpLoginConfig.Controls.Add(this.databaseIP);
            this.grpLoginConfig.Controls.Add(this.txtDatabasePassword);
            this.grpLoginConfig.Controls.Add(this.txtDatabaseUserName);
            this.grpLoginConfig.Controls.Add(this.txtDatabaseName);
            this.grpLoginConfig.Controls.Add(this.lblDatabasePassword);
            this.grpLoginConfig.Controls.Add(this.lblDatabaseUserName);
            this.grpLoginConfig.Controls.Add(this.lblDatabaseName);
            this.grpLoginConfig.Controls.Add(this.lblServerIP);
            this.grpLoginConfig.Font = new System.Drawing.Font("宋体", 10F);
            this.grpLoginConfig.Location = new System.Drawing.Point(33, 43);
            this.grpLoginConfig.Name = "grpLoginConfig";
            this.grpLoginConfig.Size = new System.Drawing.Size(230, 149);
            this.grpLoginConfig.TabIndex = 14;
            this.grpLoginConfig.TabStop = false;
            this.grpLoginConfig.Text = "数据库设置";
            // 
            // txtDatabasePassword
            // 
            this.txtDatabasePassword.Font = new System.Drawing.Font("宋体", 9F);
            this.txtDatabasePassword.Location = new System.Drawing.Point(113, 92);
            this.txtDatabasePassword.Name = "txtDatabasePassword";
            this.txtDatabasePassword.PasswordChar = '#';
            this.txtDatabasePassword.Size = new System.Drawing.Size(75, 21);
            this.txtDatabasePassword.TabIndex = 6;
            // 
            // txtDatabaseUserName
            // 
            this.txtDatabaseUserName.Font = new System.Drawing.Font("宋体", 9F);
            this.txtDatabaseUserName.Location = new System.Drawing.Point(113, 70);
            this.txtDatabaseUserName.Name = "txtDatabaseUserName";
            this.txtDatabaseUserName.Size = new System.Drawing.Size(75, 21);
            this.txtDatabaseUserName.TabIndex = 5;
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Font = new System.Drawing.Font("宋体", 9F);
            this.txtDatabaseName.Location = new System.Drawing.Point(113, 48);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(75, 21);
            this.txtDatabaseName.TabIndex = 4;
            // 
            // lblDatabasePassword
            // 
            this.lblDatabasePassword.AutoSize = true;
            this.lblDatabasePassword.Font = new System.Drawing.Font("宋体", 9F);
            this.lblDatabasePassword.Location = new System.Drawing.Point(17, 97);
            this.lblDatabasePassword.Name = "lblDatabasePassword";
            this.lblDatabasePassword.Size = new System.Drawing.Size(41, 12);
            this.lblDatabasePassword.TabIndex = 3;
            this.lblDatabasePassword.Text = "密　码";
            // 
            // lblDatabaseUserName
            // 
            this.lblDatabaseUserName.AutoSize = true;
            this.lblDatabaseUserName.Font = new System.Drawing.Font("宋体", 9F);
            this.lblDatabaseUserName.Location = new System.Drawing.Point(17, 72);
            this.lblDatabaseUserName.Name = "lblDatabaseUserName";
            this.lblDatabaseUserName.Size = new System.Drawing.Size(41, 12);
            this.lblDatabaseUserName.TabIndex = 2;
            this.lblDatabaseUserName.Text = "用户名";
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.AutoSize = true;
            this.lblDatabaseName.Font = new System.Drawing.Font("宋体", 9F);
            this.lblDatabaseName.Location = new System.Drawing.Point(17, 47);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(41, 12);
            this.lblDatabaseName.TabIndex = 1;
            this.lblDatabaseName.Text = "数据库";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Font = new System.Drawing.Font("宋体", 9F);
            this.lblServerIP.Location = new System.Drawing.Point(17, 22);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(41, 12);
            this.lblServerIP.TabIndex = 0;
            this.lblServerIP.Text = "服务器";
            // 
            // databaseIP
            // 
            this.databaseIP.Font = new System.Drawing.Font("宋体", 9F);
            this.databaseIP.Location = new System.Drawing.Point(113, 19);
            this.databaseIP.Name = "databaseIP";
            this.databaseIP.Size = new System.Drawing.Size(75, 21);
            this.databaseIP.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmSystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpLoginConfig);
            this.Name = "FrmSystemConfig";
            this.Text = "系统配置";
            this.grpLoginConfig.ResumeLayout(false);
            this.grpLoginConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLoginConfig;
        private System.Windows.Forms.TextBox txtDatabasePassword;
        private System.Windows.Forms.TextBox txtDatabaseUserName;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label lblDatabasePassword;
        private System.Windows.Forms.Label lblDatabaseUserName;
        private System.Windows.Forms.Label lblDatabaseName;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.TextBox databaseIP;
        private System.Windows.Forms.Button button1;
    }
}