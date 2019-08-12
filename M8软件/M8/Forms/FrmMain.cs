using M8.Class;
using M8.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using M8.User;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;


namespace M8
{

    public delegate void InsertProjectDataHandler();


    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class FrmMain : Form
    {

        public int index1 = 0;
        public string databaseIP, txtDatabaseName, txtDatabaseUserName, txtDatabasePassword, FileName, directoryName, directoryDVName, Size = null;
        public ClsSQLMethod sqlConn = new ClsSQLMethod();
        public string sqlIP = null;
        public int sqlPort = 0;
        public int interval = 1;
        public CUser curUser = null;
        ClsSQLMethod sqlMethod = new ClsSQLMethod();
        public string strProjectName, strBuilder, description, strTaskName, strDisk, strStartPosition, strDescription, strPhotoPath, strDVpath, strDataPath;
        public int projectID, taskID;
        public bool boolChecked;
        List<ClsCPoint> lstPoint = new List<ClsCPoint>();
        List<ClsCPoint> lstPoint800 = new List<ClsCPoint>();//先取800个点
        double circleL = 0.000010919615;//中心点画圆的半径，显示在圆内的点
        int count = 0;

        [DllImport("CrackD.dll", EntryPoint = "ShowDialog", CharSet = CharSet.Auto)]
        public static extern IntPtr showdialog();
        [DllImport("CrackF.dll", EntryPoint = "ShowDialog", CharSet = CharSet.Auto)]
        public static extern IntPtr showdialogF();
        [DllImport("Cheliang.dll", EntryPoint = "ShowDialog", CharSet = CharSet.Auto)]
        public static extern IntPtr showdialogC();
        [DllImport("Zhibei.dll", EntryPoint = "ShowDialog", CharSet = CharSet.Auto)]
        public static extern IntPtr showdialogZ();

        public FrmMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            string str_url = System.Environment.CurrentDirectory.Replace("bin\\Debug", "index2.html");
            Uri url = new Uri(str_url);
            webBrowser1.Url = url;
            webBrowser1.ObjectForScripting = this;

            tabControl1.TabPages.Remove(tabPage3);   //暂时将第三页隐藏了
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            refreshProjectTree();
            数据导入ToolStripMenuItem.Enabled = false;
            数据导出ToolStripMenuItem.Enabled = false;
            //数据处理ToolStripMenuItem.Enabled = false;
        }

        public void refreshProjectTree()
        {
            try
            {
                treeView1.Nodes.Clear();
                treeView1.ShowLines = true;
                treeView1.ImageList = imageList1;
                DataTable dt1, dt2;
                dt1 = ClsSQLMethod.SearchProject();
                dt2 = ClsSQLMethod.SearchTask();
                TreeNode node1 = treeView1.Nodes.Add("A", "项目列表", 1, 2);
                foreach (DataRow row in dt1.Rows)
                {
                    TreeNode newNode1 = new TreeNode("项目：" + row[1].ToString(), 3, 4);
                    newNode1.Nodes.Add("A", "测试人员：" + row[3].ToString(), 7, 8);
                    newNode1.Nodes.Add("A", "测试时间：" + row[2].ToString(), 9, 10);
                    newNode1.Nodes.Add("A", "备注：" + row[4].ToString(), 11, 12);
                    foreach (DataRow row1 in dt2.Rows)
                    {
                        if (row[0].ToString() == row1[1].ToString())
                        {
                            TreeNode node2 = newNode1.Nodes.Add("A", "任务：" + row1[2].ToString(), 1, 2);
                            //node2.Nodes.Add("A", "任务名称：" + row1[2].ToString(), 3, 4);
                            node2.Nodes.Add("A", "物理硬盘：" + row1[3].ToString(), 5, 6);
                            node2.Nodes.Add("A", "起始位置：" + row1[4].ToString(), 7, 8);
                            node2.Nodes.Add("A", "备注：" + row1[5].ToString(), 9, 10);
                        }
                    }
                    node1.Nodes.Add(newNode1);
                }
                node1.Expand();
            }
            
            catch (Exception)
            { }
        }

        void SqlDB_SqlConnectionStateChange(StateChangeEventArgs e)  //监控与数据库连接状态
        {
            if (e.CurrentState == ConnectionState.Open)
                tssSqlConn.Text = "与数据库连接正常";
            else if (e.CurrentState == ConnectionState.Broken || e.CurrentState == ConnectionState.Closed)
                tssSqlConn.Text = "与数据库连接断开，请重新连接！";
            //throw new Exception("The method or operation is not implemented.");
        }

