using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MolecularDynamic
{
    class Atom
    {
        int id = 0;
        double radius = 0;
        double weight = 0;
        double speed = 0;
        Color color;
        //среднее квадратичное отклонение
        double middleQuadraSpeed = 0;
        //координаты расположения атома в плоскости x, y
        Point coordinate = new Point(100, 100);
        int Z = 0; //координата высоты молекулы относительно крышки бокса
        //вектор направления движения
        Point direction = new Point();
        //событие столкновения
        bool crash = false;
        //с чем столкнулась молекула
        string[] global_states = new string[] { "TOP", "RIGHT", "BOTTOM", "LEFT", "OTHER" };
        Dictionary<string, string> states = new Dictionary<string, string>();
        string state = "";
        public Atom(int id, double radius, double weight, double speed, double middleQuadraSpeed, Point coordinate, int z, Point direction, Color color)
        {
            this.id = id;
            this.radius = radius;
            this.weight = weight;
            this.speed = speed;
            this.middleQuadraSpeed = middleQuadraSpeed;
            this.coordinate.X = coordinate.X;
            this.coordinate.Y = coordinate.Y;
            this.Z = z;
            this.direction.X = direction.X;
            this.direction.Y = direction.Y;
            this.color = color;
            states.Add("top", global_states[0]);
            states.Add("right", global_states[1]);
            states.Add("bottom", global_states[2]);
            states.Add("left", global_states[3]);
            states.Add("other", global_states[4]);
        }

        public bool isEqual(Atom a)
        {
            if (coordinate.X == a.getCoordinates().X && coordinate.Y == a.getCoordinates().Y)
                if (direction.X == a.getCoordinates().X && direction.Y == a.getCoordinates().Y)
                    if (Z == a.getZ())
                        if (radius == a.getRadius() && speed == a.getSpeed())
                            if (middleQuadraSpeed == a.getMiddleQuadraSpeed())
                                return true;
            return false;
        }

        public int getId()
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public Color getColor()
        {
            return color;
        }

        //событие столкновения с крышкой 
        public void isCrashWithTop()
        {
            state = states["top"];
        }

        //событие столкновения с крышкой 
        public void isCrashWithLeft()
        {
            state = states["left"];
        }

        //событие столкновения с крышкой 
        public void isCrashWithRight()
        {
            state = states["right"];
        }

        //событие столкновения с крышкой 
        public void isCrashWithBottom()
        {
            state = states["bottom"];
        }

        //событие столкновения с крышкой 
        public void isCrashWithOther()
        {
            state = states["other"];
        }

        public string getState()
        {
            return state;
        }

        public void setIsCrashed()
        {
            crash = true;
        }

        public void setNotCrashed()
        {
            crash = false;
        }

        public bool isCrashed()
        {
            return crash;
        }

        public Point getDirection()
        {
            return direction;
        }

        public void setDirection(int x, int y)
        {
            direction.X = x;
            direction.Y = y;
        }

        public Point getCoordinates()
        {
            return coordinate;
        }

        public void setCoordinates(int x, int y)
        {
            coordinate.X = x;
            coordinate.Y = y;
        }

        public int getZ()
        {
            return this.Z;
        }

        public void setZ(int z)
        {
            this.Z = z;
        }

        public double getRadius()
        {
            return this.radius;
        }

        public void setRadius(double radius)
        {
            this.radius = radius;
        }

        public double getWeight()
        {
            return this.weight;
        }

        public void setWeight(double weight)
        {
            this.weight = weight;
        }

        public double getSpeed()
        {
            return this.speed;
        }

        public void setSpeed(double speed)
        {
            this.speed = speed;
        }

        public double getMiddleQuadraSpeed()
        {
            return this.middleQuadraSpeed;
        }

        public void setMiddleQuadraSpeed(double middleQuadraSpeed)
        {
            this.middleQuadraSpeed = middleQuadraSpeed;
        }

    }
}
