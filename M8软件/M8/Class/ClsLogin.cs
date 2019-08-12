using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using M8.User;

namespace M8.Class
{
    class ClsLogin
    {
        public CUser curUser = new CUser();
       public ClsSQLMethod sqlMethod;
        public string Login(string sqlIP, int sqlPort,  string userName, string passWord)
        {
            try
            {
                sqlMethod = new ClsSQLMethod();
                string info = string.Empty;
                if (sqlMethod.Connection(sqlIP, sqlPort))
                {
                    //lblConnection.Text = "与数据库成功建立连接！";
                }
                else
                {
                    info = "与数据库连接失败！";
                }
                switch (CheckUserExist(userName, passWord))
                {
                    case 1:
                        info = "登陆成功";
                        break;
                    case 2:
                        info = "密码不正确！";
                        return info;
                    case 0:
                        info = "用户名不存在！";
                        return info;
                    default:
                        return info;
                }

                return info;
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }
        private int CheckUserExist(string userName, string passWord)
        {
            int loadIdent = 0;
            DataTable dtUser = new DataTable();
            dtUser = ClsSQLMethod.SearchUser();
            foreach (DataRow row in dtUser.Rows)
            {
                if (row[1].ToString() == userName)
                {
                    if (row[2].ToString() == passWord)
                    {
                        loadIdent = 1;
                        FillCUserInfo(row);
                        break;
                    }
                    else
                    {
                        loadIdent = 2;
                        break;
                    }
                }
            }
            return loadIdent;
        }

        private void FillCUserInfo(DataRow row)
        {
            curUser.UserID = int.Parse(row[0].ToString());
            curUser.UserName = row[1].ToString();
            curUser.PassWord = row[2].ToString();
            //curUser.Authority = (M8.User.CUser.EnumUserAutho)(Enum.Parse(typeof(M8.User.CUser.EnumUserAutho), row[3].ToString()));
            curUser.Description = row[4].ToString();
        }
        
    }
}
