namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;

internal interface IEditStateObserver
{
    void OnDoneButtonTapped();
    void OnCancelButtonTapped();
    Task OnRotateButtonTapped();
}