using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.API.Tip;

public static partial class TipService 
{
    public static async partial void Show(string message, View anchoredView)
    {
        if (anchoredView.Handler is not ViewHandler viewHandler) return;
        if (viewHandler.PlatformView is null) return;

        var tipUiViewController = new TipUIViewController(message);
        tipUiViewController.ModalPresentationStyle = UIModalPresentationStyle.Popover;

        if (tipUiViewController.PopoverPresentationController is not null)
        {
            tipUiViewController.PopoverPresentationController.SourceRect = viewHandler.PlatformView.Bounds;
            tipUiViewController.PopoverPresentationController.SourceView = viewHandler.PlatformView;
            tipUiViewController.PopoverPresentationController.Delegate = new TipUIPopoverPresentationControllerDelegate();
        }

        await UIApplication.SharedApplication.KeyWindow.RootViewController?.PresentViewControllerAsync(tipUiViewController,
            true);
    }
}

internal class TipUIViewController : UIViewController
{
    private readonly string m_message;

    public TipUIViewController(string message)
    {
        m_message = message;
    }
    
    public override void ViewDidLoad()
    {
        View = new UILabel() {Text = m_message};
        base.ViewDidLoad();
    }
}

internal class TipUIPopoverPresentationControllerDelegate : UIPopoverPresentationControllerDelegate
{
    public override UIModalPresentationStyle GetAdaptivePresentationStyle(UIPresentationController controller,
        UITraitCollection traitCollection)
    {
        return UIModalPresentationStyle.None;
    }
}
