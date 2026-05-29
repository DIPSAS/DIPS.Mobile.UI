using DIPS.Mobile.UI.Effects.Layout;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Animations;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using SkiaSharp.Extended.UI.Controls;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using GraphicsColors = Microsoft.Maui.Graphics.Colors;
using LayoutEffect = DIPS.Mobile.UI.Effects.Layout.Layout;

namespace DIPS.Mobile.UI.Components.StepFlow;

/// <summary>
/// A single step in a <see cref="StepFlow"/>. Authored as XAML default content of the parent
/// <see cref="StepFlow"/>; the orchestrating accordion behavior is owned by the container.
/// </summary>
[ContentProperty(nameof(Content))]
public partial class StepFlowItem : ContentView
{
    internal const uint ExpandDurationMs = 380;
    private const uint CollapseDurationMs = 280;
    private const uint LiftDurationMs = 300;
    private const uint CompletionDimDurationMs = 360;
    private const uint IndicatorShiftDurationMs = 260;

    private readonly double m_indicatorSlotWidth;

    private readonly Grid m_root = new();
    private readonly Grid m_headerGrid = new();
    private readonly ContentView m_indicatorHost = new();
    private readonly Label m_titleLabel = new();
    private readonly Label m_subtitleLabel = new();
    private readonly VerticalStackLayout m_titleStack = new();
    private readonly ContentView m_bodyContainer = new();
    private readonly SKLottieView m_completionAnimation;
    private readonly Command m_cardTappedCommand;

    private string m_animationToken;

    /// <summary>The 1-based number rendered in the default indicator.</summary>
    internal int DisplayNumber { get; set; } = 1;

    /// <summary>The total number of steps in the parent <see cref="StepFlow"/>. Used for accessibility announcements.</summary>
    internal int TotalSteps { get; set; } = 1;

    /// <summary>Internal index in the parent <see cref="StepFlow"/>.</summary>
    internal int Index { get; set; } = -1;

    /// <summary>Raised when the user taps the card. The container decides whether the tap should activate the step.</summary>
    internal event EventHandler? CardTapped;

