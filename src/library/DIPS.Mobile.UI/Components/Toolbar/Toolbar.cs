namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// A cross-platform toolbar component that displays a horizontal bar of icon buttons using native platform views.
/// </summary>
/// <remarks>
/// iOS: https://developer.apple.com/design/human-interface-guidelines/toolbars
/// Android: https://m3.material.io/components/toolbars/overview (Bottom App Bar, height 80dp)
/// </remarks>
[ContentProperty(nameof(Buttons))]
public partial class Toolbar : View
{
    public Toolbar()
    {
        // M3 Bottom App Bar spec: height = 80dp
        HeightRequest = Sizes.GetSize(SizeName.size_20);
    }

    private void OnButtonsChanged()
    {
        Handler?.UpdateValue(nameof(Buttons));
    }

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
