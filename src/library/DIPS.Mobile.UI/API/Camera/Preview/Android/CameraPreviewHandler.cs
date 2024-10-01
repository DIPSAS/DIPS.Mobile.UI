using System.ComponentModel;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera.Preview;

//Preview: https://developer.android.com/media/camera/camera2/camera-preview
public class CameraPreviewHandler() : ViewHandler<CameraPreview, View>(ViewMapper)
{
    private StatusAndNavigationBarColors? m_statusAndNavigationBarColors;

    protected override View CreatePlatformView()
    {
        return VirtualView.ConstructView().ToPlatform(DUI.GetCurrentMauiContext!);
    }

    protected override void ConnectHandler(View platformView)
    {
        base.ConnectHandler(platformView);
        
        if(!VirtualView.IsInFullscreen)
            return;
        
        VirtualView.PropertyChanged += VirtualViewOnPropertyChanged;
        
        SetStatusBarColor();    
    }

    private void VirtualViewOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == VisualElement.IsVisibleProperty.PropertyName)
        {
           SetStatusBarColor();
        }
    }

    private void SetStatusBarColor()
    {
        if (VirtualView.IsVisible)
        {
            m_statusAndNavigationBarColors = Context.SetStatusAndNavigationBarColor(VirtualView.BackgroundColor);
        }
        else
        {
            if (m_statusAndNavigationBarColors != null)
            {
                Context.ResetStatusAndNavigationBarColor(m_statusAndNavigationBarColors);
            }
        }
    }

    protected override void DisconnectHandler(View platformView)
    {
        VirtualView.PropertyChanged -= VirtualViewOnPropertyChanged;
        
        if (m_statusAndNavigationBarColors != null)
        {
            Context.ResetStatusAndNavigationBarColor(m_statusAndNavigationBarColors);
        }
        
        base.DisconnectHandler(platformView);
    }
}