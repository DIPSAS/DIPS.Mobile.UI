namespace Components.ComponentsSamples.TextFields.Entry;

public partial class EntrySamples
{
    public EntrySamples()
    {
        InitializeComponent();
    }

    private void Switch_OnToggled(object? sender, ToggledEventArgs e)
    {
        Entry.ShouldSelectAllTextOnFocused = e.Value;
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        Entry.Unfocus();
    }
}