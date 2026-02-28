namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// A cross-platform toolbar component that displays a horizontal bar of icon buttons using native platform views.
/// </summary>
/// <remarks>
/// iOS: https://developer.apple.com/design/human-interface-guidelines/toolbars
/// Android: https://m3.material.io/components/toolbars/overview
/// </remarks>
[ContentProperty(nameof(Buttons))]
public partial class Toolbar : View
{
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (Buttons is null)
            return;

        foreach (var toolbarButton in Buttons)
        {
            toolbarButton.BindingContext = BindingContext;
        }
    }
}
