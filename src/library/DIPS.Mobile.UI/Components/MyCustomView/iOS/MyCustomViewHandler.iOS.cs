using Microsoft.Maui.Handlers;
using UIKit;

namespace  DIPS.Mobile.UI.Components.MyCustomView
{
    public partial class MyCustomViewHandler : ViewHandler<MyCustomView, UIView>
    {
        internal static partial void MapMyString(MyCustomViewHandler h, MyCustomView b)
        {
            if (h.PlatformView is UILabel label)
            {
                label.Text = b.MyString;
            }
        }

        protected override UIView CreatePlatformView()
        {
            return new UILabel();
        }
    }
}

