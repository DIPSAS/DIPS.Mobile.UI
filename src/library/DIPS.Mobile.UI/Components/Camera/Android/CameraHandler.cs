using Microsoft.Maui.Handlers;
using View = Android.Views.View;
namespace DIPS.Mobile.UI.Components.Camera;

public partial class CameraHandler : ViewHandler<Camera, global::Android.Views.View>
    {
        Android.Hardware.Camera camera;

        protected override View CreatePlatformView()
        {
            var context = MauiContext?.Context;
            return new global::Android.Views.View(context);
            
        }

        protected override void ConnectHandler(global::Android.Views.View nativeView)
        {
            base.ConnectHandler(nativeView);
            SetupCamera();
        }

        protected override void DisconnectHandler(global::Android.Views.View nativeView)
        {
            base.DisconnectHandler(nativeView);
            CleanupCamera();
        }

        void SetupCamera()
        {
            try
            {
                var context = MauiContext?.Context;
                camera = Android.Hardware.Camera.Open();

                var parameters = camera.GetParameters();
                parameters.FocusMode = Android.Hardware.Camera.Parameters.FocusModeContinuousPicture;
                camera.SetParameters(parameters);

                var previewCallback = new CameraPreviewCallback();
                previewCallback.PhotoCaptured += OnPhotoCaptured;

                camera.SetPreviewCallback(previewCallback);
                // var preview = new CameraPreview(context, camera);
                // NativeView.AddView(preview);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting up camera: {ex.Message}");
            }
        }

        void CleanupCamera()
        {
            if (camera != null)
            {
                camera.SetPreviewCallback(null);
                camera.StopPreview();
                camera.Release();
                camera = null;
            }
        }

        void OnPhotoCaptured(byte[] photoData)
        {
            // Handle the captured photo data as needed
            Console.WriteLine("Photo captured!");
        }
    }

    public class CameraPreviewCallback : Java.Lang.Object, Android.Hardware.Camera.IPreviewCallback
    {
        public event Action<byte[]> PhotoCaptured;

        public void OnPreviewFrame(byte[] data, Android.Hardware.Camera camera)
        {
            PhotoCaptured?.Invoke(data);
        }
    }