using System;

namespace Modules.GridModule
{
    public static class Array2DExtensions
    {
        public static void ForEach<T>(this T[,] array, Action<T> action)
        {
            foreach (var item in array)
            {
                action(item);
            }
        }

        public static bool WithinBounds<T>(this T[,] array, int x, int y)
        {
            var xLength = array.GetLength(0);
            var yLength = array.GetLength(1);
            return x >= 0 && x < xLength && y >= 0 && y < yLength;
        }
    }
}