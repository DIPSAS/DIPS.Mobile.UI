using DIPS.Mobile.UI.Effects.DUIImageEffect;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingActionButton;

public partial class FloatingActionButton : Grid
{
    private readonly ImageButton m_imageButton;

    public FloatingActionButton()
    {
        RowDefinitions = new RowDefinitionCollection { new() { Height = GridLength.Auto } };
        ColumnDefinitions = new ColumnDefinitionCollection { new() { Width = GridLength.Auto } };
        
        m_imageButton = new ImageButton
        {
            BorderColor = Colors.GetColor(ColorName.color_system_white),
            BorderWidth = 3
        };
        
        DUIImageEffect.SetColor(m_imageButton, Colors.GetColor(ColorName.color_system_white));
        m_imageButton.SetBinding(ImageButton.SourceProperty, new Binding(nameof(Icon), source: this));
        m_imageButton.SetBinding(ImageButton.CommandProperty, new Binding(nameof(Command), source: this));
        m_imageButton.SetBinding(RotationProperty, new Binding(nameof(IconRotation), source: this));
        m_imageButton.SetBinding(BackgroundColorProperty, new Binding(nameof(ButtonBackgroundColor), source: this));
        
        // Can not use design system here, because we need to set corner radius to half of the width/height
        m_imageButton.WidthRequest = 60;
        m_imageButton.HeightRequest = 60;
        m_imageButton.CornerRadius = 30;


        BadgeLabel = new Label
        {
            Text = "1", TextColor = Colors.GetColor(ColorName.color_system_white),
            LineBreakMode = LineBreakMode.NoWrap,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
            FontSize = Sizes.GetSize(SizeName.size_3),
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center
        };
        Badge = new Border
        {
            Content = BadgeLabel,
            BackgroundColor = Colors.GetColor(ColorName.color_obsolete_danger),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            StrokeShape = new RoundRectangle { CornerRadius = 10 },
            WidthRequest = 20,
            HeightRequest = 20,
            Padding = 0,
            Opacity = .9,
            InputTransparent = true
        };

        Add(m_imageButton);
        Add(Badge);
    }

    private Border Badge { get; }

    private Label BadgeLabel { get; }

    protected override async void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        // Workaround for a bug when setting padding on ImageButton (Android): https://github.com/dotnet/maui/pull/14905
        await Task.Delay(1);
        m_imageButton.Padding = Sizes.GetSize(SizeName.size_3);
    }

    public void RotateIconTo(float rotation)
    {
        m_imageButton.RotateTo(rotation, 125U);
    }

    private static void OnBadgeCountChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not FloatingActionButton floatingActionButton)
            return;

        if (newValue is not int badgeCount)
            return;

        floatingActionButton.Badge.IsVisible = badgeCount is not 0;
        floatingActionButton.BadgeLabel.Text = badgeCount.ToString();
    }
}