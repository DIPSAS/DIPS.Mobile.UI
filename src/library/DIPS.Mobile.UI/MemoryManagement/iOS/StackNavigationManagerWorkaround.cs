// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.MemoryManagement;

internal static partial class StackNavigationManagerWorkaround
{
    internal static partial object? CaptureNavigationManager(NavigationPage navigationPage) => null;
    internal static partial void ClearLeakedReferences(object? capturedManager) { }
}
