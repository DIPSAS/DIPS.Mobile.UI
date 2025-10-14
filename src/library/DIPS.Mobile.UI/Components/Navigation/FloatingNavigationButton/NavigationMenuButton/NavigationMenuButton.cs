using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.Animations;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Controls.Shapes;
using SkiaSharp.Extended.UI.Controls;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.NavigationMenuButton;

internal partial class NavigationMenuButton : Grid
{

    public NavigationMenuButton()
    {
        RowDefinitions = new RowDefinitionCollection {new() {Height = GridLength.Auto}};
        ColumnDefinitions = new ColumnDefinitionCollection {new() {Width = GridLength.Auto}};
        
        Button = new Button()
        {
            AutomationId = "Button".ToDUIAutomationId<NavigationMenuButton>(),
            BorderColor = Colors.GetColor(ColorName.color_border_default),
            BorderWidth = 3,
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            HeightRequest = Sizes.GetSize(SizeName.size_15)
        };
        ((Button)Button).CornerRadius = (int)(Button.HeightRequest / 2);
        ((Button)Button).Padding = DeviceInfo.Platform == DevicePlatform.Android
            ? Sizes.GetSize(SizeName.content_margin_xsmall)
            : Sizes.GetSize(SizeName.content_margin_medium);
        
        Button.SetBinding(Microsoft.Maui.Controls.Button.ImageSourceProperty, static (NavigationMenuButton navigationMenuButton) => navigationMenuButton.Icon, source: this);
        Button.SetBinding(BackgroundColorProperty, static (NavigationMenuButton navigationMenuButton) => navigationMenuButton.ButtonBackgroundColor, source: this);
        Button.SetBinding(IsEnabledProperty, static (NavigationMenuButton navigationMenuButton) => navigationMenuButton.IsEnabled, source: this);
        Button.SetBinding(OpacityProperty, static (NavigationMenuButton navigationMenuButton) => navigationMenuButton.IsEnabled, converter: new BoolToObjectConverter {TrueObject = (double)1, FalseObject = 0.5}, source: this);
        Button.SetBinding(Microsoft.Maui.Controls.Button.CommandProperty, static (NavigationMenuButton navigationMenuButton) => navigationMenuButton.Command, source: this);

        BadgeLabel = new Label
        {
            AutomationId = "BadgeLabel".ToDUIAutomationId<NavigationMenuButton>(),
            TextColor = Microsoft.Maui.Graphics.Colors.White,
            LineBreakMode = LineBreakMode.NoWrap,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
            FontSize = Sizes.GetSize(SizeName.size_3),
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
        };

        Badge = new Border
        {
            AutomationId = "Badge".ToDUIAutomationId<NavigationMenuButton>(),
            Content = BadgeLabel,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            StrokeShape = new Ellipse(),
            WidthRequest = 20,
            HeightRequest = 20,
            Padding = 0,
            Opacity = .9,
            InputTransparent = true
        };
#if __ANDROID__
        Badge.Padding = new Thickness(1, 0, 1, 1);
#endif
        Badge.SetBinding(BackgroundColorProperty, static (NavigationMenuButton navigationMenuButton) => navigationMenuButton.BadgeColor, source: this);

        Add(Button);
        Add(Badge);
    }

    private Border Badge { get; }

    private Label BadgeLabel { get; }

    internal View Button { get; }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
            return;
        
        // We must set this here, because on Android, the ImageButton disappears when binding rotation
        Button.Rotation = IconRotation;

        if (BadgeCount is not 0)
            ShowBadge();
        else
            HideBadge();
    }

    public void RotateIconTo(float rotation)
    {
        Button.RotateTo(rotation, 125U);
    }

    private static void OnBadgeCountChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not NavigationMenuButton floatingActionButton)
            return;

        if (newValue is not int badgeCount)
            return;

        if (badgeCount is not 0)
            floatingActionButton.ShowBadge();
        else
            floatingActionButton.HideBadge();

        floatingActionButton.BadgeLabel.Text = badgeCount.ToString();
    }

    public void ShowBadge()
    {
        Badge.IsVisible = true;
    }

    public void HideBadge()
    {
        Badge.IsVisible = false;
    }
}