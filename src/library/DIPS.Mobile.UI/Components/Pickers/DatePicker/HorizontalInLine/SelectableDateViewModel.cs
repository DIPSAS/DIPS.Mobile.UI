using System.Globalization;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

public class SelectableDateViewModel : ViewModel
{
    private readonly DateTime m_dateTime;
    private bool m_isSelected;

    public SelectableDateViewModel(DateTime dateTime, bool isSelected = false)
    {
        m_dateTime = dateTime;
        m_isSelected = isSelected;
    }

    public bool IsSelected
    {
        get => m_isSelected;
        set => RaiseWhenSet(ref m_isSelected, value);
    }

    public string YearName => m_dateTime.ToString("yyyy", CultureInfo.CurrentCulture).Substring(0, 4);
    public int Day => m_dateTime.Day;
    public DateTime FullDate => m_dateTime;

    public string MonthName => m_dateTime.ToString("MMMM", CultureInfo.CurrentCulture).Substring(0, 3);

    public string DayName =>
        DateTime.Now.Date == FullDate.Date
            ? DUILocalizedStrings.Today
            : m_dateTime.ToString("dddd", CultureInfo.CurrentCulture).Substring(0, 3);

    public string FormattedTime => m_dateTime.ToString("HH:mm", CultureInfo.InvariantCulture);

    public bool IsCurrentYear => m_dateTime.Year == DateTime.Now.Year;
}