using System;
using System.Collections.Generic;
using System.Linq;
using Gustnado.Objects;
using Newtonsoft.Json;

namespace Gustnado.Converters
{
    public class TrackCompactor : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = value as List<Track>;
            if (list == null)
                return;

            writer.WriteStartArray();

            foreach (var track in list.Where(t => t.Id != null))
                serializer.Serialize(writer, new Track {Id = track.Id});

            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<List<Track>>(reader);
        }

        public override bool CanConvert(Type objectType) => objectType == typeof (List<Track>);
    }
}