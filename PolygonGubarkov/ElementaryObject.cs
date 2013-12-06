using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PolygonGubarkov
{
    //недо абстрактный класс, от которого наследуются все остальные объекты
    class elementaryObject
    {
        int index = -1;
        Point location = new Point();
        string TYPE = "";
        string name = "";
        //залит ли цветом
        public bool colorFill = false;
        Color foreColor = Color.Black;
        Color backgroundColor = Color.White;
        bool isError = false;
        public string errorMessage = "";

        public elementaryObject()
        { }

        public elementaryObject(int index, string type, Point location, Color color, Color backgroundColor, string name)
        {
            setLocation(location);
            setColor(color);
            setBackgroundColor(backgroundColor);
            setName(name);
            setType(type);
            setIndex(index);
        }

        public elementaryObject(string s)
        {
            setProperties(s);
        }

        /// <summary>
        /// Экранирует все свойства класса в виде строки
        /// </summary>
        /// <returns></returns>
        public string toString() {
            return String.Format("index:{0}; location:{1}; type:{2}; name:{3}; foreColor:{4}; colorFill:{5}; backgroundColor:{6}; isError:{7}; errorMessage:{8}",
                index, location, TYPE, name, foreColor, colorFill, backgroundColor, isError, errorMessage);
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
                    if (a.IndexOf("index:") != -1)
                    {
                        setIndex(Convert.ToInt16(a.Split(':')[1]));
                    }
                    else if (a.IndexOf("location:") != -1)
                    {
                        //Location:{X=0,Y=0}
                        string[] location = a.Split('{')[1].Split('}')[0].Split(',');
                        int x = Convert.ToInt16(location[0].Split('=')[1]);
                        int y = Convert.ToInt16(location[1].Split('=')[1]);
                        setLocation(new Point(x, y));
                    }
                    else if (a.IndexOf("type:") != -1)
                    {
                        setType(a.Split(':')[1]);
                    }
                    else if (a.IndexOf("name:") != -1)
                    {
                        setName(a.Split(':')[1]);
                    }
                    else if (a.IndexOf("foreColor:") != -1)
                    {
                        setColor(Color.FromName(a.Split(':')[1]));
                    }
                    else if (a.IndexOf("colorFill:") != -1)
                    {
                        colorFill = Convert.ToBoolean(a.Split(':')[1]);
                    }
                    else if (a.IndexOf("backgroundColor:") != -1)
                    {
                        setBackgroundColor(Color.FromName(a.Split(':')[1]));
                    }
                    else if (a.IndexOf("isError:") != -1)
                    {
                        isError = Convert.ToBoolean(a.Split(':')[1]);
                    }
                    else if (a.IndexOf("errorMessage:") != -1)
                    {
                        errorMessage += a.Split(':')[1];
                    }
                }
            }
            catch (Exception e)
            {
                isError = true;
                errorMessage += e.Message + "\n" + e.StackTrace + "\n";
            }
        }


        public int getIndex()
        {
            return index;
        }

        public void setIndex(int index)
        {
            this.index = index;
        }

        public Point getLocation()
        {
            return location;
        }

        public void setLocation(Point p)
        {
            location = p;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getType()
        {
            return TYPE;
        }

        public void setType(string type)
        {
            this.TYPE = type;
        }

        public Color getColor()
        {
            return foreColor;
        }

        public void setColor(Color c)
        {
            this.foreColor = c;
        }

        public Color getBackgroundColor()
        {
            return foreColor;
        }

        public void setBackgroundColor(Color c)
        {
            this.backgroundColor = c;
        }

        public bool hasError()
        {
            return isError;
        }
        

    }
}
