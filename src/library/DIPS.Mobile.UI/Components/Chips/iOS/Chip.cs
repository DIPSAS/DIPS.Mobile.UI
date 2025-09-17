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
    private Border m_closeButton;
    private Label m_titleLabel;
    private Border m_border;
    private Grid m_container;

    internal void ConstructView()
    {
        m_container = new Grid
        {
            ColumnSpacing = Sizes.GetSize(SizeName.content_margin_xsmall),
            Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_small), Sizes.GetSize(SizeName.content_margin_xsmall)),
            ColumnDefinitions = 
            [
                new ColumnDefinition(GridLength.Auto), 
                new ColumnDefinition(GridLength.Star), 
                new ColumnDefinition(GridLength.Auto)
            ],
            RowDefinitions = [new RowDefinition(GridLength.Star)]
        };

        m_customIcon = CreateCustomIcon();
        m_border = CreateBorder();
        m_toggleableIcon = CreateIsToggleableIcon();
        m_titleLabel = CreateTitleLabel();
        m_closeButton = CreateCloseButton();
        
        Touch.SetCommand(m_border, new Command(() => OnTappedButtonChip(false)));
        
        m_container.Add(m_customIcon);
        m_container.Add(m_toggleableIcon);
        m_container.Add(m_titleLabel, 1);
        m_container.Add(m_closeButton, 2);

        m_border.Content = m_container;
        
        Content = m_border;
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
            BackgroundColor = Colors.GetColor(ColorName.color_surface_active)
        }};
        
        border.SetBinding(CornerRadiusProperty, static (Chip chip) => chip.CornerRadius, source: this);
        border.SetBinding(BackgroundColorProperty, static (Chip chip) => chip.Color, source: this);
        border.SetBinding(Border.StrokeProperty, static (Chip chip) => chip.BorderColor, source: this);
        border.SetBinding(Border.StrokeThicknessProperty, static (Chip chip) => chip.BorderWidth, source: this);

        return border;
    }

    private Border CreateCloseButton()
    {
        var closeChipCommand = new Command(() => OnTappedButtonChip(true));
        var closeButtonIcon = new Images.ImageButton.ImageButton
        { 
            Command = closeChipCommand, 
            Source = Icons.GetIcon(CloseIconName),
            HeightRequest = Sizes.GetSize(SizeName.size_4), 
            WidthRequest = Sizes.GetSize(SizeName.size_4),
            VerticalOptions = LayoutOptions.Center,
        };
        
        closeButtonIcon.SetBinding(Images.ImageButton.ImageButton.TintColorProperty, static (Chip chip) => chip.CloseButtonColor, source: this);
        
        var closeButtonWrapper = new Border()
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Orange,
            Padding = new Thickness(
                Sizes.GetSize(SizeName.content_margin_xsmall), 
                Sizes.GetSize(SizeName.content_margin_xsmall), 
                Sizes.GetSize(SizeName.content_margin_small), 
                Sizes.GetSize(SizeName.content_margin_xsmall)),
        };
        
        var tapGestureRecognizer = new TapGestureRecognizer();
        
        tapGestureRecognizer.Tapped += (_, _) => closeChipCommand.Execute(null);
        
        closeButtonWrapper.GestureRecognizers.Add(tapGestureRecognizer);
        closeButtonWrapper.SetBinding(IsVisibleProperty, static (Chip chip) => chip.IsCloseable, source: this);
        closeButtonWrapper.Content = closeButtonIcon;
        
        return closeButtonWrapper;
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
            TextColor = Colors.GetColor(ColorName.color_text_default),
            Style = Styles.GetLabelStyle(LabelStyle.Body200),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };
        label.SetBinding(Label.TextProperty, static (Chip chip) => chip.Title, source: this);
        label.SetBinding(Label.TextColorProperty, static (Chip chip) => chip.TitleColor, source: this);

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
        
        if (propertyName.Equals(nameof(IsCloseable)))
        {
            m_container.Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_medium), 0, 0, 0);
            m_container.ColumnSpacing = 0;
            m_titleLabel.Padding = new Thickness(0, Sizes.GetSize(SizeName.content_margin_xsmall));
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