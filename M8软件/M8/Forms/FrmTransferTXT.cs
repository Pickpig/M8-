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
using System.Collections;

namespace M8.Forms
{
    public partial class FrmTransferTXT : Form
    {
        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string strStatus;

        public FrmTransferTXT()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入源目标路径");
                return;
            }
            string[] stringLines = File.ReadAllLines(textBox1.Text, Encoding.GetEncoding("GB2312"));
            StreamWriter sw = new StreamWriter(fileName);
            object sx = string.Empty;
            object sy = string.Empty;
            try
            {
                foreach (string s in stringLines)
                {
                    //if ((s.Contains("\t照片编号") || s.Contains("\tDSC")))
                    //{
                    //    string[] data = s.Split('\t');
                    //    //string X = data[4];
                    //    //string Y = data[5];
                    //    string X = data[0];
                    //    string Y = data[1];
                    //    string photoNO = data[2];
                    //    decimal dx = 0.0M;
                    //    if (data.Length < 25)
                    //        continue;
                    //    if (decimal.TryParse(data[24], out dx))
                    //    {
                    //        sx = Convert.ToDecimal(data[24]) * 180 / (decimal)Math.PI;
                    //        sy = Convert.ToDecimal(data[25]) * 180 / (decimal)Math.PI;
                    //    }
                    //    else
                    //    {
                    //        sx = data[24];
                    //        sy = data[25];
                    //    }
                    //    //sw.Write(X);
                    //    //sw.Write("\t");
                    //    //sw.Write(Y);
                    //    //sw.Write("\t");
                    //    sw.Write(photoNO);
                    //    sw.Write(" ");
                    //    sw.Write(sx);
                    //    sw.Write(" ");
                    //    sw.Write(sy);
                    //    //sw.Write(" ");
                    //    sw.WriteLine();
                    //}
                    sw.WriteLine(s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sw.Close();
            sw.Dispose();
            MessageBox.Show("数据转化完成！");
            strStatus = "TXT数据导入完成";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
        }
        private void FrmTransferTXT_Load(object sender, EventArgs e)
        {
            textBox2.Text = FileName;
        }
    }
}