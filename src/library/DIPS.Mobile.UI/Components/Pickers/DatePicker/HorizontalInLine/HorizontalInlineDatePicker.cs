using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Slidable;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using TimePickerService = DIPS.Mobile.UI.Components.Pickers.TimePicker.TimePickerService;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

public partial class HorizontalInlineDatePicker : ContentView
{
    private SlidableContentLayout m_slidableContentLayout;
    private DateTime? m_startDate;
    private DateTime? m_maxDateTime;
    private DateTime? m_minDateTime;

    public HorizontalInlineDatePicker()
    {
        m_slidableContentLayout =
            new SlidableContentLayout
            {
                HeightRequest = GetHeightBasedOnScreenHeight(),
                BackgroundColor = Colors.GetColor(ColorName.color_surface_subtle), ScaleDown = false,
                BindingContextFactory = CreateSelectableDateViewModel,
                ItemTemplate = new DataTemplate(() => new DateView()),
                Config = new SliderConfig(-MaxSelectableDaysFromToday, MaxSelectableDaysFromToday),
                SelectedItemChangedCommand = new Command<int>(OnDateScrolledTo)
            };
        m_slidableContentLayout.ContentTapped += ItemTapped;
        Content = m_slidableContentLayout;
        
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplayOnMainDisplayInfoChanged;
    }

    private void DeviceDisplayOnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        m_slidableContentLayout.HeightRequest = GetHeightBasedOnScreenHeight();
    }

    private double GetHeightBasedOnScreenHeight() =>
        DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density / 6.5;

    private void ItemTapped(object? sender, ContentTappedEventArgs contentTappedEventArgs)
    {
        
        if (contentTappedEventArgs.View.BindingContext is not SelectableDateViewModel tappedSelectableDateViewModel) return;

        if (tappedSelectableDateViewModel.IsSelected) //Tapped the same date that was already selected
        {
            var datePicker = new DatePicker()
            {
                SelectedDate = tappedSelectableDateViewModel.FullDate,
                MinimumDate = m_minDateTime,
                MaximumDate = m_maxDateTime,
            };
            datePicker.SelectedDateCommand = new Command(() =>
            {
                if (TryGetIndexFromDate(datePicker.SelectedDate, out var index))
                {
                    ScrollToIndex(index, false);
                }
            });
            DatePickerService.Open(datePicker, this);
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
        if (m_startDate == null) //This will only run once
        {
            m_startDate = SelectedDate;
            m_minDateTime = m_startDate.Value.AddDays(-MaxSelectableDaysFromToday).Date;
            m_maxDateTime = m_startDate.Value.AddDays(MaxSelectableDaysFromToday).Date;
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
            UpdateInternalIsSelectedState(dateScrolledTo);
            
            if (SelectedDate.Date == dateScrolledTo.Date) return; //No need to update, and to stop this from getting into a infinite loop
            
            SelectedDate = dateScrolledTo;
            VibrationService.SelectionChanged();
            
            
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
            var slidableContentLayoutIndex = (int)Math.Round(m_slidableContentLayout.SlideProperties.Position);
            if (slidableContentLayoutIndex != index)
            {
                ScrollToIndex(index, false);
            }
        }
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
        {
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplayOnMainDisplayInfoChanged;
        }
    }
}