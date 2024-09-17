namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;

internal class CameraZoomView : Grid
{
    private readonly Action<float> m_onChangedZoomRatio;
    private ZoomType m_zoomState;
    private readonly ZoomButtons m_horizontalZoomButtons;
    private readonly ZoomSlider m_zoomSlider;

    public CameraZoomView(float minRatio, float maxRatio, Action<float> onChangedZoomRatio)
    {
        m_onChangedZoomRatio = onChangedZoomRatio;
        
        var hasWideLens = Math.Abs((int)minRatio - minRatio) > 0.001f;
        m_horizontalZoomButtons = new ZoomButtons(minRatio, maxRatio, hasWideLens, v =>
        {
            ZoomState = ZoomType.Buttons;
            OnChangedZoomRatio(v);
        }, OnPannedZoomButton);
        m_zoomSlider = new ZoomSlider(minRatio, maxRatio, hasWideLens, v =>
        {
            ZoomState = ZoomType.Slidable;
            OnChangedZoomRatio(v);
        }, 
        state =>
        {
            ZoomState = state ? ZoomType.Slidable : ZoomType.Buttons;
        })
        {
            Opacity = 0
        };
        
        Add(m_zoomSlider);
        Add(m_horizontalZoomButtons);
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
        else
        {
            m_horizontalZoomButtons.SetZoomRatio(zoomRatio);
        }
    }


    private ZoomType ZoomState
    {
        get => m_zoomState;
        set
        {
            m_zoomState = value;
            if(m_zoomState == ZoomType.Slidable)
            {
                m_horizontalZoomButtons.FadeTo(0);
                m_zoomSlider.FadeTo(1);
            }
            else
            {
                m_horizontalZoomButtons.FadeTo(1);
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


