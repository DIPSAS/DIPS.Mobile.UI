using DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;
using DIPS.Mobile.UI.Components.ListItems.Options.Title;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace DIPS.Mobile.UI.Resources.Styles.ListItem;

public class ListItemTitleStyle
{
    internal static Style KeyWithUnderlyingValue => new(typeof(Components.ListItems.ListItem))
    {
        Setters =
        {
            new Setter
            {
                Property = Components.ListItems.ListItem.TitleOptionsProperty,
                Value = new TitleOptions
                {
                    Style = Styles.GetLabelStyle(LabelStyle.KeyOverValue),
                    Width = GridLength.Star,
                    TextColor = Colors.Colors.GetColor(ColorName.color_neutral_60)
                }
            },
            new Setter
            {
                Property = Components.ListItems.ListItem.SubtitleOptionsProperty,
                Value = new SubtitleOptions
                {
                    Style = Styles.GetLabelStyle(LabelStyle.ValueBelowKey),
                    TextColor = Colors.Colors.GetColor(ColorName.color_neutral_90)
                }
            }
        }
    };
    
    internal static Style KeyWithInlineValue => new(typeof(Components.ListItems.ListItem))
    {
        Setters =
        {
            new Setter
            {
                Property = Components.ListItems.ListItem.TitleOptionsProperty,
                Value = new TitleOptions
                {
                    Style = Styles.GetLabelStyle(LabelStyle.KeyInlineWithValue),
                    Width = GridLength.Star
                }
            }
        }
    };
}