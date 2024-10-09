namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;

internal interface IEditStateObserver
{
    void OnSaveButtonTapped();
    void OnCancelButtonTapped();
    Task OnRotateButtonTapped(bool clockwise);
}