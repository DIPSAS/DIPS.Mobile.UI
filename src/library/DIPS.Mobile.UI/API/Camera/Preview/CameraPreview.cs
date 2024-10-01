using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Tip;
using DIPS.Mobile.UI.MemoryManagement;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Platform;
#if __IOS__
using UIKit;
#endif
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;

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

    private const float ThreeFourRatio = .75f;
    
    public CameraPreview()
    {
        BackgroundColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_system_black);
        Loaded += OnLoaded;

#if __IOS__
        Content = ConstructView();
        
        var safeAreaInsetsBottom = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom;
        Padding = new Thickness(0, 0, 0, safeAreaInsetsBottom);
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
            BackgroundColor = Colors.Black,
        };
        
        m_topToolbarContainer = new Grid
        {
            VerticalOptions = LayoutOptions.Start, 
            BackgroundColor = Colors.Black,
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
        var actualPreviewHeight = (Width / ThreeFourRatio);
        var letterBoxHeight = (frameHeight - actualPreviewHeight) / 2;

        Console.WriteLine($"frameHeight: {frameHeight}, Height:{Height}, Width:{Width} letterBoxHeight: {letterBoxHeight} actualPreviewHeight {actualPreviewHeight}");
        m_topToolbarContainer.HeightRequest = letterBoxHeight;
        m_bottomToolbarContainer.HeightRequest = letterBoxHeight;
        
        CameraZoomView!.Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_2) + m_bottomToolbarContainer.HeightRequest);
    }
    
    internal void AddFocusIndicator(float percentX, float percentY)
    {
        m_grid.Remove(m_indicator);
        
        m_indicator = new Border
        {
            WidthRequest = Sizes.GetSize(SizeName.size_17),
            HeightRequest = Sizes.GetSize(SizeName.size_17),
            BackgroundColor = Colors.Transparent,
            StrokeThickness = 2,
            StrokeShape = new Ellipse(),
            Stroke = Colors.White,
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Start,
            Opacity = .75f,
            Scale = .75f,
            InputTransparent = true
        };

        var borderToRemove = m_indicator;

        m_indicator.TranslationX = percentX * PreviewView.Width - m_indicator.WidthRequest / 2;
        m_indicator.TranslationY = percentY * PreviewView.Height;

#if __IOS__
        m_indicator.TranslationY -= m_indicator.HeightRequest / 2;
#else
        m_indicator.TranslationY -= m_indicator.HeightRequest / 1.5f;
#endif

        m_indicator.ScaleTo(1, easing: Easing.SpringOut);
        m_indicator.FadeTo(1);
        
        m_grid.Add(m_indicator);

        Task.Run(async () =>
        {
            await Task.Delay(2000);
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                _ = borderToRemove.ScaleTo(.75f);
                await borderToRemove.FadeTo(0);
                m_grid.Remove(borderToRemove);
                new VisualTreeMemoryResolver().TryResolveMemoryLeakCascading(borderToRemove);
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
        new VisualTreeMemoryResolver().TryResolveMemoryLeakCascading(toolbarItems);
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
        new VisualTreeMemoryResolver().TryResolveMemoryLeakCascading(toolbarItems);
    }
    
    internal void AddViewToRoot(View view, int index = -1)
    {
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
        if (m_grid.Remove(view))
        {
            new VisualTreeMemoryResolver().TryResolveMemoryLeakCascading(view!);
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
            if (m_cameraUseCase.TryGetTarget(out var target))
            {
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

        m_grid.Remove(m_indicator);
        new VisualTreeMemoryResolver().TryResolveMemoryLeakCascading(m_indicator);
    }

    public void GoToStreamingState()
    {
        PreviewView.IsVisible = true;
    }

    public void ShowZoomSliderTip(string tip)
    {
#if __IOS__
        _ = TipService.Show(tip, 4000, CameraZoomView?.ToPlatform(DUI.GetCurrentMauiContext!)!, Platform.GetCurrentUIViewController()!);
#else
        TipService.Show(tip, CameraZoomView!, 4000);
#endif
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == nameof(IsVisibleProperty))
        {
            if (IsVisible)
            {
                
            }
        }
    }
}