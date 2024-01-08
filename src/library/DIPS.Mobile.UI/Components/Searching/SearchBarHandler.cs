namespace DIPS.Mobile.UI.Components.Searching;

internal partial class SearchBarHandler
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
        [nameof(SearchBar.ReturnKeyType)] = MapReturnKeyType
    };

    private void OnInternalSearchBarFocused(object? sender, FocusEventArgs e)
    {
        VirtualView.SendFocused();
    }
    
    private void OnInternalSearchBarUnFocused(object? sender, FocusEventArgs e)
    {
        VirtualView.SendUnfocused();
    }

    public partial void Focus();

    public partial void UnFocus();
}