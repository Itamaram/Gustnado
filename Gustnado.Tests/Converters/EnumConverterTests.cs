using Gustnado.Enums;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Gustnado.Tests.Converters
{
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
            public EncodingState State { get; set; }
        }
    }
}