using Microsoft.Maui.Controls.Platform;

namespace DIPS.Mobile.UI.Effects.Touch;

public partial class TouchPlatformEffect : PlatformEffect
{
    internal static readonly Color ColorToAnimateTo =
        DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_neutral_40);
    protected override partial void OnAttached();
    protected override partial void OnDetached();
}