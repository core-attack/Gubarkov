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
        Point location = new Point();
        string name = "";
        Color color = Color.Black;

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

        public Color getColor()
        {
            return color;
        }

        public void setColor(Color c)
        {
            color = c;
        }

    }
}
