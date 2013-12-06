using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace MolecularDynamic
{
    class Logic
    {
        //очередь событий
        EventQueue queue;
        //значения параметров молекулы по умолчанию
        //вес
        double ATOM_WEIGHT = 0;
        //скорость
        double ATOM_SPEED = 0;
        //радиус
        double ATOM_RADIUS = 0;
        //среднее квадратичное отклонение
        double ATOM_MIDDLE_QUADRA_SPEED = 0;
        //количество молекул
        int COUNT_ATOMS = 0;
        //ящик
        //Box box;
        //размеры ящика
        static int BOX_WIDTH = 0;
        static int BOX_LENGTH = 0;
        static int BOX_HEIGHT = 0;
        //массив молекул
        Atom[] atoms;
        int DIRECTION_RANGE_X = 0;
        int DIRECTION_RANGE_Y = 0;
        Color COLOR;
        //количество столкновений с верхней стенкой
        public int countOfCrashes = 0;
        public EventQueue eq;

        //конструктор для задания начальных параметров молекул
        public Logic(bool generate_atoms, double atom_radius, double atom_weight, double atom_speed, double atom_middleQuadraSpeed,
            int count_atoms, int boxWidth, int boxLength, int boxHeight, int direction_range_x, int direction_range_y, Color color)
        {
            ATOM_RADIUS = atom_radius;
            ATOM_WEIGHT = atom_weight;
            ATOM_SPEED = atom_speed;
            ATOM_MIDDLE_QUADRA_SPEED = atom_middleQuadraSpeed;
            COUNT_ATOMS = count_atoms;
            BOX_WIDTH = boxWidth;
            BOX_HEIGHT = boxHeight;
            BOX_LENGTH = boxLength;
            DIRECTION_RANGE_X = direction_range_x;
            DIRECTION_RANGE_Y = direction_range_y;
            this.COLOR = color;
            if (generate_atoms)
            {
                generateAtoms(count_atoms);
            }
            eq = new EventQueue(count_atoms);
        }

        //создает атомы
        void generateAtoms(int count)
        {
            atoms = new Atom[count];
            queue = new EventQueue(count);
            Random coord = new Random();
            // кучность расположения молекул
            int border = 10;
            for (int i = 0; i < count; i++)
            {
                COLOR = Color.FromArgb(coord.Next(Int32.MaxValue));
                Atom a = new Atom(i, ATOM_RADIUS, ATOM_WEIGHT, ATOM_SPEED, ATOM_MIDDLE_QUADRA_SPEED, 
                    new Point(coord.Next(border, BOX_WIDTH - border), coord.Next(border, BOX_HEIGHT - border)), 0,
                    new Point(coord.Next(0, DIRECTION_RANGE_X), coord.Next(0, DIRECTION_RANGE_Y)), COLOR);
                a.setId(i);
                atoms[i] = a;
            }
        }

        public Atom[] getAtoms()
        {
            return atoms;
        }

        //определяет расстояние между двумя точками
        double distance(Atom a1, Atom a2)
        {
            return Math.Sqrt(Math.Pow(a1.getCoordinates().X - a2.getCoordinates().X, 2) 
                - Math.Pow(a1.getCoordinates().Y - a2.getCoordinates().Y, 2));
        }

        //реакция на столкновение атомов (считаем расстояние между центрами шаров)
        public void AtomCrash()
        {
            foreach (Atom a1 in atoms)
            {
                foreach (Atom a2 in atoms)
                {
                    if (!a1.isEqual(a2))
                    {
                        double len = distance(a1, a2);
                        if (len <= a1.getRadius() + a2.getRadius())
                        {
                            //создать событие столкновения
                            //записать его время
                            Event e = new Event(a1.getId(), a2.getId(), DateTime.Now);
                            //прописать состояние столкновения в шариках
                            a1.isCrashed();
                            a2.isCrashed();
                            //обработка события
                            atomsCrash(a1, a2);
                        }
                    }

                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
            }
        }
        
        //вычисление проекции вектора на ось абсцисс
        double AtomAxisXProjection(Atom a)
        {
            return Math.Sqrt(a.getCoordinates().X * a.getCoordinates().X 
                + a.getCoordinates().Y * a.getCoordinates().Y) * getCosAxisX(a);
        }

        //вычисление проекции вектора на ось ординат
        double AtomAxisYProjection(Atom a)
        {
            return Math.Sqrt(a.getCoordinates().X * a.getCoordinates().X 
                + a.getCoordinates().Y * a.getCoordinates().Y) * getCosAxisY(a);
        }

        //обработка столкновения со стенкой
        //вычисление отраженного вектора
        void AtomReDirection(Atom a)
        {

            /*нумерация стен
             *  1
             *4   2
             *  3
             * 
             * 
             * 
             */
            Event e = new Event();
            Point c = a.getCoordinates();
            if (c.X + a.getRadius() <= 0 )
            {
                a.setDirection(-a.getDirection().X, a.getDirection().Y);
                e = new Event(a.getId(), 4, DateTime.Now);
                a.isCrashWithLeft();
                a.setCoordinates(0, a.getCoordinates().Y);
            }
            else if (c.X + a.getRadius() >= BOX_WIDTH)
            {
                a.setDirection(-a.getDirection().X, a.getDirection().Y);
                e = new Event(a.getId(), 2, DateTime.Now);
                a.isCrashWithRight();
                a.setCoordinates(BOX_WIDTH, a.getCoordinates().Y);
            }
            if (c.Y + a.getRadius() <= 0) 
            {
                a.setDirection(a.getDirection().X, -a.getDirection().Y);
                e = new Event(a.getId(), 1, DateTime.Now);
                a.isCrashWithBottom();
                a.setCoordinates(a.getCoordinates().X, 0);
            }
            else if (c.Y + a.getRadius() >= BOX_HEIGHT)
            {
                a.setDirection(a.getDirection().X, -a.getDirection().Y);
                e = new Event(a.getId(), 3, DateTime.Now);
                a.isCrashWithTop();
                a.setCoordinates(a.getCoordinates().X, BOX_HEIGHT);
                countOfCrashes++;
            }
            if (e.getAtomId() != -1)
                eq.setEvent(e);
        }

        double oldX = 0;
        double oldY = 0;
        //сколько раз координаты повторились (не стоит ли точка на месте)
        int countCoordRepeat = 0;
        //меняет координаты расстояния, которое пройдет молекула с заданной скоростью по заданному вектору
        void setCoordValues(Atom a)
        {
            double x = AtomAxisXProjection(a) * a.getSpeed() 
                / Math.Sqrt(a.getDirection().X * a.getDirection().X + a.getDirection().Y * a.getDirection().Y);
            double y = AtomAxisYProjection(a) * a.getSpeed() 
                / Math.Sqrt(a.getDirection().X * a.getDirection().X + a.getDirection().Y * a.getDirection().Y);
            if (x >= oldX - 1 && x <= oldX + 1 || y >= oldY - 1 && y <= oldY + 1)
            //if (Math.Abs(x - oldX) < 0.001 && Math.Abs(y - oldY) < 0.001)
                countCoordRepeat++;
            
            if (countCoordRepeat > 3)
            {
                Random r = new Random();
                countCoordRepeat = 0;
                x = r.Next(BOX_WIDTH);
                y = r.Next(BOX_HEIGHT); 
            }
            a.setCoordinates(a.getCoordinates().X + Convert.ToInt16(Math.Round(x) + 1), 
                a.getCoordinates().Y + Convert.ToInt16(Math.Round(y) + 1)); 
            //единицу прибавляем, потому что при значениях вариаций координат, меньших 0.5, молекулы начинают стоять на месте
            oldX = x;
            oldY = y;
        }
        //движение молекулы за один промежуток времени
        public void moveAtom(Atom a, out string error)
        {
            error = "";
            try
            {
                AtomReDirection(a);
                setCoordValues(a);
                /*  ищем попарное столкновение с минимальным временем: 
                 *  либо ни одного события 
                 *  либо столкновение есть, но его время больше времени первого события в очереди
                 *  либо столкновение есть и его время меньше времени первого события в очереди
                 */
                Event eMolecularCrashes = getAtomsCrash();
                Event eFirst = eq.getForwardEvent();
                if (eFirst != null)
                {

                }

                /*  ищем столкновения между шариками
                 *  если его время меньше, чем время первого события в очереди (прогнозируемые события столкновения 
                 *  в недалеком будущем), то обрабатываем его
                 */
                if (eMolecularCrashes != null && eFirst != null && eMolecularCrashes.getTime() < eFirst.getTime())
                {
                    //AtomCrashEvent ace = new AtomCrashEvent(eMolecularCrashes.)
                    //atomsCrash();
                }
                else /*иначе обрабатываем столкновение шарика со стенкой*/
                {
                    AtomReDirection(a);
                    setCoordValues(a);
                }
                
            }
            catch (Exception e)
            {
                error = e.Message + "\n" + e.StackTrace;
            }
        }

        //ищет попарное столкновение шариков с наименьшим временем
        Event getAtomsCrash()
        {
            return null;
        }

        //ищет столкновения 

        //определение косинуса угла межу вектором и осью абсцисс 
        double getCosAxisX(Atom a)
        {
            double x = a.getDirection().X;
            double y = a.getDirection().Y;
            double gip = Math.Sqrt(x * x + y * y);

            if (x >= 0 && y >= 0 || x >= 0 && y < 0)
                return x / gip;
            return -1 + (x / gip);
        }

        //определение косинуса угла межу вектором и осью ординат
        double getCosAxisY(Atom a)
        {
            double x = a.getDirection().X;
            double y = a.getDirection().Y;
            double gip = Math.Sqrt(x * x + y * y);

            if (x >= 0 && y >= 0 || x < 0 && y >= 0)
                return 1 - x / gip;
            return -1 + (x / gip);
        }

        //определение новых скоростей молекул после столкновения
        void atomsCrash(Atom a1, Atom a2)
        {
            double v1 = (1 / (a1.getWeight() + a2.getWeight())) * ((a1.getWeight() - 
                a2.getWeight()) * a1.getSpeed() + 2 * a2.getWeight() * a2.getSpeed());
            double v2 = (1 / (a1.getWeight() + a2.getWeight())) * ((a2.getWeight() -
                a1.getWeight()) * a2.getSpeed() + 2 * a1.getWeight() * a1.getSpeed());
            atoms[a1.getId()].setSpeed(v1);
            atoms[a2.getId()].setSpeed(v2);
            //если шарики столкнулись горизонтально, отражаются по оси Оy иначе по Оx
            if (a1.getCoordinates().Y >= a2.getCoordinates().Y - a2.getRadius()
                && a1.getCoordinates().Y <= a2.getCoordinates().Y + a2.getRadius())
            {
                atoms[a1.getId()].setDirection(-a1.getDirection().X, a1.getDirection().Y);
                atoms[a2.getId()].setDirection(-a2.getDirection().X, a2.getDirection().Y);
            }
            else
            {
                atoms[a1.getId()].setDirection(a1.getDirection().X, -a1.getDirection().Y);
                atoms[a2.getId()].setDirection(a2.getDirection().X, -a2.getDirection().Y);
            }
        }

        //импульс атома, столькнувшегося с крышкой
        static double Impuls(Atom a)
        {
            return 2 * a.getSpeed() * 0.1 * a.getWeight() * 0.001 * BOX_HEIGHT;

        }

        Atom getAtom(int id)
        {
            foreach (Atom a in atoms)
                if (a.getId() == id)
                    return a;
            return null;
        }

        public double getPressure(long deltaT /*время*/)
        {
            return pressure(getAtom(eq.getForwardEvent().getAtomId()), deltaT);
        }

        //вычисление давления
        public static double pressure(/*double[] impulses*/ Atom a /*импульсы*/, long deltaT /*время*/)
        {
            double press = 0;
            /*for (int i = 0; i < impulses.Length; i++)
            {
                press += impulses[i] / (S * deltaT);
            }
            return press;*/
            return Impuls(a) / (BOX_LENGTH * BOX_WIDTH * deltaT);

        }

        /*
         * зарегулировать перекресток таким образом, чтобы повысить его пропускную способность
         * у каждой полосы свой светофор
         * максимальное время обслуживания и минимальное
         * 
         * сеть массового обслуживания (несколько очередей, переходить из одной очереди в другую)
         * обслуживания перекрестка - один цикл цветов
         * типы ТС (по величине)
         * вероятность появления в потоке, время обслуживания (прохождения светофора)
         * 
         */


    }
}
