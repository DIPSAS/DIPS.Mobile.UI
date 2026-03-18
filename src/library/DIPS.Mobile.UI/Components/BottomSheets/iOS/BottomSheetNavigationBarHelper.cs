using DIPS.Mobile.UI.Components.BottomSheets.Header;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetNavigationBarHelper
{
    private readonly BottomSheet m_bottomSheet;
    private readonly UINavigationItem m_navigationItem;
    private readonly WeakReference<UINavigationController?> m_weakNavigationController;

    public BottomSheetNavigationBarHelper(BottomSheet bottomSheet, UINavigationItem navigationItem, UINavigationController? navigationController)
    {
        m_bottomSheet = bottomSheet;
        m_navigationItem = navigationItem;
        m_weakNavigationController = new WeakReference<UINavigationController?>(navigationController);

        if (bottomSheet.BottomSheetHeaderBehavior is not null)
        {
            bottomSheet.BottomSheetHeaderBehavior.PropertyChanged += OnHeaderBehaviorPropertyChanged;
        }
    }

    public void Configure()
    {
        m_navigationItem.Title = m_bottomSheet.Title;

        var appearance = new UINavigationBarAppearance();
        appearance.ConfigureWithOpaqueBackground();
        appearance.BackgroundColor = Colors.GetColor(ColorName.color_surface_default).ToPlatform();
        appearance.ShadowColor = UIColor.Clear;
        appearance.TitleTextAttributes = new UIStringAttributes
        {
            ForegroundColor = Colors.GetColor(ColorName.color_text_default).ToPlatform()
        };
        m_navigationItem.StandardAppearance = appearance;
        m_navigationItem.ScrollEdgeAppearance = appearance;

        UpdateBarItems();
    }

    public void UpdateTitle()
    {
        m_navigationItem.Title = m_bottomSheet.Title;
    }

    private void OnHeaderBehaviorPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BottomSheetHeaderBehavior.IsBackButtonVisible)
            or nameof(BottomSheetHeaderBehavior.IsCloseButtonVisible)
            or nameof(BottomSheetHeaderBehavior.IsVisible)
            or nameof(BottomSheetHeaderBehavior.TitleAndBackButtonContainerCommand)
            or nameof(BottomSheetHeaderBehavior.CloseButtonCommand))
        {
            MainThread.BeginInvokeOnMainThread(UpdateBarItems);
        }
    }

    private void UpdateBarItems()
    {
        var headerBehavior = m_bottomSheet.BottomSheetHeaderBehavior;
        m_weakNavigationController.TryGetTarget(out var navigationController);

        if (headerBehavior is not null && !headerBehavior.IsVisible)
        {
            navigationController?.SetNavigationBarHidden(true, false);
            return;
        }

        navigationController?.SetNavigationBarHidden(false, false);

        // Close button (right)
        if (headerBehavior?.IsCloseButtonVisible ?? true)
        {
            var closeButton = new UIBarButtonItem( UIBarButtonSystemItem.Close, (_, _) => OnCloseButtonTapped());
            closeButton.AccessibilityLabel = DUILocalizedStrings.Close;
            m_navigationItem.RightBarButtonItem = closeButton;
        }
        else
        {
            m_navigationItem.RightBarButtonItem = null;
        }

        // Back button (left)
        if (headerBehavior?.IsBackButtonVisible ?? false)
        {
            var backImage = UIImage.GetSystemImage("chevron.left");
            var backButton = new UIBarButtonItem(backImage, UIBarButtonItemStyle.Plain, (_, _) =>
            {
                m_bottomSheet.BottomSheetHeaderBehavior?.TitleAndBackButtonContainerCommand?.Execute(null);
            });
            backButton.AccessibilityLabel = DUILocalizedStrings.Back;
            m_navigationItem.LeftBarButtonItem = backButton;
        }
        else
        {
            m_navigationItem.LeftBarButtonItem = null;
        }
    }

    private void OnCloseButtonTapped()
    {
        if (m_bottomSheet.BottomSheetHeaderBehavior?.CloseButtonCommand is not null)
        {
            // Pass the close action as parameter — the consumer decides if/when to invoke it
            m_bottomSheet.BottomSheetHeaderBehavior.CloseButtonCommand.Execute((Action)(() => m_bottomSheet.Close()));
        }
        else
        {
            m_bottomSheet.Close();
        }
    }

    public void Dispose()
    {
        if (m_bottomSheet.BottomSheetHeaderBehavior is not null)
        {
            m_bottomSheet.BottomSheetHeaderBehavior.PropertyChanged -= OnHeaderBehaviorPropertyChanged;
        }
    }
}
