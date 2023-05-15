using CoreGraphics;
using DIPS.Mobile.UI.Components.Searching.iOS;
using DIPS.Mobile.UI.Extensions.iOS;
using Foundation;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using UISearchBarStyle = UIKit.UISearchBarStyle;

namespace DIPS.Mobile.UI.Components.Searching;

internal partial class SearchBarHandler : ViewHandler<SearchBar, DuiSearchBar>
{
    public SearchBarHandler() : base(PropertyMapper)
    {
        MauiSearchBar = new InternalSearchBar();
        ActivityIndicatorView = new UIActivityIndicatorView();
    }

    private Microsoft.Maui.Controls.SearchBar MauiSearchBar { get; }

    public static readonly IPropertyMapper<SearchBar, SearchBarHandler> PropertyMapper = new PropertyMapper<SearchBar, SearchBarHandler>(ViewMapper)
    {
        [nameof(SearchBar.IconsColor)] = MapIconsColor,
        [nameof(SearchBar.IsBusy)] = MapIsBusy,
        [nameof(SearchBar.HasBusyIndication)] = MapHasBusyIndication,
        [nameof(SearchBar.HasCancelButton)] = MapHasCancelButton,
        [nameof(SearchBar.Placeholder)] = MapPlaceholder,
        [nameof(SearchBar.TextColor)] = MapTextColor,
        [nameof(SearchBar.TextFieldColor)] = MapTextFieldColor,
        [nameof(SearchBar.BarColor)] = MapBarColor,
        [nameof(SearchBar.PlaceholderColor)] = MapPlaceholderColor,
        [nameof(SearchBar.Text)] = MapText
    };

    private static void MapText(SearchBarHandler handler, SearchBar searchBar)
    {
        handler.VirtualView.Text = searchBar.Text;
        MapHasCancelButton(handler, searchBar);
    }

    private static void MapPlaceholderColor(SearchBarHandler handler, SearchBar searchBar)
    {
        MapPlaceholder(handler, searchBar);
    }

    private static void MapTextColor(SearchBarHandler handler, SearchBar searchBar)
    {
        handler.MauiSearchBar.TextColor = searchBar.TextColor;
        handler.MauiSearchBar.CancelButtonColor = searchBar.TextColor;
    }

    private static void MapPlaceholder(SearchBarHandler handler, SearchBar searchBar)
    {
        handler.MauiSearchBar.Placeholder = searchBar.Placeholder;
    }

    private static void MapHasBusyIndication(SearchBarHandler handler, SearchBar searchBar)
    {
        handler.ActivityIndicatorView.Hidden = !searchBar.HasCancelButton;
    }

    private static void MapTextFieldColor(SearchBarHandler searchBarHandler, SearchBar searchBar)
    {
        if(searchBar.TextFieldColor == null)
            return;

        searchBarHandler.PlatformView.SearchTextField.BackgroundColor = searchBar.TextFieldColor.ToPlatform();
    }

    /// <summary>
    /// Set the border color the same as the bar color to 'hide' border
    /// </summary>
    private static void MapBarColor(SearchBarHandler handler, SearchBar searchBar)
    {
        if (searchBar.BarColor == null)
        {
            return;
        }
            
        
        handler.PlatformView.Layer.BorderWidth = 1;
        handler.PlatformView.Layer.BorderColor = searchBar.BarColor?.ToCGColor();
        handler.VirtualView.BackgroundColor = searchBar.BarColor;
        
        var cancelButton = handler.PlatformView.FindDescendantView<UIButton>();

        if (cancelButton == null)
        {
            return;
        }

        cancelButton.SetTitleColor(searchBar.BarColor?.ToPlatform(), UIControlState.Normal);
        cancelButton.SetTitleColor(searchBar.BarColor?.ToPlatform(), UIControlState.Highlighted);
        cancelButton.SetTitleColor(searchBar.BarColor?.ToPlatform(), UIControlState.Disabled);
    }

    private UIActivityIndicatorView ActivityIndicatorView { get; }

    private UIImageView MagnifierIcon { get; set; } 


    private static void MapIsBusy(SearchBarHandler searchBarHandler, SearchBar searchBar)
    {
        if (!searchBar.HasBusyIndication)
            return;

        
        if (searchBar.IsBusy)
        {
            searchBarHandler.ActivityIndicatorView.Color = searchBar.IconsColor?.ToPlatform();
            searchBarHandler.ActivityIndicatorView.StartAnimating();
                
            if (searchBarHandler.PlatformView.SearchTextField.LeftView is not UIImageView uiImageView) //Magnifier icon on the left
                return;                    
                
            var leftViewSize = uiImageView.Frame.Size;
            searchBarHandler.ActivityIndicatorView.Center = new CGPoint(x:
                leftViewSize.Width / 2, y: leftViewSize.Height / 2);
            searchBarHandler.PlatformView.SearchTextField.LeftView = searchBarHandler.ActivityIndicatorView;
        }
        else
        {
           searchBarHandler.ActivityIndicatorView.RemoveFromSuperview();
           searchBarHandler.PlatformView.SearchTextField.LeftView = searchBarHandler.MagnifierIcon;
        }
    }

    private static void MapIconsColor(SearchBarHandler searchBarHandler, SearchBar searchBar)
    {
        if (searchBarHandler.PlatformView.SearchTextField.LeftView is not UIImageView uiImageView) //Magnifier icon on the left
            return;
        
        uiImageView.TintColor = searchBar.IconsColor?.ToPlatform();
    }

    private static void MapHasCancelButton(SearchBarHandler searchBarHandler, SearchBar internalSearchBar)
    {
        searchBarHandler.PlatformView.ShowsCancelButton = internalSearchBar.HasCancelButton;
        
        if(!internalSearchBar.HasCancelButton) 
            return;
        
        var cancelButton = searchBarHandler.PlatformView.FindChildView<UIButton>();
        if(cancelButton == null)
            return;
        
        cancelButton.Enabled = true;
    }

    protected override DuiSearchBar CreatePlatformView()
    {
        var uiSearchBar = (DuiSearchBar) MauiSearchBar.ToPlatform(MauiContext);
        return uiSearchBar;
    }

    protected override void ConnectHandler(DuiSearchBar platformView)
    {
        base.ConnectHandler(platformView);
        
        platformView.SearchBarStyle = UISearchBarStyle.Minimal;

        if (platformView.SearchTextField.LeftView is UIImageView uiImageView) //Magnifier icon on the left
        {
            MagnifierIcon = uiImageView;
        }
        
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        PlatformView.CancelButtonClicked += OnCancelButtonTouchDown;
    }

    private void UnSubscribeToEvents()
    {
        PlatformView.CancelButtonClicked -= OnCancelButtonTouchDown;
    }

    private void OnCancelButtonTouchDown(object? sender, EventArgs e)
    {
        VirtualView.CancelCommand.Execute(VirtualView.CancelCommandParameter);
    }

    protected override void DisconnectHandler(DuiSearchBar platformView)
    {
        base.DisconnectHandler(platformView);
        
        UnSubscribeToEvents();
    }
}