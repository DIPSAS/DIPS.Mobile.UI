using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.TextFields.MultiLineTextArea;

public partial class MultiLineInputFieldSamples
{
    public MultiLineInputFieldSamples()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        MultiLineTextArea.Text =
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
    }

    private void TruncateSwitch_OnToggled(object? sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            MultiLineTextArea.MaxLines = (int)Stepper.Value;
        }
        else
        {
            MultiLineTextArea.MaxLines = int.MaxValue;
        }
    }

    private void Stepper_OnValueChanged(object? sender, ValueChangedEventArgs e)
    {
        if (TruncateSwitch.IsToggled)
        {
            MultiLineTextArea.MaxLines = (int)e.NewValue;
        }
    }
}