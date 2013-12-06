using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1Gunarcov
{
    class Program
    {
        static int weight = 0;
        static int height = 0;
        static int r1 = 0;
        static int r2 = 0;
        static int r3 = 0;
        static int r4 = 0;

        static int k = 0;
        static int N = 1000;

        static void Main(string[] args)
        {
            try
            {
                weight = setParam("Enter weight...");
                height = setParam("Enter height");
                r1 =  setParam("Enter r1");
                r2 =  setParam("Enter r2");
                r3 =  setParam("Enter r3");
                r4 =  setParam("Enter r4");

                Random r = new Random();
                double x = 0;
                double y = 0;

                for (int i = 0; i < N; i++) 
                {
                    x = r.Next(0, weight) + r.NextDouble();
                    y = r.Next(0, height) + r.NextDouble();
                    //если попало не в 4 угловых квадрата
                    if (x >= 0 && x <= r1 && y >= r1 && y <= height - r4
                        ||
                        x >= r1 && x <= weight - r2 && y >= 0 && y <= height
                        ||
                        x >= weight - r2 && x <= weight && y >= r2 && y <= height - r3
                        )
                        k++;
                    //если попало в закругленный угол
                    else if (Math.Sqrt((x - r1) * (x - r1) + (y - r1) * (y - r1)) < r1 * r1 ||
                        Math.Sqrt((x - (weight - r2)) * (x - (weight - r2)) + (y - r2) * (y - r2)) < r2 * r2 ||
                        Math.Sqrt((x - (weight - r3)) * (x - (weight - r3)) + (y - (height - r3)) * (y - (height - r3))) < r3 * r3 ||
                        Math.Sqrt((x - r4) * (x - r4) + (y - (height - r4)) * (y - (height - r4))) < r4 * r4)
                        k++;
                }

                Console.WriteLine("Calculating method Monte-Carlo...");

                Console.WriteLine("Количество попаданий: {0},\n  Всего: {1},\n Вероятность: {2}\n", k, N, Convert.ToDouble(Convert.ToDouble(k) / Convert.ToDouble(N)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
            Console.ReadKey();
        }

        static int setParam(string message)
        {
            int a = -1;
            do
            {
                Console.WriteLine(message);
                a = Convert.ToInt16(Console.ReadLine());
            }
            while (a < 0);
            return a;
        }
    }
}
