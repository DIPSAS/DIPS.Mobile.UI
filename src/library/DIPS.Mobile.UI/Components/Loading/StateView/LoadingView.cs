namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class LoadingView : ActivityIndicator
{
    public LoadingView()
    {
        IsRunning = true;
        
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;
    }
}