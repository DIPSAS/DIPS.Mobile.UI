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
    private VerticalStackLayout RootVerticalStackLayout { get; } = new()
    {
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent, 
        Spacing = 0
    };
    
    internal Grid ContainerGrid { get; } = new()
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
        Margin = new Thickness(Sizes.GetSize(SizeName.size_4), 
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
    
    internal Border Border { get; } = new();
    internal Image? ImageIcon { get; private set; }
    internal Label? TitleLabel { get; private set; }
    internal Label? SubtitleLabel { get; private set; }
    
    private IView m_oldInLineContent;
    private IView m_oldUnderlyingContent;

    public Divider? TopDivider;
    public Divider? BottomDivider;

    public ListItem()
    {
        ((ContentView)this).BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        
        Border.StrokeShape = new RoundRectangle 
        { 
            CornerRadius = CornerRadius, 
            StrokeThickness = 0 
        };
        
        BindBorder();

        Border.Content = ContainerGrid;

        ContainerGrid.Add(m_titleAndLabelGrid, 1);
        RootVerticalStackLayout.Add(Border);
        
        this.Content = RootVerticalStackLayout;
    }

    private void BindBorder()
    {
        Border.SetBinding(Border.BackgroundColorProperty, new Binding {Source = this, Path = nameof(BackgroundColor)});
        Border.SetBinding(Border.MarginProperty, new Binding {Source = this, Path = nameof(Margin)});
        Border.SetBinding(Border.PaddingProperty, new Binding {Source = this, Path = nameof(Padding)});
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
        if (ContainerGrid.Contains(ImageIcon))
        {
            ContainerGrid.Remove(ImageIcon);
        }
        
        ImageIcon = new Image
        {
            Source = Icon
        };
        
        BindToOptions(IconOptions);
        
        ContainerGrid.Add(ImageIcon, 0);
    }

    protected virtual void AddInLineContent()
    {
        SetInLineContent(InLineContent);
    }

    protected void SetInLineContent(IView view)
    {
        if(ContainerGrid.Contains(m_oldInLineContent))
        {
            ContainerGrid.Remove(m_oldInLineContent);
        }
        
        BindToOptions(InLineContentOptions);

        ContainerGrid.Add(view, ContainerGrid.ColumnDefinitions.Count - 1);

        m_oldInLineContent = view;
    }

    private void AddUnderlyingContent()
    {
        if (ContainerGrid.Contains(m_oldUnderlyingContent))
        {
            ContainerGrid.Remove(m_oldUnderlyingContent);
        }
        else
        {
            ContainerGrid.AddRowDefinition(new RowDefinition(GridLength.Auto));
        }
        
        ContainerGrid.Add(UnderlyingContent, 0, 1);
        ContainerGrid.SetColumnSpan(UnderlyingContent, ContainerGrid.ColumnDefinitions.Count);
        
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
            if (RootVerticalStackLayout.Contains(TopDivider))
                RootVerticalStackLayout.Remove(TopDivider);
            
            TopDivider = divider;
            RootVerticalStackLayout.Insert(0, divider);
        }
        else
        {
            if (RootVerticalStackLayout.Contains(BottomDivider))
                RootVerticalStackLayout.Remove(BottomDivider);
            
            BottomDivider = divider;
            RootVerticalStackLayout.Add(divider);
        }
        
        BindToOptions(DividersOptions);
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