using System.Windows.Input;

namespace DIPS.Mobile.UI.API.Tip;

[ContentProperty(nameof(Message))]
public class TipCommand : IMarkupExtension
{
    public string Message { get; set; } = string.Empty;
    public int Duration { get; set; } = 4000;
    
    public object ProvideValue(IServiceProvider serviceProvider)
    {
        var valueProvider = serviceProvider.GetService<IProvideValueTarget>() ?? throw new ArgumentException();
        if (valueProvider.TargetObject is not View view)
        {
            return new Command(() => { });
        }

        return new Command(() =>
        {
            TipService.Show(Message, view, Duration);
        });

    }
}