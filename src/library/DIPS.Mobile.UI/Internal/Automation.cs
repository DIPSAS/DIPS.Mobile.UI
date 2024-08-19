namespace DIPS.Mobile.UI.Internal;

internal static class Automation
{
   /// <summary>
   /// /// Will insert a DUI prefix. The prefix is important as automation ids can be named the same. This is helpful when dealing with memory leaks!
   /// </summary>
   /// <param name="automationId"></param>
   /// <typeparam name="T">The type where the visual element is used in.</typeparam>
   /// <returns></returns>
    public static string ToDUIAutomationId<T>(this string automationId) where T : Element
    {
        return $"DUI.{typeof(T).Name}.{automationId}";
    }
}