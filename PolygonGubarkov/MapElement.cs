using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PolygonGubarkov
{
    class MapElement
    {
        PolygonPoint[] points;
        string text;

        bool isError = false;
        public string errorMessage = "";

        public MapElement(string type, string text)
        {
            setText(text);
        }

        public MapElement(string type, PolygonPoint[] points)
        {
            setPoints(points);
        }

        public MapElement(string s)
        {
            setProperties(s);
        }

        public string toString()
        {
            string s_points = "";
            if (points != null)
                foreach (PolygonPoint pp in points)
                {
                    s_points += pp.getX() + ":" + pp.getY() + " ";
                }
            return String.Format("points={0}; text:{1}", s_points, text);
        }

        /// <summary>
        /// Парсит входную строку и вытаскивает значения для свойств класса
        /// </summary>
        /// <param name="s"></param>
        void setProperties(string s)
        {
            try
            {
                string[] attributes = s.Split(';');
                foreach (string a in attributes)
                {
                    if (a.IndexOf("points=") != -1)
                    {
                        //Points=187:98 155:250 340:342 303:104 233:57
                        string[] points = a.Split('=')[1].Split(' ');
                        List<PolygonPoint> pl = new List<PolygonPoint>();
                        foreach (string str in points)
                        {
                            try
                            {
                                if (str != "")
                                {
                                    int x = Convert.ToInt16(str.Split(':')[0]);
                                    int y = Convert.ToInt16(str.Split(':')[1]);
                                    PolygonPoint point = new PolygonPoint(x, y);
                                    pl.Add(point);
                                }
                            }
                            catch (Exception ex)
                            {
                                isError = true;
                                errorMessage += ex.Message + "\n" + ex.StackTrace + "\n";
                            }
                        }
                        setPoints(pl.ToArray());
                    }
                    else if (a.IndexOf("text:") != -1)
                    {
                        setText(a.Split(':')[1]);
                    }
                }
            }
            catch (Exception e)
            {
                isError = true;
                errorMessage += e.Message + "\n" + e.StackTrace + "\n";
            }
        }
        
        public void setPoints(PolygonPoint[] points)
        {
            this.points = new PolygonPoint[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                this.points[i] = points[i];
            }
        }

        public PolygonPoint[] getPoints()
        {
            return points;
        }

        public Point[] getPointsAsPoint()
        {
            try
            {
                Point[] pa = new Point[points.Length];
                for (int i = 0; i < points.Length; i++)
                {
                    pa[i] = new Point((int)points[i].getX(), (int)points[i].getY());
                }
                return pa;
            }
            catch (Exception e)
            {
                isError = true;
                errorMessage += e.Message + "\n" + e.StackTrace;
                return null;
            }
        }

        public string getText()
        {
            return text;
        }

        public void setText(string text)
        {
            this.text = text;
        }

        public bool hasError()
        {
            return isError;
        }
    }
}
