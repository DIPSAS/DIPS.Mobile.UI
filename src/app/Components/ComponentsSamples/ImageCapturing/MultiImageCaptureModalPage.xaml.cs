using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;

namespace Components.ComponentsSamples.ImageCapturing;

public partial class MultiImageCaptureModalPage
{
    private readonly bool m_requiresConfirmation;
    private readonly List<CapturedImage> m_capturedImages = [];
    private readonly ImageCapture m_imageCapture = new();
    private bool m_hasStartedCapture;
    private readonly TaskCompletionSource<List<CapturedImage>> m_completion =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    public MultiImageCaptureModalPage(bool requiresConfirmation)
    {
        InitializeComponent();
        m_requiresConfirmation = requiresConfirmation;
    }

    public Task<List<CapturedImage>> CompletionTask => m_completion.Task;

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (m_hasStartedCapture)
            return;
        m_hasStartedCapture = true;

        var cameraOptions = new CameraOptions
        {
            CancelButtonCommand = new Command(OnCancelled)
        };

        var multiImageCaptureOptions = new MultiImageCaptureOptions
        {
            RequiresConfirmationOnEachImage = m_requiresConfirmation,
            FinishedButtonCommand = new Command(OnFinished),
            MaxImageCount = 10,
            OnImageRemoved = OnImageRemoved
        };
        
        await m_imageCapture.StartMultiImageCapture(CameraPreview, OnImageCaptured, OnCameraFailed,
            cameraOptions, multiImageCaptureOptions);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
            return;

        m_imageCapture.Stop();

        // Covers swipe-to-dismiss and Android back button. No-op if a handler already completed the TCS.
        m_completion.TrySetResult([]);
    }

    private void OnImageCaptured(CapturedImage capturedImage)
    {
        m_capturedImages.Add(capturedImage);
    }

    private void OnImageRemoved(CapturedImage capturedImage)
    {
        m_capturedImages.Remove(capturedImage);
    }

    private async void OnFinished()
    {
        m_completion.TrySetResult(m_capturedImages);
        await Shell.Current.Navigation.PopModalAsync();
    }

    private async void OnCancelled()
    {
        m_completion.TrySetResult([]);
        await Shell.Current.Navigation.PopModalAsync();
    }

    private async void OnCameraFailed(CameraException e)
    {
        await DisplayAlertAsync("Something failed!", e.Message, "Ok");
        m_completion.TrySetResult([]);
        await Shell.Current.Navigation.PopModalAsync();
    }
}
