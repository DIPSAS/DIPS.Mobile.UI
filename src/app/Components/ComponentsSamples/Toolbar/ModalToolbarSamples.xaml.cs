namespace Components.ComponentsSamples.Toolbar;

public partial class ModalToolbarSamples
{
    public ModalToolbarSamples()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
