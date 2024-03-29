﻿using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.UnitTests.Converters.ValueConverters
{
    public class IsEmptyConverterTests
    {
        private readonly IsEmptyConverter m_isEmptyConverter;

        public IsEmptyConverterTests()
        {
            m_isEmptyConverter = new IsEmptyConverter();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("", true)]
        [InlineData(null, true)]
        public void Convert_EmptyInputValues_ReturnsCorrect(object value, bool inverted = false)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.Convert(value, null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Convert_EmptyListValue_ReturnsCorrect(bool inverted)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.Convert(new List<string>(), null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
            
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Convert_NonEmptyInputString_ReturnsCorrect(bool inverted)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.Convert("Non empty string" , null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeFalse();
            }
            else
            {
                result.Should().BeTrue();
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Convert_NonEmptyListValue_ReturnsCorrect(bool inverted)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.Convert(new List<string>(){"Non empty string"}, null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeFalse();
            }
            else
            {
                result.Should().BeTrue();
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1.0)]
        [InlineData(1.0f)]
        [InlineData(1, true)]
        [InlineData(1.0, true)]
        [InlineData(1.0f, true)]
        public void Convert_NonEmptyNumbers_ReturnsCorrect(object number, bool inverted = false)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.Convert(number, null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeFalse();
            }
            else
            {
                result.Should().BeTrue();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData(0, true)]
        [InlineData(0.0, true)]
        [InlineData(0.0f, true)]
        public void Convert_EmptyNumbers_ReturnsCorrect(object number, bool inverted = false)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.Convert(number, null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }


        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConvertBack_NonEmptyListValue_ReturnsCorrect(bool inverted)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.ConvertBack(new List<string>(){"Non empty string"}, null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConvertBack_NonString_ReturnsCorrect(bool inverted)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.ConvertBack("Non empty string" , null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1.0)]
        [InlineData(1.0f)]
        [InlineData(1, true)]
        [InlineData(1.0, true)]
        [InlineData(1.0f, true)]
        public void ConvertBack_NonEmptyNumbers_ReturnsCorrect(object number, bool inverted = false)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.ConvertBack(number, null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData(0, true)]
        [InlineData(0.0, true)]
        [InlineData(0.0f, true)]
        public void ConvertBack_EmptyNumbers_ReturnsCorrect(object number, bool inverted = false)
        {
            m_isEmptyConverter.Inverted = inverted;

            var result = (bool)m_isEmptyConverter.ConvertBack(number, null!, null!, null!);

            if (!inverted)
            {
                result.Should().BeFalse();
            }
            else
            {
                result.Should().BeTrue();
            }
        }
    }
}
