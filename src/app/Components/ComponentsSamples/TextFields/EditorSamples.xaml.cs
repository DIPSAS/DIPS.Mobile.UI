namespace Components.ComponentsSamples.TextFields;

public partial class EditorSamples
{
    public EditorSamples()
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