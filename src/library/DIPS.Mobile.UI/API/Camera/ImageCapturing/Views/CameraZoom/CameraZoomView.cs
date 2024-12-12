using DIPS.Mobile.UI.API.Tip;
#if __IOS__
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;
#endif

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;

internal class CameraZoomView : Grid
{
    private CancellationTokenSource m_cancellationTokenSource = new();

    private ZoomType m_zoomState;
    private readonly ZoomButtons m_zoomButtons;
    private readonly ZoomSlider m_zoomSlider;

    public CameraZoomView(float minRatio, float maxRatio, Action<float> onChangedZoomRatio)
    {
        InputTransparent = true;
        CascadeInputTransparent = false;
        
        // We do not support ultra-wide cameras yet, so we can safely assume that the minimum ratio is 1
        minRatio = 1;
        
        VerticalOptions = LayoutOptions.End;

        Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_4));
        
        m_zoomButtons = new ZoomButtons(minRatio, maxRatio, v =>
        {
            onChangedZoomRatio(v);
            m_zoomSlider?.SetZoomRatio(v);
        }, OnPanned);
        m_zoomSlider = new ZoomSlider(minRatio, maxRatio, onChangedZoomRatio, OnPanned);
        
        
        Add(m_zoomSlider);
        Add(m_zoomButtons);
    }

    public void ShowZoomTip(string tip)
    {
#if __IOS__
        _ = TipService.Show(tip, 4000, m_zoomButtons.ToPlatform(DUI.GetCurrentMauiContext!), Platform.GetCurrentUIViewController()!, UIPopoverArrowDirection.Down);
#else
        TipService.Show(tip, m_zoomButtons, 4000);
#endif
    }
    
    private void OnPanned(PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                m_cancellationTokenSource.Cancel();
                m_cancellationTokenSource = new CancellationTokenSource();
                if(ZoomState is not ZoomType.Slidable)
                    ZoomState = ZoomType.Slidable; 
                break;
            case GestureStatus.Completed:
            case GestureStatus.Canceled:
                _ = TryChangeToZoomButtons();
                break;
        }
        
        m_zoomSlider.TranslateZoomSlider(e);
    }
    
    private async Task TryChangeToZoomButtons()
    {
        try
        {
            await Task.Delay(ZoomSlider.DelayUntilFadeOut, m_cancellationTokenSource.Token);
            m_cancellationTokenSource.Token.ThrowIfCancellationRequested();
            ZoomState = ZoomType.Buttons;
            m_zoomButtons.SetZoomRatio((float)m_zoomSlider.ZoomRatioLevel);
        }
        catch
        {
            // ignored
        }
    }

    public async Task OnPinchToZoom(float zoomRatio)
    {
        if(ZoomState is ZoomType.Buttons)
            ZoomState = ZoomType.Slidable;
        
        var wasCancelled = await m_zoomSlider.OnPinchToZoom(zoomRatio);
        if (!wasCancelled)
        {
            ZoomState = ZoomType.Buttons;
            m_zoomButtons.SetZoomRatio(zoomRatio);
        }
    }

    public void SetZoomRatio(float zoomRatio)
    {
        m_zoomButtons.SetZoomRatio(zoomRatio);
        m_zoomSlider.SetZoomRatio(zoomRatio);
    }

    private ZoomType ZoomState
    {
        get => m_zoomState;
        set
        {
            if (ZoomState == value)
                return;
            
            m_zoomState = value;
            if(m_zoomState == ZoomType.Slidable)
            {
                m_zoomButtons.FadeTo(0);
                m_zoomSlider.FadeTo(1);
            }
            else
            {
                m_zoomButtons.FadeTo(1);
                m_zoomSlider.FadeTo(0);
            }
        }
    }
    
    private enum ZoomType
    {
        Buttons,
        Slidable
    }
    
}


