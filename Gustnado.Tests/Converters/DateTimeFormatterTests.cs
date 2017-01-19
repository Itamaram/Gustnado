using System;
using Gustnado.Converters;
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
}