using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.Fragment.App;
using AndroidX.ViewPager.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.Android;
using Google.Android.Material.Dialog;
using Microsoft.Maui.Platform;
using Button = Android.Widget.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Entry = DIPS.Mobile.UI.Components.TextFields.Entry.Entry;

namespace DIPS.Mobile.UI.Components.Alerting.Dialog.Android;

internal class InputDialog : DialogFragment
{
    private readonly IInputDialog m_inputDialogConfigurator;
    private readonly string m_actionTitle;
    private readonly string? m_cancelTitle;
    private readonly Action m_onActionTapped;
    private readonly Action m_onCancelTapped;
    private Button? m_actionButton;

    public InputDialog(
        IInputDialog inputDialogConfigurator,
        string actionTitle,
        string? cancelTitle,
        Action onActionTapped,
        Action onCancelTapped)
    {
        m_inputDialogConfigurator = inputDialogConfigurator;
        m_actionTitle = actionTitle;
        m_cancelTitle = cancelTitle;
        m_onActionTapped = onActionTapped;
        m_onCancelTapped = onCancelTapped;
    }

    public override global::Android.App.Dialog? OnCreateDialog(Bundle? savedInstanceState)
    {
        var builder = new MaterialAlertDialogBuilder(DUI.GetCurrentMauiContext!.Context!)
            .SetOnCancelListener(this)!
            .SetTitle("Title")!
            .SetMessage("Beskrivelse");

        var verticalStackLayout = new VerticalStackLayout
        {
            Spacing = 12, 
            Padding = 12
        };

        foreach (var inputDialogEntryConfigurator in m_inputDialogConfigurator.InputDialogEntryConfigurators)
        {

            if (inputDialogEntryConfigurator is StringDialogInputField dialogInputField)
            {
                var entry = new Entry
                {
                    AutomationId = dialogInputField.Identifier.ToString(),
                    Placeholder = dialogInputField.Placeholder,
                    Text = dialogInputField.Value,
                };

                entry.TextChanged += (s, e) =>
                {
                    dialogInputField.Value = entry.Text;
                    if (m_actionButton != null)
                    {
                        m_actionButton.Enabled = DialogService.ActionEnabledState(m_inputDialogConfigurator);
                    }
                };
                
                verticalStackLayout.Add(entry);
            }
        }

        builder?.SetView(verticalStackLayout.ToPlatform(DUI.GetCurrentMauiContext));

        if (m_cancelTitle != null)
        {
            builder?.SetNegativeButton(m_cancelTitle, ((_, _) => { m_onCancelTapped.Invoke(); }));
        }
        builder?.SetPositiveButton(m_actionTitle, ((_, _) => { m_onActionTapped.Invoke(); }));
        
        var dialog = builder?.Create();

        dialog?.Window?.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
        dialog!.ShowEvent += OnShow;
            
        var window = (((global::Android.App.Activity)Context!)).Window;
        if (window is {Attributes: { }}) //Make sure the dialog inherits window flag from the activity, useful when the activity is set as secured.
        {
            var flags = window.Attributes.Flags;
            dialog.Window?.SetFlags(flags, flags);
        }
        return dialog;
    }
    
    private void OnShow(object? sender, EventArgs e)
    {
        if (sender is not AndroidX.AppCompat.App.AlertDialog alertDialog)
            return;

        m_actionButton = alertDialog.GetButton((int)DialogButtonType.Positive);
        if (m_actionButton is not null)
        {
            m_actionButton.Enabled = DialogService.ActionEnabledState(m_inputDialogConfigurator);
        }
    }
    
    public override void OnDismiss(IDialogInterface dialog)
    {
        if (Dialog is AndroidX.AppCompat.App.AlertDialog alertDialog)
        {
            alertDialog.ShowEvent -= OnShow;
        }
            
        base.OnDismiss(dialog);
        /*m_onCancelTapped.Invoke();*/
    }

   
}