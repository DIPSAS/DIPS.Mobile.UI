using DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet.BottomToolbar;
using DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet.ObserverInterfaces;
using DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet.TopToolbar;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.BottomSheets.Header;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;

#if __IOS__
using UIKit;
#endif
#if __ANDROID__
using DIPS.Mobile.UI.Extensions.Android;
#endif


using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet;

internal partial class GalleryBottomSheet : Components.BottomSheets.BottomSheet, IGalleryDefaultStateObserver, IImageEditStateObserver
{
    private readonly Action<int> m_onRemoveImage;
    private readonly Action m_updateImages;
    private readonly Button m_navigatePreviousImageButton;
    private readonly Button m_navigateNextImageButton;
    private CarouselView? m_carouselView;
    private readonly ContentView m_carouselViewWrapperView = new();
    private CancellationTokenSource m_cancellationTokenSource = new();
    private int? m_positionBeforeRemoval;
    private readonly Grid m_grid;

    private readonly GalleryBottomSheetTopToolbar m_topToolbar;
    private readonly GalleryBottomSheetBottomToolbar m_bottomToolbar;
    
    private CapturedImage m_currentlyRotatedCaptureImageDisplayed;
    private CapturedImage m_currentlyCapturedImageDisplayed;
    private Image m_currentlyRotatedImageDisplayed;
    private TaskCompletionSource? m_rotatingImageTcs;
    
    private double? m_startingImageWidth;
    private double m_startingImageHeight;
    private bool m_hasSetToolbarHeights;

    public GalleryBottomSheet(List<CapturedImage> images, int startingIndex, Action<int> onRemoveImage, Action updateImages)
    {
        Positioning = Positioning.Large;
        IsDraggable = false;
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Black;

        BottomSheetHeaderBehavior = new BottomSheetHeaderBehavior { IsVisible = false };
        
#if __IOS__
        Padding = new Thickness(0, UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Top, 0, UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom);
#elif __ANDROID__
        Padding = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.content_margin_small));
#endif
        
        m_onRemoveImage = onRemoveImage;
        m_updateImages = updateImages;

        var fadedBlackColor = Microsoft.Maui.Graphics.Colors.Black.WithAlpha(.5f);
        
        m_topToolbar = new GalleryBottomSheetTopToolbar(() => new CapturedImageInfoBottomSheet(Images[m_carouselView?.Position ?? 0]).Open(), GoToEditState)
        {
            VerticalOptions = LayoutOptions.Start
        };

        m_bottomToolbar = new GalleryBottomSheetBottomToolbar
        {
            VerticalOptions = LayoutOptions.End
        };
        m_bottomToolbar.GoToDefaultState(this);
        
        m_navigatePreviousImageButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.chevron_left_line),
            ImageTintColor = Colors.GetColor(ColorName.color_icon_on_fill_inverted),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonSmall),
            BackgroundColor = fadedBlackColor,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Start,
            Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_large), 0, 0, 0),
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
            ImageTintColor = Colors.GetColor(ColorName.color_icon_on_fill_inverted),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonSmall),
            BackgroundColor = fadedBlackColor,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.content_margin_large), 0),
            Command = new Command(() =>
            {
                if(m_carouselView is null || m_carouselView.Position == Images.Count - 1)
                    return;

                m_carouselView.Position += 1;
            })
        };

        m_grid =
        [
            m_topToolbar,
            m_navigatePreviousImageButton,
            m_navigateNextImageButton,
            m_bottomToolbar
        ];

        Content = m_grid;

        Images = images;
        StartingIndex = startingIndex;
    }

    private void GoToEditState()
    {
        m_topToolbar.GoToEditState();
        m_bottomToolbar.GoToEditState(this);
        
        m_currentlyRotatedCaptureImageDisplayed = Images[m_carouselView!.Position];
        m_currentlyRotatedImageDisplayed = new Image
        {
            Source = ImageSource.FromStream(() => new MemoryStream(m_currentlyRotatedCaptureImageDisplayed.AsByteArray))
        };

        m_currentlyRotatedImageDisplayed.TranslationY -= m_topToolbar.HeightRequest;

        m_currentlyRotatedImageDisplayed.SizeChanged += delegate
        {
            if (m_startingImageWidth is not null)
                return;
            
            m_startingImageWidth = m_currentlyRotatedImageDisplayed.Width;
            m_startingImageHeight = m_currentlyRotatedImageDisplayed.Height;
            
            m_carouselView.Opacity = 0;
            m_navigatePreviousImageButton.IsVisible = false;
            m_navigateNextImageButton.IsVisible = false;
        };
        
        m_rotatingImageTcs = null;
        
        m_grid.Insert(0, m_currentlyRotatedImageDisplayed);
    }

    private void GoToDefaultState()
    {
        m_topToolbar.GoToDefaultState();
        m_bottomToolbar.GoToDefaultState(this);
        
        m_grid.Remove(m_currentlyRotatedImageDisplayed);
        UpdateNavigationButtonsVisibility(m_carouselView.Position);
        OnImagesChanged();
    }

    async void IGalleryDefaultStateObserver.RemoveImage()
    {
        if(Images.Count == 0)
            return;

        var dialogResult = await DialogService.ShowDestructiveConfirmationMessage(DUILocalizedStrings.RemoveImageTitle,
            DUILocalizedStrings.RemoveImageDescription, DUILocalizedStrings.Close, DUILocalizedStrings.Remove);
        
        if(dialogResult == DialogAction.Closed)
            return;

        m_carouselView!.PositionChanged -= CarouselViewOnPositionChanged;
        
        m_positionBeforeRemoval = m_carouselView.Position;
        
        var newImages = new List<CapturedImage>(Images);
        newImages.RemoveAt(m_carouselView.Position);
        Images = newImages;
        
        _ = OnCarouselViewPositionChanged(m_positionBeforeRemoval.Value);
        
        m_onRemoveImage.Invoke(m_carouselView.Position);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
        {
            OnImagesChanged();
            _ = OnCarouselViewPositionChanged(m_carouselView!.Position);    
        }
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
            
            UpdateNavigationButtonsVisibility(currentPosition);
            m_currentlyCapturedImageDisplayed = Images[currentPosition];
            
            UpdateNumberOfImagesLabel(currentPosition);
        }
        catch
        {
            // We dont give a fak 
        }
    }

    private void UpdateNavigationButtonsVisibility(int currentPosition)
    {
        m_navigatePreviousImageButton.IsVisible = currentPosition != 0;
        m_navigateNextImageButton.IsVisible = currentPosition != Images.Count - 1;
    }

    private void UpdateNumberOfImagesLabel(int position)
    {
        var text = Images.Count > 0 ? $"{position + 1}/{Images.Count}" : string.Empty;
        
        m_topToolbar.UpdateNumberOfImagesLabel(text);
    }

    private static Image CreateImageView()
    {
        var image = new Image { VerticalOptions = LayoutOptions.Center };
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
                m_grid.Remove(m_carouselViewWrapperView);
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
            ItemsSource = Images.Select(i => i.AsByteArray)
        };
        m_carouselViewWrapperView.Content = m_carouselView;
        
        m_carouselView.SizeChanged += delegate
        {
            if (m_hasSetToolbarHeights)
            {
                return;
            }
            
            var actualImageHeight = m_carouselView.Width / CameraPreview.ThreeFourRatio;
            var letterBoxHeight = m_carouselView.Height - actualImageHeight;

            m_topToolbar.HeightRequest = letterBoxHeight * CameraPreview.TopToolbarPercentHeightOfLetterBox;
            m_bottomToolbar.HeightRequest = letterBoxHeight * CameraPreview.BottomToolbarPercentHeightOfLetterBox;
            
            m_carouselViewWrapperView.TranslationY -= m_topToolbar.HeightRequest;
            m_navigatePreviousImageButton.TranslationY -= m_topToolbar.HeightRequest;
            m_navigateNextImageButton.TranslationY -= m_topToolbar.HeightRequest;

            m_hasSetToolbarHeights = true;
        };
        
        m_carouselView.PositionChanged += CarouselViewOnPositionChanged;
        m_grid.Insert(0, m_carouselViewWrapperView);
    }

    private void OnStartingIndexChanged()
    {
        if(m_carouselView is not null)
            m_carouselView.Position = StartingIndex;
    }

