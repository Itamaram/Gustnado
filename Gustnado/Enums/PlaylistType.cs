using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Enums
{
    [JsonConverter(typeof(EnumConverter<PlaylistType>))]
    public enum PlaylistType
    {
        [JsonValue("ep single")]
        EpSingle,
        [JsonValue("album")]
        Album,
        [JsonValue("compilation")]
        Compilation,
        [JsonValue("project files")]
        ProjectFiles,
        [JsonValue("archive")]
        Archive,
        [JsonValue("showcase")]
        Showcase,
        [JsonValue("demo")]
        Demo,
        [JsonValue("sample pack")]
        SamplePack,
        [JsonValue("other")]
        Other
    }
}