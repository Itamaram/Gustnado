using Bearded.Monads;
using Gustnado.Extensions;
using Gustnado.Objects;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints
{
    public class UsersEndpoint
    {
        private static readonly SearchContext context = new SearchContext("users");

        public RestRequestMany<User> Get(int pagesize = 50) => Get(Option<string>.None, pagesize);

        public RestRequestMany<User> Get(Option<string> q, int pagesize = 50)
        {
            return RestRequestMany<User>.Get(context, pagesize)
                .AddQueryParameter("q", q);
        }

        public UserEndpoint this[int id] => new UserEndpoint(context, id);
    }

    public abstract class ReadOnlyUserEndpoint
    {
        protected SearchContext Context { get; set; }

        protected ReadOnlyUserEndpoint(SearchContext context)
        {
            Context = context;
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(Context);

        public UserTracksEndpoint Tracks => new UserTracksEndpoint(Context);
        public UserPlaylistsEndpoint Playlists => new UserPlaylistsEndpoint(Context);
        public ReadOnlyFollowingsEndpoint Followings => new ReadOnlyFollowingsEndpoint(Context);
        public FollowersEndpoint Followers => new FollowersEndpoint(Context);
        public UserCommentsEndpoint Comments => new UserCommentsEndpoint(Context);
        public ReadOnlyFavoritesEndpoint Favorites => new ReadOnlyFavoritesEndpoint(Context);
        public ReadOnlyWebProfilesEndpoint WebProfiles => new ReadOnlyWebProfilesEndpoint(Context);
    }

    public class UserEndpoint : ReadOnlyUserEndpoint
    {
        public UserEndpoint(SearchContext context, int id)
            : base(context.Add(id))
        {
        }
    }

    public class UserTracksEndpoint
    {
        private readonly SearchContext context;

        public UserTracksEndpoint(SearchContext context)
        {
            this.context = context.Add("tracks");
        }

        public RestRequestMany<Track> Get(int pagesize = 50) => RestRequestMany<Track>.Get(context, pagesize);
    }

    public class UserPlaylistsEndpoint
    {
        private readonly SearchContext context;

        public UserPlaylistsEndpoint(SearchContext context)
        {
            this.context = context.Add("playlists");
        }

        public RestRequestMany<Playlist> Get(int pagesize = 50) => RestRequestMany<Playlist>.Get(context, pagesize);
    }

    public class ReadOnlyFollowingsEndpoint
    {
        protected SearchContext Context { get; }

        public ReadOnlyFollowingsEndpoint(SearchContext context)
        {
            Context = context.Add("followings");
        }

        public RestRequestMany<User> Get(int pagesize = 50) => RestRequestMany<User>.Get(Context, pagesize);

        public ReadOnlyUserFollowingEndpoint this[int id] => new ReadOnlyUserFollowingEndpoint(Context, id);
    }

    public class ReadWriteFollowingsEndpoint : ReadOnlyFollowingsEndpoint
    {
        public ReadWriteFollowingsEndpoint(SearchContext context) : base(context)
        {
        }

        public new ReadWriteUserFollowingEndpoint this[int id] => new ReadWriteUserFollowingEndpoint(Context, id);
    }

    public class ReadOnlyUserFollowingEndpoint
    {
        protected SearchContext Context { get; }

        public ReadOnlyUserFollowingEndpoint(SearchContext context, int id)
        {
            Context = context.Add(id);
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(Context);
    }

    public class ReadWriteUserFollowingEndpoint : ReadOnlyUserFollowingEndpoint
    {
        public ReadWriteUserFollowingEndpoint(SearchContext context, int id) : base(context, id) { }

        public RestRequest<DeleteResponse> Put() => RestRequest<DeleteResponse>.Put(Context);

        public RestRequest<User> Delete() => RestRequest<User>.Delete(Context);
    }

    public class FollowersEndpoint
    {
        private readonly SearchContext context;

        public FollowersEndpoint(SearchContext context)
        {
            this.context = context.Add("followers");
        }

        public RestRequestMany<User> Get(int pagesize = 50) => RestRequestMany<User>.Get(context, pagesize);

        public FollowerEndpoint this[int id] => new FollowerEndpoint(context, id);
    }

    public class FollowerEndpoint
    {
        private readonly SearchContext context;

        public FollowerEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(context);
    }

    public class UserCommentsEndpoint
    {
        private readonly SearchContext context;

        public UserCommentsEndpoint(SearchContext context)
        {
            this.context = context.Add("comments");
        }

        public RestRequestMany<Comment> Get(int pagesize = 50) => RestRequestMany<Comment>.Get(context, pagesize);
    }

    public class ReadOnlyFavoritesEndpoint
    {
        protected SearchContext Context { get; }

        public ReadOnlyFavoritesEndpoint(SearchContext context)
        {
            Context = context.Add("favorites");
        }

        public RestRequestMany<Track> Get(int pagesize = 50) => RestRequestMany<Track>.Get(Context, pagesize);

        public ReadOnlyFavoriteEndpoint this[int id] => new ReadOnlyFavoriteEndpoint(Context, id);
    }

    public class ReadOnlyFavoriteEndpoint
    {
        protected SearchContext Context { get; }

        public ReadOnlyFavoriteEndpoint(SearchContext context, int id)
        {
            Context = context.Add(id);
        }

        public RestRequest<Track> Get() => RestRequest<Track>.Get(Context);
    }

    public class ReadWriteFavoritesEndpoint : ReadOnlyFavoritesEndpoint
    {
        public ReadWriteFavoritesEndpoint(SearchContext context) : base(context)
        {
        }

        public new ReadWriteFavoriteEndpoint this[int id] => new ReadWriteFavoriteEndpoint(Context, id);
    }

    public class ReadWriteFavoriteEndpoint : ReadOnlyFavoriteEndpoint
    {
        public ReadWriteFavoriteEndpoint(SearchContext context, int id) : base(context, id)
        {
        }

        public RestRequest<Track> Put() => RestRequest<Track>.Put(Context);

        public RestRequest<DeleteResponse> Delete() => RestRequest<DeleteResponse>.Delete(Context);
    }

    public class ReadOnlyWebProfilesEndpoint
    {
        private readonly SearchContext context;

        public ReadOnlyWebProfilesEndpoint(SearchContext context)
        {
            this.context = context.Add("web-profiles");
        }

        public RestRequestMany<WebProfile> Get(int pagesize = 50) => RestRequestMany<WebProfile>.Get(context, pagesize);
    }

    public class ReadWriteWebProfilesEndpoint : ReadOnlyWebProfilesEndpoint
    {
        public ReadWriteWebProfilesEndpoint(SearchContext context) : base(context)
        {
        }

        //todo PUT DELETE
    }
}