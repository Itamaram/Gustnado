using Gustnado.Endpoints.OAuth;
using Gustnado.Endpoints.Tracks;
using Gustnado.Endpoints.Users;

namespace Gustnado.Endpoints
{
    public static class SoundCloudApi
    {
        public static TracksEndpoint Tracks { get; } = new TracksEndpoint();
        public static UsersRequest Users { get; } = new UsersRequest();
        public static MeRequest Me { get; } = new MeRequest();
        public static OAuthEndpoint OAuth2 { get; } = new OAuthEndpoint();
    }
}
