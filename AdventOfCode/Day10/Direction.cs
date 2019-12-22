using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Day10
{
    public class Direction : IComparable<Direction>
    {
        public Direction(int x, int y)
        {
            var signX = Math.Sign(x);
            var signY = Math.Sign(y);

            X = signX * x;
            Y = signY * y;

            if (X == 0 && Y == 0)
                return;

            if (X == 0)
            {
                Y = signY;
                return;
            }

            if (Y == 0)
            {
                X = signX;
                return;
            }

            var divisor = Gcd(X, Y);
            X /= divisor;
            Y /= divisor;

            X *= signX;
            Y *= signY;
        }

        public int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public bool InLine(Direction other)
        {
            if (other.X < X)
                return other.InLine(this);

            if (X == 0 && Y == 0)
                return false;

            if (!Logic.Xor(other.X > X, other.Y > Y))
                return false;

            return Equals(other, false);
        }

        public (int X, int Y) ToPair() => (X, Y);

        public Direction Multiply(int f)
        {
            var copy = Copy();
            copy.X *= f;
            copy.Y *= f;

            return copy;
        }

        public Direction Copy() => new Direction(X, Y);

        public double EuclidianNorm() => Math.Sqrt(X * X + Y * Y);

        protected bool Equals(Direction other, bool sign = true) => (X == other.X || !sign && -X == other.X) && (Y == other.Y || !sign && -Y == other.Y);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Direction) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString() => $"{X},{Y}";

        public double GetAngle() => Normalize(Math.Atan2(-Y, -X) - Math.PI / 2);

        private static double Normalize(double angle) => angle < 0 ? Normalize(angle + Math.PI * 2) : angle > Math.PI * 2 ? Normalize(angle - Math.PI * 2) : angle;

        public int CompareTo(Direction other) => GetAngle().CompareTo(other.GetAngle());
    }
}