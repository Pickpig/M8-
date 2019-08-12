using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using M8.Forms;
using M8.User;
using M8.Class;
using System.Net;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Net.Sockets;

namespace M8
{
    public partial class FrmLogin : Form
    {
        //FrmLogin login;
        public CUser curUser = new CUser();
        FrmSQLConfig frmSqlConfig;
        string port;
        string sqlIP;
        public FrmMain frmMain = new FrmMain();
        bool isLogin = false;
        ClsLogin login = new ClsLogin();
        ClsSQLMethod sqlMethod = null;
        public FrmLogin()
        {

            InitializeComponent();
            txtUserName.Text = Properties.Settings.Default.Setting;
            txtPassWord.Text = Properties.Settings.Default.Setting1;
            port = Properties.Settings.Default.SQLPort;
            //sqlIP = Properties.Settings.Default.SQLIP;    //当网址不变时候可采用此IP
            foreach(string ip in GetLocalIpv4())
            {
                sqlIP = ip;
            }
            if (lbl_Error.Text != "")
            {
                lbl_Error.Visible = true;
            }
            //this.txt_SQLIP.Text = Properties.Settings.Default.SQLIP;
            //this.txt_SQLPort.Text = Properties.Settings.Default.SQLPort;
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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string Login(string sqlIP, int sqlPort,  string userName, string passWord)
        {
            string info = string.Empty;
            try
            {
                info = login.Login(sqlIP, sqlPort,  userName, passWord);
                curUser = login.curUser;
                sqlMethod = login.sqlMethod;
                frmMain.curUser = curUser;
                //taskCfg.curUser = curUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return info;
        }
        [DllImport("SURF_dll.dll", EntryPoint = "?SURF_dll_main@@YAHPAD000@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SURF_dll_main(char[] imgreadpath1, char[] imgreadpath2, char[] imgreadpath3, char[] imgsavepath);
        private void btn_Login_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string passWord = txtPassWord.Text.Trim();

            //string imgreadpath1 = "D:\\飞行数据\\test4\\shuru\\gaoqingxiangji\\DSC_000909.jpg";
            //string imgreadpath2 = "D:\\飞行数据\\test4\\shuru\\gaoqingxiangji\\DSC_000910.jpg";
            //string imgreadpath3 = "D:\\飞行数据\\test4\\shuru\\gaoqingxiangji\\DSC_000911.jpg";
            //string imgsavepath = "E:\\公路局数据\\5\\6.jpg";

            //char[] aa = imgreadpath1.ToCharArray();
            //char[] bb = imgreadpath2.ToCharArray();
            //char[] cc = imgreadpath3.ToCharArray();
            //char[] dd = imgsavepath.ToCharArray();

            //SURF_dll_main(aa, bb, cc, dd);

            string info;
            try
            {
                if (string.IsNullOrEmpty(sqlIP) || string.IsNullOrEmpty(port))
                {
                    MessageBox.Show("登陆前请先进行连接设置！", "登陆");
                    return;
                }
                IPAddress sqlip;
                if (!IPAddress.TryParse(sqlIP, out sqlip))
                {
                    MessageBox.Show("数据库IP地址输入有误！", "连接设置");
                    return;
                }
                int port1;
                if (!int.TryParse(port, out port1))
                {
                    MessageBox.Show("数据库端口号输入有误！", "连接设置");
                    return;
                }
                info = Login(sqlIP, Convert.ToInt32(port), userName, passWord);
                //info = "登陆成功";
                lbl_Error.Text = info;


                if (info == "登陆成功")
                {
                    Properties.Settings.Default.Setting = userName;
                    Properties.Settings.Default.Setting1 = passWord;
                    Properties.Settings.Default.SQLIP = sqlIP;
                    Properties.Settings.Default.SQLPort = port;
                    Properties.Settings.Default.Save();
                    isLogin = true;
                    frmMain.sqlConn = this.sqlMethod;
                    this.Close();
                    //mainWindow.control = control;
                    //frmMain.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登陆失败！\r\n" + ex.Message, "登陆");
            }
            if (isLogin == true)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            frmSqlConfig = new FrmSQLConfig();
            frmSqlConfig.Owner = this;
            if(frmSqlConfig.ShowDialog()==DialogResult.Yes)
            //if (frmSqlConfig.DialogResult==DialogResult.Yes)
            {
                sqlIP = frmSqlConfig.sqlip;
                port = frmSqlConfig.sqlport;
            }
        }
    }
}
