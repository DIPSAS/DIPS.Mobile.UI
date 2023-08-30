using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.ListItems.Options;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(InLineContent))]
public partial class ListItem : ContentView
{
    private VerticalStackLayout RootContent { get; } = new()
    {
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent, 
        Spacing = 0
    };
    
    internal Grid MainContent { get; } = new()
    {
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new(GridLength.Auto),
            new(GridLength.Auto),
            new(GridLength.Star)
        },
        RowDefinitions = new RowDefinitionCollection()
        {
            new(GridLength.Auto),
        },
        Padding = new Thickness(Sizes.GetSize(SizeName.size_4), 
            Sizes.GetSize(SizeName.size_3),
            Sizes.GetSize(SizeName.size_4),
            Sizes.GetSize(SizeName.size_3))
    };

    private readonly Grid m_titleAndLabelGrid = new()
    {
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new(),
        },
        RowDefinitions = new RowDefinitionCollection
        {
            new(),
            new()
        },
        VerticalOptions = LayoutOptions.Center
    };
    
    public Border Border { get; } = new();
    internal Image ImageIcon { get; private set; }
    internal Label TitleLabel { get; private set; }
    internal Label SubtitleLabel { get; private set; }
    
    private IView m_oldInLineContent;
    private IView m_oldUnderlyingContent;

    private Divider? m_topDivider;
    private Divider? m_bottomDivider;

    public ListItem()
    {
        Border.StrokeShape = new RoundRectangle 
        { 
            CornerRadius = CornerRadius, 
            StrokeThickness = 0 
        };
        
        BackgroundColor = Colors.GetColor(ColorName.color_system_white);
        Border.SetBinding(Border.BackgroundColorProperty, new Binding { Source = this, Path = nameof(BackgroundColor)} );
        
        Border.Content = MainContent;

        MainContent.Add(m_titleAndLabelGrid, 1);
        RootContent.Add(Border);
        
        this.Content = RootContent;
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

#if __ANDROID__
        // To remove margin around border, will be fixed: https://github.com/dotnet/maui/pull/14402
        Border.StrokeThickness = 0;
#endif
        
        AddTouch();
    }

    private void AddTitle()
    {
        if (m_titleAndLabelGrid.Contains(TitleLabel))
        {
            m_titleAndLabelGrid.Remove(TitleLabel);
        }
        
        TitleLabel = new Label
        {
            Text = Title
        };
        
        BindToOptions(TitleOptions);

        m_titleAndLabelGrid.Insert(0, TitleLabel);
        
        UpdateTitleSubtitleLogic();
    }

    private void AddSubtitle()
    {
        if (m_titleAndLabelGrid.Contains(SubtitleLabel))
        {
            m_titleAndLabelGrid.Remove(SubtitleLabel);
        }

        SubtitleLabel = new Label
        {
            Text = Subtitle
        };
        
        BindToOptions(SubtitleOptions);

        m_titleAndLabelGrid.Add(SubtitleLabel, 0, 1);
        
        UpdateTitleSubtitleLogic();
    }

    private void UpdateTitleSubtitleLogic()
    {
        if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Subtitle))
        {
            TitleOptions.VerticalTextAlignment = TextAlignment.End;
            m_titleAndLabelGrid.SetRowSpan(TitleLabel, 1);
        }
        else if (!string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Subtitle))
        {
            TitleOptions.VerticalTextAlignment = TextAlignment.Center;
            m_titleAndLabelGrid.SetRowSpan(TitleLabel, 2);
        }
    }
    
    private void AddIcon()
    {
        if (MainContent.Contains(ImageIcon))
        {
            MainContent.Remove(ImageIcon);
        }
        
        ImageIcon = new Image
        {
            Source = Icon
        };
        
        BindToOptions(IconOptions);
        
        MainContent.Add(ImageIcon, 0);
    }

    protected virtual void AddInLineContent()
    {
        SetInLineContent(InLineContent);
    }

    protected void SetInLineContent(IView view)
    {
        if(MainContent.Contains(m_oldInLineContent))
        {
            MainContent.Remove(m_oldInLineContent);
        }
        
        BindToOptions(InLineContentOptions);

        MainContent.Add(view, MainContent.ColumnDefinitions.Count - 1);

        m_oldInLineContent = view;
    }

    private void AddUnderlyingContent()
    {
        if (MainContent.Contains(m_oldUnderlyingContent))
        {
            MainContent.Remove(m_oldUnderlyingContent);
        }
        else
        {
            MainContent.AddRowDefinition(new RowDefinition(GridLength.Auto));
        }
        
        MainContent.Add(UnderlyingContent, 0, 1);
        MainContent.SetColumnSpan(UnderlyingContent, MainContent.ColumnDefinitions.Count);
        
        m_oldUnderlyingContent = UnderlyingContent;
    }

    private void SetCornerRadius()
    {
        Border.StrokeShape = new RoundRectangle { CornerRadius = CornerRadius };
    }
    
    private void AddDivider(bool top)
    {
        var divider = new Divider();
        if (top)
        {
            if (RootContent.Contains(m_topDivider))
                RootContent.Remove(m_topDivider);
            
            m_topDivider = divider;
            RootContent.Insert(0, divider);
        }
        else
        {
            if (RootContent.Contains(m_bottomDivider))
                RootContent.Remove(m_bottomDivider);
            
            m_bottomDivider = divider;
            RootContent.Add(divider);
        }
    }

    private void AddTouch()
    {
        Touch.SetAccessibilityContentDescription(Border, string.Join(".", Title, Subtitle));
        Touch.SetCommand(Border, new Command(() =>
        {
            Command?.Execute(CommandParameter);
            Tapped?.Invoke(this, EventArgs.Empty);
        }));
        SetTouchIsEnabled();
    }

    private void SetTouchIsEnabled() => Touch.SetIsEnabled(Border, IsEnabled && Command is not null);

    private void BindToOptions(ListItemOptions? options) => options?.Bind(this);
}