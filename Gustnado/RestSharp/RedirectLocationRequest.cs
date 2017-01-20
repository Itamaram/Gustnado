using System;
using System.Linq;
using System.Net;
using RestSharp;

namespace Gustnado.RestSharp
{
    public class RedirectLocationRequest : RestRequest, SoundCloudRestRequest<string>
    {
        public RedirectLocationRequest(string url)
        {
            AddQueryParameter("url", url);
        }

        public string Execute(SoundCloudHttpClient client)
        {
            var response = client.Http
                .Execute(client.Authenticate(this));

            if(response.StatusCode != HttpStatusCode.Redirect)
                throw new Exception("RedirectLocationRequest returned non redirect response");

            return (string) response.Headers.First(p => p.Name == "Location").Value;
        }
    }
}