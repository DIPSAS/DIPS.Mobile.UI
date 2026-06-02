using DIPS.Mobile.UI.Components.BottomSheets.iOS;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheet
{
    private partial Task PlatformPushAsync(ContentPage page)
    {
        var navigationController = ViewController?.NavigationController ??
            throw new InvalidOperationException("NavigationController is not available. Cannot push content to bottom sheet.");

        var vc = new BottomSheetNavigationContentViewController(page, this);
        navigationController.PushViewController(vc, animated: true);
        
        return Task.CompletedTask;
    }

    private partial Task PlatformPopAsync(BottomSheetNavigationEntry popped)
    {
        var navigationController = ViewController?.NavigationController ??
            throw new InvalidOperationException("NavigationController is not available. Cannot pop content from bottom sheet.");

        navigationController.PopViewController(animated: true);
        // Content cleanup is handled by BottomSheetNavigationContentViewController.Dispose

        return Task.CompletedTask;
    }
}
