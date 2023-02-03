using CoreGraphics;
using DIPS.Mobile.UI.iOS.Components.Toolbars;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DUIToolbar = DIPS.Mobile.UI.Components.Toolbars.Toolbar;
using DUIToolbarRenderer = DIPS.Mobile.UI.iOS.Components.Toolbars.ToolbarRenderer;

[assembly: ExportRenderer(typeof(ToolbarRenderer), typeof(ToolbarRenderer))]
namespace DIPS.Mobile.UI.iOS.Components.Toolbars
{
    public class ToolbarRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is DUIToolbar duiToolbar)
                {
                    var toolbar = new UIToolbar(new CGRect(100,100,100,100));
                    toolbar.LargeContentTitle = "Test";
                    SetNativeControl(toolbar);
                }
            }
        }
    }
}