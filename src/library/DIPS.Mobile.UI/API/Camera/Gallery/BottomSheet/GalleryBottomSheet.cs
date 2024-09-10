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

namespace DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet;

internal partial class GalleryBottomSheet : Components.BottomSheets.BottomSheet
{
    private readonly Action<int> m_onRemoveImage;
    private readonly Label m_numberOfImagesLabel;
    private readonly Button m_navigatePreviousImageButton;
    private readonly Button m_navigateNextImageButton;
    private CarouselView? m_carouselView;
    private CancellationTokenSource m_cancellationTokenSource = new();
    private int? m_positionBeforeRemoval;
    private readonly Grid m_grid;

    public GalleryBottomSheet(List<byte[]> images, int startingIndex, Action<int> onRemoveImage)
    {
        Positioning = Positioning.Large;
        IsDraggable = false;

        BackgroundColor = Colors.GetColor(ColorName.color_system_black);
        
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
            BackgroundColor = Colors.GetColor(ColorName.color_neutral_90),
            Stroke = Microsoft.Maui.Graphics.Colors.Transparent,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_4)),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_4))
            }
        };

        var toolbarLayout = new Grid
        {
            ColumnDefinitions = [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star)],
            Padding = new Thickness(Sizes.GetSize(SizeName.size_8), 0, Sizes.GetSize(SizeName.size_8), Sizes.GetSize(SizeName.size_10))
        };
        
        var removeButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.delete_line),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            BackgroundColor = Colors.GetColor(ColorName.color_neutral_90),
            VerticalOptions = LayoutOptions.Center,
            Command = new Command(RemoveImage)
        };

        var removeLabel = new Label
        {
            Text = DUILocalizedStrings.Remove,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            TextColor = Colors.GetColor(ColorName.color_system_white),
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_3), 0, 0)
        };
        
        var doneButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.check_line),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            BackgroundColor = Colors.GetColor(ColorName.color_neutral_90),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            VerticalOptions = LayoutOptions.Center,
            Command = new Command(() => Close())
        };

        var doneLabel = new Label
        {
            Text = DUILocalizedStrings.Done,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            TextColor = Colors.GetColor(ColorName.color_system_white),
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_3), 0, 0)
        };
        
        var leftColumn = new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Start, 
            VerticalOptions = LayoutOptions.Center,
            Children = { removeButton, removeLabel }
        };
        
        var rightColumn = new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.End, 
            VerticalOptions = LayoutOptions.Center,
            Children = { doneButton, doneLabel }
        };
        
        toolbarLayout.Add(leftColumn);
        toolbarLayout.Add(rightColumn, 1);
        
        m_navigatePreviousImageButton = new Button
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
                if(m_carouselView is null || m_carouselView.Position == 0)
                    return;

                m_carouselView.Position -= 1;
            })
        };

        m_navigateNextImageButton = new Button
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
                if(m_carouselView is null || m_carouselView.Position == Images.Count - 1)
                    return;

                m_carouselView.Position += 1;
            })
        };

        m_grid = [];

        m_grid.AddRowDefinition(new RowDefinition(GridLength.Star));
        m_grid.AddRowDefinition(new RowDefinition(GridLength.Auto));
        
        m_grid.Add(m_navigatePreviousImageButton);
        m_grid.Add(m_navigateNextImageButton);
        m_grid.Add(labelWrapper);
        m_grid.Add(toolbarLayout, 0, 1);

        Content = m_grid;

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

        m_carouselView!.PositionChanged -= CarouselViewOnPositionChanged;
        
        m_positionBeforeRemoval = m_carouselView.Position;
        
        var newImages = new List<byte[]>(Images);
        newImages.RemoveAt(m_carouselView.Position);
        Images = newImages;
        
        _ = OnCarouselViewPositionChanged(m_positionBeforeRemoval.Value);
        
        m_onRemoveImage.Invoke(m_carouselView.Position);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnImagesChanged();
        _ = OnCarouselViewPositionChanged(m_carouselView!.Position);
    }

    private void CarouselViewOnPositionChanged(object? sender, PositionChangedEventArgs e)
    {
        m_cancellationTokenSource.Cancel();
        m_cancellationTokenSource = new CancellationTokenSource();
        _ = OnCarouselViewPositionChanged(e.CurrentPosition);
    }

    private async Task OnCarouselViewPositionChanged(int currentPosition)
    {
        try
        {
            // For some reason setting position programatically triggers this event three times
            // and currentPosition is set to its last value 1/3 of the time
            // So we only take the third time into account
            await Task.Delay(50, m_cancellationTokenSource.Token);
            
            if(m_cancellationTokenSource.IsCancellationRequested)
                return;
            
            m_navigatePreviousImageButton.IsVisible = currentPosition != 0;
            m_navigateNextImageButton.IsVisible = currentPosition != Images.Count - 1;
            
            UpdateNumberOfImagesLabel(currentPosition);
        }
        catch
        {
            // We dont give a fak 
        }
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
        
        if(m_positionBeforeRemoval > Images.Count - 1)
            m_positionBeforeRemoval = Images.Count - 1;

        if (m_carouselView is not null)
        {
            m_carouselView.PositionChanged -= CarouselViewOnPositionChanged;
            try
            {
                m_grid.Remove(m_carouselView);
                m_carouselView.Handler?.DisconnectHandler();
            }
            catch
            {
                // ignored
            }
        }
        
        m_carouselView = new CarouselView
        {
            Loop = false,
            ItemTemplate = new DataTemplate(CreateImageView),
            Position = m_positionBeforeRemoval ?? m_startingIndex, 
            ItemsSource = Images
        };
        m_carouselView.PositionChanged += CarouselViewOnPositionChanged;
        m_grid.Insert(0, m_carouselView);
    }

    private void OnStartingIndexChanged()
    {
        if(m_carouselView is not null)
            m_carouselView.Position = StartingIndex;
    }
}