using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Enums
{
    [JsonConverter(typeof(EnumConverter<PlaylistType>))]
    public enum PlaylistType
    {
        [JsonProperty("ep single")]
        EpSingle,
        [JsonProperty("album")]
        Album,
        [JsonProperty("compilation")]
        Compilation,
        [JsonProperty("project files")]
        ProjectFiles,
        [JsonProperty("archive")]
        Archive,
        [JsonProperty("showcase")]
        Showcase,
        [JsonProperty("demo")]
        Demo,
        [JsonProperty("sample pack")]
        SamplePack,
        [JsonProperty("other")]
        Other
    }
}