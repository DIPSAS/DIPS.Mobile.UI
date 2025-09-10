using DIPS.Mobile.UI.Components.Labels;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.InputField;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = Microsoft.Maui.Controls.Label;

namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField;

public partial class MultiLineInputField : SingleLineInputField
{
    private Grid? m_buttonsLayout;

    private string? m_textWhenFirstFocused;

    private readonly Label m_label = new Label
    { 
        Style = Styles.GetLabelStyle(LabelStyle.Body200), 
        IsVisible = false,
        Margin = new Thickness(0, Sizes.GetSize(SizeName.content_margin_xsmall), Sizes.GetSize(SizeName.content_margin_small), 0),
        VerticalTextAlignment = TextAlignment.Start,
        LineBreakMode = LineBreakMode.TailTruncation
    };

    private Button m_doneButton = new ();
    private Button m_cancelButton = new();

    private Label m_textLengthLabel = new Labels.Label
    {
        Text = string.Empty,
        Style = Styles.GetLabelStyle(LabelStyle.Body100),
        IsVisible = true,
        MaxLines = 1,
        VerticalTextAlignment = TextAlignment.Center,
        HorizontalTextAlignment = TextAlignment.Start,
        HorizontalOptions = LayoutOptions.Start,
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
        /*m_label.SetBinding(CheckTruncatedLabel.IsTruncatedProperty, static (MultiLineInputField multiLineInputField) => multiLineInputField.IsTruncated, source: this);*/
        m_label.SetBinding(Label.TextProperty, static (MultiLineInputField multiLineInputField) => multiLineInputField.Text, source: this);
        m_label.SetBinding(Label.MaxLinesProperty, static (MultiLineInputField multiLineInputField) => multiLineInputField.MaxLines, source: this);
        
        InnerGrid.Add(m_label, 0, 1);
    }

