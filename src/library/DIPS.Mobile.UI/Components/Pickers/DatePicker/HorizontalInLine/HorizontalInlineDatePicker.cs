using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Slidable;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

public partial class HorizontalInlineDatePicker : ContentView
{
    private SlidableContentLayout m_slidableContentLayout;
    private DateTime? m_startDate;

    public HorizontalInlineDatePicker()
    {
        m_slidableContentLayout =
            new SlidableContentLayout()
            {
#if __IOS__
                HeightRequest = Sizes.GetSize(SizeName.size_25) + Sizes.GetSize(SizeName.size_3),
#elif __ANDROID__
                HeightRequest = Sizes.GetSize(SizeName.size_25) + Sizes.GetSize(SizeName.size_10),
#endif
                
                BackgroundColor = Colors.GetColor(ColorName.color_neutral_05), ScaleDown = false,
            };
        m_slidableContentLayout.BindingContextFactory = CreateSelectableDateViewModel;
        m_slidableContentLayout.ItemTemplate = new DataTemplate(() => new DateView());
        m_slidableContentLayout.Config = new SliderConfig(-MaxSelectableDaysFromToday, MaxSelectableDaysFromToday);
        m_slidableContentLayout.SelectedItemChangedCommand = new Command<int>(OnDateScrolledTo);
        m_slidableContentLayout.ContentTapped += ItemTapped;
        Content = m_slidableContentLayout;
    }

    private void ItemTapped(object? sender, ContentTappedEventArgs contentTappedEventArgs)
    {
        
        if (contentTappedEventArgs.View.BindingContext is not SelectableDateViewModel tappedSelectableDateViewModel) return;

        if (tappedSelectableDateViewModel.IsSelected) //Tapped the same date that was already selected
        {
            var minDateTime = SelectedDate.AddDays(-MaxSelectableDaysFromToday);
            var maxDateTime = SelectedDate.AddDays(MaxSelectableDaysFromToday);
            var datePicker = new DatePicker()
            {
                SelectedDate = tappedSelectableDateViewModel.FullDate,
                MinimumDate = minDateTime,
                MaximumDate = maxDateTime,
            };
            datePicker.SelectedDateCommand = new Command(() =>
            {
                if (TryGetIndexFromDate(datePicker.SelectedDate, out var index))
                {
                    ScrollToIndex(index, false);
                }
            });
            DatePickerService.Open(datePicker);
        }
        else
        {
            ScrollToIndex(contentTappedEventArgs.Index, true);
        }
    }

    private bool TryGetIndexFromDate(DateTime dateTime, out int index)
    {
        index = 0;
        if (m_startDate == null) return false;
        index = (dateTime.Date - m_startDate.Value.Date).Days;
        return true;
    }
    
    private bool TryGetDateFromIndex(int index, out DateTime dateTime)
    {
        dateTime = DateTime.MinValue;
        if (m_startDate is null) return false;
        dateTime = m_startDate.Value.AddDays(index);
        return true;
    }

    private void ScrollToIndex(int index, bool shouldAnimate)
    {
        try
        {
            if (shouldAnimate)
            {
                m_slidableContentLayout.ScrollTo(index);    
            }
            else
            {
                m_slidableContentLayout.SlideProperties = new SlidableProperties(index);    
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private object CreateSelectableDateViewModel(int i)
    {
        if (m_startDate == null)
        {
            m_startDate = SelectedDate;
        }
        
        var dateTime = m_startDate.Value.AddDays(i);
        
        var isSelected = dateTime.Date == m_startDate.Value.Date; //Only true the first time we load the layout
        var selectableDateViewModel = new SelectableDateViewModel(dateTime, isSelected);
        return selectableDateViewModel;
    }

    private void OnDateScrolledTo(int i)
    {
        if (TryGetDateFromIndex(i, out var dateScrolledTo))
        {
            if (SelectedDate.Date == dateScrolledTo.Date) return; //No need to update, and to stop this from getting into a infinite loop
            
            SelectedDate = dateScrolledTo;
            VibrationService.SelectionChanged();
            
            UpdateInternalIsSelectedState(dateScrolledTo);
        }
    }

    private void UpdateInternalIsSelectedState(DateTime dateScrolledTo)
    {
        var views = m_slidableContentLayout.GetCurrentViews();
        foreach (var currentView in views)
        {
            if (currentView is DateView {BindingContext: SelectableDateViewModel currentSelectableDateViewModel})
            {
                currentSelectableDateViewModel.IsSelected = false;
            }
        }
        TryGetIndexFromDate(dateScrolledTo, out var index);
        var view = m_slidableContentLayout.GetView(index);
        if (view is DateView {BindingContext: SelectableDateViewModel selectableDateViewModel})
        {
            selectableDateViewModel.IsSelected = true;
        }
    }

    private void OnSelectedDateChanged()
    {
        if (TryGetIndexFromDate(SelectedDate, out var index))
        {
            ScrollToIndex(index, false);
            UpdateInternalIsSelectedState(SelectedDate);
        }
    }
}