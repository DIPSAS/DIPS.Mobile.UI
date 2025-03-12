using Microsoft.Maui.Platform;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public static partial class DialogService
{
    private static TaskCompletionSource<InputDialogAction>? s_inputTaskCompletionSource;
    private static TaskCompletionSource<DialogAction>? s_taskCompletionSource;
    private static UIWindow Window { get; } = new() { BackgroundColor = Colors.Transparent.ToPlatform() };

    public static partial async Task<InputDialogAction> ShowInputDialog(Action<IInputDialogConfigurator> configurator)
    {
        await Remove();
        
        s_inputTaskCompletionSource = new TaskCompletionSource<InputDialogAction>();
        var inputDialogConfigurator = new InputDialogConfigurator();
        
        configurator.Invoke(inputDialogConfigurator);

        IDialog dialog = inputDialogConfigurator;
        IInputDialog inputDialog = inputDialogConfigurator;

        var uiAlertController = ConstructGeneralAlertController(dialog, () =>
        {
            OnCancelButtonTapped(inputDialogConfigurator, s_inputTaskCompletionSource);
        },
        () =>
        {
            OnActionButtonTapped(inputDialog, s_inputTaskCompletionSource);
        });
        
        foreach (var dialogInputField in inputDialog.InputDialogEntryConfigurators)
        {
            if (dialogInputField is StringDialogInputField stringInputField)
            {
                uiAlertController.AddTextField(e =>
                {
                    e.RestorationIdentifier = stringInputField.Identifier.ToString();
                    e.Placeholder = stringInputField.Placeholder;
                    e.Text = stringInputField.Value;
                    e.EditingChanged += delegate
                    {
                        stringInputField.Value = e.Text;
                        
                        SetActionButtonEnabled();
                    };
                });
            }
        }
        
        void SetActionButtonEnabled()
        {
            var actionButton = uiAlertController.Actions.FirstOrDefault(action => action.Title == dialog.ActionTitle);
            if (actionButton is not null)
            {
                actionButton.Enabled = ActionEnabledState(inputDialog);
            }
        }

        SetActionButtonEnabled();
        
        Window.RootViewController?.PresentViewController(uiAlertController, true, () => { });
        
        return await s_inputTaskCompletionSource.Task;
    }

    public static partial async Task<DialogAction> ShowMessage(Action<IDialogConfigurator> configurator)
    {
        var dialogConfigurator = new DialogConfigurator();
        configurator.Invoke(dialogConfigurator);
        
        IDialog dialog = dialogConfigurator;
        
        await Remove();
        
        s_taskCompletionSource = new TaskCompletionSource<DialogAction>();
        var uiAlertController = ConstructGeneralAlertController(dialog, () =>
            {
                s_taskCompletionSource.SetResult(DialogAction.Closed);
            },
            () =>
            {
                s_taskCompletionSource.SetResult(DialogAction.TappedAction);
            }, dialog.IsDestructive);
       
        Window.RootViewController?.PresentViewController(uiAlertController, true, () => { });

        return await s_taskCompletionSource.Task;
    }
    
    public async static partial Task Remove()
    {
        if (TryGetUiAlertViewController(out var viewController))
        {
            await viewController?.DismissViewControllerAsync(false)!;
            Window.Hidden = true;
            s_taskCompletionSource?.TrySetResult(DialogAction.Closed);  
            s_inputTaskCompletionSource?.TrySetResult(new InputDialogAction
            {
                DialogAction = DialogAction.Closed,
                DialogInputs = []
            });
        }
    }

    internal static bool TryGetUiAlertViewController(out UIViewController? viewController)
    {
        viewController = null;
        if (Window.RootViewController?.PresentedViewController is null)
        {
            return false;
        }

        viewController = Window.RootViewController;
        return true;
    }
    
    public static partial bool IsShowing()
    {
        return TryGetUiAlertViewController(out _);
    }

    private static UIAlertController ConstructGeneralAlertController(IDialog dialog, Action onCancelButtonTapped, Action onActionButtonTapped, bool isDestructiveDialog = false)
    {
        var uiAlertController = UIAlertController.Create(dialog.Title, dialog.Description, UIAlertControllerStyle.Alert);
        if (dialog.CancelTitle is not null)
        {
            uiAlertController.AddAction(
                UIAlertAction.Create(
                    dialog.CancelTitle,
                    UIAlertActionStyle.Default,
                    _ =>
                    {
                        Window.Hidden = true;
                        onCancelButtonTapped.Invoke();
                    }));
        }

        uiAlertController.AddAction(
            UIAlertAction.Create(
                dialog.ActionTitle,
                isDestructiveDialog ? UIAlertActionStyle.Destructive : UIAlertActionStyle.Default,
                _ =>
                {
                    Window.Hidden = true;
                    onActionButtonTapped.Invoke();
                }));
        
        Window.RootViewController = new UIViewController();
        
        Window.RootViewController.View!.BackgroundColor = Colors.Transparent.ToPlatform();
        Window.WindowLevel = UIWindowLevel.Alert + 1;
        Window.MakeKeyAndVisible();

        return uiAlertController;
    }
}