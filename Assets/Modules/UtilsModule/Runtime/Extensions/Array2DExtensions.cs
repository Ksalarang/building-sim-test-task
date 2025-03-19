using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.UtilsModule.Runtime.Extensions
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

        public static bool Contains<T>(this T[,] array, T searchedItem)
        {
            foreach (var item in array)
            {
                if (EqualityComparer<T>.Default.Equals(searchedItem, item))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IndicesOf<T>(this T[,] array, T searchedItem, out Vector2Int indices)
        {
            var xLength = array.GetLength(0);
            var yLength = array.GetLength(1);

            for (var x = 0; x < xLength; x++)
            {
                for (var y = 0; y < yLength; y++)
                {
                    var item = array[x, y];

                    if (EqualityComparer<T>.Default.Equals(item, searchedItem))
                    {
                        indices = new Vector2Int(x, y);
                        return true;
                    }
                }
            }

            indices = new Vector2Int(-1, -1);
            return false;
        }
    }
}