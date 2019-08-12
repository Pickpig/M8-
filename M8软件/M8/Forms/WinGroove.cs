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

namespace M8.Forms
{
    public partial class WinGroove : Form
    {
        private static bool isStop = false;
        public WinGroove()
        {
            InitializeComponent();
            try
            {
                Bitmap bitMap = new Bitmap(@"../../Image/647.jpg");
                ima_result.Image = ResizeImage(bitMap, ima_result);
                this.FormClosed += WinGroove_FormClosed;
            }
            catch (Exception)
            { }
        }

        void WinGroove_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                isStop = true;
            }
            catch (Exception)
            { }
        }
        /// <summary>
        /// 开始车辙分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            isStop = false;
            string des = tbx_outputdir.Text;
            try
            {
                if (!File.Exists(textBox1.Text))
                {
                    MessageBox.Show("请输入正确的“输入文件”文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!File.Exists(textBox2.Text))
                {
                    MessageBox.Show("请输入正确的“输入数据”文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (tbx_outputdir.Text == string.Empty)
                {
                    MessageBox.Show("未设置“输出目录”", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                File.Copy(@"../../outp/groove/预处理结果.xlsx", des + "\\预处理结果.xlsx",true);
                if (!Directory.Exists(des + "\\输出结果\\output_img"))
                    //Directory.Delete(des + "\\输出结果", true);
                    Directory.CreateDirectory(des + "\\输出结果\\output_img");
                File.Copy(@"../../outp/groove/输出结果/Output_Data.xlsx", des + "\\输出结果\\Output_Data.xlsx", true);
            }
            catch (Exception)
            {
                MessageBox.Show("请检查路径是否合法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            button5.Enabled = false;
            button4.Enabled = true;
            Task t = new Task(() =>
            {
                Bitmap bitMap = null;
                int index = GetStartIndex (des + "\\输出结果\\output_img");
                for (int i = index; i < 100; i++)
                {
                    try
                    {
                        if (isStop)
                        {
                            isStop = false;
                            break;
                        }
                        System.Threading.Thread.Sleep(500);
                        bitMap = new Bitmap(@"../../outp/groove/输出结果/output_img/chezhedata" + i.ToString() + ".jpg");
                        ima_result.Image = ResizeImage(bitMap, ima_result);
                        bitMap.Save(des + "\\输出结果\\output_img\\chezhedata" + i.ToString() + ".jpg");
                    }
                    catch (Exception)
                    { continue; }
                }
            });
            t.ContinueWith((r) =>
                {
                    this.Cursor = Cursors.Arrow;
                    button5.Enabled = true;
                    button4.Enabled = false;
                },scheduler);
            t.Start();
                //ima_result.Width = bitMap.Width;
                //ima_result.Height = bitMap.Height;
                //ima_result
                
            //ima_result.Image = bitMap;
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
        private int GetStartIndex(string dir)
        {
            int index = 1;
            int result;
            DirectoryInfo di = new DirectoryInfo(dir);
            foreach (FileInfo fi in di.GetFiles ())
            {
              if ( int.TryParse ( fi.Name.Replace ("chezhedata",string.Empty).Replace(".jpg",string.Empty),out result))
              {
                  index =Math.Max(index, result);
              }
            }
            return index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.log|*.log";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog ()==DialogResult.OK )
            {
                textBox1.Text = dialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.xlsx|*.xlsx";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择输出结果所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                tbx_outputdir.Text = dialog.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
