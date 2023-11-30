using DIPS.Mobile.UI.Components.CheckBoxes;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.InputField;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;

namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField;

public partial class MultiLineInputField : SingleLineInputField
{
    private HorizontalStackLayout? m_buttonsLayout;

    private string? m_textWhenFirstFocused;

    private readonly Label m_label = new Labels.Label
    { 
        Style = Styles.GetLabelStyle(LabelStyle.Body200), 
        IsVisible = false,
        Margin = new Thickness(0, 4, 8, 0),
        VerticalTextAlignment = TextAlignment.Start,
        LineBreakMode = LineBreakMode.TailTruncation
    };

    private ActivityIndicator m_activityIndicator;

    public MultiLineInputField()
    {
        CreateButtons();
        SetupLabel();
        CreateSaveView();
    }

    private void CreateSaveView()
    {
        m_activityIndicator = new Components.Loading.ActivityIndicator
        { 
            IsRunning = true, 
            Opacity = 0,
        };
        
        ContentBorderGrid.Add(m_activityIndicator);
    }

    protected override void CreateInputView()
    {
        InputView = new Editor.Editor
        {
            AutoSize = EditorAutoSizeOption.TextChanges,
            VerticalTextAlignment = TextAlignment.Start,
            HasBorder = false,
            IsSpellCheckEnabled = false,
            ShouldUseDefaultPadding = false
        };
    }

    /// <summary>
    /// Use a label when the Editor is in "read-only" mode because truncating for Editor is not working on Android
    /// Also, using Label for both platforms for truncation we can utilize 'MaxLines' instead of 'MaxHeightRequest', and it's easier to check if the text is truncated
    /// </summary>
    private void SetupLabel()
    {
        m_label.SetBinding(Labels.Label.IsTruncatedProperty, new Binding(nameof(IsTruncated), source: this));
        m_label.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
        m_label.SetBinding(Label.MaxLinesProperty, new Binding(nameof(MaxLines), source: this));

        InnerGrid.Add(m_label, 0, 1);
    }

    private void CreateButtons()
    {
        var cancelButton = new Button
        {
            Text = DUILocalizedStrings.Cancel, 
            Style = Styles.GetButtonStyle(ButtonStyle.SecondarySmall),
            Command = new Command(() => OnButtonTapped(false))
        };

        var doneButton = new Button
        {
            Text = DUILocalizedStrings.Save, 
            Style = Styles.GetButtonStyle(ButtonStyle.PrimarySmall),
            Command = new Command(() => OnButtonTapped(true)),
            CommandParameter = InputView.Text
        };

        m_buttonsLayout = new HorizontalStackLayout 
        { 
            HorizontalOptions = LayoutOptions.End,
            Spacing = 10,
            Margin = new Thickness(0, 8, 0, 0),
            Children = { cancelButton, doneButton } 
        };
    }
    
    protected override void OnInputViewFocused(object? sender, FocusEventArgs e)
    {
        base.OnInputViewFocused(sender, e);

        IsTruncated = false;
        
        m_label.IsVisible = false;

        ToggleButtonsVisibility(true);
        
        if(m_textWhenFirstFocused is not null)
            return;

        m_textWhenFirstFocused = InputView?.Text;
    }

    private bool IsDirty => m_textWhenFirstFocused != InputView?.Text;
    
    protected override void OnInputViewUnFocused(object? sender, FocusEventArgs e)
    {
        base.OnInputViewUnFocused(sender, e);
        
        m_label.SetBinding(Labels.Label.IsTruncatedProperty, new Binding(nameof(IsTruncated), source: this));
        
        UpdateLabelVisibility();
        
        InputView.IsVisible = false;
        
        if (IsDirty)
            return;

        Reset();
    }

    private void ToggleButtonsVisibility(bool isEnabled)
    {
        if (!InnerGrid.Contains(m_buttonsLayout))
        {
            InnerGrid.Add(m_buttonsLayout, 0, 2);
            return;
        }
        
        m_buttonsLayout!.IsVisible = isEnabled;
    }

    private void OnButtonTapped(bool isSaving)
    {
        if (isSaving)
        {
            m_textWhenFirstFocused = InputView?.Text;
            SaveCommand?.Execute(SaveCommandParameter);
        }
        else
        {
            InputView!.Text = m_textWhenFirstFocused;
        }

        if (!InputView!.IsFocused)
        {
            Reset();
        }
        else
        {
            InputView?.Unfocus();
        }
        
    }

    protected override void OnTextChanged()
    {
        base.OnTextChanged();

        UpdateLabelVisibility();
    }

    private void UpdateLabelVisibility()
    {
        if (!InputView.IsFocused)
        {
            m_label.IsVisible = Text != string.Empty;
        }
    }

    private void Reset()
    {
        ToggleButtonsVisibility(false);
        m_textWhenFirstFocused = null;
    }

    private void OnIsSavingChanged()
    {
        if (!IsSaving)
            return;

        OnIsSaving();
    }

    private async void OnIsSaving()
    {
        FadeOutContent();
            
        m_activityIndicator.IsVisible = true;
        _ = m_activityIndicator.FadeTo(1);

        // A small delay because OnUnfocused sets touch enabled to true
        await Task.Delay(1);
        SetTouchEnabled(false);
    }
    
    private void OnIsSavingSuccessChanged()
    {
        if (!IsSavingSuccess)
            return;

        OnStopSaving(true);
    }

    private async void OnStopSaving(bool success)
    {
        FadeInContent();

        SetTouchEnabled(true);

        if (success)
        {
            Style = Styles.GetInputFieldStyle(InputFieldStyle.Success);
        }
        
        await m_activityIndicator.FadeTo(0);
        m_activityIndicator.IsVisible = false;
    }

    private void OnIsErrorChanged()
    {
        SetStyle();
        ChangeHeaderTextStyle();
        
        if (IsError)
        {
            OnError();
        }
        else
        {
            OnChangedToNoError();
        }
    }

    private void OnError()
    {
        OnStopSaving(false);
        HelpTextLabel.SetBinding(Label.TextProperty, new Binding(nameof(ErrorText), source: this));
        HelpTextLabel.TextColor = Colors.GetColor(ColorName.color_error_dark);
    }

    private void OnChangedToNoError()
    {
        HelpTextLabel.SetBinding(Label.TextProperty, new Binding(nameof(HelpText), source: this));
        HelpTextLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(HelpTextColor), source: this));
        OnPropertyChanged(nameof(HelpText));
    }

    protected override void ChangeHeaderTextStyle()
    {
        base.ChangeHeaderTextStyle();

        if (IsError)
        {
            if(IsFocused)
                HeaderTextLabel.TextColor = Colors.GetColor(ColorName.color_error_dark);
            
            if(Text != string.Empty)
                HeaderTextLabel.TextColor = Colors.GetColor(ColorName.color_error_dark);
        }
        else
        {
            HeaderTextLabel.TextColor = Colors.GetColor(ColorName.color_neutral_70);
        }
    }

    protected override void SetStyle()
    {
        base.SetStyle();

        if (IsError && !IsFocused)
        {
            Style = Styles.GetInputFieldStyle(InputFieldStyle.Error);
        }
    }

    private void FadeOutContent()
    {
        InnerGrid.FadeTo(.25);
    }

    private void FadeInContent()
    {
        InnerGrid.FadeTo(1);
    }
}