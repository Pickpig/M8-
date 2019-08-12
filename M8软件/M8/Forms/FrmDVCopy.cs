using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace M8.Forms
{
    public partial class FrmDVCopy : Form
    {
        public int FileCount;
        public string strStatus, directoryDVName;
        public FrmDVCopy()
        {
            InitializeComponent();
        }


        private void GetFileCount(string sourceDirName)
        {
            DirectoryInfo Dir = new DirectoryInfo(sourceDirName);
            FileCount += Dir.GetFiles().Length;
            foreach (DirectoryInfo di in Dir.GetDirectories())
            {
                GetFileCount(di.FullName);
            }
        }

        public delegate void InvokeDelegate();
        public void CopyDirectory(string sourceDirName, string destDirName)
        {
            try
            {
                if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                    destDirName = destDirName + Path.DirectorySeparatorChar;
                string[] files = Directory.GetFiles(sourceDirName);
                foreach (string file in files)
                {
                    if (File.Exists(destDirName + Path.GetFileName(file)))
                        continue;
                    File.Copy(file, destDirName + Path.GetFileName(file), true);
                    File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
                    this.BeginInvoke(new InvokeDelegate(InvokeMethod));
                }
                string[] dirs = Directory.GetDirectories(sourceDirName);
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, destDirName);
                }
            }

            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\log.txt", true);
                sw.Write(ex.Message + "     " + DateTime.Now + "\r\n");
                sw.Close();
            }
        }

        void InvokeMethod()
        {
            progressBar1.Value += 1;
            label4.Text = (Convert.ToDouble(progressBar1.Value) / Convert.ToDouble(progressBar1.Maximum)).ToString("0.0%");
        }

        private System.Threading.Thread theAddFile; //创建一个线程
        public delegate void AddFile();

        public void RunAddFile()
        {
            try
            {
                CopyDirectory(label2.Text, directoryDVName);
                theAddFile.Abort();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = FileCount;
                //progressBar1.Value = 0;
                label5.Text = "正在复制 " + FileCount.ToString() + " 个文件";
                if (label4.Text == "100.0%")
                {
                    label5.Text = "复制 " + FileCount.ToString() + " 个文件完成";
                }
            }
            catch (Exception)
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label6.Text = "从" + " " + label2.Text + " " + "到" + " " + label3.Text;
                if (label2.Text == "")
                {
                    MessageBox.Show("请选择源路径！");
                    return;
                }
                FileCount = 0;
                GetFileCount(label2.Text);
                progressBar1.Minimum = 0;
                progressBar1.Maximum = FileCount;
                progressBar1.Value = 0;
                theAddFile = new System.Threading.Thread(new ThreadStart(RunAddFile));
                theAddFile.IsBackground = true;
                theAddFile.Start();
                label5.Text = "正在复制 " + FileCount.ToString() + " 个项目";
                label6.Enabled = true;
                strStatus = "视频导入完成";
            }
            catch (Exception)
            { }
        }

        private void FrmDVCopy_Load(object sender, EventArgs e)
        {
            label3.Text = directoryDVName;
        }

        private void btn_OpenSourceFile_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            label2.Text = folderBrowserDialog1.SelectedPath;
            if (label2.Text != "")
            {
                DirectoryInfo dir = new DirectoryInfo(label2.Text);
                DirectoryInfo[] f = dir.GetDirectories();
            }
            else return;
        }


    }
}
