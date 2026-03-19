namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// A button to display in a <see cref="Toolbar"/>.
/// </summary>
public partial class ToolbarButton : Element
{
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (Menu is not null)
        {
            SetInheritedBindingContext(Menu, BindingContext);
        }
    }
}
