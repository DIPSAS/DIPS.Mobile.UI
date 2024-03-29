﻿
using DIPS.Mobile.UI.Converters.ValueConverters;
namespace DIPS.Mobile.UI.UnitTests.Converters.ValueConverters
{
    [Collection("Sequential")] //This test class is using an static shared property that is used in other tests
    public class DateAndTimeConverterTests
    {
        private readonly DateTime m_now = new DateTime(1990, 12, 12, 13, 00, 00);
        private readonly DateAndTimeConverter m_dateAndTimeConverter = new DateAndTimeConverter()
        {
            IgnoreLocalTime = true
        };

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData("test")]
        public void Convert_InvalidInput_XamlParseExceptionThrown(object invalidInput)
        {
            Action act = () => m_dateAndTimeConverter.Convert<string>(invalidInput);

            act.Should().Throw<XamlParseException>();
        }

        [Fact]
        public void Convert_NullInput_ShouldReturnEmptyString()
        {
            var actual = m_dateAndTimeConverter.Convert<string>(null!);

            actual.Should().Be(string.Empty);
        }

        public static IEnumerable<object[]> TestDataForShortFormat =>
            new List<object[]>()
            {
                new object[] {"nb", new DateTime(1991, 12, 12, 13, 12, 12), "12. des 1991 kl 13:12"},
            };

        [Theory]
        [MemberData(nameof(TestDataForShortFormat))]
        public void Convert_WithShortFormat_WithCulture_CorrectFormat(string cultureName, DateTime date,
            string expected)
        {
            m_dateAndTimeConverter.Format = DateAndTimeConverter.DateAndTimeConverterFormat.Short;

            var actual = m_dateAndTimeConverter.Convert<string>(date, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> TestDataForTextFormat =>
            new List<object[]>()
            {
                new object[] {"nb", new DateTime(1990, 12, 12, 13, 00, 00), "I dag, kl 13:00"},
                new object[] {"nb", new DateTime(1990, 12, 12, 13, 00, 00).AddDays(-1), "I går, kl 13:00"},
                new object[] {"nb", new DateTime(1990, 12, 12, 13, 00, 00).AddDays(1), "I morgen, kl 13:00"},
                new object[] {"nb", new DateTime(1990, 12, 10, 13, 00, 00), "10. des kl 13:00"}
            };

        [Theory]
        [MemberData(nameof(TestDataForTextFormat))]
        public void Convert_WithTextFormat_WithDate_WithCulture_CorrectFormat(string cultureName, DateTime date,
            string expected)
        {
            Clock.OverrideClock(m_now);

            m_dateAndTimeConverter.Format = DateAndTimeConverter.DateAndTimeConverterFormat.Text;
            DUILocalizedStrings.Culture = new CultureInfo(cultureName); //To force localized strings

            var actual = m_dateAndTimeConverter.Convert<string>(date, DUILocalizedStrings.Culture);
            actual.Should().Be(expected);
        }
    }
}