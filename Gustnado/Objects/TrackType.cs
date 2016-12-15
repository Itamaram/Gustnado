using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Objects
{
    [JsonConverter(typeof(EnumConverter<TrackType>))]
    public enum TrackType
    {
        [JsonValue("original")]
        Original,
        [JsonValue("remix")]
        Remix,
        [JsonValue("live")]
        Live,
        [JsonValue("recording")]
        Recording,
        [JsonValue("spoken")]
        Spoken,
        [JsonValue("podcast")]
        Podcast,
        [JsonValue("demo")]
        Demo,
        [JsonValue("in progress")]
        InProgress,
        [JsonValue("stem")]
        Stem,
        [JsonValue("loop")]
        Loop,
        [JsonValue("sound effect")]
        SoundEffect,
        [JsonValue("sample")]
        Sample,
        [JsonValue("other")]
        Other
    }
}