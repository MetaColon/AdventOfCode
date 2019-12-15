using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Day6
{
    public class AdvancedObject : Object
    {
        public AdvancedObject (Object o) : base (o.Name) => Orbits = o.Orbits == null ? null : new AdvancedObject (o.Orbits);

        public void CorrectPointer (HashSet <AdvancedObject> objects)
        {
            Orbits     = objects.FirstOrDefault (o => o.Equals (Orbits));
            Satellites = Satellites.Select (o => objects.First (o.Equals)).ToHashSet ();
        }

        public HashSet <AdvancedObject> Satellites { get; private set; } = new HashSet <AdvancedObject> ();

        public new AdvancedObject Orbits { get; private set; }
    }
}