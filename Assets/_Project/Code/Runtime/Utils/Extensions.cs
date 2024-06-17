using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Code.Runtime.Utils
{
    public static class Extensions
    {
        public static int IndexOf<T>(this IEnumerable<T> collection, T searchObject)
        {
            int index = 0;

            foreach (var obj in collection)
            {
                if (object.ReferenceEquals(obj, searchObject))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }
        
        public static int RemoveAll<T>(this List<T> list, Predicate<T> match)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (match == null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            int initialCount = list.Count;
            int i = 0;

            while (i < list.Count)
            {
                if (match(list[i]))
                {
                    list.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            return initialCount - list.Count;
        }
        
        public static T GetRandomElement<T>(this IEnumerable<T> enumerable)
        {
            int count = enumerable.Count();
            int randomIndex = Random.Range(0, count);
            return enumerable.ElementAt(randomIndex);
        }
    }
}
