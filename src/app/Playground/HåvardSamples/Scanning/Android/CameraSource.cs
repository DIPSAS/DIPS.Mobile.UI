using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Hardware;
using Android.Media.TV;
using Android.Views;
using AndroidX.ConstraintLayout.Core.Motion.Utils;
using Java.Nio;
using Java.Util;
using Camera = Android.Hardware.Camera;
using Size = Android.Util.Size;
using Utils = Microsoft.VisualBasic.CompilerServices.Utils;

namespace Playground.HåvardSamples.Scanning.Android;
#pragma warning disable CS0618 // Type or member is obsolete

// Based on this:
// https://developer.android.com/codelabs/camerax-getting-started#0
public class CameraSource : Java.Lang.Object, Camera.IPreviewCallback
{
    private int m_rotationDegrees;
    private const ImageFormatType ImageFormat = ImageFormatType.Nv21;
    private const int MinCameraPreviewWidth = 400;
    private const int MaxCameraPreviewWidth = 1300;
    private const int DefaultRequestedCameraPreviewWidth = 640;
    private const int DefaultRequestedCameraPreviewHeight = 360;
    private const float RequestedCameraFps = 30.0f;
    private const float AspectRatioTolerance = 0.01f;

    /** Returns the preview size that is currently in use by the underlying camera.  */
    internal global::Android.Util.Size? m_previewSize;

    private Dictionary<byte[],ByteBuffer> m_bytesToByteBuffer;

    public Camera? Start(Preview preview, Context context)
    {
        var camera = Camera.Open();
        if (camera == null) return null;

        var parameters = camera.GetParameters();
        SetPreviewAndPictureSize(camera, context, preview, parameters);
        SetRotation(context, camera, camera.GetParameters());

        var previewFpsRange = SelectPreviewFpsRange(camera);
        parameters.SetPreviewFpsRange(previewFpsRange[(int)global::Android.Hardware.Preview.FpsMinIndex],
            previewFpsRange[(int)global::Android.Hardware.Preview.FpsMaxIndex]);

        parameters.PreviewFormat = ImageFormat;

        if (parameters.SupportedFocusModes.Contains(Camera.Parameters.FocusModeContinuousPicture))
        {
            parameters.FocusMode = Camera.Parameters.FocusModeContinuousVideo;
        }
        
        camera.SetParameters(parameters);
        
        camera.SetPreviewCallbackWithBuffer(this);


        if (m_previewSize != null)
        {
            camera.AddCallbackBuffer(CreatePreviewBuffer(m_previewSize));
            camera.AddCallbackBuffer(CreatePreviewBuffer(m_previewSize));
            camera.AddCallbackBuffer(CreatePreviewBuffer(m_previewSize));
            camera.AddCallbackBuffer(CreatePreviewBuffer(m_previewSize));
        }

        return camera;
        //inspiration = https://github.com/googlesamples/mlkit/blob/master/android/material-showcase/app/src/main/java/com/google/mlkit/md/camera/CameraSource.kt
    }

    private Byte[] CreatePreviewBuffer(Size previewSize)
    {
        var bitsPerPixel = global::Android.Graphics.ImageFormat.GetBitsPerPixel(ImageFormat);
        var sizeInBits = (long)previewSize.Height * previewSize.Width * bitsPerPixel;
        var bufferSize = Math.Ceiling(((sizeInBits / 8.0)) + 1);
        
        // Creating the byte array this way and wrapping it, as opposed to using .allocate(),
        // should guarantee that there will be an array to work with.
        var byteArray = new byte[(int)bufferSize];
        var byteBuffer = ByteBuffer.Wrap(byteArray);


        m_bytesToByteBuffer = new Dictionary<byte[], ByteBuffer>();
        m_bytesToByteBuffer[byteArray] = byteBuffer;
        return byteArray;
    }

