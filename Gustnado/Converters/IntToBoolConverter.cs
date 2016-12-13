using System;
using Newtonsoft.Json;

namespace Gustnado.Converters
{
    public class IntToBoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((bool) value ? 1 : 0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (int) reader.Value != 0;
        }

        public override bool CanConvert(Type type) => type == typeof(int);
    }
}