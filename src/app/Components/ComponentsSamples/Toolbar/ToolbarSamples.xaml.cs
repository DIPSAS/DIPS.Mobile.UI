namespace Components.ComponentsSamples.Toolbar;

public partial class ToolbarSamples
{
    public ToolbarSamples()
    {
        InitializeComponent();
    }

    private async void OnOpenModalClicked(object? sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new NavigationPage(new ModalToolbarSamples()));
    }
}
