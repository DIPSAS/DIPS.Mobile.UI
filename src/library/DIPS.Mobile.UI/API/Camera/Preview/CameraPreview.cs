using DIPS.Mobile.UI.API.Camera.Shared;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview : ContentView
{
    private readonly TaskCompletionSource m_hasLoadedTcs = new();
    private ICameraUseCase? m_cameraUseCase;
    private VerticalStackLayout m_customViewsContainer;
    private Grid m_grid;

    public CameraPreview()
    {
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
            Padding = new Thickness(25, 10),
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

    public void AddView(View toolbarItems)
    {
        toolbarItems.VerticalOptions = LayoutOptions.Center;
        
        m_customViewsContainer.Add(toolbarItems);
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
        if(args.NewHandler == null) //I am not needed anymore
        {
            m_cameraUseCase?.Stop();
            m_cameraUseCase = null;
        }

        base.OnHandlerChanging(args);
    }
}