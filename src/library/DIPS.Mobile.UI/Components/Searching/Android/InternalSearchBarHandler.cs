using Android.Graphics;
using Android.Widget;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using SearchView = AndroidX.AppCompat.Widget.SearchView;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Searching.Android;

public class InternalSearchBarHandler : SearchBarHandler
{
    public InternalSearchBarHandler() : base(PropertyMapper)
    {
    }
    
    public static readonly IPropertyMapper<InternalSearchBar, InternalSearchBarHandler> PropertyMapper = new PropertyMapper<InternalSearchBar, InternalSearchBarHandler>(Mapper)
    {
        [nameof(InternalSearchBar.CornerRadius)] = MapCornerRadius,
        [nameof(InternalSearchBar.IconsColor)] = MapIconsColor
    };

    protected override void ConnectHandler(SearchView platformView)
    {
        base.ConnectHandler(platformView);
        
        RemoveHorizontalLineUnderSearchEditTextView();
    }

    private void RemoveHorizontalLineUnderSearchEditTextView()
    {
        if (QueryEditor is AutoCompleteTextView)
        {
            if (QueryEditor.Parent is View parentView)
            {
                parentView.SetBackgroundColor(Colors.Transparent.ToPlatform());
            }
        }
    }
    
    private static void MapCornerRadius(InternalSearchBarHandler internalSearchBarHandler, InternalSearchBar internalSearchBar)
    {
        if(internalSearchBar.BackgroundColor == null)
            return;
        
        internalSearchBarHandler.PlatformView.SetRoundedRectangularBackground(internalSearchBar.CornerRadius, internalSearchBar.BackgroundColor);
    }

    private static void MapIconsColor(InternalSearchBarHandler internalSearchBarHandler, InternalSearchBar internalSearchBar)
    {
        if(internalSearchBar.IconsColor == null)
            return;
        
        //Set color of icons in the search bar
        foreach (var view in internalSearchBarHandler.PlatformView.GetFlatViewHierarchyCollection())
        {
            if (view is ImageView imageView)
            {
                imageView.SetColorFilter(internalSearchBar.IconsColor.ToPlatform());
            }
        }
        
        //Change color of cursor on the search text edit to the same color as the text color
        if (internalSearchBarHandler.QueryEditor is AutoCompleteTextView autoCompleteTextView)
        {
#pragma warning disable CS0618
            if (PorterDuff.Mode.SrcIn != null)
            {
                autoCompleteTextView.TextCursorDrawable?.SetColorFilter(internalSearchBar.IconsColor.ToPlatform(),
                    PorterDuff.Mode.SrcIn);
            }
#pragma warning restore CS0618
        }
    }
}