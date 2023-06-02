using DIPS.Mobile.UI.Components.Slideable;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

public partial class HorizontalInlineDatePicker : ContentView
{
    private SlidableContentLayout m_slidableContentLayout;
    private SelectableDateViewModel? m_selectedDateViewModel;

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
        m_slidableContentLayout.ItemTemplate = new DataTemplate(() => new DateView()
        {
            Command = new Command<SelectableDateViewModel>(OnDateTapped)
        });
        m_slidableContentLayout.Config = new SliderConfig(-MaxSelectableDaysFromToday, MaxSelectableDaysFromToday);
        m_slidableContentLayout.SelectedItemChangedCommand = new Command<int>(OnDateScrolledTo);
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

    private void OnDateTapped(SelectableDateViewModel selectableDateViewModel)
    {
        if (selectableDateViewModel.IsSelected)
        {
            // m_datePicker.Focus(); //Opens the date picker
        }
        else
        {
            var i = (int)Math.Round((selectableDateViewModel.FullDate.Date.Date - DateTime.Now.Date).TotalDays);
            m_slidableContentLayout.ScrollTo(i);
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

        return selectableDateViewModel;
    }

    private void OnDateScrolledTo(int i)
    {
        SelectedDate = DateTime.Now.AddDays(i).Date;
        var selectedDateBindingContext = m_slidableContentLayout.GetView(i)?.BindingContext;
        if (selectedDateBindingContext is SelectableDateViewModel selectedDateViewModel)
        {
            if (m_selectedDateViewModel != null)
            {
                m_selectedDateViewModel.IsSelected = false;
            }

            m_selectedDateViewModel = selectedDateViewModel;
            selectedDateViewModel.IsSelected = true;
        }
    }
}