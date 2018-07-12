using System;

namespace _2DShooter
{
    class Vector
    {
        public double X;
        public double Y;

        public Vector(double ax, double ay)
        {
            X = ax;
            Y = ay;
        }
        public void Rotate(double angle)
        {
            X = X * Math.Cos(angle) - Y * Math.Sin(angle);
            Y = X * Math.Sin(angle) + Y * Math.Cos(angle);
            Normalize();
        }
        public void Normalize()
        {
            double lenth = (Math.Sqrt(X * X + Y * Y));
            X = X / lenth;
            Y = Y / lenth;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }
        
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector operator *(Vector v, double r)
        {
            return new Vector(v.X * r, v.Y * r);
        }

        public static double operator *(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

    }
}
