using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Enums
{
    [JsonConverter(typeof(EnumConverter<License>))]
    public enum License
    {
        [JsonProperty("no-rights-reserved")]
        NoRightsReserved,
        [JsonProperty("all-rights-reserved")]
        AllRightsReserved,
        [JsonProperty("cc-by")]
        Attribution,
        [JsonProperty("cc-by-nc")]
        AttributionNonCommercial,
        [JsonProperty("cc-by-nd")]
        AttributionNoDerivatives,
        [JsonProperty("cc-by-sa")]
        AttributionShareAlike,
        [JsonProperty("cc-by-nc-nd")]
        AttributionNonCommercialNoDerivatives,
        [JsonProperty("cc-by-nc-sa")]
        AttributionNonCommercialShareAlike
    }
}