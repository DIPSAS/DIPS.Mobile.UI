using CoreAnimation;
using CoreGraphics;
using Microsoft.Maui.Platform;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class LayoutPlatformEffect
{
    private bool? m_originalClipToBound;
    private nfloat? m_prevCornerRadius;
    private nfloat? m_originalBorderWidth;
    private CGColor? m_originalBorderColor;

    private partial void PlatformOnAttached(CornerRadius? cornerRadius, Color? stroke)
    {
        m_originalClipToBound = Control.ClipsToBounds;
        m_prevCornerRadius = Control.Layer.CornerRadius;
        m_originalBorderWidth = Control.Layer.BorderWidth;
        m_originalBorderColor = Control.Layer.BorderColor;

        if (cornerRadius is not null)
        {
            var highestCornerRadius = cornerRadius.Value.HighestCornerRadius();

            var maskedCorners = CACornerMaskHelper.GetCACornerMask(cornerRadius.Value);
            
            Control.ClipsToBounds = true;
            Control.Layer.CornerRadius = (nfloat)highestCornerRadius;
            Control.Layer.MaskedCorners = maskedCorners;
        }

        if (stroke is not null)
        {
            Control.Layer.BorderColor = stroke.ToCGColor();
            Control.Layer.BorderWidth = (nfloat)Layout.GetStrokeThickness(Element);
        }
    }

    protected override partial void OnDetached()
    {
        if(Control is null)
            return;

        try
        {
            if (m_originalClipToBound.HasValue && m_prevCornerRadius.HasValue)
            {
                Control.Layer.MaskedCorners = 0;
                Control.ClipsToBounds = m_originalClipToBound.Value;
                Control.Layer.CornerRadius = m_prevCornerRadius.Value;
            }

            if (m_originalBorderWidth.HasValue && m_originalBorderColor is not null)
            {
                Control.Layer.BorderWidth = m_originalBorderWidth.Value;
                Control.Layer.BorderColor = m_originalBorderColor;
            }
        }
        catch
        {
            // I believe this can happen if Layer is null
            // We can safely swallow this, as the Control is no longer visible anyway when Layer is null
            // The idea here was to reset the CornerRadius if consumer removes the effect while the Control is still visible
        }
    }
}