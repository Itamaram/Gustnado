using System;
using Bearded.Monads;
using Newtonsoft.Json;

namespace Gustnado.Serialisation
{
    public class AddToRequestBodyAsFile : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.MaybeCast<RequestWriter>()
                .WhenSome(w => w.WriteFile((string)value))
                //Note that this leaves us with a null regardless of serialisation settings, as the prop name had already been written at this point
                .WhenNone(writer.WriteNull);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => null;
        public override bool CanConvert(Type type) => type == typeof(string);
    }
}