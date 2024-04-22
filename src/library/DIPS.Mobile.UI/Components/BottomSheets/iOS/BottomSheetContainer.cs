using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetContainer : Grid
{
    private readonly BottomSheet m_bottomsheet;

    public BottomSheetContainer(BottomSheet bottomSheet)
    {
        m_bottomsheet = bottomSheet;
        
        BackgroundColor = Colors.GetColor(ColorName.color_system_white);
        
        AddRowDefinition(new RowDefinition(GridLength.Auto));
        AddRowDefinition(new RowDefinition(GridLength.Star));

        this.Add(bottomSheet, 0, 1);
    }
    
    public void ModifySearchbar(bool add)
    {
        if (add)
        {
            this.Add(m_bottomsheet.SearchBar);
        }
        else
        {
            Remove(m_bottomsheet.SearchBar);
        }
    }

    public void AddToView(UIView rootView, UINavigationBar? navigationBar)
    {
        var nativeView = this.ToPlatform(DUI.GetCurrentMauiContext!);
        
        rootView.AddSubview(nativeView);
        SetPadding(navigationBar);
        SetConstraints(rootView, nativeView);
    }

    private void SetPadding(UINavigationBar? navigationBar)
    {
        var bottom = (UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom) == 0
            ? Sizes.GetSize(SizeName.size_4) //There is a physical home button
            : Sizes.GetSize(SizeName.size_1); //There is no physical home button, but we need some air between the safe area and the content

        Padding = new Thickness(0, navigationBar is not null ? navigationBar.Frame.Height : Sizes.GetSize(SizeName.size_4), 0, bottom);
    }

    private static void SetConstraints(UIView rootView, UIView uiView)
    {
        uiView.TranslatesAutoresizingMaskIntoConstraints = false;
        
        NSLayoutConstraint.ActivateConstraints([
            uiView.LeadingAnchor.ConstraintEqualTo(rootView.LeadingAnchor),
            uiView.TrailingAnchor.ConstraintEqualTo(rootView.TrailingAnchor),
            uiView.HeightAnchor.ConstraintEqualTo(rootView.Frame.Height),
        ]);
    }
}