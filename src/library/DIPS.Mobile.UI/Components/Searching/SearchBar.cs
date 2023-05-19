using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar : View
    {
        public SearchBar()
        {
            this.SetAppThemeColor(IconsColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(BarColorProperty, ColorName.color_neutral_05);
            this.SetAppThemeColor(iOSSearchFieldBackgroundColorProperty, ColorName.color_neutral_05);
        }
        
        private static void OnTextChanged(BindableObject bindable, object value, object newValue)
        {
            if (bindable is SearchBar searchBar && value is string oldTextValue && newValue is string newTextValue)
            {
                searchBar.TextChanged?.Invoke(searchBar, new TextChangedEventArgs(oldTextValue, newTextValue));
            }
        }
    }
}