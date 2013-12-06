using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ZedGraph;

namespace MolecularDynamic
{
    public partial class Form1 : Form
    {
        //основной поток
        Thread general;
        //побочный поток
        Thread second;
        System.Globalization.NumberFormatInfo format;
        bool STOP = false;
        int delay = 0;
        Logic logic;

        TimeSpan timeStart;
        TimeSpan timeEnd;
        //отрезок времени, который собирается информация
        TimeSpan timeDelta;

        public Form1()
        {
            InitializeComponent();
            format = new System.Globalization.NumberFormatInfo();
            format.NumberDecimalSeparator = ",";
            resizePanelBox();
        }

        void generateAtoms()
        {
            Random r = new Random();
            logic = new Logic(true, get(textBoxRadius), get(textBoxWeight), getDouble(textBoxSpeed),
                get(textBoxMQS), get(textBoxCount), get(textBoxWidth), get(textBoxLength), get(textBoxHeight), get(textBox_dirRangeX),
                get(textBox_dirRangeY), Color.Red);
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            
            buttonGo.Enabled = false;
            buttonStop.Enabled = true;
            try
            {
                //general = new Thread(live);
                second = new Thread(moveAtoms);
                timeDelta = new TimeSpan(get(textBoxDelta));
                //timer1.Enabled = true;
                //timer1.Start();
                Graphics g = panelBox.CreateGraphics();
                Brush b = new SolidBrush(Color.Red);
                Pen pen = new Pen(b);
                Atom[] atoms;
                atoms = logic.getAtoms();

                timeStart = new TimeSpan(DateTime.Now.Ticks);

                second.Start();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то не так", ex.Message + "\n" + ex.StackTrace);
            }
        }

        void moveAtoms()
        {
            try
            {
                Graphics g = panelBox.CreateGraphics();
                Brush b;
                string error = "";
                Atom[] atoms;
                atoms = logic.getAtoms();
                timeStart = new TimeSpan(DateTime.Now.Ticks);
                DateTime time;
                while (!STOP || error.Equals(""))
                {
                    time = DateTime.Now;
                    clearPanel(panelBox);
                    g = panelBox.CreateGraphics();
                    
                    foreach (Atom a in atoms)
                    {
                        b = new SolidBrush(a.getColor());
                        logic.moveAtom(a, out error);
                        
                        //paintFillPoint(g, b, a.getCoordinates().X, a.getCoordinates().Y, Convert.ToInt16(a.getRadius()));
                        g.FillEllipse(b, a.getCoordinates().X, a.getCoordinates().Y, Convert.ToInt16(a.getRadius()), Convert.ToInt16(a.getRadius()));
                        
                    }
                    logic.AtomCrash();
                    buildGraph();
                    //g.Dispose();


                    //int i = 0;
                    //while (i < delay)
                    //    i++;
                    
                }
                if (!error.Equals(""))
                    MessageBox.Show(error);
            }
            catch {
                MessageBox.Show("Ой");
            }
            
        }

        //остановка молекул
        void death()
        {
            STOP = true;
            second.Abort();
            //timer1.Stop();
            timeEnd = new TimeSpan(DateTime.Now.Ticks);
        }

        int get(TextBox t)
        {
            try
            {
                return Convert.ToInt16(t.Text);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message + "\n" + e.StackTrace);
            }
            return 1;
        }

        double getDouble(TextBox t)
        {
            try
            {
                return Convert.ToDouble(t.Text, format);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace);
                return -1;
            }
        }

        private void textBoxWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        //рисуем все точки
        void paintPoint(Graphics g, Pen pen, int x, int y, int radius)
        {
            g.DrawEllipse(pen, x, y, radius, radius);
        }

        void paintFillPoint(Graphics g, Brush b, int x, int y, int radius)
        {
            g.FillEllipse(b, x, y, radius, radius);
        }
        //очищаем панель
        void clearPanel(Panel p)
        {
            p.Invalidate();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            death();
            timer1.Enabled = false;
            buttonGo.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //live();
        }

        private void panelBox_Resize(object sender, EventArgs e)
        {
            resizePanelBox();
        }

        void resizePanelBox()
        {
            textBoxLength.Text = panelBox.Height.ToString();
            textBoxWidth.Text = panelBox.Width.ToString();
        }

        private void textBoxDelay_TextChanged(object sender, EventArgs e)
        {
            delay = Convert.ToInt16(textBoxDelta.Text);
            generateAtoms();
        }

        private void textBoxWeight_TextChanged(object sender, EventArgs e)
        {
            generateAtoms();
        }

        private void textBoxSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBoxSpeed_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void textBoxSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox)
            {
                if (e.KeyCode == Keys.Down)
                {
                     if (getDouble((TextBox)sender) - 0.1 > 0)
                        ((TextBox)sender).Text = (getDouble((TextBox)sender) - 0.1).ToString(format);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    if (getDouble((TextBox)sender) + 0.1 > 0)
                        ((TextBox)sender).Text = (getDouble((TextBox)sender) + 0.1).ToString(format);
                }
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox)
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (getDouble((TextBox)sender) - 1 > 0)
                        ((TextBox)sender).Text = (getDouble((TextBox)sender) - 1).ToString(format);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    if (getDouble((TextBox)sender) + 1 > 0)
                        ((TextBox)sender).Text = (getDouble((TextBox)sender) + 1).ToString(format);
                }
            }
        }

        /*
         *  
         * Обработка рисования графика
         * 
         */
        PointPairList list = new PointPairList();

        long getMSeconds(DateTime t)
        {
            return t.Hour * 3600 * 1000
                 + t.Minute * 60 * 1000
                 + t.Second * 1000
                 + t.Millisecond;
        }

        long ticks = 0;
        
        void buildGraph()
        {
            try
            {
                // Получим панель для рисования
                GraphPane pane = zedGraphControl1.GraphPane;
                // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
                pane.CurveList.Clear();
                // Создадим список точек
                TimeSpan time = new TimeSpan(DateTime.Now.Ticks);
                int value = time.CompareTo(timeStart);
                if (value > 0 && !logic.eq.empty())
                {
                    TimeSpan cur = time - timeStart;
                    list.Add(ticks, logic.getPressure(cur.Milliseconds));
                    ticks++;
                    //for (double i = -2  * Math.PI; i < 2 * Math.PI; i += 1.0 / 1376.0)
                    //{
                    //    PointPair p = new PointPair(i, Math.Log(Math.Tan(i)));
                    //    list.Add(p);
                    //}

                    // Устанавливаем интересующий нас интервал по оси X
                    pane.XAxis.Scale.Min = 0;
                    pane.XAxis.Scale.Max = 100;

                    // !!!
                    // Устанавливаем интересующий нас интервал по оси Y
                    pane.YAxis.Scale.Min = 0;
                    pane.YAxis.Scale.Max = 100;

                    // Создадим кривую, в которую входит разрыв
                    LineItem myCurve = pane.AddCurve("", list, Color.Blue, SymbolType.None);

                    // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                    // В противном случае на рисунке будет показана только часть графика, 
                    // которая умещается в интервалы по осям, установленные по умолчанию
                    //zedGraphControl1.AxisChange();

                    // Обновляем график
                    zedGraphControl1.Invalidate();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace);
            }
        }

    }
}
