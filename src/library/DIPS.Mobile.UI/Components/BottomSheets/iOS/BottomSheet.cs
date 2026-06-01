using DIPS.Mobile.UI.Components.BottomSheets.iOS;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheet
{
    private partial Task PlatformPushAsync(View content, string? title)
    {
        var navigationController = ViewController?.NavigationController ??
            throw new InvalidOperationException("NavigationController er ikke tilgjengelig. Kan ikke pushe innhold til bottom sheet.");

        var vc = new BottomSheetNavigationContentViewController(content, title, this);
        navigationController.PushViewController(vc, animated: true);
        
        return Task.CompletedTask;
    }

    private partial Task PlatformPopAsync(BottomSheetNavigationEntry popped)
    {
        var navigationController = ViewController?.NavigationController ??
            throw new InvalidOperationException("NavigationController er ikke tilgjengelig. Kan ikke poppe innhold fra bottom sheet.");

        navigationController.PopViewController(animated: true);
        // Opprydding av innhold håndteres av BottomSheetNavigationContentViewController.Dispose

        return Task.CompletedTask;
    }
}
