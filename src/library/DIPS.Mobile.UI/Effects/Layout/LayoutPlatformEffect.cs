using Microsoft.Maui.Controls.Platform;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class LayoutPlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        var autoCornerRadius = Layout.GetAutoCornerRadius(Element);
        CornerRadius? cornerRadius = Layout.GetCornerRadius(Element);
        var stroke = Layout.GetStroke(Element);

        if (Element is CollectionView collectionView)
        {
            collectionView.AutoCornerRadius = autoCornerRadius ?? false;
        }
        else
        {
            if (autoCornerRadius.HasValue)
            {
                cornerRadius = autoCornerRadius.Value ? Sizes.GetSize(SizeName.size_2) : cornerRadius;
            }
        }
        
        PlatformOnAttached(cornerRadius, stroke);
    }

    private partial void PlatformOnAttached(CornerRadius? cornerRadius, Color? stroke);
    protected override partial void OnDetached();
}