using System.Collections.Generic;
using System.Linq;
using Gustnado.Converters;
using Gustnado.Objects;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Gustnado.Tests.Converters
{
    public class TrackCompactorTests
    {
        public class Foo
        {
            [JsonConverter(typeof(TrackCompactor))]
            public List<Track> Bar { get; set; }
        }

        [Test]
        public void SerialiseTest()
        {
            var foo = new Foo
            {
                Bar = new List<Track>
                {
                    new Track(),
                    new Track {Id = 1},
                    new Track {Title = "My Track", Id = 2},
                }
            };

            Assert.AreEqual(@"{""Bar"":[{""id"":1},{""id"":2}]}", JsonConvert.SerializeObject(foo, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [Test]
        public void DeserialiseTest()
        {
            var track = @"{""Bar"": [{""id"": 303154602,""user_id"": 97999179}]}"
                .Map(JsonConvert.DeserializeObject<Foo>)
                .Map(foo => foo.Bar.Single());

            Assert.AreEqual(303154602, track.Id);
            Assert.AreEqual(97999179, track.UserId);
        }
    }
}