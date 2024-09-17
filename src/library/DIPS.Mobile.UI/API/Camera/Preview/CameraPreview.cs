using DIPS.Mobile.UI.API.Camera.Shared;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview : ContentView
{
    private readonly TaskCompletionSource m_hasLoadedTcs = new();
    private ICameraUseCase? m_cameraUseCase;
    private VerticalStackLayout? m_customViewsContainer;
    private Grid m_grid;

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
        
        m_grid =
        [
            PreviewView,
            m_customViewsContainer
        ];

        return m_grid;
    }
    
    internal PreviewView PreviewView { get; set; }

    public void AddToolbarView(View? toolbarItems)
    {
        m_customViewsContainer?.Add(toolbarItems);
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
#if __ANDROID__
        PlatformGoToConfirmingState();
#endif
        
    }

    public void GoToStreamingState()
    {
        PreviewView.IsVisible = true;
        
#if __ANDROID__
        PlatformGoToStreamingState();
#endif
    }
}