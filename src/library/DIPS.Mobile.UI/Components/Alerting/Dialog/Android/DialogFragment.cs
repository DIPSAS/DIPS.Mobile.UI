using Android.Content;
using Android.OS;
using Android.Views;
using DIPS.Mobile.UI.API.Library;
using Google.Android.Material.Dialog;
using Microsoft.Maui.Platform;
using Button = Android.Widget.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Entry = DIPS.Mobile.UI.Components.TextFields.Entry.Entry;

namespace DIPS.Mobile.UI.Components.Alerting.Dialog.Android;

internal class DialogFragment : AndroidX.Fragment.App.DialogFragment
{
    private readonly IInputDialog? m_inputDialog;
    private readonly IDialog m_dialog;
    private readonly Action m_onActionTapped;
    private readonly Action m_onCancelTapped;
    private Button? m_actionButton;

    public DialogFragment(
        IDialog dialog,
        Action onActionTapped,
        Action onCancelTapped)
    {
        m_inputDialog = dialog as IInputDialog;
        m_dialog = dialog;
        m_onActionTapped = onActionTapped;
        m_onCancelTapped = onCancelTapped;
    }

    public override global::Android.App.Dialog OnCreateDialog(Bundle? savedInstanceState)
    {
        var builder = new MaterialAlertDialogBuilder(DUI.GetCurrentMauiContext!.Context!)
            .SetOnCancelListener(this)!
            .SetTitle(m_dialog.Title)!
            .SetMessage(m_dialog.Description);

        TryAddInput(builder);

        if(m_dialog.CancelTitle is not null)
            builder?.SetNegativeButton(m_dialog.CancelTitle, ((_, _) => { m_onCancelTapped.Invoke(); }));
        builder?.SetPositiveButton(m_dialog.ActionTitle, ((_, _) => { m_onActionTapped.Invoke(); }));
        
        var dialog = builder?.Create();

        dialog?.Window?.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
        dialog!.ShowEvent += OnShow;
            
        var window = (((global::Android.App.Activity)Context!)).Window;
        if (window is not { Attributes: not null }) //Make sure the dialog inherits window flag from the activity, useful when the activity is set as secured.
        {
            return dialog;
        }

        var flags = window.Attributes.Flags;
        dialog.Window?.SetFlags(flags, flags);
        return dialog;
    }

    private void TryAddInput(AndroidX.AppCompat.App.AlertDialog.Builder? builder)
    {
        if(m_inputDialog is null)
            return;
        
        var verticalStackLayout = new VerticalStackLayout
        {
            Spacing = 12, 
            Padding = 12
        };

        foreach (var inputDialogEntryConfigurator in m_inputDialog.InputDialogEntryConfigurators)
        {
            if (inputDialogEntryConfigurator is not StringDialogInputField dialogInputField)
                continue;

            var entry = new Entry
            {
                AutomationId = dialogInputField.Identifier.ToString(),
                Placeholder = dialogInputField.Placeholder,
                Text = dialogInputField.Value,
                HasBorder = true 
            };

            entry.TextChanged += delegate
            {
                dialogInputField.Value = entry.Text;
                if (m_actionButton != null)
                {
                    m_actionButton.Enabled = DialogService.ActionEnabledState(m_inputDialog);
                }
            };
                
            verticalStackLayout.Add(entry);
        }

        builder?.SetView(verticalStackLayout.ToPlatform(DUI.GetCurrentMauiContext!));
    }

    private void OnShow(object? sender, EventArgs e)
    {
        if (sender is not AndroidX.AppCompat.App.AlertDialog alertDialog)
            return;

        m_actionButton = alertDialog.GetButton((int)DialogButtonType.Positive);
        if (m_actionButton is null)
            return;

        if (m_dialog.IsDestructive)
        {
            m_actionButton.SetTextColor(Colors.GetColor(ColorName.color_text_action).ToPlatform());
        }

        if(m_inputDialog is not null)
            m_actionButton.Enabled = DialogService.ActionEnabledState(m_inputDialog);
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

   
}