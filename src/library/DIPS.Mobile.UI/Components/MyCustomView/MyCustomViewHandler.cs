using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.MyCustomView
{
    public partial class MyCustomViewHandler
    {
        public MyCustomViewHandler():base(PropertyMapper, CommandMapper)
        {
        }

       public static IPropertyMapper<MyCustomView, MyCustomViewHandler> PropertyMapper = new PropertyMapper<MyCustomView, MyCustomViewHandler>(ViewMapper)
        {
            [nameof(MyCustomView.MyString)] = MapMyString
        };
       
       public static CommandMapper<MyCustomView, MyCustomViewHandler> CommandMapper = new (ViewCommandMapper)
       {
           [nameof(MyCustomView.DoSomething)] = DoSomething
       };

       public static partial void DoSomething(MyCustomViewHandler handler, MyCustomView video, object? args);

       internal static partial void MapMyString(MyCustomViewHandler h, MyCustomView b);
    }
}

