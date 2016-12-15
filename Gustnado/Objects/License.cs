using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Objects
{
    [JsonConverter(typeof(EnumConverter<License>))]
    public enum License
    {
        [JsonValue("no-rights-reserved")]
        NoRightsReserved,
        [JsonValue("all-rights-reserved")]
        AllRightsReserved,
        [JsonValue("cc-by")]
        Attribution,
        [JsonValue("cc-by-nc")]
        AttributionNonCommercial,
        [JsonValue("cc-by-nd")]
        AttributionNoDerivatives,
        [JsonValue("cc-by-sa")]
        AttributionShareAlike,
        [JsonValue("cc-by-nc-nd")]
        AttributionNonCommercialNoDerivatives,
        [JsonValue("cc-by-nc-sa")]
        AttributionNonCommercialShareAlike
    }
}