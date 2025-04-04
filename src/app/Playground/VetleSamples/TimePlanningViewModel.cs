using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class TimePlanningViewModel : ViewModel
{
    private readonly VetlePageViewModel m_vetlePageViewModel;
    private DateTime m_dateTime;

    public TimePlanningViewModel(VetlePageViewModel vetlePageViewModel)
    {
        m_vetlePageViewModel = vetlePageViewModel;
    }
    
    public DateTime DateTime
    {
        get => m_dateTime;
        set
        {
            if(value == m_dateTime) return;
            RaiseWhenSet(ref m_dateTime, value);
            m_vetlePageViewModel.OnDateChanged();
        }
    }

    public List<object> Tests { get; set; } = ["Test", "Test2"];
    public string Test { get; } = "Test";

    public void Update()
    {
        m_vetlePageViewModel.OnDateChanged();
    }
}