using System.Collections.Generic;
using Gustnado.Objects;
using Gustnado.Serialisation;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Gustnado.Tests.Objects
{
    public class SerialisationTests
    {
        [TestCaseSource(nameof(EmptyObjects))]
        public void AllFieldsAreNullable(object o)
        {
            Assert.AreEqual("{}", JsonConvert.SerializeObject(o, SerialiserSettingsPresets.IgnoreNulls));
        }

        private static IEnumerable<TestCaseData> EmptyObjects { get; } = new[]
        {
            new TestCaseData(new App()),
            new TestCaseData(new Comment()),
            new TestCaseData(new Me()),
            new TestCaseData(new OAuthRequest()),
            new TestCaseData(new Playlist()),
            new TestCaseData(new Track()),
            new TestCaseData(new User()),
            new TestCaseData(new WebProfile()),
        };
    }
}