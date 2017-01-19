using System;
using System.Collections.Generic;
using Gustnado.Converters;
using Gustnado.Enums;
using Gustnado.Extensions;
using Gustnado.Objects;
using Newtonsoft.Json;
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

    public class TracksRequestFilter
    {
        /// <summary>
        /// a string to search for (see search documentation)
        /// </summary>
        [JsonProperty("q")]
        public string Q { get; set; }

        /// <summary>
        /// a comma separated list of tags
        /// </summary>
        [JsonProperty("tags")]
        [JsonConverter(typeof(CommaSeparatedList))]
        public List<string> Tags { get; set; }

        /// <summary>
        /// (all,public,private)
        /// </summary>
        [JsonProperty("filter")]
        public TrackVisibility? Filter { get; set; }

        /// <summary>
        /// Filter on license. (see license attribute)
        /// </summary>
        [JsonProperty("license")]
        public License? License { get; set; }

        /// <summary>
        /// return tracks with at least this bpm value
        ///</summary>
        [JsonProperty("bpm[from]")]
        public int? BpmFrom { get; set; }

        /// <summary>
        /// return tracks with at most this bpm value
        ///</summary>
        [JsonProperty("bpm[to]")]
        public int? BpmTo { get; set; }

        /// <summary>
        /// return tracks with at least this duration (in millis)
        ///</summary>
        [JsonProperty("duration[from]")]
        public int? DurationFrom { get; set; }

        /// <summary>
        /// return tracks with at most this duration (in millis)
        ///</summary>
        [JsonProperty("duration[to]")]
        public int? DurationTo { get; set; }

        /// <summary>
        /// (yyyy-mm-dd hh:mm:ss) return tracks created at this date or later
        ///</summary>
        [JsonProperty("created_at[from]")]
        [JsonConverter(typeof(SoundCloudDateTime))]
        public DateTime? CreateAtFrom { get; set; }

        /// <summary>
        /// (yyyy-mm-dd hh:mm:ss) return tracks created at this date or earlier
        ///</summary>
        [JsonProperty("created_at[to]")]
        [JsonConverter(typeof(SoundCloudDateTime))]
        public DateTime? CreatedAtTo { get; set; }

        /// <summary>
        /// a comma separated list of track ids to filter on
        ///</summary>
        [JsonProperty("ids")]
        [JsonConverter(typeof(CommaSeparatedList))]
        public List<int> Ids { get; set; }

        /// <summary>
        /// a comma separated list of genres
        ///</summary>
        [JsonProperty("genres")]
        [JsonConverter(typeof(CommaSeparatedList))]
        public List<string> Genres { get; set; }

        /// <summary>
        /// a comma separated list of types
        ///</summary>
        [JsonProperty("types")]
        [JsonConverter(typeof(CommaSeparatedList))]
        public List<TrackType> Types { get; set; }
    }
}