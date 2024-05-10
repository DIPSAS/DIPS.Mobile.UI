using System.Runtime.InteropServices.ComTypes;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Pickers;

public class DateTimePickerSamplesViewModel : ViewModel
{
    private DateTime m_selectedBirthday;
    private DateTime m_selectedDeadline;
    private TimeSpan m_selectedShoppingTime;
    private DateTime m_maximumDate;
    private DateTime m_minimumDate;
    private DateTime m_test;
    private DateTime m_test2;
    private DateTime m_test3;
    private DateTime? m_dateTimeNullDefault;
    private TimeSpan? m_timeSpanNullDefault;
    private DateTime? m_dateAndTimeNullDefault;
    private DateTime? m_dateTimeNullNotDefault = new DateTime(2024, 5, 7, 0, 0, 0);

    public DateTimePickerSamplesViewModel()
    {
        SelectedBirthday = new DateTime(1989, 01, 28);
        SelectedDeadline = new DateTime(2023, 05, 10, 20, 10, 0);
        SelectedShoppingTime = new TimeSpan(16, 23, 0);
        MaximumDate = new DateTime(2023, 05, 16);
        MinimumDate = new DateTime(2023, 01, 01);

        Test = new DateTime(2023, 01, 28);
        Test2 = new DateTime(2024, 01, 29);
        Test3 = new DateTime(2023, 10, 29);
    }

    public DateTime SelectedBirthday
    {
        get => m_selectedBirthday;
        set => RaiseWhenSet(ref m_selectedBirthday, value);
    }

    public Func<int, object> CreateMyObjectsBasedOnPosition => i => $"Number {i}";

    public DateTime SelectedDeadline
    {
        get => m_selectedDeadline;
        set => RaiseWhenSet(ref m_selectedDeadline, value);
    }

    public DateTime Test
    {
        get => m_test;
        set => RaiseWhenSet(ref m_test, value);
    }

    public DateTime Test2
    {
        get => m_test2;
        set => RaiseWhenSet(ref m_test2, value);
    }

    public DateTime Test3
    {
        get => m_test3;
        set => RaiseWhenSet(ref m_test3, value);
    }

    public TimeSpan SelectedShoppingTime
    {
        get => m_selectedShoppingTime;
        set => RaiseWhenSet(ref m_selectedShoppingTime, value);
    }

    public DateTime MaximumDate
    {
        get => m_maximumDate;
        set => RaiseWhenSet(ref m_maximumDate, value);
    }

    public DateTime MinimumDate
    {
        get => m_minimumDate;
        set => RaiseWhenSet(ref m_minimumDate, value);
    }

    public ICommand SelectedDateCommand => new Command(async () =>
    {
        
    });

    public ICommand SelectedTimeCommand => new Command(() =>
    {
        var test = TimeSpanNullDefault;
        Console.WriteLine(test);
    });

    public ICommand SetToNull => new Command(() =>
    {
        DateTimeNullDefault = null;
    });

    public DateTime? DateTimeNullDefault
    {
        get => m_dateTimeNullDefault;
        set => RaiseWhenSet(ref m_dateTimeNullDefault, value);
    }

    public DateTime? DateTimeNullNotDefault
    {
        get => m_dateTimeNullNotDefault;
        set => RaiseWhenSet(ref m_dateTimeNullNotDefault, value);
    }

    public DateTime? DateAndTimeNullDefault
    {
        get => m_dateAndTimeNullDefault;
        set => RaiseWhenSet(ref m_dateAndTimeNullDefault, value);
    }

    public TimeSpan? TimeSpanNullDefault
    {
        get => m_timeSpanNullDefault;
        set => RaiseWhenSet(ref m_timeSpanNullDefault, value);
    }
}