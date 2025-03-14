using System.Collections;
using DIPS.Mobile.UI.Components.Dividers;

namespace DIPS.Mobile.UI.Components.Lists;

public class VerticalStackLayout : Microsoft.Maui.Controls.VerticalStackLayout
{
    public VerticalStackLayout()
    {
       Spacing = Sizes.GetSize(SizeName.content_margin_xsmall);
    }

    protected override void OnChildAdded(Element child)
    {
        base.OnChildAdded(child);

        TrySetDividerToInvisible(child);
    }

    /// <summary>
    /// Attempts to set the last elements' divider to invisible
    /// </summary>
    private void TrySetDividerToInvisible(Element child)
    {
        if (!UI.Effects.Layout.Layout.GetAutoHideLastDivider(this))
            return;
        
        var itemsSource = BindableLayout.GetItemsSource(this);
        if(itemsSource is not IList list || child is not View view)
            return;

        var indexOfChild = list.IndexOf(child.BindingContext);
        if(indexOfChild != list.Count - 1)
            return;
        
        var divider = view.FindChildOfTypeClosestToRoot<Divider>();
        if (divider is not null)
        {
            divider.IsVisible = false;
        }
    }
}