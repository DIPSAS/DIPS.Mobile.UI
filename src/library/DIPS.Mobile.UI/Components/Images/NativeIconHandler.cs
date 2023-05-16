namespace DIPS.Mobile.UI.Components.Images;

public partial class NativeIconHandler
{
    public NativeIconHandler() : base(PropertyMapper)
    {
        
    }
    
    public static IPropertyMapper<NativeIcon, NativeIconHandler> PropertyMapper = new PropertyMapper<NativeIcon, NativeIconHandler>(ViewMapper)
    {
        [nameof(NativeIcon.AndroidIconResourceName)] = TrySetSystemImage,
        [nameof(NativeIcon.iOSSystemIconName)] = TrySetSystemImage,
        [nameof(NativeIcon.Color)] = TrySetImageColor
    };

    private static partial void TrySetSystemImage(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon);

    private static partial void TrySetImageColor(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon);

}