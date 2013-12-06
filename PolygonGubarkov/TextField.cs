using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace PolygonGubarkov
{
    class TextField : elementaryObject
    {
        string text = "";
        static bool isError = false;
        public string errorMessage = "";

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

        public TextField(string textfield, string text)
        {
            
            setText(text);
            readStringForThisTextfield(textfield);
            
        }

        public TextField(string text)
        {
            setText(text);
        }

        public TextField(string text, Color color, Point location, string name, bool colorFill)
        {
            setText(text);
            this.setColor(color);
            this.setLocation(location);
            this.setName(name);
            this.colorFill = colorFill;
        }

        new public string toString()
        {
            return String.Format("Name:{0}#Type:{4}#Location:{1}#foreColor:{2}#Text:{3}#backgroundColor:{5}", this.getName(), this.getLocation(), this.getColor(), this.getText(), "TEXTFIELD", this.getBackgroundColor());
        }

        //считывает предыдущую строку, создавая новый полигон и заполняя его параметрами
        
        public void readStringForThisTextfield(string s)
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
                    else if (a.IndexOf("Text:") != -1)
                    {
                        setText(a.Split(':')[1]);
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
                errorMessage = e.Message + "\n" + e.StackTrace;
            }
        }

        public static void paint(Panel p, string text, Point location, Color color)
        {
            Label t = new Label();
            t.Text = text;
            t.ForeColor = color;
            t.Location = location;
            p.Controls.Add(t);
        }
    }
}
