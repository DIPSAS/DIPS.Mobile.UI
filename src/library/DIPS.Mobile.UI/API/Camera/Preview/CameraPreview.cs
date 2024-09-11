using DIPS.Mobile.UI.API.Camera.ImageCapturing;
#if __ANDROID__
using DIPS.Mobile.UI.API.Camera.Preview.Android.PreviewView;
using DIPS.Mobile.UI.API.Library;
#endif
using DIPS.Mobile.UI.API.Camera.Shared;
using Microsoft.Maui.Platform;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview : ContentView
{
    private readonly TaskCompletionSource m_hasLoadedTcs = new();
    private ICameraUseCase? m_cameraUseCase;
    private readonly VerticalStackLayout m_customViewsContainer;
    private readonly Grid m_grid;

    public CameraPreview()
    {
#if __ANDROID__
        
        m_customViewsContainer = new VerticalStackLayout
        {
            Padding = new Thickness(25, 10),
            BackgroundColor = Colors.Transparent, 
            VerticalOptions = LayoutOptions.End
        };

        var previewView = new PreviewView();
        previewView.HandlerChanged += PreviewViewOnHandlerChanged;
        PreviewView = (AndroidX.Camera.View.PreviewView?)previewView.ToPlatform(DUI.GetCurrentMauiContext!);
        
        m_grid =
        [
            previewView,
            m_customViewsContainer
        ];
#endif
        Content = m_grid;
    }

    private void PreviewViewOnHandlerChanged(object? sender, EventArgs e)
    {
        m_hasLoadedTcs.TrySetResult();
    }

#if __ANDROID__
    internal AndroidX.Camera.View.PreviewView? PreviewView { get; }
#endif
    
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