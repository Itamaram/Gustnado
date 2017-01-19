using Gustnado.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;

namespace Gustnado.Tests.Converters
{
    public class EnumberConverterTests
    {
        public class Container
        {
            public Decorated? D { get; set; }
            public Undecorated? U { get; set; }
        }

        [JsonConverter(typeof(EnumConverter<Decorated>))]
        public enum Decorated
        {
            [JsonValue("default")]
            Default,
            [JsonValue("foo")]
            Foo
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Undecorated
        {
            [JsonProperty("default")]
            Default,
            [JsonProperty("bar")]
            Bar
        }

        [Test]
        public void SerialisationTestDecorated()
        {
            var json = JsonConvert.SerializeObject(new Container { D = Decorated.Foo }, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            Assert.AreEqual(@"{""D"":""foo""}", json);
        }

        [Test]
        public void DeserialisationTestDecorated()
        {
            var container = JsonConvert.DeserializeObject<Container>(@"{""D"":""foo""}");
            Assert.AreEqual(Decorated.Foo, container.D);
        }

        [Test]
        public void SerialisationTestUndecorated()
        {
            var json = JsonConvert.SerializeObject(new Container { U = Undecorated.Bar }, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            Assert.AreEqual(@"{""U"":""bar""}", json);
        }

        [Test]
        public void DeserialisationTestUndecorated()
        {
            var container = JsonConvert.DeserializeObject<Container>(@"{""U"":""bar""}");
            Assert.AreEqual(Undecorated.Bar, container.U);
        }
    }
}