    public StepFlowItem()
    {
        m_animationToken = "stepflow-item-" + Guid.NewGuid().ToString("N");
        m_cardTappedCommand = new Command(InvokeCardTapped);
        m_indicatorSlotWidth = Sizes.GetSize(SizeName.size_6) + Sizes.GetSize(SizeName.size_3);

        BackgroundColor = GraphicsColors.Transparent;
        Padding = 0;

        m_completionAnimation = new SKLottieView
        {
            Source = Animations.GetAnimation(AnimationName.task_complete),
            HeightRequest = Sizes.GetSize(SizeName.size_6),
            WidthRequest = Sizes.GetSize(SizeName.size_6),
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            IsAnimationEnabled = false,
            IsVisible = false,
            Opacity = 0,
            RepeatCount = 0,
            InputTransparent = true
        };

        // Indicator slot — overlays the title at the start of the header. The title gets a
        // measured left margin when the Lottie checkmark appears on completion, so Android
        // re-wraps long titles using the reduced width instead of clipping transformed text.
        m_indicatorHost.HorizontalOptions = LayoutOptions.Start;
        m_indicatorHost.VerticalOptions = LayoutOptions.Center;
        m_indicatorHost.Content = m_completionAnimation;

        // ---- Title / subtitle stack ----
        m_titleLabel.Style = Styles.GetLabelStyle(LabelStyle.Body300);
        m_titleLabel.LineBreakMode = LineBreakMode.WordWrap;
        m_titleLabel.HorizontalOptions = LayoutOptions.Fill;
        m_titleLabel.VerticalOptions = LayoutOptions.Center;

        m_subtitleLabel.Style = Styles.GetLabelStyle(LabelStyle.UI100);
        m_subtitleLabel.TextColor = Colors.GetColor(ColorName.color_text_subtle);
        m_subtitleLabel.IsVisible = false;
        m_subtitleLabel.LineBreakMode = LineBreakMode.WordWrap;
        m_subtitleLabel.HorizontalOptions = LayoutOptions.Fill;

        m_titleStack.Spacing = 0;
        m_titleStack.VerticalOptions = LayoutOptions.Center;
        m_titleStack.HorizontalOptions = LayoutOptions.Fill;
        m_titleStack.Children.Add(m_titleLabel);
        m_titleStack.Children.Add(m_subtitleLabel);

        // ---- Header grid ----
        // Single-column overlay so the title can sit flush left and the indicator floats
        // above it. The title's left margin is animated to push it past the Lottie.
        m_headerGrid.ColumnDefinitions = [new ColumnDefinition(GridLength.Star)];
        m_headerGrid.Padding = 0;
        m_headerGrid.BackgroundColor = GraphicsColors.Transparent;
        m_headerGrid.Add(m_titleStack, 0, 0);
        m_headerGrid.Add(m_indicatorHost, 0, 0);

        // The title stack and indicator host are purely visual; their text is composed into
        // a single accessibility announcement on the card. Excluding them keeps screen
        // readers from navigating into individual labels and animation views.
        AutomationProperties.SetExcludedWithChildren(m_titleStack, true);
        AutomationProperties.SetExcludedWithChildren(m_indicatorHost, true);

        // Use the canonical DUI Touch effect for card tap handling so the platform applies
        // the "Button" accessibility trait (UIAccessibilityTrait.Button on iOS, button role
        // on Android via the accessibility delegate). Setting SemanticProperties.Description
        // before the effect is required for the trait to be applied.
        Touch.SetIsButtonTraitEnabled(m_root, true);
        RefreshAccessibilityDescription();

        // ---- Body container (animated) ----
        // IsClippedToBounds lets us animate only HeightRequest while content stays at its
        // natural size and is masked. This removes the need for parallel fade/translate
        // animations on the body, cutting per-frame work during expand/collapse.
        m_bodyContainer.HeightRequest = 0;
        m_bodyContainer.IsVisible = false;
        m_bodyContainer.IsClippedToBounds = true;
        m_bodyContainer.Margin = new Thickness(0, Sizes.GetSize(SizeName.size_3), 0, 0);

        // ---- Root grid (header + body inside a Border) ----
        m_root.RowDefinitions =
        [
            new RowDefinition(GridLength.Auto),
            new RowDefinition(GridLength.Auto)
        ];
        m_root.Add(m_headerGrid, 0, 0);
        m_root.Add(m_bodyContainer, 0, 1);

        // Style the root via the Layout effect rather than wrapping in a Border. This keeps
        // the visual tree flat (one less native handler per item) and uses the same stroke /
        // corner-radius primitives as the rest of DUI.
        m_root.BackgroundColor = Colors.GetColor(ColorName.color_surface_default);
        m_root.Padding = Sizes.GetSize(SizeName.size_4);
        LayoutEffect.SetCornerRadius(m_root, new CornerRadius(Sizes.GetSize(SizeName.size_2)));
        LayoutEffect.SetStroke(m_root, Colors.GetColor(ColorName.color_border_default));
        LayoutEffect.SetStrokeThickness(m_root, Sizes.GetSize(SizeName.stroke_medium));

        base.Content = m_root;

        // Initial visual state matches default StepFlowItemState.Disabled.
        ApplyStateVisuals(StepFlowItemState.Disabled, animate: false);
    }

    private void OnTitleChanged()
    {
        RefreshTitleText();
    }

    private void OnSubtitleChanged()
    {
        m_subtitleLabel.Text = Subtitle ?? string.Empty;
        m_subtitleLabel.IsVisible = !string.IsNullOrEmpty(Subtitle);
        RefreshAccessibilityDescription();
    }

    private void OnContentChanged()
    {
        m_bodyContainer.Content = Content;
    }

    private void OnIndicatorTemplateChanged()
    {
        if (IndicatorTemplate is null)
        {
            // Restore default indicator slot (Lottie only).
            var defaultOverlay = new Grid
            {
                HeightRequest = Sizes.GetSize(SizeName.size_6),
                WidthRequest = Sizes.GetSize(SizeName.size_6),
                VerticalOptions = LayoutOptions.Center
            };
            defaultOverlay.Children.Add(m_completionAnimation);
            m_indicatorHost.Content = defaultOverlay;
            return;
        }

        if (IndicatorTemplate.CreateContent() is View customIndicator)
        {
            m_indicatorHost.Content = customIndicator;
        }
    }

    internal void RefreshTitleText()
    {
        // Renders the header as "<Step> N: <Title>", e.g. "Step 2: Confirm patient" /
        // "Steg 2: Confirm patient" depending on culture.
        m_titleLabel.Text = $"{DUILocalizedStrings.StepFlow_Step} {DisplayNumber}: {Title}";
        RefreshAccessibilityDescription();
    }

