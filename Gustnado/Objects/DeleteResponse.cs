using Newtonsoft.Json;

namespace Gustnado.Objects
{
    public class DeleteResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}