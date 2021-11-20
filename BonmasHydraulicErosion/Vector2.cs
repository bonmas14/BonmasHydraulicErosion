using System;

namespace BonmasHydraulicErosion
{
    internal struct Vector2
    {
        public readonly float X;
        public readonly float Y;

        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            }
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2 GetNormalized()
        {
            float magn = Magnitude;

            Vector2 norm = new Vector2(X / magn, Y / magn);

            return norm;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            if (b == 0) throw new DivideByZeroException("Деление вектора на ноль");

            return new Vector2(a.X / b, a.Y / b);
        }
    }
}
