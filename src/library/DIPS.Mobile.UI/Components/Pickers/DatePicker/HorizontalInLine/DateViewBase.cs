using DIPS.Mobile.UI.Components.Content;
using DIPS.Mobile.UI.Components.Content.DataTemplateSelectors;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Extensions.Markup;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

/// <summary>
/// TODO: Rewrite this so its easy to use publicly if its needed.
/// </summary>
public abstract class DateViewBase : Grid
{
    protected DateViewBase()
    {
        Loaded += CreateView;
    }

    private void CreateView(object? sender, EventArgs eventArgs)
    {
        this.SetBinding(BackgroundColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_text_action),
            // TODO: Lisa
            FalseObject = Colors.GetColor(ColorName.color_neutral_05)
        });
        
        if (DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait)
        {
            CreatePortraitView();
        }
        else
        {
            CreateLandscapeView();
        }
        
        Loaded -= CreateView;
    }
    
    private void CreatePortraitView()
    {
        RowSpacing = Sizes.GetSize(SizeName.size_2);
        RowDefinitions =
            new RowDefinitionCollection(new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Auto));

        //Month header
        this.Add(CreateMonthHeaderContentControl(), 0, 0);

        //Day number label
        var dayLabel = CreateLabel(new Label());
        dayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_system_white),
            FalseObject = Colors.GetColor(ColorName.color_system_black),
        });
        dayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.Day);

        this.Add(dayLabel, 0, 1);

        OnViewCreated();
        
    }

    private void CreateLandscapeView()
    {
        ColumnDefinitions =
            new ColumnDefinitionCollection(new ColumnDefinition(new GridLength(2, GridUnitType.Star)), 
                new ColumnDefinition(GridLength.Star), new ColumnDefinition(new GridLength(2, GridUnitType.Star)));
        
        //Month header
        this.Add(CreateMonthHeaderContentControl());
        
        //Day number label
        var dayLabel = CreateLabel(new Label());
        dayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_system_white),
            FalseObject = Colors.GetColor(ColorName.color_system_black),
        });
        dayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.Day);

        this.Add(dayLabel, 1);

        OnViewCreated();
    }
    
    protected virtual void OnViewCreated() {}

    private ContentControl CreateMonthHeaderContentControl()
    {
        var contentControl = new ContentControl { Padding = new Thickness(Sizes.GetSize(SizeName.size_2)) };

        contentControl.SetBinding(BackgroundColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_secondary_90),
            FalseObject = Colors.GetColor(ColorName.color_neutral_05)
        });
        contentControl.SetBinding(ContentControl.SelectorItemProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsCurrentYear);
        
        contentControl.TemplateSelector = new BooleanDataTemplateSelector()
        {
            TrueTemplate = new DataTemplate(() =>
            {
                var monthLabel = CreateLabel(new Label() {FontSize = Sizes.GetSize(SizeName.size_4)});
                monthLabel.HorizontalTextAlignment = TextAlignment.Center;
                monthLabel.TextTransform = TextTransform.Uppercase;
                monthLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.MonthName);

                if (DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape)
                {
                    monthLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
                    {
                        TrueObject = Colors.GetColor(ColorName.color_system_white),
                        FalseObject = Colors.GetColor(ColorName.color_system_black),
                    });
                }

                return monthLabel;
            }),
            FalseTemplate = new DataTemplate(() =>
            {
                var monthAndYearLabel = CreateLabel(new Label() {FontSize = Sizes.GetSize(SizeName.size_4)});
                var monthNameSpan =
                    new Span() {FontSize = Sizes.GetSize(SizeName.size_4), TextTransform = TextTransform.Uppercase};
                monthNameSpan.SetBinding(Span.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.MonthName,
                    converter: new StringCaseConverter() {StringCase = StringCase.Upper});
                var blankSpan = new Span() {Text = " "};
                var yearNameSpan = new Span() {FontSize = Sizes.GetSize(SizeName.size_4)};
                yearNameSpan.SetBinding(Span.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.YearName);
                monthAndYearLabel.FormattedText =
                    new FormattedString() {Spans = {monthNameSpan, blankSpan, yearNameSpan}};

                if (DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape)
                {
                    monthAndYearLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
                    {
                        TrueObject = Colors.GetColor(ColorName.color_system_white),
                        FalseObject = Colors.GetColor(ColorName.color_system_black),
                    });
                }

                return monthAndYearLabel;
            })
        };
        return contentControl;
    }

    protected static Label CreateLabel(Label theLabel, SizeName size = SizeName.size_4)
    {
        theLabel.FontSize = Sizes.GetSize(size);
        theLabel.HorizontalTextAlignment = TextAlignment.Center;
        theLabel.VerticalTextAlignment = TextAlignment.Center;
        return theLabel;
    }
}