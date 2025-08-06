using DIPS.Mobile.UI.Components.Loading.Android;
using DIPS.Mobile.UI.Extensions.Android;
using Google.Android.Material.ProgressIndicator;

namespace DIPS.Mobile.UI.Components.Loading;

public partial class ActivityIndicator
{
    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName != WidthRequestProperty.PropertyName)
            return;

        SetIndicatorSize();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        SetIndicatorSize();
    }

    private void SetIndicatorSize()
    {
        if (Handler is ActivityIndicatorHandler { PlatformView: CircularProgressIndicator circularProgressIndicator })
        {
            circularProgressIndicator.IndicatorSize = (int)WidthRequest.ToMauiPixel();
        }
    }
}