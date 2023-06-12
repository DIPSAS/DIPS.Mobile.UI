using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using DIPS.Mobile.UI.Components.Slidable.Util;

[assembly: UsesPermission(Manifest.Permission.Vibrate)]

namespace DIPS.Mobile.UI.API.Vibration
{
    public static partial class VibrationService
    {
        private static Vibrator? s_vibrator;

        public static partial void Vibrate(int duration)
        {
            if (!ShouldVibrate())
            {
                return;
            }

            s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(duration, VibrationEffect.DefaultAmplitude));
        }

        public static partial void Click()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            if (Build.VERSION.SdkInt <= BuildVersionCodes.P)
            {
                s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(10, VibrationEffect.DefaultAmplitude));
            }
            else
            {
                s_vibrator?.Vibrate(VibrationEffect.CreatePredefined(VibrationEffect.EffectClick));
            }
        }

        public static partial void HeavyClick()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            s_vibrator?.Vibrate(Build.VERSION.SdkInt <= BuildVersionCodes.P
                ? VibrationEffect.CreateOneShot(20, VibrationEffect.DefaultAmplitude + 10)
                : VibrationEffect.CreatePredefined(VibrationEffect.EffectHeavyClick));
        }

        public static async partial void DoubleClick()
        {
            if (!ShouldVibrate())
            {
                return;
            }
            
            if (Build.VERSION.SdkInt <= BuildVersionCodes.P)
            {
                s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(10, VibrationEffect.DefaultAmplitude));
                await Task.Delay(20);
                s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(10, VibrationEffect.DefaultAmplitude));
            }
            else
            {
                s_vibrator?.Vibrate(VibrationEffect.CreatePredefined(VibrationEffect.EffectDoubleClick));
            }
        }

        public static partial void Error()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            var pause = 50;
            var action = 80;
            s_vibrator?.Vibrate(
                VibrationEffect.CreateWaveform(new long[] {0, action, pause, action, pause, action, pause, 150}, -1));
        }

        public static partial void Success()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            var pause = 50;
            var action = 80;
            s_vibrator?.Vibrate(VibrationEffect.CreateWaveform(new long[] {0, action, pause, action}, -1));
        }
        
        public static partial void SelectionChanged()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(5, 150));
        }

        private static bool ShouldVibrate()
        {
            if (CheckPermission() == Permission.Denied)
            {
                return false;
            }
            
            s_vibrator ??= Vibrator.FromContext(Platform.AppContext);
            return true;
        }

        private static Permission? CheckPermission()
        {
            return Platform.CurrentActivity?.CheckSelfPermission(Manifest.Permission.Vibrate);
        }
    }
}