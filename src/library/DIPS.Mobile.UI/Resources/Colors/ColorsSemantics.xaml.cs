namespace DIPS.Mobile.UI.Resources.Colors;

/// <summary>
/// A XAML <see cref="ResourceDictionary"/> containing all semantic color tokens from the Puls Design System
/// as <see cref="AppThemeBinding"/> entries that automatically switch between light and dark values
/// when the OS theme changes — no extra code required.
/// </summary>
/// <remarks>
/// Merge this dictionary alongside <see cref="ColorsPalette"/> in your App.xaml.
/// Use <c>DynamicResource</c> in XAML — colors update automatically when the OS theme changes:
/// <code>
/// &lt;Application.Resources&gt;
///   &lt;ResourceDictionary&gt;
///     &lt;ResourceDictionary.MergedDictionaries&gt;
///       &lt;dui:ColorsPalette /&gt;
///       &lt;dui:ColorsSemantics /&gt;
///     &lt;/ResourceDictionary.MergedDictionaries&gt;
///   &lt;/ResourceDictionary&gt;
/// &lt;/Application.Resources&gt;
/// </code>
/// Then in XAML, use <c>DynamicResource</c> for any semantic color key:
/// <code>
/// &lt;Label TextColor="{DynamicResource color_text_default}"
///        BackgroundColor="{DynamicResource color_surface_default}" /&gt;
/// </code>
/// The <c>AppThemeBinding</c> inside each <c>Color</c> entry handles the light/dark switch
/// automatically — no <c>RequestedThemeChanged</c> subscription is needed.
/// </remarks>
public partial class ColorsSemantics : ResourceDictionary
{
    public ColorsSemantics()
    {
        InitializeComponent();
    }
}
