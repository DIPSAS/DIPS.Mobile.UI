using DIPS.Mobile.UI.Components.TextFields.Dictation;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;

namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class EditorHandler : IDictationConsumerDelegate
{
    public void UpdateTextWithDictationResult(string textToAdd)
    {
        if (VirtualView is not Editor editor)
            return;

        if (string.IsNullOrEmpty(textToAdd))
            return;

        var insertion = DictationTextInserter.Insert(
            editor.Text,
            caretIndex: editor.CursorPosition,
            selectionLength: editor.SelectionLength,
            textToAdd);

        editor.Text = insertion.Text;
        editor.CursorPosition = insertion.CaretIndex;
        editor.SelectionLength = 0;
    }
}
