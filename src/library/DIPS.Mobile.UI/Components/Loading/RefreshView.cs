namespace DIPS.Mobile.UI.Components.Loading;

public class RefreshView : Microsoft.Maui.Controls.RefreshView
{
    public RefreshView()
    {
        this.SetAppThemeColor(RefreshColorProperty, ActivityIndicator.LoadingIndicatorColorName);
    }
}