namespace DIPS.Mobile.UI.Components.Alerting.Alert;

public class AlertViewService
{
    internal static event Action? OnAnimationTriggered;
    
    /// <summary>
    /// Will trigger the animation of all AlertViews currently displayed
    /// <remarks><see cref="AlertView"/> must have <see cref="AlertView.ShouldAnimate"/> set to true</remarks>
    /// </summary>
    public static void TriggerAnimation()
    {
        OnAnimationTriggered?.Invoke();
    }
}