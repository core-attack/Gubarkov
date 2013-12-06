using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleMatchProbability
{
    class Program
    {///необходимо вычислить вероятность того, что брошенная под произвольным углом в произвольное место спичка, попадет на сетку, 
     ///состоящую из параллельных прямых, расположенных друг от друга на расстоянии длины спички

        
        static void Main(string[] args)
        {
            ConsoleKeyInfo cont = new ConsoleKeyInfo('\n', ConsoleKey.Enter, false, false, false);
            ConsoleKeyInfo now;
            do
            {
                System.Globalization.NumberFormatInfo format;
                format = new System.Globalization.NumberFormatInfo();
                format.NumberDecimalSeparator = ".";
                double maxX = read("Введите длину плоскости...", format);
                double maxY = read("Введите ширину плоскости...", format);
                double side = read("Введите длину спички...", format);
                long count = readInt("Введите количество итераций...");

                //Console.WriteLine("{0}%{1}={2}", maxX, maxY, rest(maxX, maxY));

                //угол касания спичкой плоскости
                //если угол 0, то учитывается вся длина спички, иначе только её точка касания
                int angle = 0;
                //координаты центра спички на плоскости
                double x = 0;
                double y = 0;

                go(angle, x, y, side, count, maxX, maxY);
                Console.WriteLine("");
                Console.WriteLine("Для продолжения нажмите \"Enter\", для выхода нажмите любую другую клавишу.");
                now = Console.ReadKey();
            }
            while (now.Key == cont.Key);
        }

        static void go(int angle, double x, double y, double len, long count, double mX, double mY)
        {
            Random r = new Random();
            int good = 0;
            for (int i = 0; i < count; i++)
            {
                angle = r.Next(0, 180);
                //сли длина или ширина меньше единицы, отбрасываем целочисленный рандом
                x = r.Next(0, Convert.ToInt16(mX)) + r.NextDouble();
                y = r.Next(0, Convert.ToInt16(mY)) + r.NextDouble();

                //определяем область между двумя параллельными прямыми, куда попал центр отрезка и работаем с этой областью
                if (toHit(angle, x, y, len, count, mX, mY))
                    good++;

            }
            Console.WriteLine("Количество испытаний: {0}\n Количество успехов: {1}\n Вероятность: {2}", count, good, Convert.ToDouble(good) / Convert.ToDouble(count));
        }

        static bool toHit(int angle, double x, double y, double side, long count, double mX, double mY)
        {
            double x1 = 0;
            double x2 = 0;
            double y1 = 0;
            double y2 = 0;
            topCalc(out x1, out y1, out x2, out y2, angle, x, y, side);

            //остатки от деления на длину спички
            double x1Rest = x1 % side;
            double x2Rest = x2 % side;
            //целая часть от деления на длину спички
            double x1Div = rest(x1 - x1Rest, side);
            double x2Div = rest(x2 - x2Rest, side);
            //если центр попал на одну из параллельных прямых
            if (x % side == 0)
                return true;
            //если целые части от деления на расстояние между прямыми не равны, то спичка пересекает одну из прямых
            else if (x1Div != x2Div)
                return true;
            else if (x1Div == x2Div && angle == 0)
                return true;

            return false;
        }

        //вычисляет координаты вершин отрезка
        static void topCalc(out double x1, out double y1, out double x2, out double y2, int angle, double x, double y, double side)
        {
            double h_side = side / 2;
            //прилежащий катет
            double byKat = h_side * Math.Cos(angle);
            //протеволежащий катет
            double ofKat = h_side * Math.Sin(angle);
            x1 = x - byKat;
            x2 = x + byKat;
            y1 = y - ofKat;
            y2 = y + ofKat;
        }

        static double read(string mes, System.Globalization.NumberFormatInfo format)
        {
            double result = 0.0;
            do
            {
                Console.WriteLine(mes);
                try
                {
                    result = Convert.ToDouble(Console.ReadLine(), format);
                }
                catch (Exception e)
                {
                    Console.WriteLine("");
                    Console.WriteLine("ERROR");
                    Console.WriteLine("Неверный формат числа\n" + e.Message + "\n" + e.StackTrace);
                    Console.WriteLine("");
                    result = -1;
                }
            }            
            while (result <= 0);
            return result;
        }

        static long readInt(string mes)
        {
            long result = 0;
            do
            {
                Console.WriteLine(mes);
                result = Convert.ToInt64(Console.ReadLine());
            }
            while (result <= 0);
            return result;
        }

        //остаток от деления дробного числа на дробное
        static int rest(double a, double b)
        {
            int count = 0;
            double rest = a;
            if (a < b)
                return 0;
            while (rest > 0 && rest >= b)
            {
                count++;
                rest -= b;
            }
            return count;
        }
    }
}
