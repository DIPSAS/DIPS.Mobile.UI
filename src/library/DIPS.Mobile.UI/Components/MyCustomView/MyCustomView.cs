namespace DIPS.Mobile.UI.Components.MyCustomView;

public class MyCustomView : View
{
    public static readonly BindableProperty MyStringProperty = BindableProperty.Create(
            nameof(MyString),
            typeof(string),
            typeof(MyCustomView));

    public string MyString
    {
        get => (string)GetValue(MyStringProperty);
        set => SetValue(MyStringProperty, value);
    }

    public void RunSomething() { }
}
