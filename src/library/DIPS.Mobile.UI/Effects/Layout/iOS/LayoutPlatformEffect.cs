using System;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class LayoutPlatformEffect
{
    private bool m_originalClipToBound;
    private nfloat m_prevCornerRadius;
    
    protected override partial void OnAttached()
    {
        m_originalClipToBound = Control.ClipsToBounds;
        m_prevCornerRadius = Control.Layer.CornerRadius;
        
        Control.ClipsToBounds = true;
        Control.Layer.CornerRadius = (nfloat)Layout.GetCornerRadius(Element).TopLeft;
    }

    protected override partial void OnDetached()
    {
        if(Control is null)
            return;

        try
        {
            Control.ClipsToBounds = m_originalClipToBound;
            Control.Layer.CornerRadius = m_prevCornerRadius;
        }
        catch
        {
            // I believe this can happen if Layer is null
            // We can safely swallow this, as the Control is no longer visible anyway when Layer is null
            // The idea here was to reset the CornerRadius if consumer removes the effect while the Control is still visible
        }
    }
}