using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Google.Android.Material.Shape;
using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class LayoutPlatformEffect
{
    private Drawable? m_originalBackground;
    private Drawable? m_originalForeground;

    private partial void PlatformOnAttached(CornerRadius? cornerRadius, Color? stroke)
    {
        if (Control == null)
            return;

        m_originalBackground = Control.Background;
        m_originalForeground = Control.Foreground;

        var strokeThickness = Layout.GetStrokeThickness(Element);

        // Background drawable (with corner + fill)
        var backgroundDrawable = MaterialShapeDrawableHelper.CreateDrawable(cornerRadius);
        if (Element is VisualElement ve)
        {
            backgroundDrawable.FillColor = ve.BackgroundColor?.ToDefaultColorStateList()
                                           ?? Colors.Transparent.ToDefaultColorStateList();
        }

        // Foreground drawable (stroke only)
        var strokeDrawable = MaterialShapeDrawableHelper.CreateDrawable(cornerRadius);
        strokeDrawable.FillColor = Colors.Transparent.ToDefaultColorStateList();
        if (stroke != null)
        {
            strokeDrawable.StrokeColor = stroke.ToDefaultColorStateList();
            strokeDrawable.StrokeWidth = (float)strokeThickness;
        }

        // Apply
        Control.Background = backgroundDrawable;
        Control.Foreground = strokeDrawable;

        // Needed for corners to visually clip content
        Control.ClipToOutline = true;
        Control.OutlineProvider = ViewOutlineProvider.Background;
    }

    protected override partial void OnDetached()
    {
        if (Control == null)
            return;

        Control.Background = m_originalBackground;
        Control.Foreground = m_originalForeground;
    }
}