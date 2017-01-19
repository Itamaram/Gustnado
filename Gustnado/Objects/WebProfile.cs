using System;
using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Objects
{
    public class WebProfile
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(SoundCloudDateTime))]
        public DateTime? CreatedAt { get; set; }
    }
}