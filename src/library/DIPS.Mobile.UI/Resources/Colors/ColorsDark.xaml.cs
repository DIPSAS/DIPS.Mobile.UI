namespace DIPS.Mobile.UI.Resources.Colors;

/// <summary>
/// A XAML <see cref="ResourceDictionary"/> containing all semantic color tokens for the <b>dark</b> theme from the Puls Design System.
/// </summary>
/// <remarks>
/// Merge this dictionary alongside <see cref="ColorsPalette"/> in your App.xaml when dark theme is active.
/// Use <c>DynamicResource</c> for colors so that swapping <see cref="ColorsLight"/> with this dictionary at runtime will automatically update all views.
/// <code>
/// // In your App.xaml.cs or theme-switching logic:
/// var themeDict = Application.Current.RequestedTheme == AppTheme.Dark
///     ? (ResourceDictionary)new ColorsDark()
///     : new ColorsLight();
/// // Replace the semantic color dictionary at index 1 (after ColorsPalette):
/// Application.Current.Resources.MergedDictionaries[1] = themeDict;
/// </code>
/// </remarks>
public partial class ColorsDark : ResourceDictionary
{
    public ColorsDark()
    {
        InitializeComponent();
    }
}
