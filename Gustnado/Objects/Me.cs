using Newtonsoft.Json;

namespace Gustnado.Objects
{
    public class Me : User
    {
        /// <summary>
        /// subscription plan of the user
        /// </summary>
        /// <example>Pro Plus</example>
        [JsonProperty("plan")]
        public string Plan { get; set; }

        /// <summary>
        /// number of private tracks
        /// </summary>
        /// <example>34</example>
        [JsonProperty("private_tracks_count")]
        public int? PrivateTracksCount { get; set; }

        /// <summary>
        /// number of private playlists
        /// </summary>
        /// <example>6</example>
        [JsonProperty("private_playlists_count")]
        public int? PrivatePlaylistsCount { get; set; }

        /// <summary>
        /// boolean if email is confirmed
        /// </summary>
        /// <example>true</example>
        [JsonProperty("primary_email_confirmed")]
        public bool? PrimaryEmailConfirmed { get; set; }
    }
}