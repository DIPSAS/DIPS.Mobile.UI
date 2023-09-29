using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
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
            BorderColor = Colors.GetColor(ColorName.color_system_white),
            BorderWidth = 3,
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            HeightRequest = Sizes.GetSize(SizeName.size_15)
        };
        ((Button)Button).CornerRadius = (int)(Button.HeightRequest / 2);
        ((Button)Button).Padding = DeviceInfo.Platform == DevicePlatform.Android
            ? Sizes.GetSize(SizeName.size_1)
            : Sizes.GetSize(SizeName.size_3);
        Button.SetBinding(Microsoft.Maui.Controls.Button.ImageSourceProperty,
            new Binding(nameof(Icon), source: this));
        Button.SetBinding(BackgroundColorProperty, new Binding(nameof(ButtonBackgroundColor), source: this));
        Button.SetBinding(IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        Button.SetBinding(OpacityProperty,
            new Binding(nameof(IsEnabled),
                converter: new BoolToObjectConverter {TrueObject = (double)1, FalseObject = 0.5}, source: this));
        Button.SetBinding(Microsoft.Maui.Controls.Button.CommandProperty, new Binding(nameof(Command), source: this));

        BadgeLabel = new Label
        {
            TextColor = Colors.GetColor(ColorName.color_system_white),
            LineBreakMode = LineBreakMode.NoWrap,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
            FontSize = Sizes.GetSize(SizeName.size_3),
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
        };


        Badge = new Border
        {
            Content = BadgeLabel,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            StrokeShape = new RoundRectangle {CornerRadius = 10},
            WidthRequest = 20,
            HeightRequest = 20,
            Padding = 0,
            Opacity = .9,
            InputTransparent = true
        };
#if __ANDROID__
        Badge.Padding = new Thickness(1, 0, 1, 1);
#endif
        Badge.SetBinding(BackgroundColorProperty, new Binding(nameof(BadgeColor), source: this));

        Add(Button);
        Add(Badge);
    }

    private Border Badge { get; }

    private Label BadgeLabel { get; }

    internal View Button { get; }

    protected override async void OnHandlerChanged()
    {
        base.OnHandlerChanged();

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