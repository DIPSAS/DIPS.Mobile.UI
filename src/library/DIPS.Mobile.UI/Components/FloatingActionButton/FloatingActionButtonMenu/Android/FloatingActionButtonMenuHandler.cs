using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Handlers;
using Color = Android.Graphics.Color;
using FAB = Google.Android.Material.FloatingActionButton.FloatingActionButton;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

public partial class FloatingActionButtonMenuHandler : ViewHandler<FloatingActionButtonMenu, RelativeLayout>
{

    protected override RelativeLayout CreatePlatformView()
    {
        var constraintLayout = new RelativeLayout(Context);

        var fab = new FAB(Context);
        
        constraintLayout.LayoutParameters = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
        
        var fabLayoutParams =
            new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
        fabLayoutParams.AddRule(LayoutRules.AlignParentBottom);
        fabLayoutParams.AddRule(LayoutRules.AlignParentEnd);

        fabLayoutParams.BottomMargin = 176;
        fabLayoutParams.MarginEnd = 24;

        constraintLayout.AddView(fab, fabLayoutParams);
        
        return constraintLayout;
    }
}