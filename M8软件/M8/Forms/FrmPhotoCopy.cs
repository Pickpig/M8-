using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace M8.Forms
{
    public partial class FrmPhotoCopy : Form
    {
        public int FileCount,totalFileCount;
        public string directoryName;

        public FrmPhotoCopy()
        {
            InitializeComponent();
        }

        public string Info()
        {
            StringBuilder strBuilder = new StringBuilder();
            if (label2.Text == "")
            {
                strBuilder.Append("请选择源路径！");
            }
            return strBuilder.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
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

        
        private void GetFileCount(string sourceDirName)
        {
            DirectoryInfo Dir = new DirectoryInfo(sourceDirName);
            FileCount += Dir.GetFiles().Length;
            foreach (DirectoryInfo di in Dir.GetDirectories())
            {
                GetFileCount(di.FullName);
            }
        }
        private void GetFileTotalCount(string desDirName)
        {
            DirectoryInfo Dir = new DirectoryInfo(desDirName);
            totalFileCount += Dir.GetFiles().Length;
            foreach (DirectoryInfo di in Dir.GetDirectories())
            {
                GetFileTotalCount(di.FullName);
            }
        }

        public delegate void InvokeDelegate();

        /// <summary>
        /// 复制多个文件夹到一个文件夹
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        public void CopyDirectory(string sourceDirName, string destDirName)
        {
            try
            {
                if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                    destDirName = destDirName + Path.DirectorySeparatorChar;
                string[] files = Directory.GetFiles(sourceDirName);
                foreach (string file in files)
                {
                    if (File.Exists(destDirName + "DSC_00" + Path.GetFileName(file).Remove(0, 4))||
                        File.Exists(destDirName + "DSC_10" + Path.GetFileName(file).Remove(0, 4))||
                        File.Exists(destDirName + "DSC_20" + Path.GetFileName(file).Remove(0, 4)))
                    {
                        FileCount--;
                        continue;
                    }
                    totalFileCount++;
                    if (totalFileCount < 10000)
                    {
                        File.Copy(file, destDirName + "DSC_00" + Path.GetFileName(file).Remove(0, 4), true);
                        File.SetAttributes(destDirName + "DSC_00" + Path.GetFileName(file).Remove(0, 4), FileAttributes.Normal);
                    }
                    if (totalFileCount >= 10000 && totalFileCount < 19999)
                    {
                        File.Copy(file, destDirName + "DSC_10" + Path.GetFileName(file).Remove(0, 4), true);
                        File.SetAttributes(destDirName + "DSC_10" + Path.GetFileName(file).Remove(0, 4), FileAttributes.Normal);
                    }
                    else if (totalFileCount >= 20000)
                    {
                        File.Copy(file, destDirName + "DSC_20" + Path.GetFileName(file).Remove(0, 4), true);
                        File.SetAttributes(destDirName + "DSC_20" + Path.GetFileName(file).Remove(0, 4), FileAttributes.Normal);
                    }
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
            try
            {
                progressBar1.Value += 1;
                label4.Text = (Convert.ToDouble(progressBar1.Value) / Convert.ToDouble(FileCount)).ToString("0.0%");
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

        private System.Threading.Thread theAddFile; //创建一个线程
        public delegate void AddFile();

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                label6.Text = "从" + " " + label2.Text + " " + "到" + " " + label3.Text;
                string info = Info();
                if (info != "")
                {
                    MessageBox.Show(info);
                    return;
                }
                FileCount = 0;
                GetFileCount(label2.Text);
                GetFileTotalCount(label3.Text);
                sourcePath = label2.Text;
                desPath = label3.Text;
                theAddFile = new System.Threading.Thread(new ThreadStart(RunAddFile));
                theAddFile.IsBackground = true;
                theAddFile.Start();
                progressBar1.Value = 0;
                //label5.Text = "正在复制 " + FileCount.ToString() + " 个项目";

            }
            catch (Exception)
            { }
        }

        string sourcePath = "";
        string desPath = "";
        public void RunAddFile()
        {
            try
            {
                CopyDirectory(label2.Text, directoryName);
                if (FileCount != 0)
                {
                    label4.Enabled = true;
                }
                else
                {
                    MessageBox.Show("源文件夹中文件都包含在目标文件夹中");
                }
                theAddFile.Abort();
            }
            catch (Exception)
            { }
        }

        private void FrmPhotoCopy_Load(object sender, EventArgs e)
        {
            label3.Text = directoryName;
            label6.Enabled = false;
        }
    }
}
