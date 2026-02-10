using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.ListItems.Options;
using DIPS.Mobile.UI.Components.ListItems.Options.Icon;
using DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;
using DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;
using DIPS.Mobile.UI.Components.ListItems.Options.Title;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Internal.Logging;
using Colors = Microsoft.Maui.Graphics.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(InLineContent))]
public partial class ListItem : Grid
{
    internal Image? ImageIcon { get; private set; }
    internal Label? TitleLabel { get; private set; }
    internal Label? SubtitleLabel { get; private set; } 
    internal Divider? TopDivider { get; private set; }
    internal Divider? BottomDivider { get; private set; }
    
    private IView m_oldInLineContent;
    private IView? m_oldUnderlyingContent;

    private readonly Grid m_titleAndSubtitleContainer = new()
    {
        AutomationId = "TitleAndSubtitleContainer".ToDUIAutomationId<ListItem>(),
        VerticalOptions = LayoutOptions.Center,
        RowDefinitions =
            new RowDefinitionCollection(new RowDefinition(GridLength.Star), new RowDefinition(GridLength.Auto))
    };

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
            new RowDefinition(GridLength.Auto), 
            new RowDefinition(GridLength.Auto)
        ];

        BackgroundColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_surface_default);
        
        this.Add(m_titleAndSubtitleContainer, 1);
    }

    private void AddTitle()
    {
        if (TitleLabel is not null)
            return;
        
        TitleLabel = new Label { AutomationId = "TitleLabel".ToDUIAutomationId<ListItem>() };
        TitleLabel.SetBinding(Label.TextProperty, static (ListItem listItem) => listItem.Title, source: this);
        
        m_titleAndSubtitleContainer.Add(TitleLabel);

        SetDefaultValuesOrBindToOptions(TitleOptions, () =>
        {
            TitleOptions.SetDefaultValues(this);
        });
    }

    private void AddSubtitle()
    {
        if (SubtitleLabel is not null)
            return;
        
        SubtitleLabel = new Label { AutomationId = "SubtitleLabel".ToDUIAutomationId<ListItem>() };
        SubtitleLabel.SetBinding(Label.TextProperty, static(ListItem listItem) => listItem.Subtitle, source: this);
        
        m_titleAndSubtitleContainer.Add(SubtitleLabel, 0, 1);

        SetDefaultValuesOrBindToOptions(SubtitleOptions, () =>
        {
            SubtitleOptions.SetupDefaults(this);
        });
    }

    private void AddIcon()
    {
        if (ImageIcon is not null)
            return;
        
        ImageIcon = new Image();
        ImageIcon.SetBinding(Microsoft.Maui.Controls.Image.SourceProperty, static (ListItem listItem) => listItem.Icon, source: this);

        SetDefaultValuesOrBindToOptions(IconOptions, () =>
        {
            IconOptions.SetupDefaults(this);
        });
        
        this.Add(ImageIcon, 0);
        
        UpdateInternalAccessibility();
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

        SetDefaultValuesOrBindToOptions(InLineContentOptions, () =>
        {
            InLineContentOptions.SetupDefaults(this);
        });

        m_oldInLineContent = view;
    }

    private void AddUnderlyingContent()
    {
        if (Contains(m_oldUnderlyingContent))
        {
            Remove(m_oldUnderlyingContent);
        }
        
        this.Add(UnderlyingContent, 0, 1);
        SetColumnSpan(UnderlyingContent, ColumnDefinitions.Count);
        
        m_oldUnderlyingContent = UnderlyingContent;
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

        SetDefaultValuesOrBindToOptions(DividersOptions, () =>
        {
            Options.Dividers.DividersOptions.SetupDefaults(this);
        });
    }
    
    private void AddTouch()
    {
        if(Touch.GetCommand(this) is not null)
            return;
        
        Touch.SetCommand(this, new Command(() =>
        {
            Command?.Execute(CommandParameter);
            Tapped?.Invoke(this, EventArgs.Empty);
        }));
    }

    /// <summary>
    /// E.g if TitleOptions is set in xaml before Title, the TitleOptions will not be null, and should bind the properties to the Label instantly
    /// <remarks>If options is set after the corresponding view has been created, they will run <see cref="Bind{T}"/> themselves</remarks>
    /// </summary>
    private void SetDefaultValuesOrBindToOptions(ListItemOptions? options, Action onDefault)
    {
        if (options is null)
        {
            onDefault.Invoke();
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

    private void UpdateInternalAccessibility()
    {
        // When DisableInternalAccessibility is true, exclude internal elements from accessibility tree
        // This allows interactive elements (like Switch, Button) to receive focus directly
        if (m_titleAndSubtitleContainer is not null)
        {
            AutomationProperties.SetExcludedWithChildren(m_titleAndSubtitleContainer, DisableInternalAccessibility);
        }

        if (ImageIcon is not null)
        {
            AutomationProperties.SetExcludedWithChildren(ImageIcon, DisableInternalAccessibility);
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

    private void SetDebugMode()
    {
        if (TitleLabel is not null)
        {
            TitleLabel.BackgroundColor = Colors.Red;
        }
            
        if (SubtitleLabel is not null)
        {
            SubtitleLabel.BackgroundColor = Colors.Pink;
        }
            
        if (InLineContent is not null)
        {
            ((InLineContent as View)!).BackgroundColor = Colors.Green;
        }
            
        if (UnderlyingContent is not null)
        {
            ((UnderlyingContent as View)!).BackgroundColor = Colors.Blue;
        }

        if (ImageIcon is not null)
        {
            ImageIcon.BackgroundColor = Colors.Yellow;
        }
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
        {
            if (IsDebugMode)
            {
                SetDebugMode();
            }

            if (Tapped is not null && Tapped.HasSubscriptions())
            {
                AddTouch();
            }
            
            return;
        }
        
        ParentChanged -= OnParentChanged;
        
        if(m_collectionView is not null)
            m_collectionView.ChildrenReordered -= OnCollectionViewChildrenReordered;
        
        if(m_verticalStackLayout is not null)
            m_verticalStackLayout.SizeChanged -= OnVerticalStackLayoutSizeChanged;
        
        // Clean up all options
        SubtitleOptions?.Unbind();
        TitleOptions?.Unbind();
        IconOptions?.Unbind();
        InLineContentOptions?.Unbind();
    }
}