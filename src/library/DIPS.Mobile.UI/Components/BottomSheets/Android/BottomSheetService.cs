using Android.App;
using DIPS.Mobile.UI.Components.BottomSheets.Android;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.BottomSheets;

public static partial class BottomSheetService
{
    internal const string BottomSheetFragmentTag = nameof(BottomSheetFragment);

    internal static BottomSheetFragment? Current { get; set; }
    
    public static async partial Task OpenBottomSheet(BottomSheet bottomSheet)
    {
        if (IsBottomSheetOpen())
        {
            await CloseCurrentBottomSheet();
        }

        var fragment = new BottomSheetFragment(bottomSheet);
        await fragment.Show();
        Current = fragment;
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