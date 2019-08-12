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
using M8.User;
using System.Collections.ObjectModel;

namespace M8.Forms
{
    public partial class FrmUserMng : Form
    {
        ObservableCollection<CUser> users = new ObservableCollection<CUser>();
        public CUser curUser;
        
        public FrmUserMng()
        {
            InitializeComponent();
        }
        
        private void FrmUserMng_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
            dataGridView.CurrentRow.Selected = true;
        }
        private void LoadUserInfo()
        {
            DataTable dt;
            dt = ClsSQLMethod.SearchUser();
            
            if (curUser.Authority == EnumUserAutho.管理员)
            {
                dataGridView.Rows.Clear();
                DataGridViewRow dgvRow;
                foreach (DataRow row in dt.Rows)
                {
                    dgvRow = new DataGridViewRow();
                    dgvRow.CreateCells(dataGridView);
                    dgvRow.Cells[0].Value = row[1].ToString();
                    dgvRow.Cells[1].Value = row[2].ToString();
                    dgvRow.Cells[2].Value = (EnumUserAutho)(Enum.Parse(typeof(EnumUserAutho), row[3].ToString()));
                    dgvRow.Cells[3].Value = row[4].ToString();
                    dgvRow.Cells[4].Value = row[5].ToString();
                    dgvRow.Cells[5].Value = row[6].ToString();
                    dgvRow.Tag = row[0];
                    dataGridView.Rows.Add(dgvRow);
                }

            }
            if (curUser.Authority == EnumUserAutho.普通用户)
            {
                btn_newUser.Enabled = false;
                btn_modifyUser.Enabled = false;
                btn_deleteUser.Enabled = false;
                users.Clear();
                CUser user;
                foreach (DataRow row in dt.Rows)
                {
                    user = new CUser();
                    user.UserID = curUser.UserID;
                    if (user.UserID == int.Parse(row[0].ToString()))
                    {
                        user.UserName = row[1].ToString();
                        user.PassWord = row[2].ToString();
                        user.Authority = (EnumUserAutho)(Enum.Parse(typeof(EnumUserAutho), row[3].ToString()));
                        user.Description = row[4].ToString();
                        user.Contact = row[5].ToString();
                        user.TrueName = row[6].ToString();
                        users.Add(user);
                    }
                }
            }
        }

        private void btn_newUser_Click(object sender, EventArgs e)
        {
            FrmUserNew frmUserNew = new FrmUserNew();
            frmUserNew.StartPosition = FormStartPosition.CenterScreen;
            frmUserNew.ShowDialog();
            LoadUserInfo();
        }

        private void btn_modifyUser_Click(object sender, EventArgs e)
        {
            FrmUserNew frmUserNew = new FrmUserNew();
            DataGridViewRow item = dataGridView.CurrentRow;
            if (item == null)
                return;
            frmUserNew.User.UserName = item.Cells[0].Value.ToString();
            frmUserNew.User.PassWord = item.Cells[1].Value.ToString();
            frmUserNew.User.Authority = (EnumUserAutho)(Enum.Parse(typeof(EnumUserAutho), item.Cells[2].Value.ToString()));
            frmUserNew.User.Description = item.Cells[5].Value.ToString();
            frmUserNew.User.Contact = item.Cells[4].Value.ToString();
            frmUserNew.User.TrueName = item.Cells[3].Value.ToString();
            frmUserNew.User.UserID = (int)item.Tag;
            frmUserNew.StartPosition = FormStartPosition.CenterScreen;
            frmUserNew.ShowDialog();
            LoadUserInfo();
        }

        private void btn_deleteUser_Click(object sender, EventArgs e)
        {
            DataTable dt;
            FrmUserNew frmUserNew = new FrmUserNew();
            dt = ClsSQLMethod.SearchUser();
            DataGridViewRow item = dataGridView.CurrentRow;
            if (item == null || item.Tag==null)
                return;
            if (curUser.Authority != EnumUserAutho.管理员)
            {
                MessageBox.Show("你没有此权限！");
                return;
            }
            frmUserNew.User.UserID = (int)item.Tag;
            try
            {
                if (MessageBox.Show("确认删除用户：" + frmUserNew.User.UserID + "?", "删除用户", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (frmUserNew.User.UserID == int.Parse(row[0].ToString()))
                        {
                            break;
                        }
                    }
                    ClsSQLMethod.DeleteUser(frmUserNew.User);
                    LoadUserInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败！");
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.CurrentRow.Selected = true;
        }
    }
}
