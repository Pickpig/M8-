﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using M8.Class;

namespace M8.Forms
{
    public partial class FrmCode2 : Form
    {
        public ClsTask clsTask;
        public FrmCode2()
        {
            InitializeComponent();
            clsTask = new ClsTask();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Bitmap image = Create_ImgCode(textBox1.Text, 10);
        //    //保存图片
        //    SaveImg(currentPath, image);

        //    //打开文件夹
        //    Open_File(currentPath);
        //}

        #region 二维码生成

        //程序路径
        //readonly string currentPath = Application.StartupPath + @"\BarCode_Images";
        readonly string currentPath = "E:\\123" + @"\BarCode_Images";
        string savePath;
        string defaultPath="";
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="strPath">保存路径</param>
        /// <param name="img">图片</param>
        //public void SaveImg(string strPath, Bitmap img)
        //{
        //    //保存图片到目录
        //    if (Directory.Exists(strPath))
        //    {
        //        //文件名称
        //        string guid = Guid.NewGuid().ToString().Replace("-", "") + ".png";
        //        img.Save(strPath + "/" + guid, System.Drawing.Imaging.ImageFormat.Png);
        //    }
        //    else
        //    {
        //        //当前目录不存在，则创建
        //        Directory.CreateDirectory(strPath);
        //    }
        //}

        public void SaveImg(string strPath, Bitmap img)
        {
            try
            {
                string guid = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                img.Save(strPath + "/" + guid, System.Drawing.Imaging.ImageFormat.Png);
                //img.Save(@"D:111.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception)
            { }
        }


        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="codeNumber">要生成二维码的字符串</param>     
        /// <param name="size">大小尺寸</param>
        /// <returns>二维码图片</returns>
        public Bitmap Create_ImgCode(string codeNumber, int size)
        {
            //创建二维码生成类
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber);
            return image;
        }
        /// <summary>
        /// /打开指定目录
        /// </summary>
        /// <param name="path"></param>
        public void Open_File(string path)
        {
            System.Diagnostics.Process.Start("explorer.exe", path);
        }
        /// <summary>
        /// 删除目录下所有文件
        /// </summary>
        /// <param name="aimPath">路径</param>
        public void DeleteDir(string aimPath)
        {
            try
            {
                //目录是否存在
                if (Directory.Exists(aimPath))
                {
                    // 检查目标目录是否以目录分割字符结束如果不是则添加之
                    if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                        aimPath += Path.DirectorySeparatorChar;
                    // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                    // 如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法
                    string[] fileList = Directory.GetFiles(aimPath);
                    //string[] fileList = Directory.GetFileSystemEntries(aimPath);
                    // 遍历所有的文件和目录
                    foreach (string file in fileList)
                    {
                        // 先当作目录处理如果存在这个目录就递归Delete该目录下面的文件
                        if (Directory.Exists(file))
                        {
                            DeleteDir(aimPath + Path.GetFileName(file));
                        }
                        // 否则直接Delete文件
                        else
                        {
                            File.Delete(aimPath + Path.GetFileName(file));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        private void button1_Click_1(object sender, EventArgs e)
        {



        }

        private void openFileDialog_Click(object sender, EventArgs e)
        {
            if (defaultPath == "") 
            {


                FolderBrowserDialog folder = new FolderBrowserDialog();
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    //folder.SelectedPath = savePath;
                    defaultPath = folder.SelectedPath;
                    Application.UserAppDataRegistry.SetValue("path", defaultPath);
                    savePath = Application.UserAppDataRegistry.GetValue("path") as string;
                }
            }
            
        }

        private void generateCode2_Click(object sender, EventArgs e)
        {
            textBox1.Text = clsTask.TaskID.ToString();
            Bitmap image = Create_ImgCode(textBox1.Text, 10);
            //保存图片
            //SaveImg(textBox1.Text, image);
            SaveImg(savePath, image);
            //打开文件夹
            Open_File(savePath);
            //this.ShowDialog();
        }
    }
}
