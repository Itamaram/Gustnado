using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Gustnado.Converters
{
    public class DateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(DateTimeFormatter.Format((DateTime) value));
        }

        public override bool CanConvert(Type type) => type == typeof (DateTime);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTimeFormatter.Parse(reader.Value.ToString());
        }
    }

    public static class DateTimeFormatter
    {
        public static string Format(DateTime d) => d.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss") + " +0000";

        public static DateTime Parse(string s) => DateTime.ParseExact(s, "yyyy/MM/dd HH:mm:ss K", new CultureInfo("en-US")).ToUniversalTime();
    }
}