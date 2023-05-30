using DIPS.Mobile.UI.Effects.DUIImageEffect;
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
        VerticalOptions = LayoutOptions.End;

        InputTransparent = true;
        CascadeInputTransparent = false;
        
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
            StrokeShape = new RoundRectangle { CornerRadius = Sizes.GetSize(SizeName.size_2) },
            Padding = new Thickness(Sizes.GetSize(SizeName.size_2)),
            VerticalOptions = LayoutOptions.Center,
            Content = label
        };

        var button = new ImageButton
        {
            InputTransparent = false,
            CornerRadius = 30,
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            HeightRequest = Sizes.GetSize(SizeName.size_15),
            HorizontalOptions = LayoutOptions.End,
            BorderColor = Colors.GetColor(ColorName.color_system_white),
            BorderWidth = 3,
            Padding = Sizes.GetSize(SizeName.size_3),
            Rotation = 90,
            BackgroundColor = Colors.GetColor(ColorName.color_primary_90)
        };
        DUIImageEffect.SetColor(button, Colors.GetColor(ColorName.color_system_white));
        button.SetBinding(ImageButton.SourceProperty, new Binding(nameof(Icon), source: this));
        button.SetBinding(ImageButton.CommandProperty, new Binding(nameof(Command), source: this));
        
        Add(border);
        Add(button);
        Grid.SetColumn(button, 1);

    }

  
}