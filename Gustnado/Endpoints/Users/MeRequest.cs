using Gustnado.Objects;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints.Users
{
    public class MeRequest
    {
        private static readonly SearchContext context = new SearchContext("me");

        public RestRequest<Me> Get() => RestRequest<Me>.Get(context);

        public UserTracksRequest Tracks => new UserTracksRequest(context);
        public UserPlaylistsRequest Playlists => new UserPlaylistsRequest(context);
        public FollowingsRequest Followings => new FollowingsRequest(context);
        public FollowersRequest Followers => new FollowersRequest(context);
        public CommentsRequest Comments => new CommentsRequest(context);
        public FavoritesRequest Favorites => new FavoritesRequest(context);
        public WebProfilesRequest WebProfiles => new WebProfilesRequest(context);

        //todo activities
        //todo connections
    }
}