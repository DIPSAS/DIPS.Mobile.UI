using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

/// <summary>
/// Container-VC som presenteres modalt. Inneholder UINavigationController (med sheet-innholdet)
/// øverst, og en fast bunnlinje nederst. Bunnlinjen er utenfor UINavigationController og påvirkes
/// derfor ikke av push/pop-navigasjon.
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

        // Sett bakgrunnsfarge lik bottomsheetets farge slik at bunnlinjens gradient blender korrekt
        View.BackgroundColor = m_bottomSheetViewController.BottomSheet.BackgroundColor.ToPlatform();

        // Legg til UINavigationController som child VC
        AddChildViewController(m_navigationController);
        View.AddSubview(m_navigationController.View!);
        m_navigationController.DidMoveToParentViewController(this);

        // Opprett bunnlinjen direkte i dette viewet (utenfor nav controller)
        m_bottomBar = new BottomBarView(View, m_bottomSheetViewController.BottomSheet);

        // Layout: nav controller fyller alt over bunnlinjen, bunnlinje fast i bunn
        m_navigationController.View!.TranslatesAutoresizingMaskIntoConstraints = false;
        m_bottomBar.NativeView.TranslatesAutoresizingMaskIntoConstraints = false;

        m_bottomBar.NativeView.RemoveFromSuperview(); // Fjern fra eventuell annen parent
        View.AddSubview(m_bottomBar.NativeView);

        NSLayoutConstraint.ActivateConstraints([
            // Nav controller: fyller HELE viewet (innhold kan scrolle bak bunnlinjen)
            m_navigationController.View!.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            m_navigationController.View!.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            m_navigationController.View!.TopAnchor.ConstraintEqualTo(View.TopAnchor),
            m_navigationController.View!.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),

            // Bunnlinje: fast i bunn, OPPÅ nav controller (overlay for gradient-fade)
            m_bottomBar.NativeView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            m_bottomBar.NativeView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            m_bottomBar.NativeView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
            m_bottomBar.NativeView.HeightAnchor.ConstraintEqualTo((float)BottomSheet.BottomBarHeight)
        ]);
    }

    /// <summary>
    /// Viser eller skjuler bunnlinjen. Kalles fra handler-mapperen.
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

