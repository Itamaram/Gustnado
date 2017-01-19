using Newtonsoft.Json;

namespace Gustnado.Serialisation
{
    public static class SerialiserSettingsPresets
    {
        public static JsonSerializerSettings IgnoreNulls => new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
    }
}