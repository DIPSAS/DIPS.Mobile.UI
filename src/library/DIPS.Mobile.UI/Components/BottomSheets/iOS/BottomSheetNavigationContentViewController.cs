using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets.Header;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
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

        ConfigureCloseButton();

        if (m_bottomSheet.BottomSheetHeaderBehavior is { } behavior)
        {
            behavior.PropertyChanged += OnHeaderBehaviorPropertyChanged;
        }
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
            m_bottomSheet.HandleInteractivePop(m_content);
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
            m_content.DisconnectHandlers();
        }

        base.Dispose(disposing);
    }
}
