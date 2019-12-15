using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Day6
{
    public static class Part2
    {
        public static int Solve ()
        {
            var objects = FillSatellites (Part1.GetObjects (Data.Map));

            return GetDistance (objects.First (o => o.Name == "YOU").Orbits, objects.First (o => o.Name == "SAN").Orbits);
        }

        private static HashSet <AdvancedObject> FillSatellites (HashSet <Object> objects)
        {
            var fin = objects.Select (o => new AdvancedObject (o)).ToHashSet ();

            foreach (var o in fin)
                o.CorrectPointer (fin);

            foreach (var o in fin)
                o.Orbits?.Satellites.Add (o);

            return fin;
        }

        private static int GetDistance (AdvancedObject a, AdvancedObject b, HashSet <AdvancedObject> done = null)
        {
            if (a.Equals (b))
                return 0;

            if (done == null)
                done = new HashSet <AdvancedObject> ();

            done.Add (a);

            if (a.Orbits != null && !done.Contains (a.Orbits))
            {
                var res = GetDistance (a.Orbits, b, done);
                if (res != -1)
                    return res + 1;
            }

            foreach (var satellite in a.Satellites.Where(o => !done.Contains(o)))
            {
                var res = GetDistance (satellite, b, done);
                if (res != -1)
                    return res + 1;
            }

            return -1;
        }
    }
}