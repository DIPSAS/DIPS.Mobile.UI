namespace DIPS.Mobile.UI.Components.FullScreenPresenter;

/// <summary>
/// A service to present any view in full screen using modal navigation.
/// A screenshot of the view is captured and displayed in a modal page.
/// The original view is never moved or modified.
/// </summary>
public static class FullScreenPresenterService
{
    /// <summary>
    /// Captures a screenshot of the view and presents it full screen in a modal page.
    /// The original view is not modified.
    /// </summary>
    /// <param name="content">The view to capture and display in full screen.</param>
    public static async Task Present(View content)
    {
        if (IsPresenting)
            return;

        var page = Application.Current?.MainPage;
        if (page is null)
            return;

        // Capture a screenshot of the view
        var result = await content.CaptureAsync();
        if (result is null)
            return;

        var stream = await result.OpenReadAsync();
        var imageSource = ImageSource.FromStream(() => stream);

        IsPresenting = true;

        var fullScreenPage = new FullScreenPage(imageSource);
        await page.Navigation.PushModalAsync(fullScreenPage, animated: true);
    }

    /// <summary>
    /// Closes the current full screen presentation.
    /// </summary>
    public static async Task Close()
    {
        if (!IsPresenting)
            return;

        var page = Application.Current?.MainPage;
        if (page is null)
            return;

        await page.Navigation.PopModalAsync(animated: true);
        IsPresenting = false;
    }

    /// <summary>
    /// Determines if a view is currently being presented in full screen.
    /// </summary>
    public static bool IsPresenting { get; private set; }
}
