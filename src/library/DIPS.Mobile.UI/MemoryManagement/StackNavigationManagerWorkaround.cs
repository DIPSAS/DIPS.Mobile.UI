namespace DIPS.Mobile.UI.MemoryManagement;

/// <summary>
/// Workaround for .NET 10 MAUI regression where <c>StackNavigationManager.Disconnect()</c> on Android
/// does not clear <c>_currentPage</c>, <c>_fragmentContainerView</c>, and <c>_fragmentManager</c> fields,
/// causing memory leaks when modal pages wrapped in NavigationPage are popped.
/// See: https://github.com/dotnet/maui/issues/34456
/// </summary>
internal static partial class StackNavigationManagerWorkaround
{
    /// <summary>
    /// Captures a reference to the StackNavigationManager while the handler is still connected.
    /// Must be called when the NavigationPage is first tracked (before MAUI auto-disconnects).
    /// </summary>
    internal static partial object? CaptureNavigationManager(NavigationPage navigationPage);

    /// <summary>
    /// Clears leaked fields on a previously captured StackNavigationManager.
    /// Safe to call after handlers have been disconnected.
    /// </summary>
    internal static partial void ClearLeakedReferences(object? capturedManager);
}
