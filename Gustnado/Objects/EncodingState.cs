using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Objects
{
    [JsonConverter(typeof(EnumConverter<EncodingState>))]
    public enum EncodingState
    {
        [JsonValue("processing")]
        Processing,
        [JsonValue("failed")]
        Failed,
        [JsonValue("finished")]
        Finished
    }
}