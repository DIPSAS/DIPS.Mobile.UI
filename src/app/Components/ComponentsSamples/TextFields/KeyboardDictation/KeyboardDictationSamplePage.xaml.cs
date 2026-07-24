using DIPS.Mobile.UI.Components.BottomSheets;

namespace Components.ComponentsSamples.TextFields.KeyboardDictation;

public partial class KeyboardDictationSamplePage
{
    private bool m_softInputResizes = true;

    public KeyboardDictationSamplePage()
    {
        InitializeComponent();
    }

    private async void OnOpenInModalClicked(object? sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushModalAsync(new NavigationPage(new DictationModalSamplePage()));
    }

    private async void OnOpenInBottomSheetClicked(object? sender, EventArgs e)
    {
        await BottomSheetService.Open(new DictationBottomSheet());
    }

    private void OnToggleSoftInputModeClicked(object? sender, EventArgs e)
    {
        m_softInputResizes = !m_softInputResizes;

        ApplySoftInputMode(m_softInputResizes);

        SoftInputModeButton.Text = m_softInputResizes
            ? "Keyboard: resize page (tap for pan)"
            : "Keyboard: pan page (tap for resize)";
    }

    private static void ApplySoftInputMode(bool resize)
    {
#if ANDROID
        var window = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity?.Window;
        if (window is null)
            return;

        window.SetSoftInputMode(resize ? Android.Views.SoftInput.AdjustResize : Android.Views.SoftInput.AdjustPan);
#endif
    }
}
