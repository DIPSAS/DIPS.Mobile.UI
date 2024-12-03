using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Internal;
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
    private readonly Button m_labelButton;

    public ExtendedNavigationMenuButton()
    {
        Spacing = 8;
        
        m_labelButton = new Button()
        {
            AutomationId = "LabelButton".ToDUIAutomationId<ExtendedNavigationMenuButton>(),
            Style = DIPS.Mobile.UI.Resources.Styles.Styles.GetButtonStyle(ButtonStyle.SecondarySmall),
            TextColor = Colors.GetColor(ColorName.color_primary_90),
            BackgroundColor = Colors.GetColor(ColorName.color_system_white), 
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        m_labelButton.SetBinding(Microsoft.Maui.Controls.Button.TextProperty, static (ExtendedNavigationMenuButton extendedNavigationMenuButton) => extendedNavigationMenuButton.Title, source: this);
        m_labelButton.SetBinding(IsEnabledProperty, static (ExtendedNavigationMenuButton extendedNavigationMenuButton) => extendedNavigationMenuButton.IsEnabled, source: this);
        m_labelButton.SetBinding(Microsoft.Maui.Controls.Button.CommandProperty, static (ExtendedNavigationMenuButton extendedNavigationMenuButton) => extendedNavigationMenuButton.Command, source: this);


        var floatingActionButton = new NavigationMenuButton.NavigationMenuButton();
        AutomationProperties.SetExcludedWithChildren(floatingActionButton, true);
        
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.IconProperty, static (ExtendedNavigationMenuButton extendedNavigationMenuButton) => extendedNavigationMenuButton.Icon, source: this);
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.CommandProperty, static (ExtendedNavigationMenuButton extendedNavigationMenuButton) => extendedNavigationMenuButton.Command, source: this);
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.ButtonBackgroundColorProperty, static (ExtendedNavigationMenuButton extendedNavigationMenuButton) => extendedNavigationMenuButton.ButtonBackgroundColor, source: this);
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.BadgeCountProperty, static (ExtendedNavigationMenuButton extendedNavigationMenuButton) => extendedNavigationMenuButton.BadgeCount, source: this);
        floatingActionButton.SetBinding(NavigationMenuButton.NavigationMenuButton.BadgeColorProperty, static (ExtendedNavigationMenuButton extendedNavigationMenuButton) => extendedNavigationMenuButton.BadgeColor, source: this);
        floatingActionButton.SetBinding(IsEnabledProperty, static (ExtendedNavigationMenuButton extendedNavigationMenuButton) => extendedNavigationMenuButton.IsEnabled, source: this);

        Add(m_labelButton);
        Add(floatingActionButton);
    }

    public void SetSemanticFocus()
    {
        m_labelButton.SetSemanticFocus();
    }
}