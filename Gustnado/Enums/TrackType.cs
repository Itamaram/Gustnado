using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Enums
{
    [JsonConverter(typeof(EnumConverter<TrackType>))]
    public enum TrackType
    {
        [JsonProperty("original")]
        Original,
        [JsonProperty("remix")]
        Remix,
        [JsonProperty("live")]
        Live,
        [JsonProperty("recording")]
        Recording,
        [JsonProperty("spoken")]
        Spoken,
        [JsonProperty("podcast")]
        Podcast,
        [JsonProperty("demo")]
        Demo,
        [JsonProperty("in progress")]
        InProgress,
        [JsonProperty("stem")]
        Stem,
        [JsonProperty("loop")]
        Loop,
        [JsonProperty("sound effect")]
        SoundEffect,
        [JsonProperty("sample")]
        Sample,
        [JsonProperty("other")]
        Other
    }
}