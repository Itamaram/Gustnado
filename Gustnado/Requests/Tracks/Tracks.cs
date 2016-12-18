﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bearded.Monads;
using Gustnado.Converters;
using Gustnado.Enums;
using Gustnado.Objects;

namespace Gustnado.Requests.Tracks
{
    public static class UnauthedTracksRequestExtensions
    {
        public static Task<IEnumerable<Track>> Get(this UnauthedTracksRequest r)
        {
            return r.Get(Option<TracksRequestFilter>.None);
        }
    }

    public interface UnauthedTracksRequest
    {
        Task<IEnumerable<Track>> Get(Option<TracksRequestFilter> filter);
        UnauthedTrackRequest this[int id] { get; }
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

        public Task<IEnumerable<Track>> Get(Option<TracksRequestFilter> filter)
        {
            return client.FetchMany<Track>(context, filter.Map(QueryStringFormatter.FromObject).ElseEmpty());
        }

        public UnauthedTrackRequest this[int id] => new TrackRequest(client, context.Add(id));
    }

    public interface UnauthedTrackRequest
    {
        Task<Track> Get();
        UnauthedCommentsRequest Comments { get; }
        UnauthedFavoritesRequest Favorites { get; }
        //secret token? Can you get it if unauthed? Surely not?
    }

    public class TrackRequest : UnauthedTrackRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public TrackRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<Track> Get() => client.Fetch<Track>(context);

        public UnauthedCommentsRequest Comments => new CommentsRequest(client, context.Add("comments"));
        public UnauthedFavoritesRequest Favorites => new FavoritesRequest(client, context.Add("favorites"));
    }

    public interface UnauthedFavoritesRequest
    {
        Task<IEnumerable<User>> Get();
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

        public Task<IEnumerable<User>> Get() => client.FetchMany<User>(context);

        public UnauthedFavoriteRequest this[int id] => new FavoriteRequest(client, context.Add(id));
    }

    public interface UnauthedCommentsRequest
    {
        Task<IEnumerable<Comment>> Get();
        UnauthedCommentRequest this[int id] { get; }
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

        public UnauthedCommentRequest this[int id] => new CommentRequest(client, context.Add(id));
    }

    public interface UnauthedFavoriteRequest
    {
        Task<User> Get();
    }

    public class FavoriteRequest : UnauthedFavoriteRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public FavoriteRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<User> Get() => client.Fetch<User>(context);
    }

    public interface UnauthedCommentRequest
    {
        Task<Comment> Get();
    }

    public class CommentRequest : UnauthedCommentRequest
    {
        private readonly SoundCloudHttpClient client;
        private readonly SearchContext context;

        public CommentRequest(SoundCloudHttpClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<Comment> Get() => client.Fetch<Comment>(context);
    }

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

    public class CommaSeparatedList : ParameterFormatter<IEnumerable>
    {
        protected override string Format(IEnumerable t) => string.Join(",", t);
    }

    public class EnumFormatter<T> : ParameterFormatter<T>
    {
        protected override string Format(T t) => EnumJsonValueMap<T>.ToJson(t);
    }

    public class DateTimeFormatter : ParameterFormatter<DateTime>
    {
        protected override string Format(DateTime t) => t.ToString("yyyy-mm-dd HH:mm:ss");
    }

    public class CommaSeparatedEnum<T> : ParameterFormatter<List<T>>
    {
        protected override string Format(List<T> t) => string.Join(",", t.Select(EnumJsonValueMap<T>.ToJson));
    }

    public enum TrackVisibility
    {
        [JsonValue("all")]
        All,
        [JsonValue("public")]
        Public,
        [JsonValue("private")]
        Private
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class QueryParameterAttribute : Attribute
    {
        public QueryParameterAttribute(string key)
            : this(key, typeof(DefaultFormatter)) { }

        public QueryParameterAttribute(string key, Type formatter)
        {
            Key = key;
            Formatter = formatter;
        }

        public string Key { get; }

        public Type Formatter { get; }
    }

    public class TracksRequestFilter
    {
        /// <summary>
        /// a string to search for (see search documentation)
        /// </summary>
        [QueryParameter("q")]
        public string Q { get; set; }

        /// <summary>
        /// a comma separated list of tags
        /// </summary>
        [QueryParameter("tags", typeof(CommaSeparatedList))]
        public List<string> Tags { get; set; }

        /// <summary>
        /// (all,public,private)
        /// </summary>
        [QueryParameter("filter", typeof(EnumFormatter<TrackVisibility>))]
        public TrackVisibility? Filter { get; set; }

        /// <summary>
        /// Filter on license. (see license attribute)
        /// </summary>
        [QueryParameter("license", typeof(EnumFormatter<License>))]
        public License? License { get; set; }

        /// <summary>
        /// return tracks with at least this bpm value
        ///</summary>
        [QueryParameter("bpm[from]")]
        public int? BpmFrom { get; set; }

        /// <summary>
        /// return tracks with at most this bpm value
        ///</summary>
        [QueryParameter("bpm[to]")]
        public int? BpmTo { get; set; }

        /// <summary>
        /// return tracks with at least this duration (in millis)
        ///</summary>
        [QueryParameter("duration[from]")]
        public int? DurationFrom { get; set; }

        /// <summary>
        /// return tracks with at most this duration (in millis)
        ///</summary>
        [QueryParameter("duration[to]")]
        public int? DurationTo { get; set; }

        /// <summary>
        /// (yyyy-mm-dd hh:mm:ss) return tracks created at this date or later
        ///</summary>
        [QueryParameter("created_at[from]", typeof(DateTimeFormatter))]
        public DateTime? CreateAtFrom { get; set; }

        /// <summary>
        /// (yyyy-mm-dd hh:mm:ss) return tracks created at this date or earlier
        ///</summary>
        [QueryParameter("created_at[to]", typeof(DateTimeFormatter))]
        public DateTime? CreatedAtTo { get; set; }

        /// <summary>
        /// a comma separated list of track ids to filter on
        ///</summary>
        [QueryParameter("ids", typeof(CommaSeparatedList))]
        public List<int> Ids { get; set; }

        /// <summary>
        /// a comma separated list of genres
        ///</summary>
        [QueryParameter("genres", typeof(CommaSeparatedList))]
        public List<string> Genres { get; set; }

        /// <summary>
        /// a comma separated list of types
        ///</summary>
        [QueryParameter("types", typeof(CommaSeparatedEnum<TrackType>))]
        public List<TrackType> Types { get; set; }
    }

    public static class QueryStringFormatter
    {
        public static IEnumerable<KeyValuePair<string, string>> FromObject<T>(T item)
        {
            return from property in typeof(T).GetProperties()
                   from attribute in property.GetCustomAttribute<QueryParameterAttribute>().AsEnumerable()
                   let formatter = (ParameterFormatter)Activator.CreateInstance(attribute.Formatter)
                   let value = formatter.Format(property.GetMethod.Invoke(item))
                   select new KeyValuePair<string, string>(attribute.Key, value);
        }
    }
}