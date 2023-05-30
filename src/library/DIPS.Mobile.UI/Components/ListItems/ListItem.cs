using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(ContentItem))]
public partial class ListItem : Border
{
    private Grid GridContent { get; }
    
    public ListItem()
    {
        BackgroundColor = Colors.GetColor(ColorName.color_system_white);
        StrokeShape = new RoundRectangle { CornerRadius = CornerRadius };

        GridContent = new Grid 
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new(GridLength.Auto),
                new(GridLength.Star),
                new(GridLength.Auto)
            },
            RowDefinitions = new RowDefinitionCollection()
            {
                new(GridLength.Auto)
            },
            Padding = new Thickness(Sizes.GetSize(SizeName.size_4), 
                Sizes.GetSize(SizeName.size_3),
                Sizes.GetSize(SizeName.size_4),
                Sizes.GetSize(SizeName.size_3))
        };

        Content = GridContent;
    }
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        AddLabel();
    }

    private void AddLabel()
    {
        var label = new Label 
        { 
            FontSize = Sizes.GetSize(SizeName.size_4),
            TextColor = Colors.GetColor(ColorName.color_neutral_90),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Start
        };

        if (string.IsNullOrEmpty(SubTitle))
        {
            label.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
        }
        else
        {
            var title = new Span();
            title.SetBinding(Span.TextProperty, new Binding(nameof(Title), source: this));

            var newLine = new Span { Text = Environment.NewLine };

            var subTitle = new Span { FontSize = Sizes.GetSize(SizeName.size_3), TextColor = Colors.GetColor(ColorName.color_neutral_60)};
            subTitle.SetBinding(Span.TextProperty, new Binding(nameof(SubTitle), source: this));
                
            label.FormattedText = new FormattedString { Spans = { title, newLine, subTitle } };
        }
        
        GridContent.Add(label);
    }

    private static void CornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not ListItem listItem)
            return;

        listItem.StrokeShape = new RoundRectangle { CornerRadius = (CornerRadius)newValue };
    }

    private static void OnContentItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not ListItem listItem || newValue is not View view)
            return;
        
        view.HorizontalOptions = LayoutOptions.End;
        listItem.GridContent.Add(view, 2);
    }
 
}