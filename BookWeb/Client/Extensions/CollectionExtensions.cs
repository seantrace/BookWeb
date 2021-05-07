using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> original)
        {
            return original ?? Enumerable.Empty<T>();
        }
    }
}
