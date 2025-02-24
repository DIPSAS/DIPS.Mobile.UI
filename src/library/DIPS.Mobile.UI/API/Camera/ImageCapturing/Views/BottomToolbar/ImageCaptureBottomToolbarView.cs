using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.ConfirmState;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.EditState;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;
using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar;

internal class ImageCaptureBottomToolbarView : Grid
{
    public ImageCaptureBottomToolbarView()
    {
        Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_large), 0);
    }

    public void GoToConfirmState(IConfirmStateObserver confirmStateObserver)
    {
        DisconnectHandlers(() =>
        {
            Add(new ConfirmStateView(confirmStateObserver));
        });
    }

    public void GoToStreamingState(IStreamingStateObserver streamingStateObserver)
    {
        DisconnectHandlers(() =>
        {
            Add(new StreamingStateView(streamingStateObserver));
        });
    }

    public void GoToEditState(IImageEditStateObserver imageEditStateObserver)
    {
        DisconnectHandlers(() =>
        {
            Add(new EditStateBottomView(imageEditStateObserver));
        });
    }
    
    private void DisconnectHandlers(Action beforeResolve)
    {
        var childrenThatWillBeRemoved = Children.ToList();
        Clear();
        
        beforeResolve.Invoke();
        
        foreach (var view in childrenThatWillBeRemoved)
        {
            view.DisconnectHandlers();
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