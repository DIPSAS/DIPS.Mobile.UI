using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components
{
    public partial class MyButtonHandler : ButtonHandler
    {
        public MyButtonHandler():base(PropertyMapper, MyButtonCommandMapper)
        {

        }

        public static IPropertyMapper<MyButton, MyButtonHandler> PropertyMapper = new PropertyMapper<MyButton, MyButtonHandler>(ButtonHandler.ViewMapper)
        {
            [nameof(MyButton.MyString)] = MapMyString
        };

        internal static partial void MapMyString(MyButtonHandler h, MyButton b);

        public static CommandMapper<MyButton, MyButtonHandler> MyButtonCommandMapper = new CommandMapper<MyButton, MyButtonHandler>(ButtonHandler.CommandMapper)
        {
            [nameof(MyButton.RunSomething)] = RunSomething
        };
        internal static partial void RunSomething(MyButtonHandler h, MyButton b, object args);
    }
}

