namespace AdventOfCode.Day06
{
    public class Object
    {
        public Object (string name) => Name = name;

        public string Name   { get; }
        public Object Orbits { get; set; }

        public override bool Equals (object obj) => obj is Object o && Equals (o);

        public override int GetHashCode () => Name != null ? Name.GetHashCode () : 0;

        protected bool Equals (Object other) => other != null && Name == other.Name;
    }
}