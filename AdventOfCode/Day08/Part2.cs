using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day08
{
    public static class Part2
    {
        public static int Solve ()
        {
            PrintImage (StackLayers (Part1.GetLayers (Data.EncodedImage, Data.Height, Data.Width)), Data.Height, Data.Width);
            return 0;
        }

        private static void PrintImage (List <byte> decoded, int height, int width)
        {
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                    Console.Write (decoded [y * width + x] == 1 ? '█' : ' ');
                Console.WriteLine ();
            }
        }

        private static List <byte> StackLayers (List <List <byte>> layers)
        {
            var fin = Enumerable.Repeat ((byte) 2, layers [0].Count).ToList ();

            foreach (var layer in layers)
                for (var i = 0; i < layer.Count; i++)
                    if (fin [i] == 2 && layer [i] != 2)
                        fin [i] = layer [i];

            return fin;
        }
    }
}