using System;
using System.Threading.Tasks;
using Android.Content;
using AndroidX.AppCompat.App;
using DIPS.Mobile.UI.Resources.Colors;
using Google.Android.Material.Dialog;
using Java.Lang.Annotation;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Mobile.UI.Samples.Droid
{
    public class NativeTesting : INativeTesting
    {
        public Task OpenMaterialDialog()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var builder = new MaterialAlertDialogBuilder(DIPS.Mobile.UI.Droid.DUI.Context)
                .SetTitle("title")
                .SetMessage("message");
            
                builder.SetNegativeButton("cancel",((sender, e) =>
                {
                    if (sender is AlertDialog alertDialog)
                    {
                        taskCompletionSource.TrySetResult(true);
                        alertDialog.Dismiss();
                    }
                }));
                builder.SetPositiveButton("action",((sender, e) =>
                {
                    taskCompletionSource.TrySetResult(true);
                }));

                var dialog = builder.Create();
                dialog.DismissEvent += delegate { taskCompletionSource.TrySetResult(true); };
                
                dialog.ShowEvent += delegate
                {
                    var button = dialog.GetButton((int)DialogButtonType.Positive);
                    button.SetTextColor(Colors.GetColor(ColorName.color_error_dark).ToAndroid());
                };
                
                
                dialog.Show();
                
                return taskCompletionSource.Task;
        }
        
    }
}