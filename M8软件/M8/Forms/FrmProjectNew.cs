using M8.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M8.Forms
{
    public partial class FrmProjectNew : Form
    {
        private ClsProject curProject = new ClsProject();
        public ClsSQLMethod sqlMethod = new ClsSQLMethod();

        private ClsProject _project;
        public ClsProject Project
        {
            get { return _project; }
            set { _project = value; }
        }

        public FrmProjectNew()
        {
            InitializeComponent();
            Project = new ClsProject();
        }

        private void FrmProjectNew_Load(object sender, EventArgs e)
        {
            if (Project.intProjectID!= 0)
            {
                textProjectBuilder.Enabled = false;
                textProjectID.Enabled = false;
                textProjectName.Enabled = false;
                textProjectName.Text = _project.strProjectName;
                textProjectBuilder.Text = _project.strProjectBuilder;
                textProjectID.Text = _project.intProjectID.ToString();
                txt_description.Text = _project.description.Trim();
            }
           
        }

        public string Info()
        {
            StringBuilder strBuilder = new StringBuilder();
            DataTable dt = new DataTable();
            dt = ClsSQLMethod.SearchProject();
            if (textProjectName.Text == "")
                strBuilder.Append("请输入项目名称！");

            foreach (DataRow row in dt.Rows)
            {
                if (textProjectName.Text == row[1].ToString())
                    strBuilder.Append("此项目已存在！");
            }
            return strBuilder.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //修改项目
            if (Project.intProjectID != 0)
            {
                //Project.strProjectName = textProjectID.Text;
                Project.strProjectName = textProjectName.Text;
                Project.strProjectBuilder = textProjectBuilder.Text;
                Project.description = txt_description.Text;
                Project.intProjectID = sqlMethod.ModifyProject(Project);
                MessageBox.Show("项目修改完成！");
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            //新建项目
            else
            {
                string info = Info();
                if (info != "")
                {
                    MessageBox.Show(info);
                }
                else
                {
                    //curProject.strProjectName = textProjectID.Text;
                    curProject.strProjectName = textProjectName.Text;
                    curProject.dtpTime = dtpTime.Value;
                    curProject.strProjectBuilder = textProjectBuilder.Text;
                    curProject.description = txt_description.Text;
                    sqlMethod.AddProject(curProject);
                    MessageBox.Show("项目新建完成！");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            }
        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
