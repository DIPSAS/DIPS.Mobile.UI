using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace DIPS.Mobile.UI.Components.FullScreenPresenter;

/// <summary>
/// A service to present any view in full screen using a native overlay.
/// The view is moved natively to a fullscreen overlay on top of the window, preserving all interactivity.
/// </summary>
public static partial class FullScreenPresenterService
{
    /// <summary>
    /// Presents a view in full screen using a native overlay on the window.
    /// The view keeps all interactivity (scrolling, gestures, etc.).
    /// </summary>
    /// <param name="content">The view to present in full screen.</param>
    /// <param name="closesOnAppBackgrounded">When true, the full screen presentation will automatically close when the app is backgrounded.</param>
    public static async Task Present(View content, bool closesOnAppBackgrounded = false)
    {
        if (IsPresenting)
            return;

        IsPresenting = true;

        if (closesOnAppBackgrounded)
        {
            SubscribeToAppLifecycle();
        }

        PresentOnPlatform(content);

        await Task.CompletedTask;
    }

    /// <summary>
    /// Closes the current full screen presentation and returns the view to its original position.
    /// </summary>
    public static async Task Close()
    {
        if (!IsPresenting)
            return;

        UnsubscribeFromAppLifecycle();

        CloseOnPlatform();

        IsPresenting = false;

        await Task.CompletedTask;
    }

    /// <summary>
    /// Determines if a view is currently being presented in full screen.
    /// </summary>
    public static bool IsPresenting { get; private set; }

    static partial void PresentOnPlatform(View content);
    static partial void CloseOnPlatform();

    private static void SubscribeToAppLifecycle()
    {
        var app = Application.Current;
        if (app is null)
            return;

        var window = app.Windows.FirstOrDefault();
        if (window is not null)
        {
            window.Deactivated += OnWindowDeactivated;
        }
    }

    private static void UnsubscribeFromAppLifecycle()
    {
        var app = Application.Current;
        if (app is null)
            return;

        var window = app.Windows.FirstOrDefault();
        if (window is not null)
        {
            window.Deactivated -= OnWindowDeactivated;
        }
    }

    private static async void OnWindowDeactivated(object? sender, EventArgs e)
    {
        await Close();
    }
}
