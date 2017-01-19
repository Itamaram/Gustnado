using System;
using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Objects
{
    public class Connection
    {
        /// <summary>
        /// <example>2010/12/05 16:46:34 +0000</example>
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(SoundCloudDateTime))]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// <example>a facebook artist page</example>
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// <example>313104</example>
        /// </summary>
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// <example>false</example>
        /// </summary>
        [JsonProperty("post_favorite")]
        public bool? PostFavorite { get; set; }

        /// <summary>
        /// <example>false</example>
        /// </summary>
        [JsonProperty("post_publish")]
        public bool? PostPublish { get; set; }

        /// <summary>
        /// <example>facebook_page</example>
        /// </summary>
        [JsonProperty("service")]
        public ConnectionService? Service { get; set; }

        /// <summary>
        /// <example>facebook_page</example>
        /// </summary>
        [JsonProperty("type")]
        public ConnectionService? Type { get; set; }

        /// <summary>
        /// <example>https://api.soundcloud.com/connections/313104</example>
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }

    }

    [JsonConverter(typeof(EnumConverter<ConnectionService>))]
    public enum ConnectionService
    {
        [JsonProperty("facebook")]
        Facebook,
        [JsonProperty("twitter")]
        Twitter,
        [JsonProperty("myspace")]
        MySpace
    }
}