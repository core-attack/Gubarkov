using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Crossroad
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //автомобили
        //enum cars { car = "Автомобиль", public_car = "Общественный транспорт", lorry = "Грузовик"};
        //сигналы светофора
        enum trafficLightsColors : int { STOP, GO };
        /// <summary>
        /// массив светофоров
        /// </summary>
        trafficLightsColors[] trafficLigths = new trafficLightsColors[0];
        /// <summary>
        /// время работы красного сигнала светофора
        /// </summary>
        int[] timeRoadBandRed = new int[0];
        /// <summary>
        /// время работы зеленого сигнала светофора
        /// </summary>
        int[] timeRoadBandGreen = new int[0];
        /// <summary>
        /// Максимальное время работы светофора
        /// </summary>
        int maxTimeForRoad = 60;
        /// <summary>
        /// Минимальное время работы светофора
        /// </summary>
        int minTimeForRoad = 10;
        /// <summary>
        ///Cписок очередей
        /// </summary>
        List<Queue<Vehicle>> queueList;
        /// <summary>
        ///Последний созданный список
        /// </summary>
        ListBox lastListBox;
        Label lastlabel;
        string start = "Начать эксперимент";
        string end = "Закончить эксперимент";
        Random random = new Random();
        //для остановки эксперимента
        bool STOP = false;
        Thread t;

        public MainWindow()
        {
            InitializeComponent();
            trafficLigths = new trafficLightsColors[Convert.ToInt16(textBox1.Text)];
            timeRoadBandRed = new int[Convert.ToInt16(textBox1.Text)];
            timeRoadBandGreen = new int[Convert.ToInt16(textBox1.Text)];
            queueList = new List<Queue<Vehicle>>();
            listBox1.Visibility = labelTest.Visibility = System.Windows.Visibility.Hidden;
            t = new Thread(startExperiment);
        }

        void startExperiment()
        {
            STOP = false;
            generateQueues();
            buttonStart.Content = end;
            Title = "Идет эксперимент...";
            experiment();
            t.Start();
        }

        void stopExperiment()
        {
            STOP = true;
            buttonStart.Content = start;
            t.Interrupt();
            Title = "Эксперимент завершен";
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender is Button)
            {
                if (((Button)sender).Content.Equals(start))
                {
                    startExperiment();
                }
                else if (((Button)sender).Content.Equals(end))
                {
                    stopExperiment();
                }
            }
            
        }

        /// <summary>
        /// Генерирует очереди, заполняя их случайными ТС
        /// </summary>
        void generateQueues()
        {
            Random random = new Random(21);
            lastListBox = null;
            lastlabel = null;
            for (int i = 0; i < trafficLigths.Length; i++)
            {
                Queue<Vehicle> q = new Queue<Vehicle>();
                //наибольшее начальное количество ТС в очереди
                int count = random.Next(Convert.ToInt16(textBoxCount.Text));
                for (int j = 0; j < count; j++)
                {
                    q.Enqueue(Vehicle.getRandomVehicle());
                }
                queueList.Add(q);
                generateListBox(q, i);
            }
        }

        
        /// <summary>
        /// Генерирует список для очереди
        /// </summary>
        void generateListBox(Queue<Vehicle> q, int i)
        {
            ListBox listBox = new ListBox();
            Thickness margin = new Thickness(0, 35, 0, 0);
            if (lastListBox == null)
                margin.Left = 0;
            else
                margin.Left = lastListBox.Margin.Left + Convert.ToInt16(textBoxCount.Text) + listBox1.Width;
            listBox.Name = "generatedlistBox" + i;
            listBox.Margin = margin;
            listBox.Width = listBox1.Width;
            listBox.Height = listBox1.Height;
            listBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            listBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            listBox.Visibility = System.Windows.Visibility.Visible;
            foreach (Vehicle v in q)
                listBox.Items.Add(v.getCarType());

            listBox.DragEnter += new DragEventHandler(listBox1_DragEnter);
            listBox.DragLeave += new DragEventHandler(listBox1_DragLeave);
            listBox.Drop += new DragEventHandler(listBox1_Drop);
            listBox.DragOver += new DragEventHandler(listBox1_DragOver);
            
            lastListBox = listBox;
            Map.Children.Add(listBox);

            Label label = new Label();
            margin = new Thickness(0, 0, 0, 0);
            if (lastlabel == null)
                margin.Left = 0;
            else
                margin.Left = lastlabel.Margin.Left + Convert.ToInt16(textBoxCount.Text) + listBox1.Width;
            label.Name = "generatedLabelForListBox" + i;
            label.Margin = margin;
            label.Content = i;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            label.Visibility = System.Windows.Visibility.Visible;
            lastlabel = label;
            Map.Children.Add(label);
        }

        /// <summary>
        /// Заполняет список поданными значениями
        /// </summary>
        /// <param name="q"></param>
        /// <param name="i"></param>
        void setListBox(Queue<Vehicle> q, int i)
        {
            for (int j = 0; j < Map.Children.Count; j++)
            {
                if (Map.Children[j] is ListBox)
                {
                    if (((ListBox)Map.Children[j]).Name.Equals("generatedlistBox" + i))
                    {
                        ((ListBox)Map.Children[j]).Items.Clear();
                        foreach (Vehicle v in q)
                            ((ListBox)Map.Children[j]).Items.Add(v.getCarType());
                    }
                }
            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            Content = "DragEnter " + sender.GetType().ToString();
        }

        private void listBox1_Drop(object sender, DragEventArgs e)
        {
            Content = "Drop " + sender.GetType().ToString();
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            Content = "DragOver " + sender.GetType().ToString();
        }

        private void listBox1_DragLeave(object sender, DragEventArgs e)
        {
            Content = "DragLeave " + sender.GetType().ToString();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            stopExperiment();
            queueList.Clear();
            for (int i = 0; i < Map.Children.Count; i++ )
            {
                if (Map.Children[i] is ListBox)
                {
                    if (((ListBox)Map.Children[i]).Name.IndexOf("generatedlistBox") != -1 )
                    {
                        Map.Children.RemoveAt(i);
                        i--;
                    }
                }
                else if (Map.Children[i] is Label)
                {
                    if (((Label)Map.Children[i]).Name.IndexOf("generatedLabelForListBox") != -1)
                    {
                        Map.Children.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        /// <summary>
        /// Экспоненциальное случайная логическая величина
        /// </summary>
        /// <returns></returns>
        bool expBool()
        {
            int j = random.Next(1000);
            if (j % 3 == 0)
                return true;
            else
                return false;
        }

        //время должно распределяться по экспоненциальной случайной величине(кнут 2 том)
        /// <summary>
        /// Приход автомобилей в очереди
        /// </summary>
        void newCars()
        {
            int max = 2;
            int countCars = random.Next(max);
            int numberQueue = random.Next(trafficLigths.Length);
            for (int i = 0; i < trafficLigths.Length; i++)
            {

                if (expBool())//здесь должно быть случайное значение true/false с экспоненциальное распределение
                {
                    
                    countCars = random.Next(max);
                    numberQueue = random.Next(trafficLigths.Length);
                    for (int j = 0; j < countCars; j++)
                        queueList[numberQueue].Enqueue(Vehicle.getRandomVehicle());
                    setListBox(queueList[numberQueue], numberQueue);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueIndex"></param>
        void newCars(int queueIndex)
        {
            int max = 3;
            int countCars = random.Next(max);
            for (int j = 0; j < countCars; j++)
                queueList[queueIndex].Enqueue(Vehicle.getRandomVehicle());
            setListBox(queueList[queueIndex], queueIndex);
        }

        /// <summary>
        /// Задает время работы каждого из светофоров
        /// </summary>
        void setTimeForTrafficLigths()
        {
            for (int i = 0; i < trafficLigths.Length; i++)
            {
                timeRoadBandRed[i] = random.Next(minTimeForRoad, maxTimeForRoad);
                timeRoadBandGreen[i] = maxTimeForRoad - timeRoadBandRed[i];
            }
        }

        /// <summary>
        /// Модель работы перекрестка
        /// </summary>
        void experiment()
        {
            try
            {
                //задать время работы каждого из светофоров
                setTimeForTrafficLigths();
                //задать начальные значения для цветов светофора
                for (int i = 0; i < trafficLigths.Length; i++)
                {
                    if (expBool())
                        trafficLigths[i] = trafficLightsColors.GO;
                    else
                        trafficLigths[i] = trafficLightsColors.STOP;
                }
                //создать и запустить таймер
                DateTime start = DateTime.Now;
                TimeSpan currentTime;
                //for (int i = 0; i < trafficLigths.Length; i++)
                //{

                //}
                while (!STOP)
                {
                    //автомобили уходят из очереди только в зеленый сигнал своей линии и только, если успевают проехать перекресток
                    //выделить количество потоков, для которых горит зеленый
                    int[] greenIndexes = getGreenQueue();
                    //выделить количество автомобилей, которые могут проехать за отведенное время 
                    for (int i = 0; i < queueList.Count; i++)
                    {
                        //запомнили количество автомобилей, на случай, если оно изменится
                        int countVehicle = queueList[i].Count;
                        for (int j = 0; j < queueList[i].Count; j++)
                        {
                            currentTime = DateTime.Now - start;
                            if (queueList[i].Peek().getTimeForCrossroad() < currentTime.Seconds)
                            {
                                queueList[i].Dequeue();
                            }
                        }
                        //автомобили добавляются в очередь все время через случайное количество времени
                        newCars(i);
                    }

                    Thread.Sleep(100);
                }
                Title += "Время эксперимента " + (DateTime.Now - start);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// Изменяет свет светофоров на противоположный
        /// </summary>
        void checkTrafficLigth()
        {
            
        }

        /// <summary>
        /// Возвращает массив индексов очередей, для которых горит зеленый свет
        /// </summary>
        /// <returns></returns>
        int[] getGreenQueue()
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < trafficLigths.Length; i++)
            {
                if (trafficLigths[i] == trafficLightsColors.GO)
                    indexes.Add(i);
            }
            return indexes.ToArray();
        }


        private void buttonStep_Click(object sender, RoutedEventArgs e)
        {
            newCars();
        }
        
    }
}
