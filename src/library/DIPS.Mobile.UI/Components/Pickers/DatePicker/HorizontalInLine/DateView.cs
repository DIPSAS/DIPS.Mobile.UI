using DIPS.Mobile.UI.Components.Content;
using DIPS.Mobile.UI.Components.Content.DataTemplateSelectors;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Extensions.Markup;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

internal class DateView : Grid
{
    public DateView()
    {
        RowSpacing = Sizes.GetSize(SizeName.size_2);
        RowDefinitions =
            new RowDefinitionCollection(new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Auto));

        SetBinding(BackgroundColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_primary_90),
                    FalseObject = Colors.GetColor(ColorName.color_neutral_05)
                }));


        //Month header
        this.Add(CreateMonthHeaderContentControl(), 0, 0);

        //Day number label
        var dayLabel = CreateLabel(new Label());
        dayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_system_white),
                    FalseObject = Colors.GetColor(ColorName.color_system_black),
                }));
        dayLabel.SetBinding(Label.TextProperty,
            new Binding(nameof(SelectableDateViewModel.Day)));

        this.Add(dayLabel, 0, 1);

        //Day shortname label
        var dayNameLabel = CreateLabel(new Label());
        dayNameLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_system_white),
                    FalseObject = Colors.GetColor(ColorName.color_system_black),
                }));
        dayNameLabel.SetBinding(Label.TextProperty,
            new Binding(nameof(SelectableDateViewModel.DayName)));

        this.Add(dayNameLabel, 0, 2);
    }

    private ContentControl CreateMonthHeaderContentControl()
    {
        var contentControl = new ContentControl();
        contentControl.Padding = new Thickness(Sizes.GetSize(SizeName.size_2));
        
        contentControl.SetBinding(BackgroundColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_secondary_90),
                    FalseObject = Colors.GetColor(ColorName.color_neutral_05)
                }));
        contentControl.SetBinding(ContentControl.SelectorItemProperty,
            new Binding(nameof(SelectableDateViewModel.IsCurrentYear)));
        contentControl.TemplateSelector = new BooleanDataTemplateSelector()
        {
            TrueTemplate = new DataTemplate(() =>
            {
                var monthLabel = CreateLabel(new Label() {FontSize = Sizes.GetSize(SizeName.size_4)});
                monthLabel.HorizontalTextAlignment = TextAlignment.Center;
                monthLabel.TextTransform = TextTransform.Uppercase;
                monthLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty,
                    new Binding(nameof(SelectableDateViewModel.MonthName)));
                return monthLabel;
            }),
            FalseTemplate = new DataTemplate(() =>
            {
                var monthAndYearLabel = CreateLabel(new Label() {FontSize = Sizes.GetSize(SizeName.size_4)});
                var monthNameSpan =
                    new Span() {FontSize = Sizes.GetSize(SizeName.size_4), TextTransform = TextTransform.Uppercase};
                monthNameSpan.SetBinding(Span.TextProperty,
                    new Binding(nameof(SelectableDateViewModel.MonthName),
                        converter: new StringCaseConverter() {StringCase = StringCase.Upper}));
                var blankSpan = new Span() {Text = " "};
                var yearNameSpan = new Span() {FontSize = Sizes.GetSize(SizeName.size_4)};
                yearNameSpan.SetBinding(Span.TextProperty,
                    new Binding(nameof(SelectableDateViewModel.YearName)));
                monthAndYearLabel.FormattedText =
                    new FormattedString() {Spans = {monthNameSpan, blankSpan, yearNameSpan}};
                return monthAndYearLabel;
            })
        };
        return contentControl;
    }

    private Label CreateLabel(Label theLabel)
    {
        theLabel.FontSize = Sizes.GetSize(SizeName.size_4);
        theLabel.HorizontalTextAlignment = TextAlignment.Center;
        theLabel.VerticalTextAlignment = TextAlignment.Center;
        return theLabel;
    }
}