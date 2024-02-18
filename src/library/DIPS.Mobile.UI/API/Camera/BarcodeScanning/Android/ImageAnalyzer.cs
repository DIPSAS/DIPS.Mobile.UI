using AndroidX.Camera.Core;

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
}