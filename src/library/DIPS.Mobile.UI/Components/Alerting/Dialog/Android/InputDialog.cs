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
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Entry = DIPS.Mobile.UI.Components.TextFields.Entry.Entry;

namespace DIPS.Mobile.UI.Components.Alerting.Dialog.Android;

internal class InputDialog : DialogFragment
{
    private readonly IInputDialog m_inputDialogConfigurator;

    public InputDialog(IInputDialog inputDialogConfigurator)
    {
        m_inputDialogConfigurator = inputDialogConfigurator;
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
            var entry = new Entry
            {
                Placeholder = inputDialogEntryConfigurator.Placeholder ?? string.Empty,
                Text = inputDialogEntryConfigurator.Text ?? string.Empty
            };
            
            verticalStackLayout.Add(entry);
        }

        builder?.SetView(verticalStackLayout.ToPlatform(DUI.GetCurrentMauiContext));
            
        builder?.SetNegativeButton("Cancel", ((_, _) => { }));
        builder?.SetPositiveButton("Ok", ((_, _) => {}));

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
        if (sender is AndroidX.AppCompat.App.AlertDialog alertDialog)
        {
            var button = alertDialog.GetButton((int)DialogButtonType.Positive);
            button!.SetTextColor(Colors.GetColor(ColorName.color_error_dark).ToPlatform());
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