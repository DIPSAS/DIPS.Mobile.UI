using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets.Header;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetNavigationContentViewController : UIViewController
{
    private readonly ContentPage m_page;
    private readonly BottomSheet m_bottomSheet;

    public BottomSheetNavigationContentViewController(ContentPage page, BottomSheet bottomSheet)
    {
        m_page = page;
        m_bottomSheet = bottomSheet;
        Title = page.Title ?? string.Empty;
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        if (View is null) return;

        // Use the content's background color if explicitly set, otherwise inherit from the bottom sheet
        var contentBackgroundColor = m_page.BackgroundColor;
        var effectiveColor = contentBackgroundColor ?? m_bottomSheet.BackgroundColor;
        View.BackgroundColor = effectiveColor.ToPlatform();

        ConfigureNavigationBarAppearance();

        var mauiContext = DUI.GetCurrentMauiContext;
        if (mauiContext is null) return;

        var nativeView = m_page.Content.ToPlatform(mauiContext);
        View.AddSubview(nativeView);

        nativeView.TranslatesAutoresizingMaskIntoConstraints = false;
        NSLayoutConstraint.ActivateConstraints([
            nativeView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            nativeView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            nativeView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor),
            nativeView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor)
        ]);

        ConfigureCloseButton();

        if (m_bottomSheet.BottomSheetHeaderBehavior is { } behavior)
        {
            behavior.PropertyChanged += OnHeaderBehaviorPropertyChanged;
        }
    }

    private void ConfigureNavigationBarAppearance()
    {
        var appearance = new UINavigationBarAppearance();
        appearance.ConfigureWithOpaqueBackground();
        appearance.BackgroundColor = Colors.GetColor(ColorName.color_surface_default).ToPlatform();
        appearance.ShadowColor = UIColor.Clear;
        appearance.TitleTextAttributes = new UIStringAttributes
        {
            ForegroundColor = Colors.GetColor(ColorName.color_text_default).ToPlatform()
        };
        NavigationItem.StandardAppearance = appearance;
        NavigationItem.ScrollEdgeAppearance = appearance;
    }

    private void OnHeaderBehaviorPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BottomSheetHeaderBehavior.IsCloseButtonVisible)
            or nameof(BottomSheetHeaderBehavior.CloseButtonCommand))
        {
            MainThread.BeginInvokeOnMainThread(ConfigureCloseButton);
        }
    }

    private void ConfigureCloseButton()
    {
        var headerBehavior = m_bottomSheet.BottomSheetHeaderBehavior;

        if (headerBehavior?.IsCloseButtonVisible ?? true)
        {
            var closeButton = new UIBarButtonItem(UIBarButtonSystemItem.Close, (_, _) => OnCloseButtonTapped());
            closeButton.AccessibilityLabel = DUILocalizedStrings.Close;
            NavigationItem.RightBarButtonItem = closeButton;
        }
        else
        {
            NavigationItem.RightBarButtonItem = null;
        }
    }

    private void OnCloseButtonTapped()
    {
        if (m_bottomSheet.BottomSheetHeaderBehavior?.CloseButtonCommand is not null)
        {
            m_bottomSheet.BottomSheetHeaderBehavior.CloseButtonCommand.Execute((Action)(() => m_bottomSheet.Close()));
        }
        else
        {
            m_bottomSheet.Close();
        }
    }

    public override void ViewWillDisappear(bool animated)
    {
        base.ViewWillDisappear(animated);

        if (IsMovingFromParentViewController)
        {
            // The user interactively popped (swipe-back gesture) or this VC was programmatically removed.
            // Keep the managed navigation stack in sync.
            m_bottomSheet.HandleInteractivePop(m_page);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (m_bottomSheet.BottomSheetHeaderBehavior is { } behavior)
            {
                behavior.PropertyChanged -= OnHeaderBehaviorPropertyChanged;
            }
            NavigationItem.RightBarButtonItem = null;
            m_page.Content.DisconnectHandlers();
        }

        base.Dispose(disposing);
    }
}
