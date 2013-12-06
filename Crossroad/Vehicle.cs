using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossroad
{
    //позаботиться о сборке мусора

    /// <summary>
    /// Транспортное средство
    /// </summary>
    class Vehicle
    {
        //типы транспортных средств (далее ТС)
        static string[] types = { "Автомобиль", "Автомобиль", "Автомобиль", "Автомобиль", "Автомобиль", "Автомобиль", "Автомобиль","Общественный транспорт", "Общественный транспорт", "Общественный транспорт", "Грузовик", "Грузовик"};
        static Random random = new Random();

        //тип автомобиля
        string carType = "";
        //количество занимаемых мест в полосе
        int countPlaces = 0;
        //количество времени, необходимое для прохождения перекрестка
        int timeForCrossroad = 0;

        public Vehicle(string carType, int countPlaces, int timeForCrossroad)
        {
            setCarType(carType);
            setCountPlaces(countPlaces);
            setTimeForCrossroad(timeForCrossroad);
        }

        public void setCarType(string carType)
        {
            this.carType = carType;
        }

        public void setCountPlaces(int countPlaces)
        {
            this.countPlaces = countPlaces;
        }

        public void setTimeForCrossroad(int time)
        {
            this.timeForCrossroad = time;
        }

        public string getCarType()
        {
            return carType;
        }

        public int getCountPlaces()
        {
            return countPlaces;
        }

        public int getTimeForCrossroad()
        {
            return timeForCrossroad;
        }

        /// <summary>
        /// Возвращает количество занимаемых мест в полосе для определенного типа ТС
        /// </summary>
        /// <param name="index">Индекс типа ТС в массиве types</param>
        /// <returns></returns>
        static int getCountPlasesForType(int index)
        {
            if (index <= 6)
                return 1;
            else if (index > 6 && index <= 9)
                return 2;
            else if (index >= 9 && index <= 11)
                return 2;
            return 0;
        }

        /// <summary>
        /// Возвращает время прохождения перекрестка для определенного типа ТС
        /// </summary>
        /// <param name="index">Индекс типа ТС в массиве types</param>
        /// <returns></returns>
        static int getTimeForCrossroadForType(int index)
        {
            if (index <= 6)
                return 8;
            else if (index > 6 && index <= 9)
                return 10;
            else if (index >= 9 && index <= 11)
                return 12;
            return 0;
        }

        /// <summary>
        /// Возвращает случайное транспортное средство
        /// </summary>
        /// <returns></returns>
        public static Vehicle getRandomVehicle()
        {
            int indexType = random.Next(types.Length);
            return new Vehicle(types[indexType], getCountPlasesForType(indexType), random.Next(types.Length));
        }
    }
}
