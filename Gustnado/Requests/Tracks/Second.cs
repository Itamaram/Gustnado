using System.Collections.Generic;
using Gustnado.Extensions;
using Gustnado.Objects;
using RestSharp;

namespace Gustnado.Requests.Tracks
{
    //todo override json serializer?
    public class RestRequest<T> : RestRequest
    {
        public RestRequest(SearchContext context, Method method)
            : base(context.AsResource(), method) { }

        public RestRequest<T> AddSoundCloudObject<A>(A item)
        {
            return this.WriteToRequest(item);
        }

        public static RestRequest<T> Get(SearchContext context)
        {
            return new RestRequest<T>(context, Method.GET);
        }

        public static RestRequest<T> Post(SearchContext context)
        {
            return new RestRequest<T>(context, Method.POST);
        }

        public static RestRequest<T> Put(SearchContext context)
        {
            return new RestRequest<T>(context, Method.PUT);
        }

        public static RestRequest<T> Delete(SearchContext context)
        {
            return new RestRequest<T>(context, Method.DELETE);
        }
    }

    public class RestRequestMany<T> : RestRequest
    {
        public RestRequestMany(SearchContext context)
            : base(context.AsResource(), Method.GET) { }
    }

    public class TracksRequest
    {
        private static readonly SearchContext context = new SearchContext("tracks");

        public RestRequestMany<Track> Get()
        {
            return new RestRequestMany<Track>(context);
        }

        public RestRequestMany<Track> Get(TracksRequestFilter filter)
        {
            return new RestRequestMany<Track>(context)
                .WriteToQueryString(filter);
        }

        public RestRequest<Track> Post(Track track)
        {
            return RestRequest<Track>.Post(context)
                .AddSoundCloudObject(track);
        }

        public TrackRequest this[int id] => new TrackRequest(context, id);
    }

    public class TrackRequest
    {
        private readonly SearchContext context;

        public TrackRequest(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<Track> Get() => RestRequest<Track>.Get(context);

        public RestRequest<Track> Put(Track track)
        {
            return RestRequest<Track>.Put(context)
                .AddSoundCloudObject(track);
        }

        public RestRequest<object> Delete() => RestRequest<object>.Delete(context);

        public TrackCommentsRequest Comments => new TrackCommentsRequest(context);
        public TrackFavoritersRequest Favoriters => new TrackFavoritersRequest(context);
        public TrackSecretTokenRequest SecretToken => new TrackSecretTokenRequest(context);
    }

    public class TrackCommentsRequest
    {
        private readonly SearchContext context;

        public TrackCommentsRequest(SearchContext context)
        {
            this.context = context.Add("comments");
        }

        public RestRequestMany<Comment> Get()
        {
            return new RestRequestMany<Comment>(context);
        }

        public RestRequest<Comment> Post(Comment comment)
        {
            return RestRequest<Comment>.Post(context)
                //todo pretty sure this is wrong, Should be sending as json, not as request body
                .AddSoundCloudObject(comment);
        }

        public TrackCommentRequest this[int id] => new TrackCommentRequest(context, id);
    }

    public class TrackCommentRequest
    {
        private readonly SearchContext context;

        public TrackCommentRequest(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<Comment> Get() => RestRequest<Comment>.Get(context);

        public RestRequest<Comment> Put(Comment comment)
        {
            return RestRequest<Comment>.Put(context)
                .AddSoundCloudObject(comment);
        }
    }

    public class TrackFavoritersRequest
    {
        private readonly SearchContext context;

        public TrackFavoritersRequest(SearchContext context)
        {
            this.context = context.Add("favoriters");
        }

        public RestRequest<IEnumerable<User>> Get() => RestRequest<IEnumerable<User>>.Get(context);

        public TrackFavoriterRequest this[int id] => new TrackFavoriterRequest(context, id);
    }

    public class TrackFavoriterRequest
    {
        private readonly SearchContext context;

        public TrackFavoriterRequest(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(context);
    }

    public class TrackSecretTokenRequest
    {
        private readonly SearchContext contexet;

        public TrackSecretTokenRequest(SearchContext contexet)
        {
            this.contexet = contexet.Add("secret-token");
        }

        public RestRequest<object> Put(string secret)
        {
            return RestRequest<object>.Put(contexet)
                .AddSoundCloudObject(new SecretTokenContainer { SecretToken = secret });
        }

        public RestRequest<SecretTokenContainer> Get()
        {
            return RestRequest<SecretTokenContainer>.Get(contexet);
        }
    }

    //todo this is probably definitely wrong
    public class SecretTokenContainer
    {
        public string SecretToken { get; set; }
    }
}