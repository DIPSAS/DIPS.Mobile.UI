using Android.Hardware;

namespace Playground.HåvardSamples.Scanning.Android;

/**
 * Stores a preview size and a corresponding same-aspect-ratio picture size. To avoid distorted
 * preview images on some devices, the picture size must be set to a size that is the same aspect
 * ratio as the preview size or the preview may end up being distorted. If the picture size is null,
 * then there is no picture size with the same aspect ratio as the preview size.
 */
public class CameraSizePair
{

    public CameraSizePair(Camera.Size preview, Camera.Size? picture)
    {
        Preview = new global::Android.Util.Size(preview.Width, preview.Height);
        if (picture != null)
        {
            Picture = new global::Android.Util.Size(picture.Width, picture.Height);
        }
    }

    public CameraSizePair(global::Android.Util.Size preview, global::Android.Util.Size? picture)
    {
        Preview = preview;
        if (picture != null)
        {
            Picture = picture;
        }
    }
    
    public global::Android.Util.Size Preview { get; set; }
    public global::Android.Util.Size? Picture { get; set; }
   
}