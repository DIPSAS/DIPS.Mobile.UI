using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip
{
    private Image m_toggleableIcon;
    private Image m_customIcon;
    private Image m_customRightIcon;
    private ImageButton m_closeButton;
    private Label m_titleLabel;
    private Border m_border;

    internal void ConstructView()
    {
        var container = new Grid
        {
            ColumnSpacing = Sizes.GetSize(SizeName.content_margin_xsmall),
            
            ColumnDefinitions = 
            [
                new ColumnDefinition(GridLength.Auto), 
                new ColumnDefinition(GridLength.Star), 
                new ColumnDefinition(GridLength.Auto)
            ],
            RowDefinitions = [new RowDefinition(GridLength.Star)]
        };
        
        container.SetBinding(PaddingProperty, static (Chip chip) => chip.InnerPadding, source: this);

        m_customIcon = CreateCustomIcon();
        m_border = CreateBorder();
        m_toggleableIcon = CreateIsToggleableIcon();
        m_titleLabel = CreateTitleLabel();
        m_closeButton = CreateCloseButton();
        m_customRightIcon = CreateCustomRightIcon();
        
        Touch.SetCommand(m_border, new Command(() => OnTappedButtonChip(false)));

        container.Add(m_customIcon);
        container.Add(m_toggleableIcon);
        container.Add(m_titleLabel, 1);
        container.Add(m_closeButton, 2);
        container.Add(m_customRightIcon, 2);

        m_border.Content = container;
        
        Content = m_border;
    }

    private Image CreateCustomRightIcon()
    {
        var image = new Images.Image.Image 
        { 
            Source = Icons.GetIcon(ToggledIconName), 
            HeightRequest = Sizes.GetSize(SizeName.size_4), 
            WidthRequest = Sizes.GetSize(SizeName.size_4),
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
        };
        image.SetBinding(Image.SourceProperty, static (Chip chip) => chip.CustomRightIcon, source: this);
        
        return image;
    }

    private Image CreateCustomIcon()
    {
        var image = CreateIsToggleableIcon();
        image.SetBinding(Image.SourceProperty, static (Chip chip) => chip.CustomIcon, source: this);
        image.SetBinding(Images.Image.Image.TintColorProperty, static (Chip chip) => chip.CustomIconTintColor, source: this);
        image.SetBinding(IsVisibleProperty, static (Chip chip) => chip.CustomIcon, source: this);
        
        return image;
    }

    private Border CreateBorder()
    {
        var border = new Border { StrokeShape = new RoundRectangle
        {
            CornerRadius = Sizes.GetSize(SizeName.radius_small),
            BackgroundColor = Colors.GetColor(ColorName.color_surface_default)
        }};
        
        border.SetBinding(CornerRadiusProperty, static (Chip chip) => chip.CornerRadius, source: this);
        border.SetBinding(BackgroundColorProperty, static (Chip chip) => chip.Color, source: this);
        border.SetBinding(Border.StrokeProperty, static (Chip chip) => chip.BorderColor, source: this);
        border.SetBinding(Border.StrokeThicknessProperty, static (Chip chip) => chip.BorderWidth, source: this);

        return border;
    }

    private ImageButton CreateCloseButton()
    {
        var imageButton = new Images.ImageButton.ImageButton
        { 
            Command = new Command(() => OnTappedButtonChip(true)), 
            Source = Icons.GetIcon(CloseIconName),
            HeightRequest = Sizes.GetSize(SizeName.size_4), 
            WidthRequest = Sizes.GetSize(SizeName.size_4),
            VerticalOptions = LayoutOptions.Center,
            AdditionalHitBoxSize = Sizes.GetSize(SizeName.content_margin_xsmall)
        };

        imageButton.SetBinding(IsVisibleProperty, static (Chip chip) => chip.IsCloseable, source: this);
        imageButton.SetBinding(Images.ImageButton.ImageButton.TintColorProperty, static (Chip chip) => chip.CloseButtonColor, source: this);
        
        return imageButton;
    }

    private Image CreateIsToggleableIcon()
    {
        var image = new Images.Image.Image 
        { 
            Source = Icons.GetIcon(ToggledIconName), 
            HeightRequest = Sizes.GetSize(SizeName.size_4), 
            WidthRequest = Sizes.GetSize(SizeName.size_4),
            VerticalOptions = LayoutOptions.Center
        };
        
        image.SetBinding(IsVisibleProperty, static (Chip chip) => chip.IsToggled, source: this);
        image.SetBinding(Images.Image.Image.TintColorProperty, static (Chip chip) => chip.TitleColor, source: this);
        
        return image;
    }

    private Labels.Label CreateTitleLabel()
    {
        var label = new Labels.Label
        {
            TextColor = Colors.GetColor(ColorName.color_text_action),
            Style = Styles.GetLabelStyle(LabelStyle.Body200),
            VerticalOptions = LayoutOptions.Center,
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };
        label.SetBinding(Label.TextProperty, static (Chip chip) => chip.Title, source: this);
        label.SetBinding(Label.TextColorProperty, static (Chip chip) => chip.TitleColor, source: this);
        label.SetBinding(Label.HorizontalTextAlignmentProperty, static (Chip chip) => chip.TitleTextAlignment, source: this);

        return label;
    }

    private void OnTappedButtonChip(bool didTouchCloseButton)
    {
        if (IsToggleable)
        {
            IsToggled = !IsToggled;
            SendTapped();
        }
        else if (didTouchCloseButton)
        {
            SendCloseTapped();
        }
        else
        {
            SendTapped();
        }
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName.Equals(nameof(CornerRadius)))
        {
            SetCornerRadius();
        }

        if (propertyName.Equals(nameof(IsToggleable)))
        {
            m_customIcon.IsVisible = !IsToggleable;
        }

        if (propertyName.Equals(nameof(CustomIcon)))
        {
            m_customIcon.IsVisible = CustomIcon is not null;
        }

        if (propertyName.Equals(nameof(CustomRightIcon)))
        {
            m_customRightIcon.IsVisible = CustomRightIcon is not null && !IsCloseable;
        }
    }

    private void SetCornerRadius()
    {
        m_border.StrokeShape = new RoundRectangle
        {
            CornerRadius = CornerRadius
        };
    }
}