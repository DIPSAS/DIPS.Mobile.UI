using DIPS.Mobile.UI.Components.MyCustomView;
using Microsoft.Maui.Handlers;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.MyCustomView
{
    public partial class MyCustomViewHandler : ViewHandler<MyCustomView, View>
    {
        internal static partial void MapMyString(Components.MyCustomView.MyCustomViewHandler h, MyCustomView b)
        {
            
        }
        
        public static partial void DoSomething(MyCustomViewHandler handler, MyCustomView video, object? args)
        {
            
        }

        protected override View CreatePlatformView()
        {
            return new View(MauiContext.Context);
        }
    }
}

