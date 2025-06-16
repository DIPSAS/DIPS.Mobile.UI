using Android.Content;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Dividers;

internal partial class DividerHandler
{
    protected override MauiShapeView CreatePlatformView()
    {
        return new DividerShapeView(Context) { WeakVirtualView = new WeakReference<Divider>((Divider)VirtualView) };
    }
}

internal class DividerShapeView : MauiShapeView
{
    public DividerShapeView(Context? context) : base(context)
    {
    }
    
    public WeakReference<Divider>? WeakVirtualView { private get; init; }
    
    public Divider? VirtualView
    {
        get
        {
            if (WeakVirtualView?.TryGetTarget(out var target) ?? false)
                return target;

            return null;
        }
    }
}