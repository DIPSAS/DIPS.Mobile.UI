namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public class InputDialogConfigurator : IInputDialogConfigurator, IInputDialog
{
    public IInputDialogConfigurator AddInputField<T>(IDialogInputField<T> dialogInputField)
    {
        ((IInputDialog)this).InputDialogEntryConfigurators.Add(dialogInputField);
        return this;
    }

    List<IDialogInputField> IInputDialog.InputDialogEntryConfigurators { get; } = [];
}
    
public interface IInputDialogConfigurator
{
    IInputDialogConfigurator AddInputField<T>(IDialogInputField<T> inputField);
}

public interface IDialogInputField
{
    internal Guid Identifier { get; }
}

public interface IDialogInputField<T> : IDialogInputField
{
    public T Value { get; }
    public bool MustBeSet { get; set; }
}

public class StringDialogInputField : IDialogInputField<string>
{
    public StringDialogInputField(string? placeholder = null, string? value = null, bool mustBeSet = false)
    {
        Placeholder = placeholder ?? string.Empty;
        Value = value ?? string.Empty;
        MustBeSet = mustBeSet;
    }
    public string Placeholder { get; set; }
    public string Value { get; set; }
    public bool MustBeSet { get; set; }
    public Guid Identifier { get; } = Guid.NewGuid();
}

internal interface IInputDialog
{
    List<IDialogInputField> InputDialogEntryConfigurators { get; }
}