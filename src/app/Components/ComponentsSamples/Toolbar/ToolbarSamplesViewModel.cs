using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Toolbar;

internal class ToolbarSamplesViewModel : ViewModel
{
    private string m_lastAction = "None";

    public Command EditCommand => new(OnEdit);
    public Command SaveCommand => new(OnSave);
    public Command FilterCommand => new(OnFilter);
    public Command AddCommand => new(OnAdd);
    public Command SelectTasksCommand => new(OnSelectTasks);

    public string LastAction
    {
        get => m_lastAction;
        set => RaiseWhenSet(ref m_lastAction, value);
    }

    private void OnEdit() => LastAction = "Edit tapped";
    private void OnSave() => LastAction = "Save tapped";
    private void OnFilter() => LastAction = "Filter tapped";
    private void OnAdd() => LastAction = "Add tapped";
    private void OnSelectTasks() => LastAction = "Select tasks tapped";
}
