using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.UnitTests.Converters.ValueConverters
{
    public class InvertedBoolConverterTests
    {
        private readonly InvertedBoolConverter m_invertedBoolConverter;

        public InvertedBoolConverterTests()
        {
            m_invertedBoolConverter = new InvertedBoolConverter();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Convert_BooleanValue_InvertedResult(bool value)
        {
            var result = m_invertedBoolConverter.Convert(value, null!, null!, null!);
            result.Should().Be(!value);
        }


        [Theory]
        [InlineData("Not a bool")]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(null)]
        public void Convert_ValueIsNull_XamlParseExceptionThrown(object value)
        {
            Action act = () => m_invertedBoolConverter.Convert(value, null!, null!, null!);

            act.Should().Throw<XamlParseException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ConvertBack_BooleanValue_IsNotInverted(bool value)
        {
            var result = m_invertedBoolConverter.ConvertBack(value, null!, null!, null!);
            result.Should().Be(value);
        }
    }
}
