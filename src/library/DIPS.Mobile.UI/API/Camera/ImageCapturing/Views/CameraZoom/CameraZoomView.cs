namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;

internal class CameraZoomView : Grid
{
    private readonly Action<float> m_onChangedZoomRatio;
    private ZoomType m_zoomState;
    private readonly ZoomButtons m_zoomButtons;
    private readonly ZoomSlider m_zoomSlider;

    public CameraZoomView(float minRatio, float maxRatio, Action<float> onChangedZoomRatio)
    {
        m_onChangedZoomRatio = onChangedZoomRatio;
        
        VerticalOptions = LayoutOptions.End;
        
        m_zoomButtons = new ZoomButtons(minRatio, maxRatio, v =>
        {
            ZoomState = ZoomType.Buttons;
            OnChangedZoomRatio(v);
        }, OnPannedZoomButton);
        m_zoomSlider = new ZoomSlider(minRatio, maxRatio, v =>
            {
                ZoomState = ZoomType.Slidable;
                OnChangedZoomRatio(v);
            },
            state =>
            {
                ZoomState = state ? ZoomType.Slidable : ZoomType.Buttons;
                if (!state)
                {
                    m_zoomButtons.SetZoomRatio((float)m_zoomSlider!.ZoomRatioLevel);
                }
            });
        
        Add(m_zoomSlider);
        Add(m_zoomButtons);
    }

    private void OnPannedZoomButton(PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                ZoomState = ZoomType.Slidable; 
                break;
            case GestureStatus.Completed:
            case GestureStatus.Canceled:
                ZoomState = ZoomType.Buttons;
                break;
        }
        
        m_zoomSlider.TranslateZoomSlider(e);
    }

    private void OnChangedZoomRatio(float zoomRatio)
    {
        m_onChangedZoomRatio(zoomRatio);

        if (ZoomState is ZoomType.Buttons)
        {
            m_zoomSlider.SetZoomRatio(zoomRatio);
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


