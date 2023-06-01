using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Slideable;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.SlideLayoutSamples
{
    public class SlideLayoutViewModel : ViewModel
    {
        private SlidableProperties m_slideableProperties;
        private string m_selected;
        private int m_panStartedIndex;
        private int m_panEndedIndex;


        public SlideLayoutViewModel()
        {
            OnSelectedIndexChangedCommand = new Command(o => Selected = o.ToString());
        }

        public async void Initialize()
        {
            await Task.Delay(1);
            SlidableProperties = new SlidableProperties(1);
            await Task.Delay(1000);
            SlidableProperties = new SlidableProperties(-3);
        }

        public SliderConfig Config => new SliderConfig(-10, 0);

        public ICommand OnSelectedIndexChangedCommand { get; }
        public string Selected { get => m_selected; set => RaiseWhenSet(ref m_selected, value); }
        public Func<int, object> CreateCalendar => i => new CalendarViewModel(DateTime.Now.AddDays(i));

        public SlidableProperties SlidableProperties
        {
            get => m_slideableProperties;
            set => RaiseWhenSet(ref m_slideableProperties, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int PanStartedIndex
        {
            get => m_panStartedIndex;
            set => RaiseWhenSet(ref m_panStartedIndex, value);
        }

        public int PanEndedIndex
        {
            get => m_panEndedIndex;
            set => RaiseWhenSet(ref m_panEndedIndex, value);
        }
    }

    public class CalendarViewModel : ViewModel, ISliderSelectable
    {
        private bool selected;

        public CalendarViewModel(DateTime time)
        {
            Value = time.Day + "." + time.Month;
        }

        public string Value { get; }
        public bool Selected { get => selected; set => RaiseWhenSet(ref selected, value); }

        public void OnSelectionChanged(bool selected)
        {
            Selected = selected;
        }
    }
}