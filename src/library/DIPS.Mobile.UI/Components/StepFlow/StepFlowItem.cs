using DIPS.Mobile.UI.Effects.Layout;
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
    private const double DisabledOpacity = 0.45;
    private const double CompletedOpacity = 0.78;
    private const double HeaderPressScale = 0.97;

    private const uint ExpandDurationMs = 420;
    private const uint CollapseDurationMs = 320;
    private const uint LiftDurationMs = 380;
    private const uint CompletionDimDurationMs = 500;
    private const uint IndicatorShiftDurationMs = 360;
    private const uint PressDownMs = 90;
    private const uint PressUpMs = 240;

    private readonly double m_indicatorSlotWidth;

    private readonly Grid m_root = new();
    private readonly Grid m_headerGrid = new();
    private readonly ContentView m_indicatorHost = new();
    private readonly Label m_titleLabel = new();
    private readonly Label m_subtitleLabel = new();
    private readonly VerticalStackLayout m_titleStack = new();
    private readonly ContentView m_bodyContainer = new();
    private readonly SKLottieView m_completionAnimation;

    private string m_animationToken;

    /// <summary>The 1-based number rendered in the default indicator.</summary>
    internal int DisplayNumber { get; set; } = 1;

    /// <summary>Internal index in the parent <see cref="StepFlow"/>.</summary>
    internal int Index { get; set; } = -1;

    /// <summary>Raised when the user taps the header. The container decides whether the tap should activate the step.</summary>
    internal event EventHandler? HeaderTapped;

    public StepFlowItem()
    {
        m_animationToken = "stepflow-item-" + Guid.NewGuid().ToString("N");
        m_indicatorSlotWidth = Sizes.GetSize(SizeName.size_6) + Sizes.GetSize(SizeName.size_3);

        BackgroundColor = GraphicsColors.Transparent;
        Padding = 0;

        m_completionAnimation = new SKLottieView
        {
            Source = Animations.GetAnimation(AnimationName.saved),
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

        // Indicator slot — overlays the title at the start of the header. The title slides
        // right (via Margin animation) to make room when the Lottie checkmark appears on
        // completion, so non-completed steps render their title flush left.
        m_indicatorHost.HorizontalOptions = LayoutOptions.Start;
        m_indicatorHost.VerticalOptions = LayoutOptions.Center;
        m_indicatorHost.Content = m_completionAnimation;

        // ---- Title / subtitle stack ----
        m_titleLabel.Style = Styles.GetLabelStyle(LabelStyle.UI300);
        m_titleLabel.LineBreakMode = LineBreakMode.TailTruncation;
        m_titleLabel.VerticalOptions = LayoutOptions.Center;

        m_subtitleLabel.Style = Styles.GetLabelStyle(LabelStyle.UI100);
        m_subtitleLabel.TextColor = Colors.GetColor(ColorName.color_text_subtle);
        m_subtitleLabel.IsVisible = false;
        m_subtitleLabel.LineBreakMode = LineBreakMode.TailTruncation;

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

        var headerTap = new TapGestureRecognizer();
        headerTap.Tapped += OnHeaderTapped;
        m_headerGrid.GestureRecognizers.Add(headerTap);

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
    }

    private void OnHeaderTapped(object? sender, TappedEventArgs e)
    {
        if (!IsEnabled) return;
        if (State == StepFlowItemState.Completed && LockWhenCompleted) return;
        if (State == StepFlowItemState.Disabled && Parent is StepFlow flow && !flow.AllowDirectStepActivation) return;

        AnimatePressFeedback();
        HeaderTapped?.Invoke(this, EventArgs.Empty);
    }

    private void OnStateChanged(StepFlowItemState oldState, StepFlowItemState newState)
    {
        if (oldState == newState) return;
        ApplyStateVisuals(newState, animate: true);
    }

    /// <summary>
    /// Applies the visuals for <paramref name="state"/>. When <paramref name="animate"/> is
    /// <c>true</c>, runs the choreographed expand/collapse/completion/lift animations. When
    /// <c>false</c> (initial setup), applies the visuals instantly.
    /// </summary>
    internal void ApplyStateVisuals(StepFlowItemState state, bool animate)
    {
        switch (state)
        {
            case StepFlowItemState.Disabled:
                if (animate)
                {
                    _ = CollapseAsync();
                    this.AbortAnimation(m_animationToken + "-opacity");
                    new Animation(v => Opacity = v, Opacity, DisabledOpacity)
                        .Commit(this, m_animationToken + "-opacity", rate: 16, length: CompletionDimDurationMs, easing: Easing.CubicOut);
                }
                else
                {
                    Opacity = DisabledOpacity;
                    m_bodyContainer.IsVisible = false;
                    m_bodyContainer.HeightRequest = 0;
                }
                StopCompletionAnimation();
                AnimateIndicator(show: false, animate);
                LayoutEffect.SetStroke(m_root, Colors.GetColor(ColorName.color_border_default));
                m_titleLabel.TextColor = Colors.GetColor(ColorName.color_text_default);
                break;

            case StepFlowItemState.Active:
                StopCompletionAnimation();
                AnimateIndicator(show: false, animate);
                LayoutEffect.SetStroke(m_root, Colors.GetColor(ColorName.color_text_default));
                m_titleLabel.TextColor = Colors.GetColor(ColorName.color_text_default);
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
                }
                break;

            case StepFlowItemState.Completed:
                if (animate)
                {
                    _ = CollapseAsync();
                    _ = AnimateCompletionAsync();
                    new Animation(v => Opacity = v, Opacity, CompletedOpacity)
                        .Commit(this, m_animationToken + "-opacity", rate: 16, length: CompletionDimDurationMs, easing: Easing.CubicOut);
                }
                else
                {
                    Opacity = CompletedOpacity;
                    m_bodyContainer.IsVisible = false;
                    m_bodyContainer.HeightRequest = 0;
                    LayoutEffect.SetStroke(m_root, Colors.GetColor(ColorName.color_border_default));
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

        // Height drives the slot growth; opacity fades the content in over the same curve.
        // Both share one parent Animation so they're kicked from a single tick handle.
        var heightAnim = new Animation(v => m_bodyContainer.HeightRequest = v, 0, targetHeight, easing: Easing.CubicOut);
        var fadeAnim = new Animation(v => m_bodyContainer.Opacity = v, 0, 1, easing: Easing.CubicOut);

        var parent = new Animation();
        parent.Add(0, 1, heightAnim);
        parent.Add(0, 1, fadeAnim);

        this.AbortAnimation(m_animationToken + "-body");
        parent.Commit(this, m_animationToken + "-body", rate: 16, length: ExpandDurationMs,
            easing: Easing.Linear, finished: (_, _) =>
            {
                if (State != StepFlowItemState.Active || Handler is null) return;
                // Hand the slot back to auto-sizing so future content changes (async loads,
                // text wraps) just work without a re-measure dance.
                m_bodyContainer.HeightRequest = -1;
                m_bodyContainer.Opacity = 1;
            });
    }

    private Task CollapseAsync()
    {
        if (!m_bodyContainer.IsVisible) return Task.CompletedTask;

        var current = m_bodyContainer.Height > 0 ? m_bodyContainer.Height : (double)m_bodyContainer.HeightRequest;
        if (current <= 0) current = 1;

        m_bodyContainer.HeightRequest = current;

        var heightAnim = new Animation(v => m_bodyContainer.HeightRequest = v, current, 0, easing: StepFlowEasings.CollapseCubic);
        var fadeAnim = new Animation(v => m_bodyContainer.Opacity = v, m_bodyContainer.Opacity, 0, easing: StepFlowEasings.CollapseCubic);

        var parent = new Animation();
        parent.Add(0, 1, heightAnim);
        parent.Add(0, 0.85, fadeAnim);

        this.AbortAnimation(m_animationToken + "-body");
        parent.Commit(this, m_animationToken + "-body", rate: 16, length: CollapseDurationMs,
            easing: Easing.Linear, finished: (_, _) =>
            {
                m_bodyContainer.IsVisible = false;
            });

        return Task.CompletedTask;
    }

    private Task AnimateLiftAsync()
    {
        Scale = 0.97;
        var scaleAnim = new Animation(v => Scale = v, 0.97, 1.0, easing: StepFlowEasings.SpringOut);
        var fadeAnim = new Animation(v => Opacity = v, Opacity, 1, easing: Easing.CubicOut);
        var parent = new Animation();
        parent.Add(0, 1, scaleAnim);
        parent.Add(0, 1, fadeAnim);
        this.AbortAnimation(m_animationToken + "-lift");
        parent.Commit(this, m_animationToken + "-lift", rate: 16, length: LiftDurationMs);
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
    /// Slides the title left/right to make room for the Lottie checkmark, and crossfades the
    /// Lottie itself. Uses TranslationX (transform, no layout cost) for the title so the
    /// header is never re-measured during the shift.
    /// </summary>
    private void AnimateIndicator(bool show, bool animate)
    {
        var translateToken = m_animationToken + "-indicator-translate";
        var fadeToken = m_animationToken + "-indicator-fade";
        this.AbortAnimation(translateToken);
        this.AbortAnimation(fadeToken);

        var targetX = show ? m_indicatorSlotWidth : 0;
        var targetOpacity = show ? 1d : 0d;

        if (!animate)
        {
            m_titleStack.TranslationX = targetX;
            m_completionAnimation.Opacity = targetOpacity;
            m_completionAnimation.IsVisible = show;
            return;
        }

        if (show) m_completionAnimation.IsVisible = true;

        var startX = m_titleStack.TranslationX;
        new Animation(v => m_titleStack.TranslationX = v, startX, targetX)
            .Commit(this, translateToken, rate: 16, length: IndicatorShiftDurationMs, easing: Easing.CubicInOut);

        var startOpacity = m_completionAnimation.Opacity;
        new Animation(v => m_completionAnimation.Opacity = v, startOpacity, targetOpacity)
            .Commit(this, fadeToken, rate: 16, length: IndicatorShiftDurationMs, easing: Easing.CubicInOut,
                finished: (_, __) => { if (!show) m_completionAnimation.IsVisible = false; });
    }

    private void StopCompletionAnimation()
    {
        m_completionAnimation.IsAnimationEnabled = false;
    }

    private void AnimatePressFeedback()
    {
        var down = new Animation(v => m_headerGrid.Scale = v, 1.0, HeaderPressScale);
        this.AbortAnimation(m_animationToken + "-press");
        down.Commit(m_headerGrid, m_animationToken + "-press", rate: 16, length: PressDownMs,
            easing: Easing.CubicOut, finished: (_, _) =>
            {
                var up = new Animation(v => m_headerGrid.Scale = v, HeaderPressScale, 1.0, easing: StepFlowEasings.SpringOut);
                up.Commit(m_headerGrid, m_animationToken + "-press-up", rate: 16, length: PressUpMs);
            });
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
            this.AbortAnimation(m_animationToken + "-press");
            this.AbortAnimation(m_animationToken + "-press-up");

            m_completionAnimation.IsAnimationEnabled = false;

            foreach (var gr in m_headerGrid.GestureRecognizers.OfType<TapGestureRecognizer>().ToList())
            {
                gr.Tapped -= OnHeaderTapped;
            }
            m_headerGrid.GestureRecognizers.Clear();
        }
    }
}
