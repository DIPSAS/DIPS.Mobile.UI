namespace DIPS.Mobile.UI.Components.Loading;

[ContentProperty(nameof(MainContent))]
public partial class RefreshView : Microsoft.Maui.Controls.RefreshView
{
    //TODO .NET8: Remove
    public RefreshView()
    {
        this.SetAppThemeColor(RefreshColorProperty, ActivityIndicator.LoadingIndicatorColorName);
#if __IOS__ //Added because of this not being backported to .NET 7 as of 29 june 2023: https://github.com/dotnet/maui/issues/7315
        Content = new ContentView();
        Content.SetBinding(ContentProperty, new Binding(){Path = nameof(MainContent), Source = this});
#elif __ANDROID__
        this.SetBinding(ContentProperty, new Binding(){Path = nameof(MainContent), Source = this});
#endif
        
    }
}