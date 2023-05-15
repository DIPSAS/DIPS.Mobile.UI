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
    
}