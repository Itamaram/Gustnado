using Gustnado.Extensions;
using RestSharp;

namespace Gustnado.RestSharp
{
    public class RestRequest<T> : RestRequest, SoundCloudRestRequest<T> where T : new()
    {
        public RestRequest(SearchContext context, Method method)
            : base(context.AsResource(), method) { }

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

        public T Execute(SoundCloudHttpClient client)
        {
            //todo error handling :/
            return this.AddClientId(client)
                .Authenticate(client)
                .Map(r => client.Http.Execute<T>(r).Data);
        }
    }
}