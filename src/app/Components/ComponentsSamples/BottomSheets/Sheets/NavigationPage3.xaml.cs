using DIPS.Mobile.UI.Components.BottomSheets;

namespace Components.ComponentsSamples.BottomSheets.Sheets;

public partial class NavigationPage3
{
    private readonly BottomSheet m_sheet;

    public NavigationPage3(BottomSheet sheet)
    {
        m_sheet = sheet;
        InitializeComponent();
    }

    private async void OnPopToRootClicked(object? sender, EventArgs e)
    {
        await m_sheet.PopToRootAsync();
    }
}

