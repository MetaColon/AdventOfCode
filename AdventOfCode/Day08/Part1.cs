using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day08
{
    public static class Part1
    {
        public static int Solve ()
        {
            var layer = GetFewestZerosLayer (GetLayers (Data.EncodedImage, Data.Height, Data.Width));
            return Count (layer, 1) * Count (layer, 2);
        }

        private static List <byte> GetFewestZerosLayer (List <List <byte>> layers)
        {
            var         min      = int.MaxValue;
            List <byte> minLayer = null;

            foreach (var layer in layers)
            {
                var zeros = Count (layer, 0);
                if (zeros > min)
                    continue;

                min      = zeros;
                minLayer = layer;
            }

            return minLayer;
        }

        public static List <List <byte>> GetLayers (List <byte> encoded, int height, int width)
            => Enumerable.Range (0, encoded.Count / (height * width)).Select (i => encoded.Skip (i * height * width).Take (height * width).ToList ()).ToList ();

        private static int Count (IEnumerable <byte> layer, byte value) => layer.Count (i => i == value);
    }
}