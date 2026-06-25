using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using DuiDateAndTimePicker = DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker.DateAndTimePicker;
using DuiCollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using DuiContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using DuiDatePicker = DIPS.Mobile.UI.Components.Pickers.DatePicker.DatePicker;
using DuiLabel = DIPS.Mobile.UI.Components.Labels.Label;
using DuiRefreshView = DIPS.Mobile.UI.Components.Loading.RefreshView;
using DuiTimePicker = DIPS.Mobile.UI.Components.Pickers.TimePicker.TimePicker;

namespace Playground.VetleSamples;

public class DatePickerFlingReproPage : DuiContentPage
{
    private readonly ObservableCollection<DatePickerReproItem> m_items = new(
        Enumerable.Range(1, 80).Select(index => new DatePickerReproItem(index)));

    private readonly DuiLabel m_statusLabel = new()
    {
        Style = Styles.GetLabelStyle(LabelStyle.UI200),
        Text = "No selection yet"
    };

    public DatePickerFlingReproPage()
    {
        Title = "DatePicker Fling Repro";

        var header = new Grid
        {
            Padding = new Thickness(
                Sizes.GetSize(SizeName.size_5),
                Sizes.GetSize(SizeName.size_4),
                Sizes.GetSize(SizeName.size_5),
                Sizes.GetSize(SizeName.size_2))
        };

        header.Add(new DuiLabel
        {
            Text = "Picker rows (80)",
            Style = Styles.GetLabelStyle(LabelStyle.SectionHeader),
            VerticalTextAlignment = TextAlignment.Center
        });

        var collectionView = new DuiCollectionView
        {
            ItemSpacing = Sizes.GetSize(SizeName.size_2),
            ShouldBounce = true,
            HasAdditionalSpaceAtTheEnd = true,
            ItemsSource = m_items,
            ItemTemplate = new DataTemplate(CreateDatePickerCell),
            EmptyView = m_statusLabel
        };

        var refreshView = new DuiRefreshView
        {
            Content = collectionView,
            Command = new Command(() => SetStatus("Refresh completed"))
        };

        refreshView.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == DuiRefreshView.IsRefreshingProperty.PropertyName && refreshView.IsRefreshing)
            {
                refreshView.IsRefreshing = false;
            }
        };

        Grid.SetRow(refreshView, 1);

        Content = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Star)
            ],
            Children =
            {
                header,
                refreshView
            }
        };
    }

    private View CreateDatePickerCell()
    {
        var root = new Grid
        {
            ColumnDefinitions =
            [
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            ],
            ColumnSpacing = Sizes.GetSize(SizeName.size_3),
            Padding = new Thickness(Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_2)),
            BackgroundColor = Colors.GetColor(ColorName.color_surface_default)
        };

        Touch.SetCommand(root, new Command(() => SetStatus("Date row tapped")));
        SemanticProperties.SetDescription(root, "Date row");

        var textGrid = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Auto)
            ]
        };

        var titleLabel = new DuiLabel
        {
            Style = Styles.GetLabelStyle(LabelStyle.UI300),
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };
        titleLabel.SetBinding(Label.TextProperty, nameof(DatePickerReproItem.Title));

        var subtitleLabel = new DuiLabel
        {
            Style = Styles.GetLabelStyle(LabelStyle.UI200),
            TextColor = Colors.GetColor(ColorName.color_text_subtle)
        };
        subtitleLabel.SetBinding(Label.TextProperty, nameof(DatePickerReproItem.Subtitle));

        textGrid.Add(titleLabel);
        textGrid.Add(subtitleLabel, 0, 1);

        var picker = CreatePicker();

        root.Add(textGrid);
        root.Add(picker, 1);

        return root;
    }

    private View CreatePicker()
    {
        var datePicker = new DuiDatePicker
        {
            IgnoreLocalTime = true,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        datePicker.SetBinding(DuiDatePicker.SelectedDateProperty, nameof(DatePickerReproItem.Date));
        datePicker.SelectedDateCommand = new Command<DateTime?>(date =>
            SetStatus(date.HasValue ? $"Date selected: {date.Value:yyyy-MM-dd}" : "Date cleared"));

        var timePicker = new DuiTimePicker
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        timePicker.SetBinding(DuiTimePicker.SelectedTimeProperty, nameof(DatePickerReproItem.Time));
        timePicker.SelectedTimeCommand = new Command<TimeSpan?>(time =>
            SetStatus(time.HasValue ? $"Time selected: {time.Value:hh\\:mm}" : "Time cleared"));

        var dateAndTimePicker = new DuiDateAndTimePicker
        {
            IgnoreLocalTime = true,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        dateAndTimePicker.SetBinding(DuiDateAndTimePicker.SelectedDateTimeProperty, nameof(DatePickerReproItem.DateAndTime));
        dateAndTimePicker.SelectedDateTimeCommand = new Command<DateTime?>(dateTime =>
            SetStatus(dateTime.HasValue ? $"Date and time selected: {dateTime.Value:yyyy-MM-dd HH:mm}" : "Date and time cleared"));

        datePicker.SetBinding(IsVisibleProperty, nameof(DatePickerReproItem.IsDatePicker));
        timePicker.SetBinding(IsVisibleProperty, nameof(DatePickerReproItem.IsTimePicker));
        dateAndTimePicker.SetBinding(IsVisibleProperty, nameof(DatePickerReproItem.IsDateAndTimePicker));

        return new Grid
        {
            Children =
            {
                datePicker,
                timePicker,
                dateAndTimePicker
            }
        };
    }

    private void SetStatus(string status)
    {
        m_statusLabel.Text = status;
        Console.WriteLine(status);
    }

    private sealed class DatePickerReproItem(int index)
    {
        public string Title { get; } = $"Picker row {index:00}";
        public string Subtitle { get; } = GetSubtitle(index);
        public DateTime Date { get; set; } = DateTime.Today.AddDays(index);
        public TimeSpan Time { get; set; } = new(8 + index % 10, index % 60, 0);
        public DateTime DateAndTime { get; set; } = DateTime.Today.AddDays(index).AddHours(8 + index % 10).AddMinutes(index % 60);
        public bool IsDatePicker => index % 3 == 1;
        public bool IsTimePicker => index % 3 == 2;
        public bool IsDateAndTimePicker => index % 3 == 0;

        private static string GetSubtitle(int index) => (index % 3) switch
        {
            1 => "DatePicker - open, dismiss, then scroll",
            2 => "TimePicker - open, dismiss, then scroll",
            _ => "DateAndTimePicker - open, dismiss, then scroll"
        };
    }
}