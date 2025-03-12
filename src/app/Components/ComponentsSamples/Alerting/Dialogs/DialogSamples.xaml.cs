namespace Components.ComponentsSamples.Alerting.Dialogs;

public partial class DialogSamples
{
    public DialogSamples()
    {
        InitializeComponent();
    }

    private void ListItem_OnTappedInputDialog(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new InputDialogSamples());
    }

    private void ListItem_OnTappedMessageDialog(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new MessageDialogSamples());
    }
}