using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M8.Class;
namespace M8.User
{
    public class CUser
    {
        private int userID;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value; 
            }
        }

        private string passWord;
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private EnumUserAutho authority;
        public EnumUserAutho Authority
        {
            get { return authority; }
            set { authority = value; }
        }
      
        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        private string contact;
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        private string trueName;
        public string TrueName
        {
            get { return trueName; }
            set { trueName = value; }
        }

        private int logID;
        public int LogID
        {
            get { return logID; }
            set { logID = value; }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string loginTime;
        public string LoginTime
        {
            get;
            set;
        }

        private string time;
        public string Time
        {
            get;
            set;
        }
    }

    public enum EnumUserAutho
    {
        管理员 = 0,
        普通用户 = 1,
        其他用户 = 2,
    }
}
