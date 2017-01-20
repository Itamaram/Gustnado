using System;
using System.Linq;
using System.Net;
using RestSharp;

namespace Gustnado.RestSharp
{
    public class RedirectRequest : RestRequest, SoundCloudRestRequest<string>
    {
        public RedirectRequest(string url)
        {
            AddQueryParameter("url", url);
        }

        public string Execute(SoundCloudHttpClient client)
        {
            var response = client.Http
                .Execute(client.Authenticate(this));

            if(response.StatusCode != HttpStatusCode.Redirect)
                throw new Exception("RedirectRequest returned non redirect response");

            return (string) response.Headers.First(p => p.Name == "Location").Value;
        }
    }
}