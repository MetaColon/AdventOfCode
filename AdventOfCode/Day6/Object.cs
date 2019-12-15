namespace AdventOfCode.Day6
{
    public class Object
    {
        /// <inheritdoc />
        public Object (string name, Object orbits)
        {
            Name   = name;
            Orbits = orbits;
        }

        /// <inheritdoc />
        public Object (string name) => Name = name;

        public string Name   { get; }
        public Object Orbits { get; set; }

        /// <inheritdoc />
        public override bool Equals (object obj) => obj is Object o && Equals (o);

        /// <inheritdoc />
        public override int GetHashCode () => Name != null ? Name.GetHashCode () : 0;

        protected bool Equals (Object other) => Name == other.Name;
    }
}