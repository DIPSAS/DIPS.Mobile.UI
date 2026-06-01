using Android.Content;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Transition.Platform;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using View = Microsoft.Maui.Controls.View;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.BottomSheets;

/// <summary>
/// Håndterer intern navigasjon (push/pop) i bottom sheet på Android.
/// </summary>
public partial class BottomSheetHandler
{
    // Navigasjonstilstand
    private FrameLayout? m_navigationContainer;
    private AView? m_currentNativeContentView;
    private Stack<AView> m_nativeNavigationStack = new();
    private NavigationBackPressedCallback? m_navigationBackCallback;

    /// <summary>
    /// Setter opp navigasjonscontaineren og legger til innholdsviewet i bottom sheet-layouten.
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
    }

    internal void PushNavigationContent(View content, string? title)
    {
        var mauiContext = DUI.GetCurrentMauiContext;
        if (mauiContext is null || m_navigationContainer is null || m_bottomSheetLayout is null) return;

        // Lagre gjeldende native view på stacken
        m_nativeNavigationStack.Push(m_currentNativeContentView!);

        // Opprett nytt native view
        var newNativeView = content.ToPlatform(mauiContext);

        // Material 3 shared axis-overgang (X-akse, fremover)
        var transition = new MaterialSharedAxis(MaterialSharedAxis.X, /* entering */ true);
        transition.SetDuration(300);
        global::Android.Transitions.TransitionManager.BeginDelayedTransition(m_navigationContainer, transition);

        // Skjul gammel, vis ny
        m_currentNativeContentView!.Visibility = ViewStates.Gone;
        m_navigationContainer.AddView(newNativeView);
        m_currentNativeContentView = newNativeView;

        // Oppdater toolbaren med tittel og tilbake-knapp
        UpdateHeaderToolbarForNavigation(title);
        m_navigationBackCallback?.UpdateEnabled();
    }

    internal void PopNavigationContent(BottomSheetNavigationEntry popped)
    {
        if (m_navigationContainer is null || m_nativeNavigationStack.Count == 0) return;

        var previousNativeView = m_nativeNavigationStack.Pop();

        // Material 3 shared axis-overgang (X-akse, bakover)
        var transition = new MaterialSharedAxis(MaterialSharedAxis.X, /* entering */ false);
        transition.SetDuration(300);
        global::Android.Transitions.TransitionManager.BeginDelayedTransition(m_navigationContainer, transition);

        // Fjern gjeldende, gjenopprett forrige
        m_navigationContainer.RemoveView(m_currentNativeContentView);
        previousNativeView.Visibility = ViewStates.Visible;
        m_currentNativeContentView = previousNativeView;

        // Koble fra handlers for det poppede MAUI-viewet
        popped.Content.DisconnectHandlers();

        // Oppdater toolbar: enten gjenopprett rottilstand eller vis forrige pushede entry
        if (m_nativeNavigationStack.Count == 0)
        {
            UpdateHeaderToolbarFromBottomSheet();
        }
        else
        {
            var previousEntry = m_bottomSheet.NavigationStack.Peek();
            UpdateHeaderToolbarForNavigation(previousEntry.Title);
        }

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
            entry.Content.DisconnectHandlers();
        }
    }

    /// <summary>
    /// Håndterer tilbake-knapp under navigasjon. Returnerer true om navigasjon ble poppet.
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
            // Bare fang opp tilbake-knapp når det er navigasjonsinnhold å poppe
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



