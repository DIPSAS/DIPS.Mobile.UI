using System.Reflection;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.MemoryManagement;

internal static partial class StackNavigationManagerWorkaround
{
    private static readonly FieldInfo? CurrentPageField =
        typeof(StackNavigationManager).GetField("_currentPage", BindingFlags.NonPublic | BindingFlags.Instance);

    private static readonly FieldInfo? FragmentContainerViewField =
        typeof(StackNavigationManager).GetField("_fragmentContainerView", BindingFlags.NonPublic | BindingFlags.Instance);

    private static readonly FieldInfo? FragmentManagerField =
        typeof(StackNavigationManager).GetField("_fragmentManager", BindingFlags.NonPublic | BindingFlags.Instance);

    private static readonly PropertyInfo? NavigationStackProperty =
        typeof(StackNavigationManager).GetProperty("NavigationStack", BindingFlags.Public | BindingFlags.Instance);

    internal static partial object? CaptureNavigationManager(NavigationPage navigationPage)
    {
        if (navigationPage.Handler is not NavigationViewHandler handler)
            return null;

        return handler.StackNavigationManager;
    }

    internal static partial void ClearLeakedReferences(object? capturedManager)
    {
        if (capturedManager is not StackNavigationManager stackNavigationManager)
            return;

        // Clear the leaked fields that StackNavigationManager.Disconnect() fails to clear
        // in the buggy MAUI version. If MAUI has already fixed these, setting them to null again is harmless.
        // Must be called AFTER Disconnect() so that event unsubscriptions happen first.
        CurrentPageField?.SetValue(stackNavigationManager, null);
        FragmentContainerViewField?.SetValue(stackNavigationManager, null);
        FragmentManagerField?.SetValue(stackNavigationManager, null);
        NavigationStackProperty?.SetValue(stackNavigationManager, Array.Empty<IView>());

        GarbageCollection.Print("🔧 Cleared StackNavigationManager leaked references (workaround for dotnet/maui#34456)");
    }
}
