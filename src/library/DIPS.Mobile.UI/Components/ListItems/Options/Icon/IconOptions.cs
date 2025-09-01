using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Icon;

public partial class IconOptions : ListItemOptions
{
    internal static void SetupDefaults(ListItem listItem)
    {
        if (listItem.ImageIcon is null)
            return;
        
        listItem.ImageIcon.Margin = (Thickness)MarginProperty.DefaultValue;
        listItem.ImageIcon.TintColor = (Color?)ColorProperty.DefaultValue;
        listItem.ImageIcon.VerticalOptions = (LayoutOptions)VerticalOptionsProperty.DefaultValue;
    }

    protected override void DoBind(ListItem listItem)
    {
        if (listItem.ImageIcon is null)
            return;
        
        listItem.ImageIcon.SetBinding(View.MarginProperty, static (IconOptions options) => options.Margin, source: this);
        listItem.ImageIcon.SetBinding(Image.TintColorProperty, static (IconOptions options) => options.Color, source: this);
        listItem.ImageIcon.SetBinding(VisualElement.IsVisibleProperty, static (IconOptions options) => options.IsVisible, source: this);
        listItem.ImageIcon.SetBinding(View.VerticalOptionsProperty, static (IconOptions options) => options.VerticalOptions, source: this);
        listItem.ImageIcon.SetBinding(VisualElement.OpacityProperty, static (IconOptions options) => options.Opacity, source: this);
    }

}