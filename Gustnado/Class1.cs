using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bearded.Monads;
using Gustnado.Extensions;
using Gustnado.Requests.Tracks;
using Newtonsoft.Json;
using RestSharp;

namespace Gustnado
{
    public static class OptionsExtensions
    {
        public static IEnumerable<A> AsEnumerable<A>(this Option<A> option)
        {
            return option.Map(a => a.Yield()).ElseEmpty();
        }

        public static IEnumerable<KeyValuePair<string, string>> AsParameter(this Option<string> value, string key)
        {
            if (value.IsSome)
                yield return new KeyValuePair<string, string>(key, value.ForceValue());
        }

        public static Option<B> MaybeGetReadonlyValue<A, B>(this IReadOnlyDictionary<A, B> d, A key)
        {
            B b;
            return d.TryGetValue(key, out b) ? b : Option<B>.None;
        }
    }

    public static class ObjectExtensions
    {
        public static A Do<A>(this A a, Action<A> action)
        {
            action(a);
            return a;
        }

        public static B Map<A, B>(this A a, Func<A, B> map) => map(a);

        public static Option<A> GetCustomAttribute<A>(this Type type) where A : Attribute
        {
            return type.GetCustomAttributes()
                .Select(a => a.MaybeCast<A>())
                .ConcatOptions()
                .FirstOrNone();
        }
    }

    public static class ReflectionExtensions
    {
        public static IEnumerable<A> GetCustomAttributes<A>(this PropertyInfo property) where A : Attribute
        {
            return property.GetCustomAttributes()
                .Select(a => a.MaybeCast<A>())
                .ConcatOptions();
        }

        public static Option<A> GetCustomAttribute<A>(this PropertyInfo property) where A : Attribute
        {
            return property.GetCustomAttributes<A>().SingleOrNone();
        }

        public static object Invoke(this MethodBase m, object o) => m.Invoke(o, new object[0]);
    }



    //This can be instantiated as either authed, or not authed. It also takes care of reauthing.
    public class SoundCloudHttpClient
    {
        private readonly string clientId;
        private readonly IRestClient http;

        //todo Ensure client is fed the deserialiser
        public SoundCloudHttpClient(string clientId, IRestClient http)
        {
            this.clientId = clientId;
            this.http = http;
        }

        public T Execute<T>(RestRequest<T> request) where T : new()
        {
            //todo Add auth if authed? Or is that a different class?
            //todo error handling :/
            return request.AddQueryParameter("client_id", clientId)
                .Map(r => http.Execute<T>(r).Data);
        }

        public IEnumerable<T> Execute<T>(RestRequestMany<T> request, int limit = 50)
        {
            return GetPages(request, limit).SelectMany(p => p.Collection);
        }

        private IEnumerable<PaginationResult<T>> GetPages<T>(RestRequestMany<T> request, int limit)
        {
            var page = request.AddQueryParameter("client_id", clientId)
                .AddQueryParameter("linked_partitioning", "1")
                .AddQueryParameter("limit", limit.ToString())
                .Map(r => http.Execute<PaginationResult<T>>(r).Data);

            yield return page;

            while (page.NextHref != null)
            {
                var next = new Uri(page.NextHref).PathAndQuery;

                page = http.Execute<PaginationResult<T>>(new RestRequest(next)).Data;
                
                yield return page;
            }
        }
    }

    public class GustnadoRestClient : RestClient
    {
        public GustnadoRestClient()
            : base(Constants.ApiEndpoint)
        {
            AddHandler("application/json", CustomSerializer.Default);
        }
    }

    public class SearchContext : IEnumerable<string>
    {
        private readonly IReadOnlyList<string> terms;

        public SearchContext(params string[] terms)
        {
            this.terms = terms.ToList();
        }

        public SearchContext(IEnumerable<string> terms)
        {
            this.terms = terms.ToList();
        }

        public SearchContext Add(string s)
        {
            return new SearchContext(new List<string>(terms) { s });
        }

        public SearchContext Add(int id) => Add(id.ToString());

        public IEnumerator<string> GetEnumerator() => terms.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static class Constants
    {
        public static string ApiEndpoint = "http://api.soundcloud.com";
    }

    public class PaginationResult<T>
    {
        [JsonProperty("collection")]
        public List<T> Collection { get; set; }

        [JsonProperty("next_href")]
        public string NextHref { get; set; }
    }
}
