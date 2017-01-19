using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bearded.Monads;
using Gustnado.Converters;
using Gustnado.Enums;
using Gustnado.Objects;
using Newtonsoft.Json;

namespace Gustnado.Requests.Tracks
{
    //public static class TracksRequestExtensions
    //{
    //    public static Task<IEnumerable<Track>> Get(this TracksRequest r)
    //    {
    //        return r.Get(Option<TracksRequestFilter>.None);
    //    }
    //}

    //public interface UnauthedTracksRequest
    //{
    //    Task<IEnumerable<Track>> Get(Option<TracksRequestFilter> filter);
    //    UnauthedTrackRequest this[int id] { get; }
    //}

    //public interface AuthedTracksRequest
    //{
    //    Task<IEnumerable<Track>> Get(Option<TracksRequestFilter> filter);
    //    Task<Track> Post(Track track);
    //    AuthedTrackRequest this[int id] { get; }
    //}

    //public interface AuthedTrackRequest
    //{
    //    Task<Track> Get();
    //    Task<Track> Put(Track track);
    //    Task<Track> Delete();
    //    AuthedCommentsRequest Comments { get; }
    //    AuthedFavoritersRequest Favoriters { get; }
    //    AuthedSecretTokenRequest SecretToken { get; }
    //}

    //public interface AuthedFavoritersRequest
    //{

    //}

    //public interface AuthedCommentsRequest
    //{
    //    Task<IEnumerable<Comment>> Get();
    //    Task<Comment> Post(Comment comment);
    //    AuthedCommentRequest this[int id] { get; }
    //}

    //public class TracksRequest : UnauthedTracksRequest
    //{
    //    private readonly SoundCloudHttpClient client;
    //    private readonly SearchContext context;

    //    public TracksRequest(SoundCloudHttpClient client, SearchContext context)
    //    {
    //        this.client = client;
    //        this.context = context;
    //    }

    //    public Task<IEnumerable<Track>> Get(Option<TracksRequestFilter> filter)
    //    {
    //        return client.FetchMany<Track>(context, filter.Map(QueryStringFormatter.FromObject).ElseEmpty());
    //    }

    //    public UnauthedTrackRequest this[int id] => new TrackRequest(client, context.Add(id));
    //}

    //public interface UnauthedTrackRequest
    //{
    //    Task<Track> Get();
    //    UnauthedCommentsRequest Comments { get; }
    //    UnauthedFavoritersRequest Favoriters { get; }
    //    //secret token? Can you get it if unauthed? Surely not?
    //}

    //public class TrackRequest : UnauthedTrackRequest
    //{
    //    private readonly SoundCloudHttpClient client;
    //    private readonly SearchContext context;

    //    public TrackRequest(SoundCloudHttpClient client, SearchContext context)
    //    {
    //        this.client = client;
    //        this.context = context;
    //    }

    //    public Task<Track> Get() => client.Fetch<Track>(context);

    //    public UnauthedCommentsRequest Comments => new CommentsRequest(client, context.Add("comments"));
    //    public UnauthedFavoritersRequest Favoriters => new FavoritersRequest(client, context.Add("favoriters"));
    //}

    //public interface UnauthedFavoritersRequest
    //{
    //    Task<IEnumerable<User>> Get();
    //    UnauthedFavoriteRequest this[int id] { get; }
    //}

    //public class FavoritersRequest : UnauthedFavoritersRequest
    //{
    //    private readonly SoundCloudHttpClient client;
    //    private readonly SearchContext context;

    //    public FavoritersRequest(SoundCloudHttpClient client, SearchContext context)
    //    {
    //        this.client = client;
    //        this.context = context;
    //    }

    //    public Task<IEnumerable<User>> Get() => client.FetchMany<User>(context);

    //    public UnauthedFavoriteRequest this[int id] => new FavoriteRequest(client, context.Add(id));
    //}

    //public interface UnauthedCommentsRequest
    //{
    //    Task<IEnumerable<Comment>> Get();
    //    UnauthedCommentRequest this[int id] { get; }
    //}

    //public class CommentsRequest : UnauthedCommentsRequest
    //{
    //    private readonly SoundCloudHttpClient client;
    //    private readonly SearchContext context;

    //    public CommentsRequest(SoundCloudHttpClient client, SearchContext context)
    //    {
    //        this.client = client;
    //        this.context = context;
    //    }

    //    public Task<IEnumerable<Comment>> Get() => client.FetchMany<Comment>(context);

    //    public UnauthedCommentRequest this[int id] => new CommentRequest(client, context.Add(id));
    //}

    //public interface UnauthedFavoriteRequest
    //{
    //    Task<User> Get();
    //}

    //public class FavoriteRequest : UnauthedFavoriteRequest
    //{
    //    private readonly SoundCloudHttpClient client;
    //    private readonly SearchContext context;

    //    public FavoriteRequest(SoundCloudHttpClient client, SearchContext context)
    //    {
    //        this.client = client;
    //        this.context = context;
    //    }

    //    public Task<User> Get() => client.Fetch<User>(context);
    //}

    //public interface UnauthedCommentRequest
    //{
    //    Task<Comment> Get();
    //}

    //public class CommentRequest : UnauthedCommentRequest
    //{
    //    private readonly SoundCloudHttpClient client;
    //    private readonly SearchContext context;

    //    public CommentRequest(SoundCloudHttpClient client, SearchContext context)
    //    {
    //        this.client = client;
    //        this.context = context;
    //    }

    //    public Task<Comment> Get() => client.Fetch<Comment>(context);
    //}

    public interface ParameterFormatter
    {
        string Format(object o);
    }

    public class DefaultFormatter : ParameterFormatter
    {
        public string Format(object o) => o.ToString();
    }

    public abstract class ParameterFormatter<T> : ParameterFormatter
    {
        protected abstract string Format(T t);

        public string Format(object o) => Format((T)o);
    }

    public class StringBuildingWriter : JsonWriter
    {
        private readonly StringBuilder builder = new StringBuilder();

        public override void WriteValue(string value)
        {
            builder.Append(value);
            base.WriteValue(value);
        }

        public override void WriteValue(int value)
        {
            builder.Append(value);
            base.WriteValue(value);
        }

        public string Value => builder.ToString();

        public void Append(string s) => builder.Append(s);

        public override void Flush() { }
    }

    public class CommaSeparatedList : JsonConverter
    {
        public override bool CanRead { get; } = false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            using (var sb = new StringBuildingWriter())
            {
                var e = (value as IEnumerable).GetEnumerator();

                if (!e.MoveNext())
                    return;

                serializer.Serialize(sb, e.Current);

                while (e.MoveNext())
                {
                    sb.Append(",");
                    serializer.Serialize(sb, e.Current);
                }

                writer.WriteValue(sb.Value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType) => typeof(IEnumerable).IsAssignableFrom(objectType);
    }

    [JsonConverter(typeof(EnumConverter<TrackVisibility>))]
    public enum TrackVisibility
    {
        [JsonProperty("all")]
        All,
        [JsonProperty("public")]
        Public,
        [JsonProperty("private")]
        Private
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

    [AttributeUsage(AttributeTargets.Class)]
    public class RequestBodyKeyFormatAttribute : Attribute
    {
        public string Format { get; }
        public RequestBodyKeyFormatAttribute(string format)
        {
            Format = format;
        }
    }
}