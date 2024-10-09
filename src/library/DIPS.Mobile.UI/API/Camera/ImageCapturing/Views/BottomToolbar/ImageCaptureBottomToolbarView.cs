using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.ConfirmState;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.EditState;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;
using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar;

internal class ImageCaptureBottomToolbarView : Grid
{
    public ImageCaptureBottomToolbarView()
    {
        Margin = new Thickness(Sizes.GetSize(SizeName.size_5), 0);
    }

    public void GoToConfirmState(Action onUsePhoto, Action onRetakePhoto)
    {
        ResolveMemoryLeak(() =>
        {
            Add(new ConfirmStateView(onUsePhoto, onRetakePhoto));
        });
    }

    public void GoToStreamingState(Action onTappedShutterButton, Action onTappedFlashButton, bool shouldBlitzBeActive = false)
    {
        ResolveMemoryLeak(() =>
        {
            Add(new StreamingStateView(onTappedShutterButton, onTappedFlashButton, shouldBlitzBeActive));
        });
    }

    public void GoToEditState(Action onDoneButtonTapped, Action onCancelButtonTapped, Action onRotateButtonTapped)
    {
        ResolveMemoryLeak(() =>
        {
            Add(new EditStateView(onDoneButtonTapped, onCancelButtonTapped, onRotateButtonTapped));
        });
    }
    
    private void ResolveMemoryLeak(Action beforeResolve)
    {
        var childrenThatWillBeRemoved = Children.ToList();
        Clear();
        
        beforeResolve.Invoke();
        
        foreach (var view in childrenThatWillBeRemoved)
        {
            new VisualTreeMemoryResolver().TryResolveMemoryLeakCascading(view);
        }
    }

    public void SetShutterButtonEnabled(bool state)
    {
        if (Children[0] is StreamingStateView streamingStateView)
        {
            streamingStateView.SetShutterButtonEnabled(state);
        }
    }
}