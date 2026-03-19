namespace DIPS.Mobile.UI.Resources.Colors;

/// <summary>
/// A XAML <see cref="ResourceDictionary"/> containing all palette color tokens from the Puls Design System.
/// These are theme-independent colors that can be used across light and dark modes.
/// </summary>
/// <remarks>
/// Merge this dictionary in your App.xaml to gain IDE color previews and use colors via StaticResource or DynamicResource:
/// <code>
/// &lt;Application.Resources&gt;
///   &lt;ResourceDictionary&gt;
///     &lt;ResourceDictionary.MergedDictionaries&gt;
///       &lt;dui:ColorsPalette /&gt;
///     &lt;/ResourceDictionary.MergedDictionaries&gt;
///   &lt;/ResourceDictionary&gt;
/// &lt;/Application.Resources&gt;
/// </code>
/// </remarks>
public partial class ColorsPalette : ResourceDictionary
{
    public ColorsPalette()
    {
        InitializeComponent();
    }
}
