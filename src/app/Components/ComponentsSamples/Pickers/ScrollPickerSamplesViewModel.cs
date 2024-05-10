using System.Globalization;
using System.Windows.Input;
using Components.ComponentsSamples.Pickers.ScrollPickerComponents;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;
using DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

namespace Components.ComponentsSamples.Pickers;

public class ScrollPickerSamplesViewModel
{
    private int? m_lastSelectedIndex;

    public ScrollPickerSamplesViewModel()
    {
        
        var englishFootballers = new List<string> { "Foden", "Kane", "Rashford" };
        var norwegianFootballers = new List<string> { "Haaland", "Odegaard", "Sørloth" };
        var englishFootballersComponent = new StandardScrollPickerComponent<string>(englishFootballers, 1);
        var norwegianFootballersComponent = new StandardScrollPickerComponent<string>(norwegianFootballers);
        FootballersComponents = [englishFootballersComponent, norwegianFootballersComponent];

        var yearsScrollPickerItemsSource = new YearScrollPickerComponent();
        var weekScrollPickerItemsSource = new StandardScrollPickerComponent<int>(GetWeeksInYear(DateTime.Now.Year), 10, OnSelectedItemChanged, true);
        var daysScrollPickerItemsSource = new DayScrollPickerComponent();
        DateComponents = [yearsScrollPickerItemsSource, weekScrollPickerItemsSource, daysScrollPickerItemsSource];

        var currentWeek = GetCurrentWeek();
        var test = Enumerable.Range(1, GetNumberOfWeeksInCurrentYear() - 1).ToList();
        var nullableItems = new StandardScrollPickerComponent<int>(test, currentWeek, OnSelectedItemChanged, true, true);
        ItemComponents = [nullableItems];
    }

    private static int GetCurrentWeek()
    {
        var dfi = DateTimeFormatInfo.CurrentInfo;
        var date = DateTime.Now;
        var cal = dfi.Calendar;
        return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
    }
    
    private static int GetNumberOfWeeksInCurrentYear()
    {
        var dfi = DateTimeFormatInfo.CurrentInfo;
        var date1 = new DateTime(DateTime.Now.Year, 12, 31);
        var cal = dfi.Calendar;
        return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
    }
    
    private async void OnSelectedItemChanged(int obj)
    {
    }

    private List<int> GetWeeksInYear(int year)
    {
        var dfi = DateTimeFormatInfo.CurrentInfo;
        var date1 = new DateTime(year, 12, 31);
        var cal = dfi.Calendar;
        return Enumerable.Range(1, cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek)).ToList();
    }

    public List<IScrollPickerComponent> DateComponents { get; }
    public List<IScrollPickerComponent> FootballersComponents { get; }
    public List<IScrollPickerComponent> ItemComponents { get; set; }
    
    public List<IScrollPickerComponent> NullableItemComponents { get; }
}