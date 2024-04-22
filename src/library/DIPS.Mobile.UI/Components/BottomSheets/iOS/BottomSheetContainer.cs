using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetContainer : Grid
{
    private readonly BottomSheet m_bottomsheet;

    public BottomSheetContainer(BottomSheet bottomSheet)
    {
        m_bottomsheet = bottomSheet;
        
        BackgroundColor = bottomSheet.BackgroundColor;
        
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

    public void SetPadding(UINavigationBar? navigationBar)
    {
        var bottom = (UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom) == 0
            ? Sizes.GetSize(SizeName.size_4) //There is a physical home button
            : Sizes.GetSize(SizeName.size_1); //There is no physical home button, but we need some air between the safe area and the content

        Padding = new Thickness(m_bottomsheet.Padding.Left, navigationBar is not null ? navigationBar.Frame.Height + m_bottomsheet.Padding.Top : m_bottomsheet.Padding.Top,  m_bottomsheet.Padding.Right, bottom +  m_bottomsheet.Padding.Bottom);
        m_bottomsheet.Padding = 0;
    }

    public void SetConstraints(UIView rootView)
    {
        var nativeView = this.ToPlatform(DUI.GetCurrentMauiContext!);
        nativeView.TranslatesAutoresizingMaskIntoConstraints = false;
        
        NSLayoutConstraint.ActivateConstraints([
            nativeView.LeadingAnchor.ConstraintEqualTo(rootView.LeadingAnchor),
            nativeView.TrailingAnchor.ConstraintEqualTo(rootView.TrailingAnchor),
            nativeView.HeightAnchor.ConstraintEqualTo(rootView.Frame.Height),
        ]);
    }
}