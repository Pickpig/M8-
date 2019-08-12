using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M8.Forms
{
    public partial class FrmProjectAnalyse : Form
    {
        [DllImport("CrackD.dll", EntryPoint = "ShowDialog", CharSet = CharSet.Auto)]
        public static extern IntPtr showdialog();
        [DllImport("CrackF.dll", EntryPoint = "ShowDialog", CharSet = CharSet.Auto)]
        public static extern IntPtr showdialogF();
        [DllImport("MFCLibrary4-9cheliang.dll", EntryPoint = "ShowDialog", CharSet = CharSet.Auto)]
        public static extern IntPtr showdialogC();
        [DllImport("Zhibei.dll", EntryPoint = "ShowDialog", CharSet = CharSet.Auto)]
        public static extern IntPtr showdialogZ();

        public FrmProjectAnalyse()
        {
            InitializeComponent();
        }

        private void btn_CrackD_Click(object sender, EventArgs e)
        {
            showdialog();
        }

        private void btn_CrackF_Click(object sender, EventArgs e)
        {
            showdialogF();
        }

        private void btn_cheliang_Click(object sender, EventArgs e)
        {
            showdialogC();
        }

        private void btn_zhibei_Click(object sender, EventArgs e)
        {
            showdialogZ();
        }

        private void FrmProjectAnalyse_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
    }
}
