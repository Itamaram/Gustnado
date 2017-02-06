using System;
using System.Collections.Generic;
using Gustnado.Converters;
using Gustnado.Enums;
using Newtonsoft.Json;

namespace Gustnado.Objects
{
    public class Playlist
    {
        /// <summary>
        /// integer ID
        /// </summary>
        /// <example>123</example>
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// timestamp of creation
        /// </summary>
        /// <example>"2009/08/13 18:30:10 +0000"</example>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(SoundCloudDateTime))]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// user-id of the owner
        /// </summary>
        /// <example>343</example>
        [JsonProperty("user_id")]
        public int? UserId { get; set; }

        /// <summary>
        /// mini user representation of the owner
        /// </summary>
        /// <example>{id: 343, username: "Doctor Wilson"...}</example>
        [JsonProperty("user")]
        public User User { get; set; }

        /// <summary>
        /// track title
        /// </summary>
        /// <example>"S-Bahn Sounds"</example>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// permalink of the resource
        /// </summary>
        /// <example>"sbahn-sounds"</example>
        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        /// <summary>
        /// URL to the SoundCloud.com page
        /// </summary>
        /// <example>"http://soundcloud.com/bryan/sbahn-sounds"</example>
        [JsonProperty("permalink_url")]
        public string PermalinkUrl { get; set; }

        /// <summary>
        /// API resource URL
        /// </summary>
        /// <example>"http://api.soundcloud.com/tracks/123"</example>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// public/private sharing
        /// </summary>
        /// <example>"public"</example>
        [JsonProperty("sharing")]
        public string Sharing { get; set; }

        /// <summary>
        /// who can embed this track or playlist
        /// </summary>
        /// <example>"all", "me", or "none"</example>
        [JsonProperty("embeddable_by")]
        public EmbeddableBy? EmbeddableBy { get; set; }

        /// <summary>
        /// external purchase link
        /// </summary>
        /// <example>"http://amazon.com/buy/a43aj0b03"</example>
        [JsonProperty("purchase_url")]
        public string PurchaseUrl { get; set; }

        /// <summary>
        /// URL to a JPEG image
        /// </summary>
        /// <example>"http://i1.sndcdn.com/a....-large.jpg?142a848"</example>
        [JsonProperty("artwork_url")]
        public string ArtworkUrl { get; set; }

        /// <summary>
        /// HTML description
        /// </summary>
        /// <example>"my first track"</example>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// label mini user object
        /// </summary>
        /// <example>{id:123, username: "BeatLabel"...}</example>
        [JsonProperty("label")]
        public User Label { get; set; }

        /// <summary>
        /// duration in milliseconds
        /// </summary>
        /// <example>1203400</example>
        [JsonProperty("duration")]
        public long? Duration { get; set; }

        /// <summary>
        /// genre
        /// </summary>
        /// <example>"HipHop"</example>
        [JsonProperty("genre")]
        public string Genre { get; set; }

        /// <summary>
        /// list of tags
        /// </summary>
        /// <example>"tag1 \"hip hop\" geo:lat=32.444 geo:lon=55.33"</example>
        [JsonProperty("tag_list")]
        public string TagList { get; set; }

        /// <summary>
        /// id of the label user
        /// </summary>
        /// <example>54677</example>
        [JsonProperty("label_id")]
        public string LabelId { get; set; }

        /// <summary>
        /// label name
        /// </summary>
        /// <example>"BeatLabel"</example>
        [JsonProperty("label_name")]
        public string LabelName { get; set; }

        /// <summary>
        /// release number
        /// </summary>
        /// <example>3234</example>
        [JsonProperty("release")]
        public int? Release { get; set; }

        /// <summary>
        /// day of the release
        /// </summary>
        /// <example>21</example>
        [JsonProperty("release_day")]
        public int? ReleaseDay { get; set; }

        /// <summary>
        /// month of the release
        /// </summary>
        /// <example>5</example>
        [JsonProperty("release_month")]
        public int? ReleaseMonth { get; set; }

        /// <summary>
        /// year of the release
        /// </summary>
        /// <example>2001</example>
        [JsonProperty("release_year")]
        public int? ReleaseYear { get; set; }

        /// <summary>
        /// streamable via API (boolean)
        /// </summary>
        /// <example>true</example>
        /// <remarks>This will aggregate the playlists tracks streamable attribute. Its value will be nil if not all tracks have the same streamable value.</remarks>
        [JsonProperty("streamable")]
        public bool? Streamable { get; set; } //todo custom parser for nil?

        /// <summary>
        /// downloadable (boolean)
        /// </summary>
        /// <example>true</example>
        /// <remarks>This will aggregate the playlists tracks downloadable attribute. Its value will be nil if not all tracks have the same downloadable value.</remarks>
        [JsonProperty("downloadable")]
        public bool? Downloadable { get; set; }

        /// <summary>
        /// EAN identifier for the playlist
        /// </summary>
        /// <example>"123-4354345-43"</example>
        [JsonProperty("ean")]
        public string EAN { get; set; }

        /// <summary>
        /// playlist type
        /// </summary>
        /// <example>"recording"</example>
        [JsonProperty("playlist_type")]
        public PlaylistType? PlaylistType { get; set; }

        //todo tell the soundcloud api documentation team to get their head in the game
        [JsonProperty("tracks")]
        [JsonConverter(typeof(TrackCompactor))]
        public List<Track> Tracks { get; set; }

        [JsonProperty("tracks_uri")]
        public string TracksUri { get; set; }

        [JsonProperty("track_count")]
        public int? TrackCount { get; set; }
    }
}