#if __ANDROID__
    private StatusAndNavigationBarColors? m_statusAndNavigationBarColors;
    protected override void OnOpened()
    {
        if (Handler is BottomSheetHandler handler)
        {
            m_statusAndNavigationBarColors = handler.Context.SetStatusAndNavigationBarColor(BackgroundColor);
        }

        base.OnOpened();
    }

    protected override void OnClosed()
    {
        if (Handler is BottomSheetHandler handler && m_statusAndNavigationBarColors != null)
        {
            handler.Context.ResetStatusAndNavigationBarColor(m_statusAndNavigationBarColors);
        }
        base.OnClosed();
    }
#endif
    void IImageEditStateObserver.OnSaveButtonTapped()
    {
        if(!m_rotatingImageTcs?.Task.IsCompleted ?? false)
            return;
        
        m_startingImageWidth = null;
        Images[m_carouselView!.Position] = m_currentlyRotatedCaptureImageDisplayed;
        m_currentlyCapturedImageDisplayed = m_currentlyRotatedCaptureImageDisplayed;
        GoToDefaultState();   
        m_updateImages.Invoke();
    }

    void IImageEditStateObserver.OnCancelButtonTapped()
    {
        m_startingImageWidth = null;
        GoToDefaultState();
    }

    async Task IImageEditStateObserver.OnRotateButtonTapped(bool clockwise)
    {
        if((!m_rotatingImageTcs?.Task.IsCompleted ?? false) || m_startingImageWidth is null)
            return;

        m_rotatingImageTcs = new TaskCompletionSource();
            
        await Task.WhenAll(CapturedImage.RotateImage(clockwise, m_currentlyRotatedImageDisplayed, m_currentlyRotatedCaptureImageDisplayed, m_startingImageWidth.Value, m_startingImageHeight, m_currentlyCapturedImageDisplayed.Transformation.OrientationDegree), Task.Run(async () =>
        {
            // Run on background thread, cuz this is heavy shit
            m_currentlyRotatedCaptureImageDisplayed = await m_currentlyRotatedCaptureImageDisplayed.Rotate(clockwise);
        }));
            
        m_rotatingImageTcs.SetResult();   
    }
}