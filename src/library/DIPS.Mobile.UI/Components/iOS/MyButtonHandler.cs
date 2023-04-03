using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.Components
{
    public partial class MyButtonHandler : ButtonHandler
    {
        internal static partial void MapMyString(MyButtonHandler h, MyButton b)
        {
                
        }

        internal static partial void RunSomething(MyButtonHandler h, MyButton b, object args)
        {
            new UIViewController();
        }
    }
}

