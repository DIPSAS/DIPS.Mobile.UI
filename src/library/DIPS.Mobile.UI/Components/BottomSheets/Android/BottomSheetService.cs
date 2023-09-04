using Android.App;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets.Android;
using DIPS.Mobile.UI.Components.Labels;
using Google.Android.Material.AppBar;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;
using Application = Microsoft.Maui.Controls.Application;
using Button = Microsoft.Maui.Controls.Button;
using Colors = Microsoft.Maui.Graphics.Colors;
using Label = Microsoft.Maui.Controls.Label;
using Window = Microsoft.Maui.Controls.Window;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.BottomSheets;

public static partial class BottomSheetService
{
    internal static BottomSheetFragment? Current { get; set; }
    
    public static async partial Task OpenBottomSheet(BottomSheet bottomSheet)
    {
        try
        {
            if (IsBottomSheetOpen())
            {
                await CloseCurrentBottomSheet();
            }
            
        
            var fragment = new BottomSheetFragment(bottomSheet);
            await fragment.Show();
            Current = fragment;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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