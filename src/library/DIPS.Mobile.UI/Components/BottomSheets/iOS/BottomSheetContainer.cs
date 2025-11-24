using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets.Header;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetContainer : Grid
{
    private readonly BottomSheet m_bottomSheet;
    private BottomSheetHeader m_bottomSheetHeader;

    public BottomSheetContainer(BottomSheet bottomSheet)
    {
        m_bottomSheet = bottomSheet;
        
        BackgroundColor = bottomSheet.BackgroundColor;
        
        AddRowDefinition(new RowDefinition(GridLength.Auto));
        AddRowDefinition(new RowDefinition(GridLength.Auto));
        AddRowDefinition(new RowDefinition(GridLength.Star));

        m_bottomSheetHeader = new BottomSheetHeader(m_bottomSheet);
        this.Add(m_bottomSheetHeader);
        
        this.Add(bottomSheet, 0, 2);
    }

    public void ModifySearchbar(bool add)
    {
        if (add)
        {
            this.Add(m_bottomSheet.SearchBar, 0, 1);
        }
        else
        {
            Remove(m_bottomSheet.SearchBar);
        }
    }

    public void AddToView(UIView rootView)
    {
        var nativeView = this.ToPlatform(DUI.GetCurrentMauiContext!);
        
        rootView.AddSubview(nativeView);
        SetPadding();
        SetConstraints(rootView, nativeView);
    }

    private void SetPadding()
    {
        var bottom = (UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom) == 0
            ? Sizes.GetSize(SizeName.content_margin_large) //There is a physical home button
            : Sizes.GetSize(SizeName.content_margin_xsmall); //There is no physical home button, but we need some air between the safe area and the content

        Padding = new Thickness(0, 0, 0, bottom);
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

    public void SetSemanticFocusToHeader()
    {
        m_bottomSheetHeader.SetSemanticFocus();
    }
}