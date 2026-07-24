namespace Components.ComponentsSamples.TextFields.KeyboardDictation;

public partial class DictationModalSamplePage
{
    public DictationModalSamplePage()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopModalAsync();
    }
}
