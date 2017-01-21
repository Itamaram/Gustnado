using Bearded.Monads;
using Gustnado.Extensions;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints
{
    public class ResolveEndpoint
    {
        private static readonly SearchContext context = new SearchContext("resolve");

        public RestRequest<T> Get<T>(string url) where T : new()
        {
            return RestRequest<T>.Get(context)
                .AddQueryParameter("url", url.AsOption());
        }
    }
}