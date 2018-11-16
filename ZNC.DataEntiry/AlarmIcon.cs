using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ZNC.DataEntiry
{
    public static class AlarmIcon
    {
        public static Dictionary<Enum, DoublePoint> Deviceposition = new Dictionary<Enum, DoublePoint>
            {
                {DeviceID.Robot1, new DoublePoint(2.26, 0.92)},
                {DeviceID.Robot2, new DoublePoint(2.26, 2.32)},
                {DeviceID.Robot3, new DoublePoint(2.26, 3.73)},
                {DeviceID.Robot4, new DoublePoint(2.26, 5.14)},
                {DeviceID.Robot5, new DoublePoint(2.26, 6.54)},
                {DeviceID.Robot6, new DoublePoint(2.26, 7.96)},
                {DeviceID.AGV1, new DoublePoint(0.1, 0.1)},
                {DeviceID.AGV2, new DoublePoint(0.1, 1.9)},
                {DeviceID.AGV3, new DoublePoint(0.1, 3.7)},
                {DeviceID.AGV4, new DoublePoint(5.28, 0.1)},
                {DeviceID.AGV5, new DoublePoint(5.28, 1.9)},
                {DeviceID.AGV6, new DoublePoint(5.28, 3.7)},
                {DeviceID.CNC1, new DoublePoint(0.1, 5.5)},
                {DeviceID.CNC2, new DoublePoint(0.1, 7.75)},
                {DeviceID.CNC3, new DoublePoint(5.28, 5.5)},
                {DeviceID.CNC4, new DoublePoint(5.28, 7.75)}
            };

        public static Dictionary<Enum, string> Picurl = new Dictionary<Enum, string>
        {
            {PicType.greengif,"./image/greenf.gif"},
            {PicType.redgif,"./image/redf.gif"},
            {PicType.yellowgif,"./image/yellowf.gif"},
            {PicType.greenpng,"./image/green.png"},
            {PicType.redpng,"./image/red.png"},
            {PicType.yellowpng,"./image/yellow.png"}
        };
    }

    public enum PicType
    {
        greengif = 1,
        redgif = 2,
        yellowgif = 3,
        greenpng = 4,
        redpng = 5,
        yellowpng = 6
    }

    public class DoublePoint
    {
        public double X = 0;
        public double Y = 0;

        public DoublePoint(double _x, double _y)
        {
            X = _x;
            Y = _y;
        }
    }

    public enum DeviceID
    {
        Robot1 = 1,
        Robot2 = 2,
        Robot3 = 3,
        Robot4 = 4,
        Robot5 = 5,
        Robot6 = 6,
        AGV1 = 7,
        AGV2 = 8,
        AGV3 = 9,
        AGV4 = 10,
        AGV5 = 11,
        AGV6 = 12,
        CNC1 = 13,
        CNC2 = 14,
        CNC3 = 15,
        CNC4 = 16
    }
}
