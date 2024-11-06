using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.MemoryManagement;
using Microsoft.Maui.Controls.Shapes;
#if __IOS__
using Microsoft.Maui.Handlers;
using UIKit;
#endif
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview : ContentView
{
    private readonly TaskCompletionSource m_hasLoadedTcs = new();
    private WeakReference<ICameraUseCase?> m_cameraUseCase;
    
    private Grid m_grid;
    private Grid m_bottomToolbarContainer;
    private Grid m_topToolbarContainer;
    private CameraZoomView? m_cameraZoomView;
    private Border? m_indicator;
    private Grid m_indicatorWrapper;
    private bool m_hasSetToolbarHeights;

    internal const float ThreeFourRatio = .75f;
    internal const float TopToolbarPercentHeightOfLetterBox = .25f;
    internal const float BottomToolbarPercentHeightOfLetterBox = .75f;
    
    public CameraPreview()
    {
        BackgroundColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_system_black);
        Loaded += OnLoaded;

#if __IOS__
        Content = ConstructView();

        if (UIApplication.SharedApplication.KeyWindow != null)
        {
            Padding = new Thickness(0, UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Top, 0,
                UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom);
        }
#endif
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
        m_hasLoadedTcs.TrySetResult();
    }
    
    public Grid ConstructView()
    {
        m_bottomToolbarContainer = new Grid
        {
            VerticalOptions = LayoutOptions.End, 
            BackgroundColor = Colors.Transparent,
        };
        
        m_topToolbarContainer = new Grid
        {
            VerticalOptions = LayoutOptions.Start, 
            BackgroundColor = Colors.Transparent,
        };

        PreviewView = new PreviewView();

        m_grid = new Grid
        {
            Children = { PreviewView, m_bottomToolbarContainer, m_topToolbarContainer },
            ColumnDefinitions = [new ColumnDefinition(GridLength.Star)]
        };
        
        return m_grid;
    }

    internal PreviewView PreviewView { get; set; }

    internal CameraZoomView? CameraZoomView
    {
        get => m_cameraZoomView;
        set
        {
            if(CameraZoomView is not null)
                return;
            
            m_cameraZoomView = value;
            m_grid.Add(value);
        }
    }

    /// <summary>
    /// Here we set the height of the top and bottom toolbar relative to the <see cref="ThreeFourRatio"/>
    /// </summary>
    /// <param name="frameHeight"></param>
    internal void SetToolbarHeights(float frameHeight)
    {
        if (m_hasSetToolbarHeights)
        {
            return;
        }
        
        var actualPreviewHeight = (Width / ThreeFourRatio);
        var totalLetterBoxHeight = frameHeight - actualPreviewHeight;

        // Looks like the top toolbar is about 25% of the total letterbox height looking at android/ios' native camera app
        m_topToolbarContainer.HeightRequest = totalLetterBoxHeight * TopToolbarPercentHeightOfLetterBox;
        m_bottomToolbarContainer.HeightRequest = totalLetterBoxHeight * BottomToolbarPercentHeightOfLetterBox;

        CameraZoomView!.Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_2) + m_bottomToolbarContainer.HeightRequest);
        PreviewView.TranslationY -= m_topToolbarContainer.HeightRequest;
        
        m_hasSetToolbarHeights = true;
    }
    
    internal void AddFocusIndicator(float percentX, float percentY)
    {
        m_grid.Remove(m_indicatorWrapper);
        
        m_indicator = new Border
        {
            WidthRequest = Sizes.GetSize(SizeName.size_17),
            HeightRequest = Sizes.GetSize(SizeName.size_17),
            BackgroundColor = Colors.Transparent,
            StrokeThickness = 2,
            StrokeShape = new Ellipse(),
            Stroke = Colors.White,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Opacity = .75f,
            Scale = .75f,
            InputTransparent = true
        };

        m_indicatorWrapper = new Grid
        {
            Children = { m_indicator },
            InputTransparent = true,
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Start
        };
        
        var viewToRemove = m_indicatorWrapper;
        var borderToRemoveAnimate = m_indicator;

        m_indicatorWrapper.TranslationX = percentX * PreviewView.Width - m_indicator.WidthRequest / 2;
        m_indicatorWrapper.TranslationY = percentY * PreviewView.Height;

#if __IOS__
        m_indicatorWrapper.TranslationY -= (m_indicator.HeightRequest / 2) - PreviewView.TranslationY;
#else
        m_indicatorWrapper.TranslationY -= m_indicator.HeightRequest / 1.25f;
#endif
        
        m_indicator.ScaleTo(1, easing: Easing.SpringOut);
        m_indicator.FadeTo(1);
        
        m_grid.Add(m_indicatorWrapper);

        Task.Run(async () =>
        {
            await Task.Delay(2000);
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (borderToRemoveAnimate is not null)
                {
                    _ = borderToRemoveAnimate.ScaleTo(.75f);
                    await borderToRemoveAnimate.FadeTo(0);
                }
                m_grid.Remove(viewToRemove);
                viewToRemove.DisconnectHandlers();
            });
        });
    }

    internal void AddTopToolbarView(View? toolbarItems)
    {
        if(m_topToolbarContainer.Contains(toolbarItems))
            return;
        
        m_topToolbarContainer.Add(toolbarItems);
    }
    
    internal void RemoveTopToolbarView(View? toolbarItems)
    {
        if (toolbarItems is null) 
            return;
        
        m_topToolbarContainer.Remove(toolbarItems);
        toolbarItems.DisconnectHandlers();
    }

    internal void AddBottomToolbarView(View? toolbarItems)
    {
        if(m_bottomToolbarContainer.Contains(toolbarItems))
            return;
        
        m_bottomToolbarContainer.Add(toolbarItems);
    }
    
    internal void RemoveBottomToolbarView(View? toolbarItems)
    {
        if (toolbarItems is null) 
            return;
        
        m_bottomToolbarContainer.Remove(toolbarItems);
        toolbarItems.DisconnectHandlers();
    }
    
    internal void AddViewToRoot(View view, int index = -1, bool usePreviewViewTranslation = false)
    {
        if (usePreviewViewTranslation)
        {
            view.TranslationY = PreviewView.TranslationY;
        }
        
        if (index == -1)
        {
            m_grid.Add(view);
        }
        else
        {
            m_grid.Insert(index, view);
        }
    }
    
    public void RemoveViewFromRoot(View? view)
    {
        if(view is null)
            return;
        
        if (m_grid.Remove(view))
        {
            view.DisconnectHandlers();
        }
    }
    
    public Task HasLoaded()
    {
        return m_hasLoadedTcs.Task;
    }

    internal void AddUseCase(ICameraUseCase cameraUseCase)
    {
        m_cameraUseCase = new WeakReference<ICameraUseCase?>(cameraUseCase);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        if(args.NewHandler == null) // User has navigated from the page
        {
#if __ANDROID__
            // On Android, the view is constructed in the handler, so the automatic leak resolver can not access the content of this view.
            new VisualTreeMemoryResolver().TryResolveMemoryLeakCascading(m_grid);
#endif
            if (m_cameraUseCase.TryGetTarget(out var target))
            {
                CameraZoomView?.DisconnectHandlers();
                var collectionContentTarget = target.ToCollectionContentTarget();
                _ = GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(collectionContentTarget);
                target.StopAndDispose();

            }
        }

        base.OnHandlerChanging(args);
    }

    public void GoToConfirmingState()
    {
        PreviewView.IsVisible = false;

        if (m_indicator is not null)
        {
            if (m_grid.Remove(m_indicator))
            {
                m_indicator.DisconnectHandlers();
            }
        }
    }

    public void GoToStreamingState()
    {
        PreviewView.IsVisible = true;
        if (CameraZoomView is not null)
        {
            CameraZoomView.Opacity = 1;
        }
    }

    public void ShowZoomSliderTip(string tip)
    {
        CameraZoomView?.ShowZoomTip(tip);
    }
}