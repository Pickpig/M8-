using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Win32;
using M8.User;


namespace M8.Class
{
    public delegate void SqlConnectionStateChangeEventHandler(StateChangeEventArgs e);
    public delegate object ReturnObjectUserParaEventHandler(string strCmdText, List<string> listNames, List<SqlDbType> listDBTypes, List<object> listValues);
    public delegate DataTable QueryDatabaseUseParaEventHandler(string strCmdText, List<string> listNames, List<SqlDbType> listDBTypes, List<object> listValues);
    public delegate int UpdateTableUseParaEventHandler(string strCmdText, List<string> listNames, List<SqlDbType> listDBTypes, List<object> listValues);

   public class ClsSQLMethod
    {
        private string strConn;              //数据库连接字符串
        public string sqlIP = null;
        public int sqlPort = 0;
        public bool Isconnected;
        private SqlDataAdapter sqlAdapter;
        public static StringBuilder strBuilder;
        private SqlConnection sqlConn;       //数据库连接
        public static ClsSQLConnection csqlConn = null;
        public ClsSQLMethod sqlDB;

        private static event ReturnObjectUserParaEventHandler EventReturnObjectUserPara;
        public event SqlConnectionStateChangeEventHandler SqlConnectionStateChange;

        public void SetConnectString(string NetworkAddress, string DatabaseName, string UserId, string PassWord)    //设定与数据库连接字符串
        {
            strConn = "Network Address=" + NetworkAddress + ";Database=" + DatabaseName + ";user id=" + UserId + ";password=" + PassWord + ";MultipleActiveResultSets=true";//MultipleActiveResultSets=true
        }

        public string GetConnectString()
        {
            return strConn;
        }

