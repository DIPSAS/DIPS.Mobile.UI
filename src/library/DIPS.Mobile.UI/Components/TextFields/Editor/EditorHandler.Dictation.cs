using DIPS.Mobile.UI.Components.TextFields.Dictation;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;

namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class EditorHandler : IDictationConsumerDelegate
{
    public void UpdateTextWithDictationResult(string textToAdd)
    {
        if (VirtualView is not Editor editor)
            return;

        editor.Text = DictationTextAppender.Append(editor.Text, textToAdd);
    }
}
