using DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;
using DIPS.Mobile.UI.Components.Slidable;
using DIPS.Mobile.UI.MVVM;

namespace Playground.EirikSamples;

public class EirikPageViewModel : ViewModel
{
    private readonly List<SelectableDateViewModel> m_dateViewModels =
    [
        new SelectableDateViewModel(DateTime.Now - new TimeSpan(4, 0, 0, 0)),
        new SelectableDateViewModel(DateTime.Now - new TimeSpan(3, 0, 0, 0)),
        new SelectableDateViewModel(DateTime.Now - new TimeSpan(2, 0, 0, 0)),
        new SelectableDateViewModel(DateTime.Now - new TimeSpan(1, 0, 0, 0))
    ];

    private SliderConfig m_config = new(-3, 0);
    private SlidableProperties m_properties = new(0);

    public Func<int, SelectableDateViewModel> DateViewModelFactory => i => m_dateViewModels[Math.Abs(i)];

    public SliderConfig Config
    {
        get => m_config;
        set => RaiseWhenSet(ref m_config, value);
    }

    public SlidableProperties Properties
    {
        get => m_properties;
        set => RaiseWhenSet(ref m_properties, value);
    }
}