        private void 预处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 硬盘管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //private void 载入数据ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog openDialog = new OpenFileDialog();
        //    //string strFileName;

        //    openDialog.InitialDirectory = @"e:\";
        //    openDialog.Filter = "文本文件|*.*";
        //    openDialog.Multiselect = true;


        //    if (openDialog.ShowDialog() == DialogResult.OK)
        //    {

        //    }
        //}

        private void 新建项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProjectNew frmProject = new FrmProjectNew();
                frmProject.sqlMethod = this.sqlConn;
                frmProject.Tag = "New";
                frmProject.StartPosition = FormStartPosition.CenterScreen;
                if (frmProject.ShowDialog() == DialogResult.OK)
                {
                    refreshProjectTree();
                }
            }
            catch (Exception)
            { }
        }

        private void 系统配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSystemConfig frmSysConfig = new FrmSystemConfig();
            frmSysConfig.ShowDialog();
        }

        private void 修改项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProjectMng frmProjectModify = new FrmProjectMng();
                frmProjectModify.sqlMethod = this.sqlConn;
                frmProjectModify.Tag = "Modify";
                frmProjectModify.StartPosition = FormStartPosition.CenterScreen;
                //frmProjectModify.ShowDialog();
                if (frmProjectModify.ShowDialog() == DialogResult.Yes)
                {
                    refreshProjectTree();
                }
            }
            catch (Exception)
            { }
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmUserMng frmUserMng = new FrmUserMng();
                frmUserMng.curUser = curUser;
                frmUserMng.StartPosition = FormStartPosition.CenterScreen;
                frmUserMng.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 新建项目ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProjectNew frmProject = new FrmProjectNew();
                frmProject.sqlMethod = this.sqlConn;
                frmProject.Tag = "New";
                frmProject.StartPosition = FormStartPosition.CenterScreen;
                if (frmProject.ShowDialog() == DialogResult.OK)
                {
                    refreshProjectTree();
                }
            }
            catch (Exception)
            { }
        }

        private void 修改项目ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProjectModify frmProjectModify = new FrmProjectModify();
                frmProjectModify.sqlMethod = this.sqlConn;
                frmProjectModify.Tag = "Modify";
                frmProjectModify.StartPosition = FormStartPosition.CenterScreen;
                frmProjectModify.Project.strProjectName = strProjectName;
                frmProjectModify.Project.strProjectBuilder = strBuilder;
                frmProjectModify.Project.description = description;
                frmProjectModify.Project.intProjectID = projectID;
                if (frmProjectModify.ShowDialog() == DialogResult.OK)
                {
                    refreshProjectTree();
                }
            }
            
            catch (Exception)
            { }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTaskNew frmTaskNew = new FrmTaskNew();
                frmTaskNew.StartPosition = FormStartPosition.CenterScreen;
                frmTaskNew.ClsTask.ProjectID = projectID;
                frmTaskNew.ShowDialog();
                //if (frmTaskNew.ShowDialog() == DialogResult.OK)
                //{
                //    refreshProjectTree();
                //}
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTaskNew frmTaskNew = new FrmTaskNew();
                frmTaskNew.StartPosition = FormStartPosition.CenterScreen;
                frmTaskNew.ClsTask.ProjectID = projectID;
                frmTaskNew.ClsTask.strTaskName = strTaskName;
                frmTaskNew.ClsTask.Disk = strDisk;
                frmTaskNew.ClsTask.StartPosition = strStartPosition;
                frmTaskNew.ClsTask.Description = strDescription;
                //frmTaskNew.ShowDialog();
                frmTaskNew.ClsTask.TaskID = sqlMethod.ModifyTask(frmTaskNew.ClsTask);
                frmTaskNew.ShowDialog();
                //if (frmTaskNew.ShowDialog() == DialogResult.OK)
                //{
                //    refreshProjectTree();
                //}
            }
            
            catch (Exception)
            { }
        }

        private void 生成硬盘二维码ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmCode2 frmCode2 = new FrmCode2();
            frmCode2.clsTask.TaskID = taskID;
            frmCode2.ShowDialog();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                treeView1.SelectedNode = (TreeNode)e.Node;
                if (treeView1.SelectedNode.Level == 1)
                {
                    strProjectName = e.Node.Text.Remove(0, 3);
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (treeView1.SelectedNode.Level == 1)
                    {
                        if ((e.Node.Text.Remove(3) != "项目："))
                            return;
                        strProjectName = e.Node.Text.Remove(0, 3);
                        DataTable dt = sqlMethod.SearchSpecificProject(strProjectName);
                        foreach (DataRow row in dt.Rows)
                        {
                            strProjectName = row[1].ToString();
                            projectID = Convert.ToInt32(row[0].ToString());
                            strBuilder = row[3].ToString();
                            description = row[4].ToString();
                        }
                    }
                    if (treeView1.SelectedNode.Level == 1)
                    {
                        contextMenuStrip1.Show(treeView1, treeView1.PointToClient(Control.MousePosition));
                    }
                    else if (treeView1.SelectedNode.Level == 2)
                    {
                        if (e.Node.Text.Contains("任务"))
                        {
                            contextMenuStrip2.Show(treeView1, treeView1.PointToClient(Control.MousePosition));
                            strProjectName = e.Node.Parent.Text.Remove(0, 3);
                            strTaskName = e.Node.FirstNode.Parent.Text.Remove(0, 3);
                            DataTable dt = sqlMethod.SearchSpecificTask(strTaskName);
                            foreach (DataRow row in dt.Rows)
                            {
                                projectID = Convert.ToInt32(row[1].ToString());
                                strTaskName = row[2].ToString();
                                strDisk = row[3].ToString();
                                strStartPosition = row[4].ToString();
                                description = row[5].ToString();
                                taskID = (int)row[0];
                            }
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void 详细信息toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = strProjectName;
                textBox2.Text = strTaskName;
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                数据导入ToolStripMenuItem.Enabled = true;
                数据导出ToolStripMenuItem.Enabled = true;
                数据处理ToolStripMenuItem.Enabled = true;
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择数据存储路径";
                string path;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                    directoryName = path + @"\shuru\gaoqingxiangji";
                    directoryDVName = path + @"\shuru\guangdiandiaocang\kejianguang";
                    if (!Directory.Exists(path + @"\\shuru\\tongbuban"))   //只判断了一个
                    {
                        Directory.CreateDirectory(path + @"\shuru\tongbuban");
                        Directory.CreateDirectory(path + @"\shuru\zijiayi");
                        Directory.CreateDirectory(path + @"\shuru\jiguangleida");
                        Directory.CreateDirectory(path + @"\shuru\gaoqingxiangji");
                        Directory.CreateDirectory(path + @"\shuru\guangdiandiaocang\kejianguang");
                        File.Create(path + @"\shuru\tongbuban\tongbuban.txt").Close ();
                    }
                    if (Directory.GetFiles(directoryName).Length == 0)
                    {
                        label14.Text = "高清相片未导入";
                    }
                    else
                    {
                        label14.Text = "高清相片已导入";
                    }

                    FileInfo fi = new FileInfo(path + @"\\shuru\\tongbuban\\tongbuban.txt");
                    Size = fi.Length.ToString();
                    if (Size == "0")
                    {
                        label12.Text = "TXT数据未导入";
                        FileName = path + @"\shuru\tongbuban\tongbuban.txt";
                    }
                    else
                    {
                        label12.Text = "TXT数据已导入";
                        FileName = path + @"\shuru\tongbuban\tongbuban.txt";
                        button1_Click(null, null);
                        tabControl1.SelectedTab = tabPage2;
                    }

                    directoryName = textBox5.Text = path + @"\shuru\gaoqingxiangji";
                    textBox4.Text = path + @"\shuru\guangdiandiaocang\kejianguang";
                    textBox3.Text = path + @"\shuru\guangdiandiaocang\kejianguang";
                    if (Directory.GetFiles(directoryDVName).Length == 0)
                    {
                        label13.Text = "视频未导入";
                    }
                    else
                    {
                        label13.Text = "视频已导入";
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void 新建任务toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTaskNew frmTaskNew = new FrmTaskNew();
                frmTaskNew.StartPosition = FormStartPosition.CenterScreen;
                frmTaskNew.ClsTask.ProjectID = projectID;
                if (frmTaskNew.ShowDialog() == DialogResult.OK)
                {
                    refreshProjectTree();
                }
            }
            catch (Exception)
            { }
        }

        private void 修改任务toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTaskNew frmTaskNew = new FrmTaskNew();
                frmTaskNew.StartPosition = FormStartPosition.CenterScreen;
                frmTaskNew.ClsTask.ProjectID = projectID;
                frmTaskNew.ClsTask.strTaskName = strTaskName;
                frmTaskNew.ClsTask.Disk = strDisk;
                frmTaskNew.ClsTask.StartPosition = strStartPosition;
                frmTaskNew.ClsTask.Description = description;
                frmTaskNew.ClsTask.TaskID = taskID;
                if (frmTaskNew.ShowDialog() == DialogResult.OK)
                {
                    refreshProjectTree();
                }
            }
            catch (Exception)
            { }
        }

        private void 数据同步ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (label12.Text == "TXT数据已导入")
                {
                    if (MessageBox.Show("TXT数据已导入，是否覆盖原数据？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        FrmTransferTXT frmTransfer = new FrmTransferTXT();
                        frmTransfer.FileName = FileName;
                        frmTransfer.ShowDialog();
                        label12.Text = frmTransfer.strStatus;
                        button1_Click(null, null);
                        tabControl1.SelectedTab = tabPage2;
                    }
                }
                else
                {
                    FrmTransferTXT frmTransfer = new FrmTransferTXT();
                    frmTransfer.FileName = FileName;
                    frmTransfer.ShowDialog();
                    FileInfo fi = new FileInfo(FileName);
                    Size = fi.Length.ToString();
                    if (Size != "0")
                    {
                        label12.Text = "TXT数据导入完成";
                        button1_Click(null, null);
                        tabControl1.SelectedTab = tabPage1;
                    }
                    else
                    {
                        label12.Text = "TXT数据未导入";
                    }
                }
            }
            catch (Exception)
            { }
        }


        private void 高清相机数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (label14.Text == "高清相片已导入")
                {
                    DialogResult dr = MessageBox.Show("高清相片已导入，是否覆盖原数据？", "提示", MessageBoxButtons.YesNoCancel);
                    {
                        if (dr == DialogResult.Yes)
                        {
                            MessageBox.Show("将覆盖原数据");
                            Directory.Delete(directoryName, true);
                            Directory.CreateDirectory(directoryName);
                            FrmPhotoCopy frmPhotoCopy = new FrmPhotoCopy();
                            frmPhotoCopy.directoryName = directoryName;
                            frmPhotoCopy.ShowDialog();
                            if (Directory.GetFiles(directoryName).Length != 0)
                            {
                                label14.Text = "高清相片已导入";
                            }
                            else
                            {
                                label14.Text = "高清相片未导入";
                            }
                        }
                        if (dr == DialogResult.No)
                        {
                            MessageBox.Show("将继续传输数据");
                            FrmPhotoCopy frmPhotoCopy = new FrmPhotoCopy();
                            frmPhotoCopy.directoryName = directoryName;
                            frmPhotoCopy.ShowDialog();
                            if (Directory.GetFiles(directoryName).Length != 0)
                            {
                                label14.Text = "高清相片已导入";
                            }
                            else
                            {
                                label14.Text = "高清相片未导入";
                            }

                        }
                        else
                        {

                        }

                    }
                }
                else
                {
                    FrmPhotoCopy frmPhotoCopy = new FrmPhotoCopy();
                    frmPhotoCopy.directoryName = directoryName;
                    frmPhotoCopy.ShowDialog();
                    if (Directory.GetFiles(directoryName).Length != 0)
                    {
                        label14.Text = "高清相片已导入";
                    }
                    else
                    {
                        label14.Text = "高清相片未导入";
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void 光电吊舱数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (label13.Text == "视频已导入")
                {
                    if (MessageBox.Show("视频已导入,是否覆盖原数据？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        FrmDVCopy frmDVCopy = new FrmDVCopy();
                        frmDVCopy.directoryDVName = directoryName;
                        frmDVCopy.ShowDialog();
                        if (Directory.GetFiles(directoryDVName).Length != 0)
                        {
                            label13.Text = "视频已导入";
                        }
                        else
                        {
                            label13.Text = "视频未导入";
                        }
                    }
                }
                else
                {
                    FrmDVCopy frmDVCopy = new FrmDVCopy();
                    frmDVCopy.directoryDVName = directoryDVName;
                    frmDVCopy.ShowDialog();
                    if (Directory.GetFiles(directoryDVName).Length != 0)
                    {
                        label13.Text = "视频已导入";
                    }
                    else
                    {
                        label13.Text = "视频未导入";
                    }
                }
            }
            catch (Exception)
            { }
        }

        ClsOrdinateTrans cTrans = new ClsOrdinateTrans();//坐标转换
        //public string hhh = "";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button13.Text = "隐藏航迹";
                button9.Text = "显示所有航迹";
                btn_ShowRuts.Text = "显示车辙";
                button7.Text = "显示破损";
                string hhh = "";
                if (!File.Exists(FileName))
                    return;
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string info = sr.ReadToEnd();
                fs.Close();
                string[] infos = info.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                int index = 0;
                lstPoint.Clear();

                double[] trans = new double[2];
                string[] strPoint = new string[2];
                double X = 0, Y = 0;
                foreach (string point in infos)
                {
                    strPoint = point.Split(new String[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (strPoint.Length < 3)
                        continue;
                    if (X == 0)
                    {
                        X = Convert.ToDouble(strPoint[0]);
                        Y = Convert.ToDouble(strPoint[1]);
                    }
                    if (double.TryParse(strPoint[0], out trans[0]) && double.TryParse(strPoint[1], out trans[1]))
                    {
                        ClsCPoint p = new ClsCPoint();
                        p.PointID = index;
                        trans = cTrans.WGS84_BD09(trans[1], trans[0]);
                        p.PointLng = trans[0];
                        p.PointLat = trans[1];
                        //hhh += p.PointLng.ToString() + " " + p.PointLat.ToString() + "\r\n";
                        p.enumBadType = EnumBadType.其他;
                        lstPoint.Add(p);
                        index++;
                    }
                }
                //string hhh = "";
                #region 选取800个点，后期注释
                lstPoint800.Clear();
                int jump = (int)(lstPoint.Count / 800);
                for (int i = 0; i < lstPoint.Count; i += jump)
                {
                    lstPoint800.Add(lstPoint[i]);
                }
                int ranDom = 0;
                int len = lstPoint800.Count - 800;
                Random rDom = new Random();
                for (int i = 0; i < len; i++)
                {
                    ranDom = rDom.Next(0, lstPoint800.Count);
                    lstPoint800.RemoveAt(ranDom);
                }
                for (int i = 0; i < 800; i++)
                {
                    lstPoint800[i].PointID = i;
                    hhh += lstPoint800[i].PointLng.ToString() + " " + lstPoint800[i].PointLat.ToString() + "\r\n";
                }
                Random rDom1 = new Random();
                for (int i = 0; i < 10; i++)
                {
                    lstPoint800[rDom1.Next(200, 600)].enumBadType = EnumBadType.路面车辙;
                    lstPoint800[rDom1.Next(200, 600)].enumBadType = EnumBadType.路面破损;
                }
                //hhh = "";
                #endregion
                //this.webBrowser1.Document.InvokeScript("setZoom(point)",Point);
                this.webBrowser1.Document.InvokeScript("CenterAndZoom", new object[] { X, Y });
                this.webBrowser1.Document.InvokeScript("initMap");
                this.webBrowser1.Document.InvokeScript("addLine", new object[] { hhh });
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// 以鼠标点击为中心获取点集合，该方法由javascript调用
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetMouseCenterPoint(string info, int radius)
        {
            string pInfo = string.Empty;
            string[] mouseP = info.Split(new String[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            double lng = Convert.ToDouble(mouseP[0]);
            double lat = Convert.ToDouble(mouseP[1]);

            string rInfo = string.Empty;
            foreach (ClsCPoint p in lstPoint800)
            {
                if (Math.Sqrt((p.PointLng - lng) * (p.PointLng - lng) + (p.PointLat - lat) * (p.PointLat - lat)) < circleL * radius)
                {
                    rInfo += p.PointID.ToString();
                    rInfo += "|";
                    rInfo += p.PointLng.ToString();
                    rInfo += "|";
                    rInfo += p.PointLat.ToString();
                    rInfo += "\r\n";
                }
            }
            return rInfo;
        }

        /// <summary>
        /// 获取所有的点/破损的点/车辙点
        /// </summary>
        /// <returns></returns>
        public string GetPointByBadType(string info)
        {
            EnumBadType badType = EnumBadType.其他;
            if (info != "")
                badType = (EnumBadType)Enum.Parse(typeof(EnumBadType), info);
            string pInfo = string.Empty;
            foreach (ClsCPoint p in lstPoint800)
            {
                if (info == "")
                {
                    if (p.enumBadType == EnumBadType.其他)
                    {
                        pInfo += p.PointID.ToString();
                        pInfo += "|";
                        pInfo += p.PointLng.ToString();
                        pInfo += "|";
                        pInfo += p.PointLat.ToString();
                        pInfo += "\r\n";
                    }
                    continue;
                }

                if (p.enumBadType == badType)
                {
                    pInfo += p.PointID.ToString();
                    pInfo += "|";
                    pInfo += p.PointLng.ToString();
                    pInfo += "|";
                    pInfo += p.PointLat.ToString();
                    pInfo += "\r\n";
                }
            }
            return pInfo;
        }

        #region 地图相关操作

        private void button2_Click(object sender, EventArgs e)
        {
            if (button13.Text == "显示航迹")
            {
                this.webBrowser1.Document.InvokeScript("showLine");
                button13.Text = "隐藏航迹";
            }
            else
            {
                this.webBrowser1.Document.InvokeScript("hideLine");
                button13.Text = "显示航迹";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                index1++;
                string hhh = "";
                while (index1 < 9)
                {
                    if (!File.Exists(FileName))
                        return;
                    FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    string info = sr.ReadToEnd();
                    fs.Close();
                    string[] infos = info.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    int index = 0;
                    lstPoint.Clear();

                    double[] trans = new double[2];
                    string[] strPoint = new string[2];
                    foreach (string point in infos)
                    {
                        strPoint = point.Split(new String[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                        if (strPoint.Length < 3)
                            continue;
                        if (double.TryParse(strPoint[0], out trans[0]) && double.TryParse(strPoint[1], out trans[1]))
                        {
                            ClsCPoint p = new ClsCPoint();
                            p.PointID = index;
                            trans = cTrans.WGS84_BD09(trans[1], trans[0]);
                            p.PointLng = trans[0];
                            p.PointLat = trans[1];
                            //hhh += p.PointLng.ToString() + " " + p.PointLat.ToString() + "\r\n";
                            p.enumBadType = EnumBadType.其他;
                            lstPoint.Add(p);
                            index++;
                        }
                    }
                    //string hhh = "";
                    #region 选取800个点，后期注释
                    lstPoint800.Clear();
                    int jump = (int)(lstPoint.Count / 800);
                    for (int i = 0; i < lstPoint.Count; i += jump)
                    {
                        lstPoint800.Add(lstPoint[i]);
                    }
                    int ranDom = 0;
                    int len = lstPoint800.Count - 800;
                    Random rDom = new Random();
                    for (int i = 0; i < len; i++)
                    {
                        ranDom = rDom.Next(0, lstPoint800.Count);
                        lstPoint800.RemoveAt(ranDom);
                    }
                    for (int i = 0; i < 100 * index1; i++)
                    {
                        lstPoint800[i].PointID = i;
                        hhh += lstPoint800[i].PointLng.ToString() + " " + lstPoint800[i].PointLat.ToString() + "\r\n";
                    }
                    Random rDom1 = new Random();
                    for (int i = 0; i < 10; i++)
                    {
                        lstPoint800[rDom1.Next(200, 600)].enumBadType = EnumBadType.路面车辙;
                        lstPoint800[rDom1.Next(200, 600)].enumBadType = EnumBadType.路面破损;
                    }
                    #endregion
                    this.webBrowser1.Document.InvokeScript("initMap");
                    this.webBrowser1.Document.InvokeScript("addLine", new object[] { hhh });
                    break;
                }
            }
            catch (Exception)
            { }
            //Thread.Sleep(1000);
        }

        public void addDynamicLine()
        {
            
            string hhh = "";
            if (!File.Exists(FileName))
                return;
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string info = sr.ReadToEnd();
            fs.Close();
            string[] infos = info.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int index = 0;
            lstPoint.Clear();

            double[] trans = new double[2];
            string[] strPoint = new string[2];
            foreach (string point in infos)
            {
                strPoint = point.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (strPoint.Length < 3)
                    continue;
                if (double.TryParse(strPoint[1], out trans[0]) && double.TryParse(strPoint[2], out trans[1]))
                {
                    ClsCPoint p = new ClsCPoint();
                    p.PointID = index;
                    trans = cTrans.WGS84_BD09(trans[1], trans[0]);
                    p.PointLng = trans[0];
                    p.PointLat = trans[1];
                    //hhh += p.PointLng.ToString() + " " + p.PointLat.ToString() + "\r\n";
                    p.enumBadType = EnumBadType.其他;
                    lstPoint.Add(p);
                    index++;
                }
            }
            //string hhh = "";
            #region 选取800个点，后期注释
            lstPoint800.Clear();
            int jump = (int)(lstPoint.Count / 800);
            for (int i = 0; i < lstPoint.Count; i += jump)
            {
                lstPoint800.Add(lstPoint[i]);
            }
            int ranDom = 0;
            int len = lstPoint800.Count - 800;
            Random rDom = new Random();
            for (int i = 0; i < len; i++)
            {
                ranDom = rDom.Next(0, lstPoint800.Count);
                lstPoint800.RemoveAt(ranDom);
            }
            for (int i = 0; i < 100 * index1; i++)
            {
                lstPoint800[i].PointID = i;
                hhh += lstPoint800[i].PointLng.ToString() + " " + lstPoint800[i].PointLat.ToString() + "\r\n";
            }
            Random rDom1 = new Random();
            for (int i = 0; i < 10; i++)
            {
                lstPoint800[rDom1.Next(200, 600)].enumBadType = EnumBadType.路面车辙;
                lstPoint800[rDom1.Next(200, 600)].enumBadType = EnumBadType.路面破损;
            }
            //hhh = "";
            #endregion
            this.webBrowser1.Document.InvokeScript("initMap");
            this.webBrowser1.Document.InvokeScript("addLine", new object[] { hhh });
        }


        private void btnPointDisVisible_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Document.InvokeScript("hideRut");
        }

        private void btnPointVisible_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Document.InvokeScript("showRut");
        }

        private void btnClearMap_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Document.InvokeScript("clearMapOverLays");
        }

        private void btnVisibleAllPoint_Click(object sender, EventArgs e)
        {
            //this.webBrowser1.Document.InvokeScript("showAllPoint");
            if (button9.Text == "显示所有航迹点")
            {
                this.Cursor = Cursors.WaitCursor;
                this.webBrowser1.Document.InvokeScript("showAllPoint");
                button9.Text = "隐藏所有航迹点";
                this.Cursor = Cursors.Arrow;
            }
            else
            {
                this.webBrowser1.Document.InvokeScript("hideAllPoint");
                button9.Text = "显示所有航迹点";
            }
        }

        //private void btnClearCircle_Click(object sender, EventArgs e)
        //{
        //    this.webBrowser1.Document.InvokeScript("clearCircle");
        //}

        private void btnShowCircle_Click(object sender, EventArgs e)
        {
            if (button5.Text == "显示圆")
            {
                this.webBrowser1.Document.InvokeScript("showCircle");
                button5.Text = "隐藏圆";
            }
            else
            {
                this.webBrowser1.Document.InvokeScript("hideCircle");
                button5.Text = "显示圆";
            }
        }

        private void btnShowBroken_Click(object sender, EventArgs e)
        {
            if (button7.Text == "显示破损")
            {
                this.webBrowser1.Document.InvokeScript("showBroken");
                button7.Text = "隐藏破损";
            }
            else
            {
                this.webBrowser1.Document.InvokeScript("hideBroken");
                button7.Text = "显示破损";
            }
        }



        private void btn_ShowRuts_Click(object sender, EventArgs e)
        {
            if (btn_ShowRuts.Text == "显示车辙")
            {
                this.webBrowser1.Document.InvokeScript("showRut");
                btn_ShowRuts.Text = "隐藏车辙";
            }
            else
            {
                this.webBrowser1.Document.InvokeScript("hideRut");
                btn_ShowRuts.Text = "显示车辙";
            }
        }

        #endregion

        private void dockableWindow2_Closing(object sender, TD.SandDock.DockControlClosingEventArgs e)
        {

        }

        int height = 630;
        int width = 868;
        private void tabPage1_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                height = (tabPage3.Height - 20) / 2 - 20;
                width = tabPage3.Width / 2 - 10;

                gb_21.Location = new Point(gb_11.Location.X, gb_11.Location.Y + height + 5);
                gb_12.Location = new Point(gb_11.Location.X + 5 + width, gb_11.Location.Y);
                gb_22.Location = new Point(gb_11.Location.X + 5 + width, gb_11.Location.Y + height + 5);

                gb_11.Height = height;
                gb_12.Height = height;
                gb_21.Height = height;
                gb_22.Height = height;
                gb_11.Width = width;
                gb_12.Width = width;
                gb_21.Width = width;
                gb_22.Width = width;
            }
            catch (Exception)
            { }
        }
        /// <summary>
        /// 在地图界面点击点时，调用该方法，显示该点的相关数据
        /// </summary>
        /// <param name="index"></param>
        public void ShowPointInfo(int index)
        {
            try
            {
                tabControl1.SelectedTab = tabPage3;
                toolLabNum.Text = lstPoint800[index].PointID.ToString();
                toolLabLngLat.Text = lstPoint800[index].PointLng.ToString() + "," + lstPoint800[index].PointLat.ToString();
                textBox17.Text = toolLabLngLat.Text;
                textBox23.Text = toolLabLngLat.Text;
                textBox16.Text = toolLabLngLat.Text;
                string ss = DateTime.Now.ToString();
            }
            catch (Exception)
            { }
        }
        /// <summary>
        /// 双击图片显示大图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                PictureBox pic = sender as PictureBox;
                FrmScrollPic frmScrollPic = new FrmScrollPic();
                Bitmap bitMap = new Bitmap(pic.Image);
                frmScrollPic.myBmp = bitMap;
                frmScrollPic.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void 破损信息分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBreakageAnalysis frmBreakAnalysis = new FrmBreakageAnalysis();
            frmBreakAnalysis.ShowDialog();
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Document.InvokeScript("play");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string hhh = "";
                if (!File.Exists(FileName))
                    return;
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string info = sr.ReadToEnd();
                fs.Close();
                string[] infos = info.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                int index = 0;
                lstPoint.Clear();

                double[] trans = new double[2];
                string[] strPoint = new string[2];
                foreach (string point in infos)
                {
                    strPoint = point.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (strPoint.Length < 3)
                        continue;
                    if (double.TryParse(strPoint[1], out trans[0]) && double.TryParse(strPoint[2], out trans[1]))
                    {
                        ClsCPoint p = new ClsCPoint();
                        p.PointID = index;
                        trans = cTrans.WGS84_BD09(trans[1], trans[0]);
                        p.PointLng = trans[0];
                        p.PointLat = trans[1];
                        //hhh += p.PointLng.ToString() + " " + p.PointLat.ToString() + "\r\n";
                        p.enumBadType = EnumBadType.其他;
                        lstPoint.Add(p);
                        index++;
                    }
                }
                //string hhh = "";
                #region 选取800个点，后期注释
                lstPoint800.Clear();
                int jump = (int)(lstPoint.Count / 800);
                for (int i = 0; i < lstPoint.Count; i += jump)
                {
                    lstPoint800.Add(lstPoint[i]);
                }
                int ranDom = 0;
                int len = lstPoint800.Count - 800;
                Random rDom = new Random();
                for (int i = 0; i < len; i++)
                {
                    ranDom = rDom.Next(0, lstPoint800.Count);
                    lstPoint800.RemoveAt(ranDom);
                }
                for (int i = 0; i < 100 * index1; i++)
                {
                    lstPoint800[i].PointID = i;
                    hhh += lstPoint800[i].PointLng.ToString() + " " + lstPoint800[i].PointLat.ToString() + "\r\n";
                }
                Random rDom1 = new Random();
                for (int i = 0; i < 10; i++)
                {
                    lstPoint800[rDom1.Next(200, 600)].enumBadType = EnumBadType.路面车辙;
                    lstPoint800[rDom1.Next(200, 600)].enumBadType = EnumBadType.路面破损;
                }
                //hhh = "";
                #endregion
                this.webBrowser1.Document.InvokeScript("initMap");
                this.webBrowser1.Document.InvokeScript("addLine", new object[] { hhh });
            }
            catch (Exception)
            { }
        }

        private void 交通载荷分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmProjectAnalyse frmProjectAnalyse = new FrmProjectAnalyse();
            //frmProjectAnalyse.ShowDialog();FrmProjectAnalyse frmProjectAnalyse = new FrmProjectAnalyse();
            //showdialogC();
            //WinTraffic win = new WinTraffic();
            //win.Show();
            showdialogC();
        }

        private void toolStripMenu生成硬盘二维码_Click(object sender, EventArgs e)
        {
            FrmCode2 frmCode2 = new FrmCode2();
            frmCode2.clsTask.TaskID = taskID;
            frmCode2.ShowDialog();
        }

        private void 硬盘列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 景观信息分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                showdialogZ();
            }
            catch (Exception)
            { }
        }

        private void 车辙分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            showdialogF();
            }
            catch (Exception)
            { }
        }

        private void 破损分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            showdialog();
            }
            catch (Exception)
            { }
        }

        private void 路面车辙分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            WinGroove win = new WinGroove();
            win.Show();
            }
            catch (Exception)
            { }
        }

    }
}
