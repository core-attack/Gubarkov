using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PolygonGubarkov
{
    public partial class GeneralForm : Form
    {
        enum types { POLYGON, TEXTFIELD, COMPONENT_OBJECT};
        enum LOGGER { INFO, DEBUG, WANR, ERROR };

        //для создаваемых элементов
        Polygon newPolygon;
        Label newLabel;
        TextField newTextField;

        Point point = new Point();
        public int oldMouseX = 0;
        public int oldMouseY = 0;

        //вершины вводимые пользователем для создания полигона
        List<PolygonPoint> points = new List<PolygonPoint>();

        bool isPolygonCreate = false;
        //передвинули ли лейбл
        bool isLabelMove = false;
        //режим ввода (либо режим создания многоугольников, либо режим навигации)
        bool isPolygonCreatingRegime = true;
        Polygons polygons = new Polygons();

        //текущий цвет
        Color currentColor;
        //цвет близости курсора к многоугольнику
        Color colorClose = Color.Green;
        //цвет несамопересекающегося прямоугольника
        Color colorDefault = Color.Blue;
        //цвет смопересекающегося прямоугольника
        Color colorintersect = Color.Red;

        //зажата ли левая клавиша мыши
        bool isLeftMouseKeyChecked = false;
        //индекс редактируемого полигона
        int indexOfEditedPolygon = -1;
        //добавляемый объект
        string currentAddObject = "";

        //списки созданных на форме объектов
        //List<Label> labelList;
        //List<Polygon> polygonList;
        //List<TextField> textFieldList;
        //List<Pictogram> pictogramList;

        //единый массив для хранения данных
        elementaryObject[] index;
        MapElement[] elements;
        int countTextField = 0;
        int countPolygons = 0;


        //текущий редактируемый элемент
        Object currentElement;
        Label currentLabel;
        Polygon currentPolygon;

        //создается или сохраняется ли элемент
        bool elementCreate = true;
        //редактируется ли элемент
        bool elementEdit = false;
        
        //текст по умолчанию в текстбоксе редактирования текста
        string default_text = "Введите текст...";

        Encoding encoding = Encoding.UTF8;

        string filename = "";

        Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();

        public GeneralForm()
        {
            InitializeComponent();
            setLog(LOGGER.DEBUG, "Инициализация компонентов...");
            help.Text = "Подсказка: Вводите вершины многоугольника левой клавишей мыши. Для остановки нажмите правую клавишу мыши.";
            setLog(LOGGER.DEBUG, "Создание типов объектов...");
            objectsTypes.Items.Add(types.POLYGON);
            objectsTypes.Items.Add(types.TEXTFIELD);
            objectsTypes.Items.Add(types.COMPONENT_OBJECT);
            objectsTypes.SelectedIndex = 0;
            textBoxEdit.Text = default_text;
            setLog(LOGGER.DEBUG, "Создание цветов...");
            setColorsToComboBox();
            setLog(LOGGER.DEBUG, "Выделение памяти...");
            //labelList = new List<Label>();
            //polygonList = new List<Polygon>();
            //textFieldList = new List<TextField>();
            setLog(LOGGER.DEBUG, "Инициализация завершена успешно.");
            index = new elementaryObject[1000];
            elements = new MapElement[1000];
        }

        /// <summary>
        /// Возвращает первый свободный индекс в массиве index
        /// </summary>
        /// <returns></returns>
        int getFirstFreeIndex()
        {
            for (int i = 0; i < index.Length; i++)
                if (index[i] == null)
                    return i;
            return -1;
        }

        void setPoint(MouseEventArgs e)
        {
            PolygonPoint p = new PolygonPoint(e.X, /*panelGraph.Height - */e.Y, e.X, e.Y);
            points.Add(p);
        }

        void setPoint(int x, int y, int paintX, int paintY)
        {
            PolygonPoint p = new PolygonPoint(x, y, paintX, paintY);
            points.Add(p);
        }

        PolygonPoint getOldPoint()
        {
            if (points.Count != 0)
                return points[points.Count - 1];
            return null;
        }

        //рисует точку на форме
        void paintPoint(int x, int y, int w, int h, Color c)
        {
            Graphics g = panelGraph.CreateGraphics();
            Pen p = new Pen(c);
            g.DrawEllipse(p, x, y, w, h);
            p.Dispose();
            g.Dispose();
        }

        //рисует линию на форме
        void paintLine(MouseEventArgs e)
        {
            //if (!isPolygonCreate)
            if (points.Count > 0)
            {
                Graphics g = panelGraph.CreateGraphics();
                Pen p = new Pen(Color.FromName(colorComboBox.Text));
                Point[] ps = new Point[2];
                ps[0].X = Convert.ToInt16(getOldPoint().getX());
                ps[0].Y = Convert.ToInt16(getOldPoint().getY());
                ps[1].X = e.X;
                ps[1].Y = e.Y;
                g.DrawCurve(p, ps);
                p.Dispose();
                g.Dispose();
            }
        }
        //рисует линию на форме
        void paintLine(long x1, long y1, long x2, long y2, Color color)
        {
            if (!isPolygonCreate)
            {
                Graphics g = panelGraph.CreateGraphics();
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
        }

        void polygonIsCreated()
        {
            if (points.Count > 0)
            {
                paintLine(points[0].getX(), points[0].getY(), points[points.Count - 1].getX(), points[points.Count - 1].getY(), Color.FromName(colorComboBox.Text));
                isPolygonCreate = true;
                currentPolygon = new Polygon(points.ToArray(), Color.FromName(colorComboBox.Text));
                GeneralForm.ActiveForm.Text = currentPolygon.provPolygon(points);
                if (affiliation.Checked)
                    help.Text = "Проверка: введите точку левой кнопкой мыши";
            }
        }

        //рисует точку на панели, определяя её цвет принадлежностью многограннику
        void paintPoint(MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
            //paintLine(point.X, point.Y, panelGraph.Width, point.Y);
            paintPoint(point.X, point.Y, PolygonPoint.width, PolygonPoint.height, currentPolygon.provPoint(point, panelGraph.Height));
                //Polygon.provPoint(point, points, panelGraph.Height));
        }

        //очищает форму, обнуляет все вершины
        void ClearForm()
        {
            panelGraph.Invalidate();
            panelGraph.Controls.Clear();
            points.Clear();
            isPolygonCreate = false;
            editedPolygon.Items.Clear();
            elements = null;
            index = null;
            //polygonList.Clear();
            //textFieldList.Clear();
        }

        private void clearToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ClearForm();
        }

        //переключение лабораторок
        private void selfintersectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                //самопересечение
                if (((ToolStripMenuItem)sender).Name.Equals("selfIntersection"))
                {
                    ((ToolStripMenuItem)sender).Checked = true;
                    affiliation.Checked = false;
                    polygonsItem.Checked = false;
                    pointDistance.Checked = false;
                    polygonEdit.Checked = false;
                }
                //принадлежность точки многограннику
                else if (((ToolStripMenuItem)sender).Name.Equals("affiliation"))
                {
                    ((ToolStripMenuItem)sender).Checked = true;
                    selfIntersection.Checked = false;
                    polygonsItem.Checked = false;
                    pointDistance.Checked = false;
                    polygonEdit.Checked = false;
                }
                //ввод нескольких многоугольников
                else if (((ToolStripMenuItem)sender).Name.Equals("polygonsItem"))
                {
                    ((ToolStripMenuItem)sender).Checked = true;
                    selfIntersection.Checked = false;
                    affiliation.Checked = false;
                    pointDistance.Checked = false;
                    polygonEdit.Checked = false;
                }
                //кратчайшее растояние от точки до многогранников
                else if (((ToolStripMenuItem)sender).Name.Equals("pointDistance"))
                {
                    ((ToolStripMenuItem)sender).Checked = true;
                    selfIntersection.Checked = false;
                    affiliation.Checked = false;
                    polygonsItem.Checked = false;
                    polygonEdit.Checked = false;
                }
                //редактирование многогранника
                else if (((ToolStripMenuItem)sender).Name.Equals("polygonEdit"))
                {
                    ((ToolStripMenuItem)sender).Checked = true;
                    selfIntersection.Checked = false;
                    affiliation.Checked = false;
                    polygonsItem.Checked = false;
                    pointDistance.Checked = false;
                }
            }
        }
        //переключение режима
        private void polygonCreating_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                //самопересечение
                if (((ToolStripMenuItem)sender).Name.Equals("polygonCreating"))
                {
                    ((ToolStripMenuItem)sender).Checked = true;
                    isPolygonCreatingRegime = true;
                    navigation.Checked = false;
                    //rebuildPolygons.Checked = false;
                    polygonEditItem.Checked = false;
                }
                //принадлежность точки многограннику
                else if (((ToolStripMenuItem)sender).Name.Equals("navigation"))
                {
                    ((ToolStripMenuItem)sender).Checked = true;
                    isPolygonCreatingRegime = false;
                    polygonCreating.Checked = false;
                    //rebuildPolygons.Checked = false;
                    polygonEditItem.Checked = false;
                }
                else if (((ToolStripMenuItem)sender).Name.Equals("polygonEditItem"))
                {
                    ((ToolStripMenuItem)sender).Checked = true;
                    isPolygonCreatingRegime = false;
                    polygonCreating.Checked = false;
                    navigation.Checked = false;

                }
            }
        }

        bool isInPolygon(MouseEventArgs e)
        {
            List<Polygon> polygons_ = polygons.getPolygons();
            for (int i = 0; i < polygons_.Count; i++)
            {
                Color color = polygons_[i].provPoint(new Point(e.X, e.Y), panelGraph.Height);
                if (color != Polygon.colorNotAffilPolygon)
                    return true;
            }
            return false;
        }

        //перерисовывает все многоугольники
        void reBuildAllPolygons()
        {
            if (index != null)
                for (int i = 0; i < index.Length; i++)
                {
                    if (index[i] != null)
                        if (index[i].getType().Equals("POLYGON"))
                            Polygon.paint(panelGraph, elements[index[i].getIndex()].getPoints(), index[i].getColor());
                }
        }

        private void panelGraph_MouseHover(object sender, EventArgs e)
        {
            reBuildAllPolygons();
        }

        private void rebuildPolygonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reBuildAllPolygons();
        }

        private void panelGraph_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            }

        private void panelGraph_MouseDown(object sender, MouseEventArgs e)
        {
            isLeftMouseKeyChecked = true;
            //MessageBox.Show("down");
        }

        private void panelGraph_MouseUp(object sender, MouseEventArgs e)
        {
            isLeftMouseKeyChecked = false;
            //MessageBox.Show("up");
        }

        private void editedPolygon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (editedPolygon.SelectedIndex >= 0)
                indexOfEditedPolygon = editedPolygon.SelectedIndex;
        }

        private void objectsTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentAddObject = objectsTypes.Text;
        }

        void buildObject(string type, MouseEventArgs e)
        {
            switch (currentAddObject)
            {
                case "POLYGON": { buildPolygon(e.X, e.Y); } break;
                case "TEXTFIELD": { buildLabel(e.X, e.Y); } break;
                case "COMPONENT_OBJECT": { buildComponentObject(e.X, e.Y); } break;
                case "NONE":{} break;
            }
        }

        //добавляет подпись на форму
        void buildLabel(int x, int y)
        {
            Label l = new Label();
            l.Name = "polygonLabel" + countTextField;
            l.Text = "Введите текст";
            l.Location = new Point(x, y);
            l.MouseClick += new MouseEventHandler(label1_MouseClick);
            l.DragDrop += new DragEventHandler(label1_DragDrop);
            l.MouseDoubleClick += new MouseEventHandler(label1_MouseDoubleClick);
            l.Visible = false;
            textBoxEdit.Visible = true;
            textBoxEdit.Location = l.Location;
            currentLabel = l;
            currentElement = l;
            l.ForeColor = Color.FromName(colorComboBox.Text);
            //labelList.Add(l);
            panelGraph.Controls.Add(l);
        }

        //добавляет ломанную на форму
        void buildPolygon(int x, int y)
        {
 
        }

        //добавляет составной объект на форму
        void buildComponentObject(int x, int y)
        {
 
        }

        private void editTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setLabelText(textBoxEdit.Text);
            }
        }

        //ищет на форме соответствующую label и меняет её текст
        void setLabelText(string text)
        {
            for (int i = 0; i < panelGraph.Controls.Count; i++)
            {
                if (panelGraph.Controls[i].Name == currentLabel.Name)
                {
                    panelGraph.Controls[i].Text = text;
                }
            }
        }

        //изменяет свойство текст у полигона, по которому нажали правой кнопкой
        void changeTextLabel(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ((Label)sender).Text = textBoxEdit.Text;
                currentLabel = ((Label)sender);
            }
        }

        //событие для реакции на нажатие по надписи
        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            //changeTextLabel(sender, e);
            if (sender is Label)
            {
                currentLabel = ((Label)sender);
                currentElement = ((Label)sender);
            }
        }

        //задает все цвета
        void setColorsToComboBox()
        {
            Color[] colors = { Color.Black, Color.Blue, Color.Coral, Color.DarkViolet, Color.Red, Color.Yellow, Color.Green };
            for (int i = 0; i < colors.Length; i++)
            {
                colorComboBox.Items.Add(colors[i].Name);
                toolStripComboBoxBrushes.Items.Add(colors[i].Name);
            }
            colorComboBox.SelectedIndex = 0;
            toolStripComboBoxBrushes.SelectedIndex = 0;
            brushes.Add(colors[0].Name, Brushes.Black);
            brushes.Add(colors[1].Name, Brushes.Blue);
            brushes.Add(colors[2].Name, Brushes.Coral);
            brushes.Add(colors[3].Name, Brushes.DarkViolet);
            brushes.Add(colors[4].Name, Brushes.Red);
            brushes.Add(colors[5].Name, Brushes.Yellow);
            brushes.Add(colors[6].Name, Brushes.Green);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {

        }
        //перетаскиваем лейбл
        private void label1_DragDrop(object sender, DragEventArgs e)
        {
            if (sender is Label)
                ((Label)sender).Location = new Point(e.X, e.Y);
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //isLabelMove = !isLabelMove;
            //((Label)sender).Location = new Point(e.X, e.Y);
            if (sender is Label)
            {
                currentLabel = ((Label)sender);
                currentElement = ((Label)sender);
                textBoxEdit.Visible = true;
                currentLabel.Visible = false;
                textBoxEdit.Location = currentLabel.Location;
            }
            
                
        }

        //по нажатию на кнопку заканчивается редактирование элемента
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            
        }

        private void textBoxEdit_VisibleChanged(object sender, EventArgs e)
        {
            textBoxEdit.SelectAll();
        }

        private void textBoxEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxEdit.Visible = false;
                newTextField.setText(textBoxEdit.Text);
                setLabelText(textBoxEdit.Text);
                textBoxEdit.Text = default_text;
                currentLabel.Visible = true;
            }
        }

        //создает новый елемент на форме
        void createOrSaveNewElement()
        {
            try
            {
                /*
                * Создаем элемент
                */
                if (elementCreate)
                {
                    switch (currentAddObject)
                    {
                        case "POLYGON":
                            {
                                newPolygon = new Polygon(currentColor);
                                elementEdit = true;
                                currentElement = newPolygon;
                                setLog(LOGGER.INFO, "Создём ломанную...");
                            }
                            break;
                        case "TEXTFIELD":
                            {
                                newTextField = new TextField("");
                                elementEdit = true;
                                currentElement = newTextField;
                                currentLabel = new Label();
                                setLog(LOGGER.INFO, "Создём подпись...");
                                reBuildAllPolygons();
                            }
                            break;
                        case "COMPONENT_OBJECT":
                            {
                                setLog(LOGGER.INFO, "Создём составной объект...");
                            }
                            break;
                        case "NONE": 
                            {
                                setLog(LOGGER.INFO, "Ничего не делаем...");
                            } 
                        break;
                    }

                    создатьToolStripMenuItem.Text = "Сохранить";
                    elementCreate = false;
                }
                /*
                * Сохраняем созданный элемент
                */
                else
                {

                    int i = getFirstFreeIndex();
                    switch (currentAddObject)
                    {
                        case "POLYGON":
                            {
                                //newPolygon.setPoints(points.ToArray());
                                //polygonList.Add(new Polygon(newPolygon.getPoints(), newPolygon.getColor(), newPolygon.getLocation(), newPolygon.getName(), newPolygon.colorFill));
                                index[i] = new elementaryObject(i, currentAddObject, newPolygon.getLocation(), newPolygon.getColor(), newPolygon.getBackgroundColor(), newPolygon.getName());
                                elements[i] = new MapElement(currentAddObject, points.ToArray());
                                newPolygon = null;
                                elementEdit = false;
                                currentElement = null;
                                points.Clear();
                                setLog(LOGGER.INFO, "Сохранили ломанную.");
                            }
                            break;
                        case "TEXTFIELD":
                            {
                                //newTextField.setText(textBoxEdit.Text);
                                //newTextField.setColor(Color.FromName(colorComboBox.Text));
                                //textFieldList.Add(new TextField(newTextField.getText(), newTextField.getColor(), newTextField.getLocation(), newTextField.getName(), newTextField.colorFill));
                                index[i] = new elementaryObject(i, currentAddObject, newTextField.getLocation(), newTextField.getColor(), newTextField.getBackgroundColor(), newTextField.getName());
                                elements[i] = new MapElement(currentAddObject, textBoxEdit.Text);
                                elementEdit = false;
                                currentElement = null;
                                textBoxEdit.Visible = false;
                                Label l = new Label();
                                l.Location = newTextField.getLocation();
                                l.ForeColor = newTextField.getColor();
                                l.Text = newTextField.getText();
                                panelGraph.Controls.Add(l);
                                newTextField = null;
                                setLog(LOGGER.INFO, "Сохранили подпись \"" + l.Text + "\".");
                                textBoxEdit.Text = default_text;
                            }
                            break;
                        case "COMPONENT_OBJECT":
                            {
                                setLog(LOGGER.INFO, "Сохранили составной объект.");
                            }
                            break;
                        case "NONE":
                            {
                                setLog(LOGGER.INFO, "Ничего не делаем...");
                            } 
                            break;
                    }

                    создатьToolStripMenuItem.Text = "Создать";
                    elementCreate = true;
                }
            }
            catch (Exception ex)
            {
                setLog(LOGGER.ERROR, ex.Message + "\n" + ex.StackTrace);
            }
        }

        //изменяет выделенный элемент
        void changeCurrentElement()
        {
            try
            {
                if (elementEdit)
                {
                    редактироватьToolStripMenuItem.Text = "Закончили редактировать";
                    elementEdit = false;
                }
                else
                {
                    редактироватьToolStripMenuItem.Text = "Редактировать";
                    elementEdit = true;
                }
            }
            catch (Exception ex)
            {
                setLog(LOGGER.ERROR, ex.Message + "\n" + ex.StackTrace);
            }
        }

        //удаляет выделенный элемент с формы
        void deleteCurrentElement()
        {
            try
            {
                for (int i = 0; i < panelGraph.Controls.Count; i++)
                {
                    if (panelGraph.Controls[i] is Label && currentElement is Label)
                    {
                        panelGraph.Controls.RemoveAt(i);
                    }
                }   
            }
            catch (Exception ex)
            {
                setLog(LOGGER.ERROR, ex.Message + "\n" + ex.StackTrace);
            }
        }

        /*
        int index1 = 0;

        private void GeneralForm_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                GeneralForm.ActiveForm.Text = "Polygion " + e.X + ":" + (panelGraph.Height - e.Y);
                if (polygons.getPolygons().Count != 0)
                    if (polygonsItem.Checked)
                    {
                        if (!isPolygonCreatingRegime)
                        {
                            if (isInPolygon(e))
                                GeneralForm.ActiveForm.Text = "Точка в многограннике";
                            else
                                GeneralForm.ActiveForm.Text = "Точка не в многограннике";
                        }
                    }
                    else if (pointDistance.Checked)
                    {
                        double minimalDistance = Double.MaxValue;
                        int index = 0;
                        for (int i = 0; i < polygons.getPolygons().Count; i++)
                        {
                            double distance = polygons.getPolygons()[i].minimalDistance(e.X, e.Y);
                            if (minimalDistance > distance)
                            {
                                minimalDistance = polygons.getPolygons()[i].minimalDistance(e.X, e.Y);
                                index = i;
                            }
                        }
                        GeneralForm.ActiveForm.Text = "Расстояние до ближайшего многогранника: " + minimalDistance;
                        if (index != index1)
                        {
                            for (int i = 0; i < polygons.getPolygons().Count; i++)
                            {
                                if (i == index)
                                    polygons.getPolygons()[index].setColor(Color.Yellow);
                                else
                                    polygons.getPolygons()[index].setColor(colorDefault);
                            }
                            reBuildAllPolygons();
                            index1 = index;
                        }
                    }
                    //редактировать многогранники
                    else if (polygonEdit.Checked && navigation.Checked)
                    {

                        
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }*/

        //достраивает последнее ребро (соединяет первую и последнюю вершины)
        /*private void GeneralForm_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("click");
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (isPolygonCreatingRegime)
                {
                    if (!isPolygonCreate)
                    {
                        polygonIsCreated();
                        if (polygonsItem.Checked || pointDistance.Checked || polygonEdit.Checked)
                        {
                            polygons.getPolygons().Add(currentPolygon);
                            points.Clear();
                            isPolygonCreate = false;
                            editedPolygon.Items.Add("Polygon " + polygons.getPolygons().Count);
                        }
                    }
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //режим создания многогранника
                if (isPolygonCreatingRegime)
                {
                    if (!isPolygonCreate)
                    {
                        if (points.Count > 0)
                        {
                            paintLine(e);
                        }
                        setPoint(e);
                    }
                    else
                    {
                        paintPoint(e);
                    }
                    buildObject(currentAddObject, e);
                }
                //режим его редактирования
                else if (polygonEditItem.Checked)
                {
                    //пока только изменять вершины
                    //определить к какой вершине ближе был клик
                    //изменить координаты этой вершины
                    if (isLeftMouseKeyChecked)
                    {
                        try
                        {
                            if (editedPolygon.SelectedIndex >= 0)
                            {
                                //нужен метод, возвращающий ближайшую вершину
                                int index = polygons.getPolygons()[indexOfEditedPolygon].getIndexNearestTop(e.X, e.Y);
                                if (index != -1)
                                {
                                    polygons.getPolygons()[indexOfEditedPolygon].getPoints()[index].setXY(e.X, e.Y);
                                    reBuildAllPolygons();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                        }
                    }

                }
                if (objectsTypes.Text.Equals("Нет"))
                {
 
                }
            }
        }*/

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createOrSaveNewElement();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteCurrentElement();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeCurrentElement();
            
        }

        /*
         * 
         * ниже будут обработаны события мыши для формы 
         * 
         *       
         */

        //одинарный клик
        void mouseClick(object sender, MouseEventArgs e)
        { 
            

        }

        //перетаскивание 
        void mouseDragDrop(object sender, MouseEventArgs e)
        {
        }

        private void colorComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                currentColor = Color.FromName(colorComboBox.Text);
                if (currentElement is Polygon)
                {
                    ((Polygon)currentElement).setColor(currentColor);
                }
                else if (currentElement is TextField)
                {
                    ((TextField)currentElement).setColor(currentColor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                setLog(LOGGER.ERROR, ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void panelGraph_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentElement is Polygon)
            {
                ActiveForm.Text = "Текущий элемент: " + types.POLYGON + " " + ((Polygon)currentElement).getName();
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    paintLine(e);
                    setPoint(e);
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
 
                }
                
            }
            else if (currentElement is TextField)
            {
                ActiveForm.Text = "Текущий элемент: " + types.TEXTFIELD + " " + ((TextField)currentElement).getText();
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    textBoxEdit.Location = new Point(e.X, e.Y);
                    textBoxEdit.Visible = true;

                    newTextField.setLocation(textBoxEdit.Location);

                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {

                }
            }
        }

        //замыкает ломанную
        void enclosePolygon()
        {
            try
            {
                if (currentElement != null)
                    if (currentElement is Polygon)
                    {
                        ((Polygon)currentElement).isEnclose = true;
                        paintLine(points[points.Count - 1].getX(),
                                    points[points.Count - 1].getY(),
                                    points[0].getX(),
                                    points[0].getY(), Color.FromName(colorComboBox.Text));
                    }
            }
            catch (Exception e)
            {
                setLog(LOGGER.ERROR, e.Message + "\n" + e.StackTrace);
            }
        }

        private void замкнутьЛоманнуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enclosePolygon();
        }

        //записывает в лог сообщение
        void setLog(string message)
        {
            richTextBoxLog.Text += message + "\n";
        }

        void setLog(LOGGER type, string message)
        {
            richTextBoxLog.Text += type.ToString() + ": " + message + "\n";
        }

        /// <summary>
        /// Показывает массивы данных
        /// </summary>
        /// <returns></returns>
        string getDataArrays()
        {
            try
            {
                string result = "";
                for (int i = 0; i < index.Length; i++)
                {
                    if (index[i] != null)
                        result += "index#" + index[i].toString() + "\n";
                }
                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i] != null)
                        result += "element#" + elements[i].toString() + "\n";
                }
                return result;
            }
            catch (Exception e)
            {
                setLog(LOGGER.ERROR, e.Message + "\n" + e.StackTrace);
                return "";
            }
            /*
            result += "polygonList: ";
            foreach (Polygon p in polygonList)
            {
                result += String.Format("Name:{0} Location:{1} Color:{2}\n", p.getName(), p.getLocation(), p.getColor());
                string pPoints = "";
                foreach (PolygonPoint pp in p.getPoints())
                {
                    pPoints += pp.getX() + ":" + pp.getY() + ", ";
                }
                pPoints = pPoints.Substring(0, pPoints.Length - 2);
                result += "Points: " + pPoints + ".\n";
            }

            result += "textFieldList: ";
            foreach (TextField t in textFieldList)
            {
                result += String.Format("Name:{0} Location:{1} Color:{2} Text:{3}\n", t.getName(), t.getLocation(), t.getColor(), t.getText());
            }
            */
           
        }

        private void показатьМассивыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setLog(getDataArrays());
        }

        /*формат карты:
         * MapName:mapname
         * LastSaveDate:date
         * Polygons:
         * - Name:name, Location:x;y, Color:color, Points:{x;y x;y x;y x;y}
         * - Name:name, Location:x;y, Color:color, Points:{x;y x;y x;y x;y}
         * - Name:name, Location:x;y, Color:color, Points:{x;y x;y x;y x;y}
         * TextFields:
         * - Name:name, Location:x;y, Color:color, Text:text
         * - Name:name, Location:x;y, Color:color, Text:text
         * - Name:name, Location:x;y, Color:color, Text:text
         * #
         
         */
        /// <summary>
        /// Сохраняет в файл все массивы
        /// </summary>
        /// <param name="filename"></param>
        void saveToTxt(string filename)
        {

            string result = "";
            try
            {
                result += "MapName:" + filename + "\n";
                result += "LastSaveDate:" + DateTime.Now.ToString() + "\n";
                result += getDataArrays() + "\n";   
                File.WriteAllLines(filename, result.Split('\n'), encoding);
            }
            catch(Exception e)
            {
                MessageBox.Show("Не удалось сохранить файл", e.Message + "\n" + e.StackTrace);
                setLog(LOGGER.ERROR, "Не удалось сохранить файл\n" + e.Message + e.StackTrace);
            }
            
        }

        // сохраняет только имя файла
        string shortName(string file)
        {
            char[] sep = { '\\' };
            string[] shortName = file.Split(sep);
            return shortName[shortName.Length - 1];
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = saveFileDialog1;
                saveFileDialog1.Filter = "Текстовый файл|*.txt|Все форматы|*.*";
                saveFileDialog1.Title = "Сохранить карту как...";
                char[] sep = { '\\' };
                sfd.FileName = this.filename;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                    switch (strFilExtn)
                    {
                        case "txt":
                            saveToTxt(fileName);
                            break;
                        default:
                            break;
                    }
                    setLog(LOGGER.INFO, "Данные сохранены в файл " + fileName);
                }
            }
            catch (Exception ex)
            {
                setLog(LOGGER.ERROR, ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Перестраивает карту
        /// </summary>
        /// <param name="panel"></param>
        void rebuildMap(Panel panel) {
            for (int i = 0; i < index.Length; i++)
            {
                if (index[i] != null)
                {
                    if (index[i].getType().Equals("POLYGON"))
                    {
                        Polygon.paint(panelGraph, elements[i].getPoints(), index[i].getColor());
                    }
                    else if (index[i].getType().Equals("TEXTFIELD"))
                    {
                        TextField.paint(panelGraph, elements[i].getText(), index[i].getLocation(), index[i].getColor());
                    }
                }
            }
        }

        int getIndex(string s)
        {
            try
            {
                return Convert.ToInt16(s.Split(';')[0].Split(':')[1]);
            }
            catch (Exception e)
            {
                setLog(LOGGER.ERROR, "Не удалось открыть файл\n" + e.Message + e.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// Открытие файла
        /// </summary>
        /// <param name="filename"></param>
        void myOpen(string filename) {
            try
            {
                index = new elementaryObject[1000];
                elements = new MapElement[1000];
                string[] text = File.ReadAllLines(filename, encoding);
                this.filename = filename;
                int j = 0;
                foreach (string s in text)
                {
                    if (s != "" && s.IndexOf("MapName") == -1 && s.IndexOf("LastSaveDate") == -1)
                        if (s.IndexOf("index#") != -1)
                        {
                            //Polygon pol = new Polygon(s);
                            int i = getIndex(s);
                            index[i] = new elementaryObject(s.Split('#')[1]);
                            if (!index[i].hasError())
                            {
                                setLog(LOGGER.INFO, "Добавлен индекс...");
                            }
                            else
                                setLog(LOGGER.ERROR, index[i].errorMessage);
                        }
                        else if (s.IndexOf("element#") != -1)
                        {
                            //int i = getFirstFreeIndex();
                            elements[j] = new MapElement(s.Split('#')[1]);

                            if (!elements[j].hasError())
                            {
                                setLog(LOGGER.INFO, "Добавлен элемент...");
                            }
                            else
                                setLog(LOGGER.ERROR, elements[j].errorMessage);
                            j++;
                        }
                }
                rebuildMap(panelGraph);
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось открыть файл", e.Message + "\n" + e.StackTrace);
                setLog(LOGGER.ERROR, "Не удалось открыть файл\n" + e.Message + e.StackTrace);
            }

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = openFileDialog1;
            saveFileDialog1.Filter = "Текстовый файл|*.txt|Все форматы|*.*";
            saveFileDialog1.Title = "Открыть карту...";
            char[] sep = { '\\' };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFilExtn = ofd.FileName.Remove(0, ofd.FileName.Length - 3);
                switch (strFilExtn)
                {
                    case "txt":
                        myOpen(ofd.FileName);
                        break;
                    default:
                        break;
                }
                setLog(LOGGER.INFO, "Открыт файл " + ofd.FileName);
            }
        }

        Brush getBrush()
        {
            switch (toolStripComboBoxBrushes.Text)
            {
                //case "Black": Brushes.Black; break;
                //case "Red": Brushes.Black; break;
                //case "Blue": Brushes.Black; break;
                //case "Green": Brushes.Black; break;
                //case "Black": Brushes.Black; break;
            }
            return Brushes.BlanchedAlmond;
        }

        void solidFill(Color color)
        {
            try
            {
                Graphics g = panelGraph.CreateGraphics();
                Brush b = brushes[colorComboBox.Text];
                Point[] pa = new Point[points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    pa[i] = new Point((int)points[i].getX(), (int)points[i].getY());
                }
                if (currentElement is Polygon)
                {
                    if (pa != null)
                        g.FillPolygon(b, pa);
                }

            }
            catch (Exception e)
            {
                setLog(LOGGER.ERROR, e.Message + "\n" + e.StackTrace);
            }

        }

        private void залитьВыбраннымЦветомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            solidFill(Color.FromName(colorComboBox.Text));
        }

        private void перестроитьКартуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rebuildMap(panelGraph);
        }
            
        }
}
