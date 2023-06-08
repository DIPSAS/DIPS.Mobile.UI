using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Slideable;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

public partial class HorizontalInlineDatePicker : ContentView
{
    private SlidableContentLayout m_slidableContentLayout;
    private SelectableDateViewModel? m_selectedDateViewModel;
    private List<SelectableDateViewModel> m_selectableViewModels = new(); 

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
        m_slidableContentLayout.TappedCommand = new Command<int>(ItemTapped);
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

    private void ItemTapped(int index)
    {
        var selectedDate = m_selectableViewModels.FirstOrDefault(s => s.IsSelected);
        if (selectedDate != null)
        {
            var selectedDateIndex = m_selectableViewModels.IndexOf(selectedDate);
            
            var tappedIndex = selectedDateIndex + index;
            var tappedSelectableDateViewModel = m_selectableViewModels[tappedIndex];
            OnDateTapped(tappedSelectableDateViewModel.FullDate);
        }
    }

    private void OnDateTapped(DateTime dateTime)
    {
        var previousSelectedDateTime = m_selectableViewModels.FirstOrDefault(s => s.IsSelected);
        if (previousSelectedDateTime == null) return;
        
        if (previousSelectedDateTime.FullDate.Date == dateTime.Date) //Tapped the same date that was already selected
        {
            var minDateTime = DateTime.Now.AddDays(-MaxSelectableDaysFromToday);
            var maxDateTime = DateTime.Now.AddDays(MaxSelectableDaysFromToday);
            var datePicker = new DatePicker()
            {
                SelectedDate = dateTime,
                MinimumDate = minDateTime,
                MaximumDate = maxDateTime,
            };
            datePicker.SelectedDateCommand = new Command(() =>
            {
                ScrollToDate(datePicker.SelectedDate, false);
            });
            DatePickerService.Open(datePicker);
        }
        else
        {
            ScrollToDate(dateTime.Date, true);
        }
        
        
    }

    private void ScrollToDate(DateTime date, bool shouldAnimate)
    {
        var i = (int) Math.Round((date.Date - DateTime.Now.Date).TotalDays);
        
        if (shouldAnimate)
        {
            m_slidableContentLayout.ScrollTo(i);    
        }
        else
        {
            m_slidableContentLayout.SlideProperties = new SlidableProperties(i);    
        }
        
    }

    private object CreateSelectableDateViewModel(int i)
    {
        var dateTime = DateTime.Now.AddDays(i);
        var isSelected = dateTime.Date == SelectedDate.Date;
        var selectableDateViewModel = new SelectableDateViewModel(dateTime, isSelected);

        if (selectableDateViewModel.IsSelected)
        {
            m_selectedDateViewModel = selectableDateViewModel;
        }
        
        m_selectableViewModels.Add(selectableDateViewModel);
        return selectableDateViewModel;
    }

    private void OnDateScrolledTo(int i)
    {
        SelectedDate = DateTime.Now.AddDays(i).Date;

        var previousSelectedDateTime = m_selectableViewModels.FirstOrDefault(s => s.IsSelected);
        if (previousSelectedDateTime != null)
        {
            previousSelectedDateTime.IsSelected = false;
        }

        var newSelectedDateTime = m_selectableViewModels.FirstOrDefault(s => s.FullDate.Date == SelectedDate.Date);
        if (newSelectedDateTime != null)
        {
            newSelectedDateTime.IsSelected = true;
        }
    }
}