    // private fun createPreviewBuffer(previewSize: Size): ByteArray {
    //     val bitsPerPixel = ImageFormat.getBitsPerPixel(IMAGE_FORMAT)
    //     val sizeInBits = previewSize.height.toLong() * previewSize.width.toLong() * bitsPerPixel.toLong()
    //     val bufferSize = ceil(sizeInBits / 8.0).toInt() + 1
    //
    //     // Creating the byte array this way and wrapping it, as opposed to using .allocate(),
    //     // should guarantee that there will be an array to work with.
    //     val byteArray = ByteArray(bufferSize)
    //     val byteBuffer = ByteBuffer.wrap(byteArray)
    //     check(!(!byteBuffer.hasArray() || !byteBuffer.array()!!.contentEquals(byteArray))) {
    //         // This should never happen. If it does, then we wouldn't be passing the preview content to
    //         // the underlying detector later.
    //         "Failed to create valid buffer for camera source."
    //     }
    //
    //     bytesToByteBuffer[byteArray] = byteBuffer
    //     return byteArray
    // }
    // // private fun createCamera(): Camera {
    // //     val camera = Camera.open() ?: throw IOException("There is no back-facing camera.")
    // //     val parameters = camera.parameters
    // //     setPreviewAndPictureSize(camera, parameters)
    // //     setRotation(camera, parameters)
    // //
    // //     val previewFpsRange = selectPreviewFpsRange(camera)
    // //         ?: throw IOException("Could not find suitable preview frames per second range.")
    // //     parameters.setPreviewFpsRange(
    // //         previewFpsRange[Parameters.PREVIEW_FPS_MIN_INDEX],
    // //         previewFpsRange[Parameters.PREVIEW_FPS_MAX_INDEX]
    // //     )
    // //
    // //     parameters.previewFormat = IMAGE_FORMAT
    // //
    // //     if (parameters.supportedFocusModes.contains(Parameters.FOCUS_MODE_CONTINUOUS_VIDEO)) {
    // //         parameters.focusMode = Parameters.FOCUS_MODE_CONTINUOUS_VIDEO
    // //     } else {
    // //         Log.i(TAG, "Camera auto focus is not supported on this device.")
    // //     }
    // //
    // //     camera.parameters = parameters
    // //
    // //     camera.setPreviewCallbackWithBuffer(processingRunnable::setNextFrame)
    // //
    // //     // Four frame buffers are needed for working with the camera:
    // //     //
    // //     //   one for the frame that is currently being executed upon in doing detection
    // //     //   one for the next pending frame to process immediately upon completing detection
    // //     //   two for the frames that the camera uses to populate future preview images
    // //     //
    // //     // Through trial and error it appears that two free buffers, in addition to the two buffers
    // //     // used in this code, are needed for the camera to work properly. Perhaps the camera has one
    // //     // thread for acquiring images, and another thread for calling into user code. If only three
    // //     // buffers are used, then the camera will spew thousands of warning messages when detection
    // //     // takes a non-trivial amount of time.
    // //     previewSize?.let {
    // //         camera.addCallbackBuffer(createPreviewBuffer(it))
    // //         camera.addCallbackBuffer(createPreviewBuffer(it))
    // //         camera.addCallbackBuffer(createPreviewBuffer(it))
    // //         camera.addCallbackBuffer(createPreviewBuffer(it))
    // //     }
    // //
    // //     return camera
    // // }

    private int[] SelectPreviewFpsRange(Camera camera)
    {
        var desiredPreviewFpsScaled = (int)(RequestedCameraFps * 1000f);
        var selectedFpsRange = new List<int>().ToArray();
        var minDiff = int.MaxValue;
        foreach (var range in camera.GetParameters().SupportedPreviewFpsRange)
        {
            var deltaMin = desiredPreviewFpsScaled - range[(int)global::Android.Hardware.Preview.FpsMinIndex];
            var deltaMax = desiredPreviewFpsScaled - range[(int)global::Android.Hardware.Preview.FpsMaxIndex];
            var diff = Math.Abs(deltaMin) + Math.Abs(deltaMax);
            if (diff < minDiff)
            {
                selectedFpsRange = range;
                minDiff = diff;
            }
        }

        return selectedFpsRange;
    }
    
