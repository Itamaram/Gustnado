using System.Collections.Generic;
using System.Linq;
using Gustnado.Extensions;
using Newtonsoft.Json;
using RestSharp;

namespace Gustnado.RestSharp
{
    public class RestRequestMany<T> : RestRequest, SoundCloudRestRequest<IEnumerable<T>>
    {
        private readonly int limit;

        public RestRequestMany(SearchContext context, int limit)
            : base(context.AsResource(), Method.GET)
        {
            this.limit = limit;
        }

        public static RestRequestMany<T> Get(SearchContext context, int limit)
        {
            return new RestRequestMany<T>(context, limit);
        }

        public IEnumerable<T> Execute(SoundCloudHttpClient client)
        {
            return GetPages(client).SelectMany(p => p.Collection);
        }

        private IEnumerable<PaginationResult<T>> GetPages(SoundCloudHttpClient client)
        {
            var page = this.AddClientId(client)
                .Authenticate(client)
                .AddQueryParameter("linked_partitioning", "1")
                .AddQueryParameter("limit", limit.ToString())
                .Map(r => client.Http.Execute<PaginationResult<T>>(r).Data);

            yield return page;

            while (page.NextHref != null)
            {
                page = client.Http.Execute<PaginationResult<T>>(new RestRequest(page.NextHref.PathAndQuery())).Data;

                yield return page;
            }
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