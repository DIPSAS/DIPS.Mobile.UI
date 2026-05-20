using AndroidX.Camera.Core;
using Size = Android.Util.Size;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning.Android;

public class ImageAnalyzer : Java.Lang.Object, ImageAnalysis.IAnalyzer
{
    private readonly Action<IImageProxy> m_analyze;

    public static ImageAnalyzer Create(Action<IImageProxy> analyze) => new(analyze);
    public ImageAnalyzer(Action<IImageProxy> analyze)
    {
        m_analyze = analyze;
    }

    public void Analyze(IImageProxy p0)
    {
        m_analyze.Invoke(p0);
    }
    // See:
    // https://github.com/dotnet/android-libraries/issues/767
    // https://github.com/dotnet/android/pull/9656
#pragma warning disable CS8603 // Possible null reference return.
    public global::Android.Util.Size DefaultTargetResolution => null;
#pragma warning restore CS8603 // Possible null reference return.
}