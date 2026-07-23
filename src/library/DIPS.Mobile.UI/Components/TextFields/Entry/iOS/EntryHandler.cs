using DIPS.Mobile.UI.Components.TextFields.Dictation;
using DIPS.Mobile.UI.Components.TextFields.Entry.iOS;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.TextFields.Entry;

public partial class EntryHandler
{
    private MauiDoneAccessoryView? m_accessoryView;
    private bool m_isDictationActive;
    private bool m_ownsDictationSession;

    protected override MauiTextField CreatePlatformView()
    {
        var platformEntry = new TryFixCrashMauiTextField();

        var accessoryView = new MauiDoneAccessoryView();
        accessoryView.SetDataContext(this);
        accessoryView.SetDoneClicked(OnDoneClicked);
        accessoryView.SetMicrophoneClicked(OnMicrophoneButtonClicked);
        platformEntry.InputAccessoryView = accessoryView;
        m_accessoryView = accessoryView;

        return platformEntry;
    }

    static void OnDoneClicked(object sender)
    {
        if (sender is EntryHandler handler)
        {
            handler.PlatformView.ResignFirstResponder();
            handler.VirtualView.Completed();
        }
    }

    static void OnMicrophoneButtonClicked(object sender)
    {
        if (sender is EntryHandler handler)
            handler.ToggleDictation();
    }

    protected override void ConnectHandler(MauiTextField platformView)
    {
        base.ConnectHandler(platformView);

        platformView.EditingDidBegin += OnEditingDidBegin;
        platformView.EditingDidEnd += OnEditingDidEnd;
    }

    private static partial void MapShouldSelectTextOnTapped(EntryHandler handler, Entry entry)
    {
    }

    private static partial void MapShouldUseDefaultPadding(EntryHandler handler, Entry entry)
    {

    }

    private async void OnEditingDidBegin(object? sender, EventArgs e)
    {
        ShowMicrophoneForFocusedField();

        if(!((VirtualView as Entry)!).ShouldSelectAllTextOnFocused)
            return;

        await Task.Delay(1);
        PlatformView.SelectAll(null);
    }

    private void OnEditingDidEnd(object? sender, EventArgs e)
    {
        StopDictationWhenOwned();
        m_accessoryView?.SetMicrophoneButtonActive(false);
    }

    private void ShowMicrophoneForFocusedField()
    {
        m_isDictationActive = false;
        m_accessoryView?.SetMicrophoneButtonVisible(DictationFeature.IsAvailable);
        m_accessoryView?.SetMicrophoneButtonActive(false);
    }

    private async void ToggleDictation()
    {
        if (m_isDictationActive)
        {
            m_isDictationActive = false;
            m_accessoryView?.SetMicrophoneButtonActive(false);
            DictationSessionCoordinator.Current.StopSession();
            return;
        }

        m_isDictationActive = true;
        m_ownsDictationSession = true;
        m_accessoryView?.SetMicrophoneButtonActive(true);
        AnnounceDictationStarted();

        StartDictationResult startResult;
        
        try
        {
            startResult = await DictationSessionCoordinator.Current.StartSession(this);
        }
        catch (Exception error)
        {
            startResult = new StartDictationResult(error.Message);
        }

        m_isDictationActive = false;
        m_ownsDictationSession = false;
        m_accessoryView?.SetMicrophoneButtonActive(false);

        if (startResult.IsError)
            DictationErrorDialog.Show(startResult.ErrorMessage);
    }

    private void StopDictationWhenOwned()
    {
        if (!m_ownsDictationSession)
            return;

        m_ownsDictationSession = false;
        m_isDictationActive = false;
        DictationSessionCoordinator.Current.StopSession();
    }

    private static void AnnounceDictationStarted() =>
        UIAccessibility.PostNotification(UIAccessibilityPostNotification.Announcement, new NSString(DUILocalizedStrings.Dictation));

    private static partial void MapHasBorder(EntryHandler handler, Entry entry)
    {
        handler.PlatformView.BorderStyle = entry.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
    }

    protected override void DisconnectHandler(MauiTextField platformView)
    {
        StopDictationWhenOwned();
        platformView.InputAccessoryView = null;
        m_accessoryView = null;

        base.DisconnectHandler(platformView);

        platformView.EditingDidBegin -= OnEditingDidBegin;
        platformView.EditingDidEnd -= OnEditingDidEnd;
    }
}
