using System.Globalization;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Dividers;

/// <summary>
/// Make it so that the divider in the ListItem is always at the bottom or top
/// </summary>
internal class MinusPaddingOfListItemConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Thickness thickness)
        {
            throw new Exception("Value must be of type Thickness");
        }
        
        if(parameter is not Thickness ListItemPadding)
            throw new Exception("Parameter must be of type Thickness");

        return new Thickness(thickness.Left - ListItemPadding.Left, thickness.Top - ListItemPadding.Top, thickness.Right - ListItemPadding.Right, thickness.Bottom - ListItemPadding.Bottom);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}