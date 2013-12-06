using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace PolygonGubarkov
{
    class Polygon : elementaryObject
    {
        PolygonPoint[] points;
        static bool isIntersection = false;
        public bool isEnclose = false;
        static bool isError = false;
        static public string errorMessage = "";

        public string toString()
        {
            string points = "";
            foreach(PolygonPoint pp in this.getPoints())
            {
                points += pp.getX() + ":" + pp.getY() + " ";
            }
            return String.Format("Name:{0}#Type:{4}#Location:{1}#foreColor:{2}#Points={3}#isEnclose:{5}#backgroundColor:{6}", this.getName(), this.getLocation(), this.getColor(), points, "POLYGON", isEnclose, this.getBackgroundColor());
        }

        //считывает предыдущую строку
        public void readStringForThisPolygon(string s)
        {
            try
            {
                string[] attributes = s.Split('#');
                foreach (string a in attributes)
                {
                    if (a.IndexOf("Name:") != -1)
                    {
                        setName(a.Split(':')[1]);
                    }
                    else if (a.IndexOf("Location:") != -1)
                    {
                        //Location:{X=0,Y=0}
                        string[] location = a.Split('{')[1].Split('}')[0].Split(',');
                        int x = Convert.ToInt16(location[0].Split('=')[1]);
                        int y = Convert.ToInt16(location[1].Split('=')[1]);
                        setLocation(new Point(x, y));
                    }
                    else if (a.IndexOf("foreColor:") != -1)
                    {
                        setColor(Color.FromName(a.Split(':')[1]));
                    }
                    else if (a.IndexOf("Points=") != -1)
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
                    else if (a.IndexOf("isEnclose:") != -1)
                    {
                        isEnclose = Convert.ToBoolean(a.Split(':')[1]);
                    }
                    else if (a.IndexOf("backgroundColor:") != -1)
                    {
                        setBackgroundColor(Color.FromName(a.Split(':')[1]));
                    }
                }
            }
            catch (Exception e)
            {
                isError = true;
                errorMessage += e.Message + "\n" + e.StackTrace + "\n";
            }
        }

        //точка принадлежит многограннику
        public static Color colorAffilPolygon = Color.Green;
        //точка не принадлежит многограннику
        public static Color colorNotAffilPolygon = Color.Red;
        //точка лежит на вершине многогранника
        public static Color colorInPolygon = Color.Blue;

        public bool hasIntersection()
        {
            return isIntersection;
        }

        public bool hasError()
        {
            return isError;
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

        public void setPoints(PolygonPoint[] points)
        {
            this.points = new PolygonPoint[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                this.points[i] = points[i];
            }
        }

        public Polygon()
        {
            
        }

        public Polygon(string polygon)
        {
            readStringForThisPolygon(polygon);
        }

        public Polygon(Color color)
        {
            setColor(color);
        }

        public Polygon(PolygonPoint[] points, Color color)
        {
            setPoints(points);
            setColor(color);
        }

        public Polygon(PolygonPoint[] points, Color color, Point location, string name, bool colorFill)
        {
            setPoints(points);
            setColor(color);
            this.setLocation(location);
            this.setName(name);
            this.colorFill = colorFill;
        }

        public static void paint(Panel panel, PolygonPoint[] points, Color color)
        {
            try
            {
                for (int j = 0; j < points.Length; j++)
                {
                    if (j + 1 < points.Length)
                        paintLine(points[j].getX(), points[j].getY(), points[j + 1].getX(), points[j + 1].getY(), color, panel);
                    else
                        paintLine(points[j].getX(), points[j].getY(), points[0].getX(), points[0].getY(), color, panel);
                }
            }
            catch (Exception e)
            {
                isError = true;
                errorMessage += e.Message + "\n" + e.StackTrace + "\n";
            }
        }

        static void paintLine(long x1, long y1, long x2, long y2, Color color, Panel panel)
        {
            Graphics g = panel.CreateGraphics();
            Pen p = new Pen(color);
            Point[] ps = new Point[2];
            ps[0].X = Convert.ToInt16(x1);
            ps[0].Y = Convert.ToInt16(y1);
            ps[1].X = Convert.ToInt16(x2);
            ps[1].Y = Convert.ToInt16(y2);
            g.DrawCurve(p, ps);
            p.Dispose();
            g.Dispose();
        }

        //проверка многоугольника на выпуклость
        public string provPolygon(List<PolygonPoint> points)
        {
            //для площадей треугольников
            //List<double> trianglesS = new List<double>();
            //вычитаем из всех координат координаты первой вершины
            //for (int i = 1; i < points.Count - 1; i++)
            //{

            //}
            bool isIntersect = false;
            for (int i = 0; i < points.Count - 1; i++)
            {
                for (int j = 0; j < points.Count - 1; j++)
                {
                    if (i != j)
                    {
                        if (i + 1 < points.Count - 1 && j + 1 < points.Count - 1)
                        {
                            //if (intersection(new Point(Convert.ToInt16(points[i].getX()), Convert.ToInt16(points[i].getY())), 
                            //    new Point(Convert.ToInt16(points[i + 1].getX()), Convert.ToInt16(points[i + 1].getY())),
                            //new Point(Convert.ToInt16(points[j].getX()), Convert.ToInt16(points[j].getY())), 
                            //    new Point(Convert.ToInt16(points[j + 1].getX()), Convert.ToInt16(points[j + 1].getY()))))
                            if (intersection(points[i].getX(), points[i].getY(),
                                points[i + 1].getX(), points[i + 1].getY(),
                                points[j].getX(), points[j].getY(),
                                points[j + 1].getX(), points[j + 1].getY()))
                            {
                                isIntersect = true;
                            }
                        }
                        else if (i + 1 == points.Count - 1 && j + 1 < points.Count - 1)
                        {
                            //if (intersection(new Point(Convert.ToInt16(points[0].getX()), Convert.ToInt16(points[0].getY())),
                            //    new Point(Convert.ToInt16(points[i + 1].getX()), Convert.ToInt16(points[i + 1].getY())),
                            //new Point(Convert.ToInt16(points[j].getX()), Convert.ToInt16(points[j].getY())),
                            //    new Point(Convert.ToInt16(points[j + 1].getX()), Convert.ToInt16(points[j + 1].getY()))))
                            if (intersection(points[0].getX(), points[0].getY(),
                                points[i + 1].getX(), points[i + 1].getY(),
                                points[j].getX(), points[j].getY(),
                                points[j + 1].getX(), points[j + 1].getY()))
                            {
                                isIntersect = true;
                            }
                        }
                        else if (i + 1 < points.Count - 1 && j + 1 == points.Count - 1)
                        {
                            //if (intersection(new Point(Convert.ToInt16(points[i].getX()), Convert.ToInt16(points[i].getY())),
                            //    new Point(Convert.ToInt16(points[i + 1].getX()), Convert.ToInt16(points[i + 1].getY())),
                            //new Point(Convert.ToInt16(points[0].getX()), Convert.ToInt16(points[0].getY())),
                            //    new Point(Convert.ToInt16(points[j + 1].getX()), Convert.ToInt16(points[j + 1].getY()))))
                            if (intersection(points[i].getX(), points[i].getY(),
                                points[i + 1].getX(), points[i + 1].getY(),
                                points[0].getX(), points[0].getY(),
                                points[j + 1].getX(), points[j + 1].getY()))
                            {
                                isIntersect = true;
                            }
                        }
                        else if (i + 1 == points.Count - 1 && j + 1 == points.Count - 1)
                        {
                            //if (intersection(new Point(Convert.ToInt16(points[0].getX()), Convert.ToInt16(points[0].getY())),
                            //    new Point(Convert.ToInt16(points[i + 1].getX()), Convert.ToInt16(points[i + 1].getY())),
                            //new Point(Convert.ToInt16(points[0].getX()), Convert.ToInt16(points[0].getY())),
                            //    new Point(Convert.ToInt16(points[j + 1].getX()), Convert.ToInt16(points[j + 1].getY()))))
                            if (intersection(points[0].getX(), points[0].getY(),
                                points[i + 1].getX(), points[i + 1].getY(),
                                points[0].getX(), points[0].getY(),
                                points[j + 1].getX(), points[j + 1].getY()))
                            {
                                isIntersect = true;
                            }
                        }
                    }
                }
            }

            //for (int i = 0; i < points.Count - 1; i++)
            //{
            //    trianglesS.Add(triangleS(0, 0, points[i].getX(), points[i].getY(), points[i + 1].getX(), points[i + 1].getY()));
            //}
            //trianglesS.Add(triangleS(0, 0, points[0].getX(), points[0].getY(), points[points.Count - 1].getX(), points[points.Count - 1].getY()));

            ////не знаю, как определить знак площади
            //double S1 = 0;
            //foreach (double s in trianglesS)
            //    S1 += s;
            //S1 = Math.Abs(S1);

            //double S2 = 0;
            //foreach (double s in trianglesS)
            //    S2 += Math.Abs(s);

            if (!isIntersect)
                return String.Format("Многоугольник не самопересекающийся.");
            else
                return String.Format("Многоугольник самопересекающийся.");
        }

        //проверка точки на принадлежность многоугольнику
        public Color provPoint(Point point, int panelHeight)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].setY(panelHeight - points[i].getY());
            }
            int j = 0;
            int l = 0;
            int r = 0;
            int h = 0;
            //проверка принадлежности точки одной из сторон многогранника
            for (int i = 0; i < points.Length; i++)
            {
                if (i == points.Length - 1)
                    j = 1;
                else
                    j = i + 1;
                if ((point.X - points[j].getX()) * (points[i].getY() - points[j].getY()) == ((point.Y - points[j].getY()) * (points[i].getX() - points[j].getX())))
                {
                    if ((points[i].getX() < points[j].getX()) && (points[i].getX() <= point.X) && (point.X <= points[j].getX()) || (points[i].getX() > points[j].getX()) && (points[j].getX() <= point.X) && (point.X <= points[i].getX()))
                    {
                        //paintPoint(point.X, point.Y, PolygonPoint.width, PolygonPoint.height, PolygonPoint.colorInPolygon);
                        return colorInPolygon;
                    }
                }

            }
            //проводим прямую || ox
            //paintLine(point.X, point.Y, GeneralForm.ActiveForm.Width, point.Y);
            //счетчик, записывающий количество пересечений многоугольника лучом
            int count = 0;
            for (int i = 0; i < points.Length; i++)
            {
                if (i == points.Length - 1)
                    j = 1;
                else
                    j = i + 1;
                //проверяем, пересекает ли прямая сторону
                //строгое неравенство, потому что не берется случай, когда прямая проходит через концы отрезка
                if (((point.Y - points[i].getY()) * (point.Y - points[j].getY())) < 0)
                {
                    //проверка с какой стороны точка
                    if (point.X < ((point.Y - points[i].getY()) * (points[j].getX() - points[i].getX())
                        / (points[j].getY() - points[i].getY()) + points[i].getX()))
                    {
                        //если справа, то увеличиваем счетчик
                        count++;
                    }
                }
                else if ((points[i].getY() == points[j].getY()) && (point.Y == points[i].getY()))
                {
                    if (i == 0)
                        l = points.Length - 1;
                    else
                        l = i - 1;
                    if (i == points.Length - 2)
                        r = 0;
                    else
                        r = j + 1;
                    /* \_  или  _/ , тогда инкрементируем с шагом 1
                     *   \     /
                     *   иначе если
                     *   \_/ или _, тогда инкрементируем с шагом 2
                     *          / \
                     */
                    if (((point.Y - points[l].getY()) * (point.Y - points[r].getY())) < 0)
                        count++;
                    else if (((point.Y - points[l].getY()) * (point.Y - points[r].getY())) > 0)
                        count += 2;

                }
                //если же прямая проходит через конец отрезка, то
                //проверяем, как расположены стороны отностельно данной точки
                else if (point.Y == points[i].getY())
                {
                    if (i == 0)
                        h = points.Length - 1;
                    else
                        h = i - 1;
                    /*если стороны расположены:
                     * \ или /
                     * /     \ , то инкрементируем на единицу
                     
                     иначе если
                     * \/ или /\ , то инкрементируем двойку
                     */
                    if (((point.Y - points[h].getY()) * (point.Y - points[j].getY())) < 0)
                        count++;
                    else if (((point.Y - points[h].getY()) * (point.Y - points[j].getY())) > 0)
                        count += 2;
                }
            }
            if (count % 2 == 0) // если четное количество пересечений, то точка снаружи
                //paintPoint(point.X, point.Y, PolygonPoint.width, PolygonPoint.height, PolygonPoint.colorAffilPolygon);
                return colorAffilPolygon;
            else
                //paintPoint(point.X, point.Y, PolygonPoint.width, PolygonPoint.height, PolygonPoint.colorNotAffilPolygon);
                return colorNotAffilPolygon;

        }

        //оперделяет самопересекаемость двух отрезков
        public bool intersection(long x11, long y11, long x12, long y12,
                                long x21, long y21, long x22, long y22)
        {
            isIntersection = ((x21 - x11) * (y12 - y11) - (y21 - y11) * (x21 - x11) *
                (x22 - x11) * (y12 - y11) - (y22 - y11) * (x21 - x11) <= 0)
                &&
                (((x11 - x21) * (y22 - y21) - (y11 - y21) * (x22 - x21)) *
                ((x12 - x21) * (y22 - y21) - (y12 - y21) * (x22 - x21)) <= 0);
            return isIntersection;

        }

        //пересекаются ли отрезки
        static bool intersection(Point start1, Point end1, Point start2, Point end2)
        {
            Point dir1 = new Point();
            dir1.X = end1.X - start1.X;
            dir1.Y = end1.Y - start1.Y;

            Point dir2 = new Point();
            dir2.X = end2.X - start2.X;
            dir2.Y = end2.Y - start2.Y;

            //считаем уравнения прямых проходящих через отрезки
            float a1 = -dir1.Y;
            float b1 = +dir1.X;
            float d1 = -(a1 * start1.X + b1 * start1.Y);

            float a2 = -dir2.Y;
            float b2 = +dir2.X;
            float d2 = -(a2 * start2.X + b2 * start2.Y);

            //подставляем концы отрезков, для выяснения в каких полуплоскотях они
            float seg1_line2_start = a2 * start1.X + b2 * start1.Y + d2;
            float seg1_line2_end = a2 * end1.X + b2 * end1.Y + d2;

            float seg2_line1_start = a1 * start2.X + b1 * start2.Y + d1;
            float seg2_line1_end = a1 * end2.X + b1 * end2.Y + d1;

            //если концы одного отрезка имеют один знак, значит он в одной полуплоскости и пересечения нет.
            if (seg1_line2_start * seg1_line2_end >= 0 || seg2_line1_start * seg2_line1_end >= 0)
                return false;

            //float u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);
            //*out_intersection = start1 + u * dir1;

            return true;

        }


        //площадь треугольника по координатам
        public static double triangleS(long x1, long y1, long x2, long y2, long x3, long y3)
        {
            return 0.5 * ((x1 - x3) * (y2 - y3) - (x2 - x3) * (y1 - y3));
        }

        double sqr(double a)
        {
            return a * a;
        }

        //расстояние между двумя точками
        double distanceBetweenPoints(double ax, double ay, double bx, double by){
            return sqr(ax-bx)+sqr(ay-by);
        }

        //расстояние от точки с координатами (ox; oy) до отрезка [(ax; ay); (bx; by)]
        double distanceBetweenPointAndSegment(long ox, long oy, long ax, long ay, long bx, long by)
        {
            double p, s, a, b, c;

            a = distanceBetweenPoints(ox, oy, ax, ay);
            b = distanceBetweenPoints(ox, oy, bx, by);
            c = distanceBetweenPoints(ax, ay, bx, by);

            if (a >= b + c) return Math.Sqrt(b);
            if (b >= a + c) return Math.Sqrt(a);

            a = Math.Sqrt(a); b = Math.Sqrt(b); c = Math.Sqrt(c);
            p = (a + b + c) / 2;
            s = Math.Sqrt((p - a) * (p - b) * (p - c) * p);

            return s * 2 / c;
        }

        //возвращает квадрат расстояния
        double distanceSqr(long x1, long x2, long y1, long y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }

        //возвращает минимальное расстояние от точки до многоугольника
        public double minimalDistance(long x, long y)
        {
            double[] distances = new double[points.Length];
            for (int i = 0; i < distances.Length; i++)
                if (i + 1 < distances.Length - 1)
                    distances[i] = distanceBetweenPointAndSegment(x, y, 
                                                                  points[i].getX(), points[i].getY(), 
                                                                  points[i + 1].getX(), points[i + 1].getY());
                else
                    distances[i] = distanceBetweenPointAndSegment(x, y,
                                                                  points[i].getX(), points[i].getY(),
                                                                  points[0].getX(), points[0].getY());
            return distances.Min();
        }

        public int getIndexNearestTop(long x, long y)
        {
            int current = -1;
            double minDistance = Double.MaxValue;
            for (int i = 0; i < points.Length; i++)
            {
                double distance = distanceSqr(x, y, points[i].getX(), points[i].getY());
                if (minDistance > distance)
                {
                    minDistance = distance;
                    current = i;
                }
            }
            return current;
        }


    }
}
