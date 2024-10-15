namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;

internal interface IStreamingStateObserver
{
    void OnTappedShutterButton();
    void OnTappedFlashButton();
    void OnSettingsChanged();
    bool FlashActive { get; }
}