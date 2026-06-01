using DIPS.Mobile.UI.Components.BottomSheets.iOS;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheet
{
    private partial Task PlatformPushAsync(View content, string? title)
    {
        var navigationController = ViewController?.NavigationController;
        if (navigationController is null) return Task.CompletedTask;

        var vc = new BottomSheetNavigationContentViewController(content, title, this);
        navigationController.PushViewController(vc, animated: true);
        
        return Task.CompletedTask;
    }

    private partial Task PlatformPopAsync(BottomSheetNavigationEntry popped)
    {
        var navigationController = ViewController?.NavigationController;
        if (navigationController is null) return Task.CompletedTask;

        navigationController.PopViewController(animated: true);
        // Content cleanup is handled by BottomSheetNavigationContentViewController.Dispose

        return Task.CompletedTask;
    }
}
