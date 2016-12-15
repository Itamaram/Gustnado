using System;
using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Objects
{
    public class Comment
    {
        /// <summary>
        /// integer ID
        /// </summary>
        /// <example>123</example>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// API resource URL
        /// </summary>
        /// <example>"http://api.soundcloud.com/comments/32562"</example>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// timestamp of creation
        /// </summary>
        /// <example>"2009/08/13 18:30:10 +0000"</example>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(SoundCloudDateTime))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// HTML comment body
        /// </summary>
        /// <example>"i love this beat!"</example>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// associated timestamp in milliseconds
        /// </summary>
        /// <example>55593</example>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// user id of the owner
        /// </summary>
        /// <example>343</example>
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// mini user representation of the owner
        /// </summary>
        /// <example>{id: 343, username: "Doctor Wilson"...}</example>
        [JsonProperty("user")]
        public User User { get; set; }

        /// <summary>
        /// the track id of the related track
        /// </summary>
        /// <example>54</example>
        [JsonProperty("track_id")]
        public int TrackId { get; set; }
    }
}