    // private fun selectPreviewFpsRange(camera: Camera): IntArray? {
    //     // The camera API uses integers scaled by a factor of 1000 instead of floating-point frame
    //     // rates.
    //     val desiredPreviewFpsScaled = (REQUESTED_CAMERA_FPS * 1000f).toInt()
    //
    //     // The method for selecting the best range is to minimize the sum of the differences between
    //     // the desired value and the upper and lower bounds of the range.  This may select a range
    //     // that the desired value is outside of, but this is often preferred.  For example, if the
    //     // desired frame rate is 29.97, the range (30, 30) is probably more desirable than the
    //     // range (15, 30).
    //     var selectedFpsRange: IntArray? = null
    //     var minDiff = Integer.MAX_VALUE
    //     for (range in camera.parameters.supportedPreviewFpsRange) {
    //         val deltaMin = desiredPreviewFpsScaled - range[Parameters.PREVIEW_FPS_MIN_INDEX]
    //         val deltaMax = desiredPreviewFpsScaled - range[Parameters.PREVIEW_FPS_MAX_INDEX]
    //         val diff = abs(deltaMin) + abs(deltaMax)
    //         if (diff < minDiff) {
    //             selectedFpsRange = range
    //             minDiff = diff
    //         }
    //     }
    //     return selectedFpsRange
    // }

    private void SetRotation(Context context, Camera camera, Camera.Parameters parameters)
    {
        var windowManager = ((Activity)context).WindowManager;

        if (windowManager?.DefaultDisplay == null)
        {
            return;
        }

        var degrees = windowManager.DefaultDisplay.Rotation switch
        {
            SurfaceOrientation.Rotation0 => 0,
            SurfaceOrientation.Rotation180 => 180,
            SurfaceOrientation.Rotation270 => 270,
            SurfaceOrientation.Rotation90 => 90,
            _ => throw new ArgumentOutOfRangeException()
        };

        var cameraInfo = new Camera.CameraInfo();
        Camera.GetCameraInfo((int)CameraFacing.Back, cameraInfo);
        var angle = (cameraInfo.Orientation - degrees + 360) % 360;
        m_rotationDegrees = angle;
        camera.SetDisplayOrientation(angle);
        parameters.SetRotation(angle);
    }
    
    private void SetPreviewAndPictureSize(Camera camera, Context context, Preview graphicsOverlay, Camera.Parameters parameters)
    {
        var displayAspectRationInLandscape = (IsPortraitMode(context))
            ? (float)graphicsOverlay.Height / (float)graphicsOverlay.Width
            : (float)graphicsOverlay.Width / (float)graphicsOverlay.Height;
        var sizePair = SelectSizePair(camera, displayAspectRationInLandscape);
        
        m_previewSize = sizePair.Preview;
        var pictureSize = sizePair.Picture;
        if (m_previewSize != null)
        {
            parameters.SetPreviewSize(m_previewSize.Width, m_previewSize.Height);

        }

        if (pictureSize != null)
        {
            parameters.SetPictureSize(pictureSize.Width, pictureSize.Height);

        }
    }

    private static bool IsPortraitMode(Context context) => context.Resources is {Configuration.Orientation: Orientation.Portrait};

