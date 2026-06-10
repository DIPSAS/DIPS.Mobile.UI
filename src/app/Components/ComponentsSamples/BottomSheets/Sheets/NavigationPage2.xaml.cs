using DIPS.Mobile.UI.Components.BottomSheets;

namespace Components.ComponentsSamples.BottomSheets.Sheets;

public partial class NavigationPage2
{
    private readonly BottomSheet m_sheet;

    public NavigationPage2(BottomSheet sheet)
    {
        m_sheet = sheet;
        InitializeComponent();
    }

    private async void OnGoDeeperTapped(object? sender, EventArgs e)
    {
        await m_sheet.PushAsync(new NavigationPage3(m_sheet));
    }

    private async void OnPopToRootClicked(object? sender, EventArgs e)
    {
        await m_sheet.PopToRootAsync();
    }
}

