namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public class InputDialogConfigurator : IInputDialogConfigurator, IInputDialog
{
    public IInputDialogConfigurator AddInputField(string? placeholder = null, string? text = null)
    {
            
        return this;
    }

    List<InputDialogEntryConfigurator> IInputDialog.InputDialogEntryConfigurators { get; } = [];
}

public class InputDialogEntryConfigurator
{
    public string? Placeholder { get; set; }
    public string? Text { get; set; }
}
    
public interface IInputDialogConfigurator
{
    IInputDialogConfigurator AddInputField(string? placeholder = null, string? text = null);
}
    
internal interface IInputDialog
{
    List<InputDialogEntryConfigurator> InputDialogEntryConfigurators { get; }
}