    public CameraSizePair SelectSizePair(Camera camera, float displayAspectRationInLandscape)
    {
        var parameters = camera.GetParameters();
        var supportedPreviewSizes = parameters.SupportedPreviewSizes;
        var supportedPictureSizes = parameters.SupportedPictureSizes;

        var validPreviewSizes = new List<CameraSizePair>();
        foreach (var previewSize in supportedPreviewSizes)
        {
            var previewAspectRation = previewSize.Width / (float)previewSize.Height;

            foreach (var pictureSize in supportedPictureSizes)
            {
                var pictureAspetRatio = pictureSize.Width / pictureSize.Height;
                if (Math.Abs(previewAspectRation - pictureAspetRatio) < AspectRatioTolerance)
                {
                    validPreviewSizes.Add(new CameraSizePair(previewSize, pictureSize));
                }
            }
        }

        CameraSizePair? selectedPair = null;
        var minAspectRatioDiff = float.MaxValue;

        foreach (var sizePair in validPreviewSizes)
        {
            var previewSize = sizePair.Preview;
            if (previewSize.Width < MinCameraPreviewWidth || previewSize.Width > MaxCameraPreviewWidth)
            {
                continue;
            }

            var previewAspectRatio = previewSize.Width / previewSize.Height;
            var aspectRatioDiff = Math.Abs(displayAspectRationInLandscape - previewAspectRatio);
            if (Math.Abs(aspectRatioDiff - minAspectRatioDiff) < AspectRatioTolerance)
            {
                if (selectedPair == null || selectedPair.Preview.Width > sizePair.Preview.Width)
                {
                    selectedPair = sizePair;
                }
                else if (aspectRatioDiff < minAspectRatioDiff)
                {
                    minAspectRatioDiff = aspectRatioDiff;
                    selectedPair = sizePair;
                }
            }
        }

        if (selectedPair == null)
        {
            // Picks the one that has the minimum sum of the differences between the desired values and
            // the actual values for width and height.
            var minDiff = int.MaxValue;
            foreach (var sizePair in validPreviewSizes)
            {
                var size = sizePair.Preview;
                var diff =
                    Math.Abs(size.Width - DefaultRequestedCameraPreviewWidth) +
                    Math.Abs(size.Height - DefaultRequestedCameraPreviewHeight);
                if (diff < minDiff)
                {
                    selectedPair = sizePair;
                    minDiff = diff;
                }
            }
        }

        return selectedPair;
    }
    
    // fun generateValidPreviewSizeList(camera: Camera): List<CameraSizePair> {
    //     val parameters = camera.parameters
    //     val supportedPreviewSizes = parameters.supportedPreviewSizes
    //     val supportedPictureSizes = parameters.supportedPictureSizes
    //     val validPreviewSizes = ArrayList<CameraSizePair>()
    //     for (previewSize in supportedPreviewSizes) {
    //         val previewAspectRatio = previewSize.width.toFloat() / previewSize.height.toFloat()
    //
    //         // By looping through the picture sizes in order, we favor the higher resolutions.
    //         // We choose the highest resolution in order to support taking the full resolution
    //         // picture later.
    //         for (pictureSize in supportedPictureSizes) {
    //             val pictureAspectRatio = pictureSize.width.toFloat() / pictureSize.height.toFloat()
    //             if (abs(previewAspectRatio - pictureAspectRatio) < ASPECT_RATIO_TOLERANCE) {
    //                 validPreviewSizes.add(CameraSizePair(previewSize, pictureSize))
    //                 break
    //             }
    //         }
    //     }
    //
    //     // If there are no picture sizes with the same aspect ratio as any preview sizes, allow all of
    //     // the preview sizes and hope that the camera can handle it.  Probably unlikely, but we still
    //     // account for it.
    //     if (validPreviewSizes.isEmpty()) {
    //         Log.w(TAG, "No preview sizes have a corresponding same-aspect-ratio picture size.")
    //         for (previewSize in supportedPreviewSizes) {
    //             // The null picture size will let us know that we shouldn't set a picture size.
    //             validPreviewSizes.add(CameraSizePair(previewSize, null))
    //         }
    //     }
    //
    //     return validPreviewSizes
    // }
    // private fun selectSizePair(camera: Camera, displayAspectRatioInLandscape: Float): CameraSizePair? {
    //         val validPreviewSizes = Utils.generateValidPreviewSizeList(camera)
    //
    //         var selectedPair: CameraSizePair? = null
    //         // Picks the preview size that has closest aspect ratio to display view.
    //         var minAspectRatioDiff = Float.MAX_VALUE
    //
    //         for (sizePair in validPreviewSizes) {
    //             val previewSize = sizePair.preview
    //             if (previewSize.width < MIN_CAMERA_PREVIEW_WIDTH || previewSize.width > MAX_CAMERA_PREVIEW_WIDTH) {
    //                 continue
    //             }
    //
    //             val previewAspectRatio = previewSize.width.toFloat() / previewSize.height.toFloat()
    //             val aspectRatioDiff = abs(displayAspectRatioInLandscape - previewAspectRatio)
    //             if (abs(aspectRatioDiff - minAspectRatioDiff) < Utils.ASPECT_RATIO_TOLERANCE) {
    //                 if (selectedPair == null || selectedPair.preview.width < sizePair.preview.width) {
    //                     selectedPair = sizePair
    //                 }
    //             } else if (aspectRatioDiff < minAspectRatioDiff) {
    //                 minAspectRatioDiff = aspectRatioDiff
    //                 selectedPair = sizePair
    //             }
    //         }
    //
    //         if (selectedPair == null) {
    //             // Picks the one that has the minimum sum of the differences between the desired values and
    //             // the actual values for width and height.
    //             var minDiff = Integer.MAX_VALUE
    //             for (sizePair in validPreviewSizes) {
    //                 val size = sizePair.preview
    //                 val diff =
    //                     abs(size.width - DEFAULT_REQUESTED_CAMERA_PREVIEW_WIDTH) +
    //                             abs(size.height - DEFAULT_REQUESTED_CAMERA_PREVIEW_HEIGHT)
    //                 if (diff < minDiff) {
    //                     selectedPair = sizePair
    //                     minDiff = diff
    //                 }
    //             }
    //         }
    //
    //         return selectedPair
    //     }
    
