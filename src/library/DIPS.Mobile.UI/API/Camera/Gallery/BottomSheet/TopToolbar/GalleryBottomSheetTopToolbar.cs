using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet.TopToolbar;

internal class GalleryBottomSheetTopToolbar : Grid
{
    private readonly Label m_numberOfImagesLabel;
    private readonly ContentView m_borderAroundNumberOfImages;
    private readonly Button m_editButton;
    private readonly Button m_infoButton;

    public GalleryBottomSheetTopToolbar(Action onInfoIconTapped, Action onEditButtonTapped)
    {
        m_numberOfImagesLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Microsoft.Maui.Graphics.Colors.White,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
        };
        
        m_borderAroundNumberOfImages = new ContentView
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Content = m_numberOfImagesLabel,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.DarkSlateGray,
            Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_small))
        };

        UI.Effects.Layout.Layout.SetCornerRadius(m_borderAroundNumberOfImages, Sizes.GetSize(SizeName.radius_large));

        m_infoButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            ImageSource = Icons.GetIcon(IconName.information_line),
            ImageTintColor = Colors.GetColor(ColorName.color_icon_on_fill_inverted),
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
            Command = new Command(onInfoIconTapped)
        };
        
        m_editButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            ImageSource = Icons.GetIcon(IconName.filter_fill),
            ImageTintColor = Colors.GetColor(ColorName.color_icon_on_fill_inverted),
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
            Command = new Command(onEditButtonTapped)
        };

        var horizontalStackLayout = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Children = { m_infoButton, m_editButton },
            Spacing = Sizes.GetSize(SizeName.size_1)
        };
        
        Add(m_borderAroundNumberOfImages);
        Add(horizontalStackLayout);
    }
    
    internal void UpdateNumberOfImagesLabel(string text)
    {
        m_numberOfImagesLabel.Text = text;
        m_borderAroundNumberOfImages.IsVisible = !string.IsNullOrEmpty(text);
    }

    public void GoToEditState()
    {
        m_borderAroundNumberOfImages.IsVisible = false;
        m_editButton.IsVisible = false;
        m_infoButton.IsVisible = false;
    }

    public void GoToDefaultState()
    {
        m_borderAroundNumberOfImages.IsVisible = true;
        m_editButton.IsVisible = true;
        m_infoButton.IsVisible = true;
    }
}