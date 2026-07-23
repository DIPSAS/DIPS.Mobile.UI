using DIPS.Mobile.UI.Components.TextFields.Dictation;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;

namespace DIPS.Mobile.UI.Components.TextFields.Entry;

public partial class EntryHandler : IDictationConsumerDelegate
{
    public void UpdateTextWithDictationResult(string textToAdd)
    {
        if (VirtualView is not Entry entry)
            return;

        entry.Text = DictationTextAppender.Append(entry.Text, textToAdd);
    }
}
