using Bearded.Monads;
using Gustnado.Endpoints;
using Gustnado.Extensions;
using Gustnado.Objects;
using Gustnado.RestSharp;
using RestSharp;

namespace Gustnado
{
    //todo exceptions, reauthing
    public class SoundCloudHttpClient
    {
        private readonly string clientId;
        private readonly string secret;
        private Option<string> oauth = Option<string>.None;

        public SoundCloudHttpClient(string clientId, string secret, IRestClient http)
        {
            this.clientId = clientId;
            this.secret = secret;
            Http = http;
        }

        public SoundCloudHttpClient Authenticate(string token)
        {
            oauth = token;
            return this;
        }

        public SoundCloudHttpClient Authenticate(string username, string password)
        {
            var request = SoundCloudApi.OAuth2.Post(new OAuthRequest
            {
                ClientId = clientId,
                ClientSecret = secret,
                GrantType = GrantType.Password,
                Username = username,
                Password = password,
            });

            oauth = Http.Execute<OAuthResponse>(request).Data.AccessToken;

            return this;
        }

        public IRestClient Http { get; }

        public Request Authenticate<Request>(Request r) where Request : IRestRequest
        {
            r.AddQueryParameter("oauth_token", oauth);
            return r;
        }

        public Request AddClientId<Request>(Request r) where Request : IRestRequest
        {
            r.AddQueryParameter("client_id", clientId);
            return r;
        }

        public T Execute<T>(SoundCloudRestRequest<T> request) => request.Execute(this);
    }
}