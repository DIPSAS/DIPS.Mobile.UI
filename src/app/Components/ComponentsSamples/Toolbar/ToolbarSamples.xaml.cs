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

    private void OnScrollViewLoaded(object? sender, EventArgs e)
    {
        bottomToolbar.HidesOnScrollFor = scrollView;
    }

    private void OnToolbarVisibilityToggled(object? sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            bottomToolbar.Show();
        }
        else
        {
            bottomToolbar.Hide();
        }
    }
}
