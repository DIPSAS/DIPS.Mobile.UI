using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Labels;
using DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel;
using DIPS.Mobile.UI.Effects.Animation;
using DIPS.Mobile.UI.Effects.Animation.Effects;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Alert;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using DIPS.Mobile.UI.Resources.Styles.Span;
using Microsoft.Maui.Controls.Shapes;
using Animation = DIPS.Mobile.UI.Effects.Animation.Animation;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton.ImageButton;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Alerting.Alert;

public partial class AlertView : Grid
{
    private readonly HorizontalStackLayout m_buttonsContainer;
    private Image? m_icon;
    private ImageButton? m_closeIcon;
    private CustomTruncationTextView? m_titleAndDescriptionLabel;

    public AlertView()
    {
        Style = Styles.GetAlertStyle(AlertStyle.Information);
        AutomationId = "AlertView".ToDUIAutomationId<AlertView>();
        ColumnDefinitions =
        [
            new ColumnDefinition(GridLength.Auto),
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Auto)
        ];
        RowDefinitions =
        [
            new RowDefinition(GridLength.Star),
            new RowDefinition(GridLength.Auto),
        ];
        ColumnSpacing = Sizes.GetSize(SizeName.content_margin_small);
        Padding = Sizes.GetSize(SizeName.content_margin_small);
        
        UI.Effects.Layout.Layout.SetCornerRadius(this, Sizes.GetSize(SizeName.radius_small));
        UI.Effects.Layout.Layout.SetStrokeThickness(this, Sizes.GetSize(SizeName.stroke_medium));
        
        m_buttonsContainer = new HorizontalStackLayout
        {
            AutomationId = "HorizontalStackLayout".ToDUIAutomationId<AlertView>(),
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            Spacing = Sizes.GetSize(SizeName.size_2),
            IsVisible = false
        };

