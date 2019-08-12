using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M8
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            FrmMain frmMain = new FrmMain();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.frmMain = frmMain;

            Application.ThreadException += (System.Threading.ThreadExceptionEventHandler)delegate(object sender, System.Threading.ThreadExceptionEventArgs e)
            {

            };
            AppDomain.CurrentDomain.UnhandledException += (UnhandledExceptionEventHandler)delegate(object sender, UnhandledExceptionEventArgs e)
            {

            };
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Application.Run(frmMain);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "运行主窗体出错");
                }
            }
            else
            {
                return;
            }
        }
    }
}
