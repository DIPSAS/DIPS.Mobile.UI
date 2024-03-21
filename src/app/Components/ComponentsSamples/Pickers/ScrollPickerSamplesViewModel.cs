using System.Globalization;
using Components.ComponentsSamples.Pickers.ScrollPickerComponents;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers;

public class ScrollPickerSamplesViewModel
{
    public ScrollPickerSamplesViewModel()
    {
        var items = new List<string> { "Item 1", "Item 2", "Item 3" };
        var itemsComponents = new StandardScrollPickerComponent<string>(items);
        ItemComponents = [itemsComponents];
        
        var englishFootballers = new List<string> { "Foden", "Kane", "Rashford" };
        var norwegianFootballers = new List<string> { "Haaland", "Odegaard", "Sørloth" };
        var englishFootballersComponent = new StandardScrollPickerComponent<string>(englishFootballers, englishFootballers[1]);
        var norwegianFootballersComponent = new StandardScrollPickerComponent<string>(norwegianFootballers);
        FootballersComponents = [englishFootballersComponent, norwegianFootballersComponent];
        
        var yearsScrollPickerItemsSource = new YearScrollPickerComponent();
        var weekScrollPickerItemsSource = new StandardScrollPickerComponent<int>(GetWeeksInYear(DateTime.Now.Year), 10, OnSelectedItemChanged);
        var daysScrollPickerItemsSource = new DayScrollPickerComponent();
        DateComponents = [yearsScrollPickerItemsSource, weekScrollPickerItemsSource, daysScrollPickerItemsSource];
    }

    private static void OnSelectedItemChanged(int obj)
    {
        SystemMessageService.Display(config =>
        {
            config.Text = obj.ToString();
        });
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


}