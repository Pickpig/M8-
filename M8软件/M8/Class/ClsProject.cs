using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace M8.Class
{
    
    public class ClsProject
    {
        private int _intProjectID;
        public int intProjectID
        {
            get { return _intProjectID;}
            set { _intProjectID=value;}
        }

        private string _strProjctName;
        public string strProjectName
        {
            get;
            set;
        }

        private DateTime _dtpTime;
        public DateTime dtpTime
        {
            get;
            set;
        }

        private string _strProjectBuilder;
        public string strProjectBuilder
        {
            get;
            set;
        }

        private string _description;
        public string description
        {
            get;
            set;
        }
    }
}
