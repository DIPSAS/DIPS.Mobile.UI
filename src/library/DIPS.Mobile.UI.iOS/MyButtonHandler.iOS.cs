using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.Components
{
    public partial class MyButtonHandler : ButtonHandler
    {
        public static partial void MapMyString(MyButtonHandler h, MyButton b)
        {
            
        }

        public static partial void RunSomething(MyButtonHandler h, MyButton b, object args)
        {
            new UIViewController();
        }
    }
}

