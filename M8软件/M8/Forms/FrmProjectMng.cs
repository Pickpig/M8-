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
using System.Collections.ObjectModel;
using M8.Forms;
using System.Data.SqlClient;

using System.Net;
using System.Collections.Specialized;
using System.Net.Sockets;


namespace M8.Forms
{
    public partial class FrmProjectMng : Form
    {
        public ClsSQLMethod sqlMethod = null;
        private ClsProject curProject = new ClsProject();
        ObservableCollection<ClsProject> projects = new ObservableCollection<ClsProject>();
        public string strSql;
        public string strTestStopTime;
        public string strTestTime;
        public string strTestStuff;
        public static int intCount = 0;
        public string[] strSource = new string[4];
        public int intAdd;
        public string sqlIP = null;
        public int sqlPort = 0;
        StringBuilder strBuilder = new StringBuilder();
         
        
        public FrmProjectMng()
        {
            InitializeComponent();
        }

        private void FrmProjectModify_Load(object sender, EventArgs e)
        {
            LoadProjectInfo();
            dgvProjectList.CurrentRow.Selected = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProjectModify projectModify = new FrmProjectModify();
                FrmTaskNew frmTaskNew = new FrmTaskNew();
                DataGridViewRow item = dgvProjectList.CurrentRow;
                if (item == null || item.Cells.Count == 0 || item.Cells[0].Value == null)
                    return;
                frmTaskNew.ClsTask.ProjectName = item.Cells[0].Value.ToString();
                projectModify.Project.strProjectName = item.Cells[0].Value.ToString();
                if (item.Cells[2].Value!=null)
                    projectModify.Project.strProjectBuilder = item.Cells[2].Value.ToString();
                if (item.Cells[3].Value != null)
                    projectModify.Project.description = item.Cells[3].Value.ToString();
                if (item.Tag != null)
                    projectModify.Project.intProjectID = (int)item.Tag;
                projectModify.StartPosition = FormStartPosition.CenterScreen;
                projectModify.ShowDialog();
                //if (projectModify.ShowDialog() == DialogResult.OK)
                //{
                //    this.DialogResult = DialogResult.OK;
                //}
                LoadProjectInfo();
            }
            catch (Exception)
            { }
        }

