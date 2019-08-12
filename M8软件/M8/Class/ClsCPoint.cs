using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M8.Class
{
  public  class ClsCPoint
    {
        public int PointID
        {
            get;
            set;
        }

        public double PointLng
        {
            get;
            set;
        }

        public double PointLat
        {
            get;
            set;
        }

        public EnumBadType enumBadType
        { get; set; }
    }
    public enum EnumBadType
    {
        路面车辙=0,
        路面破损=1,
        其他=2
    }
}
