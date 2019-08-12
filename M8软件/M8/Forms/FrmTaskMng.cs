using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using M8.Class;
using System.IO;

namespace M8.Forms
{
    public partial class FrmTaskMng : Form
    {
        private ClsTask _clsTask;
        public ClsTask ClsTask
        {
            get { return _clsTask; }
            set { _clsTask = value; }
        }

        public string taskName, projectName;

        public ClsTask curTask;

        public FrmTaskMng()
        {
            ClsTask = new ClsTask();
            InitializeComponent();
        }
        

        private void btn_TaskBuild_Click(object sender, EventArgs e)
        {
            FrmTaskNew frmTaskNew = new FrmTaskNew();
            frmTaskNew.ClsTask.ProjectID = Convert.ToInt32(txt_projectID.Text);
            //frmTaskNew.ClsTask.ProjectName = txt_projectID.Text;
            DataGridViewRow item = dgvTaskList.CurrentRow;
            frmTaskNew.ShowDialog();
            //if (frmTaskNew.ShowDialog() == DialogResult.OK)
            //{
            //    this.DialogResult = DialogResult.OK;
            //}
            LoadTaskInfo();
        }

        private void btn_TaskModify_Click(object sender, EventArgs e)
        {
            FrmTaskNew frmTaskNew = new FrmTaskNew();
            DataGridViewRow item = dgvTaskList.CurrentRow;
            if (item == null || item.Cells.Count == 0 || item.Cells[0].Value ==null )
                return;
            frmTaskNew.ClsTask.ProjectID =Convert.ToInt32(txt_projectID.Text);
            frmTaskNew.ClsTask.strTaskName = item.Cells[0].Value.ToString();
            frmTaskNew.ClsTask.Disk = item.Cells[1].Value.ToString();
            frmTaskNew.ClsTask.StartPosition = item.Cells[2].Value.ToString();
            frmTaskNew.ClsTask.Description = item.Cells[3].Value.ToString();
            frmTaskNew.ClsTask.TaskID = (int)item.Tag;
            frmTaskNew.StartPosition = FormStartPosition.CenterScreen;
            frmTaskNew.ShowDialog();
            //if (frmTaskNew.ShowDialog() == DialogResult.OK)
            //{
            //    this.DialogResult = DialogResult.OK;
            //}
            LoadTaskInfo();
        }

        private void LoadTaskInfo()
        {
            dgvTaskList.Rows.Clear();
            DataTable dt;
            dt = ClsSQLMethod.SearchTask();
            DataGridViewRow dgvRow;
            foreach (DataRow row in dt.Rows)
            {
                if (row[1].ToString() == curTask.ProjectID.ToString())
                {
                    dgvRow = new DataGridViewRow();
                    dgvRow.CreateCells(dgvTaskList);
                    dgvRow.Cells[0].Value = row[2].ToString();
                    dgvRow.Cells[1].Value = row[3].ToString();
                    dgvRow.Cells[2].Value = row[4].ToString();
                    dgvRow.Cells[3].Value = row[5].ToString();
                    dgvRow.Tag = row[0];
                    dgvTaskList.Rows.Add(dgvRow);
                }
            }
        }

        private void FrmTaskMng_Load(object sender, EventArgs e)
        {

            FrmTaskNew frmTaskNew = new FrmTaskNew();
            txt_projectID.Text = ClsTask.ProjectID.ToString();
            projectName = ClsTask.ProjectName;
            frmTaskNew.ClsTask.ProjectID =Convert.ToInt32(txt_projectID.Text);
            dgvTaskList.Rows.Clear();
            DataTable dt;
            dt = ClsSQLMethod.SearchTask();
            DataGridViewRow dgvRow;
            FrmTaskMng frmTaskMng=new FrmTaskMng();
            curTask = new ClsTask();
            curTask.ProjectID =frmTaskNew.ClsTask.ProjectID;
            
            foreach (DataRow row in dt.Rows)
            {
                if (row[1].ToString() == curTask.ProjectID.ToString())
                {
                    dgvRow = new DataGridViewRow();
                    dgvRow.CreateCells(dgvTaskList);
                    dgvRow.Cells[0].Value = row[2].ToString();
                    dgvRow.Cells[1].Value = row[3].ToString();
                    dgvRow.Cells[2].Value = row[4].ToString();
                    dgvRow.Cells[3].Value = row[5].ToString();
                    dgvRow.Tag = row[0];
                    dgvTaskList.Rows.Add(dgvRow);
                }
            }
            //DataGridViewRow item = dgvTaskList.CurrentRow;
            //taskName = item.Cells[3].Value.ToString();
            if (dgvTaskList.CurrentRow!=null )
                dgvTaskList.CurrentRow.Selected = true;
        }

        private void dgvTaskList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvTaskList.CurrentRow.Selected = true;
        }

        private void btn_TaskDelete_Click(object sender, EventArgs e)
        {
            FrmTaskNew frmTaskNew = new FrmTaskNew();
            DataTable dt;
            dt = ClsSQLMethod.SearchTask();
            DataGridViewRow item = dgvTaskList.CurrentRow;
            if (item == null)
                return;
            frmTaskNew.ClsTask.strTaskName = item.Cells[0].Value.ToString();
            frmTaskNew.ClsTask.TaskID = (int)item.Tag;
            try
            {
                if (MessageBox.Show("确认删除任务：" + frmTaskNew.ClsTask.strTaskName + "?", "删除任务", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (((int)item.Tag == int.Parse(row[0].ToString())))
                        {
                            break;
                        }
                    }
                    ClsSQLMethod.DeleteTask(frmTaskNew.ClsTask);
                    //this.DialogResult = DialogResult.OK;
                    LoadTaskInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败！", "删除任务");
            }

        }

        private void btn_inputData_Click(object sender, EventArgs e)
        {
            DataGridViewRow item = dgvTaskList.CurrentRow;
            if (item == null || item.Cells.Count == 0 || item.Cells[0].Value == null)
                return;
            taskName = item.Cells[0].Value.ToString();
            if (Directory.Exists(@"e:\" + projectName + @"\" + taskName + @"\" + "shuru" + @"\" + "tongbuban") == false)
            {
                MessageBox.Show("文件夹不存在");
                if (DialogResult.Yes == MessageBox.Show("是否要创建文件夹：" + taskName, "提示", MessageBoxButtons.YesNo))
                {
                    Directory.CreateDirectory(@"e:\" + projectName + @"\" + taskName + @"\" + "shuru" + @"\" + "tongbuban");
                    Directory.CreateDirectory(@"e:\" + projectName + @"\" + taskName + @"\" + "shuru" + @"\" + "zijiayi");
                    Directory.CreateDirectory(@"e:\" + projectName + @"\" + taskName + @"\" + "shuru" + @"\" + "jiguangleida");
                    Directory.CreateDirectory(@"e:\" + projectName + @"\" + taskName + @"\" + "shuru" + @"\" + "gaoqingxiangji");
                    Directory.CreateDirectory(@"e:\" + projectName + @"\" + taskName + @"\" + "shuru" + @"\" + "guangdiandiaocang" + @"\" + "kejianguang");
                    MessageBox.Show(taskName + "创建完成！");
                }
            }
            else
            {
                FrmPhotoCopy copyDisk = new FrmPhotoCopy();
                copyDisk.ShowDialog();
            }
           
        }
    }
}
