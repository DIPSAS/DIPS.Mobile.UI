using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.ExtendedNavigationMenuButton;

/// <summary>
/// An extended <see cref="NavigationMenuButton"/> that can also display text
/// </summary>
internal partial class ExtendedNavigationMenuButton : HorizontalStackLayout
{
    public ExtendedNavigationMenuButton()
    {
        Spacing = 8;
        
        var labelButton = new Button()
        {
            Style = DIPS.Mobile.UI.Resources.Styles.Styles.GetButtonStyle(ButtonStyle.SecondarySmall),
            TextColor = Colors.GetColor(ColorName.color_primary_90),
            BackgroundColor = Colors.GetColor(ColorName.color_system_white), 
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        labelButton.SetBinding(Microsoft.Maui.Controls.Button.TextProperty, new Binding(nameof(Title), source: this));
        labelButton.SetBinding(IsEnabledProperty,
            new Binding(nameof(IsEnabled), source: this));


        var floatingActionButton = new NavigationMenuButton.NavigationMenuButton();
        AutomationProperties.SetExcludedWithChildren(floatingActionButton, true);
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.IconProperty,
            new Binding(nameof(Icon), source: this));
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.CommandProperty,
            new Binding(nameof(Command), source: this));
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.ButtonBackgroundColorProperty,
            new Binding(nameof(ButtonBackgroundColor), source: this));
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.BadgeCountProperty,
            new Binding(nameof(BadgeCount), source: this));
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.BadgeColorProperty,
            new Binding(nameof(BadgeColor), source: this));
        floatingActionButton.SetBinding(IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        


        Add(labelButton);
        Add(floatingActionButton);
    }
}