namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheet
{
    private partial Task PlatformPushAsync(View content, string? title)
    {
        if (Handler is not BottomSheetHandler handler) return Task.CompletedTask;
        handler.PushNavigationContent(content, title);
        return Task.CompletedTask;
    }

    private partial Task PlatformPopAsync(BottomSheetNavigationEntry popped)
    {
        if (Handler is not BottomSheetHandler handler) return Task.CompletedTask;
        handler.PopNavigationContent(popped);
        return Task.CompletedTask;
    }
}
