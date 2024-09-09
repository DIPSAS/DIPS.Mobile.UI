using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.API.Camera.ImageGallery;

internal partial class ImageGalleryBottomSheet : BottomSheet
{
    private readonly Action<int> m_onRemoveImage;
    private readonly CarouselView m_carouselView;
    private readonly Label m_numberOfImagesLabel;

    public ImageGalleryBottomSheet(List<byte[]> images, int startingIndex, Action<int> onRemoveImage)
    {
        Positioning = Positioning.Large;
        
        m_onRemoveImage = onRemoveImage;
        var fadedBlackColor = Colors.GetColor(ColorName.color_system_black);
        fadedBlackColor = new Color(fadedBlackColor.Red, fadedBlackColor.Green, fadedBlackColor.Blue, 0.5f);
        
        m_numberOfImagesLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.GetColor(ColorName.color_system_white),
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            Margin = new Thickness(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_1))
        };

        var labelWrapper = new Border
        {
            Content = m_numberOfImagesLabel,
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = fadedBlackColor,
            Stroke = Microsoft.Maui.Graphics.Colors.Transparent,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_4)),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_4))
            }
        };

        var toolbarLayout = new Grid
        {
            BackgroundColor = Colors.GetColor(ColorName.color_system_black),
            ColumnDefinitions = [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star)],
            Padding = new Thickness(Sizes.GetSize(SizeName.size_6), Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_6), Sizes.GetSize(SizeName.size_10))
        };

        var removeImageLabel = new Label
        {
            Text = "Remove image",
            Style = Styles.GetLabelStyle(LabelStyle.UI300),
            TextColor = Colors.GetColor(ColorName.color_system_white),
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center
        };
        
        Touch.SetCommand(removeImageLabel, new Command(RemoveImage));
        
        var doneLabel = new Label
        {
            Text = "Done",
            Style = Styles.GetLabelStyle(LabelStyle.UI300),
            TextColor = Colors.GetColor(ColorName.color_system_white),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };

        Touch.SetCommand(doneLabel, new Command(() => Close()));
        
        toolbarLayout.Add(removeImageLabel);
        toolbarLayout.Add(doneLabel, 1);

        m_carouselView = new CarouselView { Loop = false };
        
        
        m_carouselView.ItemTemplate = new DataTemplate(CreateImageView);
        m_carouselView.PositionChanged += CarouselViewOnPositionChanged;
        
        var navigatePreviousImageButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.chevron_left_line),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonSmall),
            BackgroundColor = fadedBlackColor,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Start,
            Margin = new Thickness(Sizes.GetSize(SizeName.size_4), 0, 0, 0),
            Command = new Command(() =>
            {
                if(m_carouselView.Position > 0)
                    m_carouselView.Position -= 1;
            })
        };

        var navigateNextImageButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.chevron_right_line),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonSmall),
            BackgroundColor = fadedBlackColor,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.size_4), 0),
            Command = new Command(() =>
            {
                if(m_carouselView.Position < Images.Count - 1)
                    m_carouselView.Position += 1;
            })
        };

        var grid = new Grid();
        
        grid.AddRowDefinition(new RowDefinition(GridLength.Star));
        grid.AddRowDefinition(new RowDefinition(GridLength.Auto));
        
        grid.Add(m_carouselView);
        grid.Add(navigatePreviousImageButton);
        grid.Add(navigateNextImageButton);
        grid.Add(labelWrapper);
        grid.Add(toolbarLayout, 0, 1);

        Content = grid;

        Images = images;
        StartingIndex = startingIndex;
    }

    private async void RemoveImage()
    {
        if(Images.Count == 0)
            return;

        var dialogResult = await DialogService.ShowDestructiveConfirmationMessage(DUILocalizedStrings.RemoveImageTitle,
            DUILocalizedStrings.RemoveImageDescription, DUILocalizedStrings.Close, DUILocalizedStrings.Remove);
        
        if(dialogResult == DialogAction.Closed)
            return;
        
        var newImages = new List<byte[]>(Images);
        newImages.RemoveAt(m_carouselView.Position);
        Images = newImages;
        
        m_onRemoveImage.Invoke(m_carouselView.Position);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnImagesChanged();
    }

    private void CarouselViewOnPositionChanged(object? sender, PositionChangedEventArgs e)
    {
        UpdateNumberOfImagesLabel(e.CurrentPosition);
    }

    private void UpdateNumberOfImagesLabel(int position)
    {
        m_numberOfImagesLabel.IsVisible = Images.Count > 0;
        m_numberOfImagesLabel.Text = $"{position + 1}/{Images.Count}";   
    }

    private static Image CreateImageView()
    {
        var image = new Image();
        image.SetBinding(Image.SourceProperty, new Binding(".", converter: new ByteArrayToImageSourceConverter()));
        return image;
    }

    private void OnImagesChanged()
    {
        if (Images.Count == 0)
        {
            Close();
            return;
        }
        
        UpdateNumberOfImagesLabel(m_carouselView.Position);
        m_carouselView.ItemsSource = Images;
    }

    private void OnStartingIndexChanged()
    {
        m_carouselView.Position = StartingIndex;
    }
}