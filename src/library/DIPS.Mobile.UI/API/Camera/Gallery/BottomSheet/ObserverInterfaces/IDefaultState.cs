namespace DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet.ObserverInterfaces;

public interface IGalleryDefaultStateObserver
{
    void RemoveImage();
    Task Close(bool animated = true);
}