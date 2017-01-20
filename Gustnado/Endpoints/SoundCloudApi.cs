using Gustnado.Endpoints.OAuth;
using Gustnado.Endpoints.Tracks;
using Gustnado.Endpoints.Users;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints
{
    public static class SoundCloudApi
    {
        public static TracksEndpoint Tracks { get; } = new TracksEndpoint();
        public static UsersEndpoint Users { get; } = new UsersEndpoint();
        public static MeEndpoint Me { get; } = new MeEndpoint();
        public static OAuthEndpoint OAuth2 { get; } = new OAuthEndpoint();
        public static RedirectLocationRequest Resolve(string url) => new RedirectLocationRequest(url);
    }
}
