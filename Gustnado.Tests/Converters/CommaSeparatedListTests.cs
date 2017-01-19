using System.Collections.Generic;
using System.Linq;
using Gustnado.Converters;
using Gustnado.Enums;
using Gustnado.Requests.Tracks;
using Gustnado.Serialisation;
using Newtonsoft.Json;
using NUnit.Framework;
using static Gustnado.Enums.PlaylistType;

namespace Gustnado.Tests.Converters
{
    public class CommaSeparatedListTests
    {
        public class Foo
        {
            [JsonConverter(typeof(CommaSeparatedList))]
            public List<string> Strings { get; set; }

            [JsonConverter(typeof(CommaSeparatedList))]
            public List<int> Ints { get; set; }

            [JsonConverter(typeof(CommaSeparatedList))]
            public List<PlaylistType> Types { get; set; }
        }

        [TestCase(@"{""Strings"":null}")]
        [TestCase(@"{""Strings"":""A""}", "A")]
        [TestCase(@"{""Strings"":""A,B""}", "A", "B")]
        [TestCase(@"{""Strings"":""A,B,C""}", "A", "B", "C")]
        public void StringTest(string expected, params string[] values)
        {
            var result = JsonConvert.SerializeObject(new Foo
            {
                Strings = values.ToList()
            }, SerialiserSettingsPresets.IgnoreNulls);

            Assert.AreEqual(expected, result);
        }

        [TestCase(@"{""Ints"":null}")]
        [TestCase(@"{""Ints"":""1""}", 1)]
        [TestCase(@"{""Ints"":""1,2""}", 1, 2)]
        [TestCase(@"{""Ints"":""1,2,4""}", 1, 2, 4)]
        public void IntsTest(string expected, params int[] values)
        {
            var result = JsonConvert.SerializeObject(new Foo
            {
                Ints = values.ToList()
            }, SerialiserSettingsPresets.IgnoreNulls);

            Assert.AreEqual(expected, result);
        }

        [TestCase(@"{""Types"":null}")]
        [TestCase(@"{""Types"":""album""}", Album)]
        [TestCase(@"{""Types"":""album,demo""}", Album, Demo)]
        [TestCase(@"{""Types"":""album,demo,other""}", Album, Demo, Other)]
        public void EnumsTest(string expected, params PlaylistType[] values)
        {
            var result = JsonConvert.SerializeObject(new Foo
            {
                Types = values.ToList()
            }, SerialiserSettingsPresets.IgnoreNulls);

            Assert.AreEqual(expected, result);
        }
    }
}