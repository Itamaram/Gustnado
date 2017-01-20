using System;
using System.Collections;
using System.Text;
using Newtonsoft.Json;

namespace Gustnado.Converters
{
    public class CommaSeparatedList : JsonConverter
    {
        public override bool CanRead { get; } = false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            using (var sb = new StringBuildingWriter())
            {
                var e = (value as IEnumerable).GetEnumerator();

                if (!e.MoveNext())
                    return;

                serializer.Serialize(sb, e.Current);

                while (e.MoveNext())
                {
                    sb.Append(",");
                    serializer.Serialize(sb, e.Current);
                }

                writer.WriteValue(sb.Value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType) => typeof(IEnumerable).IsAssignableFrom(objectType);

        private class StringBuildingWriter : JsonWriter
        {
            private readonly StringBuilder builder = new StringBuilder();

            public override void WriteValue(string value)
            {
                builder.Append(value);
                base.WriteValue(value);
            }

            public override void WriteValue(int value)
            {
                builder.Append(value);
                base.WriteValue(value);
            }

            public string Value => builder.ToString();

            public void Append(string s) => builder.Append(s);

            public override void Flush() { }
        }
    }
}