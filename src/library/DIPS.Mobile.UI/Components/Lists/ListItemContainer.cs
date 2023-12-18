using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Options.Dividers;

namespace DIPS.Mobile.UI.Components.Lists;

/// <summary>
/// A container that automatically adds rounded corners to the first and last ListItems.
/// Additionally, it sets a divider between ListItems and the margin of the divider
/// </summary>
public partial class ListItemContainer : VerticalStackLayout
{
    public ListItemContainer()
    {
        Spacing = 0;
    }

    protected override async void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        await Task.Delay(1);
        
        var visibleListItems = Children.Where(element => element is ListItem { IsVisible: true }).Cast<ListItem>().ToList();
        
        ResetAndSetListItemDefaultValues(visibleListItems);

        var firstListItem = visibleListItems.FirstOrDefault();
        
        if (firstListItem is not null)
        {
            if (visibleListItems.Count == 1)
            {
                firstListItem.CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_2));
                return;
            }
            
            firstListItem.CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2), 0, 0);
        }

        var lastListItem = visibleListItems.LastOrDefault();
        
        if (lastListItem is not null)
        {
            lastListItem.CornerRadius = new CornerRadius(0, 0, Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2));
        }
    }

    private void ResetAndSetListItemDefaultValues(List<ListItem> visibleListItems)
    {
        foreach (var visibleListItem in visibleListItems)
        {
            visibleListItem.CornerRadius = new CornerRadius(0);
            if(visibleListItem.HasTopDivider)
                visibleListItem.HasTopDivider = false;

            if (visibleListItems.IndexOf(visibleListItem) == 0)
                continue;

            visibleListItem.HasTopDivider = true;
            visibleListItem.DividersOptions = new DividersOptions { TopDividerMargin = DividerMargin };
        }
    }
}