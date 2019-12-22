namespace AdventOfCode.Day12
{
    public class Moon
    {
        public (int X, int Y, int Z) Position { get; private set; }
        public (int X, int Y, int Z) Velocity { get; private set; }

        public Moon((int X, int Y, int Z) position) => Position = position;

        public void Move() => Position = Position.Add(Velocity);

        public void Accelerate((int X, int Y, int Z) a) => Velocity = Velocity.Add(a);

        public void AccelerateTo(Moon b)
            => Accelerate(b.Position.Subtract(Position).Sign());

        public int Energy() => Position.Abs().Sum() * Velocity.Abs().Sum();
    }
}