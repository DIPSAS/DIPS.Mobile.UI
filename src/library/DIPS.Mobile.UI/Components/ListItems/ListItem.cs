using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.ListItems.Options;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Controls.Shapes;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(InLineContent))]
public partial class ListItem : ContentView
{
    private Grid RootGrid { get; } = new()
    {
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent, 
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
        Margin = new Thickness(Sizes.GetSize(SizeName.size_3), 
            Sizes.GetSize(SizeName.size_3),
            Sizes.GetSize(SizeName.size_3),
            Sizes.GetSize(SizeName.size_3))
    };

    internal Grid TitleAndLabelGrid { get; } = new()
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
    internal Label TitleLabel { get; private set; } = new();
    internal Label? SubtitleLabel { get; private set; }
    
    private IView m_oldInLineContent;
    private IView? m_oldUnderlyingContent;

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

        ContainerGrid.Add(TitleAndLabelGrid, 1);
        RootGrid.Add(Border);
        
        TitleAndLabelGrid.Add(TitleLabel);
        
        this.Content = RootGrid;
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
        if (TitleAndLabelGrid.Contains(TitleLabel))
        {
            TitleAndLabelGrid.Remove(TitleLabel);
        }
        
        TitleLabel = new Label
        {
            Text = Title
        };
        
        BindToOptions(TitleOptions);

        TitleAndLabelGrid.Insert(0, TitleLabel);
        
        UpdateTitleSubtitleLogic();
        
        if(IsDebugMode)
            BindToOptions(DebuggingOptions);
    }

    private void AddSubtitle()
    {
        if (TitleAndLabelGrid.Contains(SubtitleLabel))
        {
            TitleAndLabelGrid.Remove(SubtitleLabel);
        }

        SubtitleLabel = new Label
        {
            Text = Subtitle
        };
        
        BindToOptions(SubtitleOptions);

        TitleAndLabelGrid.Add(SubtitleLabel, 0, 1);
        
        UpdateTitleSubtitleLogic();
        
        if(IsDebugMode)
            BindToOptions(DebuggingOptions);
    }

    private void UpdateTitleSubtitleLogic()
    {
        if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Subtitle))
        {
            TitleOptions.VerticalTextAlignment = TextAlignment.End;
            TitleAndLabelGrid.SetRowSpan(TitleLabel, 1);
        }
        else if (!string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Subtitle))
        {
            TitleOptions.VerticalTextAlignment = TextAlignment.Center;
            TitleAndLabelGrid.SetRowSpan(TitleLabel, 2);
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
        
        if(IsDebugMode)
            BindToOptions(DebuggingOptions);
    }

    protected virtual void AddInLineContent()
    {
        SetInLineContent(InLineContent!);
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
        
        if(IsDebugMode)
            BindToOptions(DebuggingOptions);
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
        
        if(IsDebugMode)
            BindToOptions(DebuggingOptions);
    }

    private void SetCornerRadius()
    {
        Border.StrokeShape = new RoundRectangle { CornerRadius = CornerRadius, StrokeThickness = 0};
    }
    
    private void AddDivider(bool top)
    {
        var divider = new Divider();
        if (top)
        {
            if (RootGrid.Contains(TopDivider))
                RootGrid.Remove(TopDivider);
            
            TopDivider = divider;
            TopDivider.VerticalOptions = LayoutOptions.Start;
            RootGrid.Add(divider);
        }
        else
        {
            if (RootGrid.Contains(BottomDivider))
                RootGrid.Remove(BottomDivider);
            
            BottomDivider = divider;
            BottomDivider.VerticalOptions = LayoutOptions.End;
            RootGrid.Add(divider);
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

    private void AddContextMenu()
    {
        ContextMenuEffect.SetMenu(Border, ContextMenu);
        BindToOptions(ContextMenuOptions);
    }
}