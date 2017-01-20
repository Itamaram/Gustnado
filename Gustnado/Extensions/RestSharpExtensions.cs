using Bearded.Monads;
using Gustnado.Attributes;
using Gustnado.Serialisation;
using Newtonsoft.Json;
using RestSharp;

namespace Gustnado.Extensions
{
    public static class RestSharpExtensions
    {
        public static T AddQueryParameter<T>(this T request, string key, Option<string> value) where T : IRestRequest
        {
            value.WhenSome(v => request.AddQueryParameter(key, v));
            return request;
        }

        public static Request WriteToRequest<Request, T>(this Request request, T item) where Request : IRestRequest
        {
            var format = typeof(T).GetCustomAttribute<RequestBodyKeyFormatAttribute>()
                .Map(a => a.Format);

            using (var writer = new RequestWriter(request, format))
                JsonSerializer.Create(SerialiserSettingsPresets.IgnoreNulls).Serialize(writer, item);

            return request;
        }

        public static Request WriteToQueryString<Request, T>(this Request request, T item) where Request : IRestRequest
        {
            using (var writer = new RequestQueryStringWriter(request))
                JsonSerializer.Create(SerialiserSettingsPresets.IgnoreNulls).Serialize(writer, item);

            return request;
        }

        public static Request AddJsonToRequestBody<Request, T>(this Request request, T item) where Request : IRestRequest
        {
            request.JsonSerializer = CustomSerializer.Default;
            request.AddJsonBody(item);
            return request;
        }

        public static Request Authenticate<Request>(this Request request, SoundCloudHttpClient client) where Request : IRestRequest
        {
            return client.Authenticate(request);
        }
        
        public static Request AddClientId<Request>(this Request request, SoundCloudHttpClient client) where Request : IRestRequest
        {
            return client.AddClientId(request);
        }
    }
}