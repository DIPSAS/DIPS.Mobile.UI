namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public class InputDialogConfigurator : DialogConfigurator, IInputDialogConfigurator, IInputDialog
{
    public IInputDialogConfigurator AddInputField<T>(IDialogInputField<T> dialogInputField)
    {
        ((IInputDialog)this).InputDialogEntryConfigurators.Add(dialogInputField);
        return this;
    }

    List<IDialogInputField> IInputDialog.InputDialogEntryConfigurators { get; } = [];
}
    
public interface IInputDialogConfigurator : IDialogConfigurator
{
    IInputDialogConfigurator AddInputField<T>(IDialogInputField<T> inputField);
}

public interface IDialogInputField
{
    internal Guid Identifier { get; }
    
    /// <summary>
    /// Resets the value of the input field to the value it had when it was created.
    /// </summary>
    void Reset();
}

public interface IDialogInputField<T> : IDialogInputField
{
    T Value { get; }
    bool MustBeSet { get; set; }
}

public class StringDialogInputField : IDialogInputField<string>
{
    private readonly string m_valueOnCreation;

    public StringDialogInputField(string? placeholder = null, string? value = null, bool mustBeSet = false)
    {
        Placeholder = placeholder ?? string.Empty;
        Value = value ?? string.Empty;
        MustBeSet = mustBeSet;
        m_valueOnCreation = Value;
    }
    public string Placeholder { get; set; }
    public string Value { get; set; }
    public bool MustBeSet { get; set; }

    public void Reset()
    {
        Value = m_valueOnCreation;
    }

    public Guid Identifier { get; } = Guid.NewGuid();
}

internal interface IInputDialog
{
    List<IDialogInputField> InputDialogEntryConfigurators { get; }
}