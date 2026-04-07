namespace DIPS.Mobile.UI.Components.FullScreenPresenter;

/// <summary>
/// A service to present any view in full screen using modal navigation.
/// The view is temporarily moved to a modal page, preserving full interactivity (scrolling, gestures, etc.).
/// A screenshot placeholder is left in the original position to avoid visual gaps.
/// </summary>
public static class FullScreenPresenterService
{
    private static Layout? s_originalParent;
    private static int s_originalIndex;
    private static View? s_originalView;
    private static Image? s_placeholder;

    /// <summary>
    /// Presents a view in full screen by moving it to a modal page.
    /// A screenshot placeholder replaces the view in its original parent.
    /// </summary>
    /// <param name="content">The view to present in full screen. Must be a child of a Layout.</param>
    public static async Task Present(View content)
    {
        if (IsPresenting)
            return;

        var page = Application.Current?.MainPage;
        if (page is null)
            return;

        if (content.Parent is not Layout parentLayout)
            return;

        // Capture a screenshot to use as placeholder
        ImageSource? placeholderSource = null;
        try
        {
            var result = await content.CaptureAsync();
            if (result is not null)
            {
                var stream = await result.OpenReadAsync();
                placeholderSource = ImageSource.FromStream(() => stream);
            }
        }
        catch
        {
            // If capture fails, we still proceed — the placeholder will just be empty space
        }

        // Remember where the view lives
        s_originalParent = parentLayout;
        s_originalView = content;
        s_originalIndex = parentLayout.Children.IndexOf(content);

        // Create a placeholder with the same dimensions
        s_placeholder = new Image
        {
            Source = placeholderSource,
            Aspect = Aspect.Fill,
            WidthRequest = content.Width,
            HeightRequest = content.Height
        };

        // Swap: remove original view, insert placeholder at same position
        parentLayout.Children.RemoveAt(s_originalIndex);
        parentLayout.Children.Insert(s_originalIndex, s_placeholder);

        IsPresenting = true;

        var fullScreenPage = new FullScreenPage(content);
        await page.Navigation.PushModalAsync(fullScreenPage, animated: true);
    }

    /// <summary>
    /// Closes the full screen presentation and returns the view to its original position.
    /// </summary>
    public static async Task Close()
    {
        if (!IsPresenting)
            return;

        var page = Application.Current?.MainPage;
        if (page is null)
            return;

        await page.Navigation.PopModalAsync(animated: true);

        // Move the view back to its original parent
        if (s_originalParent is not null && s_originalView is not null && s_placeholder is not null)
        {
            var placeholderIndex = s_originalParent.Children.IndexOf(s_placeholder);
            if (placeholderIndex >= 0)
            {
                s_originalParent.Children.RemoveAt(placeholderIndex);
                s_originalParent.Children.Insert(placeholderIndex, s_originalView);
            }
        }

        s_originalParent = null;
        s_originalView = null;
        s_placeholder = null;
        IsPresenting = false;
    }

    /// <summary>
    /// Determines if a view is currently being presented in full screen.
    /// </summary>
    public static bool IsPresenting { get; private set; }
}