        private void LoadProjectInfo()
        {
            try
            {
                dgvProjectList.Rows.Clear();
                DataTable dt;
                dt = ClsSQLMethod.SearchProject();
                DataGridViewRow dgvRow;
                foreach (DataRow row in dt.Rows)
                {
                    dgvRow = new DataGridViewRow();
                    dgvRow.CreateCells(dgvProjectList);
                    dgvRow.Cells[0].Value = row[1].ToString();
                    dgvRow.Cells[1].Value = row[2].ToString();
                    dgvRow.Cells[2].Value = row[3].ToString();
                    dgvRow.Cells[3].Value = row[4].ToString();
                    dgvRow.Tag = row[0];
                    dgvProjectList.Rows.Add(dgvRow);
                }
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FrmProjectNew projectNew=new FrmProjectNew();
            DataTable dt;
            dt = ClsSQLMethod.SearchProject();
            DataGridViewRow item = dgvProjectList.CurrentRow;
            if (item == null||item.Tag == null)
                return;
            projectNew.Project.intProjectID = (int)item.Tag;
            try
            {
                if (MessageBox.Show("确认删除项目：" + projectNew.Project.intProjectID + "?", "删除项目" , MessageBoxButtons.OKCancel , MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if ((int)item.Tag == int.Parse(row[0].ToString()))
                        {
                            break;
                        }
                    }
                    //this.DialogResult = DialogResult.OK;
                    ClsSQLMethod.DeleteProject(projectNew.Project);
                    LoadProjectInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败！", "删除项目");
            }
        }

        string[] GetLocalIpv4()
        {
            IPAddress[] localIPs;
            localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            StringCollection IpCollection = new StringCollection();
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    IpCollection.Add(ip.ToString());
            }
            string[] IpArrar = new string[IpCollection.Count];
            IpCollection.CopyTo(IpArrar, 0);
            return IpArrar;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (cbTestTime.Checked == true)
            {
                if (dtpStartTime.Text != null&&dtpStopTime.Text!=null)
                {
                    strTestTime = "测试时间 between '" + dtpStartTime.Value.Date + "' and '" + dtpStopTime.Value.Date.AddDays(1) + "'";
                    strSource[intCount] = strTestTime;
                    intCount++;
                }
            }

            if (cbTestStuff.Checked == true)
            {
                if (txtTestStuff.Text != "")
                {
                    strTestStuff = "创建人  like '%" + txtTestStuff.Text + "%'";
                    strSource[intCount] = strTestStuff;
                    intCount++;
                }
            }
            for (int i = 0; i < strSource.Length; i++)
            {
                if (strSource[i] != null)
                {
                    strSql += strSource[i];
                    intAdd++;
                }
            }
            switch (intAdd)
            {
                case 0:
                    strSql = "select*from 项目列表";
                    break;
                case 1:
                    strSql = "select*from 项目列表 where " + strSource[0];
                    break;
                case 2:
                    strSql = "select*from 项目列表 where " + strSource[0] + " and " + strSource[1];
                    break;
                case 3:
                    strSql = "select*from 项目列表 where " + strSource[0] + " and " + strSource[1] + " and " + strSource[2];
                    break;
                case 4:
                    strSql = "select*from 项目列表 where " + strSource[0] + " and " + strSource[1] + " and " + strSource[2] + "and" + strSource[3];
                    break;
            }

            GetSource(strSql);
            intAdd = 0;
            intCount = 0;
            strSql = "";
            for (int i = 0; i < strSource.Length; i++)
            {
                if (strSource[i] != null)
                {
                    strSource[i] = null;
                }
            }
        }

        public void GetSource(string strSql)
        {
            foreach (string ip in GetLocalIpv4())
            {
                sqlIP = ip.ToString();
            }
            sqlPort = 1433;
            SqlConnection con = new SqlConnection("Data Source=" + sqlIP + "," + sqlPort.ToString() + ";Initial Catalog=M8;User ID=sa;Password=123456");
            con.Open();
            SqlCommand com = new SqlCommand(strSql, con);
            dgvProjectList.Rows.Clear();
            SqlDataReader dr = com.ExecuteReader();
            DataGridViewRow dgvRow;
            while (dr.Read())
            {
                //dgvProjectList.Rows.Add(dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), Tag = dr[0].ToString());
                dgvRow = new DataGridViewRow();
                dgvRow.CreateCells(dgvProjectList);
                dgvRow.Cells[0].Value = dr[1].ToString();
                dgvRow.Cells[1].Value = dr[2].ToString();
                dgvRow.Cells[2].Value = dr[3].ToString();
                dgvRow.Cells[3].Value = dr[4].ToString();
                dgvRow.Tag = dr[0];
                dgvProjectList.Rows.Add(dgvRow);
            }
            dr.Close();
            con.Close();
        }

        private void btn_TaskMng_Click(object sender, EventArgs e)
        {

        }

        private void dgvProjectList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvProjectList.CurrentRow.Selected = true;
        }

        private void btn_detailTask_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTaskMng frmTaskMng = new FrmTaskMng();
                DataGridViewRow item = dgvProjectList.CurrentRow;
                if (item == null || item.Tag == null || item.Cells.Count == 0 || item.Cells[0].Value == null)
                    return;
                frmTaskMng.ClsTask.ProjectID = Convert.ToInt32(item.Tag.ToString());
                frmTaskMng.ClsTask.ProjectName = item.Cells[0].Value.ToString();
                frmTaskMng.StartPosition = FormStartPosition.CenterScreen;
                frmTaskMng.ShowDialog();
                //if (frmTaskMng.ShowDialog() == DialogResult.OK)
                //{
                //    this.DialogResult = DialogResult.OK;
                //}
            }
            catch (Exception)
            { }
        }

        private void FrmProjectMng_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
    }
}
