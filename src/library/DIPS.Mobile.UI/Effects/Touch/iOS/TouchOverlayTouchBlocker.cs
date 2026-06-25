using UIKit;

namespace DIPS.Mobile.UI.Effects.Touch.iOS;

internal static class TouchOverlayTouchBlocker
{
    private static readonly Lock s_lock = new();
    private static readonly HashSet<int> s_activeTokenIds = [];
    private static int s_nextTokenId;

    internal static bool IsBlocked
    {
        get
        {
            lock (s_lock)
            {
                return s_activeTokenIds.Count > 0;
            }
        }
    }

    internal static bool IsBlockedByActivePopover => UIApplication.SharedApplication.ConnectedScenes
        .OfType<UIWindowScene>()
        .SelectMany(scene => scene.Windows)
        .Any(window => IsPopoverPresented(window.RootViewController));

    internal static IDisposable Block()
    {
        lock (s_lock)
        {
            var tokenId = ++s_nextTokenId;
            s_activeTokenIds.Add(tokenId);
            return new Token(tokenId);
        }
    }

    private sealed class Token(int tokenId) : IDisposable
    {
        private bool m_isDisposed;

        public void Dispose()
        {
            if (m_isDisposed)
                return;

            lock (s_lock)
            {
                s_activeTokenIds.Remove(tokenId);
            }

            m_isDisposed = true;
        }
    }

    private static bool IsPopoverPresented(UIViewController? viewController)
    {
        while (viewController is not null)
        {
            if (viewController.ModalPresentationStyle == UIModalPresentationStyle.Popover &&
                !viewController.IsBeingDismissed)
            {
                return true;
            }

            viewController = viewController.PresentedViewController;
        }

        return false;
    }
}