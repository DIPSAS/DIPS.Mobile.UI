using DIPS.Mobile.UI.Converters.ValueConverters;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.ExtendedNavigationMenuButton;

/// <summary>
/// An extended <see cref="NavigationMenuButton"/> that can also display text
/// </summary>
internal partial class ExtendedNavigationMenuButton : HorizontalStackLayout
{
    public ExtendedNavigationMenuButton()
    {
        Spacing = 8;
        
        var label = new Label
        {
            TextColor = Colors.GetColor(ColorName.color_primary_90),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        label.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
        
        TextBorder = new Border
        {
            BackgroundColor = Colors.GetColor(ColorName.color_system_white),
            StrokeShape = new RoundRectangle { CornerRadius = Sizes.GetSize(SizeName.size_2) },
            Padding = new Thickness(Sizes.GetSize(SizeName.size_2)),
            VerticalOptions = LayoutOptions.Center,
            Content = label
        };
        TextBorder.SetBinding(OpacityProperty, new Binding(nameof(IsEnabled), converter: new BoolToObjectConverter{TrueObject = (double)1, FalseObject = 0.5}, source: this));


        var floatingActionButton = new NavigationMenuButton.NavigationMenuButton();
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.IconProperty, new Binding(nameof(Icon), source: this));
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.CommandProperty, new Binding(nameof(Command), source: this));
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.ButtonBackgroundColorProperty, new Binding(nameof(ButtonBackgroundColor), source: this));
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.BadgeCountProperty, new Binding(nameof(BadgeCount), source: this));
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.BadgeColorProperty, new Binding(nameof(BadgeColor), source: this));
        floatingActionButton.SetBinding(IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        
        Add(TextBorder);
        Add(floatingActionButton);
    }

    public Border TextBorder { get; set; }

}