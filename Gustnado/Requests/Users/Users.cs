using Bearded.Monads;
using Gustnado.Objects;
using Gustnado.Requests.Tracks;

namespace Gustnado.Requests.Users
{
    public class UsersRequest
    {
        private static readonly SearchContext context = new SearchContext("users");

        public RestRequestMany<User> Get() => Get(Option<string>.None);

        public RestRequestMany<User> Get(Option<string> q)
        {
            return new RestRequestMany<User>(context)
                .Do(r => q.WhenSome(query => r.AddQueryParameter("q", query)));
        }

        public UserRequest this[int id] => new UserRequest(context, id);
    }

    public class UserRequest
    {
        private readonly SearchContext context;

        public UserRequest(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(context);

        public UserTracksRequest Tracks => new UserTracksRequest(context);
        public UserPlaylistsRequest Playlists => new UserPlaylistsRequest(context);
        public FollowingsRequest Followings => new FollowingsRequest( context);
        public FollowersRequest Followers => new FollowersRequest( context);
        public CommentsRequest Comments => new CommentsRequest( context);
        public FavoritesRequest Favorites => new FavoritesRequest( context);
        public WebProfilesRequest WebProfiles => new WebProfilesRequest( context);
    }
    
    public class UserTracksRequest
    {
        private readonly SearchContext context;

        public UserTracksRequest(SearchContext context)
        {
            this.context = context.Add("tracks");
        }

        public RestRequestMany<Track> Get() => new RestRequestMany<Track>(context);
    }
    
    public class UserPlaylistsRequest
    {
        private readonly SearchContext context;

        public UserPlaylistsRequest(SearchContext context)
        {
            this.context = context.Add("playlists");
        }

        public RestRequestMany<PlayList> Get() => new RestRequestMany<PlayList>(context);
    }

    public class FollowingsRequest
    {
        private readonly SearchContext context;

        public FollowingsRequest(SearchContext context)
        {
            this.context = context.Add("followings");
        }

        public RestRequestMany<User> Get() => new RestRequestMany<User>(context);

        public UserFollowingRequest this[int id] => new UserFollowingRequest(context, id);
    }

    public class UserFollowingRequest
    {
        private readonly SearchContext context;

        public UserFollowingRequest(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(context);
    }

    public class FollowersRequest
    {
        private readonly SearchContext context;

        public FollowersRequest(SearchContext context)
        {
            this.context = context.Add("followers");
        }

        public RestRequestMany<User> Get() => new RestRequestMany<User>(context);

        public FollowerRequest this[int id] => new FollowerRequest(context, id);
    }

    public class FollowerRequest
    {
        private readonly SearchContext context;

        public FollowerRequest(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(context);
    }
    
    public class CommentsRequest
    {
        private readonly SearchContext context;

        public CommentsRequest(SearchContext context)
        {
            this.context = context.Add("comments");
        }

        public RestRequestMany<Comment> Get() => new RestRequestMany<Comment>(context);
    }
    
    public class FavoritesRequest
    {
        private readonly SearchContext context;

        public FavoritesRequest(SearchContext context)
        {
            this.context = context.Add("favorites");
        }

        public RestRequestMany<Track> Get() => new RestRequestMany<Track>(context);

        public FavoriteRequest this[int id] => new FavoriteRequest(context,id);
    }

    public class FavoriteRequest
    {
        private readonly SearchContext context;

        public FavoriteRequest(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<Track> Get() => RestRequest<Track>.Get(context);
    }

    public class WebProfilesRequest 
    {
        private readonly SearchContext context;

        public WebProfilesRequest(SearchContext context)
        {
            this.context = context.Add("web-profiles");
        }

        public RestRequestMany<WebProfile> Get() => new RestRequestMany<WebProfile>(context);
    }
}