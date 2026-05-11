namespace DIPS.Mobile.UI.Components.StepFlow;

/// <summary>
/// Easing functions used throughout <see cref="StepFlow"/>. Defined as
/// static <see cref="Easing"/> instances so the underlying delegates are not allocated
/// per animation invocation.
/// </summary>
internal static class StepFlowEasings
{
    /// <summary>Smooth ease-out for layout expansion.</summary>
    public static readonly Easing ExpandSmooth = new(t =>
    {
        var u = 1 - t;
        return 1 - u * u * u * u;
    });

    /// <summary>
    /// easeOutQuint with a tiny damped-sine "settle" at the end:
    /// <c>1 - (1-t)^5 + sin(t * pi * 2.2) * exp(-t * 5) * 0.04</c>.
    /// Used for expand animations.
    /// </summary>
    public static readonly Easing ExpandSettle = new(t =>
    {
        var ease = 1 - Math.Pow(1 - t, 5);
        var settle = Math.Sin(t * Math.PI * 2.2) * Math.Exp(-t * 5) * 0.04;
        return ease + settle;
    });

    /// <summary>Smooth cubic in/out for collapse animations.</summary>
    public static readonly Easing CollapseSmooth = new(t =>
        t < 0.5
            ? 4 * t * t * t
            : 1 - Math.Pow(-2 * t + 2, 3) / 2);

    /// <summary>
    /// Critically-damped sine spring-out, used for icon settles, press releases and lift animations.
    /// </summary>
    public static readonly Easing SpringOut = new(t =>
    {
        if (t >= 1) return 1;
        return 1 - Math.Cos(t * Math.PI * 0.5) * Math.Exp(-t * 4);
    });

    /// <summary>Smooth ease-out for opacity ramps that pair with the spring-out scale.</summary>
    public static readonly Easing FadeOutQuart = new(t =>
    {
        var u = 1 - t;
        return 1 - u * u * u * u;
    });
}
