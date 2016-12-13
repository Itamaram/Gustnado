using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Gustnado.Converters
{
    public class EnumConverter<T> : JsonConverter
    {
        private readonly IReadOnlyDictionary<string, T> fromJson;
        private readonly IReadOnlyDictionary<T, string> toJson;

        public EnumConverter()
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException();
            
            fromJson = Enum.GetNames(typeof(T)).Select(name => typeof(T).GetMember(name).Single()).ToDictionary(
                m => m.GetCustomAttributes().OfType<JsonValueAttribute>().Single().Value,
                m => (T) Enum.Parse(typeof (T), m.Name));

            toJson = fromJson.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(toJson[(T)value]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return fromJson[reader.Value.ToString()];
        }

        public override bool CanConvert(Type type) => type == typeof(T);
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class JsonValueAttribute : Attribute
    {
        public string Value { get; }

        public JsonValueAttribute(string value)
        {
            Value = value;
        }
    }
}