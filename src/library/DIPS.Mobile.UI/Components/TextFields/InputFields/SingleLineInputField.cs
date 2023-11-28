using DIPS.Mobile.UI.API.Accessibility;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.InputField;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.TextFields.InputFields;

public partial class SingleLineInputField : Grid
{
    private Style? m_lastStyle;
    
#nullable disable
    internal InputView InputView { get; set; }
#nullable enable

    private readonly Label m_helpTextLabel = new()
    {
        Style = Styles.GetLabelStyle(LabelStyle.Body100),
        TextColor = Colors.GetColor(ColorName.color_neutral_60),
        IsVisible = false,
        Margin = new Thickness(16, 0, 0, 0)
    };
    
    private readonly Label m_headerText = new()
    {
        Style = Styles.GetLabelStyle(LabelStyle.Body200),
        TextColor = Colors.GetColor(ColorName.color_neutral_70),
        VerticalTextAlignment = TextAlignment.Center
    };
    
    internal Grid InnerGrid { get; } = new()
    { 
        Margin = new Thickness(16, 8, 8, 8),
        RowDefinitions = new RowDefinitionCollection { new(GridLength.Star), new(GridLength.Auto), new(GridLength.Auto) }
    };

    private readonly Border m_contentBorder = new()
    {
        BackgroundColor = Colors.GetColor(ColorName.color_system_white)
    };

    public SingleLineInputField()
    {
        Style = Styles.GetTextAreaStyle(InputFieldStyle.Default);
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        RowSpacing = 4;
        
        AutomationProperties.SetIsInAccessibleTree(this, false);
        
        AddRowDefinition(new RowDefinition(GridLength.Star));
        AddRowDefinition(new RowDefinition(GridLength.Auto));
        
        SetupHeaderText();
        SetupHelpText();
        
        CreateInputView();
        SetupInputView();
        
        SetupContentBorder();
        
        this.Add(m_contentBorder);
    }

    private void SetupHeaderText()
    {
        m_headerText.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, new Binding(nameof(HeaderText), source: this));
        
        InnerGrid.Add(m_headerText);
        
