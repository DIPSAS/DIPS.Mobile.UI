using System.Windows.Input;
using DIPS.Mobile.UI.Effects.Animation;
using DIPS.Mobile.UI.Effects.Animation.Effects;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Alert;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using DIPS.Mobile.UI.Resources.Styles.Span;
using Animation = DIPS.Mobile.UI.Effects.Animation.Animation;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Alerting.Alert;

public partial class AlertView : Grid
{
    private readonly HorizontalStackLayout m_buttonsContainer;
    private Image? m_image;
    private Label m_titleAndDescriptionLabel;

    public AlertView()
    {
        Style = Styles.GetAlertStyle(AlertStyle.Information);
        UI.Effects.Layout.Layout.SetCornerRadius(this, Sizes.GetSize(SizeName.radius_small));
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
        
        m_buttonsContainer = new HorizontalStackLayout
        {
            AutomationId = "HorizontalStackLayout".ToDUIAutomationId<AlertView>(),
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            Spacing = Sizes.GetSize(SizeName.size_2),
            IsVisible = false,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_2), 0, 0)
        };
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
            this.Add(m_buttonsContainer, 1, 1);
            return;
        }
        
        var maxWidth = Measure(int.MaxValue, int.MaxValue).Width;
        var buttonsWidth = m_buttonsContainer.Measure(int.MaxValue, int.MaxValue).Width;
        var remainingWidth = Width - maxWidth - buttonsWidth;
        var buttonsWillFit = remainingWidth >= Sizes.GetSize(SizeName.content_margin_small);
        
        if (buttonsWillFit)
        {
            m_buttonsContainer.Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_small));
            this.Add(m_buttonsContainer, 2);
        }
        else
        {
            m_buttonsContainer.Margin = new Thickness(0, Sizes.GetSize(SizeName.content_margin_small), 0, 0);
            this.Add(m_buttonsContainer, 1, 1);
        }
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
            if (RightButtonCommand != null)
            {
                m_buttonsContainer.Clear();
            }
            
            m_buttonsContainer.Add(CreateButton(LeftButtonText, LeftButtonCommand, LeftButtonCommandParameter, "LeftButton".ToDUIAutomationId<AlertView>()));
        }

        if (RightButtonCommand != null)
        {
            m_buttonsContainer.Add(CreateButton(RightButtonText, RightButtonCommand, RightButtonCommandParameter, "RightButton".ToDUIAutomationId<AlertView>()));
        }
        
        UpdateButtonAlignment();
    }

    private static Button CreateButton(string buttonText, ICommand buttonCommand, object buttonCommandParameter, string automationId)
    {
        return new Button
        {
            AutomationId = automationId,
            HorizontalOptions = LayoutOptions.Start,
            Style = Styles.GetButtonStyle(ButtonStyle.SecondarySmall),
            Command = buttonCommand,
            CommandParameter = buttonCommandParameter,
            Text = buttonText
        };
    }

    private void OnTitleOrDescriptionChanged()
    {
        if (Contains(m_titleAndDescriptionLabel))
        {
            Remove(m_titleAndDescriptionLabel);
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

            if (m_image is not null)
            {
                m_image.VerticalOptions = LayoutOptions.Start;
            }
        }

        m_titleAndDescriptionLabel = new Label
        {
            MaxLines = IsLargeAlert ? 4 : 1,
            LineBreakMode = LineBreakMode.TailTruncation,
            AutomationId = "TitleAndDescriptionLabel".ToDUIAutomationId<AlertView>(),
            Style = Styles.GetLabelStyle(LabelStyle.Body200),
            FormattedText = formattedString,
            TruncatedText = $" ...{DUILocalizedStrings.More.ToLower()}",
            TruncatedTextStyle = SpanStyle.UI100,
            TruncatedTextColor = Colors.GetColor(ColorName.color_text_on_fill_information),
            VerticalTextAlignment = IsLargeAlert ? TextAlignment.Start : TextAlignment.Center,
        };
        
        this.Add(m_titleAndDescriptionLabel, 1);
        
        UpdateButtonAlignment();
    }

    private void OnIconChanged()
    {
        if (Contains(m_image))
        {
            Remove(m_image);
        }
        
        m_image = new Image
        {
            AutomationId = "Image".ToDUIAutomationId<AlertView>(),
            HeightRequest = Sizes.GetSize(SizeName.size_6),
            WidthRequest = Sizes.GetSize(SizeName.size_6),
            VerticalOptions = LayoutOptions.Center,
            Source = Icon
        };
        
        m_image.SetBinding(Image.TintColorProperty, static (AlertView alertView) => alertView.IconColor, source: this);

        Add(m_image);
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
        }
    }
}