namespace DIPS.Mobile.UI.Components.Camera;

public partial class CameraHandler
{
    public CameraHandler() : base(PropertyMapper)                                                               
    {
    }

    public static readonly IPropertyMapper<Camera, CameraHandler> PropertyMapper =
        new PropertyMapper<Camera, CameraHandler>(ViewMapper) { };
}