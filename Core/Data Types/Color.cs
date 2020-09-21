using System;

namespace EpsilonEngine
{
    public sealed class Color
    {
        private int _r = 0;
        private int _g = 0;
        private int _b = 0;

        public int r { get { return _r; } set { if (value >= 0 && value <= 255) { _r = value; } else { throw new ArgumentOutOfRangeException("r"); } } }
        public int g { get { return _g; } set { if (value >= 0 && value <= 255) { _g = value; } else { throw new ArgumentOutOfRangeException("g"); } } }
        public int b { get { return _b; } set { if (value >= 0 && value <= 255) { _b = value; } else { throw new ArgumentOutOfRangeException("b"); } } }

        private Color() { }
        public static Color Create()
        {
            Color Output = new Color();
            Output.r = 0;
            Output.g = 0;
            Output.b = 0;
            return Output;
        }

        public static Color Create(int r, int g, int b)
        {
            if (r < 0 || r > 255)
            {
                throw new ArgumentOutOfRangeException("r");
            }
            if (g < 0 || g > 255)
            {
                throw new ArgumentOutOfRangeException("g");
            }
            if (b < 0 || b > 255)
            {
                throw new ArgumentOutOfRangeException("b");
            }
            Color Output = new Color();
            Output.r = r;
            Output.g = g;
            Output.b = b;
            return Output;
        }
        public override string ToString()
        {
            return $"(r:{r}, g:{g}, b:{b})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Color))
            {
                return false;
            }
            else
            {
                return this == (Color)obj;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Color a, Color b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            else if (a is null || b is null)
            {
                return false;
            }
            return (a.r == b.r) && (a.g == b.g) && (a.b == b.b);
        }
        public static bool operator !=(Color a, Color b)
        {
            return !(a == b);
        }
        public Color Clone()
        {
            Color Output = new Color();
            Output.r = r;
            Output.g = g;
            Output.b = b;
            return Output;
        }
    }
}