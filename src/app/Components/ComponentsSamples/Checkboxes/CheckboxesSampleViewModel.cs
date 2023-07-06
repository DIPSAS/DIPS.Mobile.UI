using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Checkboxes;

public class CheckboxesSampleViewModel : ViewModel
{
    private bool m_isChecked;
    private bool m_isProgressing;

    public CheckboxesSampleViewModel()
    {
        SaveCommand = new Command(async () =>
        {
            if (IsChecked)
            {
                IsChecked = false;
                return;
            }
            
            IsProgressing = true;

            await Task.Delay(1500);

            IsProgressing = false;
            IsChecked = true;
        });
    }
    
    public ICommand SaveCommand { get; }
    
    public bool IsProgressing
    {
        get => m_isProgressing;
        set => RaiseWhenSet(ref m_isProgressing, value);
    }

    public bool IsChecked
    {
        get => m_isChecked; 
        set => RaiseWhenSet(ref m_isChecked, value);
    }
}