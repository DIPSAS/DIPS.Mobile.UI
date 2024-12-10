using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class DefaultView : Label
{
    public DefaultView()
    {
        this.SetBinding(TextProperty, static (DefaultViewModel defaultViewModel) => defaultViewModel.Title);
        
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;
    }
}