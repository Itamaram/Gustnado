using System;
using System.Collections.Generic;
using Gustnado.Converters;
using Gustnado.Enums;
using Gustnado.Extensions;
using Gustnado.Objects;
using Gustnado.RestSharp;
using Newtonsoft.Json;

namespace Gustnado.Endpoints.Tracks
{
    public class TracksEndpoint
    {
        private static readonly SearchContext context = new SearchContext("tracks");

        public RestRequestMany<Track> Get()
        {
            return RestRequestMany<Track>.Get(context);
        }

        public RestRequestMany<Track> Get(TracksRequestFilter filter)
        {
            return RestRequestMany<Track>.Get(context)
                .WriteToQueryString(filter);
        }

        public RestRequest<Track> Put(Track track)
        {
            return RestRequest<Track>.Put(context)
                .WriteToRequest(track);
        }

        public TrackEndpoint this[int id] => new TrackEndpoint(context, id);
    }

    public class TrackEndpoint
    {
        private readonly SearchContext context;

        public TrackEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<Track> Get() => RestRequest<Track>.Get(context);

        public RestRequest<Track> Put(Track track)
        {
            return RestRequest<Track>.Put(context)
                .WriteToRequest(track);
        }

        public RestRequest<object> Delete() => RestRequest<object>.Delete(context);

        public TrackCommentsEndpoint Comments => new TrackCommentsEndpoint(context);
        public TrackFavoritersEndpoint Favoriters => new TrackFavoritersEndpoint(context);
        public TrackSecretTokenEndpoint SecretToken => new TrackSecretTokenEndpoint(context);
    }

    public class TrackCommentsEndpoint
    {
        private readonly SearchContext context;

        public TrackCommentsEndpoint(SearchContext context)
        {
            this.context = context.Add("comments");
        }

        public RestRequestMany<Comment> Get()
        {
            return RestRequestMany<Comment>.Get(context);
        }

        public RestRequest<Comment> Put(Comment comment)
        {
            return RestRequest<Comment>.Put(context)
                .AddJsonToRequestBody(comment);
        }

        public TrackCommentEndpoint this[int id] => new TrackCommentEndpoint(context, id);
    }

    public class TrackCommentEndpoint
    {
        private readonly SearchContext context;

        public TrackCommentEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<Comment> Get() => RestRequest<Comment>.Get(context);

        public RestRequest<Comment> Put(Comment comment)
        {
            return RestRequest<Comment>.Put(context)
                .AddJsonToRequestBody(comment);
        }

        public RestRequest<DeleteResponse> Delete() => RestRequest<DeleteResponse>.Delete(context);
    }

    public class TrackFavoritersEndpoint
    {
        private readonly SearchContext context;

        public TrackFavoritersEndpoint(SearchContext context)
        {
            this.context = context.Add("favoriters");
        }

        public RestRequestMany<User> Get() => RestRequestMany<User>.Get(context);

        public TrackFavoriterEndpoint this[int id] => new TrackFavoriterEndpoint(context, id);
    }

    public class TrackFavoriterEndpoint
    {
        private readonly SearchContext context;

        public TrackFavoriterEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<User> Get() => RestRequest<User>.Get(context);
    }

    public class TrackSecretTokenEndpoint
    {
        private readonly SearchContext contexet;

        public TrackSecretTokenEndpoint(SearchContext contexet)
        {
            this.contexet = contexet.Add("secret-token");
        }

        public RestRequest<SecretTokenContainer> Put(string secret)
        {
            return RestRequest<SecretTokenContainer>.Put(contexet)
                .AddJsonToRequestBody(new SecretTokenContainer { SecretToken = secret });
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