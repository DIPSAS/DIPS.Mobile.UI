namespace DIPS.Mobile.UI.Components.PanZoomContainer;

public partial class PanZoomContainerHandler
{
    public PanZoomContainerHandler() : base(PropertyMapper)
    {
    }
    
    public static IPropertyMapper<PanZoomContainer, PanZoomContainerHandler> PropertyMapper = new PropertyMapper<PanZoomContainer, PanZoomContainerHandler>
    {
        [nameof(PanZoomContainer.Source)] = MapContent
    };

    private static void MapContent(PanZoomContainerHandler handler, PanZoomContainer panZoomContainer)
    {
        handler.UpdateContent();
    }
}