        AutomationProperties.SetIsInAccessibleTree(m_headerText, false);
    }
    
    private void SetupHelpText()
    {
        m_helpTextLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, new Binding(nameof(HelpText), source: this));
        
        this.Add(m_helpTextLabel, 0, 1);
        
        AutomationProperties.SetIsInAccessibleTree(m_helpTextLabel, false);
    }

    private void SetupContentBorder()
    {
        m_contentBorder.Content = InnerGrid;
        m_contentBorder.SetBinding(MinimumHeightRequestProperty, new Binding(nameof(MinimumHeightRequest), source: this));
        m_contentBorder.SetBinding(Border.StrokeThicknessProperty, new Binding(nameof(BorderThickness), source: this));
        m_contentBorder.SetBinding(Border.StrokeProperty,  new Binding(nameof(BorderColor), source: this));
        
        Touch.SetCommand(m_contentBorder, new Command(Focus));
        
        SemanticProperties.SetDescription(m_contentBorder, SemanticDescription.GetDescription(DUILocalizedStrings.Accessability_InputField_HelpText, ControlType.Button));
    }

    protected virtual void CreateInputView()
    {
        InputView = new Entry.Entry
        {
            IsSpellCheckEnabled = false, 
            HasBorder = false, 
            ShouldUseDefaultPadding = false,
            VerticalTextAlignment = TextAlignment.Start
        };
    }
    
    private void SetupInputView()
    {
        InputView.FontSize = 14;
        InputView.IsVisible = false;
        InputView.Margin = new Thickness(0, 4, 8, 0);
        InputView.SetBinding(BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
        InputView.SetBinding(InputView.TextColorProperty, new Binding(nameof(InputTextColor), source: this));
        InputView.SetBinding(InputView.TextProperty, new Binding(nameof(Text), source: this));
        InputView.SetBinding(InputView.IsFocusedProperty, new Binding(nameof(IsFocused), source: this));
        
        InputView.Focused += OnInputViewFocused;
        InputView.Unfocused += OnInputViewUnFocused;
        
        InnerGrid.Add(InputView, 0, 1);
    }

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == nameof(IsEnabled))
        {
            OnIsEnabledChanged(IsEnabled);    
        }
    }

    private void OnIsEnabledChanged(bool isEnabled)
    {
        Touch.SetIsEnabled(m_contentBorder, isEnabled);
        
        if (isEnabled && m_lastStyle is not null)
        {
            Style = m_lastStyle;
        }
        else
        {
            m_lastStyle = Style;
            Style = Styles.GetTextAreaStyle(InputFieldStyle.Disabled);
        }
    }

    protected virtual void OnInputViewFocused(object? sender, FocusEventArgs e)
    {
        var prevBorderThickness = BorderThickness;
        
        m_lastStyle = Style;
        Style = Styles.GetTextAreaStyle(InputFieldStyle.Focused);

        ValidateMargin(prevBorderThickness);
        m_headerText.Style = Styles.GetLabelStyle(LabelStyle.Body100);
        
        Focused?.Invoke(this, e);
        
        Touch.SetIsEnabled(m_contentBorder, false);
        
        AutomationProperties.SetIsInAccessibleTree(m_contentBorder, false);
    }

    private void ValidateMargin(int prevBorderThickness)
    {
        var differenceInThickness = Math.Abs(BorderThickness - prevBorderThickness);
        if(differenceInThickness == 0)
            return;
        
        // Changing BorderThickness will modify the margin of elements inside, thus we will have to change the margin so that we see no difference 
        if (BorderThickness > prevBorderThickness)
        {
            InnerGrid.Margin -= differenceInThickness;
        }
        else
        {
            InnerGrid.Margin += differenceInThickness;
        }
    }

    protected virtual void OnInputViewUnFocused(object? sender, FocusEventArgs e)
    {
        if(m_lastStyle is null)
            return;
        
// An ugly workaround to hide keyboard on Android
#if __ANDROID__
        InputView!.IsEnabled = false;
        InputView.IsEnabled = true;
#endif
        
        var prevBorderThickness = BorderThickness;
        
        Style = m_lastStyle;
        
        Touch.SetIsEnabled(m_contentBorder, true);

        UpdateInputViewVisibility();
        ValidateMargin(prevBorderThickness);
        OnTextChanged();
        
        Unfocused?.Invoke(this, e);
        
        AutomationProperties.SetIsInAccessibleTree(m_contentBorder, true);
    }
    
    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
            return;

        if (InputView is null)
            return;
        
        InputView.Focused -= OnInputViewFocused;
        InputView.Unfocused -= OnInputViewUnFocused;
    }

    private void OnBorderCornerRadiusChanged()
    {
        m_contentBorder.StrokeShape = new RoundRectangle { CornerRadius = BorderCornerRadius };
    }

    protected virtual void OnTextChanged()
    {
        if (InputView is null)
            return;

        m_headerText.Style = InputView.IsFocused ? Styles.GetLabelStyle(LabelStyle.Body100) : Styles.GetLabelStyle(Text == string.Empty ? LabelStyle.Body200 : LabelStyle.Body100);
    }

    private void UpdateInputViewVisibility()
    {
        InputView.IsVisible = Text != string.Empty;
    }
    
    public new void Focus()
    {
        InputView!.IsVisible = true;
        InputView?.Focus();
    }
    
    public new void Unfocus()
    {
        InputView?.Unfocus();
    }

    private void OnHelpTextChanged()
    {
        UpdateSemanticHint();
        
        m_helpTextLabel.IsVisible = HelpText != string.Empty;
    }

    private void OnHeaderTextChanged()
    {
        UpdateSemanticHint();
    }

    private void UpdateSemanticHint()
    {
        SemanticProperties.SetHint(m_contentBorder, HeaderText + ", " + HelpText);
    }
}