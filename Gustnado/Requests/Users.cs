using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bearded.Monads;
using Gustnado.Objects;

namespace Gustnado.Requests
{
    public interface UnauthedApiClient
    {
        UnauthedUsersRequest Users { get; }
    }

    public class ApiClient : UnauthedApiClient
    {
        private readonly SoundCloudHttpClient client;

        public ApiClient(SoundCloudHttpClient client)
        {
            this.client = client;
        }

        public UnauthedUsersRequest Users => new UsersRequest(client, new SearchContext("users"));
    }

    public interface UnauthedUsersRequest
    {
        Task<IEnumerable<User>> Get(Option<string> q);
        UnauthedUserRequest this[int id] { get; }
    }

    public class UsersRequest : UnauthedUsersRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public UsersRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<User>> Get(Option<string> q)
        {
            return client.FetchMany<User>(context, q.AsParameter("q"));
        }

        public UnauthedUserRequest this[int id] => new UserRequest(client, context.Add($"{id}"));
    }

    public class UserRequest : UnauthedUserRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public UserRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<User> Get() => client.Fetch<User>(context);

        public UnauthedTracksRequest Tracks => new TracksRequest(client, context.Add("tracks"));
        public UnauthedPlaylistsRequest Playlists => new PlaylistsRequest(client, context.Add("playlists"));
        public UnauthedFollowingsRequest Followings => new FollowingsRequest(client, context.Add("followings"));
        public UnauthedFollowersRequest Followers => new FollowersRequest(client, context.Add("followers"));
        public UnauthedCommentsRequest Comments => new CommentsRequest(client, context.Add("comments"));
        public UnauthedFavoritesRequest Favorites => new FavoritesRequest(client, context.Add("favorites"));
        public UnauthedWebProfilesRequest WebProfiles => new WebProfilesRequest(client, context.Add("web-profiles"));
    }

    public static class Extensions
    {
        public static Task<T> Fetch<T>(this SoundCloudHttpClient client, SearchContext context)
        {
            return client.Fetch<T>(context, Enumerable.Empty<KeyValuePair<string, string>>());
        }
        public static Task<IEnumerable<T>> FetchMany<T>(this SoundCloudHttpClient client, SearchContext context)
        {
            return client.FetchMany<T>(context, Enumerable.Empty<KeyValuePair<string, string>>());
        }

        //todo this isn't pretty
        public static Task<IEnumerable<User>> Get(this UnauthedUsersRequest r) => r.Get(Option<string>.None);
    }

    public interface UnauthedUserRequest
    {
        Task<User> Get();
        UnauthedTracksRequest Tracks { get; }
        UnauthedPlaylistsRequest Playlists { get; }
        UnauthedFollowingsRequest Followings { get; }
        UnauthedFollowersRequest Followers { get; }
        UnauthedCommentsRequest Comments { get; }
        UnauthedFavoritesRequest Favorites { get; }
        UnauthedWebProfilesRequest WebProfiles { get; }
    }

    public interface UnauthedTracksRequest
    {
        Task<IEnumerable<Track>> Get();
    }

    public class TracksRequest : UnauthedTracksRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public TracksRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<Track>> Get() => client.FetchMany<Track>(context);
    }

    public interface UnauthedPlaylistsRequest
    {
        Task<IEnumerable<PlayList>> Get();
    }

    public class PlaylistsRequest : UnauthedPlaylistsRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public PlaylistsRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<PlayList>> Get() => client.FetchMany<PlayList>(context);
    }

    public interface UnauthedFollowingsRequest
    {
        Task<IEnumerable<User>> Get();
        UnauthedFollowingRequest this[int id] { get; }
    }

    public class FollowingsRequest : UnauthedFollowingsRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public FollowingsRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<User>> Get() => client.FetchMany<User>(context);

        public UnauthedFollowingRequest this[int id] => new FollowingRequest(client, context.Add($"{id}"));
    }

    public interface UnauthedFollowingRequest
    {
        Task<User> Get();
    }

    public class FollowingRequest : UnauthedFollowingRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public FollowingRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<User> Get() => client.Fetch<User>(context);
    }

    public interface UnauthedFollowersRequest
    {
        Task<IEnumerable<User>> Get();
        UnauthedFollowerRequest this[int id] { get; }
    }

    public class FollowersRequest: UnauthedFollowersRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public FollowersRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<User>> Get() => client.FetchMany<User>(context);

        public UnauthedFollowerRequest this[int id] => new FollowerRequest(client, context.Add($"{id}"));
    }

    public interface UnauthedFollowerRequest
    {
        Task<User> Get();
    }

    public class FollowerRequest : UnauthedFollowerRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public FollowerRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<User> Get() => client.Fetch<User>(context);
    }

    public interface UnauthedCommentsRequest
    {
        Task<IEnumerable<Comment>> Get();
    }

    public class CommentsRequest : UnauthedCommentsRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public CommentsRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<Comment>> Get() => client.FetchMany<Comment>(context);
    }

    public interface UnauthedFavoritesRequest
    {
        Task<IEnumerable<Track>> Get();
        UnauthedFavoriteRequest this[int id] { get; }
    }

    public class FavoritesRequest : UnauthedFavoritesRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public FavoritesRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<Track>> Get() => client.FetchMany<Track>(context);

        public UnauthedFavoriteRequest this[int id] => new FavoriteRequest(client, context.Add($"{id}"));
    }

    public interface UnauthedFavoriteRequest
    {
        Task<Track> Get();
    }

    public class FavoriteRequest: UnauthedFavoriteRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public FavoriteRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<Track> Get() => client.Fetch<Track>(context);
    }

    public interface UnauthedWebProfilesRequest
    {
        Task<IEnumerable<WebProfile>> Get();
    }

    public class WebProfilesRequest : UnauthedWebProfilesRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public WebProfilesRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<WebProfile>> Get() => client.FetchMany<WebProfile>(context);
    }

    public abstract class GetOne<T>
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;
        protected GetOne(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<T> Get() => client.Fetch<T>(context);
    }

    public abstract class GetMany<T>
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;
        protected GetMany(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<T>> Get() => client.FetchMany<T>(context);
    }
}