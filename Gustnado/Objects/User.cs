using Gustnado.Serialisation;
using Newtonsoft.Json;

namespace Gustnado.Objects
{
    public class User
    {
        /// <summary>
        /// integer ID
        /// </summary>
        /// <example>123</example>
        [JsonProperty("id")]
        public int? Id { get; set; }

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
        /// URL to a JPEG image
        /// </summary>
        /// <example>"http://i1.sndcdn.com/avatars-000011353294-n0axp1-large.jpg"</example>
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// country
        /// </summary>
        /// <example>"Germany"</example>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// first and last name
        /// </summary>
        /// <example>"Tom Wilson"</example>
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// city
        /// </summary>
        /// <example>"Berlin"</example>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// description
        /// </summary>
        /// <example>"Buskers playing in the S-Bahn station in Berlin"</example>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Discogs name
        /// </summary>
        /// <example>"myrandomband"</example>
        [JsonProperty("discogs-name")]
        public string DiscogName { get; set; }

        /// <summary>
        /// MySpace name
        /// </summary>
        /// <example>"myrandomband"</example>
        [JsonProperty("myspace-name")]
        public string MyspaceName { get; set; }

        /// <summary>
        /// a URL to the website
        /// </summary>
        /// <example>"http://facebook.com/myrandomband"</example>
        [JsonProperty("website")]
        public string Website { get; set; }

        /// <summary>
        /// a custom title for the website
        /// </summary>
        /// <example>"myrandomband on Facebook"</example>
        [JsonProperty("website-title")]
        public string WebsiteTitle { get; set; }

        /// <summary>
        /// online status (boolean)
        /// </summary>
        /// <example>true</example>
        [JsonProperty("online")]
        public bool? Online { get; set; }

        /// <summary>
        /// number of public tracks
        /// </summary>
        /// <example>4</example>
        [JsonProperty("track_count")]
        public int? TrackCount { get; set; }

        /// <summary>
        /// number of public playlists
        /// </summary>
        /// <example>5</example>
        [JsonProperty("playlist_count")]
        public int? PlaylistCount { get; set; }

        /// <summary>
        /// number of followers
        /// </summary>
        /// <example>54</example>
        [JsonProperty("followers_count")]
        public int? FollowersCount { get; set; }

        /// <summary>
        /// number of followed users
        /// </summary>
        /// <example>75</example>
        [JsonProperty("followings_count")]
        public int? FollowingsCount { get; set; }

        /// <summary>
        /// number of favorited public tracks
        /// </summary>
        /// <example>7</example>
        [JsonProperty("public_favorites_count")]
        public int? PublicFavoritesCount { get; set; }

        /// <summary>
        /// binary data of user avatar
        /// </summary>
        /// <example>(only for uploading)</example>
        [JsonProperty("avatar_data")]
        [JsonConverter(typeof(AddToRequestBodyAsFile))]
        public string AvatarData { get; set; }
    }
}