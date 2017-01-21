using System;
using System.Collections.Generic;

namespace Gustnado.Extensions
{
    public static class StringExtensions
    {
        public static string Join(this IEnumerable<string> items, string separator)
        {
            return string.Join(separator, items);
        }

        public static string AsResource(this IEnumerable<string> items)
        {
            return items.Join("/");
        }

        public static string PathAndQuery(this string url)
        {
            return new Uri(url).PathAndQuery;
        }
    }
}
