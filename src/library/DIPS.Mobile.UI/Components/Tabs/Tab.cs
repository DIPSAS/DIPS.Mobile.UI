using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace DIPS.Mobile.UI.Components.Tabs;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

public partial class Tab : ContentView
{
    private void OnIsSelectedChanged()
    {
        Style = IsSelected ? SelectedStyle : DefaultStyle;
    }
    
    private static Style DefaultStyle =>
        new(typeof(Tab))
        {
            
            Setters =
            {
                new Setter { Property = Label.TextColorProperty, Value = Colors.GetColor(ColorName.color_neutral_70) },
                new Setter { Property = Label.FontAttributesProperty, Value = Styles.GetLabelStyle(LabelStyle.Body200) }
            }
        };

    private static Style SelectedStyle =>
        new(typeof(Tab))
        {
            Setters =
            {
                new Setter { Property = Label.TextColorProperty, Value = Colors.GetColor(ColorName.color_neutral_90) },
                new Setter { Property = Label.FontAttributesProperty, Value = Styles.GetLabelStyle(LabelStyle.Body300) }
            }
        };
}
