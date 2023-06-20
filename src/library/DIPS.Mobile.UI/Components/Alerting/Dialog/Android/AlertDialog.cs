using Android.Content;
using Android.OS;
using AndroidX.Fragment.App;
using DIPS.Mobile.UI.API.Library;
using Google.Android.Material.Dialog;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Alerting.Dialog.Android
{
    internal class AlertDialog : DialogFragment
    {
        private readonly bool m_isDestructiveDialog;
        private readonly string m_message;
        private readonly string m_title;
        private readonly string? m_cancelTitle;
        private readonly Action m_onActionTapped;
        private readonly Action m_onCancelTapped;
        private readonly string m_actionTitle;

        internal AlertDialog(
            string title,
            string message,
            string actionTitle,
            string? cancelTitle,
            Action onActionTapped,
            Action onCancelTapped,
            bool isDestructiveDialog = false)
        {
            m_title = title;
            m_message = message;
            m_cancelTitle = cancelTitle;
            m_onActionTapped = onActionTapped;
            m_onCancelTapped = onCancelTapped;
            m_isDestructiveDialog = isDestructiveDialog;
            m_actionTitle = actionTitle;
        }

        public override global::Android.App.Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var builder = new MaterialAlertDialogBuilder(DUI.GetCurrentMauiContext!.Context!)
                .SetOnCancelListener(this)
                .SetTitle(m_title)
                .SetMessage(m_message);
            
            if (m_cancelTitle != null)
            {
                builder.SetNegativeButton(m_cancelTitle, ((_, _) => { }));
            }
            builder.SetPositiveButton(m_actionTitle, ((_, _) => m_onActionTapped.Invoke()));

            var dialog = builder.Create();
            dialog.ShowEvent += OnShow;
            
            var window = ((global::Android.App.Activity)Context).Window;
            if (window is {Attributes: { }}) //Make sure the dialog inherits window flag from the activity, useful when the activity is set as secured.
            {
                var flags = window.Attributes.Flags;
                dialog.Window?.SetFlags(flags, flags);
            }
            return dialog;
        }

        public override void OnDismiss(IDialogInterface dialog)
        {
            if (Dialog is AndroidX.AppCompat.App.AlertDialog alertDialog)
            {
                alertDialog.ShowEvent -= OnShow;
            }
            
            base.OnDismiss(dialog);
            m_onCancelTapped.Invoke();
        }

        private void OnShow(object? sender, EventArgs e)
        {
            if (m_isDestructiveDialog && sender is AndroidX.AppCompat.App.AlertDialog alertDialog)
            {
                var button = alertDialog.GetButton((int)DialogButtonType.Positive);
                button!.SetTextColor(Colors.GetColor(ColorName.color_error_dark).ToPlatform());
            }   
        }
    }
}