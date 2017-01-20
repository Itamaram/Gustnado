using Gustnado.Endpoints.Apps;
using Gustnado.Endpoints.Comments;
using Gustnado.Endpoints.OAuth;
using Gustnado.Endpoints.Playlists;
using Gustnado.Endpoints.Tracks;
using Gustnado.Endpoints.Users;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints
{
    public static class SoundCloudApi
    {
        public static OAuthEndpoint OAuth2 { get; } = new OAuthEndpoint();
        public static UsersEndpoint Users { get; } = new UsersEndpoint();
        public static TracksEndpoint Tracks { get; } = new TracksEndpoint();
        public static PlaylistsEndpoint Playlists { get; }= new PlaylistsEndpoint();
        public static CommentsEndpoint Comments { get; } = new CommentsEndpoint();
        public static MeEndpoint Me { get; } = new MeEndpoint();
        public static AppsEndpoint Apps { get; } = new AppsEndpoint();
        public static RedirectLocationRequest Resolve(string url) => new RedirectLocationRequest(url);
    }
}
