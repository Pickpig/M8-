﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M8.Class
{
    /// <summary>
    /// 坐标转换,GPS/WGS84、GCJ-02、bd-09
    /// </summary>
    public class ClsOrdinateTrans
    {
        double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        double z = 0.0;
        double theta = 0.0;
        double bd_lon = 0.0;
        double bd_lat = 0.0;

        public double[] GCJ02_BD09(double gg_lat, double gg_lon)
        {
             z = Math.Sqrt(gg_lon * gg_lon + gg_lat * gg_lat) + 0.00002 * Math.Sin(gg_lat * x_pi);
             theta = Math.Atan2(gg_lat, gg_lon) + 0.000003 * Math.Cos(gg_lon * x_pi);
             bd_lon = z * Math.Cos(theta) + 0.0065;
             bd_lat = z * Math.Sin(theta) + 0.006;
            return new double[2] { bd_lon, bd_lat };
        }


        double pi = 3.14159265358979324;

        double a = 6378245.0;
        double ee = 0.00669342162296594323;

        double mgLat=0.0;
        double mgLon=0.0;
        double dLat = 0.0;
        double dLon = 0.0;
        double radLat = 0.0;
        double magic=0.0;
        double sqrtMagic = 0.0;
        // World Geodetic System ==> Mars Geodetic System
        public double[] WGS84_GCJ02(double wgLat, double wgLon)
        {
            if (outOfChina(wgLat, wgLon))
            {
                mgLat = wgLat;
                mgLon = wgLon;
                return new double[2] { wgLat, wgLon };
            }
             dLat = transformLat(wgLon - 105.0, wgLat - 35.0);
             dLon = transformLon(wgLon - 105.0, wgLat - 35.0);
             radLat = wgLat / 180.0 * pi;
             magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            mgLat = wgLat + dLat;
            mgLon = wgLon + dLon;
            return new double[2] { mgLat, mgLon };
        }

        double[] trans = new double[2];
        public double[] WGS84_BD09(double wgLat, double wgLon)
        {
               trans= WGS84_GCJ02(wgLat,wgLon);
               return GCJ02_BD09(trans[0], trans[1]);
        }

        public bool outOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        double ret = 0.0;
        public double transformLat(double x, double y)
        {
            ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        public double transformLon(double x, double y)
        {
            ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }
    }
}