    private void CreateButtons()
    {
        m_cancelButton = new Button
        {
            Text = DUILocalizedStrings.Cancel, 
            Style = Styles.GetButtonStyle(ButtonStyle.SecondarySmall),
            Command = new Command(OnCancelTapped)
        };

        m_doneButton = new Button
        {
            Text = DUILocalizedStrings.Save, 
            Style = Styles.GetButtonStyle(ButtonStyle.PrimarySmall),
            Command = new Command(OnSaveTapped),
            CommandParameter = InputView.Text
        };
        AutomationProperties.SetIsInAccessibleTree(m_doneButton, false);

        
        m_buttonsLayout = new Grid
        { 
            ColumnDefinitions =
            [
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
            ],
            ColumnSpacing = 10,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.content_margin_small), 0, 0)
        };
        m_buttonsLayout.Add(m_textLengthLabel, column: 0);
        m_buttonsLayout.Add(m_cancelButton, column: 1);
        m_buttonsLayout.Add(m_doneButton, column: 2);
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
        
        /*m_label.SetBinding(CheckTruncatedLabel.IsTruncatedProperty, static (MultiLineInputField multiLineInputField) => multiLineInputField.IsTruncated, source: this);*/
        
        UpdateLabelVisibility();
        
        InputView.IsVisible = false;
        
        if (IsDirty)
            return;

        Reset();
    }

    private void ToggleButtonsVisibility(bool isEnabled)
    {
        if (MaxTextLength > 0)
        {
            m_textLengthLabel = UpdateTextLengthLabel();
        }
        
        if (!InnerGrid.Contains(m_buttonsLayout))
        {
            InnerGrid.Add(m_buttonsLayout, 0, 2);
            return;
        }
        
        m_buttonsLayout!.IsVisible = isEnabled && (ShowButtons || MaxTextLength > 0);
    }

    private void OnSaveTapped()
    {
        m_textWhenFirstFocused = InputView?.Text;
        SaveTapped?.Invoke(this, EventArgs.Empty);
        SaveCommand?.Execute(SaveCommandParameter);

        ResetFocus();
    }
    
    private void OnCancelTapped()
    {
        InputView!.Text = m_textWhenFirstFocused;
        CancelTapped?.Invoke(this, EventArgs.Empty);
        CancelCommand?.Execute(CancelCommandParameter);
        
        ResetFocus();   
    }

    private void ResetFocus()
    {
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
        if (MaxTextLength > 0)
        {
            m_textLengthLabel = UpdateTextLengthLabel();
        }
    }

    private Label UpdateTextLengthLabel()
    {
        if (Text.Length == 0)
        {
            m_textLengthLabel.Text = string.Format(DUILocalizedStrings.NumberOfCharactersLeft, MaxTextLength.ToString());
            m_textLengthLabel.TextColor = Colors.GetColor(ColorName.color_text_subtle);
            m_doneButton.IsEnabled = true;
        }
        else if (MaxTextLength > Text.Length)
        {
            m_textLengthLabel.Text = string.Format(DUILocalizedStrings.NumberOfCharactersLeft, (MaxTextLength - Text.Length).ToString());
            m_textLengthLabel.TextColor = Colors.GetColor(ColorName.color_text_subtle);
            m_doneButton.IsEnabled = true;
            
        }
        else if (MaxTextLength == Text.Length)
        {
            m_textLengthLabel.Text = DUILocalizedStrings.MaxCharactersReached;
            m_textLengthLabel.TextColor = Colors.GetColor(ColorName.color_text_subtle);
            m_doneButton.IsEnabled = true;
        }
        else
        {
            m_textLengthLabel.Text = string.Format(DUILocalizedStrings.NumberOfCharactersTooMany, (Text.Length - MaxTextLength).ToString());
            m_textLengthLabel.TextColor = Colors.GetColor(ColorName.color_text_danger);
            m_doneButton.IsEnabled = false;
        }

        return m_textLengthLabel;
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
        if (IsSaving)
        {
            OnIsSaving();
            return;
        }

        OnStopSaving();
    }

    private async void OnIsSaving()
    {
        FadeOutContent();
            
        ToggleButtonsVisibility(false);
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

        IsSaving = false;
        Style = Styles.GetInputFieldStyle(InputFieldStyle.Success);
    }

    private async void OnStopSaving()
    {
        FadeInContent();

        SetTouchEnabled(true);
        
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
        IsSaving = false;
        OnStopSaving();
        HelpTextLabel.SetBinding(Label.TextProperty, static (MultiLineInputField multiLineInputField) => multiLineInputField.ErrorText, source: this);
        HelpTextLabel.TextColor = Colors.GetColor(ColorName.color_text_danger);
    }

    private void OnChangedToNoError()
    {
        HelpTextLabel.SetBinding(Label.TextProperty, static (MultiLineInputField multiLineInputField) => multiLineInputField.HelpText, source: this);
        HelpTextLabel.SetBinding(Label.TextColorProperty, static (MultiLineInputField multiLineInputField) => multiLineInputField.HelpTextColor, source: this);
        OnPropertyChanged(nameof(HelpText));
    }

    protected override void ChangeHeaderTextStyle()
    {
        base.ChangeHeaderTextStyle();

        if (IsError)
        {
            if(IsFocused)
                HeaderTextLabel.TextColor = Colors.GetColor(ColorName.color_text_danger);
            
            if(Text != string.Empty)
                HeaderTextLabel.TextColor = Colors.GetColor(ColorName.color_text_danger);
        }
        else
        {
            HeaderTextLabel.TextColor = Colors.GetColor(ColorName.color_text_subtle);
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
    
    private void OnShowButtonsChanged()
    {
        m_cancelButton.IsVisible = m_doneButton.IsVisible = ShowButtons;
    }

    private void OnMaxTextLengthChanged()
    {
        m_textLengthLabel.IsVisible = MaxTextLength > 0;
        if (MaxTextLength > 0)
        {
            m_textLengthLabel = UpdateTextLengthLabel();
        }
        else
        {
            m_textLengthLabel.Text = string.Empty;
            m_doneButton.IsEnabled = true;
        }
    }
}