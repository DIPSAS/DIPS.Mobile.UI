namespace Components.ComponentsSamples.Toolbar;

public partial class ToolbarSamples
{
    public ToolbarSamples()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
