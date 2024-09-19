using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Tip;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Platform;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview : ContentView
{
    private readonly TaskCompletionSource m_hasLoadedTcs = new();
    private ICameraUseCase? m_cameraUseCase;
    private VerticalStackLayout m_customViewsContainer;
    
    private Grid m_grid;
    private CameraZoomView? m_cameraZoomView;
    private Border m_border;

    public CameraPreview()
    {
        BackgroundColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_system_black);
        Loaded += OnLoaded;

#if __IOS__
        Content = ConstructView();
#endif
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
        m_hasLoadedTcs.TrySetResult();
    }
    
    public Grid ConstructView()
    {
        m_customViewsContainer = new VerticalStackLayout
        {
            Padding = new Thickness(Sizes.GetSize(SizeName.size_8), 0, Sizes.GetSize(SizeName.size_8), Sizes.GetSize(SizeName.size_6)),
            BackgroundColor = Colors.Transparent, 
            VerticalOptions = LayoutOptions.End
        };
        
        PreviewView = new PreviewView();

        m_grid = new Grid
        {
            Children = { PreviewView, m_customViewsContainer },
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

    internal void AddFocusIndicator(float percentX, float percentY)
    {
        m_grid.Remove(m_border);
        
        m_border = new Border
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
            Scale = .75f      
        };

        var borderToRemove = m_border;

        m_border.TranslationX = percentX * PreviewView.Width - m_border.WidthRequest / 2;
        m_border.TranslationY = percentY * PreviewView.Height;

#if __IOS__
        m_border.TranslationY -= m_border.HeightRequest / 2;
#endif

        m_border.ScaleTo(1, easing: Easing.SpringOut);
        m_border.FadeTo(1);
        
        m_grid.Add(m_border);

        Task.Run(async () =>
        {
            await Task.Delay(2000);
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                _ = borderToRemove.ScaleTo(.75f);
                await borderToRemove.FadeTo(0);
                m_grid.Remove(borderToRemove);
            });
        });
    }
    
    public void AddToolbarView(View? toolbarItems, bool addAsFirstRow = false)
    {
        if(m_customViewsContainer.Contains(toolbarItems))
            return;
        
        if (addAsFirstRow)
        {
            m_customViewsContainer?.Insert(0, toolbarItems);
        }
        else
        {
            m_customViewsContainer?.Add(toolbarItems);
        }
    }
    
    public void RemoveToolbarView(View? toolbarItems)
    {
        if (toolbarItems == null) return;
        m_customViewsContainer?.Remove(toolbarItems);
    }
    
    public void AddViewToRoot(View view, int row)
    {
        m_grid.Insert(row, view);
    }
    
    public void RemoveViewFromRoot(View? view)
    {
        if (view == null) return;
        m_grid.Remove(view);
    }
    
    public Task HasLoaded()
    {
        return m_hasLoadedTcs.Task;
    }

    internal void AddUseCase(ICameraUseCase cameraUseCase)
    {
        m_cameraUseCase = cameraUseCase;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        if(args.NewHandler == null) //I am removed from the view and will auto-stop
        {
            m_cameraUseCase?.Stop();
            m_cameraUseCase = null;
        }

        base.OnHandlerChanging(args);
    }

    public void GoToConfirmingState()
    {
        PreviewView.IsVisible = false;
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
}