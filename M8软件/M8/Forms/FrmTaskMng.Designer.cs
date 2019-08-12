namespace M8.Forms
{
    partial class FrmTaskMng
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTaskList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_TaskBuild = new System.Windows.Forms.Button();
            this.btn_TaskModify = new System.Windows.Forms.Button();
            this.btn_TaskDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_projectID = new System.Windows.Forms.TextBox();
            this.btn_inputData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTaskList
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaskList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTaskList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTaskList.Location = new System.Drawing.Point(12, 78);
            this.dgvTaskList.Name = "dgvTaskList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaskList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTaskList.RowTemplate.Height = 23;
            this.dgvTaskList.Size = new System.Drawing.Size(549, 198);
            this.dgvTaskList.TabIndex = 0;
            this.dgvTaskList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTaskList_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "任务名称";
            this.Column1.Name = "Column1";
            this.Column1.Width = 120;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "物理硬盘";
            this.Column2.Name = "Column2";
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "起始位置";
            this.Column3.Name = "Column3";
            this.Column3.Width = 120;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "备注";
            this.Column5.Name = "Column5";
            this.Column5.Width = 135;
            // 
            // btn_TaskBuild
            // 
            this.btn_TaskBuild.Location = new System.Drawing.Point(12, 49);
            this.btn_TaskBuild.Name = "btn_TaskBuild";
            this.btn_TaskBuild.Size = new System.Drawing.Size(59, 23);
            this.btn_TaskBuild.TabIndex = 1;
            this.btn_TaskBuild.Text = "新建";
            this.btn_TaskBuild.UseVisualStyleBackColor = true;
            this.btn_TaskBuild.Click += new System.EventHandler(this.btn_TaskBuild_Click);
            // 
            // btn_TaskModify
            // 
            this.btn_TaskModify.Location = new System.Drawing.Point(77, 49);
            this.btn_TaskModify.Name = "btn_TaskModify";
            this.btn_TaskModify.Size = new System.Drawing.Size(59, 23);
            this.btn_TaskModify.TabIndex = 2;
            this.btn_TaskModify.Text = "修改";
            this.btn_TaskModify.UseVisualStyleBackColor = true;
            this.btn_TaskModify.Click += new System.EventHandler(this.btn_TaskModify_Click);
            // 
            // btn_TaskDelete
            // 
            this.btn_TaskDelete.Location = new System.Drawing.Point(142, 49);
            this.btn_TaskDelete.Name = "btn_TaskDelete";
            this.btn_TaskDelete.Size = new System.Drawing.Size(59, 23);
            this.btn_TaskDelete.TabIndex = 3;
            this.btn_TaskDelete.Text = "删除";
            this.btn_TaskDelete.UseVisualStyleBackColor = true;
            this.btn_TaskDelete.Click += new System.EventHandler(this.btn_TaskDelete_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(163, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "项目ID";
            // 
            // txt_projectID
            // 
            this.txt_projectID.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_projectID.Location = new System.Drawing.Point(263, 2);
            this.txt_projectID.Name = "txt_projectID";
            this.txt_projectID.ReadOnly = true;
            this.txt_projectID.Size = new System.Drawing.Size(131, 31);
            this.txt_projectID.TabIndex = 5;
            this.txt_projectID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_inputData
            // 
            this.btn_inputData.Location = new System.Drawing.Point(228, 49);
            this.btn_inputData.Name = "btn_inputData";
            this.btn_inputData.Size = new System.Drawing.Size(68, 23);
            this.btn_inputData.TabIndex = 6;
            this.btn_inputData.Text = "导入数据";
            this.btn_inputData.UseVisualStyleBackColor = true;
            this.btn_inputData.Click += new System.EventHandler(this.btn_inputData_Click);
            // 
            // FrmTaskMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 288);
            this.Controls.Add(this.btn_inputData);
            this.Controls.Add(this.txt_projectID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_TaskDelete);
            this.Controls.Add(this.btn_TaskModify);
            this.Controls.Add(this.btn_TaskBuild);
            this.Controls.Add(this.dgvTaskList);
            this.Name = "FrmTaskMng";
            this.Text = "任务管理";
            this.Load += new System.EventHandler(this.FrmTaskMng_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTaskList;
        private System.Windows.Forms.Button btn_TaskBuild;
        private System.Windows.Forms.Button btn_TaskModify;
        private System.Windows.Forms.Button btn_TaskDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_projectID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button btn_inputData;
    }
}