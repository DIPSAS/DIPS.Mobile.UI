using System;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.UnitTests.TestHelpers;
using FluentAssertions;
using Microsoft.Maui.Controls.Xaml;
using Xunit;

namespace DIPS.Mobile.UI.UnitTests.Converters.ValueConverters
{
    public class AdditionConverterTests
    {
        private readonly AdditionConverter m_additionConverter = new AdditionConverter();

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(50, 100, 150)]
        [InlineData(50000, 100000, 150000)]
        [InlineData(-5, -5, -10)]
        [InlineData(0, -5, -5)]
        public void Convert_AddTermWithAddend_ShouldBeTheCorrectSum(double term, double addend, double expectedSum)
        {
            m_additionConverter.Addend = addend;
            var actualSum = m_additionConverter.Convert<double>(term);
            actualSum.Should().Be(expectedSum);
        }

        [Fact]
        public void ConvertBack_ShouldThrowNotImplementedException()
        {
            var act = () => m_additionConverter.ConvertBack<double>(0);
            act.Should().Throw<NotImplementedException>();
        }

        [Fact]
        public void Convert_ValueIsNull_XamlParseExceptionThrown()
        {
            var act = () => m_additionConverter.Convert(null!, null!, null!, null!);

            act.Should().Throw<XamlParseException>();
        }

        [Fact]
        public void Convert_AddendIsNull_XamlParseExceptionThrown()
        {
            Action act = () => m_additionConverter.Convert(1.0, null!, null!, null!);

            act.Should().Throw<XamlParseException>();
        }
    }

}
