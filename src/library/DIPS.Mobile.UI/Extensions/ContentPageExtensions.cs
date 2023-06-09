namespace DIPS.Mobile.UI.Extensions;

public static class ContentPageExtensions
{
    public static bool ShouldHaveNavigationBar(this ContentPage contentPage) => 
        !string.IsNullOrEmpty(contentPage.Title) || contentPage.ToolbarItems is { Count: > 0 };
            
}