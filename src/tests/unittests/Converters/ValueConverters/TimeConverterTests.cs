﻿using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.UnitTests.Converters.ValueConverters
{
    [Collection("Sequential")] //This test class is using an static shared property that is used in other tests
    public class TimeConverterTests
    {
        private readonly TimeConverter m_timeConverter = new TimeConverter(){IgnoreLocalTime = true};

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData("test")]
        public void Convert_InvalidInput_XamlParseExceptionThrown(object invalidInput)
        {
            Action act = () => m_timeConverter.Convert<string>(invalidInput);

            act.Should().Throw<XamlParseException>();
        }

        [Fact]
        public void Convert_NullInput_ShouldReturnEmptyString()
        {
            var actual = m_timeConverter.Convert<string>(null!);

            actual.Should().Be(string.Empty);
        }

        public static IEnumerable<object[]> TestDataForDefaultFormat =>
            new List<object[]>()
            {
                new object[] {"nb", new TimeSpan(09, 12, 00), "09:12"},
                new object[] {"nb", new TimeSpan(21, 12, 00), "21:12"},
                new object[] {"en-gb", new TimeSpan(09, 12, 00), "09:12"},
                new object[] {"en-gb", new TimeSpan(21, 12, 00), "21:12"},
                new object[] {"en-us", new TimeSpan(09, 12, 00), "09:12 AM"},
                new object[] {"en-us", new TimeSpan(21, 12, 00), "09:12 PM"},
            };

        [Theory]
        [MemberData(nameof(TestDataForDefaultFormat))]
        public void Convert_WithDefaultFormat_WithCulture_CorrectFormat(string cultureName, TimeSpan time,
            string expected)
        {
            m_timeConverter.Format = TimeConverter.TimeConverterFormat.Default;

            var actual = m_timeConverter.Convert<string>(time, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }

        [Fact]
        public void Convert_WithShortFormat_WithDateTime_CorrectFormat()
        {
            var expected = "13:00";
            var date = new DateTime(1991, 12, 12, 13, 00, 00);
            Clock.OverrideClock(date);
            m_timeConverter.Format = TimeConverter.TimeConverterFormat.Default;

            var actual = m_timeConverter.Convert<string>(date, new CultureInfo("en-gb"));

            actual.Should().Be(expected);
        }
    }
}