using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.ContextMenus.iOS;

internal class ContextMenuPreviewViewController : UIViewController
{
    private readonly View m_previewView;
    
    internal ContextMenuPreviewViewController(View previewView)
    {
        m_previewView = previewView;
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        var mauiContext = DUI.GetCurrentMauiContext;
        if (mauiContext is null || View is null)
            return;

        var nativeView = m_previewView.ToPlatform(mauiContext);
        View.AddSubview(nativeView);

        var maxWidth = UIScreen.MainScreen.Bounds.Width - Sizes.GetSize(SizeName.content_margin_large) * 2;
        var measurement = m_previewView.Measure(maxWidth, double.PositiveInfinity);

        var width = Math.Min(measurement.Width, maxWidth);
        var height = measurement.Height;

        PreferredContentSize = new CGSize(width, height);
        nativeView.Frame = new CGRect(0, 0, width, height);
    }
}
