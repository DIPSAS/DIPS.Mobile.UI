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
        switch (valueProvider.TargetObject)
        {
            case View view:
                return new Command(() =>
                {
                    TipService.Show(Message, view, Duration);
                });
            case ToolbarItem toolbarItem:
                return new Command(() =>
                {
#if __IOS__
                    if (toolbarItem.Parent is not ContentPage cp) return;
                    if (cp.Handler is not PageHandler pageHandler) return;
                    if (pageHandler.ViewController is null) return;
                    if (pageHandler.ViewController.NavigationController is {ToolbarItems: not null} &&
                        pageHandler.ViewController.NavigationController.ToolbarItems.Any())
                    {
                    }asdasd
#endif
                });
        }

        return new Command(() => { });
    }
}