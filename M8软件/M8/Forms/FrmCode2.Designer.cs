﻿namespace M8.Forms
{
    partial class FrmCode2
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.generateCode2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(66, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 21);
            this.textBox1.TabIndex = 2;
            // 
            // generateCode2
            // 
            this.generateCode2.Location = new System.Drawing.Point(142, 112);
            this.generateCode2.Name = "generateCode2";
            this.generateCode2.Size = new System.Drawing.Size(75, 23);
            this.generateCode2.TabIndex = 3;
            this.generateCode2.Text = "生成二维码";
            this.generateCode2.UseVisualStyleBackColor = true;
            this.generateCode2.Click += new System.EventHandler(this.generateCode2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(285, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(113, 85);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Location = new System.Drawing.Point(142, 76);
            this.openFileDialog.Name = "openFileDialog";
            this.openFileDialog.Size = new System.Drawing.Size(75, 23);
            this.openFileDialog.TabIndex = 6;
            this.openFileDialog.Text = "二维码存储";
            this.openFileDialog.UseVisualStyleBackColor = true;
            this.openFileDialog.Click += new System.EventHandler(this.openFileDialog_Click);
            // 
            // FrmCode2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 164);
            this.Controls.Add(this.openFileDialog);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.generateCode2);
            this.Controls.Add(this.textBox1);
            this.Name = "FrmCode2";
            this.Text = "生成二维码";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button generateCode2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button openFileDialog;
    }
}