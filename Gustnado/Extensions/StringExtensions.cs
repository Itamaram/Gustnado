using System;
using System.Collections.Generic;
using System.Linq;
using Bearded.Monads;

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

    public static class EnumerableExtensions
    {
        public static IEnumerable<A> Yield<A>(this A a)
        {
            yield return a;
        }

        public static IEnumerable<A> Concat<A>(this IEnumerable<A> items, A item)
        {
            foreach (var a in items)
                yield return a;

            yield return item;
        }

        public static IEnumerable<Tuple<A, B>> Let<A, B>(this IEnumerable<A> items, Func<A, B> selector)
        {
            return items.Select(a => new Tuple<A, B>(a, selector(a)));
        }

        public static IEnumerable<Tuple<A, B>> ConcatOptions<A, B>(this IEnumerable<Tuple<A, Option<B>>> items)
        {
            return items.SelectMany(t => t.Item2.AsEnumerable().Select(b => new Tuple<A, B>(t.Item1, b)));
        }

        public static IEnumerable<C> Select<A, B, C>(this IEnumerable<Tuple<A, B>> items, Func<A, B, C> selector)
        {
            return items.Select(t => selector(t.Item1, t.Item2));
        }
    }
}
