using Android.App;
using DIPS.Mobile.UI.Components.BottomSheets.Android;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.BottomSheets;

public static partial class BottomSheetService
{

    internal static partial Task PlatformOpen(BottomSheet bottomSheet)
    {
        bottomSheet.BottomSheetFragment = new BottomSheetFragment(bottomSheet);
        return bottomSheet.BottomSheetFragment.Show();
    }

    public static partial Task Close(BottomSheet bottomSheet, bool animated)
    {
        return bottomSheet.BottomSheetFragment.Close(animated);
    }
}