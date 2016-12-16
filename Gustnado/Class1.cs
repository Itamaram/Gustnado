using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bearded.Monads;
using Newtonsoft.Json;

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
            if(value.IsSome)
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
    }

    //This can be instantiated as either authed, or not authed. It also takes care of reauthing.
    public interface SoundCloudHttpClient
    {
        Task<T> Fetch<T>(SearchContext context, IEnumerable<KeyValuePair<string,string>> parameters);
        Task<IEnumerable<T>> FetchMany<T>(SearchContext context, IEnumerable<KeyValuePair<string, string>> parameters);
    }

    public class SearchContext
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

        public override string ToString() => "/" + string.Join("/", terms);
    }

    public interface IMakeWebRequests
    {
        Task<string> GetStringAsync(string endpoint, IEnumerable<KeyValuePair<string, string>> queries);
    }

    public class HttpClientImplementation : IMakeWebRequests, IDisposable
    {
        private readonly HttpClient client;
        public HttpClientImplementation()
        {
            client = new HttpClient();
        }

        public Task<string> GetStringAsync(string endpoint, IEnumerable<KeyValuePair<string, string>> queries)
        {
            var query = QueryString(queries);
            return client.GetStringAsync(endpoint + (query.Any() ? "?" + query : string.Empty));
        }

        private static string QueryString(IEnumerable<KeyValuePair<string, string>> queries)
        {
            return string.Join("&", queries.Select(q => $"{q.Key}={q.Value}"));
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }

    public static class Constants
    {
        public static string ApiEndpoint = "http://api.soundcloud.com";
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
    }

    public class UnauthedClient : SoundCloudHttpClient
    {
        private readonly KeyValuePair<string, string> clientId;

        private readonly KeyValuePair<string, string>[] pagination =
        {
            new KeyValuePair<string, string>("linked_partitioning", "1"),
            new KeyValuePair<string, string>("limit", "50")
        };
         
        private readonly IMakeWebRequests web;

        public UnauthedClient(string clientId, IMakeWebRequests web)
        {
            this.clientId = new KeyValuePair<string, string>("client_id", clientId);
            this.web = web;
        }

        //include a second ctor for auth, make both ctor private, user factory?

        public async Task<T> Fetch<T>(SearchContext context, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var result = await web.GetStringAsync($"{Constants.ApiEndpoint}{context}", clientId.Yield());
            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<IEnumerable<T>> FetchMany<T>(SearchContext context, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            //todo allow custom pagination, building of own requests

            var result = new List<T>();
            
            var next = (await web.GetStringAsync($"{Constants.ApiEndpoint}{context}", parameters.Concat(clientId).Concat(pagination)))
                .Map(JsonConvert.DeserializeObject<PaginationResult<T>>)
                .Do(p => result.AddRange(p.Collection))
                .Map(p => p.NextHref);

            while (next != null)
            {
                Console.WriteLine($"fetching {next}");
                next = (await web.GetStringAsync(next, Enumerable.Empty<KeyValuePair<string, string>>()))
                    .Map(JsonConvert.DeserializeObject<PaginationResult<T>>)
                    .Do(p => result.AddRange(p.Collection))
                    .Map(p => p.NextHref);
            }

            return result;
        }
    }

    public class PaginationResult<T>
    {
        [JsonProperty("collection")]
        public List<T> Collection { get; set; } 

        [JsonProperty("next_href")]
        public string NextHref { get; set; }  
    }
}
