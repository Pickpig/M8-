using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M8.Forms
{
    public partial class WinTraffic : Form
    {
        private static bool isStop = false;
        public WinTraffic()
        {
            InitializeComponent();
            this.FormClosed += WinTraffic_FormClosed;
        }

        void WinTraffic_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                isStop = true;
            }
            catch (Exception)
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.txt|*.txt";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                textBox2.Text = dialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.txt|*.txt";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbx_outputpath.Text = dialog.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                textBox4.Text = dialog.SelectedPath;
            }
        }
        /// <summary>
        /// 开始分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            isStop = false;
            string dex = textBox4.Text;
            if (!File.Exists (textBox1.Text))
            {
                MessageBox.Show("请选择正确的输入文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(tbx_outputpath.Text))
            {
                MessageBox.Show("请选择正确的输出文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!Directory.Exists (textBox2.Text))
            {
                MessageBox.Show("请选择正确的输入图像目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox4.Text == string.Empty )
            {
                MessageBox.Show("未设置输出图像目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (!Directory.Exists(dex ))
                    Directory.CreateDirectory(dex);
                File.Copy("../../outp/traffic/write.txt", tbx_outputpath.Text, true);
            }
            catch (Exception)
            {
                MessageBox.Show("请检查输出路径是否合法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task t = new Task(() =>
            {
                DirectoryInfo di = new DirectoryInfo("../../outp/traffic/cheliang_shuchu");
                Bitmap bitMap;
                foreach (FileInfo fi in di.GetFiles())
                {
                    System.Threading.Thread.Sleep(2000);
                    try
                    {
                        if (isStop)
                        {
                            isStop = false;
                            break;
                        }
                        if (fi.Extension == ".jpg")
                        {
                            bitMap = new Bitmap(fi.FullName);
                            pictureBox1.Image = ResizeImage(bitMap, pictureBox1);
                            bitMap.Save(dex + "\\" + fi.Name);
                        }
                    }
                    catch (Exception)
                    { continue; }
                }
            });
            t.ContinueWith((r) =>
                {
                    this.Cursor = Cursors.Arrow;
                }, scheduler);
            t.Start();
        }
        private Bitmap ResizeImage(Bitmap bmp, PictureBox picBox)
        {
            float xRate = (float)bmp.Width / picBox.Size.Width;
            float yRate = (float)bmp.Height / picBox.Size.Height;
            if (xRate <= 1 && yRate <= 1)
            {
                return bmp;
            }
            else
            {
                float tRate = (xRate >= yRate) ? xRate : yRate;
                Graphics g = null;
                try
                {
                    int newW = (int)(bmp.Width / tRate);
                    int newH = (int)(bmp.Height / tRate);
                    Bitmap b = new Bitmap(newW, newH);
                    g = Graphics.FromImage(b);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                    g.Dispose();
                    //bmp.Dispose();
                    return b;
                }
                catch
                {
                    //bmp.Dispose();
                    return null;
                }
                finally
                {
                    if (null != g)
                    {
                        g.Dispose();
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                isStop = true;
            }
            catch (Exception)
            { }
        }
    }
}
