using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetNavigationContentViewController : UIViewController
{
    private readonly View m_content;
    private readonly BottomSheet m_bottomSheet;

    public BottomSheetNavigationContentViewController(View content, string? title, BottomSheet bottomSheet)
    {
        m_content = content;
        m_bottomSheet = bottomSheet;
        Title = title ?? string.Empty;
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        if (View is null) return;

        View.BackgroundColor = Colors.GetColor(BottomSheet.BackgroundColorName).ToPlatform();

        var mauiContext = DUI.GetCurrentMauiContext;
        if (mauiContext is null) return;

        var nativeView = m_content.ToPlatform(mauiContext);
        View.AddSubview(nativeView);

        nativeView.TranslatesAutoresizingMaskIntoConstraints = false;
        NSLayoutConstraint.ActivateConstraints([
            nativeView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            nativeView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            nativeView.TopAnchor.ConstraintEqualTo(View.TopAnchor),
            nativeView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor)
        ]);
    }

    public override void ViewWillDisappear(bool animated)
    {
        base.ViewWillDisappear(animated);

        if (IsMovingFromParentViewController)
        {
            // The user interactively popped (swipe-back gesture) or this VC was programmatically removed.
            // Keep the managed navigation stack in sync.
            m_bottomSheet.HandleInteractivePop(m_content);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            m_content.DisconnectHandlers();
        }

        base.Dispose(disposing);
    }
}
