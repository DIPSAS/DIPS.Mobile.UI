using Components.ComponentsSamples.Pickers.ScrollPickerComponents;
using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers;

public class ScrollPickerSamplesViewModel
{
    public ScrollPickerSamplesViewModel()
    {
        var items = new List<string> { "Item 1", "Item 2", "Item 3" };
        var scrollPickerItemsSource = new StandardScrollPickerComponent<string>(items);
        ScrollPickerViewModel = new ScrollPickerViewModel(scrollPickerItemsSource);
        
        var englishFootballers = new List<string> { "Foden", "Kane", "Rashford" };
        var norwegianFootballers = new List<string> { "Haaland", "Odegaard", "Sørloth" };
        var englishFootballersScrollPickerItemsSource = new StandardScrollPickerComponent<string>(englishFootballers, englishFootballers[1]);
        var norwegianFootballerScrollPickerItemsSource = new StandardScrollPickerComponent<string>(norwegianFootballers);
        FootballersScrollPickerViewModel = new ScrollPickerViewModel(englishFootballersScrollPickerItemsSource, norwegianFootballerScrollPickerItemsSource);
        
        
        var yearsScrollPickerItemsSource = new YearScrollPickerComponent();
        var monthsScrollPickerItemsSource = new MonthScrollPickerComponent();
        var daysScrollPickerItemsSource = new DayScrollPickerComponent();
        
        DateScrollPickerViewModel = new ScrollPickerViewModel(yearsScrollPickerItemsSource, monthsScrollPickerItemsSource, daysScrollPickerItemsSource);
    }

    public IScrollPickerViewModel DateScrollPickerViewModel { get; set; }
    public IScrollPickerViewModel ScrollPickerViewModel { get; set; }
    public IScrollPickerViewModel FootballersScrollPickerViewModel { get; set; }
}