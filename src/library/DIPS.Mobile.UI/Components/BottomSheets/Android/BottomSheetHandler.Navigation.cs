using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using Google.Android.Material.Transition.Platform;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using View = Microsoft.Maui.Controls.View;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.BottomSheets;

/// <summary>
/// Handles internal navigation (push/pop) in the bottom sheet on Android.
/// </summary>
public partial class BottomSheetHandler
{
    // Navigation state
    private FrameLayout? m_navigationContainer;
    private AView? m_currentNativeContentView;
    private Stack<AView> m_nativeNavigationStack = new();
    private NavigationBackPressedCallback? m_navigationBackCallback;

    /// <summary>
    /// Sets up the navigation container and adds the content view to the bottom sheet layout.
    /// </summary>
    private void SetupNavigationContainer(Context context, AView bottomSheetAndroidView, LinearLayout bottomSheetLayout)
    {
        m_navigationContainer = new FrameLayout(context)
        {
            LayoutParameters = new ViewGroup.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent)
        };
        m_currentNativeContentView = bottomSheetAndroidView;
        m_navigationContainer.AddView(bottomSheetAndroidView);
        bottomSheetLayout.AddView(m_navigationContainer);

        // Make the content fill the sheet height when the sheet has a fixed (non-Fit) height.
        ApplyContentFillMode();

