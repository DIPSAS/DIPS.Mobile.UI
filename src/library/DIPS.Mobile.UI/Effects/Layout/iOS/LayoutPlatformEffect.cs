using CoreAnimation;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class LayoutPlatformEffect
{
    private bool m_originalClipToBound;
    private nfloat m_prevCornerRadius;
    
    private partial void PlatformOnAttached(CornerRadius cornerRadius)
    {
        m_originalClipToBound = Control.ClipsToBounds;
        m_prevCornerRadius = Control.Layer.CornerRadius;
        
        var highestCornerRadius = cornerRadius.HighestCornerRadius();

        var maskedCorners = CACornerMaskHelper.GetCACornerMask(cornerRadius);
        
        Control.ClipsToBounds = true;
        Control.Layer.CornerRadius = (nfloat)highestCornerRadius;
        Control.Layer.MaskedCorners = maskedCorners;
    }

    protected override partial void OnDetached()
    {
        if(Control is null)
            return;

        try
        {
            Control.ClipsToBounds = m_originalClipToBound;
            Control.Layer.CornerRadius = m_prevCornerRadius;
            Control.Layer.MaskedCorners = 0;
        }
        catch
        {
            // I believe this can happen if Layer is null
            // We can safely swallow this, as the Control is no longer visible anyway when Layer is null
            // The idea here was to reset the CornerRadius if consumer removes the effect while the Control is still visible
        }
    }
}