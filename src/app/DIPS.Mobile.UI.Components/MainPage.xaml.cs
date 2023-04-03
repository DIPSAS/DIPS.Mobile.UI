namespace DIPS.Mobile.UI.Components;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // private void OnCounterClicked(object sender, EventArgs e)
    // {
    //     count++;
    //
    //     if (count == 1)
    //         CounterBtn.Text = $"Klikket {count} ganger";
    //     else
    //         CounterBtn.Text = $"Klikket {count} ganger";
    //
    //     SemanticScreenReader.Announce(CounterBtn.Text);
    // }
    private void Button_OnClicked(object sender, EventArgs e)
    {
        var myApi = new MyAPI();
        myApi.DoSomething();
    }

    void MyButton_Clicked(System.Object sender, System.EventArgs e)
    {
        if(sender is MyButton myButton)
        {
            myButton.RunSomething();
        }
    }
}

public interface ITest
{

}

public class Test : ITest
{

}