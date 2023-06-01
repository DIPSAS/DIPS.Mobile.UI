using System.Windows.Input;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.DUITouchEffect;
using DIPS.Mobile.UI.Extensions.Markup;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

internal class DateView : Grid
{
    public DateView()
    {
        RowSpacing = Sizes.GetSize(SizeName.size_2);
        WidthRequest = Sizes.GetSize(SizeName.size_20);
        HorizontalOptions = LayoutOptions.Fill;
        RowDefinitions =
            new RowDefinitionCollection(new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Auto));
        DUITouchEffect.SetCommand(this, Command);
        DUITouchEffect.SetCommandParameter(this, BindingContext);
        SetBinding(BackgroundColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_primary_90),
                    FalseObject = Colors.GetColor(ColorName.color_neutral_05)
                }));
        //Background
        // var boxView = new BoxView()
        // {
        //     WidthRequest = WidthRequest,
        //     BackgroundColor = Colors.GetColor(ColorName.color_system_white),
        //     Opacity = 0.2,
        //     HorizontalOptions = LayoutOptions.Fill
        // };
        // this.Add(boxView, 0, 0);

        //Month and year if year is not current year
        var monthAndYearLabel = CreateLabel(new Label());
        monthAndYearLabel.SetBinding(BackgroundColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_secondary_90),
                    FalseObject = Colors.GetColor(ColorName.color_neutral_05)
                }));
        monthAndYearLabel.Padding = Sizes.GetSize(SizeName.size_2);
        monthAndYearLabel.SetBinding(IsVisibleProperty,
            new Binding(nameof(SelectableDateViewModel.IsCurrentYear),
                converter: new InvertedBoolConverter()));
        var monthNameSpan =
            new Span() {FontSize = Sizes.GetSize(SizeName.size_4), TextTransform = TextTransform.Uppercase};
        monthNameSpan.SetBinding(Span.TextProperty,
            new Binding(nameof(SelectableDateViewModel.MonthName),
                converter: new StringCaseConverter() {StringCase = StringCase.Upper}));
        var blankSpan = new Span() {Text = " "};
        var yearNameSpan = new Span() {FontSize = Sizes.GetSize(SizeName.size_4)};
        yearNameSpan.SetBinding(Span.TextProperty,
            new Binding(nameof(SelectableDateViewModel.YearName)));
        monthAndYearLabel.FormattedText = new FormattedString() {Spans = {monthNameSpan, blankSpan, yearNameSpan}};

        this.Add(monthAndYearLabel, 0, 0);

        //Month label if year is current year
        var monthLabel = CreateLabel(new Label() {FontSize = Sizes.GetSize(SizeName.size_4)});
        monthLabel.SetBinding(BackgroundColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_secondary_90),
                    FalseObject = Colors.GetColor(ColorName.color_neutral_05)
                }));
        monthLabel.Padding = Sizes.GetSize(SizeName.size_2);
        monthLabel.HorizontalTextAlignment = TextAlignment.Center;
        monthLabel.TextTransform = TextTransform.Uppercase;
        monthLabel.SetBinding(IsVisibleProperty,
            new Binding(nameof(SelectableDateViewModel.IsCurrentYear)));
        monthLabel.SetBinding(Label.TextProperty,
            new Binding(nameof(SelectableDateViewModel.MonthName)));

        this.Add(monthLabel, 0, 0);

        //Year label
        var yearLabel = CreateLabel(new Label());
        yearLabel.SetBinding(Label.TextProperty,
            new Binding(nameof(SelectableDateViewModel.Day)));

        this.Add(yearLabel, 0, 1);

        //Day label
        var dayLabel = CreateLabel(new Label() {FontSize = Sizes.GetSize(SizeName.size_4)});
        dayLabel.SetBinding(Label.TextProperty,
            new Binding(nameof(SelectableDateViewModel.DayName)));

        this.Add(dayLabel, 0, 2);
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(DateView), propertyChanged: (bindable, value, newValue) => 
        {
            
        });

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    private Label CreateLabel(Label theLabel)
    {
        theLabel.FontSize = Sizes.GetSize(SizeName.size_5);
        theLabel.HorizontalTextAlignment = TextAlignment.Center;
        theLabel.VerticalTextAlignment = TextAlignment.Center;
        theLabel.SetBinding(Label.TextColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_system_white),
                    FalseObject = Colors.GetColor(ColorName.color_system_black),
                }));
        return theLabel;
    }
}