using Newtonsoft.Json;

namespace Gustnado.Objects
{
    public class App
    {
        /// <summary>
        /// integer ID
        /// </summary>
        /// <example>123</example>
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// API resource URL
        /// </summary>
        /// <example>"http://api.soundcloud.com/apps/124"</example>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// URL to the SoundCloud.com page
        /// </summary>
        /// <example>"http://soundcloud.com/bryan/sbahn-sounds"</example>
        [JsonProperty("permalink_url")]
        public string PermalinkUrl { get; set; }

        /// <summary>
        /// URL to an external site
        /// </summary>
        /// <example>"http://itunes.com/app/soundcloud"</example>
        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }

        /// <summary>
        /// username of the app creator
        /// </summary>
        /// <example>"weatherman"</example>
        [JsonProperty("creator")]
        public string Creator { get; set; }
    }
}