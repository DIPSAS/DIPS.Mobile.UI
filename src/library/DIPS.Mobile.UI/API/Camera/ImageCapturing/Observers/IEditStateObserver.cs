namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;

internal interface IImageEditStateObserver
{
    void OnSaveButtonTapped();
    void OnCancelButtonTapped();
    Task OnRotateButtonTapped(bool clockwise);
}