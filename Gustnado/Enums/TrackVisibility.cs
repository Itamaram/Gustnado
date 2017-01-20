using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Enums
{
    [JsonConverter(typeof(EnumConverter<TrackVisibility>))]
    public enum TrackVisibility
    {
        [JsonProperty("all")]
        All,
        [JsonProperty("public")]
        Public,
        [JsonProperty("private")]
        Private
    }
}