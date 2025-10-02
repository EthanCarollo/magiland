using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
        private static readonly Random _random = new Random();

        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("La liste est vide ou nulle.");

            int index = _random.Next(list.Count);
            return list[index];
        }
    }
}