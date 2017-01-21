﻿using Bearded.Monads;
using Gustnado.Extensions;
using Gustnado.Objects;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints
{
    public class UsersEndpoint
    {
        private static readonly SearchContext context = new SearchContext("users");

        public RestRequestMany<User> Get() => Get(Option<string>.None);

        public RestRequestMany<User> Get(Option<string> q)
        {
            return RestRequestMany<User>.Get(context)
                .AddQueryParameter("q", q);
        }

        public UserEndpoint this[int id] => new UserEndpoint(context, id);
    }

    public class UserEndpoint
    {
        private readonly SearchContext context;

        public UserEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(context);

        public UserTracksEndpoint Tracks => new UserTracksEndpoint(context);
        public UserPlaylistsEndpoint Playlists => new UserPlaylistsEndpoint(context);
        public FollowingsEndpoint Followings => new FollowingsEndpoint( context);
        public FollowersEndpoint Followers => new FollowersEndpoint( context);
        public UserCommentsEndpoint Comments => new UserCommentsEndpoint( context);
        public FavoritesEndpoint Favorites => new FavoritesEndpoint( context);
        public WebProfilesEndpoint WebProfiles => new WebProfilesEndpoint( context);
    }
    
    public class UserTracksEndpoint
    {
        private readonly SearchContext context;

        public UserTracksEndpoint(SearchContext context)
        {
            this.context = context.Add("tracks");
        }

        public RestRequestMany<Track> Get() => RestRequestMany<Track>.Get(context);
    }
    
    public class UserPlaylistsEndpoint
    {
        private readonly SearchContext context;

        public UserPlaylistsEndpoint(SearchContext context)
        {
            this.context = context.Add("playlists");
        }

        public RestRequestMany<Playlist> Get() => RestRequestMany<Playlist>.Get(context);
    }

    public class FollowingsEndpoint
    {
        private readonly SearchContext context;

        public FollowingsEndpoint(SearchContext context)
        {
            this.context = context.Add("followings");
        }

        public RestRequestMany<User> Get() => RestRequestMany<User>.Get(context);

        public UserFollowingEndpoint this[int id] => new UserFollowingEndpoint(context, id);
    }

    public class UserFollowingEndpoint
    {
        private readonly SearchContext context;

        public UserFollowingEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(context);

        public RestRequest<DeleteResponse> Put() => RestRequest<DeleteResponse>.Put(context);

        public RestRequest<User> Delete() => RestRequest<User>.Delete(context);
    }

    public class FollowersEndpoint
    {
        private readonly SearchContext context;

        public FollowersEndpoint(SearchContext context)
        {
            this.context = context.Add("followers");
        }

        public RestRequestMany<User> Get() => RestRequestMany<User>.Get(context);

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

        public RestRequestMany<Comment> Get() => RestRequestMany<Comment>.Get(context);
    }
    
    public class FavoritesEndpoint
    {
        private readonly SearchContext context;

        public FavoritesEndpoint(SearchContext context)
        {
            this.context = context.Add("favorites");
        }

        public RestRequestMany<Track> Get() => RestRequestMany<Track>.Get(context);

        public FavoriteEndpoint this[int id] => new FavoriteEndpoint(context,id);
    }

    public class FavoriteEndpoint
    {
        private readonly SearchContext context;

        public FavoriteEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<Track> Get() => RestRequest<Track>.Get(context);

        public RestRequest<Track> Put() => RestRequest<Track>.Put(context);
        
        public RestRequest<DeleteResponse> Delete() => RestRequest<DeleteResponse>.Delete(context); 
    }

    public class WebProfilesEndpoint 
    {
        private readonly SearchContext context;

        public WebProfilesEndpoint(SearchContext context)
        {
            this.context = context.Add("web-profiles");
        }

        public RestRequestMany<WebProfile> Get() => RestRequestMany<WebProfile>.Get(context);

        //todo PUT DELETE
    }
}