using DIPS.Mobile.UI.Components.BottomSheets.iOS;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.BottomSheets;

public static partial class BottomSheetService
{
    internal const string BottomSheetRestorationIdentifier = nameof(BottomSheetContentPage);
    internal static BottomSheetContentPage? Current { get; set; }
    public static partial Task OpenBottomSheet(BottomSheet bottomSheet)
    {
        if (IsBottomSheetOpen())
        {
            CloseCurrentBottomSheet();
        }
        
        Current = new BottomSheetContentPage(bottomSheet);
        return Current.Open();
    }

    public static async partial Task CloseCurrentBottomSheet(bool animated)
    {
        if (Current != null)
        {
            await Current.Close(animated);
        }
    }

    public static partial bool IsBottomSheetOpen()
    {
        return Current != null;
    }
}