namespace DIPS.Mobile.UI.Resources.Colors;

/// <summary>
/// A XAML <see cref="ResourceDictionary"/> containing all semantic color tokens for the <b>light</b> theme from the Puls Design System.
/// </summary>
/// <remarks>
/// Merge this dictionary alongside <see cref="ColorsPalette"/> in your App.xaml to enable IDE color previews and runtime theme support.
/// Use <c>DynamicResource</c> for colors so that swapping this dictionary with <see cref="ColorsDark"/> at runtime will automatically update all views.
/// <code>
/// &lt;Application.Resources&gt;
///   &lt;ResourceDictionary&gt;
///     &lt;ResourceDictionary.MergedDictionaries&gt;
///       &lt;dui:ColorsPalette /&gt;
///       &lt;dui:ColorsLight /&gt; &lt;!-- swap with ColorsDark when dark theme is active --&gt;
///     &lt;/ResourceDictionary.MergedDictionaries&gt;
///   &lt;/ResourceDictionary&gt;
/// &lt;/Application.Resources&gt;
/// </code>
/// </remarks>
public partial class ColorsLight : ResourceDictionary
{
    public ColorsLight()
    {
        InitializeComponent();
    }
}
