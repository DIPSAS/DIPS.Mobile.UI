namespace DIPS.Mobile.UI.Components;

public class MyButton : Button
{
    public static readonly BindableProperty MyStringProperty = BindableProperty.Create(
            nameof(MyString),
            typeof(string),
            typeof(MyButton));

    public string MyString
    {
        get => (string)GetValue(MyStringProperty);
        set => SetValue(MyStringProperty, value);
    }

    public void RunSomething() { }
}
