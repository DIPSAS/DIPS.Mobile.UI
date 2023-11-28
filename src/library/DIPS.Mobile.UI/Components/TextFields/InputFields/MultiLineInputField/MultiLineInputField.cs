using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;

namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField;

public partial class MultiLineInputField : SingleLineInputField
{
    private HorizontalStackLayout? m_buttonsLayout;

    private string? m_textWhenFirstFocused;

    private readonly Label m_label = new Labels.Label() 
    { 
        Style = Styles.GetLabelStyle(LabelStyle.Body200), 
        IsVisible = false,
        Margin = new Thickness(0, 4, 8, 0),
        VerticalTextAlignment = TextAlignment.Start,
        LineBreakMode = LineBreakMode.TailTruncation
    };
    
    public MultiLineInputField()
    {
        CreateButtons();
        SetupLabel();
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

        if (InnerGrid.Contains(m_buttonsLayout))
        {
            InnerGrid.Remove(m_buttonsLayout);
        }
        
        InnerGrid.Add(m_buttonsLayout, 0, 2);
        
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

    private void RemoveButtons()
    {
        if(InnerGrid.Contains(m_buttonsLayout))
            InnerGrid.Remove(m_buttonsLayout);
    }

    private void OnButtonTapped(bool isSaving)
    {
        if (isSaving)
        {
            m_textWhenFirstFocused = InputView?.Text;
            SaveCommand?.Execute(null);
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
        RemoveButtons();
        m_textWhenFirstFocused = null;
    }
}