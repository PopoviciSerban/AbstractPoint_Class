using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractPoint_Class
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    abstract public class AbstractPoint
    {
        public enum PointRepresentation { Polar, Rectangular }

        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract double r { get; set; }
        public abstract double A { get; set; }

        public void Move(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        public void Rotate(double angle)
        {
            A += angle;
        }

        public override string ToString() => $"({X}, {Y}) [{r}, {A}]";

        protected static double RadiusXY(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        protected static double AngleXY(double x, double y)
        {
            return Math.Atan2(y, x);
        }

        protected static double XRadiusAngle(double radius, double angle)
        {
            return radius * Math.Cos(angle);
        }

        protected static double YRadiusAngle(double radius, double angle)
        {
            return radius * Math.Sin(angle);
        }
    }

    public class Point : AbstractPoint
    {
        private double radius, angle;

        public Point(PointRepresentation p, double a, double b)
        {
            if (p == PointRepresentation.Polar)
            {
                radius = a;
                angle = b;
            }

            if (p == PointRepresentation.Rectangular)
            {
                radius = RadiusXY(a, b);
                angle = AngleXY(a, b);
            }
        }

        public override double X
        {
            get { return XRadiusAngle(radius, angle); }

            set
            {
                double yBefore = YRadiusAngle(radius, angle);

                angle = AngleXY(value, yBefore);
                radius = RadiusXY(value, yBefore);
            }
        }

        public override double Y
        {
            get{ return YRadiusAngle(radius, angle); }

            set
            {
                double xBefore = XRadiusAngle(radius, angle);

                angle = AngleXY(xBefore, value);
                radius = RadiusXY(xBefore, value);
            }
        }

        public override double r
        {
            get { return radius; }
            set { radius = value; }
        }

        public override double A
        {
            get { return angle; }
            set { angle = value; }
        }
    }
}
