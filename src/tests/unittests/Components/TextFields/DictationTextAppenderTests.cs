using DIPS.Mobile.UI.Components.TextFields.Dictation;

namespace DIPS.Mobile.UI.UnitTests.Components.TextFields;

public class DictationTextAppenderTests
{
    [Theory]
    [InlineData("", "Well", "Well")]
    [InlineData(null, "Hi", "Hi")]
    [InlineData("Hello", "world", "Hello world")]
    [InlineData("Hello ", "world", "Hello world")]
    [InlineData("Hello", "", "Hello")]
    [InlineData("Hello", null, "Hello")]
    [InlineData("Hello", " ", "Hello ")]
    [InlineData("", "", "")]
    [InlineData(null, null, "")]
    public void Append_MergesTextWithSingleSeparatingSpace(string? currentText, string? textToAdd, string expected)
    {
        var result = DictationTextAppender.Append(currentText, textToAdd);

        result.Should().Be(expected);
    }
}
