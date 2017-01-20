using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Gustnado.Serialisation
{
    public class CustomSerializer : ISerializer, IDeserializer
    {
        private readonly JsonSerializerSettings settings;

        public CustomSerializer() : this(new JsonSerializerSettings()) { }

        public CustomSerializer(JsonSerializerSettings settings)
        {
            this.settings = settings;
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content, settings);
        }

        public static CustomSerializer Default = new CustomSerializer(SerialiserSettingsPresets.IgnoreNulls);

        string IDeserializer.RootElement { get; set; }
        string IDeserializer.Namespace { get; set; }
        string IDeserializer.DateFormat { get; set; }
        string ISerializer.RootElement { get; set; }
        string ISerializer.Namespace { get; set; }
        string ISerializer.DateFormat { get; set; }
        public string ContentType { get; set; }
    }
}