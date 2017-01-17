using Gustnado.Requests.OAuth;
using Gustnado.Requests.Tracks;
using Gustnado.Requests.Users;

namespace Gustnado.Requests
{
    public static class SoundCloudApi
    {
        public static TracksRequest Tracks { get; } = new TracksRequest();
        public static UsersRequest Users { get; } = new UsersRequest();
        public static MeRequest Me { get; } = new MeRequest();
        public static OAuthEndpoint OAuth2 { get; } = new OAuthEndpoint();
    }
}
