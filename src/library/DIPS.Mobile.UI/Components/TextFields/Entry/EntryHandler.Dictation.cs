using DIPS.Mobile.UI.Components.TextFields.Dictation;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;

namespace DIPS.Mobile.UI.Components.TextFields.Entry;

public partial class EntryHandler : IDictationConsumerDelegate
{
    public void UpdateTextWithDictationResult(string textToAdd)
    {
        if (VirtualView is not Entry entry)
            return;

        if (string.IsNullOrEmpty(textToAdd))
            return;

        var insertion = DictationTextInserter.Insert(
            entry.Text,
            caretIndex: entry.CursorPosition,
            selectionLength: entry.SelectionLength,
            textToAdd);

        entry.Text = insertion.Text;
        entry.CursorPosition = insertion.CaretIndex;
        entry.SelectionLength = 0;
    }
}
