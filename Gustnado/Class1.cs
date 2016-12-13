using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado
{
    public class User
    {
        /// <summary>
        /// integer ID
        /// </summary>
        /// <example>123</example>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// permalink of the resource
        /// </summary>
        /// <example>sbahn-sounds</example>
        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        /// <summary>
        /// username
        /// </summary>
        /// <example>Doctor Wilson</example>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// API resource URL
        /// </summary>
        /// <example>http://api.soundcloud.com/comments/32562</example>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// URL to the SoundCloud.com page
        /// </summary>
        /// <example>http://soundcloud.com/bryan/sbahn-sounds</example>
        [JsonProperty("permalink_url")]
        public string PermalinkUrl { get; set; }

        /// <summary>
        ///URL to a JPEG image
        /// </summary>
        /// <example>"http://i1.sndcdn.com/avatars-000011353294-n0axp1-large.jpg"</example>
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        /// <summary>
        ///country
        /// </summary>
        /// <example>"Germany"</example>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        ///first and last name
        /// </summary>
        /// <example>"Tom Wilson"</example>
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        /// <summary>
        ///city
        /// </summary>
        /// <example>"Berlin"</example>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        ///description
        /// </summary>
        /// <example>"Buskers playing in the S-Bahn station in Berlin"</example>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///Discogs name
        /// </summary>
        /// <example>"myrandomband"</example>
        [JsonProperty("discogs-name")]
        public string DiscogName { get; set; }

        /// <summary>
        ///MySpace name
        /// </summary>
        /// <example>"myrandomband"</example>
        [JsonProperty("myspace-name")]
        public string MyspaceName { get; set; }

        /// <summary>
        ///a URL to the website
        /// </summary>
        /// <example>"http://facebook.com/myrandomband"</example>
        [JsonProperty("website")]
        public string Website { get; set; }

        /// <summary>
        ///a custom title for the website
        /// </summary>
        /// <example>"myrandomband on Facebook"</example>
        [JsonProperty("website-title")]
        public string WebsiteTitle { get; set; }

        /// <summary>
        ///online status (boolean)
        /// </summary>
        /// <example>true</example>
        [JsonProperty("online")]
        public bool Online { get; set; }

        /// <summary>
        ///number of public tracks
        /// </summary>
        /// <example>4</example>
        [JsonProperty("track_count")]
        public int TrackCount { get; set; }

        /// <summary>
        ///number of public playlists
        /// </summary>
        /// <example>5</example>
        [JsonProperty("playlist_count")]
        public int PlaylistCount { get; set; }

        /// <summary>
        ///number of followers
        /// </summary>
        /// <example>54</example>
        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        /// <summary>
        ///number of followed users
        /// </summary>
        /// <example>75</example>
        [JsonProperty("followings_count")]
        public int FollowingsCount { get; set; }

        /// <summary>
        ///number of favorited public tracks
        /// </summary>
        /// <example>7</example>
        [JsonProperty("public_favorites_count")]
        public int PublicFavoritesCount { get; set; }

        /// <summary>
        ///binary data of user avatar
        /// </summary>
        /// <example>(only for uploading)</example>
        [JsonProperty("avatar_data")]
        public object AvatarData { get; set; }
    }

    [JsonConverter(typeof(EnumConverter<EncodingState>))]
    public enum EncodingState
    {
        [JsonValue("processing")]
        Processing,
        [JsonValue("failed")]
        Failed,
        [JsonValue("finished")]
        Finished
    }

    public class Track
    {
        /// <summary>
        /// integer ID
        /// </summary>
        /// <example>123</example>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// timestamp of creation
        /// </summary>
        /// <example>2009/08/13 18:30:10 +0000</example>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        ///user-id of the owner
        ///</summary>
        /// <example>343</example>
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        /// <summary>
        ///mini user representation of the owner
        ///</summary>
        /// <example>{id: 343, username: "Doctor Wilson"...}</example>
        [JsonProperty("user")]
        public User User { get; set; }

        /// <summary>
        ///track title
        ///</summary>
        /// <example>"S-Bahn Sounds"</example>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///permalink of the resource
        ///</summary>
        /// <example>"sbahn-sounds"</example>
        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        /// <summary>
        ///URL to the SoundCloud.com page
        ///</summary>
        /// <example>"http://soundcloud.com/bryan/sbahn-sounds"</example>
        [JsonProperty("permalink_url")]
        public string PermalinkUrl { get; set; }

        /// <summary>
        ///API resource URL
        ///</summary>
        /// <example>"http://api.soundcloud.com/tracks/123"</example>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        ///public/private sharing
        ///</summary>
        /// <example>"public"</example>
        [JsonProperty("sharing")]
        public string Sharing { get; set; } //todo enum?

        /// <summary>
        ///who can embed this track or playlist
        ///</summary>
        /// <example>"all", "me", or "none"</example>
        [JsonProperty("embeddable_by")]
        public string EmbeddableBy { get; set; } //todo enum?

        /// <summary>
        ///external purchase link
        ///</summary>
        /// <example>"http://amazon.com/buy/a43aj0b03"</example>
        [JsonProperty("purchase_url")]
        public string PurchaseUrl { get; set; }

        /// <summary>
        ///URL to a JPEG image
        ///</summary>
        /// <example>"http://i1.sndcdn.com/a....-large.jpg?142a848"</example>
        [JsonProperty("artwork_url")]
        public string ArtworkUrl { get; set; }

        /// <summary>
        ///HTML description
        ///</summary>
        /// <example>"my first track"</example>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///label mini user object
        ///</summary>
        /// <example>{id:123, username: "BeatLabel"...}</example>
        [JsonProperty("label")]
        public User Label { get; set; }

        /// <summary>
        ///duration in milliseconds
        ///</summary>
        /// <example>1203400</example>
        [JsonProperty("duration")]
        public int Duration { get; set; }

        /// <summary>
        ///genre
        ///</summary>
        /// <example>"HipHop"</example>
        [JsonProperty("genre")]
        public string Genre { get; set; }

        /// <summary>
        ///list of tags
        ///</summary>
        /// <example>"tag1 \"hip hop\" geo:lat=32.444 geo:lon=55.33"</example>
        [JsonProperty("tag_list")]
        public string Tags { get; set; } //todo parse/format?

        /// <summary>
        ///id of the label user
        ///</summary>
        /// <example>54677</example>
        [JsonProperty("label_id")]
        public int? LabelId { get; set; }

        /// <summary>
        ///label name
        ///</summary>
        /// <example>"BeatLabel"</example>
        [JsonProperty("label_name")]
        public string LabelName { get; set; }

        /// <summary>
        ///release number
        ///</summary>
        /// <example>3234</example>
        [JsonProperty("release")]
        public int? Release { get; set; }

        /// <summary>
        ///day of the release
        ///</summary>
        /// <example>21</example>
        [JsonProperty("release_day")]
        public int? ReleaseDay { get; set; }

        /// <summary>
        ///month of the release
        ///</summary>
        /// <example>5</example>
        [JsonProperty("release_month")]
        public int? ReleaseMonth { get; set; }

        /// <summary>
        ///year of the release
        ///</summary>
        /// <example>2001</example>
        [JsonProperty("release_year")]
        public int? ReleaseYear { get; set; }

        /// <summary>
        ///streamable via API (boolean)
        ///</summary>
        /// <example>true</example>
        [JsonProperty("streamable")]
        public bool Streamable { get; set; }

        /// <summary>
        ///downloadable (boolean)
        ///</summary>
        /// <example>true</example>
        [JsonProperty("downloadable")]
        public bool Downloadable { get; set; }

        /// <summary>
        ///encoding state
        ///</summary>
        /// <example>"finished"</example>
        [JsonProperty("state")]
        public EncodingState State { get; set; }

        /// <summary>
        ///creative common license
        ///</summary>
        /// <example>"no-rights-reserved"</example>
        [JsonProperty("license")]
        public string License { get; set; } //todo enum

        /// <summary>
        ///track type
        ///</summary>
        /// <example>"recording"</example>
        [JsonProperty("track_type")]
        public string TrackType { get; set; } //todo enum

        /// <summary>
        ///URL to PNG waveform image
        ///</summary>
        /// <example>"http://w1.sndcdn.com/fxguEjG4ax6B_m.png"</example>
        [JsonProperty("waveform_url")]
        public string WaveformUrl { get; set; }

        /// <summary>
        ///URL to original file
        ///</summary>
        /// <example>"http://api.soundcloud.com/tracks/3/download"</example>
        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }

        /// <summary>
        ///link to 128kbs mp3 stream
        ///</summary>
        /// <example>"http://api.soundcloud.com/tracks/3/stream"</example>
        [JsonProperty("stream_url")]
        public string StreamUrl { get; set; }

        /// <summary>
        ///a link to a video page
        ///</summary>
        /// <example>"http://vimeo.com/3302330"</example>
        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }

        /// <summary>
        ///beats per minute
        ///</summary>
        /// <example>120</example>
        [JsonProperty("bpm")]
        public int BPM { get; set; }

        /// <summary>
        ///track commentable (boolean)
        ///</summary>
        /// <example>true</example>
        [JsonProperty("commentable")]
        public bool Commentable { get; set; }

        /// <summary>
        ///track ISRC
        ///</summary>
        /// <example>"I123-545454"</example>
        [JsonProperty("isrc")]
        public string ISRC { get; set; }

        /// <summary>
        ///track key
        ///</summary>
        /// <example>"Cmaj"</example>
        [JsonProperty("key_signature")]
        public string KeySignature { get; set; }

        /// <summary>
        ///track comment count
        ///</summary>
        /// <example>12</example>
        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        /// <summary>
        ///track download count
        ///</summary>
        /// <example>45</example>
        [JsonProperty("download_count")]
        public int DownloadCount { get; set; }

        /// <summary>
        ///track play count
        ///</summary>
        /// <example>435</example>
        [JsonProperty("playback_count")]
        public int PlaybackCount { get; set; }

        /// <summary>
        ///track favoriting count
        ///</summary>
        /// <example>6</example>
        [JsonProperty("favoritings_count")]
        public int FavoritingsCount { get; set; }

        /// <summary>
        ///file format of the original file
        ///</summary>
        /// <example>"aiff"</example>
        [JsonProperty("original_format")]
        public string OriginalFormat { get; set; }

        /// <summary>
        ///size in bytes of the original file
        ///</summary>
        /// <example>10211857</example>
        [JsonProperty("original_content_size")]
        public int OriginalContentSize { get; set; }

        /// <summary>
        ///binary data of the audio file
        ///</summary>
        /// <example>(only for uploading)</example>
        [JsonProperty("asset_data")]
        public object AssetData { get; set; } //todo blob? bytes?

        /// <summary>
        ///binary data of the artwork image
        ///</summary>
        /// <example>(only for uploading)</example>
        [JsonProperty("artwork_data")]
        public object ArtworkData { get; set; }

        /// <summary>
        ///track favorite of current user (boolean, authenticated requests only)
        ///</summary>
        /// <example>1</example>
        [JsonProperty("user_favorite")]
        public bool? UserFavorite { get; set; }//custom 0/1 converter?
    }

    public class PlayList
    {
        /// <summary>
        /// integer ID
        /// </summary>
        /// <example>123</example>
        [JsonProperty("id")]
        public int Id { get; set; }
    }

    public interface IMakeWebRequests
    {
        Task<string> GetStringAsync(string endpoint, IEnumerable<KeyValuePair<string, string>> queries);
    }

    public class HttpClientImplementation : IMakeWebRequests, IDisposable
    {
        private readonly HttpClient client;
        public HttpClientImplementation()
        {
            client = new HttpClient();
        }

        public Task<string> GetStringAsync(string endpoint, IEnumerable<KeyValuePair<string, string>> queries)
        {
            var query = QueryString(queries);
            return client.GetStringAsync(endpoint + (query.Any() ? "?" + query : string.Empty));
        }

        private static string QueryString(IEnumerable<KeyValuePair<string, string>> queries)
        {
            return string.Join("&", queries.Select(q => $"{q.Key}={q.Value}"));
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }

    public static class Constants
    {
        public static string ApiEndpoint = "http://api.soundcloud.com";
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<A> Yield<A>(this A a)
        {
            yield return a;
        }
    }

    public class ApiClient
    {
        private readonly KeyValuePair<string, string> clientId;
        private readonly IMakeWebRequests web;

        public ApiClient(string clientId, IMakeWebRequests web)
        {
            this.clientId = new KeyValuePair<string, string>("client_id", clientId);
            this.web = web;
        }

        public UsersRequest Users => new UsersRequest(this, new SearchContext("users"));

        public async Task<T> Get<T>(SearchContext context)
        {
            var result = await web.GetStringAsync($"{Constants.ApiEndpoint}{context}", clientId.Yield());
            return JsonConvert.DeserializeObject<T>(result);
        }
    }

    public class SearchContext
    {
        private readonly IReadOnlyList<string> terms;

        public SearchContext(params string[] terms)
        {
            this.terms = terms.ToList();
        }

        public SearchContext(IEnumerable<string> terms)
        {
            this.terms = terms.ToList();
        }

        public SearchContext Add(string s)
        {
            return new SearchContext(new List<string>(terms) { s });
        }

        public override string ToString() => "/" + string.Join("/", terms);
    }

    public class UsersRequest
    {
        private readonly ApiClient client;
        private readonly SearchContext context;

        public UsersRequest(ApiClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<User>> Get(/*query?*/)
        {
            return client.Get<IEnumerable<User>>(context);
        }

        public UserRequest this[int id] => this[$"{id}"];

        public UserRequest this[string id] => new UserRequest(client, context.Add(id));
    }

    public class UserRequest
    {
        private readonly ApiClient client;
        private readonly SearchContext context;

        public UserRequest(ApiClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<User> Get()
        {
            return client.Get<User>(context);
        }
    }

    public class TracksRequest
    {
        private readonly ApiClient client;
        private readonly SearchContext context;

        public TracksRequest(ApiClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<IEnumerable<Track>> Get()
        {
            return client.Get<IEnumerable<Track>>(context);
        }

        public TrackRequest this[int id] => this[$"{id}"];

        public TrackRequest this[string id] => new TrackRequest(client, context.Add(id));
    }

    public class TrackRequest
    {
        private readonly ApiClient client;
        private readonly SearchContext context;

        public TrackRequest(ApiClient client, SearchContext context)
        {
            this.client = client;
            this.context = context;
        }

        public Task<Track> Get() => client.Get<Track>(context);
    }
}
