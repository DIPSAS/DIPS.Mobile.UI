using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.MyCustomView
{
    public partial class MyCustomViewHandler
    {
        public MyCustomViewHandler():base(PropertyMapper)
        {
        }

        public static IPropertyMapper<MyCustomView, MyCustomViewHandler> PropertyMapper = new PropertyMapper<MyCustomView, MyCustomViewHandler>(ButtonHandler.ViewMapper)
        {
            [nameof(MyCustomView.MyString)] = MapMyString
        };

        internal static partial void MapMyString(MyCustomViewHandler h, MyCustomView b);
    }
}

