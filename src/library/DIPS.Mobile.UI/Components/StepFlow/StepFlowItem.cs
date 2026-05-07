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

    private const double ExpandStartTranslationY = -12;

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
        m_bodyContainer.HeightRequest = 0;
        m_bodyContainer.IsVisible = false;
        m_bodyContainer.Opacity = 0;
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
                    m_bodyContainer.Opacity = 0;
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
                    _ = ExpandAsync();
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
                    m_bodyContainer.Opacity = 0;
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

    private async Task ExpandAsync()
    {
        if (Content is null)
        {
            m_bodyContainer.IsVisible = true;
            return;
        }

        // Make the body part of the layout pass at its natural height while invisible, so we
        // can read the actually-laid-out Height. A manual Measure() call here gives a
        // different result than the real layout pass on Android (it doesn't account for the
        // outer Border stroke and other parent constraints), which causes the visible
        // "snap" when we later switch HeightRequest back to auto.
        m_bodyContainer.IsVisible = true;
        m_bodyContainer.HeightRequest = -1;
        m_bodyContainer.Opacity = 0;
        m_bodyContainer.TranslationY = ExpandStartTranslationY;

        var dispatcher = Dispatcher ?? Application.Current?.Dispatcher;
        if (dispatcher is not null)
        {
            // Yield one dispatcher turn so MAUI completes the layout pass before we read Height.
            await dispatcher.DispatchAsync(() => { });
        }

        if (Handler is null) return;

        var targetHeight = m_bodyContainer.Height;
        if (double.IsNaN(targetHeight) || targetHeight <= 0)
        {
            // Fallback: just show without height animation.
            m_bodyContainer.HeightRequest = -1;
            m_bodyContainer.Opacity = 1;
            m_bodyContainer.TranslationY = 0;
            return;
        }

        // Pin the height to the laid-out value, then animate from 0 → that exact value.
        // Keeping HeightRequest pinned (instead of switching back to -1) eliminates the
        // measure-vs-layout discrepancy that caused the snap. If the body's natural size
        // later grows (e.g. async content), SizeChanged re-pins it.
        m_bodyContainer.HeightRequest = 0;

        // Drive height, fade and translate from a single ease-out curve so the content
        // tracks the container's growth instead of bouncing independently. Fade starts
        // immediately so the body is never visible as an empty box mid-expand.
        var heightAnim = new Animation(v => m_bodyContainer.HeightRequest = v, 0, targetHeight, easing: Easing.CubicOut);
        var fadeAnim = new Animation(v => m_bodyContainer.Opacity = v, 0, 1, easing: Easing.CubicOut);
        var translateAnim = new Animation(v => m_bodyContainer.TranslationY = v, ExpandStartTranslationY, 0, easing: Easing.CubicOut);

        var parent = new Animation();
        parent.Add(0, 1, heightAnim);
        parent.Add(0, 1, fadeAnim);
        parent.Add(0, 1, translateAnim);

        this.AbortAnimation(m_animationToken + "-body");
        parent.Commit(this, m_animationToken + "-body", rate: 16, length: ExpandDurationMs,
            easing: Easing.Linear, finished: (_, _) =>
            {
                m_bodyContainer.HeightRequest = targetHeight;
                m_bodyContainer.SizeChanged -= OnExpandedBodySizeChanged;
                m_bodyContainer.SizeChanged += OnExpandedBodySizeChanged;
            });
    }

    private void OnExpandedBodySizeChanged(object? sender, EventArgs e)
    {
        if (State != StepFlowItemState.Active) return;
        // Re-pin the height to the natural content height when it changes (e.g. async content
        // loaded after expand finished). Temporarily detach to avoid recursion while we read.
        m_bodyContainer.SizeChanged -= OnExpandedBodySizeChanged;
        m_bodyContainer.HeightRequest = -1;
        var dispatcher = Dispatcher ?? Application.Current?.Dispatcher;
        if (dispatcher is null)
        {
            m_bodyContainer.SizeChanged += OnExpandedBodySizeChanged;
            return;
        }
        dispatcher.Dispatch(() =>
        {
            if (State != StepFlowItemState.Active || Handler is null) return;
            var h = m_bodyContainer.Height;
            if (h > 0 && !double.IsNaN(h))
            {
                m_bodyContainer.HeightRequest = h;
            }
            m_bodyContainer.SizeChanged += OnExpandedBodySizeChanged;
        });
    }

    private Task CollapseAsync()
    {
        m_bodyContainer.SizeChanged -= OnExpandedBodySizeChanged;
        if (!m_bodyContainer.IsVisible) return Task.CompletedTask;

        var current = m_bodyContainer.Height > 0 ? m_bodyContainer.Height : (double)m_bodyContainer.HeightRequest;
        if (current <= 0) current = 1;

        m_bodyContainer.HeightRequest = current;

        var heightAnim = new Animation(v => m_bodyContainer.HeightRequest = v, current, 0);
        var fadeAnim = new Animation(v => m_bodyContainer.Opacity = v, m_bodyContainer.Opacity, 0);

        var parent = new Animation();
        parent.Add(0, 1, heightAnim);
        parent.Add(0, 0.85, fadeAnim);

        this.AbortAnimation(m_animationToken + "-body");
        parent.Commit(this, m_animationToken + "-body", rate: 8, length: CollapseDurationMs,
            easing: StepFlowEasings.CollapseCubic, finished: (_, _) =>
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
    /// Lottie itself. Uses Margin (layout) for the title and Opacity for the Lottie so taps on
    /// the header still hit the right region.
    /// </summary>
    private void AnimateIndicator(bool show, bool animate)
    {
        var marginToken = m_animationToken + "-indicator-margin";
        var fadeToken = m_animationToken + "-indicator-fade";
        this.AbortAnimation(marginToken);
        this.AbortAnimation(fadeToken);

        var targetLeft = show ? m_indicatorSlotWidth : 0;
        var targetOpacity = show ? 1d : 0d;

        if (!animate)
        {
            m_titleStack.Margin = new Thickness(targetLeft, 0, 0, 0);
            m_completionAnimation.Opacity = targetOpacity;
            m_completionAnimation.IsVisible = show;
            return;
        }

        if (show) m_completionAnimation.IsVisible = true;

        var startLeft = m_titleStack.Margin.Left;
        new Animation(v => m_titleStack.Margin = new Thickness(v, 0, 0, 0), startLeft, targetLeft)
            .Commit(this, marginToken, rate: 16, length: IndicatorShiftDurationMs, easing: Easing.CubicInOut);

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

            m_bodyContainer.SizeChanged -= OnExpandedBodySizeChanged;

            foreach (var gr in m_headerGrid.GestureRecognizers.OfType<TapGestureRecognizer>().ToList())
            {
                gr.Tapped -= OnHeaderTapped;
            }
            m_headerGrid.GestureRecognizers.Clear();
        }
    }
}
