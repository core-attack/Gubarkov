using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PolygonGubarkov
{
    class PolygonPoint
    {
        long x = 0;
        long y = 0;
        //для рисования на форме
        public int paintX = 0;
        public int paintY = 0;

        //длина и ширина точки
        public static int width = 2;
        public static int height = 2;

        public long getX()
        {
            return x;
        }

        public long getY()
        {
            return y;
        }

        public void setX(long x)
        {
            this.x = x;
        }

        public void setY(long y)
        {
            this.y = y;
        }

        public void setXY(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        public PolygonPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public PolygonPoint(int x, int y, int paintX, int paintY)
        {
            this.x = x;
            this.y = y;
            this.paintX = paintX;
            this.paintY = paintY;
        }
    }
}
