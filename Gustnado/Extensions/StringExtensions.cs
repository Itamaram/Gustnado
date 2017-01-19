using System;
using System.Collections.Generic;
using System.Linq;
using Bearded.Monads;
using Gustnado.Attributes;
using Gustnado.Serialisation;
using Newtonsoft.Json;
using RestSharp;

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

        public static Option<B> Join<A, B>(this IEnumerable<A> items, Func<A, B, B> join, Func<A, B> seed)
        {
            using (var e = items.GetEnumerator())
            {
                if (!e.MoveNext())
                    return Option<B>.None;

                var accum = seed(e.Current);

                while (e.MoveNext())
                    accum = join(e.Current, accum);

                return accum;
            }
        }

        public static void Join<A>(this IEnumerable<A> items, Action<A> action, Action separator)
        {
            items.Join((a, b) =>
            {
                action(b);
                separator();
                return default(A);
            }, a =>
            {
                action(a);
                return default(A);
            });
        }
    }

    public static class RestSharpExtensions
    {
        public static T AddQueryParameter<T>(this T request, KeyValuePair<string, string> parameter) where T : IRestRequest
        {
            request.AddQueryParameter(parameter.Key, parameter.Value);
            return request;
        }

        public static T AddQueryParameters<T>(this T request, IEnumerable<KeyValuePair<string, string>> parameters) where T : IRestRequest
        {
            return parameters.Aggregate(request, (r, p) => r.AddQueryParameter(p));
        }

        public static T AddQueryParameter<T>(this T request, string key, Option<string> value) where T : IRestRequest
        {
            value.WhenSome(v => request.AddQueryParameter(key, v));
            return request;
        }

        public static Request WriteToRequest<Request, T>(this Request request, T item) where Request : IRestRequest
        {
            var format = typeof(T).GetCustomAttribute<RequestBodyKeyFormatAttribute>()
                .Map(a => a.Format);

            using (var writer = new RequestWriter(request, format))
                JsonSerializer.Create(SerialiserSettingsPresets.IgnoreNulls).Serialize(writer, item);

            return request;
        }

        public static Request WriteToQueryString<Request, T>(this Request request, T item) where Request : IRestRequest
        {
            using (var writer = new RequestQueryStringWriter(request))
                JsonSerializer.Create(SerialiserSettingsPresets.IgnoreNulls).Serialize(writer, item);

            return request;
        }

        public static Request AddJsonToRequestBody<Request, T>(this Request request, T item) where Request : IRestRequest
        {
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = CustomSerializer.Default;
            request.AddJsonBody(item);
            return request;
        }
    }
}