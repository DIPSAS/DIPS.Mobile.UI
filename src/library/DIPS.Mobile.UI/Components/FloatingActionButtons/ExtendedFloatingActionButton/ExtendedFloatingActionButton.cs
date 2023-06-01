using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.ExtendedFloatingActionButton;

/// <summary>
/// A FAB that can display both an icon and a text
/// </summary>
public partial class ExtendedFloatingActionButton : HorizontalStackLayout
{
    public ExtendedFloatingActionButton()
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
        
        var border = new Border
        {
            BackgroundColor = Colors.GetColor(ColorName.color_system_white),
            StrokeShape = new RoundRectangle { CornerRadius = Sizes.GetSize(SizeName.size_2) },
            Padding = new Thickness(Sizes.GetSize(SizeName.size_2)),
            VerticalOptions = LayoutOptions.Center,
            Content = label
        };
        
        var floatingActionButton = new FloatingActionButton.FloatingActionButton();
        floatingActionButton.SetBinding(FloatingActionButton.FloatingActionButton.IconProperty, new Binding(nameof(Icon), source: this));
        floatingActionButton.SetBinding(FloatingActionButton.FloatingActionButton.CommandProperty, new Binding(nameof(Command), source: this));
        floatingActionButton.SetBinding(FloatingActionButton.FloatingActionButton.IconRotationProperty, new Binding(nameof(IconRotation), source: this));
        floatingActionButton.SetBinding(FloatingActionButton.FloatingActionButton.ButtonBackgroundColorProperty, new Binding(nameof(ButtonBackgroundColor), source: this));
        
        Add(border);
        Add(floatingActionButton);
    }

}