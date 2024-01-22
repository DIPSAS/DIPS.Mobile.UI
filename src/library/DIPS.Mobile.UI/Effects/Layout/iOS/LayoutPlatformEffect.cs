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
        Control.Layer.CornerRadius = Layout.GetUniformCornerRadius(Element);
    }

    protected override partial void OnDetached()
    {
        Control.ClipsToBounds = m_originalClipToBound;
        Control.Layer.CornerRadius = m_prevCornerRadius;
    }
}