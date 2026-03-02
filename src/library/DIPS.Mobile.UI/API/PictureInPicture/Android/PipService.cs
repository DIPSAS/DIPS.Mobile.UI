using Android.Content.PM;
using Android.OS;
using Android.Util;

namespace DIPS.Mobile.UI.API.PictureInPicture;

public static partial class PipService
{
    public static partial bool IsSupported
    {
        get
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                return false;

            return Platform.AppContext.PackageManager
                       ?.HasSystemFeature(PackageManager.FeaturePictureInPicture) == true;
        }
    }

    public static partial void Enter() => Enter(9, 16);

    public static partial void Enter(int ratioWidth, int ratioHeight)
    {
        if (!IsSupported)
            return;

        var activity = Platform.CurrentActivity;
        if (activity is null)
            return;

        var paramsBuilder = new Android.App.PictureInPictureParams.Builder();

        if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
        {
            paramsBuilder.SetSeamlessResizeEnabled(true);
        }

        paramsBuilder.SetAspectRatio(new Rational(ratioWidth, ratioHeight));

        activity.EnterPictureInPictureMode(paramsBuilder.Build());
    }
}
