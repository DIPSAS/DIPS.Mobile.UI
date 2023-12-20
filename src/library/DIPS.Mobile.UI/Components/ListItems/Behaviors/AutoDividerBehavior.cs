namespace DIPS.Mobile.UI.Components.ListItems.Behaviors;

public class AutoDividerBehavior : Behavior<ListItem>
{
#nullable disable
    private ListItem m_listItem;
#nullable enable
    
    private VerticalStackLayout? m_verticalStackLayout;
    private CollectionView? m_collectionView;
    
    protected override void OnAttachedTo(ListItem listItem)
    {
        base.OnAttachedTo(listItem);

        m_listItem = listItem;

    }

    private void OnParentChanged(object? sender, EventArgs e)
    {
        if (m_listItem.Parent is VerticalStackLayout verticalStackLayout)
        {
            m_verticalStackLayout = verticalStackLayout;
            
            m_verticalStackLayout.SizeChanged -= OnVerticalStackLayoutSizeChanged;
            m_verticalStackLayout.SizeChanged += OnVerticalStackLayoutSizeChanged;
        }
        else if (m_listItem.Parent is CollectionView collectionView)
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

        m_listItem.HasTopDivider = false;

        if(!m_listItem.IsVisible)
            return;
        
        if (collectionViewItemsSource.FirstOrDefault() == m_listItem.BindingContext)
            return;

        m_listItem.HasTopDivider = true;
    }

    private async void OnVerticalStackLayoutSizeChanged(object? sender, EventArgs e)
    {
        m_listItem.HasTopDivider = false;
        
        await Task.Delay(1);
    
        if(!m_listItem.IsVisible)
            return;

        if (m_verticalStackLayout!.Where(item => ((item as View)!).IsVisible)
                .ToList()
                .IndexOf(m_listItem) == 0)
        {
            return;
        }

        m_listItem.HasTopDivider = true;
    }

    protected override void OnDetachingFrom(ListItem listItem)
    {
        base.OnDetachingFrom(listItem);
        
        m_listItem.ParentChanged -= OnParentChanged;
        
        if(m_collectionView is not null)
            m_collectionView.ChildrenReordered -= OnCollectionViewChildrenReordered;
        
        if(m_verticalStackLayout is not null)
            m_verticalStackLayout.SizeChanged -= OnVerticalStackLayoutSizeChanged;
    }
}