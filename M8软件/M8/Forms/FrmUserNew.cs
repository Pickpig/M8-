using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using M8.User;
using System.Xml;
using M8.Class;

namespace M8.Forms
{
    public partial class FrmUserNew : Form
    {
        public FrmUserNew()
        {
            InitializeComponent();
            User = new CUser();
        }
        public CUser curUser = new CUser();

        private XmlDocument _xmlDoc;
        public XmlDocument XmlDoc
        {
            get { return _xmlDoc; }
            set { _xmlDoc = value; }
        }

        private CUser _user;
        public CUser User
        {
            get { return _user; }
            set { _user = value; }
        }

        private void FrmUserNew_Load(object sender, EventArgs e)
        {
            if (User.UserName != null)
            {
                txt_userName.Enabled = false;
                txt_password.Enabled = false;
                txt_passwordAgain.Enabled = false;
                txt_userName.Text = User.UserName;
                txt_password.Text = _user.PassWord;
                txt_passwordAgain.Text = _user.PassWord;
                txt_Name.Text = _user.TrueName;
                txt_contact.Text = _user.Contact;
                txt_description.Text = _user.Description;
                comb_authority.Enabled = false;
                comb_authority.Text = _user.Authority.ToString();
            }
        }
        public string Info()
        {
            DataTable dt;
            dt = ClsSQLMethod.SearchUser();
            StringBuilder strBuilder = new StringBuilder();
            if (_user == null)
            {
                CUser user;
                foreach (DataRow row in dt.Rows)
                {
                    user = curUser;
                    if (txt_Name.Text == row[1].ToString())
                    {
                        strBuilder.Append("该用户已存在！");
                    }     
                }
            }
            if (comb_authority.Text!= "管理员"&&comb_authority.Text!="普通用户")
            {
                strBuilder.Append("未设置用户权限！");
            }
            if (txt_Name.Text == "")
            {
                strBuilder.AppendLine("用户名不能为空！");
            }
            if (txt_password.Text == "")
            {
                strBuilder.AppendLine("密码不能为空！");
            }
            if (6 > txt_password.Text.Length || txt_password.Text.Length >= 18)
            {
                strBuilder.Append("密码长度应介于6-18位！");
            }
            if (txt_password.Text != txt_passwordAgain.Text)
            {
                strBuilder.Append("两次输入密码不一致！");
            }
            return strBuilder.ToString();
        }

        private void Save()
        {
            try
            {
                CUser user = new CUser();
                user.UserName = txt_userName.Text.Trim();
                user.Description = txt_description.Text.Trim();
                user.Contact = txt_contact.Text.Trim();
                user.TrueName = txt_Name.Text.Trim();
                user.PassWord = txt_password.Text.Trim();
                user.Authority = (EnumUserAutho)(Enum.Parse(typeof(EnumUserAutho), comb_authority.Text.Trim()));
                user.UserID = ClsSQLMethod.AddUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show("用户注册失败\r\n" + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (_user.UserName != null)
            {
                try
                {
                    #region 修改用户
                    string info = Info();
                    if (info != "")
                    {
                        MessageBox.Show(info);
                        return;
                    }
                    else
                    {
                        User.Contact = txt_contact.Text.Trim();
                        User.TrueName = txt_Name.Text.Trim();
                        User.UserName = txt_userName.Text.Trim();
                        User.Description = txt_description.Text.Trim();
                        User.PassWord = txt_password.Text.Trim();
                        User.Authority = (EnumUserAutho)(Enum.Parse(typeof(EnumUserAutho), comb_authority.Text.Trim()));
                        User.UserID = ClsSQLMethod.ModifyUser(User);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("用户修改失败\r\n" + ex.Message);
                }
                    #endregion
            }
            else
            {
                #region 新建用户
                try
                {
                    string info = Info();
                    if (info != "")
                    {
                        MessageBox.Show(info);
                    }
                    else
                    {
                        Save();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败！" + "/r/n" + ex.Message);
                }
                #endregion
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
