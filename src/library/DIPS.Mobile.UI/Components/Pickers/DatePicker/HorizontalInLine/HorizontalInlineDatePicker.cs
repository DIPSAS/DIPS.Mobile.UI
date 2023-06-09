using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Slideable;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using TappedEventArgs = DIPS.Mobile.UI.Components.Slideable.TappedEventArgs;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

public partial class HorizontalInlineDatePicker : ContentView
{
    private SlidableContentLayout m_slidableContentLayout;
    private List<SelectableDateViewModel> m_selectableViewModels = new();
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
        /*
        *  <dxui:SlidableContentLayout.ItemTemplate>
               <DataTemplate x:DataType="{x:Type Common:DateViewModel}">
                   <Calendar:DateView effects:TouchEffect.NativeAnimation="True">
                       <Calendar:DateView.GestureRecognizers>
                               <TapGestureRecognizer Tapped="DateTapped" />
                           </Calendar:DateView.GestureRecognizers>
                   </Calendar:DateView>
               </DataTemplate>
           </dxui:SlidableContentLayout.ItemTemplate>
        */


        /*
         *<Grid RowSpacing="0">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <Shared:Separator Grid.Row="0"
                 Opacity="0.2"/>
        <!--  A fake datepicker entry to use the built in datepicker when tapping today item  -->
        <dxui:SlidableContentLayout x:Name="HorizontalCalendarSlideableContentLayout"
                                    Grid.Row="1"
                                    BackgroundColor="{x:Static dxui:ColorPalette.LightLight}"
                                    BindingContextFactory="{Binding CreateDate}"
                                    ScaleDown="False">
            <!-- Slideable Content Layout config (to set range) is set in the code behind when context changed -->
            <dxui:SlidableContentLayout.ItemTemplate>
                <DataTemplate x:DataType="{x:Type Common:DateViewModel}">
                    <Calendar:DateView effects:TouchEffect.NativeAnimation="True">
                        <Calendar:DateView.GestureRecognizers>
                                <TapGestureRecognizer Tapped="DateTapped" />
                            </Calendar:DateView.GestureRecognizers>
                    </Calendar:DateView>
                </DataTemplate>
            </dxui:SlidableContentLayout.ItemTemplate>
        </dxui:SlidableContentLayout>
    </Grid>
         * 
         */
        Content = m_slidableContentLayout;
    }

    private void ItemTapped(object? sender, ContentTappedEventArgs contentTappedEventArgs)
    {
        
        if (contentTappedEventArgs.View.BindingContext is not SelectableDateViewModel tappedSelectableDateViewModel) return;
        
        var previousSelectedDateTime = m_selectableViewModels.FirstOrDefault(s => s.IsSelected);
        if (previousSelectedDateTime == null) return;
        
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
        index = (int) Math.Round((dateTime - m_startDate.Value.Date).TotalDays);
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
        
        var isSelected = dateTime.Date == m_startDate; //Only true the first time we load the layout, every other event is happening in OnDateScrolledTo
        var selectableDateViewModel = new SelectableDateViewModel(dateTime, isSelected);
        
        if (isSelected)
        {
            var previousSelectedDateTime = m_selectableViewModels.FirstOrDefault(s => s.IsSelected);
            if (previousSelectedDateTime != null)
            {
                previousSelectedDateTime.IsSelected = false;
            }    
        }
        
        m_selectableViewModels.Add(selectableDateViewModel);
        return selectableDateViewModel;
    }

    private void OnDateScrolledTo(int i)
    {
        if (TryGetDateFromIndex(i, out var dateScrolledTo))
        {
            if (SelectedDate == dateScrolledTo) return; //No need to update, and to stop this from getting into a infinite loop
            
            SelectedDate = dateScrolledTo;
            
            UpdateInternalIsSelectedState(dateScrolledTo);
        }
    }

    private void UpdateInternalIsSelectedState(DateTime dateScrolledTo)
    {
        var previousSelectedDateTime = m_selectableViewModels.FirstOrDefault(s => s.IsSelected);
        if (previousSelectedDateTime != null)
        {
            previousSelectedDateTime.IsSelected = false;
        }

        var newSelectedDateTime = m_selectableViewModels.FirstOrDefault(s => s.FullDate.Date == dateScrolledTo.Date);
        if (newSelectedDateTime != null)
        {
            newSelectedDateTime.IsSelected = true;
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