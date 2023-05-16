using Microsoft.Maui.Handlers;

#if __IOS__
using DuiSearchBar = DIPS.Mobile.UI.Components.Searching.iOS.DuiSearchBar;
#elif __ANDROID__
using DuiSearchBar = Android.Views.View;
#else 
using DuiSearchBar = Microsoft.Maui.Controls.View;
#endif

namespace DIPS.Mobile.UI.Components.Searching;

internal partial class  SearchBarHandler : ViewHandler<SearchBar, DuiSearchBar>
{
    public SearchBarHandler() : base(SearchBarPropertyMapper)
    {
        Construct();
    }

    private partial void Construct();

    public static readonly IPropertyMapper<SearchBar, SearchBarHandler> SearchBarPropertyMapper = new PropertyMapper<SearchBar, SearchBarHandler>(ViewMapper)
    {
        [nameof(SearchBar.IconsColor)] = MapIconsColor,
        [nameof(SearchBar.IsBusy)] = MapIsBusy,
        [nameof(SearchBar.HasBusyIndication)] = MapHasBusyIndication,
        [nameof(SearchBar.HasCancelButton)] = MapHasCancelButton,
        [nameof(SearchBar.Placeholder)] = MapPlaceholder,
        [nameof(SearchBar.TextColor)] = MapTextColor,
        [nameof(SearchBar.BarColor)] = MapBarColor,
        [nameof(SearchBar.PlaceholderColor)] = MapPlaceholderColor,
        [nameof(SearchBar.Text)] = MapText,
        [nameof(SearchBar.CancelButtonTextColor)] = MapCancelButtonTextColor,
    };
}