#if __IOS__
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
#endif

using System.Windows.Input;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.API.Tip;

[ContentProperty(nameof(Message))]
public class TipCommand : IMarkupExtension
{
    public string Message { get; set; } = string.Empty;
    public int Duration { get; set; } = 4000;

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        var valueProvider = serviceProvider.GetService<IProvideValueTarget>() ?? throw new ArgumentException();
        var actionToRun = () => { };
        switch (valueProvider.TargetObject)
        {
            case View view:
                actionToRun = () =>
                {
                    TipService.Show(Message, view, Duration);
                };
                break;
        }

        return new Command(actionToRun);
    }
}