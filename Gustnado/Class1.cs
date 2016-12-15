using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Gustnado.Objects;
using Newtonsoft.Json;

namespace Gustnado
{
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

    public class ApiClient
    {
        private readonly KeyValuePair<string, string> clientId;
        private readonly IMakeWebRequests web;

        public ApiClient(string clientId, IMakeWebRequests web)
        {
            this.clientId = new KeyValuePair<string, string>("client_id", clientId);
            this.web = web;
        }

        public UsersRequest Users => new UsersRequest(this, new SearchContext("users"));

        public async Task<T> Get<T>(SearchContext context)
        {
            var result = await web.GetStringAsync($"{Constants.ApiEndpoint}{context}", clientId.Yield());
            return JsonConvert.DeserializeObject<T>(result);
        }
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

    public class UsersRequest
    {
        private readonly ApiClient client;
        private readonly SearchContext context;

        public UsersRequest(ApiClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<User>> Get(/*query?*/)
        {
            return client.Get<IEnumerable<User>>(context);
        }

        public UserRequest this[int id] => this[$"{id}"];

        public UserRequest this[string id] => new UserRequest(client, context.Add(id));
    }

    public class UserRequest
    {
        private readonly ApiClient client;
        private readonly SearchContext context;

        public UserRequest(ApiClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<User> Get()
        {
            return client.Get<User>(context);
        }
    }

    public class TracksRequest
    {
        private readonly ApiClient client;
        private readonly SearchContext context;

        public TracksRequest(ApiClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<Track>> Get()
        {
            return client.Get<IEnumerable<Track>>(context);
        }

        public TrackRequest this[int id] => this[$"{id}"];

        public TrackRequest this[string id] => new TrackRequest(client, context.Add(id));
    }

    public class TrackRequest
    {
        private readonly ApiClient client;
        private readonly SearchContext context;

        public TrackRequest(ApiClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<Track> Get() => client.Get<Track>(context);
    }
}
