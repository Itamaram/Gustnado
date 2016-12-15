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
    }

    public class UnauthedClient : SoundCloudHttpClient
    {
        private readonly KeyValuePair<string, string> clientId;
        private readonly IMakeWebRequests web;

        public UnauthedClient(string clientId, IMakeWebRequests web)
        {
            this.clientId = new KeyValuePair<string, string>("client_id", clientId);
            this.web = web;
        }

        public async Task<T> Fetch<T>(SearchContext context, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var result = await web.GetStringAsync($"{Constants.ApiEndpoint}{context}", clientId.Yield());
            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<IEnumerable<T>> FetchMany<T>(SearchContext context, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            //Kind of like Fetch, only add pagination, and keep on turning them pages
            return Enumerable.Empty<T>();
        }
    }
}