    /// <summary>
    /// Builds a single accessibility announcement that conveys position, title, subtitle and
    /// state to screen readers. The visual header children are excluded from the
    /// accessibility tree so VoiceOver / TalkBack focus the card as one unit.
    /// </summary>
    private void RefreshAccessibilityDescription()
    {
        var position = string.Format(DUILocalizedStrings.StepFlow_Accessibility_StepXOfY, DisplayNumber, TotalSteps);
        var description = string.IsNullOrEmpty(Subtitle)
            ? $"{position}: {Title}"
            : $"{position}: {Title}. {Subtitle}";

        SemanticProperties.SetDescription(m_root, description);
        SemanticProperties.SetHint(m_root, GetStateHint(State));
    }

    private static string GetStateHint(StepFlowItemState state) => state switch
    {
        StepFlowItemState.Active => DUILocalizedStrings.StepFlow_Accessibility_Active,
        StepFlowItemState.Completed => DUILocalizedStrings.StepFlow_Accessibility_Completed,
        StepFlowItemState.Disabled => DUILocalizedStrings.StepFlow_Accessibility_Disabled,
        StepFlowItemState.Error => DUILocalizedStrings.StepFlow_Accessibility_Error,
        _ => string.Empty
    };

    private void InvokeCardTapped()
    {
        if (!IsEnabled)
            return;
        if (State == StepFlowItemState.Active)
            return;
        if (State == StepFlowItemState.Completed && LockWhenCompleted)
            return;
        if (State == StepFlowItemState.Disabled && Parent is StepFlow flow && !flow.AllowDirectStepActivation)
            return;

        CardTapped?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateCardTapTarget(StepFlowItemState state)
    {
        var command = state == StepFlowItemState.Active ? null : m_cardTappedCommand;
        if (ReferenceEquals(Touch.GetCommand(m_root), command))
            return;

        Touch.SetCommand(m_root, command!);
    }

    private void OnStateChanged(StepFlowItemState oldState, StepFlowItemState newState)
    {
        if (oldState == newState)
            return;
        ApplyStateVisuals(newState, animate: true);
        RefreshAccessibilityDescription();
    }

    /// <summary>
    /// Applies the visuals for <paramref name="state"/>. When <paramref name="animate"/> is
    /// <c>true</c>, runs the choreographed expand/collapse/completion/lift animations. When
    /// <c>false</c> (initial setup), applies the visuals instantly.
    /// </summary>
    internal void ApplyStateVisuals(StepFlowItemState state, bool animate)
    {
        UpdateCardTapTarget(state);

        switch (state)
        {
            case StepFlowItemState.Disabled:
                if (animate)
                {
                    _ = CollapseAsync();
                    this.AbortAnimation(m_animationToken + "-opacity");
                    new Animation(v => Opacity = v, Opacity, 1)
                        .Commit(this, m_animationToken + "-opacity", rate: 16, length: CompletionDimDurationMs, easing: Easing.CubicOut);
                }
                else
                {
                    Opacity = 1;
                    m_bodyContainer.IsVisible = false;
                    m_bodyContainer.HeightRequest = 0;
                }
                StopCompletionAnimation();
                AnimateIndicator(show: false, animate);
                LayoutEffect.SetStroke(m_root, Colors.GetColor(ColorName.color_border_default));
                m_titleLabel.TextColor = Colors.GetColor(ColorName.color_text_default);
                m_titleLabel.Style = Styles.GetLabelStyle(LabelStyle.Body300);
                break;

            case StepFlowItemState.Active:
                StopCompletionAnimation();
                AnimateIndicator(show: false, animate);
                LayoutEffect.SetStroke(m_root, Colors.GetColor(ColorName.color_text_default));
                m_titleLabel.TextColor = Colors.GetColor(ColorName.color_text_default);
                m_titleLabel.Style = Styles.GetLabelStyle(LabelStyle.UI300);
                if (animate)
                {
                    _ = AnimateLiftAsync();
                    ExpandAsync();
                }
                else
                {
                    Opacity = 1;
                    Scale = 1;
                    m_bodyContainer.IsVisible = true;
                    m_bodyContainer.HeightRequest = -1;
                    m_bodyContainer.Opacity = 1;
                    m_bodyContainer.TranslationY = 0;
                }
                break;

            case StepFlowItemState.Completed:
                LayoutEffect.SetStroke(m_root, Colors.GetColor(ColorName.color_border_default));
                m_titleLabel.TextColor = Colors.GetColor(ColorName.color_text_default);
                m_titleLabel.Style = Styles.GetLabelStyle(LabelStyle.Body300);
                if (animate)
                {
                    _ = CollapseAsync();
                    _ = AnimateCompletionAsync();
                    new Animation(v => Opacity = v, Opacity, 1)
                        .Commit(this, m_animationToken + "-opacity", rate: 16, length: CompletionDimDurationMs, easing: Easing.CubicOut);
                }
                else
                {
                    Opacity = 1;
                    m_bodyContainer.IsVisible = false;
                    m_bodyContainer.HeightRequest = 0;
                    m_bodyContainer.TranslationY = 0;
                    AnimateIndicator(show: true, animate: false);
                    // Snap the Lottie to its final frame without animating.
                    m_completionAnimation.IsAnimationEnabled = false;
                    m_completionAnimation.Progress = TimeSpan.FromMilliseconds(int.MaxValue);
                }
                break;

            case StepFlowItemState.Error:
                StopCompletionAnimation();
                AnimateIndicator(show: false, animate);
                LayoutEffect.SetStroke(m_root, Colors.GetColor(ColorName.color_text_danger));
                m_titleLabel.TextColor = Colors.GetColor(ColorName.color_text_danger);
                m_titleLabel.Style = Styles.GetLabelStyle(LabelStyle.Body300);
                if (animate)
                {
                    new Animation(v => Opacity = v, Opacity, 1).Commit(this, m_animationToken + "-opacity",
                        rate: 16, length: 240, easing: Easing.CubicOut);
                }
                else
                {
                    Opacity = 1;
                }
                break;
        }
    }

    private void ExpandAsync()
    {
        if (Content is null)
        {
            m_bodyContainer.IsVisible = true;
            return;
        }

        // Measure the body at its natural size (without committing it to layout) so we can
        // animate HeightRequest from 0 → measured height. Using Measure() avoids the
        // dispatcher round-trip + visible "flash at full height, snap to 0" that the
        // previous implementation relied on.
        m_bodyContainer.IsVisible = true;
        m_bodyContainer.HeightRequest = -1;

        var available = m_root.Width > 0 ? m_root.Width - m_root.Padding.HorizontalThickness : double.PositiveInfinity;
        var measured = m_bodyContainer.Measure(available, double.PositiveInfinity);
        // Measure() returns DesiredSize which includes the View's own Margin. HeightRequest
        // sets the inner size (excluding margin), so we must subtract it or the animation
        // overshoots the natural height and snaps back when layout settles.
        var targetHeight = measured.Height - m_bodyContainer.Margin.VerticalThickness;

        if (double.IsNaN(targetHeight) || targetHeight <= 0)
        {
            // Fallback: just show without height animation.
            m_bodyContainer.HeightRequest = -1;
            return;
        }

        m_bodyContainer.HeightRequest = 0;
        m_bodyContainer.Opacity = 0;
        var contentOffset = Sizes.GetSize(SizeName.size_1);
        m_bodyContainer.TranslationY = -contentOffset;

        // Height drives the slot growth while opacity and a tiny Y offset soften the reveal.
        // Both share one parent Animation so they're kicked from a single tick handle.
        var heightAnim = new Animation(v => m_bodyContainer.HeightRequest = v, 0, targetHeight, easing: StepFlowEasings.ExpandSmooth);
        var fadeAnim = new Animation(v => m_bodyContainer.Opacity = v, 0, 1, easing: Easing.CubicOut);
        var offsetAnim = new Animation(v => m_bodyContainer.TranslationY = v, -contentOffset, 0, easing: Easing.CubicOut);

        var parent = new Animation();
        parent.Add(0, 1, heightAnim);
        parent.Add(0.12, 1, fadeAnim);
        parent.Add(0, 1, offsetAnim);

        this.AbortAnimation(m_animationToken + "-body");
        parent.Commit(this, m_animationToken + "-body", rate: 16, length: ExpandDurationMs,
            easing: Easing.Linear, finished: (_, _) =>
            {
                if (State != StepFlowItemState.Active || Handler is null)
                    return;
                // Hand the slot back to auto-sizing so future content changes (async loads,
                // text wraps) just work without a re-measure dance.
                m_bodyContainer.HeightRequest = -1;
                m_bodyContainer.Opacity = 1;
                m_bodyContainer.TranslationY = 0;
            });
    }

    private Task CollapseAsync()
    {
        if (!m_bodyContainer.IsVisible)
            return Task.CompletedTask;

        var current = m_bodyContainer.Height > 0 ? m_bodyContainer.Height : (double)m_bodyContainer.HeightRequest;
        if (current <= 0)
            current = 1;

        m_bodyContainer.HeightRequest = current;
        var contentOffset = Sizes.GetSize(SizeName.size_1);

        var heightAnim = new Animation(v => m_bodyContainer.HeightRequest = v, current, 0, easing: StepFlowEasings.CollapseSmooth);
        var fadeAnim = new Animation(v => m_bodyContainer.Opacity = v, m_bodyContainer.Opacity, 0, easing: Easing.CubicOut);
        var offsetAnim = new Animation(v => m_bodyContainer.TranslationY = v, m_bodyContainer.TranslationY, -contentOffset, easing: Easing.CubicOut);

        var parent = new Animation();
        parent.Add(0, 1, heightAnim);
        parent.Add(0, 0.82, fadeAnim);
        parent.Add(0, 0.9, offsetAnim);

        this.AbortAnimation(m_animationToken + "-body");
        parent.Commit(this, m_animationToken + "-body", rate: 16, length: CollapseDurationMs,
            easing: Easing.Linear, finished: (_, _) =>
            {
                m_bodyContainer.IsVisible = false;
                m_bodyContainer.TranslationY = 0;
            });

        return Task.CompletedTask;
    }

    private Task AnimateLiftAsync()
    {
        Scale = 0.985;
        var scaleAnim = new Animation(v => Scale = v, 0.985, 1.0, easing: Easing.CubicOut);
        var fadeAnim = new Animation(v => Opacity = v, Opacity, 1, easing: StepFlowEasings.FadeOutQuart);
        var parent = new Animation();
        parent.Add(0, 1, scaleAnim);
        parent.Add(0, 1, fadeAnim);
        this.AbortAnimation(m_animationToken + "-lift");
        parent.Commit(this, m_animationToken + "-lift", rate: 16, length: LiftDurationMs,
            finished: (_, _) =>
            {
                Scale = 1;
                Opacity = 1;
            });
        return Task.CompletedTask;
    }

    private Task AnimateCompletionAsync()
    {
        // Hand the indicator over to the Lottie checkmark animation: slide title right and
        // crossfade the Lottie in.
        AnimateIndicator(show: true, animate: true);
        // Reset and play from the start every time we transition into Completed.
        m_completionAnimation.IsAnimationEnabled = false;
        m_completionAnimation.Progress = TimeSpan.Zero;
        m_completionAnimation.IsAnimationEnabled = true;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Indents the title to make room for the Lottie checkmark, and crossfades the Lottie
    /// itself. Uses Margin instead of a transform so long Android titles are measured and
    /// wrapped at the width they visually occupy.
    /// </summary>
    private void AnimateIndicator(bool show, bool animate)
    {
        var marginToken = m_animationToken + "-indicator-margin";
        var fadeToken = m_animationToken + "-indicator-fade";
        this.AbortAnimation(marginToken);
        this.AbortAnimation(fadeToken);

        var targetMargin = show ? m_indicatorSlotWidth : 0;
        var targetOpacity = show ? 1d : 0d;

        if (!animate)
        {
            SetTitleStartMargin(targetMargin);
            m_completionAnimation.Opacity = targetOpacity;
            m_completionAnimation.IsVisible = show;
            return;
        }

        if (show)
            m_completionAnimation.IsVisible = true;

        var startMargin = m_titleStack.Margin.Left;
        new Animation(SetTitleStartMargin, startMargin, targetMargin)
            .Commit(this, marginToken, rate: 16, length: IndicatorShiftDurationMs, easing: Easing.CubicInOut);

        var startOpacity = m_completionAnimation.Opacity;
        new Animation(v => m_completionAnimation.Opacity = v, startOpacity, targetOpacity)
            .Commit(this, fadeToken, rate: 16, length: IndicatorShiftDurationMs, easing: Easing.CubicInOut,
                finished: (_, __) => { if (!show) m_completionAnimation.IsVisible = false; });
    }

    private void SetTitleStartMargin(double startMargin)
    {
        var currentMargin = m_titleStack.Margin;
        m_titleStack.Margin = new Thickness(startMargin, currentMargin.Top, currentMargin.Right, currentMargin.Bottom);
    }

    private void StopCompletionAnimation()
    {
        m_completionAnimation.IsAnimationEnabled = false;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        if (args.NewHandler is null)
        {
            // Abort any in-flight animations and detach gestures to avoid retaining handlers.
            this.AbortAnimation(m_animationToken + "-body");
            this.AbortAnimation(m_animationToken + "-opacity");
            this.AbortAnimation(m_animationToken + "-lift");
            this.AbortAnimation(m_animationToken + "-indicator-margin");
            this.AbortAnimation(m_animationToken + "-indicator-fade");

            m_completionAnimation.IsAnimationEnabled = false;

            // Clear the Touch command so the platform effect detaches and releases its
            // gesture recognizer / handler references.
            Touch.SetCommand(m_root, null!);
        }
    }
}
