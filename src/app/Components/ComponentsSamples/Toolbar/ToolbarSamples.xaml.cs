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

    private void OnScrollViewHandlerChanged(object? sender, EventArgs e)
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

    private void OnHidesOnScrollToggled(object? sender, ToggledEventArgs e)
    {
        bottomToolbar.HidesOnScrollFor = e.Value ? scrollView : null;
    }
}
