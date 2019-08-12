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

namespace M8.Forms
{
    public partial class FrmTaskNew : Form
    {
        ClsTask curTask = new ClsTask();
        public ClsSQLMethod sqlMethod = new ClsSQLMethod();

        private ClsTask _clsTask;
        public ClsTask ClsTask
        {
            get { return _clsTask; }
            set { _clsTask = value; }
        }
        
        public FrmTaskNew()
        {
            InitializeComponent();
            ClsTask = new ClsTask();
        }

        public string Info()
        {
            StringBuilder strBuilder = new StringBuilder();
            DataTable dt = new DataTable();
            dt = ClsSQLMethod.SearchTask();
            if (curTask.strTaskName=="")
                strBuilder.Append("请输入任务名称！");

            foreach (DataRow row in dt.Rows)
            {
                if (curTask.strTaskName== row[2].ToString())
                    strBuilder.Append("此任务已存在！");
            }
            return strBuilder.ToString();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (ClsTask.strTaskName!= null)
            {
                FrmProjectModify frmProjcetModify=new FrmProjectModify();
                ClsTask.strTaskName = txt_TaskName.Text;
                ClsTask.Disk = txt_Disk.Text;
                ClsTask.StartPosition = txt_StartPosition.Text;
                ClsTask.Description = txt_Description.Text;
                ClsTask.TaskID = sqlMethod.ModifyTask(ClsTask);
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("任务修改完成！");
                //frmProjcetModify.DialogResult = DialogResult.OK;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if (txt_TaskName.Text == "")
                    MessageBox.Show("请输入任务名称！");
                else
                {
                    curTask.ProjectID = ClsTask.ProjectID;
                    curTask.strTaskName = txt_TaskName.Text;
                    curTask.Disk = txt_Disk.Text;
                    curTask.StartPosition = txt_StartPosition.Text;
                    curTask.Description = txt_Description.Text;
                    string info = Info();
                    if (info != "")
                    {
                        MessageBox.Show(info);
                        return;
                    }
                    sqlMethod.AddTask(curTask);
                    this.Close();
                    MessageBox.Show("任务新建完成！");
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void FrmTaskNew_Load(object sender, EventArgs e)
        {
            if (_clsTask != null)
            {
                txt_ProjectID.Text = _clsTask.ProjectID.ToString();
                txt_TaskName.Text = _clsTask.strTaskName;
                txt_Disk.Text = _clsTask.Disk;
                txt_StartPosition.Text = _clsTask.StartPosition;
                txt_Description.Text = _clsTask.Description;
            }
        } 

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
