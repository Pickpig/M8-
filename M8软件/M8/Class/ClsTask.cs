using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M8.Class
{
   public class ClsTask
    {
        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        private int _projectID;
        public int ProjectID
        {
            get;
            set;
        }

        private string _strTaskName;
        public string strTaskName
        {
            get;
            set;
        }

        private string _disk;
        public string Disk
        {
            get;
            set;
        }

        private string _startPosition;
        public string StartPosition
        { 
            get;
            set;
        }

        private string _description;
        public string Description
        {
            get;
            set;
        }

        private int _taskID;
        public int TaskID
        {
            get;
            set;
        }
    }
}
