using DIPS.Mobile.UI.Components.TextFields.Dictation;

namespace DIPS.Mobile.UI.UnitTests.Components.TextFields;

public class DictationTextInserterTests
{
    [Theory]
    [InlineData("", 0, "Well", "Well")]
    [InlineData(null, 0, "Hi", "Hi")]
    [InlineData("Hello", 5, "world", "Hello world")]
    [InlineData("Hello ", 6, "world", "Hello world")]
    [InlineData("Hello", 5, " ", "Hello ")]
    public void Insert_AtEnd_MergesTextWithSingleSeparatingSpace(
        string? currentText, int caretIndex, string textToAdd, string expectedText)
    {
        var result = DictationTextInserter.Insert(currentText, caretIndex, selectionLength: 0, textToAdd);

        result.Text.Should().Be(expectedText);
    }

    [Fact]
    public void Insert_InMiddle_PutsTextAtCaretWithLeadingSpace()
    {
        var result = DictationTextInserter.Insert("Hello world", caretIndex: 5, selectionLength: 0, "there");

        result.Text.Should().Be("Hello there world");
    }

    [Fact]
    public void Insert_AtStart_AddsNoLeadingSpace()
    {
        var result = DictationTextInserter.Insert("world", caretIndex: 0, selectionLength: 0, "Hello");

        result.Text.Should().Be("Helloworld");
    }

    [Fact]
    public void Insert_ReturnsCaretDirectlyAfterInsertedText()
    {
        var result = DictationTextInserter.Insert("Hello world", caretIndex: 5, selectionLength: 0, "there");

        // "Hello" + " there" = index 11
        result.CaretIndex.Should().Be(11);
    }

    [Fact]
    public void Insert_WithSelection_ReplacesSelectedRange()
    {
        var result = DictationTextInserter.Insert("Hello cruel world", caretIndex: 6, selectionLength: 5, "kind");

        result.Text.Should().Be("Hello kind world");
    }

    [Fact]
    public void Insert_WithSelection_PutsCaretAfterReplacement()
    {
        var result = DictationTextInserter.Insert("Hello cruel world", caretIndex: 6, selectionLength: 5, "kind");

        result.CaretIndex.Should().Be(10);
    }

    [Theory]
    [InlineData("Hello", -3)]
    [InlineData("Hello", 999)]
    public void Insert_WithCaretOutOfRange_ClampsToValidPosition(string currentText, int caretIndex)
    {
        var result = DictationTextInserter.Insert(currentText, caretIndex, selectionLength: 0, "x");

        result.CaretIndex.Should().BeInRange(0, result.Text.Length);
    }

    [Fact]
    public void Insert_WithSelectionRunningPastEnd_ClampsSelectionToText()
    {
        var result = DictationTextInserter.Insert("Hello", caretIndex: 3, selectionLength: 999, "p");

        result.Text.Should().Be("Help");
    }

    [Theory]
    [InlineData("Hello", 5, "")]
    [InlineData("Hello", 5, null)]
    public void Insert_WithEmptyAddition_LeavesTextUnchanged(string currentText, int caretIndex, string? textToAdd)
    {
        var result = DictationTextInserter.Insert(currentText, caretIndex, selectionLength: 0, textToAdd);

        result.Text.Should().Be(currentText);
    }
}
