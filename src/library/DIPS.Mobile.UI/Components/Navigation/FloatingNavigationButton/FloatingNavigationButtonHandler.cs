namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

public partial class FloatingNavigationButtonHandler : Microsoft.Maui.Handlers.LayoutHandler
{
    public FloatingNavigationButtonHandler():base(s_propertyMapper)
    {
        
    }
    
    internal static IPropertyMapper<FloatingNavigationButton, FloatingNavigationButtonHandler> s_propertyMapper = new PropertyMapper<FloatingNavigationButton, FloatingNavigationButtonHandler>(ViewMapper)
    {
        [nameof(FloatingNavigationButton.IsClickable)] = MapIsClickable,
    };

    private static partial void MapIsClickable(FloatingNavigationButtonHandler handler, FloatingNavigationButton floatingNavigationButton);
}