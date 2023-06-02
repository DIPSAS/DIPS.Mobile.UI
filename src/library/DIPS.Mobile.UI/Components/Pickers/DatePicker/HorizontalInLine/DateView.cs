using System.Windows.Input;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.DUITouchEffect;
using DIPS.Mobile.UI.Extensions.Markup;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

internal class DateView : Grid
{
    public DateView()
    {
        RowSpacing = Sizes.GetSize(SizeName.size_2);
        // WidthRequest = Sizes.GetSize(SizeName.size_20);

        RowDefinitions =
            new RowDefinitionCollection(new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Auto));
        // DUITouchEffect.SetCommand(this, Command);
        // DUITouchEffect.SetCommandParameter(this, BindingContext);
        SetBinding(BackgroundColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_primary_90),
                    FalseObject = Colors.GetColor(ColorName.color_neutral_05)
                }));
        //Month and year if year is not current year, using contentview because of Release Mode bug on Android where Label backgroundcolor bindings does not work
        var monthAndYearLabel = CreateLabel(new Label() {FontSize = Sizes.GetSize(SizeName.size_4)});
        var monthAndYearLabelContentView = new ContentView {Content = monthAndYearLabel};
        monthAndYearLabelContentView.SetBinding(BackgroundColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_secondary_90),
                    FalseObject = Colors.GetColor(ColorName.color_neutral_05)
                }));
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
        this.Add(monthAndYearLabelContentView, 0, 0);

        //Month label if year is current year, using contentview because of Release Mode bug on Android where Label backgroundcolor bindings does not work
        var monthLabel = CreateLabel(new Label() {FontSize = Sizes.GetSize(SizeName.size_4)});
        var monthLabelContentView = new ContentView(){Content = monthLabel};
        // monthLabelContentView.Padding = new Thickness(Sizes.GetSize(SizeName.size_3));
        monthLabelContentView.SetBinding(BackgroundColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_secondary_90),
                    FalseObject = Colors.GetColor(ColorName.color_neutral_05)
                }));
        monthLabel.HorizontalTextAlignment = TextAlignment.Center;
        monthLabel.TextTransform = TextTransform.Uppercase;
        monthLabel.SetBinding(IsVisibleProperty,
            new Binding(nameof(SelectableDateViewModel.IsCurrentYear)));
        monthLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty,
            new Binding(nameof(SelectableDateViewModel.MonthName)));

        this.Add(monthLabelContentView, 0, 0);

        //Year label
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

        //Day label
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

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(DateView), propertyChanged: (bindable, value, newValue) =>
        {
        });

    private SelectableDateViewModel m_selectableDateViewModel;

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private Label CreateLabel(Label theLabel)
    {
        theLabel.FontSize = Sizes.GetSize(SizeName.size_4);
        theLabel.HorizontalTextAlignment = TextAlignment.Center;
        theLabel.VerticalTextAlignment = TextAlignment.Center;
        return theLabel;
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is SelectableDateViewModel selectableDateViewModel)
        {
            m_selectableDateViewModel = selectableDateViewModel;
        }
    }
}