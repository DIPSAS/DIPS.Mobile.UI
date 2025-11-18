using DIPS.Mobile.UI.API.Accessibility;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal;
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
#nullable disable
    protected InputView InputView { get; set; }
#nullable enable

    protected readonly Label HelpTextLabel = new()
    {
        Style = Styles.GetLabelStyle(LabelStyle.Body100),
        IsVisible = false,
        Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_medium), 0, 0, 0)
    };
    
    protected readonly Label HeaderTextLabel = new()
    {
        Style = Styles.GetLabelStyle(LabelStyle.Body200),
        TextColor = Colors.GetColor(ColorName.color_text_subtle),
        VerticalOptions = LayoutOptions.Center,
        HorizontalOptions = LayoutOptions.Start,
        AnchorX = 0
    };

    protected Grid ContentBorderGrid { get; } = new(){AutomationId = "ContentBorderGrid".ToDUIAutomationId<SingleLineInputField>()};
    
    protected Grid InnerGrid { get; } = new()
    { 
        AutomationId = "InnerGrid".ToDUIAutomationId<SingleLineInputField>(),
        Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_medium), Sizes.GetSize(SizeName.content_margin_small), Sizes.GetSize(SizeName.content_margin_small), Sizes.GetSize(SizeName.content_margin_small)),
        RowDefinitions = [new(GridLength.Star), new(GridLength.Auto), new(GridLength.Auto)]
    };

    private readonly Border m_contentBorder = new()
    {
        AutomationId = "ContentBorder".ToDUIAutomationId<SingleLineInputField>(),
        BackgroundColor = Colors.GetColor(ColorName.color_surface_default)
    };

    public SingleLineInputField()
    {
        Style = Styles.GetInputFieldStyle(InputFieldStyle.Default);
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        RowSpacing = Sizes.GetSize(SizeName.content_margin_small);
        
        AutomationProperties.SetIsInAccessibleTree(this, false);
        AutomationProperties.SetIsInAccessibleTree(ContentBorderGrid, false);
        
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
        HeaderTextLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.HeaderText, source: this);
        
        InnerGrid.Add(HeaderTextLabel);
        
        SetHeaderTextVisibility();
        
        AutomationProperties.SetIsInAccessibleTree(HeaderTextLabel, false);
    }
    
    private void SetupHelpText()
    {
        HelpTextLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.HelpText, source: this);
        HelpTextLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.HelpTextColor, source: this);
        
        this.Add(HelpTextLabel, 0, 1);
        
        AutomationProperties.SetIsInAccessibleTree(HelpTextLabel, false);
    }

    private void SetupContentBorder()
    {
        ContentBorderGrid.Add(InnerGrid);
        m_contentBorder.Content = ContentBorderGrid;
        m_contentBorder.SetBinding(MinimumHeightRequestProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.MinimumHeightRequest, source: this);
        m_contentBorder.SetBinding(Border.StrokeThicknessProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.BorderThickness, source: this);
        m_contentBorder.SetBinding(Border.StrokeProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.BorderColor, source: this);
        
        Touch.SetCommand(m_contentBorder, new Command(Focus));
        Touch.SetIsButtonTraitEnabled(m_contentBorder, false);
        
        SemanticProperties.SetDescription(m_contentBorder, SemanticDescription.GetDescription(DUILocalizedStrings.Accessability_InputField_HelpText, ControlType.Input));
    }


    protected virtual void CreateInputView()
    {
        InputView = new Entry.Entry
        {
            AutomationId = "InputView".ToDUIAutomationId<SingleLineInputField>(),
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
        InputView.Margin = new Thickness(0, Sizes.GetSize(SizeName.content_margin_xsmall), Sizes.GetSize(SizeName.content_margin_small), 0);
        InputView.SetBinding(BackgroundColorProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.BackgroundColor, source: this);
        InputView.SetBinding(InputView.TextColorProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.InputTextColor, source: this);
        InputView.SetBinding(InputView.TextProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.Text, source: this);
        InputView.SetBinding(InputView.IsFocusedProperty, static (SingleLineInputField singleLineInputField) => singleLineInputField.IsFocused, source: this);
        
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
        SetTouchEnabled(isEnabled);
        
        SetStyle();
    }

    protected void SetTouchEnabled(bool isEnabled)
    {
        Touch.SetIsEnabled(m_contentBorder, isEnabled);
    }

    protected virtual void OnInputViewFocused(object? sender, FocusEventArgs e)
    {
        var prevBorderThickness = BorderThickness;
        
        SetStyle();

        ValidateMargin(prevBorderThickness);
        ChangeHeaderTextStyle();
        
        Focused?.Invoke(this, e);
        
        SetTouchEnabled(false);
        
        AutomationProperties.SetIsInAccessibleTree(m_contentBorder, false);
    }

    private void ValidateMargin(double prevBorderThickness)
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
        
// An ugly workaround to hide keyboard on Android
#if __ANDROID__
        InputView!.IsEnabled = false;
        InputView.IsEnabled = true;
#endif
        
        var prevBorderThickness = BorderThickness;
        
        SetStyle();
        
        SetTouchEnabled(true);

        UpdateInputViewVisibility();
        ValidateMargin(prevBorderThickness);
        OnTextChanged();
        
        Unfocused?.Invoke(this, e);
        
        AutomationProperties.SetIsInAccessibleTree(m_contentBorder, true);
    }

    protected virtual void SetStyle()
    {
        var style = IsEnabled ? Styles.GetInputFieldStyle(InputFieldStyle.Default) : Styles.GetInputFieldStyle(InputFieldStyle.Disabled);

        if (IsFocused)
            style = Styles.GetInputFieldStyle(InputFieldStyle.Focused);

        Style = style;
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

        if (this is not MultiLineInputField.MultiLineInputField)
        {
            InputView.IsVisible = !string.IsNullOrEmpty(Text);
        }
        
        ChangeHeaderTextStyle();
        UpdateSemanticHint();
    }

    protected virtual void ChangeHeaderTextStyle()
    {
        HeaderTextLabel.Style = InputView.IsFocused ? Styles.GetLabelStyle(LabelStyle.Body100) : Styles.GetLabelStyle(Text == string.Empty ? LabelStyle.Body200 : LabelStyle.Body100);
        
        if(InputView.IsFocused)
            HeaderTextLabel.ScaleTo(.85);
        else
        {
            if (Text == string.Empty)
            {
                HeaderTextLabel.ScaleTo(1, easing: Easing.CubicOut);
            }
            else
            {
                HeaderTextLabel.ScaleTo(.85, easing: Easing.CubicOut);
            }
        }
    }

    private void UpdateInputViewVisibility()
    {
        InputView.IsVisible = Text != string.Empty;
    }
    
    public new async void Focus()
    {
        try
        {
            await Task.Delay(1);
            InputView!.IsVisible = true;
            InputView?.Focus();
        }
        catch (Exception e)
        {
            
        }
    }
    
    public new void Unfocus()
    {
        InputView?.Unfocus();
    }

    private void OnHelpTextChanged()
    {
        UpdateSemanticHint();
        
        HelpTextLabel.IsVisible = HelpText != string.Empty;
    }
    private void OnHeaderTextChanged()
    {
        SetHeaderTextVisibility();

        UpdateSemanticHint();
    }

    private void SetHeaderTextVisibility()
    {
        var headerIsVisible = !string.IsNullOrEmpty(HeaderText);
        HeaderTextLabel.IsVisible = headerIsVisible;

        InnerGrid.RowDefinitions.First().Height = headerIsVisible ? GridLength.Star : GridLength.Auto;
        RowDefinitions.First().Height = headerIsVisible ? GridLength.Star : GridLength.Auto;
    }

    private void UpdateSemanticHint()
    {
        SemanticProperties.SetHint(m_contentBorder, HeaderText + ", " + Text + ", "  + HelpText);
    }
}