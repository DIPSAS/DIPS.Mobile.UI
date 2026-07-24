namespace DIPS.Mobile.UI.Components.TextFields.Dictation;

/// <summary>
/// The text of a field after dictated text was inserted, together with the caret index that follows the inserted
/// text.
/// </summary>
internal readonly record struct DictationInsertion(string Text, int CaretIndex);

internal static class DictationTextInserter
{
    /// <summary>
    /// Inserts <paramref name="textToAdd"/> at the caret and returns the resulting text with the caret index that
    /// follows the inserted text.
    /// </summary>
    /// <remarks>
    /// Spacing follows what typing would do:
    /// <list type="bullet">
    /// <item>With no selection, a single space is added before the addition when the character before the caret is
    /// not already whitespace and the addition starts with content.</item>
    /// <item>With a selection, the addition replaces the selected text exactly, with no space added.</item>
    /// </list>
    /// </remarks>
    /// <param name="currentText">The field's current text. Null is treated as empty.</param>
    /// <param name="caretIndex">The caret index into the current text. Clamped to a valid range.</param>
    /// <param name="selectionLength">The selected character count starting at the caret. Clamped so the selection never runs past the end of the text.</param>
    /// <param name="textToAdd">The dictated text to insert. Null is treated as empty.</param>
    /// <returns>The new text and the caret index directly after the inserted text.</returns>
    public static DictationInsertion Insert(string? currentText, int caretIndex, int selectionLength, string? textToAdd)
    {
        var existingText = currentText ?? string.Empty;
        
        var addition = textToAdd ?? string.Empty;

        var insertionStart = Math.Clamp(caretIndex, 0, existingText.Length);
        var selectedCharacterCount = Math.Clamp(selectionLength, 0, existingText.Length - insertionStart);
        var selectionEnd = insertionStart + selectedCharacterCount;

        var textBeforeInsertion = existingText[..insertionStart];
        var textAfterSelection = existingText[selectionEnd..];

        var isReplacingSelection = selectedCharacterCount > 0;
        var precededByNonWhitespace = insertionStart > 0 && !char.IsWhiteSpace(existingText[insertionStart - 1]);
        var additionStartsWithContent = addition.Length > 0 && !char.IsWhiteSpace(addition[0]);
        var needsSeparatingSpace = !isReplacingSelection && precededByNonWhitespace && additionStartsWithContent;

        var insertedText = needsSeparatingSpace ? " " + addition : addition;

        var newText = textBeforeInsertion + insertedText + textAfterSelection;
        var caretIndexAfterInsertion = insertionStart + insertedText.Length;

        var insertion = new DictationInsertion(newText, caretIndexAfterInsertion);
        return insertion;
    }
}
