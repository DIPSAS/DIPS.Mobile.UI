namespace DIPS.Mobile.UI.Resources.Sizes;

/// <summary>
/// A XAML <see cref="ResourceDictionary"/> containing all size tokens from the Puls Design System as <see cref="double"/> values.
/// </summary>
/// <remarks>
/// Merge this dictionary in your App.xaml to gain IDE size previews and use sizes via StaticResource or DynamicResource.
/// Values are exposed as <c>x:Double</c> and can be used for <c>WidthRequest</c>, <c>HeightRequest</c>, <c>CornerRadius</c>, etc.
/// <code>
/// &lt;Application.Resources&gt;
///   &lt;ResourceDictionary&gt;
///     &lt;ResourceDictionary.MergedDictionaries&gt;
///       &lt;dui:SizesResourceDictionary /&gt;
///     &lt;/ResourceDictionary.MergedDictionaries&gt;
///   &lt;/ResourceDictionary&gt;
/// &lt;/Application.Resources&gt;
/// </code>
/// Example usage:
/// <code>
/// &lt;BoxView WidthRequest="{StaticResource size_4}" /&gt;
/// &lt;Frame CornerRadius="{StaticResource radius_medium}" /&gt;
/// </code>
/// </remarks>
public partial class SizesResourceDictionary : ResourceDictionary
{
    public SizesResourceDictionary()
    {
        InitializeComponent();
    }
}
