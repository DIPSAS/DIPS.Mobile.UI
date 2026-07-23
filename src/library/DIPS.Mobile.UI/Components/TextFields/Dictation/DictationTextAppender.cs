namespace DIPS.Mobile.UI.Components.TextFields.Dictation;

internal static class DictationTextAppender
{
    /// <summary>
    /// Returns <paramref name="currentText"/> with <paramref name="textToAdd"/> appended, inserting a single
    /// separating space when the current text does not already end in whitespace and the addition has content.
    /// </summary>
    public static string Append(string? currentText, string? textToAdd)
    {
        var existingText = currentText ?? string.Empty;
        var addition = textToAdd ?? string.Empty;

        if (addition.Length == 0)
            return existingText;

        if (existingText.Length == 0)
            return addition;

        var lastLetterIsWhiteSpace = char.IsWhiteSpace(existingText[^1]);

        var needsSeparatingSpace = !string.IsNullOrWhiteSpace(addition) && !lastLetterIsWhiteSpace;

        return needsSeparatingSpace ? existingText + " " + addition : existingText + addition;
    }
}
