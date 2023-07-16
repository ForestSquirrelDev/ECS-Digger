using System;
using System.Collections.Generic;

namespace Core.Utils {
    public static class ListExtensions {
        public static int IndexOf<T>(this IList<T> list, Predicate<T> match)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (match(item))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
