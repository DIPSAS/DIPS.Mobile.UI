namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheet
{
    private partial Task PlatformPushAsync(ContentPage page)
    {
        if (Handler is not BottomSheetHandler handler) throw new InvalidOperationException("BottomSheet must be opened before calling PushAsync.");
        handler.PushNavigationContent(page);
        return Task.CompletedTask;
    }

    private partial Task PlatformPopAsync(BottomSheetNavigationEntry popped)
    {
        if (Handler is not BottomSheetHandler handler) throw new InvalidOperationException("BottomSheet must be opened before calling PopAsync.");
        handler.PopNavigationContent(popped);
        return Task.CompletedTask;
    }
}
