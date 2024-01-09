using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class DefaultView : Label
{
    public DefaultView()
    {
        SetBinding(TextProperty, new Binding(nameof(DefaultViewModel.Title)));
        
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;
    }
}