        // Add breathing room at the bottom of the content (parity with iOS).
        ApplyContentBottomPadding();
    }

    /// <summary>
    /// Ensures the pushed content fills the available sheet height in non-<see cref="Positioning.Fit"/>
    /// positioning, mirroring iOS where the content is constrained to the sheet height
    /// (see <c>BottomSheetContainer.SetConstraints</c>).
    /// <para>
    /// In <see cref="Positioning.Medium"/>/<see cref="Positioning.Large"/> the sheet has a fixed height,
    /// so the content column (<see cref="m_bottomSheetLayout"/>) must fill that height and the navigation
    /// container must take the remaining space (weight) below the drag handle/header/search bar. Otherwise
    /// a star-sized/Fill row collapses and the empty area shows the sheet background.
    /// In <see cref="Positioning.Fit"/> the sheet sizes to its content, so both wrap.
    /// </para>
    /// </summary>
    private void ApplyContentFillMode()
    {
        if (m_bottomSheetLayout is null || m_navigationContainer is null)
            return;

        var fill = m_bottomSheet.Positioning is not Positioning.Fit;

        // The content column fills the sheet height (non-Fit) or wraps its content (Fit).
        if (m_bottomSheetLayout.LayoutParameters is { } layoutParameters)
        {
            layoutParameters.Height = fill
                ? ViewGroup.LayoutParams.MatchParent
                : ViewGroup.LayoutParams.WrapContent;
            m_bottomSheetLayout.LayoutParameters = layoutParameters;
        }

        // The navigation container takes the remaining vertical space (weight) when filling.
        m_navigationContainer.LayoutParameters = fill
            ? new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 0) { Weight = 1 }
            : new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
    }

    /// <summary>
    /// Sizes the content column to the sheet's currently <b>visible</b> height
    /// Only applies in non-<see cref="Positioning.Fit"/> positioning; in <see cref="Positioning.Fit"/>
    /// </summary>
    internal void UpdateContentFillHeight(AView slidingView)
    {
        if (m_bottomSheet.Positioning is Positioning.Fit)
            return;

        if (m_bottomSheetLayout?.LayoutParameters is not { } layoutParameters)
            return;

        var visibleHeight = slidingView.Height - slidingView.Top;
        if (visibleHeight <= 0 || layoutParameters.Height == visibleHeight)
            return;

        layoutParameters.Height = visibleHeight;
        m_bottomSheetLayout.LayoutParameters = layoutParameters;
    }

    /// <summary>
    /// Adds bottom padding to the content so it does not sit flush against the sheet's bottom edge,
    /// </summary>
    internal void ApplyContentBottomPadding()
    {
        if (m_navigationContainer?.Context is not { } context)
            return;

        // Paint the container with the sheet background so the padding area below the content does
        // not reveal the platform bottom-sheet's default background
        if (m_bottomSheet.BackgroundColor is { } backgroundColor)
            m_navigationContainer.SetBackgroundColor(backgroundColor.ToPlatform());

        var hasBottomInset = GetBottomNavigationBarInset() > 0;
        var extra = hasBottomInset
            ? Sizes.GetSize(SizeName.content_margin_xsmall)
            : Sizes.GetSize(SizeName.content_margin_large);

        m_navigationContainer.SetPadding(
            m_navigationContainer.PaddingLeft,
            m_navigationContainer.PaddingTop,
            m_navigationContainer.PaddingRight,
            (int)context.ToPixels(extra));
    }

    private int GetBottomNavigationBarInset()
    {
        if (m_navigationContainer is null)
            return 0;

        var insets = ViewCompat.GetRootWindowInsets(m_navigationContainer);
        return insets?.GetInsets(WindowInsetsCompat.Type.NavigationBars()).Bottom ?? 0;
    }


    internal void PushNavigationContent(ContentPage page)
    {
        var mauiContext = DUI.GetCurrentMauiContext;
        if (mauiContext is null || m_navigationContainer is null || m_bottomSheetLayout is null) return;

        // Save current native view on the stack
        m_nativeNavigationStack.Push(m_currentNativeContentView!);

        // Create new native view
        var newNativeView = page.Content.ToPlatform(mauiContext);

        // Material 3 shared axis transition (X-axis, forward)
        var transition = new MaterialSharedAxis(MaterialSharedAxis.X, /* entering */ true);
        transition.SetDuration(300);
        global::Android.Transitions.TransitionManager.BeginDelayedTransition(m_navigationContainer, transition);

        // Hide old, show new
        m_currentNativeContentView!.Visibility = ViewStates.Gone;
        m_navigationContainer.AddView(newNativeView);
        m_currentNativeContentView = newNativeView;

        // Update toolbar with title and back button
        UpdateHeaderToolbarForNavigation(page.Title);
        m_navigationBackCallback?.UpdateEnabled();
    }

    internal void PopNavigationContent(BottomSheetNavigationEntry popped)
    {
        if (m_navigationContainer is null || m_nativeNavigationStack.Count == 0) return;

        var previousNativeView = m_nativeNavigationStack.Pop();

        // Material 3 shared axis transition (X-axis, backward)
        var transition = new MaterialSharedAxis(MaterialSharedAxis.X, /* entering */ false);
        transition.SetDuration(300);
        global::Android.Transitions.TransitionManager.BeginDelayedTransition(m_navigationContainer, transition);

        // Remove current, restore previous
        m_navigationContainer.RemoveView(m_currentNativeContentView);
        previousNativeView.Visibility = ViewStates.Visible;
        m_currentNativeContentView = previousNativeView;

        // Disconnect handlers for the popped MAUI view
        popped.Page.Content.DisconnectHandlers();

        // Update toolbar: either restore root state or show previous pushed entry
        if (m_nativeNavigationStack.Count == 0)
        {
            UpdateHeaderToolbarFromBottomSheet();
        }
        else
        {
            var previousEntry = m_bottomSheet.NavigationStack.Peek();
            UpdateHeaderToolbarForNavigation(previousEntry.Page.Title);
        }

        m_navigationBackCallback?.UpdateEnabled();
    }

    internal void PopToRootNavigationContent(IReadOnlyList<BottomSheetNavigationEntry> popped)
    {
        if (m_navigationContainer is null || m_nativeNavigationStack.Count == 0) return;

        // Material 3 shared axis transition (X-axis, backward) — single animation for the whole pop
        var transition = new MaterialSharedAxis(MaterialSharedAxis.X, /* entering */ false);
        transition.SetDuration(300);
        global::Android.Transitions.TransitionManager.BeginDelayedTransition(m_navigationContainer, transition);

        // Remove the currently visible pushed view
        m_navigationContainer.RemoveView(m_currentNativeContentView);

        // Remove all intermediate pushed views still attached to the container.
        // The native stack contains, from bottom to top: [root, push1, push2, ...]. We keep the root.
        while (m_nativeNavigationStack.Count > 1)
        {
            var intermediate = m_nativeNavigationStack.Pop();
            m_navigationContainer.RemoveView(intermediate);
        }

        // Restore the root view
        var rootNativeView = m_nativeNavigationStack.Pop();
        rootNativeView.Visibility = ViewStates.Visible;
        m_currentNativeContentView = rootNativeView;

        // Disconnect handlers for every popped MAUI page
        foreach (var entry in popped)
        {
            entry.Page.Content.DisconnectHandlers();
        }

        UpdateHeaderToolbarFromBottomSheet();
        m_navigationBackCallback?.UpdateEnabled();
    }

    private void CleanupNavigationStack()
    {
        while (m_nativeNavigationStack.Count > 0)
        {
            m_nativeNavigationStack.Pop();
        }
        
        while (m_bottomSheet.NavigationStack.Count > 0)
        {
            var entry = m_bottomSheet.NavigationStack.Pop();
            entry.Page.Content.DisconnectHandlers();
        }
    }

    /// <summary>
    /// Handles back button during navigation. Returns true if navigation was popped.
    /// </summary>
    private bool TryHandleNavigationBack()
    {
        if (m_bottomSheet.CanPopNavigation)
        {
            _ = m_bottomSheet.PopAsync();
            return true;
        }
        return false;
    }

    internal class NavigationBackPressedCallback : global::AndroidX.Activity.OnBackPressedCallback
    {
        private readonly BottomSheetHandler m_bottomSheetHandler;

        public NavigationBackPressedCallback(BottomSheetHandler bottomSheetHandler)
            : base(enabled: true)
        {
            m_bottomSheetHandler = bottomSheetHandler;
            UpdateEnabled();
        }

        public void UpdateEnabled()
        {
            // Only intercept back button when there is navigation content to pop
            Enabled = m_bottomSheetHandler.m_bottomSheet?.CanPopNavigation ?? false;
        }

        public override void HandleOnBackPressed()
        {
            if (m_bottomSheetHandler.m_bottomSheet?.CanPopNavigation ?? false)
            {
                _ = m_bottomSheetHandler.m_bottomSheet.PopAsync();
            }
        }
    }
}



