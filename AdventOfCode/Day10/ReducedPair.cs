using System;
using System.Linq;

namespace AdventOfCode.Day10
{
    public class ReducedPair
    {
        public ReducedPair(int x, int y)
        {
            X = Math.Abs(x);
            Y = Math.Abs(y);

            if (X == 0 && Y == 0)
                return;

            if (X == 0)
            {
                Y = 1;
                return;
            }

            if (Y == 0)
            {
                X = 1;
                return;
            }

            var divisor = Gcd(X, Y);
            Y /= divisor;
            X /= divisor;
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

        public bool InLine(ReducedPair other)
        {
            if (other.X < X)
                return other.InLine(this);

            if (X == 0 && Y == 0)
                return false;

            if (!Logic.Xor(other.X > X, other.Y > Y))
                return false;

            for (var i = 0; other.X >= i * X && other.Y >= i * Y; i++)
                if (Multiply(i).Equals(other))
                    return true;

            return false;
        }

        public ReducedPair Multiply(int f)
        {
            var copy = Copy();
            copy.X *= f;
            copy.Y *= f;

            return copy;
        }

        public ReducedPair Copy() => new ReducedPair(X, Y);

        public double EuclidianNorm() => Math.Sqrt(X * X + Y * Y);

        protected bool Equals(ReducedPair other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ReducedPair) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString() => $"{X},{Y}";
    }
}