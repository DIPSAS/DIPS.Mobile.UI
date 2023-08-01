using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(HorizontalContentItem))]
public partial class ListItem : Border
{
    protected Grid MainContent { get; }
    private VerticalStackLayout RootContent { get; } =
        new() { BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent };

    private Image m_icon = new() { Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.size_4), 0) };

    public ListItem()
    {
        StrokeShape = new RoundRectangle 
        { 
            CornerRadius = CornerRadius, 
            StrokeThickness = 0 
        };
        
        MainContent = new Grid 
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new(TitleColumnWidth),
                new(HorizontalContentItemColumnWidth)
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
        RootContent.Add(MainContent);
        Content = RootContent;
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName.Equals(nameof(IsEnabled)))
        {
            SetTouchIsEnabled();
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        AddLabel();

#if __ANDROID__
        // To remove margin around border, will be fixed: https://github.com/dotnet/maui/pull/14402
        StrokeThickness = 0;
#endif

        if (HasTopDivider)
        {
            AddDivider(0);
        }

        if (HasBottomDivider)
        {
            AddDivider(RootContent.Count);
        }
        
        AddTouch();
        
        if(Icon is not null)
            AddIcon();
    }

    private void AddLabel()
    {
        var label = new Label 
        { 
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Start
        };
        
        if (string.IsNullOrEmpty(Subtitle))
        {
            label.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
            label.SetBinding(Label.FontAttributesProperty, new Binding(nameof(TitleFontAttributes), source: this));
            label.SetBinding(Label.FontSizeProperty, new Binding(nameof(TitleFontSize), source: this));
            label.SetBinding(Label.TextColorProperty, new Binding(nameof(TitleTextColor), source: this));
        }
        else
        {
            var title = new Span();
            title.SetBinding(Span.TextProperty, new Binding(nameof(Title), source: this));
            title.SetBinding(Span.FontAttributesProperty, new Binding(nameof(TitleFontAttributes), source: this));
            label.SetBinding(Span.FontSizeProperty, new Binding(nameof(TitleFontSize), source: this));
            label.SetBinding(Span.TextColorProperty, new Binding(nameof(TitleTextColor), source: this));

            var newLine = new Span { Text = Environment.NewLine };

            var subTitle = new Span { FontSize = Sizes.GetSize(SizeName.size_3), TextColor = Colors.GetColor(ColorName.color_neutral_60)};
            subTitle.SetBinding(Span.TextProperty, new Binding(nameof(Subtitle), source: this));
            subTitle.SetBinding(Span.FontAttributesProperty, new Binding(nameof(SubtitleFontAttributes), source: this));

                
            label.FormattedText = new FormattedString { Spans = { title, newLine, subTitle }};
        }

        label.Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.size_3), 0);
        
        MainContent.Add(label);
    }
    
    private void AddIcon()
    {
        if (Icon is null || Handler == null)
        {
            if (MainContent.Contains(m_icon))
            {
                MainContent.Remove(m_icon);
                MainContent.ColumnDefinitions.RemoveAt(0);
                ShiftChildrenColumns(-1);
            }
            return;
        }
        
        ShiftChildrenColumns(1);

        m_icon.Source = Icon;
        m_icon.SetBinding(Image.TintColorProperty, new Binding(nameof(IconColor), source: this));
        m_icon.SetBinding(Image.IsVisibleProperty, new Binding(nameof(IsIconVisible), source: this));
        
        MainContent.Add(m_icon, 0);
        MainContent.ColumnDefinitions.Insert(0, new ColumnDefinition(GridLength.Auto));
        
    }

    private void ShiftChildrenColumns(int index)
    {
        foreach (var view in MainContent.Children)
        {
            MainContent.SetColumn(view, MainContent.GetColumn(view) + index);
        }
    }

    private static void CornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not ListItem listItem)
            return;

        listItem.StrokeShape = new RoundRectangle { CornerRadius = (CornerRadius)newValue };
    }

    private static void OnHorizontalContentItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not ListItem listItem || newValue is not View view)
            return;

        if (listItem.ShouldOverrideContentItemLayoutOptions)
        {
            view.HorizontalOptions = LayoutOptions.End;
            view.VerticalOptions = LayoutOptions.Center;
        }

        var existingViewInColumn = listItem.MainContent.Children.FirstOrDefault(child => (listItem.MainContent.GetColumn(child) == 1));
        if(existingViewInColumn is not null)
        {
            listItem.MainContent.Remove(existingViewInColumn);
        }
        
        listItem.MainContent.Add(view, 1);
    }
    
    private static void OnVerticalContentItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not ListItem listItem || newValue is not View view)
            return;

        if (listItem.ShouldOverrideContentItemLayoutOptions)
        {
            view.HorizontalOptions = LayoutOptions.Start;
            view.VerticalOptions = LayoutOptions.Center;
        }
        
        listItem.MainContent.AddRowDefinition(new RowDefinition(GridLength.Auto));
        listItem.MainContent.Add(view, 0, 1);
        Grid.SetColumnSpan(view, 2);
    }

    private void AddDivider(int row)
    {
        var divider = new Divider();
        if (row > RootContent.Count - 1)
        {
            RootContent.Add(divider);
        }
        else
        {
            RootContent.Insert(row, divider);
        }
    }

    private void AddTouch()
    {
        Touch.SetAccessibilityContentDescription(this, string.Join(".", Title, Subtitle));
        Touch.SetCommand(this, new Command(() =>
        {
            Command?.Execute(CommandParameter);
            Tapped?.Invoke(this, EventArgs.Empty);
        }));
        SetTouchIsEnabled();
    }

    private void SetTouchIsEnabled() => Touch.SetIsEnabled(this, IsEnabled && Command is not null);

    private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not ListItem listItem)
            return;

        Touch.SetIsEnabled(listItem, newValue is not null);
    }

    private void OnHorizontalContentItemColumnWidthChanged()
    {
        MainContent.ColumnDefinitions.Last().Width = HorizontalContentItemColumnWidth;
    }

    private void OnTitleColumnWidthChanged()
    {
        MainContent.ColumnDefinitions.First().Width = TitleColumnWidth;
    }
}