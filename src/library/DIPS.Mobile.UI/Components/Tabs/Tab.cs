namespace DIPS.Mobile.UI.Components.Tabs;

public partial class Tab : ContentView
{
    public void SendTapped()
    {
        Command?.Execute(CommandParameter);
        Tapped?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnIsSelectedChanged()
    {
    }
}
