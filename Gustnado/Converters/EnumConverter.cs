using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bearded.Monads;
using Gustnado.Extensions;
using Newtonsoft.Json;

namespace Gustnado.Converters
{
    public static class EnumJsonValueMap<T>
    {
        private static readonly IReadOnlyDictionary<string, T> fromJson;
        private static readonly IReadOnlyDictionary<T, string> toJson;

        static EnumJsonValueMap()
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException();

            fromJson = Enum.GetNames(typeof(T)).Select(name => typeof(T).GetMember(name).Single()).ToDictionary(
                m => m.GetCustomAttributes().OfType<JsonPropertyAttribute>().Single().PropertyName,
                m => (T)Enum.Parse(typeof(T), m.Name));

            toJson = fromJson.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        }

        public static string ToJson(T t) => toJson[t];

        public static Option<T> FromJson(string s) => fromJson.MaybeGetReadonlyValue(s);
    }

    public class EnumConverter<T> : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(EnumJsonValueMap<T>.ToJson((T) value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var option = reader.Value.AsOption()
                .Map(v => v.ToString())
                .SelectMany(EnumJsonValueMap<T>.FromJson);

            if (option)
                return option.ForceValue();

            return null;
        }

        public override bool CanConvert(Type type) => type == typeof(T);
    }
}