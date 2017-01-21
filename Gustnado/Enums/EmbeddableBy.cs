using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Enums
{
    [JsonConverter(typeof(EnumConverter<EmbeddableBy>))]
    public enum EmbeddableBy
    {
        [JsonProperty("all")]
        All,
        [JsonProperty("me")]
        Me,
        [JsonProperty("none")]
        None
    }
}