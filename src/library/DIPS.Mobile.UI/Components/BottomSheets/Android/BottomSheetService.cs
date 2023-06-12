using DIPS.Mobile.UI.Components.BottomSheets.Android;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.BottomSheets;

public static partial class BottomSheetService
{
    internal const string BottomSheetFragmentTag = nameof(BottomSheetFragment);

    public static partial Task OpenBottomSheet(BottomSheet bottomSheet) => new BottomSheetFragment(bottomSheet).Show();

    public static partial Task CloseCurrentBottomSheet(bool animated)
    {
        var currentBottomSheetFragment = CurrentBottomSheetFragment();
       currentBottomSheetFragment?.Close();
        return Task.CompletedTask;
    }

    public static partial bool IsBottomSheetOpen()
    {
        return CurrentBottomSheetFragment() != null;
    }

    private static BottomSheetFragment? CurrentBottomSheetFragment()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(BottomSheetFragmentTag);
        if (fragment is BottomSheetFragment bottomSheetFragment)
        {
            return bottomSheetFragment;
        }

        return null;
    }
}