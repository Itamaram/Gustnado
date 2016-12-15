using System;
using Gustnado.Converters;
using Gustnado.Enums;
using Gustnado.Objects;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Gustnado.Tests.Converters
{
    public class DateTimeFormatterTests
    {
        [TestCase("2011/04/06 15:37:43 +0000", 2011, 4, 6, 15, 37, 43)]
        public void ParsingTests(string raw, int year, int month, int day, int hour, int minute, int second)
        {
            Assert.AreEqual(new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc), DateTimeFormatter.Parse(raw));
        }

        [TestCase(2011, 4, 6, 15, 37, 43, "2011/04/06 15:37:43 +0000")]
        public void FormattingTests(int year, int month, int day, int hour, int minute, int second, string formatted)
        {
            Assert.AreEqual(formatted, DateTimeFormatter.Format(new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc)));
        }
    }

    public class EnumConverterTests
    {
        [TestCase("processing", EncodingState.Processing)]
        [TestCase("failed", EncodingState.Failed)]
        [TestCase("finished", EncodingState.Finished)]
        public void FromJson(string json, EncodingState expected)
        {
            Assert.AreEqual(expected, JsonConvert.DeserializeObject<Foo>($"{{ State: \"{json}\"}}").State);
        }

        [TestCase(EncodingState.Processing, "processing")]
        [TestCase(EncodingState.Failed, "failed")]
        [TestCase(EncodingState.Finished, "finished")]
        public void ToJson(EncodingState state, string expected)
        {
            Assert.AreEqual($"{{\"State\":\"{expected}\"}}", JsonConvert.SerializeObject(new Foo { State = state }));
        }

        private class Foo
        {
            [JsonConverter(typeof(EnumConverter<EncodingState>))]
            public EncodingState State { get; set; }
        }
    }
}