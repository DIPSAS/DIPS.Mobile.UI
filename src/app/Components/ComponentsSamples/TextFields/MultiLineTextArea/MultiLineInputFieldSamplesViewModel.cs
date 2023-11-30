using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.TextFields.MultiLineTextArea;

public class MultiLineInputFieldSamplesViewModel : ViewModel
{
    private bool m_isSaving;
    private bool m_isSavingCompleted;
    private bool m_isError;

    public MultiLineInputFieldSamplesViewModel()
    {
        SaveCommand = new Command(async () =>
        {
            IsSavingCompleted = false;
            IsError = false;
            IsSaving = true;
            await Task.Delay(2000);
            IsSaving = false;
            if (IsErrorSwitchEnabled)
            {
                IsError = true;
            }
            else
            {
                IsSavingCompleted = true;
            }
        });
    }

    public bool IsSaving
    {
        get => m_isSaving;
        set => RaiseWhenSet(ref m_isSaving, value);
    }

    public bool IsSavingCompleted
    {
        get => m_isSavingCompleted;
        set => RaiseWhenSet(ref m_isSavingCompleted, value);
    }
    
    public ICommand SaveCommand { get; }

    public bool IsError
    {
        get => m_isError;
        set => RaiseWhenSet(ref m_isError, value);
    }

    public bool IsErrorSwitchEnabled { get; set; }
    
    
}