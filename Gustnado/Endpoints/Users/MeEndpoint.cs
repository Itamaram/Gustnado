using Gustnado.Objects;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints.Users
{
    public class MeEndpoint
    {
        private static readonly SearchContext context = new SearchContext("me");

        public RestRequest<Me> Get() => RestRequest<Me>.Get(context);

        public UserTracksEndpoint Tracks => new UserTracksEndpoint(context);
        public UserPlaylistsEndpoint Playlists => new UserPlaylistsEndpoint(context);
        public FollowingsEndpoint Followings => new FollowingsEndpoint(context);
        public FollowersEndpoint Followers => new FollowersEndpoint(context);
        public CommentsEndpoint Comments => new CommentsEndpoint(context);
        public FavoritesEndpoint Favorites => new FavoritesEndpoint(context);
        public WebProfilesEndpoint WebProfiles => new WebProfilesEndpoint(context);

        //todo activities
        //todo connections
    }
}