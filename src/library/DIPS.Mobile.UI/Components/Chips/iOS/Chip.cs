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
    private ImageButton m_closeButton;
    private Label m_titleLabel;
    private Border m_border;

    internal void ConstructView()
    {
        var container = new Grid
        {
            ColumnSpacing = Sizes.GetSize(SizeName.size_1),
            Padding = new Thickness(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_1)),
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

        container.Add(m_customIcon);
        container.Add(m_toggleableIcon);
        container.Add(m_titleLabel, 1);
        container.Add(m_closeButton, 2);

        m_border.Content = container;
        
        Content = m_border;
    }

    private Image CreateCustomIcon()
    {
        var image = CreateIsToggleableIcon();
        image.SetBinding(Image.SourceProperty, new Binding(nameof(CustomIcon), source: this));
        image.SetBinding(Images.Image.Image.TintColorProperty, new Binding(nameof(CustomIconTintColor), source: this));
        image.SetBinding(IsVisibleProperty, new Binding(nameof(CustomIcon), source: this));
        
        return image;
    }

    private Border CreateBorder()
    {
        var border = new Border { StrokeShape = new RoundRectangle
        {
            CornerRadius = Sizes.GetSize(SizeName.size_2),
            BackgroundColor = Colors.GetColor(ColorName.color_secondary_30)
        }};
        
        border.SetBinding(CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
        border.SetBinding(BackgroundColorProperty, new Binding(nameof(Color), source: this));
        border.SetBinding(Border.StrokeProperty, new Binding(nameof(BorderColor), source: this));
        border.SetBinding(Border.StrokeThicknessProperty, new Binding(nameof(BorderWidth), source: this));

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
            AdditionalHitBoxSize = Sizes.GetSize(SizeName.size_1)
        };

        imageButton.SetBinding(IsVisibleProperty, new Binding(nameof(IsCloseable), source: this));
        imageButton.SetBinding(Images.ImageButton.ImageButton.TintColorProperty, new Binding(nameof(CloseButtonColor), source: this));
        
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
        
        image.SetBinding(IsVisibleProperty, new Binding(nameof(IsToggled), source: this));
        image.SetBinding(Images.Image.Image.TintColorProperty, new Binding(nameof(TitleColor), source: this));

        return image;
    }

    private Labels.Label CreateTitleLabel()
    {
        var label = new Labels.Label
        {
            TextColor = Colors.GetColor(ColorName.color_system_black),
            Style = Styles.GetLabelStyle(LabelStyle.Body200),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        label.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
        label.SetBinding(Label.TextColorProperty, new Binding(nameof(TitleColor), source: this));

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
    }

    private void SetCornerRadius()
    {
        m_border.StrokeShape = new RoundRectangle
        {
            CornerRadius = CornerRadius
        };
    }
}