    // private fun setPreviewAndPictureSize(camera: Camera, parameters: Parameters) {
    //
    //     // Gives priority to the preview size specified by the user if exists.
    //     val sizePair: CameraSizePair = PreferenceUtils.getUserSpecifiedPreviewSize(context) ?: run {
    //         // Camera preview size is based on the landscape mode, so we need to also use the aspect
    //         // ration of display in the same mode for comparison.
    //         val displayAspectRatioInLandscape: Float =
    //         if (Utils.isPortraitMode(graphicOverlay.context)) {
    //             graphicOverlay.height.toFloat() / graphicOverlay.width
    //         } else {
    //             graphicOverlay.width.toFloat() / graphicOverlay.height
    //         }
    //         selectSizePair(camera, displayAspectRatioInLandscape)
    //     } ?: throw IOException("Could not find suitable preview size.")
    //
    //     previewSize = sizePair.preview.also {
    //         Log.v(TAG, "Camera preview size: $it")
    //         parameters.setPreviewSize(it.width, it.height)
    //         PreferenceUtils.saveStringPreference(context, R.string.pref_key_rear_camera_preview_size, it.toString())
    //     }
    //
    //     sizePair.picture?.let { pictureSize ->
    //             Log.v(TAG, "Camera picture size: $pictureSize")
    //         parameters.setPictureSize(pictureSize.width, pictureSize.height)
    //         PreferenceUtils.saveStringPreference(
    //             context, R.string.pref_key_rear_camera_picture_size, pictureSize.toString()
    //         )
    //     }
    // }
    //
    
    // public void Start(Preview preview)
    // {
    //     //Check permissions
    //     
    //     
    //     var context = DUI.GetCurrentMauiContext?.Context;
    //     if (context == null) return;
    //     
    //     //Create CameraX preview view
    //     if (preview.PreviewView.Handler is not ContentViewHandler contentViewHandler) return;
    //     var constraintLayout = new ConstraintLayout(context)
    //     {
    //         LayoutParameters = new ConstraintLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
    //             ViewGroup.LayoutParams.MatchParent)
    //     };
    //     var previewView = new PreviewView(context)
    //     {
    //         LayoutParameters = new ConstraintLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
    //             ViewGroup.LayoutParams.MatchParent)
    //     };
    //     
    //     constraintLayout.AddView(previewView);
    //         
    //     contentViewHandler.PlatformView.AddView()
    // }
    public void OnPreviewFrame(byte[] data, Camera camera)
    {
        
    }
}

#pragma warning restore CS0618 // Type or member is obsolete
