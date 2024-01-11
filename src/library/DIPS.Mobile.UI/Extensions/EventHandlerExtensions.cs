namespace DIPS.Mobile.UI.Extensions;

public static class EventHandlerExtensions
{
    /// <summary>
    /// Checks the invocation list to see if there is any subscribers to the event.
    /// </summary>
    /// <param name="eventHandler">The event to check for subscriptions.</param>
    /// <returns>bool</returns>
    public static bool HasSubscriptions(this EventHandler eventHandler)
    {
        return eventHandler.GetInvocationList().Length > 0;
    }
}