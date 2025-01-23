
using Android.OS;
using Android.Views;
using DIPS.Mobile.UI.API.Library.Android;

namespace DIPS.Mobile.UI.API.Library;

/// <summary>
/// Taken from: https://supportcenter.devexpress.com/ticket/details/t1208656/adding-more-information-on-contentpage-hidesoftinputontapped#c43630fa-4759-4fb3-bc13-593024a70426
/// </summary>
public class MauiAppCompatActivity : Microsoft.Maui.MauiAppCompatActivity
{
    private long m_touchDownTime;

    public override bool DispatchTouchEvent(MotionEvent? e) => e?.Action switch
    {
        MotionEventActions.Down => DispatchMotionDown(e),
        MotionEventActions.Up => DispatchMotionUp(e),
        _ => base.DispatchTouchEvent(e)
    };

    private bool DispatchMotionUp(MotionEvent motionEvent)
    {
        var focused = CurrentFocus;
        var dispatchTouchEventResult = base.DispatchTouchEvent(motionEvent);

        // if not focused, or dispatch is canceled, or they're dragging
        if (focused is null || !dispatchTouchEventResult || SystemClock.ElapsedRealtime() - m_touchDownTime > 150)
        {
            // just return
            return dispatchTouchEventResult;
        }

        var touchCoordinates = new int[2];
        focused.GetLocationOnScreen(touchCoordinates);
        var x = motionEvent.RawX + focused.Left - touchCoordinates[0];
        var y = motionEvent.RawY + focused.Top - touchCoordinates[1];

        //If the touch position is outside the element then we may want to hide the keyboard and remove focus from the element
        var touchedOutsideFocusedElement = x < focused.Left || x > focused.Right || y < focused.Top || y > focused.Bottom;
        if (touchedOutsideFocusedElement && HideSoftInputOnTapHandlerMappings.RequiresKeyboardDismissal(focused))
        {
            Task.Run(async () =>
            {
                // Give it a main thread cycle to update the focus
                await Task.Yield();

                var focusChanged = CurrentFocus != focused;
                if (!focusChanged)
                {
                    // focus stayed on the same item, even if user tapped away
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        // Dismiss keyboard and remove focus
                        HideSoftInputOnTapHandlerMappings.DismissKeyboard(focused);
                        focused.ClearFocus();
                    });
                }
            });
        }

        return true;
    }

    private bool DispatchMotionDown(MotionEvent motionEvent)
    {
        m_touchDownTime = SystemClock.ElapsedRealtime();
        return base.DispatchTouchEvent(motionEvent);
    }
}