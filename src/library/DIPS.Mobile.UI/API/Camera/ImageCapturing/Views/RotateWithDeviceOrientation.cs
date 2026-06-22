using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views;

internal static class RotateWithDeviceOrientationExtension
{
    /// <summary>
    /// Keeps a control upright when the device turns.
    /// Stops and unsubscribes once the control leaves the screen.
    /// </summary>
    public static void RotateWithDeviceOrientation(this VisualElement view)
    {
        ArgumentNullException.ThrowIfNull(view);

        void RotateToOrientation(OrientationDegree orientationDegree)
        {
            _ = view.RotateTo(orientationDegree.OrientationDegreeToMauiRotation());
        }

        void StopRotatingWhenRemovedFromScreen(object? sender, HandlerChangingEventArgs args)
        {
            if (args.NewHandler is not null)
                return;

            DUI.OrientationChanged -= RotateToOrientation;
            view.HandlerChanging -= StopRotatingWhenRemovedFromScreen;
        }

        DUI.OrientationChanged += RotateToOrientation;
        view.HandlerChanging += StopRotatingWhenRemovedFromScreen;
    }
}
