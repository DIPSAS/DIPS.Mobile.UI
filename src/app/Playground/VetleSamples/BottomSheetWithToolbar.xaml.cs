
namespace Playground.VetleSamples;

public partial class BottomSheetWithToolbar
{
    public BottomSheetWithToolbar()
    {
        InitializeComponent();

    }

    private void Chip_OnTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ContentPage() {Title = "Test", Content = new Label(){Text = "Hei"} });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
    }
}