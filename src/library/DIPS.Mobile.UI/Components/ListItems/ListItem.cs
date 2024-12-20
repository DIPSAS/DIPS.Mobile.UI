using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.Labels;
using DIPS.Mobile.UI.Components.ListItems.Options;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal;
using Microsoft.Maui.Controls.Shapes;
using Colors = Microsoft.Maui.Graphics.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(InLineContent))]
public partial class ListItem : ContentView
{
    private Grid RootGrid { get; } = new()
    {
        AutomationId = "RootGrid".ToDUIAutomationId<ListItem>(),
        BackgroundColor = Colors.Transparent
    };
    
    internal Grid ContainerGrid { get; } = new()
    {
        AutomationId = "ContainerGrid".ToDUIAutomationId<ListItem>(),
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new(GridLength.Auto),
            new(GridLength.Auto),
            new(GridLength.Star)
        },
        RowDefinitions = new RowDefinitionCollection()
        {
            new(GridLength.Star),
        }
    };

    internal Grid TitleAndLabelGrid { get; } = new()
    {
        AutomationId = "TitleAndLabelGrid".ToDUIAutomationId<ListItem>(),
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new(),
        },
        RowDefinitions = new RowDefinitionCollection
        {
            new(GridLength.Star),
            new(GridLength.Auto)
        },
        VerticalOptions = LayoutOptions.Center
    };

    internal Border OuterBorder { get; } = new() { StrokeThickness = 0, AutomationId = "OuterBorder".ToDUIAutomationId<ListItem>()};
    internal Image? ImageIcon { get; private set; }
    internal Label TitleLabel { get; } = new(){AutomationId = "TitleLabel".ToDUIAutomationId<ListItem>()};
    internal Label SubtitleLabel { get; } = new() { IsVisible = false, AutomationId = "SubtitleLabel".ToDUIAutomationId<ListItem>()};
    
    private IView m_oldInLineContent;
    private IView? m_oldUnderlyingContent;

    public Divider? TopDivider;
    public Divider? BottomDivider;

    public ListItem()
    {
        ((ContentView)this).BackgroundColor = Colors.Transparent;
        
        OuterBorder.StrokeShape = new RoundRectangle 
        { 
            AutomationId = "OuterBorder.StrokeShape".ToDUIAutomationId<ListItem>(),
            CornerRadius = CornerRadius, 
            StrokeThickness = 0
        };

        ContainerGrid.SetBinding(Grid.MarginProperty, static (ListItem listItem) => listItem.Padding, source: this);
        
        BindBorder();

        OuterBorder.Content = ContainerGrid;

        ContainerGrid.Add(TitleAndLabelGrid, 1);
        RootGrid.Add(OuterBorder);
        
        this.Content = RootGrid;
    }

    private void BindBorder()
    {
        OuterBorder.SetBinding(Border.BackgroundColorProperty, static (ListItem listItem) => listItem.BackgroundColor, source: this);
        OuterBorder.SetBinding(Border.MarginProperty, static (ListItem listItem) => listItem.Margin, source: this);
    }

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName?.Equals(nameof(IsEnabled)) ?? false)
        {
            SetTouchIsEnabled();
        }
    }

    private void AddTitle()
    {
        TitleAndLabelGrid.Insert(0, TitleLabel);

        BindToOptions(TitleOptions);
        
        if(IsDebugMode)
            BindToOptions(DebuggingOptions);
    }

    private void AddSubtitle()
    {
        TitleAndLabelGrid.Add(SubtitleLabel, 0, 1);
        
        BindToOptions(SubtitleOptions);
        
        if(IsDebugMode)
            BindToOptions(DebuggingOptions);
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
        
        ContainerGrid.Add(view, ContainerGrid.ColumnDefinitions.Count - 1);
        
        BindToOptions(InLineContentOptions);

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
        OuterBorder.StrokeShape = new RoundRectangle { CornerRadius = CornerRadius, StrokeThickness = 0};
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
        Touch.SetAccessibilityContentDescription(OuterBorder, string.Join(".", Title, Subtitle));
        Touch.SetCommand(OuterBorder, new Command(() =>
        {
            Command?.Execute(CommandParameter);
            Tapped?.Invoke(this, EventArgs.Empty);
        }));
        SetTouchIsEnabled();
    }

    private void SetTouchIsEnabled() => Touch.SetIsEnabled(OuterBorder, IsEnabled && (Command is not null || Tapped?.HasSubscriptions() != null));

    private void BindToOptions(ListItemOptions? options) => options?.Bind(this);

    private void AddContextMenu()
    {
        ContextMenuEffect.SetMenu(OuterBorder, ContextMenu);
        BindToOptions(ContextMenuOptions);
    }
    
    private static void OnAutoDividerChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ListItem { AutoDivider: true } listItem)
            return;

        listItem.ParentChanged += listItem.OnParentChanged;
        if (listItem.Parent is not null)
        {
            listItem.OnParentChanged();
        }
    }
    
    
    private VerticalStackLayout? m_verticalStackLayout;
    private CollectionView? m_collectionView;
    
    private void OnParentChanged(object? sender, EventArgs e)
    {
        if (Parent is VerticalStackLayout verticalStackLayout)
        {
            m_verticalStackLayout = verticalStackLayout;
            
            m_verticalStackLayout.SizeChanged -= OnVerticalStackLayoutSizeChanged;
            m_verticalStackLayout.SizeChanged += OnVerticalStackLayoutSizeChanged;
        }
        else if (Parent is CollectionView collectionView)
        {
            m_collectionView = collectionView;
            
            m_collectionView.ChildrenReordered -= OnCollectionViewChildrenReordered;
            m_collectionView.ChildrenReordered += OnCollectionViewChildrenReordered;
            
            OnCollectionViewChildrenReordered(null, null!);
        }
    }

    private async void OnCollectionViewChildrenReordered(object? sender, EventArgs e)
    {
        await Task.Delay(1);
        
        var collectionViewItemsSource = m_collectionView!.ItemsSource.Cast<object>();

        HasTopDivider = false;

        if(!IsVisible)
            return;
        
        if (collectionViewItemsSource.FirstOrDefault() == BindingContext)
            return;

        HasTopDivider = true;
    }

    private async void OnVerticalStackLayoutSizeChanged(object? sender, EventArgs e)
    {
        HasTopDivider = false;
        
        await Task.Delay(1);
    
        if(!IsVisible)
            return;

        if (m_verticalStackLayout!.Where(item => ((item as View)!).IsVisible)
                .ToList()
                .IndexOf(this) == 0)
        {
            return;
        }

        HasTopDivider = true;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null && TitleAndLabelGrid.Children.Count == 0)
        {
            AddTitle();
            AddSubtitle();
        
            AddTouch();
            return;
        }

        ParentChanged -= OnParentChanged;
        
        if(m_collectionView is not null)
            m_collectionView.ChildrenReordered -= OnCollectionViewChildrenReordered;
        
        if(m_verticalStackLayout is not null)
            m_verticalStackLayout.SizeChanged -= OnVerticalStackLayoutSizeChanged;
    }
}