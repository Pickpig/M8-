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
    public partial class FrmSystemConfig : Form
    {
       public FrmMain mainForm = null;
        public FrmSystemConfig()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.databaseIP = databaseIP.Text;
            mainForm.txtDatabaseName = txtDatabaseName.Text;
            mainForm.txtDatabasePassword = txtDatabasePassword.Text;
            mainForm.txtDatabaseUserName = txtDatabaseUserName.Text;
        }
    }
}