        // Set initial accessibility
        UpdateAccessibility();
    }

    public bool IsLargeAlert => !string.IsNullOrEmpty(Description);

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        UpdateButtonAlignment();
    }

    private void UpdateButtonAlignment()
    {
        if (LeftButtonCommand is null && RightButtonCommand is null) 
            return;
        
        Remove(m_buttonsContainer);
        
        if (IsLargeAlert)
        {
            m_buttonsContainer.Margin = new Thickness(0, Sizes.GetSize(SizeName.content_margin_small), 0, 0);
            this.Add(m_buttonsContainer, 1, 1);
            return;
        }
        
        var maxWidth = Measure(int.MaxValue, int.MaxValue).Width;
        var buttonsWidth = m_buttonsContainer.Measure(int.MaxValue, int.MaxValue).Width;
        var remainingWidth = Width - maxWidth - buttonsWidth;
        
        var buttonsWillFit = remainingWidth >= (Sizes.GetSize(SizeName.content_margin_small));
        
        if (buttonsWillFit)
        {
            m_buttonsContainer.Margin = new Thickness(0, 0, 0, 0);
            m_buttonsContainer.HorizontalOptions = LayoutOptions.End;
            this.Add(m_buttonsContainer, 2);
        }
        else
        {
            m_buttonsContainer.Margin = new Thickness(0, Sizes.GetSize(SizeName.content_margin_small), 0, 0);
            m_buttonsContainer.HorizontalOptions = LayoutOptions.Start;
            this.Add(m_buttonsContainer, 1, 1);
        }

#if __ANDROID__
        // Workaround for Android layout issue where buttons is not visible in some cases
        InvalidateMeasure();
#endif
    }

    private void OnButtonChanged()
    {
        m_buttonsContainer.IsVisible = LeftButtonCommand != null || RightButtonCommand != null;
        if (!m_buttonsContainer.IsVisible)
        {
            return;
        }

        m_buttonsContainer.Clear();
        if (LeftButtonCommand != null)
        {
            m_buttonsContainer.Add(CreateButton(LeftButtonText, LeftButtonCommand, LeftButtonCommandParameter, "LeftButton".ToDUIAutomationId<AlertView>()));
        }

        if (RightButtonCommand != null)
        {
            m_buttonsContainer.Add(CreateButton(RightButtonText, RightButtonCommand, RightButtonCommandParameter, "RightButton".ToDUIAutomationId<AlertView>()));
        }
        
        UpdateButtonAlignment();
        UpdateAccessibility();
    }

    private static Button CreateButton(string buttonText, ICommand buttonCommand, object buttonCommandParameter, string automationId)
    {
        var button = new Button
        {
            AutomationId = automationId,
            HorizontalOptions = LayoutOptions.Start,
            Style = Styles.GetButtonStyle(ButtonStyle.DefaultSmall),
            Command = buttonCommand,
            CommandParameter = buttonCommandParameter,
            Text = buttonText
        };
        
        // Set accessibility properties for the button
        SemanticProperties.SetDescription(button, buttonText);
        
        return button;
    }

    private void OnTitleOrDescriptionChanged()
    {
        if (m_titleAndDescriptionLabel != null && this.Contains(m_titleAndDescriptionLabel))
        {
            this.Remove(m_titleAndDescriptionLabel);
            m_titleAndDescriptionLabel.DisconnectHandlers();
        }
        
        var formattedString = new FormattedString
        {
            Spans =
            {
                new Span
                {
                    Text = Title, 
                    Style = IsLargeAlert ? Styles.GetSpanStyle(SpanStyle.UI200) : Styles.GetSpanStyle(SpanStyle.Body200),
                }
            }
        };

        if (IsLargeAlert)
        {
            formattedString.Spans.Add(new Span
            {
                Text = Environment.NewLine
            });
            
            formattedString.Spans.Add(new Span
            {
                Text = Description,
                Style = Styles.GetSpanStyle(SpanStyle.Body100)
            });
        }
        
        OnIsLargeAlertDetermined();

        m_titleAndDescriptionLabel = new CustomTruncationTextView()
        {
            MaxLines = GetTitleMaxLines(),
            AutomationId = "TitleAndDescriptionLabel".ToDUIAutomationId<AlertView>(),
            Style = Styles.GetLabelStyle(LabelStyle.Body200),
            TextColor = TextColor,
            FormattedText = formattedString,
            TruncatedTextStyle = Styles.GetLabelStyle(LabelStyle.UI100),
            TruncatedTextColor = TextColor,
            VerticalOptions = IsLargeAlert ? LayoutOptions.Start : LayoutOptions.Center,
        };

        m_titleAndDescriptionLabel.PropertyChanged -= TitleAndDescriptionLabelOnPropertyChanged;
        m_titleAndDescriptionLabel.PropertyChanged += TitleAndDescriptionLabelOnPropertyChanged;
        
        this.Add(m_titleAndDescriptionLabel, 1);
        
        UpdateButtonAlignment();
        UpdateAccessibility();

#if __IOS__
        InvalidateMeasure();
#endif
    }

    private int GetTitleMaxLines()
    {
        if (IsLargeAlert)
            return 4;
        
        return TitleTruncationMode is AlertTitleTruncationMode.Aggressive ? 1 : 2;
    }

    private void TitleAndDescriptionLabelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == CheckTruncatedLabel.IsTruncatedProperty.PropertyName)
        {
            TryEnableBottomSheetOnTap();
        }
    }

    private void TryEnableBottomSheetOnTap()
    {
        if (m_titleAndDescriptionLabel?.IsTruncated ?? false)
        {
            Touch.SetCommand(this, new Command(() =>
            {
                _ = BottomSheetService.Open(new AlertBottomSheet(this));
            }));
        }
        else
        {
            var touchEffect = Effects.FirstOrDefault(effect => effect is Touch);
            if (touchEffect is not null)
                Effects.Remove(touchEffect);
        }
    }

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        
        // Update accessibility when style changes
        if (propertyName == nameof(Style))
        {
            UpdateAccessibility();
        }
    }

    private AlertStyle GetCurrentAlertType()
    {
        // Infer alert type from the icon property value set by the style
        var iconSource = Icon?.ToString();
        
        if (iconSource != null)
        {
            if (iconSource.Contains("important_line"))
                return AlertStyle.Error;
            if (iconSource.Contains("alert_line"))
                return AlertStyle.Warning;
            if (iconSource.Contains("check_circle_line"))
                return AlertStyle.Success;
            if (iconSource.Contains("information_line"))
                return AlertStyle.Information;
        }
        
        // Fallback: check background color
        var backgroundColor = BackgroundColor;
        if (Equals(backgroundColor, Colors.GetColor(ColorName.color_surface_danger)))
            return AlertStyle.Error;
        if (Equals(backgroundColor, Colors.GetColor(ColorName.color_surface_warning)))
            return AlertStyle.Warning;
        if (Equals(backgroundColor, Colors.GetColor(ColorName.color_surface_success)))
            return AlertStyle.Success;
            
        // Default to Information
        return AlertStyle.Information;
    }

    private void UpdateAccessibility()
    {
        // Make the container non-focusable to allow navigation to child elements
        AutomationProperties.SetIsInAccessibleTree(this, false);
        
        // Ensure the title/description label is accessible if it exists
        if (m_titleAndDescriptionLabel != null)
        {
            SemanticProperties.SetDescription(m_titleAndDescriptionLabel, GetAccessibilityDescription());
            SemanticProperties.SetHeadingLevel(m_titleAndDescriptionLabel, SemanticHeadingLevel.Level2);
        }
    }

    private string GetAccessibilityDescription()
    {
        var alertTypeName = GetAlertTypeName();
        var description = $"{alertTypeName}";
        
        if (!string.IsNullOrEmpty(Title))
        {
            description += $". {Title}";
        }
        
        if (!string.IsNullOrEmpty(Description))
        {
            description += $". {Description}";
        }
        
        return description;
    }

    private string GetAlertTypeName()
    {
        return GetCurrentAlertType() switch
        {
            AlertStyle.Information => DUILocalizedStrings.Information,
            AlertStyle.Error => DUILocalizedStrings.Error,
            AlertStyle.Warning => DUILocalizedStrings.Warning,
            AlertStyle.Success => DUILocalizedStrings.Success,
            _ => DUILocalizedStrings.Alert
        };
    }

    /// <summary>
    /// The AlertView is large if it has a description, if it is large we want to align the icon and close button to the top of the alert
    /// </summary>
    private void OnIsLargeAlertDetermined()
    {
        if(!IsLargeAlert)
            return;
        
        if (m_icon is not null)
        {
            m_icon.VerticalOptions = LayoutOptions.Start;
        }

        if (m_closeIcon is not null)
        {
            m_closeIcon.VerticalOptions = LayoutOptions.Start;
        }
    }

    private void OnIconChanged()
    {
        if (this.Contains(m_icon))
        {
            this.Remove(m_icon);
            m_icon?.DisconnectHandlers();
        }
        
        m_icon = new Image
        {
            AutomationId = "Image".ToDUIAutomationId<AlertView>(),
            HeightRequest = Sizes.GetSize(SizeName.size_6),
            WidthRequest = Sizes.GetSize(SizeName.size_6),
            VerticalOptions = IsLargeAlert ? LayoutOptions.Start : LayoutOptions.Center,
            Source = Icon
        };
        
        // Hide icon from screen readers since alert type is already announced
        m_icon.SetValue(AutomationProperties.IsInAccessibleTreeProperty, false);
        
        m_icon.SetBinding(Image.TintColorProperty, static (AlertView alertView) => alertView.IconColor, source: this);

        this.Add(m_icon);
        
        Grid.SetRowSpan(m_icon, 2);
    }

    private void OnShouldAnimateChanged()
    {
        if (ShouldAnimate)
        {
            Animation.SetFadeIn(this, new AnimationConfig
            {
                StartingValue = .25f
            });
            Animation.SetScaleIn(this, new AnimationConfig
            {
                Easing = Easing.SpringOut,
                StartingValue = .75f
            });
        }
        else
        {
            Effects.Remove(Effects.FirstOrDefault(effect => effect is FadeInEffect));
            Effects.Remove(Effects.FirstOrDefault(effect => effect is ScaleInEffect));
        }
    }

    internal void Animate()
    {
        if(!IsVisible || !ShouldAnimate)
            return;
        
        this.Animate(AnimationType.FadeIn, config =>
        {
            config.StartingValue = .25f;
        });
        this.Animate(AnimationType.ScaleIn, config =>
        {
            config.Easing = Easing.SpringOut;
            config.StartingValue = .75f;
        });
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is not null)
        {
            OnShouldAnimateChanged();
            AlertViewService.OnAnimationTriggered += Animate;
        }
        else
        {
            AlertViewService.OnAnimationTriggered -= Animate;
            if (m_titleAndDescriptionLabel is not null)
            {
                m_titleAndDescriptionLabel.PropertyChanged -= TitleAndDescriptionLabelOnPropertyChanged;
            }
        }
    }

    private void OnShowCloseButtonChanged()
    {
        if (!ShowCloseButton)
            return;

        m_closeIcon = new ImageButton
        {
            AutomationId = "CloseImage".ToDUIAutomationId<AlertView>(),
            Source = Icons.GetIcon(IconName.close_line),
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            WidthRequest = Sizes.GetSize(SizeName.size_5),
            HorizontalOptions = LayoutOptions.End,
            TintColor = Colors.GetColor(ColorName.color_text_on_fill_information),
            AdditionalHitBoxSize = Sizes.GetSize(SizeName.size_2),
            Command = new Command(() => IsVisible = false),
            VerticalOptions = IsLargeAlert ? LayoutOptions.Start : LayoutOptions.Center
        };
        
        // Set accessibility properties for the close button
        SemanticProperties.SetDescription(m_closeIcon, DUILocalizedStrings.Accessibility_CloseAlert);
        SemanticProperties.SetHint(m_closeIcon, DUILocalizedStrings.Accessibility_TapToDismissAlert);
            
        this.Add(m_closeIcon, 2);
        UpdateAccessibility();
    }

    private void OnTitleTruncationModeChanged()
    {
        if (m_titleAndDescriptionLabel is not null && !IsLargeAlert)
        {
            m_titleAndDescriptionLabel.MaxLines = GetTitleMaxLines();
        }
    }
}