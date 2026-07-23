using DIPS.Mobile.UI.Components.TextFields.Dictation;
using DIPS.Mobile.UI.Components.TextFields.Entry.iOS;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class EditorHandler
{
    private MauiDoneAccessoryView? m_accessoryView;
    private bool m_isDictationActive;
    private bool m_ownsDictationSession;

    protected override MauiTextView CreatePlatformView()
    {
        var platformView = base.CreatePlatformView();

        var accessoryView = new MauiDoneAccessoryView();
        accessoryView.SetDataContext(this);
        accessoryView.SetDoneClicked(OnDoneClicked);
        accessoryView.SetMicrophoneClicked(OnMicrophoneButtonClicked);
        platformView.InputAccessoryView = accessoryView;
        m_accessoryView = accessoryView;

        return platformView;
    }

    protected override void ConnectHandler(MauiTextView platformView)
    {
        base.ConnectHandler(platformView);

        platformView.VerticalTextAlignment = TextAlignment.Start;
        platformView.Started += OnFocus;
        platformView.Ended += OnFocusEnded;
    }

    static void OnDoneClicked(object sender)
    {
        if (sender is EditorHandler handler)
        {
            handler.PlatformView.ResignFirstResponder();
            handler.VirtualView.Completed();
        }
    }

    static void OnMicrophoneButtonClicked(object sender)
    {
        if (sender is EditorHandler handler)
            handler.ToggleDictation();
    }

    private static partial void MapShouldUseDefaultPadding(EditorHandler handler, Editor editor)
    {
        if(editor.ShouldUseDefaultPadding)
            return;

        handler.PlatformView.TextContainer.LineFragmentPadding = 0;
        handler.PlatformView.TextContainerInset = new UIEdgeInsets(1f, 0f, 1f, 0f);
    }


    private async void OnFocus(object? sender, EventArgs e)
    {
        ShowMicrophoneForFocusedField();

        if (m_firstTimeFocus)
        {
            PlatformView.SelectedTextRange = PlatformView.GetTextRange(PlatformView.EndOfDocument, PlatformView.EndOfDocument);

            m_firstTimeFocus = false;
        }

        if(!((VirtualView as Editor)!).ShouldSelectAllTextOnFocused)
            return;

        await Task.Delay(1);
        PlatformView.SelectAll(null);
    }

    private void OnFocusEnded(object? sender, EventArgs e)
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

    private static partial void MapShouldSelectTextOnTapped(EditorHandler handler, Editor entry)
    {
    }

    private static partial void MapHasBorder(EditorHandler handler, Editor entry)
    {
    }

    protected override void DisconnectHandler(MauiTextView platformView)
    {
        StopDictationWhenOwned();
        platformView.InputAccessoryView = null;
        m_accessoryView = null;

        base.DisconnectHandler(platformView);

        platformView.Started -= OnFocus;
        platformView.Ended -= OnFocusEnded;
    }
}
