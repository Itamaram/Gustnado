using Gustnado.Serialisation;
using RestSharp;

namespace Gustnado.RestSharp
{
    public class GustnadoRestClient : RestClient
    {
        public GustnadoRestClient()
            : base(Constants.ApiEndpoint)
        {
            AddHandler("application/json", CustomSerializer.Default);
        }
    }
}