using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Icons;
using DIPS.Mobile.UI.Sizes.Sizes;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu.NavigationMenuButton;

public partial class NavigationMenuButton : Grid
{
    public NavigationMenuButton()
    {
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new() { Width = GridLength.Auto }, new() { Width = GridLength.Auto }
        };

        Opacity = 0;

        ColumnSpacing = 8;
        
        HorizontalOptions = LayoutOptions.End;

        
        var label = new Label
        {
            TextColor = Colors.GetColor(ColorName.color_primary_90),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        label.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
        
        var border = new Border
        {
            BackgroundColor = Colors.GetColor(ColorName.color_system_white),
            StrokeShape = new RoundRectangle { CornerRadius = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2) },
            Padding = new Thickness(UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2)),
            VerticalOptions = LayoutOptions.Center,
            Content = label
        };

        var button = new ImageButton
        {
            CornerRadius = 30,
            WidthRequest = 60,
            HeightRequest = 60,
            HorizontalOptions = LayoutOptions.End,
            Source = Icons.GetIcon(IconName.arrow_right_s_line),
            BorderColor = Colors.GetColor(ColorName.color_system_white),
            BorderWidth = 3,
            Padding = 10,
            Rotation = 90,
            BackgroundColor = Colors.GetColor(ColorName.color_primary_90)
        };
        button.Source.SetBinding(ImageButton.SourceProperty, new Binding(nameof(Icon), source: this));
        
        Add(border);
        Add(button);
        Grid.SetColumn(button, 1);

    }
}