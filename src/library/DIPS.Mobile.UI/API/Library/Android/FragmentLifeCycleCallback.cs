using Android.OS;
using Android.Views;
using AndroidX.Core.View;
using AndroidX.Fragment.App;
using DIPS.Mobile.UI.API.Diagnostics;
using DIPS.Mobile.UI.API.Library.Android;
using Google.Android.Material.BottomSheet;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Library;

public class FragmentLifeCycleCallback : FragmentManager.FragmentLifecycleCallbacks
{
    public override void OnFragmentStarted(FragmentManager fm, Fragment f)
    {
        if (f is DialogFragment dialogFragment)
        {
            if (f is not BottomSheetDialogFragment)
            {
                TryInheritWindowFlags(dialogFragment);
                // Register the DialogFragment so ContentPage can find it
                // Also immediately set the status bar color since OnAppearing may have already been called
                StatusBarHandler.RegisterDialogFragmentForPage(dialogFragment);
                s_currentDialogFragmentReferenceStack ??= new Stack<WeakReference<DialogFragment>?>();
                s_currentDialogFragmentReferenceStack.Push(new WeakReference<DialogFragment>(dialogFragment));
            }

            TryEnableCustomHideSoftInputOnTappedImplementation(dialogFragment);
            
            s_dialogFragmentStack.Push(new WeakReference<DialogFragment>(dialogFragment));
            LayoutDiagnosticsService.ElevateAboveDialog(dialogFragment.Dialog);
        }
     
        base.OnFragmentStarted(fm, f);
    }

    public override void OnFragmentDestroyed(FragmentManager fm, Fragment f)
    {
        if (f is DialogFragment dialogFragment)
        {
            RemoveFromDialogStack(dialogFragment);
            
            // Re-elevate above the dialog that's still visible underneath, or fall back to activity
            if (TryGetTopmostDialog(out var remainingDialog))
                LayoutDiagnosticsService.ElevateAboveDialog(remainingDialog);
            else
                LayoutDiagnosticsService.RestoreToActivityContent();
            
            if (dialogFragment is not BottomSheetDialogFragment)
            {
                RemoveCurrentDialogFragment(dialogFragment);
            }
        }

        base.OnFragmentDestroyed(fm, f);
    }
    
    public override void OnFragmentStopped(FragmentManager fm, Fragment f)
    {
        if (f is DialogFragment dialogFragment && f is not BottomSheetDialogFragment)
        {
            // Unregister the DialogFragment when it's stopped/dismissed
            StatusBarHandler.UnregisterDialogFragment(dialogFragment);
        }
        
        base.OnFragmentStopped(fm, f);
    }

    private static void TryEnableCustomHideSoftInputOnTappedImplementation(DialogFragment dialogFragment)
    {
        if(!DUI.ShouldUseCustomHideSoftInputOnTappedImplementation)
            return;
        
        // Enable HideSoftInputOnTapped for Modals and BottomSheet
        // Does not work out of the box in MAUI yet..
        var originalWindow = dialogFragment.Dialog?.Window;
        var originalCallback = originalWindow?.Callback;

        if (originalWindow is not null && originalCallback is not null)
        {
            if (dialogFragment.Dialog is { Window: not null })
            {
                dialogFragment.Dialog.Window.Callback = new UnfocusWindowCallback(originalWindow, originalCallback);
            }
        }
    }

    private void TryInheritWindowFlags(DialogFragment dialogFragment)
    {
        var activity = Platform.CurrentActivity;
        var window = activity?.Window;
        
        if (window is
            {
                Attributes: not null
            }) //Make sure the dialog inherits window flag from the activity, useful when the activity is set as secured.
        {
            var flags = window.Attributes.Flags;
            dialogFragment.Dialog?.Window?.SetFlags(flags, flags);
        }
    }

    public static DialogFragment? CurrentDialogFragment
    {
        get
        {
            while (s_currentDialogFragmentReferenceStack?.Count > 0)
            {
                var weakReference = s_currentDialogFragmentReferenceStack.Peek();
                if (weakReference?.TryGetTarget(out var dialogFragment) == true)
                {
                    try
                    {
                        if (dialogFragment.Handle != IntPtr.Zero)
                        {
                            return dialogFragment;
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                }

                s_currentDialogFragmentReferenceStack.Pop();
            }

            return null;
        }
    }

    private static Stack<WeakReference<DialogFragment>?>? s_currentDialogFragmentReferenceStack;
    
    private static readonly Stack<WeakReference<DialogFragment>> s_dialogFragmentStack = new();

    private static void RemoveCurrentDialogFragment(DialogFragment target)
    {
        while (s_currentDialogFragmentReferenceStack?.Count > 0)
        {
            var weakReference = s_currentDialogFragmentReferenceStack.Peek();
            if (weakReference?.TryGetTarget(out var currentDialogFragment) != true)
            {
                s_currentDialogFragmentReferenceStack.Pop();
                continue;
            }

            try
            {
                if (currentDialogFragment.Handle == IntPtr.Zero || target.Handle == IntPtr.Zero || currentDialogFragment.Equals(target))
                {
                    s_currentDialogFragmentReferenceStack.Pop();
                }
            }
            catch (ObjectDisposedException)
            {
                s_currentDialogFragmentReferenceStack.Pop();
            }

            return;
        }
    }

    private static void RemoveFromDialogStack(DialogFragment target)
    {
        // Rebuild the stack without the destroyed fragment
        var temp = new Stack<WeakReference<DialogFragment>>();
        while (s_dialogFragmentStack.Count > 0)
        {
            var weakRef = s_dialogFragmentStack.Pop();
            if (!weakRef.TryGetTarget(out var fragment))
                continue;

            try
            {
                if (fragment.Handle == IntPtr.Zero || target.Handle == IntPtr.Zero)
                    continue;
                if (fragment.Equals(target))
                    continue;
            }
            catch (ObjectDisposedException)
            {
                continue;
            }

            temp.Push(weakRef);
        }

        // Restore order
        while (temp.Count > 0)
            s_dialogFragmentStack.Push(temp.Pop());
    }

    private static bool TryGetTopmostDialog(out global::Android.App.Dialog? dialog)
    {
        while (s_dialogFragmentStack.Count > 0)
        {
            var weakRef = s_dialogFragmentStack.Peek();
            if (weakRef.TryGetTarget(out var fragment) && fragment.Handle != IntPtr.Zero && fragment.Dialog is not null)
            {
                dialog = fragment.Dialog;
                return true;
            }
            
            // Dead reference, discard
            s_dialogFragmentStack.Pop();
        }

        dialog = null;
        return false;
    }

#if ANDROID
    public sealed class InsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
    {
        readonly View m_scrim;
        public InsetsListener(View scrim) => m_scrim = scrim;

        public WindowInsetsCompat? OnApplyWindowInsets(View? v, WindowInsetsCompat? insets)
        {
            var ins = insets?.GetInsets(WindowInsetsCompat.Type.StatusBars());
            if (ins == null)
            {
                return insets;
            }

            var top = ins.Top;
            var lp = m_scrim.LayoutParameters;
            if (lp == null)
            {
                return insets;
            }

            lp.Height = top;
            m_scrim.LayoutParameters = lp;

            return insets;
        }
    }
#endif
}