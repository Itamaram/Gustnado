using Gustnado.Extensions;
using RestSharp;

namespace Gustnado.RestSharp
{
    public class RestRequestMany<T> : RestRequest
    {
        public RestRequestMany(SearchContext context)
            : base(context.AsResource(), Method.GET) { }

        public static RestRequestMany<T> Get(SearchContext context)
        {
            return new RestRequestMany<T>(context);
        }
    }
}