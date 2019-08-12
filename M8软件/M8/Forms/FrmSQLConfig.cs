using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;



namespace M8.Forms
{
    public partial class FrmSQLConfig : Form
    {
        public FrmSQLConfig()
        {
            InitializeComponent();
        }
        public  string sqlport;
        public  string sqlip;

        private void btn_OK_Click_Click(object sender, EventArgs e)
        {
            IPAddress sqlIP;
            if (!IPAddress.TryParse(txt_SQLIP.Text, out sqlIP))
            {
                MessageBox.Show("数据库IP地址输入有误！", "连接设置");
                return;
            }
            int port;
            if (!int.TryParse(txt_SQLPort.Text, out port))
            {
                MessageBox.Show("数据库端口号输入有误！", "连接设置");
                return;
            }
            sqlport = txt_SQLPort.Text;
            sqlip = txt_SQLIP.Text;
            this.DialogResult = DialogResult.Yes;
        }

        private void btn_Cancel_Click_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void FrmSQLConfig_Load(object sender, EventArgs e)
        {
            this.txt_SQLIP.Text = Properties.Settings.Default.SQLIP;
            this.txt_SQLPort.Text = Properties.Settings.Default.SQLPort;
        }
    }
}
