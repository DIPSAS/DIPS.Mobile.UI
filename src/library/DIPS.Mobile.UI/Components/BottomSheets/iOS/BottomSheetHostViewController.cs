using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

/// <summary>
/// Container VC that is presented modally. Contains a UINavigationController (with sheet content)
/// at the top, and a fixed bottom bar at the bottom. The bottom bar is outside the UINavigationController
/// and is therefore not affected by push/pop navigation.
/// </summary>
internal class BottomSheetHostViewController : UIViewController
{
    private readonly UINavigationController m_navigationController;
    private readonly BottomSheetViewController m_bottomSheetViewController;
    private BottomBarView? m_bottomBar;

    public BottomSheetHostViewController(UINavigationController navigationController, BottomSheetViewController bottomSheetViewController)
    {
        m_navigationController = navigationController;
        m_bottomSheetViewController = bottomSheetViewController;
    }

    public BottomSheetViewController BottomSheetViewController => m_bottomSheetViewController;

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        if (View is null) return;

        // Set background color to match the bottom sheet so the bottom bar gradient blends correctly
        View.BackgroundColor = m_bottomSheetViewController.BottomSheet.BackgroundColor.ToPlatform();

        // Add UINavigationController as child VC
        AddChildViewController(m_navigationController);
        View.AddSubview(m_navigationController.View!);
        m_navigationController.DidMoveToParentViewController(this);

        // Create the bottom bar directly in this view (outside nav controller)
        m_bottomBar = new BottomBarView(View, m_bottomSheetViewController.BottomSheet);

        // Layout: nav controller fills everything above the bottom bar, bottom bar fixed at bottom
        m_navigationController.View!.TranslatesAutoresizingMaskIntoConstraints = false;
        m_bottomBar.NativeView.TranslatesAutoresizingMaskIntoConstraints = false;

        m_bottomBar.NativeView.RemoveFromSuperview(); // Remove from any other parent
        View.AddSubview(m_bottomBar.NativeView);

        NSLayoutConstraint.ActivateConstraints([
            // Nav controller: fills the ENTIRE view (content can scroll behind the bottom bar)
            m_navigationController.View!.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            m_navigationController.View!.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            m_navigationController.View!.TopAnchor.ConstraintEqualTo(View.TopAnchor),
            m_navigationController.View!.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),

            // Bottom bar: fixed at the bottom, ON TOP of the nav controller (overlay for gradient fade)
            m_bottomBar.NativeView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            m_bottomBar.NativeView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            m_bottomBar.NativeView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
            m_bottomBar.NativeView.HeightAnchor.ConstraintEqualTo((float)BottomSheet.BottomBarHeight)
        ]);
    }

    /// <summary>
    /// Shows or hides the bottom bar. Called from the handler mapper.
    /// </summary>
    public void ModifyBottomBar(bool show)
    {
        if (m_bottomBar is null) return;
        m_bottomBar.NativeView.Hidden = !show;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            m_navigationController.WillMoveToParentViewController(null);
            m_navigationController.View?.RemoveFromSuperview();
            m_navigationController.RemoveFromParentViewController();
            m_bottomBar?.DisconnectHandlers();
        }

        base.Dispose(disposing);
    }
}

