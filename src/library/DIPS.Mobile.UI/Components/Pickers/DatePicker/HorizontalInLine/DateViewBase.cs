using DIPS.Mobile.UI.Components.Content;
using DIPS.Mobile.UI.Components.Content.DataTemplateSelectors;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Extensions.Markup;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
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
        UI.Effects.Layout.Layout.SetStroke(this, Colors.GetColor(ColorName.color_border_button));
        
        Loaded += CreateView;
    }

    private void CreateView(object? sender, EventArgs eventArgs)
    {
        this.SetBinding(BackgroundColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_fill_default_active),
            FalseObject = Colors.GetColor(ColorName.color_fill_neutral)
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
        
        dayLabel.SetBinding(StyleProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Styles.GetLabelStyle(LabelStyle.UI200),
            FalseObject = Styles.GetLabelStyle(LabelStyle.Body200),
        });
        
        dayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_text_default),
            FalseObject = Colors.GetColor(ColorName.color_text_subtle),
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
            TrueObject = Colors.GetColor(ColorName.color_text_default),
            FalseObject = Colors.GetColor(ColorName.color_text_subtle),
        });
        dayLabel.SetBinding(StyleProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Styles.GetLabelStyle(LabelStyle.UI200),
            FalseObject = Styles.GetLabelStyle(LabelStyle.Body200),
        });
        dayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.Day);

        this.Add(dayLabel, 1);

        OnViewCreated();
    }
    
    protected virtual void OnViewCreated() {}

    private View CreateMonthHeaderContentControl()
    {
        var container = new Grid { Padding = new Thickness(Sizes.GetSize(SizeName.size_2)) };

        container.SetBinding(BackgroundColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter
        {
            TrueObject = Colors.GetColor(ColorName.color_fill_default_active),
            FalseObject = Colors.GetColor(ColorName.color_fill_neutral)
        });
        
        var monthAndYearLabel = CreateLabel(new Label
        {
            Style = Styles.GetLabelStyle(LabelStyle.Body200)
        });
        
        var monthNameSpan = new Span();
        monthNameSpan.SetBinding(Span.TextColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_text_default),
            FalseObject = Colors.GetColor(ColorName.color_text_subtle),
        });
        
        monthNameSpan.SetBinding(Span.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.MonthName);

        var blankSpan = new Span { Text = Environment.NewLine };
        var yearNameSpan = new Span();
        yearNameSpan.SetBinding(Span.TextColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_text_default),
            FalseObject = Colors.GetColor(ColorName.color_text_subtle),
        });
        yearNameSpan.SetBinding(Span.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.YearName);
        
        monthAndYearLabel.FormattedText = new FormattedString { Spans = { monthNameSpan, blankSpan, yearNameSpan } };

        container.Add(monthAndYearLabel);

        return container;
    }

    protected static Label CreateLabel(Label theLabel)
    {
        theLabel.HorizontalTextAlignment = TextAlignment.Center;
        theLabel.VerticalTextAlignment = TextAlignment.Center;
        return theLabel;
    }
}