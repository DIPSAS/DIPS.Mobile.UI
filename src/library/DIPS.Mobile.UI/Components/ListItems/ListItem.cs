using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.ListItems.Options;
using DIPS.Mobile.UI.Components.ListItems.Options.Debugging;
using DIPS.Mobile.UI.Components.ListItems.Options.Icon;
using DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;
using DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;
using DIPS.Mobile.UI.Components.ListItems.Options.Title;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Internal.Logging;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(InLineContent))]
public partial class ListItem : Grid
{
    internal Image? ImageIcon { get; private set; }
    internal Label? TitleLabel { get; private set; }
    internal Label? SubtitleLabel { get; private set; } 
    
    private IView m_oldInLineContent;
    private IView? m_oldUnderlyingContent;

    public Divider? TopDivider;
    public Divider? BottomDivider;

    public ListItem()
    {
        Padding = new Thickness(
            Sizes.GetSize(SizeName.content_margin_small),
            Sizes.GetSize(SizeName.content_margin_medium),
            Sizes.GetSize(SizeName.content_margin_small),
            Sizes.GetSize(SizeName.content_margin_medium));
        
        ColumnDefinitions = [
            new ColumnDefinition(GridLength.Auto), 
            new ColumnDefinition(GridLength.Auto), 
            new ColumnDefinition(GridLength.Star)
        ];
        
        RowDefinitions = [
            new RowDefinition(GridLength.Star), 
            new RowDefinition(GridLength.Auto),
            new RowDefinition(GridLength.Auto)
        ];
        
        BackgroundColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_surface_default);
    }

    private void AddTitle()
    {
        // Guard against double creation, even though it should not happen
        if (TitleLabel is not null)
        {
            DUILogService.LogError<ListItem>("TitleLabel is already created; this method should not be called if TitleLabel is already created");
            return;
        }
        
        TitleLabel = new Label { AutomationId = "TitleLabel".ToDUIAutomationId<ListItem>() };
        
        this.Add(TitleLabel, 1);

        SetupDefaultOrBindToOptions<TitleOptions>(TitleOptions);
        
        RefreshDebugMode();
    }

    private void AddSubtitle()
    {
        // Guard against double creation, even though it should not happen
        if (SubtitleLabel is not null)
        {
            DUILogService.LogError<ListItem>("SubtitleLabel is already created; this method should not be called if SubtitleLabel is already created");
            return;
        }
        
        SubtitleLabel = new Label { AutomationId = "SubtitleLabel".ToDUIAutomationId<ListItem>()};
        
        this.Add(SubtitleLabel, 1, 1);
        
        SetupDefaultOrBindToOptions<SubtitleOptions>(SubtitleOptions);
        
        RefreshDebugMode();
        UpdateRowSpan();

        SubtitleLabel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == IsVisibleProperty.PropertyName)
            {
                UpdateRowSpan();
            }
        };
    }

    /// <summary>
    /// We need to update RowSpan on Icon and InlineContent, so that they are in the center of Title + Subtitle
    /// </summary>
    private void UpdateRowSpan()
    {
        if(SubtitleLabel is null)
            return;
        
        var rowSpan = SubtitleLabel.IsVisible ? 2 : 1;
        
        if (ImageIcon is not null)
        {
            this.SetRowSpan(ImageIcon, rowSpan);
        }
        
        if (InLineContent is not null)
        {
            this.SetRowSpan(InLineContent, rowSpan);
        }
    }

    private void AddIcon()
    {
        ImageIcon = new Image
        {
            Source = Icon
        };
        
        SetupDefaultOrBindToOptions<IconOptions>(IconOptions);
        
        this.Add(ImageIcon, 0);
        
        RefreshDebugMode();
        UpdateRowSpan();
    }

    protected virtual void AddInLineContent()
    {
        SetInLineContent(InLineContent!);
    }

    protected void SetInLineContent(IView view)
    {
        if(Contains(m_oldInLineContent))
        {
            Remove(m_oldInLineContent);
        }
        
        this.Add(view, ColumnDefinitions.Count - 1);
        
        SetupDefaultOrBindToOptions<InLineContentOptions>(InLineContentOptions);

        m_oldInLineContent = view;
        
        RefreshDebugMode();
        UpdateRowSpan();
    }

    private void AddUnderlyingContent()
    {
        if (Contains(m_oldUnderlyingContent))
        {
            Remove(m_oldUnderlyingContent);
        }
        
        this.Add(UnderlyingContent, 0, 2);
        SetColumnSpan(UnderlyingContent, ColumnDefinitions.Count);
        
        m_oldUnderlyingContent = UnderlyingContent;
        
        RefreshDebugMode();
    }
    
    private void AddDivider(bool top)
    {
        var divider = new Divider();
        if (top)
        {
            if (Contains(TopDivider))
                Remove(TopDivider);
            
            TopDivider = divider;
            TopDivider.VerticalOptions = LayoutOptions.Start;
        }
        else
        {
            if (Contains(BottomDivider))
                Remove(BottomDivider);
            
            BottomDivider = divider;
            BottomDivider.VerticalOptions = LayoutOptions.End;
        }

        this.SetRowSpan(divider, RowDefinitions.Count);
        this.SetColumnSpan(divider, ColumnDefinitions.Count);
        
        Add(divider);

        SetupDefaultOrBindToOptions<Options.Dividers.DividersOptions>(DividersOptions);
    }
    
    private void AddTouch()
    {
        Touch.SetAccessibilityContentDescription(this, string.Join(".", Title, Subtitle));
        Touch.SetCommand(this, new Command(() =>
        {
            Command?.Execute(CommandParameter);
            Tapped?.Invoke(this, EventArgs.Empty);
        }));
    }

    /// <summary>
    /// E.g if TitleOptions is set in xaml before Title, the TitleOptions will not be null, and should bind the properties to the Label instantly
    /// </summary>
    private void SetupDefaultOrBindToOptions<T>(ListItemOptions? options) where T : ListItemOptions, new()
    {
        if (options is null)
        {
            var option = new T();
            option.SetupDefaults(this);
        }
        else
        {
            options.Bind(this);
        }
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
    
    private void RefreshDebugMode()
    {
        SetupDefaultOrBindToOptions<DebuggingOptions>(DebuggingOptions);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        ParentChanged -= OnParentChanged;
        
        if(m_collectionView is not null)
            m_collectionView.ChildrenReordered -= OnCollectionViewChildrenReordered;
        
        if(m_verticalStackLayout is not null)
            m_verticalStackLayout.SizeChanged -= OnVerticalStackLayoutSizeChanged;
    }
}