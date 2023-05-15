using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Searching.iOS;

public class InternalSearchBarHandler : Microsoft.Maui.Handlers.SearchBarHandler 
{
    public InternalSearchBarHandler():base(PropertyMapper)
    {
        
    }
    
    public static readonly IPropertyMapper<InternalSearchBar, InternalSearchBarHandler> PropertyMapper = new PropertyMapper<InternalSearchBar, InternalSearchBarHandler>(Mapper)
    {
        [nameof(ISearchBar.CancelButtonColor)] = OverrideMapCancelButtonColor //MAUI cancelbutton gets removed if text is blank, this makes no sense to us
    };
    
    
    private static void OverrideMapCancelButtonColor(InternalSearchBarHandler handler, InternalSearchBar internalSearchBar)
    {
        var cancelButton = handler.PlatformView.FindDescendantView<UIButton>();

        if (cancelButton == null)
            return;

        if (internalSearchBar.CancelButtonColor == null)
            return;

        cancelButton.SetTitleColor(internalSearchBar.CancelButtonColor.ToPlatform(), UIControlState.Normal);
        cancelButton.SetTitleColor(internalSearchBar.CancelButtonColor.ToPlatform(), UIControlState.Highlighted);
        cancelButton.SetTitleColor(internalSearchBar.CancelButtonColor.ToPlatform(), UIControlState.Disabled);
    }
    
    protected override MauiSearchBar CreatePlatformView()
    {
        return new DuiSearchBar();
    }
}

public class InternalSearchBar : Microsoft.Maui.Controls.SearchBar
{
    
}