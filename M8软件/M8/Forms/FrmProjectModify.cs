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
    public partial class FrmProjectModify : Form
    {
        private ClsProject curProject = new ClsProject();
        public ClsSQLMethod sqlMethod = new ClsSQLMethod();

        private ClsProject _project;
        public ClsProject Project
        {
            get { return _project; }
            set { _project = value; }
        }
        public FrmProjectModify()
        {
            InitializeComponent();
            
            Project = new ClsProject();
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            //修改项目
            if (Project.intProjectID != 0)
            {
                //Project.strProjectName = textProjectID.Text;
                Project.strProjectName = textProjectName.Text;
                Project.strProjectBuilder = textProjectBuilder.Text;
                Project.description = txt_description.Text;
                Project.intProjectID = sqlMethod.ModifyProject(Project);
                //sqlMethod.ModifyProject(Project);
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("项目修改完成！");
                this.Close();

            }
            //新建项目
            else
            {
                if (textProjectName.Text == "")
                    MessageBox.Show("请输入项目名称！");
                else
                {
                    //curProject.strProjectName = textProjectID.Text;
                    curProject.strProjectName = textProjectName.Text;
                    curProject.dtpTime = dtpTime.Value;
                    curProject.strProjectBuilder = textProjectBuilder.Text;
                    curProject.description = txt_description.Text;
                    sqlMethod.AddProject(curProject);
                    MessageBox.Show("项目新建完成！");
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProjectModify_Load_1(object sender, EventArgs e)
        {
            if (Project.intProjectID != 0)
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
    }
}
