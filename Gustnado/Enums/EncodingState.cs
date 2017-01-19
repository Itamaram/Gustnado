using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Enums
{
    [JsonConverter(typeof(EnumConverter<EncodingState>))]
    public enum EncodingState
    {
        [JsonProperty("processing")]
        Processing,
        [JsonProperty("failed")]
        Failed,
        [JsonProperty("finished")]
        Finished
    }
}