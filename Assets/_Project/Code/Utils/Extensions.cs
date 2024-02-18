using System;
using System.Collections.Generic;

using Object = UnityEngine.Object;
namespace Code.Utils
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
    }
}
