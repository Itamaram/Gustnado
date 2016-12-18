using System.Linq;
using Gustnado.Requests.Tracks;
using NUnit.Framework;

namespace Gustnado.Tests
{
    public class QueryStringFormatterTests
    {
        [Test]
        public void DefaultFormatterStringTest()
        {
            var result = QueryStringFormatter.FromObject(new StringTest { String = "x" }).Single();
            Assert.AreEqual("string", result.Key);
            Assert.AreEqual("x", result.Value);

        }

        private class StringTest
        {
            [QueryParameter("string")]
            public string String { get; set; }
        }

        [Test]
        public void DefaultFormatterIntTest()
        {
            var result = QueryStringFormatter.FromObject(new IntTest { Int = 7 }).Single();
            Assert.AreEqual("int", result.Key);
            Assert.AreEqual("7", result.Value);

        }

        private class IntTest
        {
            [QueryParameter("int")]
            public int Int { get; set; }
        }

        [Test]
        public void CustomFormatterTest()
        {
            var result = QueryStringFormatter.FromObject(new CustomTest { Custom = "K" }).Single();
            Assert.AreEqual("custom", result.Key);
            Assert.AreEqual("Special-K", result.Value);

        }

        private class CustomTest
        {
            [QueryParameter("custom", typeof(CustomFormatter))]
            public string Custom { get; set; }
        }

        private class CustomFormatter : ParameterFormatter
        {
            public string Format(object o) => "Special-" + o;
        }

        [Test]
        public void FormatterUndecoratedTest()
        {
            CollectionAssert.IsEmpty(QueryStringFormatter.FromObject(new IgnoreTest { String = "x" }));

        }

        private class IgnoreTest
        {
            public string String { get; set; }
        }
    }
}