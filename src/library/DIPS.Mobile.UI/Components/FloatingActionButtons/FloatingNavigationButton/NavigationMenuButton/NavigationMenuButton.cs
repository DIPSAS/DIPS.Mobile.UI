using DIPS.Mobile.UI.Effects.DUIImageEffect;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton.NavigationMenuButton;

public partial class NavigationMenuButton : Grid
{
    public NavigationMenuButton()
    {
        RowDefinitions = new RowDefinitionCollection { new() { Height = GridLength.Auto } };
        ColumnDefinitions = new ColumnDefinitionCollection { new() { Width = GridLength.Auto } };
        
        ImageButton = new ImageButton
        {
            BorderColor = Colors.GetColor(ColorName.color_system_white),
            BorderWidth = 3
        };

    // Workaround for a bug in Android where the circle gets clipped left, top, right and bottom
#if __ANDROID__
        ImageButton.Clip = new EllipseGeometry
        {
            RadiusX = 30,
            RadiusY = 30,
            Center = new Point(new Size(30))
        };
        ImageButton.BorderWidth = 6;
#endif
        
        DUIImageEffect.SetColor(ImageButton, Colors.GetColor(ColorName.color_system_white));
        ImageButton.SetBinding(ImageButton.SourceProperty, new Binding(nameof(Icon), source: this));
        ImageButton.SetBinding(ImageButton.CommandProperty, new Binding(nameof(Command), source: this));
        ImageButton.SetBinding(BackgroundColorProperty, new Binding(nameof(ButtonBackgroundColor), source: this));
        
        // Can not use design system here, because we need to set corner radius to half of the width/height
        ImageButton.WidthRequest = 60;
        ImageButton.HeightRequest = 60;
        ImageButton.CornerRadius = 30;

        BadgeLabel = new Label
        {
            TextColor = Colors.GetColor(ColorName.color_system_white),
            LineBreakMode = LineBreakMode.NoWrap,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
            FontSize = Sizes.GetSize(SizeName.size_3),
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center
        };
        
        Badge = new Border
        {
            Content = BadgeLabel,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            StrokeShape = new RoundRectangle { CornerRadius = 10 },
            WidthRequest = 20,
            HeightRequest = 20,
            Padding = 0,
            Opacity = .9,
            InputTransparent = true
        };
#if __ANDROID__
        Badge.Padding = 1;
#endif
        Badge.SetBinding(BackgroundColorProperty, new Binding(nameof(BadgeColor), source: this));

        Add(ImageButton);
        Add(Badge);
    }

    private Border Badge { get; }

    private Label BadgeLabel { get; }
    
    private ImageButton ImageButton { get; }

    protected override async void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        // We must set this here, because on Android, the ImageButton disappears when binding rotation
        ImageButton.Rotation = IconRotation;
        
        // Workaround for a bug when setting padding on ImageButton (Android): https://github.com/dotnet/maui/pull/14905
        await Task.Delay(1);
        ImageButton.Padding = Sizes.GetSize(SizeName.size_3);
        
        if(BadgeCount is not 0)
            ShowBadge();
        else
            HideBadge();

    }

    public void RotateIconTo(float rotation)
    {
        ImageButton.RotateTo(rotation, 125U);
    }

    private static void OnBadgeCountChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not NavigationMenuButton floatingActionButton)
            return;

        if (newValue is not int badgeCount)
            return;

        if(badgeCount is not 0)
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

    private static void OnDisabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not NavigationMenuButton floatingActionButton)
            return;

        if (newValue is not bool disabled)
            return;

        floatingActionButton.ImageButton.Opacity = disabled ? 0.5 : 1;
        floatingActionButton.ImageButton.IsEnabled = !disabled;
    }
}