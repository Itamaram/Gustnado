using System;
using System.Collections.Generic;
using System.Linq;
using Bearded.Monads;
using Gustnado.Requests.Tracks;
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

    public static class ParameterFormatterExtensions
    {
        public static ParameterFormatter AsParameterFormatter(this Type type)
        {
            return Activator.CreateInstance(type) as ParameterFormatter;
        }
    }

    public static class RestSharpExtensions
    {
        public static T AddQueryParameter<T>(this T request, KeyValuePair<string, string> parameter) where T : IRestRequest
        {
            request.AddQueryParameter(parameter.Key, parameter.Value);
            return request;
        }

        public static T AddQueryParameters<T>(this T request, IEnumerable<KeyValuePair<string, string>> parameters) where T: IRestRequest
        {
            return parameters.Aggregate(request, (r, p) => r.AddQueryParameter(p));
        }

        public static Request AddToRequestBody<Request, T>(this Request request, T item) where Request: IRestRequest
        {
            var format = typeof (T).GetCustomAttribute<RequestBodyKeyFormatAttribute>()
                .Map(a => a.Format);

            JsonSerializer.Create(new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore})
                .Serialize(new RequestBodyWriter(request, format), item);

            return request;
        }
    }
}