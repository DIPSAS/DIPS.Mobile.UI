namespace DIPS.Mobile.UI.Extensions.Markup;

[ContentProperty(nameof(Ticks))]
[AcceptEmptyServiceProvider]
public class TimeSpanExtension : IMarkupExtension<TimeSpan>
{
    public long Ticks { get; set; }

    public TimeSpan ProvideValue(IServiceProvider serviceProvider)
    {
        return new TimeSpan(Ticks);
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return this.ProvideValue(serviceProvider);
    }
}