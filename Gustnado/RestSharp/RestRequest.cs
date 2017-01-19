using Gustnado.Extensions;
using RestSharp;

namespace Gustnado.RestSharp
{
    //todo override json serializer?
    public class RestRequest<T> : RestRequest
    {
        public RestRequest(SearchContext context, Method method)
            : base(context.AsResource(), method) { }

        public RestRequest<T> AddSoundCloudObject<A>(A item)
        {
            return this.WriteToRequest(item);
        }

        public static RestRequest<T> Get(SearchContext context)
        {
            return new RestRequest<T>(context, Method.GET);
        }

        public static RestRequest<T> Post(SearchContext context)
        {
            return new RestRequest<T>(context, Method.POST);
        }

        public static RestRequest<T> Put(SearchContext context)
        {
            return new RestRequest<T>(context, Method.PUT);
        }

        public static RestRequest<T> Delete(SearchContext context)
        {
            return new RestRequest<T>(context, Method.DELETE);
        }
    }
}