        public bool Connection(string sqlIP, int sqlPort)
        {
            try
            {
                if (!Isconnected)
                {
                    ClsSQLConnection sqlConnection = new ClsSQLConnection();
                    //sqlConnection.serverName = serverName;
                    sqlConnection.sqlIP = sqlIP;
                    sqlConnection.sqlPort = sqlPort;
                    sqlConnection.Connection();
                    csqlConn = sqlConnection;
                    Isconnected = sqlConnection.IsConnected;
                    EventReturnObjectUserPara += new ReturnObjectUserParaEventHandler(sqlConnection.ReturnObjectUsePara);
                    EventQueryDataBaseUsePara += new QueryDatabaseUseParaEventHandler(sqlConnection.QueryDataBaseUsePara);
                    EventUpdateTableUsePara += new UpdateTableUseParaEventHandler(sqlConnection.UpdateTableUsePara);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Isconnected;
        }

        private void sqlConn_StateChange(object sender, StateChangeEventArgs e)
        {
            SqlConnectionStateChange(e);
            //throw new Exception("The method or operation is not implemented.");
        }

        private static event QueryDatabaseUseParaEventHandler EventQueryDataBaseUsePara;
        private static event UpdateTableUseParaEventHandler EventUpdateTableUsePara;

        public static DataTable SearchUser()
        {
            strBuilder = new StringBuilder();
            strBuilder.Append("SELECT*FROM[用户]");
            DataTable dt = new DataTable();
            try
            {
                dt = csqlConn.QueryDatabase(strBuilder.ToString());
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public static int AddUser(CUser user)
        {
            int userID = 0;
            StringBuilder strBuilder;
            List<string> listNames;
            List<SqlDbType> listDBTypes;
            List<object> listValues;

            strBuilder = new StringBuilder("INSERT INTO [用户]([用户名],[密码],[用户权限],[备注],[姓名],[联系方式])");
            strBuilder.Append(" VALUES(@userName,@passWord,@Authority,@Description,@TrueName,@Contact);SELECT SCOPE_IDENTITY()");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@userName");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(user.UserName);

            listNames.Add("@passWord");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(user.PassWord);

            listNames.Add("@Authority");
            listDBTypes.Add(@SqlDbType.Int);
            listValues.Add((int)user.Authority);

            listNames.Add("@Description");
            listDBTypes.Add(SqlDbType.VarChar);
            listValues.Add(user.Description);

            listNames.Add("@TrueName");
            listDBTypes.Add(SqlDbType.VarChar);
            listValues.Add(user.TrueName);

            listNames.Add("@Contact");
            listDBTypes.Add(SqlDbType.VarChar);
            listValues.Add(user.Contact);

            try
            {
                object obj = EventReturnObjectUserPara(strBuilder.ToString(), listNames, listDBTypes, listValues);
                userID = int.Parse(obj.ToString());
                user.UserID = userID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return userID;
        }

        public static int ModifyUser(CUser user)
        {
            int userID = 0;
            StringBuilder strBuilder;
            List<string> listNames;
            List<SqlDbType> listDBTypes;
            List<object> listValues;

            strBuilder = new StringBuilder("UPDATE [用户] SET [用户名]=@userName,[密码]=@passWord,[用户权限]=@Authority,[备注]=@Description,[姓名]=@TrueName,[联系方式]=@Contact WHERE [用户ID]=@userID");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@userName");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(user.UserName);

            listNames.Add("@passWord");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(user.PassWord);

            listNames.Add("@Authority");
            listDBTypes.Add(@SqlDbType.Int);
            listValues.Add((int)user.Authority);

            listNames.Add("@Description");
            listDBTypes.Add(SqlDbType.VarChar);
            listValues.Add(user.Description);

            listNames.Add("@userID");
            listDBTypes.Add(SqlDbType.Int);
            listValues.Add((int)user.UserID);

            listNames.Add("@TrueName");
            listDBTypes.Add(SqlDbType.VarChar);
            if (user.TrueName != null)
                listValues.Add(user.TrueName);
            else
                listValues.Add("");

            listNames.Add("@Contact");
            listDBTypes.Add(SqlDbType.VarChar);
            if (user.Contact != null)
                listValues.Add(user.Contact);
            else
                listValues.Add("");

            if (0 == EventUpdateTableUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues))
            {
                throw new Exception("用户信息修改失败");
            }
            try
            {
                object obj = EventUpdateTableUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues);
                userID = int.Parse(obj.ToString());
                user.UserID = userID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return userID;
        }

        public static bool DeleteUser(CUser user)
        {
            bool isTrue = false;
            try
            {

                StringBuilder strBuilder;
                List<string> listNames;
                List<SqlDbType> listDBTypes;
                List<object> listValues;

                strBuilder = new StringBuilder("DELETE FROM [用户] WHERE [用户ID]=@userID");

                listNames = new List<string>();
                listDBTypes = new List<SqlDbType>();
                listValues = new List<object>();

                listNames.Add("@userID");
                listDBTypes.Add(SqlDbType.Int);
                listValues.Add(user.UserID);
           
                object obj = EventUpdateTableUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues);
                isTrue = true;
            }
            catch (Exception ex)
            {
                throw new Exception("失败", new Exception(ex.Message));
            }
            return isTrue;
           

        }

        public static DataTable SearchSoftWare()
        {
            strBuilder = new StringBuilder();
            strBuilder.Append("SELECT*FROM[软件信息]");
            DataTable dt = new DataTable();
            try
            {
                dt = csqlConn.QueryDatabase(strBuilder.ToString());
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public static DataTable SearchProject()
        {
            strBuilder = new StringBuilder();
            strBuilder.Append("SELECT*FROM[项目列表]");
            DataTable dt = new DataTable();
            try
            {
                dt = csqlConn.QueryDatabase(strBuilder.ToString());
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public DataTable SearchSpecificProject(string strProjectName)
        { 
            strBuilder = new StringBuilder();
            List<string> listNames;
            List<SqlDbType> listDBTypes;
            List<object> listValues;
            strBuilder.Append("SELECT*FROM [项目列表] WHERE [项目名称]=@strProjectName");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@strProjectName");
            listDBTypes.Add(SqlDbType.VarChar);
            listValues.Add(strProjectName);

            DataTable dt = new DataTable();
            try
            {
                dt = EventQueryDataBaseUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt; 
        }

        public int AddProject(ClsProject project)
        {
            int projectID = 0;
            StringBuilder strBuilder;
            List<string> listNames;
            List<SqlDbType> listDBTypes;
            List<object> listValues;

            strBuilder = new StringBuilder("INSERT INTO [项目列表]([项目名称],[测试时间],[创建人],[备注])");
            strBuilder.Append(" VALUES(@projectName,@testTime,@testStuff,@description);SELECT SCOPE_IDENTITY()");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@projectName");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(project.strProjectName);

            listNames.Add("@testTime");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(project.dtpTime);

            listNames.Add("@testStuff");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(project.strProjectBuilder);

            listNames.Add("@description");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(project.description);
            try
            {
                object obj = EventReturnObjectUserPara(strBuilder.ToString(), listNames, listDBTypes, listValues);
                projectID = int.Parse(obj.ToString());
                project.intProjectID = projectID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return projectID;
        }

        public int ModifyProject(ClsProject project)
        {
            int _intProjectID = 0;
            StringBuilder strBuilder;
            List<string> listNames;
            List<SqlDbType> listDBTypes; 
            List<object> listValues;

            strBuilder = new StringBuilder("UPDATE [项目列表] SET [项目名称]=@projectName,[创建人]=@testStuff,[备注]=@description WHERE [项目ID]=@intProjectID");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@intProjectID");
            listDBTypes.Add(SqlDbType.VarChar);
            listValues.Add(project.intProjectID);

            listNames.Add("@projectName");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(project.strProjectName);

            listNames.Add("@testStuff");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(project.strProjectBuilder);

            listNames.Add("@description");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(project.description);

            if (0 == EventUpdateTableUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues))
            {
                throw new Exception("用户信息修改失败");
            }
            try
            {
                object obj = EventUpdateTableUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues);
                _intProjectID = int.Parse(obj.ToString());
                project.intProjectID = _intProjectID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _intProjectID;
        }

        public static bool DeleteProject(ClsProject project)
        {
            bool isTrue = false;
            StringBuilder strBuilder;
            List<string> listNames;
            List<SqlDbType> listDBTypes;
            List<object> listValues;

            strBuilder = new StringBuilder("DELETE FROM [项目列表] WHERE [项目ID]=@intProjectID");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@intProjectID");
            listDBTypes.Add(SqlDbType.Int);
            listValues.Add(project.intProjectID);

            try
            {
                object obj = EventUpdateTableUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues);
                isTrue = true;
            }
            catch (Exception ex)
            {
                throw new Exception("失败", new Exception(ex.Message));
            }
            return isTrue;
        }

        public static DataTable SearchTask()
        {
            strBuilder = new StringBuilder();
            strBuilder.Append("SELECT*FROM[任务列表]");
            DataTable dt = new DataTable();
            try
            {
                dt = csqlConn.QueryDatabase(strBuilder.ToString());
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public int AddTask(ClsTask task)
        {
            int taskID = 0;
            StringBuilder strBuilder;
            List<string> listNames;
            List<SqlDbType> listDBTypes;
            List<object> listValues;

            strBuilder = new StringBuilder("INSERT INTO [任务列表]([项目ID],[任务名称],[物理硬盘],[起始位置],[备注])");
            strBuilder.Append(" VALUES(@projectID,@TaskName,@disk,@startPosition,@description);SELECT SCOPE_IDENTITY()");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@projectID");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(task.ProjectID);

            listNames.Add("@TaskName");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(task.strTaskName);

            listNames.Add("@disk");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(task.Disk);

            listNames.Add("@startPosition");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(task.StartPosition);

            listNames.Add("@description");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(task.Description);
            try
            {
                object obj = EventReturnObjectUserPara(strBuilder.ToString(), listNames, listDBTypes, listValues);
                taskID = int.Parse(obj.ToString());
                task.TaskID = taskID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return taskID;
        }

        public int ModifyTask(ClsTask task)
        {
            int _intTaskID = 0;
            StringBuilder strBuilder;

            List<string> listNames;
            List<SqlDbType> listDBTypes;
            List<object> listValues;

            strBuilder = new StringBuilder("UPDATE [任务列表] SET [项目ID]=@projectID,[任务名称]=@TaskName,[物理硬盘]=@disk,[起始位置]=@startPosition,[备注]=@description WHERE [任务ID]=@intTaskID");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@projectID");
            listDBTypes.Add(SqlDbType.VarChar);
            listValues.Add(task.ProjectID);

            listNames.Add("@taskName");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(task.strTaskName);

            listNames.Add("@disk");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(task.Disk);

            listNames.Add("@startPosition");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(task.StartPosition);

            listNames.Add("@description");
            listDBTypes.Add(@SqlDbType.VarChar);
            listValues.Add(task.Description);

            listNames.Add("@intTaskID");
            listDBTypes.Add(SqlDbType.VarChar);
            listValues.Add(task.TaskID);

            if (0 == EventUpdateTableUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues))
            {
                throw new Exception("用户信息修改失败");
            }
            try
            {
                object obj = EventUpdateTableUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues);
                _intTaskID = int.Parse(obj.ToString());
                task.TaskID = _intTaskID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _intTaskID;
        }

        public static bool DeleteTask(ClsTask task)
        {
            bool isTrue = false;
            StringBuilder strBuilder;
            List<string> listNames;
            List<SqlDbType> listDBTypes;
            List<object> listValues;

            strBuilder = new StringBuilder("DELETE FROM [任务列表] WHERE [任务ID]=@intTaskID");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@intTaskID");
            listDBTypes.Add(SqlDbType.Int);
            listValues.Add(task.TaskID);

            try
            {
                object obj = EventUpdateTableUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues);
                isTrue = true;
            }
            catch (Exception ex)
            {
                throw new Exception("失败", new Exception(ex.Message));
            }
            return isTrue;
        }

        public DataTable SearchSpecificTask(string strTaskName)
        {
            strBuilder = new StringBuilder();
            List<string> listNames;
            List<SqlDbType> listDBTypes;
            List<object> listValues;
            strBuilder.Append("SELECT*FROM [任务列表] WHERE [任务名称]=@strTaskName");

            listNames = new List<string>();
            listDBTypes = new List<SqlDbType>();
            listValues = new List<object>();

            listNames.Add("@strTaskName");
            listDBTypes.Add(SqlDbType.VarChar);
            listValues.Add(strTaskName);

            DataTable dt = new DataTable();
            try
            {
                dt = EventQueryDataBaseUsePara(strBuilder.ToString(), listNames, listDBTypes, listValues);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
    }
}
